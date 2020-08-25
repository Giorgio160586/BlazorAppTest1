using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DatabaseLibrary
{
    public class Anagrafica
    {
        public readonly IConfiguration configurationManager;
        public Anagrafica(IConfiguration config)
        {
            configurationManager = config;
        }
        public async Task<List<AnagraficaModel>> Select<AnagraficaModel>(object param = null)
        {
            using (var conn = new SqlConnection(configurationManager.GetConnectionString("conn")))
            {
                var ret = await conn.QueryAsync<AnagraficaModel>("SELECT * FROM Anagrafica", param);
                return ret.ToList();
            }
        }
        public async Task Insert<AnagraficaModel>(object param = null)
        {
            using (var conn = new SqlConnection(configurationManager.GetConnectionString("conn")))
            {
              await conn.ExecuteAsync("INSERT INTO Anagrafica (Nome) VALUES (@Nome)", param);
            }
        }
    }
}
