using Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Application.Core.External.AnyMarket.Dto
{
    public class FeedOrderDTO : BaseResourceDTO
    {
        public string token { get; set; }
    }
}
