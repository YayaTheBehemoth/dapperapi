using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;
using Npgsql;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Linq;
using festivalbooking.Shared;
using festivalbooking.Server.Connecter;
//data acess service til vagter -- til implementation i controlleren 
//det her er man skriver sql queries til databasen 
namespace festivalbooking.Server.Services{  
    public class vagtService{
 

        private readonly DapperConnecter connecter;

       public vagtService (DapperConnecter _connecter){
            
          connecter = _connecter;
      
       }
            public List<vagt_områderDTO> getVagtOmråder(){
           var sql = "SELECT * FROM vagt_områder";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagt_områderDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagt_områderDTO>();
           }
       }
       public List<vagtDTO> getVagter(){
           var sql = "SELECT v.vagt_id, v.start, v.slut, vo.område_navn, vo.område_id,status.status_id,status.status_navn from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status ON v.status_id = status.status_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
       }
       
       public async Task deleteVagt(int id){
           var parameters = new {ID = id};
           var sql ="DELETE FROM vagter WHERE vagt_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql, parameters);
               //Console.WriteLine("service nået");   
               
           }
       }

      public async Task postVagt(vagtDTO vagt){
          var parameters = new {START = vagt.start, SLUT = vagt.slut, OID = vagt.område_id};
          var sql ="INSERT INTO vagter (start, slut, område_id) VALUES (@START, @SLUT, @OID)";
          using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }

      public async Task putVagt(vagtDTO vagt){
             var parameters = new {START = vagt.start, SLUT = vagt.slut, OID = vagt.område_id, ID = vagt.vagt_id};
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
             var sql ="UPDATE vagter SET start = @START, slut = @SLUT, område_id = @OID WHERE vagt_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }
      public List<vagtDTO> filterByStatus(int id)
      {
          var parameters = new {ID = id};
   var sql = "SELECT v.vagt_id, v.start, v.slut, vo.område_navn, vo.område_id,s.status_id,s.status_navn from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status AS s ON v.status_id = s.status_id WHERE v.status_id = @ID";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql,parameters);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
      }

      public List<vagt_statusDTO> getStatus()
      {
       
            var sql = "SELECT * from status";
            using (var connection = connecter.Connect()){
                var statusList = connection.Query<vagt_statusDTO>(sql);
                return statusList.ToList<vagt_statusDTO>();
            }
      }
    }

}