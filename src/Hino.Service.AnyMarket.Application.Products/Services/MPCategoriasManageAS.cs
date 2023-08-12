using Hino.Service.AnyMarket.Application.Core.External.AnyMarket;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto;
using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using Hino.Service.AnyMarket.Application.Core.Interfaces;
using Hino.Service.AnyMarket.Application.Products.Interfaces;
using Hino.Service.AnyMarket.Domain.Products.Interfaces.Services.Marketplace.Categorias;
using Hino.Service.AnyMarket.Entities.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Products.Services
{
    public class MPCategoriasManageAS: IMPCategoriasManageAS
    {
        public List<string> Errors
        {
            get
            {
                var list = new List<string>();
                list.AddRange(MPCategoriasManageService.Errors.ToArray());
                list.AddRange(MPCategoriasUploadService.Errors.ToArray());
                list.AddRange(MPCategoriasDownloadService.Errors.ToArray());

                return list;
            }
        }

        readonly IMPCategoriasManageService MPCategoriasManageService;
        readonly IMPCategoriasUploadService MPCategoriasUploadService;
        readonly IMPCategoriasDownloadService MPCategoriasDownloadService;
        readonly Api ApiAnyMarket;

        public MPCategoriasManageAS(IMPCategoriasManageService mPCategoriasManageService,
            IMPCategoriasUploadService mPCategoriasUploadService,
            IMPCategoriasDownloadService mPCategoriasDownloadService,
            Api apiAnyMarket)
        {
            ApiAnyMarket = apiAnyMarket;
            MPCategoriasManageService = mPCategoriasManageService;
            MPCategoriasUploadService = mPCategoriasUploadService;
            MPCategoriasDownloadService = mPCategoriasDownloadService;
        }

        public async Task ManageUploadAsync(CancellationToken cancellation)
        {
            MPCategoriasManageService.Errors.Clear();
            MPCategoriasUploadService.Errors.Clear();
            MPCategoriasDownloadService.Errors.Clear();

            Logs.Logger.LogInformation($"Buscando as categorias do ERP");
            var MarcasToUpload = await MPCategoriasManageService.GetCategoriasToUploadAsync(cancellation);

            if (MPCategoriasManageService.Errors.Any())
                return;

            Logs.Logger.LogInformation($"Gerando as categorias na MPCategorias");
            await MPCategoriasManageService.GenerateCategoriasAsync(cancellation, MarcasToUpload.ToList());

            if (MPCategoriasManageService.Errors.Any())
                return;

            Logs.Logger.LogInformation($"Buscando as categorias para serem atualizadas no AnyMarket");

            var CategoriasPendentes = await MPCategoriasUploadService.GetCategoriasToUploadAsync(cancellation);

            if (MPCategoriasUploadService.Errors.Any())
                return;

            var CategoriasSalvas = new List<MPCategorias>();

            if (CategoriasPendentes.Any())
            {
                Logs.Logger.LogInformation($"Comunicando com a API do AnyMarket para envio das categorias");
                CategoriasSalvas = (await SendCategoriasAPIAsync(cancellation, CategoriasPendentes)).ToList();
            }

            MPCategoriasManageService.Errors.Clear();

            if (CategoriasSalvas.Any() && !Errors.Any())
            {
                Logs.Logger.LogInformation($"Atualizando a MPCategorias com o retorno da AnyMarket");
                await MPCategoriasDownloadService.UpdateERPAsync(cancellation, CategoriasSalvas);

                if (MPCategoriasDownloadService.Errors.Any())
                    return;
            }
        }

        async Task<long> SendCategoryAPIAsync(CancellationToken cancellation, CategoryDTO category)
        {
            try
            {
                ResponseBaseDTO<CategoryDTO> Result;

                Result = await ApiAnyMarket.Categories.GetByIdAsync<CategoryDTO>(cancellation, (category.id ?? 0).ToString());

                if (Result.Items == null)
                {
                    category.id = null;
                    Result = await ApiAnyMarket.Categories.PostAsync(cancellation, category);
                }
                else
                    Result = await ApiAnyMarket.Categories.PutAsync(cancellation, category.id.ToString(), category);

                await Task.Delay(200);

                ApiAnyMarket.Categories.GenerateLogResult(Result);

                if (Result.IsSuccessful && Result.Items?.Length > 0)
                    category.id = Result.Items[0].id;
            }
            catch (Exception ex)
            {
                Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.Categories.URN} do recurso: {ApiAnyMarket.Categories.CurrentResource}, Exception: {ex.Message}", ex);
            }

            return category.id ?? 0;
        }

        public async Task<IEnumerable<MPCategorias>> SendCategoriasAPIAsync(CancellationToken cancellation, IEnumerable<MPCategorias> categoriasToSend)
        {

            if (categoriasToSend.Any())
            {
                var categories = CategoryDTO.GenerateTree(categoriasToSend.ToArray());

                foreach (var categoria in categories.Categorias)
                {
                    categoria.id = await SendCategoryAPIAsync(cancellation, categoria);

                    if (categoria.id > 0)
                    {
                        categoriasToSend
                            .Where(x => $"CAT_{x.CODCATEGORIA}" == categoria.partnerId)
                            .ToList()
                            .ForEach(x => x.IDCATEGNV4 = categoria.id);

                        foreach (var classe in categories.Classes
                            .Where(r => r.parent.parentId == categoria.partnerId))
                        {
                            classe.parent.id = categoria.id ?? 0;

                            classe.id = await SendCategoryAPIAsync(cancellation, classe);

                            if (classe.id > 0)
                            {
                                categoriasToSend
                                    .Where(x => $"CLA_{x.CODCLASSE}" == classe.partnerId)
                                    .ToList()
                                    .ForEach(x => x.IDCATEGNV3 = classe.id);

                                foreach (var grupo in categories.Grupos
                                    .Where(r => r.parent.parentId == classe.partnerId))
                                {
                                    grupo.parent.id = classe.id ?? 0;

                                    grupo.id = await SendCategoryAPIAsync(cancellation, grupo);

                                    if (grupo.id > 0)
                                    {
                                        categoriasToSend
                                            .Where(x => $"GRU_{x.CODGRUPO}" == grupo.partnerId)
                                            .ToList()
                                            .ForEach(x => x.IDCATEGNV2 = grupo.id);

                                        foreach (var familia in categories.Familias
                                            .Where(r => r.parent.parentId == grupo.partnerId))
                                        {
                                            familia.parent.id = grupo.id ?? 0;

                                            familia.id = await SendCategoryAPIAsync(cancellation, familia);

                                            categoriasToSend
                                                .Where(x => $"FAM_{x.CODFAMILIA}" == familia.partnerId)
                                                .ToList()
                                                .ForEach(x => x.IDCATEGNV1 = familia.id);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

               /* foreach (var c in categoriasToSend)
                {
                    var categoriesDTO = CategoryDTO.FromEntity(c);

                    CategoryParentDTO? prevCateg = null;

                    foreach (var category in categoriesDTO.Reverse())
                    {
                        try
                        {
                            ResponseBaseDTO<CategoryDTO> Result;

                            Result = await ApiAnyMarket.Categories.GetByIdAsync<CategoryDTO>(cancellation, (category.id ?? 0).ToString());

                            if (prevCateg != null)
                                category.parent = prevCateg;
                            if (Result.Items == null)
                            {
                                category.id = null;
                                Result = await ApiAnyMarket.Categories.PostAsync(cancellation, category);
                            }
                            else
                                Result = await ApiAnyMarket.Categories.PutAsync(cancellation, category.id.ToString(), category);

                            await Task.Delay(200);

                            ApiAnyMarket.Categories.GenerateLogResult(Result);

                            if (Result.IsSuccessful && Result.Items?.Length > 0)
                            {
                                category.id = Result.Items[0].id;
                                prevCateg = new CategoryParentDTO
                                {
                                    id = (category.id ?? 0)
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            Logs.Logger.LogError($"Falha na URN:{ApiAnyMarket.Categories.URN} do recurso: {ApiAnyMarket.Categories.CurrentResource}, Exception: {ex.Message}", ex);
                        }
                    }

                    var Converted = CategoryDTO.ToEntity(categoriesDTO);

                    c.IDCATEGNV1 = Converted.IDCATEGNV1 ?? null;
                    c.IDCATEGNV2 = Converted.IDCATEGNV2 ?? null;
                    c.IDCATEGNV3 = Converted.IDCATEGNV3 ?? null;
                    c.IDCATEGNV4 = Converted.IDCATEGNV4 ?? null;
                }*/
            }

            return categoriasToSend;
        }

        public void Dispose()
        {
            MPCategoriasManageService?.Dispose();
            MPCategoriasUploadService?.Dispose();
            MPCategoriasDownloadService?.Dispose();
        }
    }
}
