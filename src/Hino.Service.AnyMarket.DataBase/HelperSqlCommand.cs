using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase
{
    internal static class HelperSqlCommand
    {
        public static async Task<int> ExecuteSqlCommandAsync(this DbContext pContext, string query, params object[] parameters)
        {
            using var command = pContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            pContext.Database.OpenConnection();

            var result = await command.ExecuteNonQueryAsync();

            pContext.Database.CloseConnection();

            return result;
        }

        public static object RawSqlQuery(this DbContext pContext, string query, params object[] parameters)
        {
            using var command = pContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            pContext.Database.OpenConnection();

            var result = command.ExecuteScalar();

            pContext.Database.CloseConnection();

            return result;
        }

        public static async Task<object> RawSqlQueryAsync(this DbContext pContext, string query, params object[] parameters)
        {
            using var command = pContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            pContext.Database.OpenConnection();

            var result = await command.ExecuteScalarAsync();

            pContext.Database.CloseConnection();

            return result;
        }

        public static async Task<IEnumerable<T>> RawSqlQueryAsync<T>(this DbContext pContext, CancellationToken cancellation, string query, params object[] parameters)
        {
            await pContext.Database.OpenConnectionAsync(cancellation);
            return await pContext.Database.SqlQueryRaw<T>(query, parameters).ToListAsync(cancellation);
        }
    }
}
