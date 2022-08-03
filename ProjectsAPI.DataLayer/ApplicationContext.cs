using ProductsAPI.DataAccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProductsAPI.DataAccess
{
    public class ApplicationContext : IDbContext
    {
        private string _connectionString;
        public ApplicationContext(String connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(string nomenclature, CancellationToken token)
        {
            string strpostgracecommand = @"INSERT INTO public.""Products""( nomenclature)" + $"VALUES ({nomenclature});";
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = strpostgracecommand;
            await command.ExecuteNonQueryAsync(token);
            connection.Close();
            return "OK";
        }

        public async Task<string> AddMany(IEnumerable<string> nomenclatures, CancellationToken token)
        {
            foreach (var nomenclature in nomenclatures)
            {
                string strpostgracecommand = @"INSERT INTO public.""Products""( nomenclature)" + $"VALUES ({nomenclature});";
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString) ;
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = strpostgracecommand;
                await command.ExecuteNonQueryAsync(token);
                connection.Close();
            }
            return "OK";
        }
    }
}
