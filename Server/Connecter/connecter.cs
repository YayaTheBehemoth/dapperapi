using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
namespace festivalbooking.Server.Connecter{
    public class DapperConnecter{
        private readonly IConfiguration _configuration;
        private readonly string connString;
        public DapperConnecter(IConfiguration configuration){
            _configuration = configuration;
            connString = _configuration.GetConnectionString("NpgSql_Connection");
        }
        public IDbConnection Connect() {
          return new  NpgsqlConnection(connString);
      
        }

    }
}