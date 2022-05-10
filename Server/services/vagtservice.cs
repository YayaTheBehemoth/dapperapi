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
       public List<testDTO> getVagter(){
           var sql = "SELECT * FROM testtable";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<testDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<testDTO>();
           }
       }
       
       public async Task deleteVagt(int id){
           var parameters = new {ID = id};
           var sql ="DELETE FROM testtable WHERE id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql, parameters);
               //Console.WriteLine("service nået");   
               
           }
       }

      public async Task postVagt(testDTO vagt){
          var parameters = new {NAVN = vagt.navn, TAL = vagt.tal};
          var sql ="INSERT INTO testtable (navn, tal) VALUES (@NAVN, @TAL)";
          using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }

      public async Task putVagt(testDTO vagt){
             var parameters = new {NAVN = vagt.navn, TAL = vagt.tal, ID = vagt.id};
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
             var sql ="UPDATE testtable SET navn = @NAVN, tal = @TAL WHERE id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }
    }

}