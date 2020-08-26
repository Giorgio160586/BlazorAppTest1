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
        public async Task<List<AnagraficaModel>> Select<AnagraficaModel>()
        {
            using (var conn = new SqlConnection(configurationManager.GetConnectionString("conn")))
            {
                var ret = await conn.QueryAsync<AnagraficaModel>("SELECT * FROM Anagrafica");
                return ret.ToList();
            }
        }
        public async Task<AnagraficaModel> Insert<AnagraficaModel>(AnagraficaModel param)
        {
            using (var conn = new SqlConnection(configurationManager.GetConnectionString("conn")))
            {
                var ret = await conn.QueryAsync<AnagraficaModel>(@"INSERT INTO Anagrafica (Nome) VALUES (@Nome);
                                                  SELECT * FROM Anagrafica WHERE ID = CAST(SCOPE_IDENTITY() as int)", param);
                return ret.FirstOrDefault();
            }
        }
    }
}
