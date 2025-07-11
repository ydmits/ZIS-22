using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace KP_Mitsura
{
    internal static class DB
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        public static async Task<DataTable> QuerySIDAsync(string query, Dictionary<string, (MySqlDbType, object)> parameters = null)
        {
            DataTable table = new DataTable();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(param.Key, param.Value.Item1).Value = param.Value.Item2;
                        }
                    }
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        try
                        {
                            await Task.Run(() => adapter.Fill(table));
                        }
                        catch (Exception ex)
                        {
                            Win_Meaasge_Box.MsgB(ex.Message);
                        }
                    }
                }
            }
            return table;
        }
        public static async Task AddLogAsync(string[] messages)
        {
            string query = "INSERT INTO `logs` (`request`) VALUES (@request)";
            string request = string.Join(" ", messages);
            var parameters = new Dictionary<string, (MySqlDbType, object)>
            {
                { "@request", (MySqlDbType.VarChar, request) }
            };
            await QuerySIDAsync(query, parameters);
        }
        public static async Task HandleDatabaseOperationAsync(string query, Dictionary<string, (MySqlDbType, object)> parameters = null)
        {
            try
            {
                await QuerySIDAsync(query, parameters);
            }
            catch (Exception ex)
            {
                Win_Meaasge_Box.MsgB(ex.Message);
            }
        }
    }
}
