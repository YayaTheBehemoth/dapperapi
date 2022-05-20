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
           var sql = "SELECT v.vagt_id, v.vagt_start, v.vagt_slut, vo.område_navn, vo.område_id,status.status_id,status.status_navn from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status ON v.status_id = status.status_id";
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
          var parameters = new {START = vagt.vagt_start, SLUT = vagt.vagt_slut, OID = vagt.område_id};
          var sql ="INSERT INTO vagter (vagt_start, vagt_slut, område_id) VALUES (@START, @SLUT, @OID)";
          using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }

      public async Task putVagt(vagtDTO vagt){
             var parameters = new {START = vagt.vagt_start, SLUT = vagt.vagt_slut, OID = vagt.område_id, ID = vagt.vagt_id};
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
             var sql ="UPDATE vagter SET vagt_start = @START, vagt_slut = @SLUT, område_id = @OID WHERE vagt_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }
      public List<vagtDTO> filterByStatus(int id)
      {
          var parameters = new {ID = id};
   var sql = "SELECT v.vagt_id, v.vagt_start, v.vagt_slut, vo.område_navn, vo.område_id,s.status_id,s.status_navn from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status AS s ON v.status_id = s.status_id WHERE v.status_id = @ID";
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
        public List<doneFrivilligDTO> getFrivillige()
      {
       
            var sql = "SELECT * from frivillig";
            var sql_bridge = "SELECT * FROM frivillig_kompetence_bridge";
            var sql1 = "SELECT * from kompetence";
            using (var connection = connecter.Connect()){
                 var tempFrivilligList = connection.Query<frivilligDTO>(sql);
                 var tempKompetenceList = connection.Query<kompetenceDTO>(sql1);
                 var tempBinder = connection.Query<kompetenceBinder>(sql_bridge);
                List<doneFrivilligDTO> tempList = new List<doneFrivilligDTO>();
                foreach(var item in tempFrivilligList)
                {
                    doneFrivilligDTO tempFrivillig = new doneFrivilligDTO();
                    tempFrivillig.frivillig_id = item.frivillig_id;
                    tempFrivillig.frivillig_navn = item.frivillig_navn;
                    tempFrivillig.frivillig_email = item.frivillig_email;
                    tempFrivillig.frivillig_tlf = item.frivillig_tlf;
                    tempFrivillig.kompetencer =  new List<string>();
                    tempList.Add(tempFrivillig);
                    
                }
                
                  foreach (var binder in tempBinder.ToList<kompetenceBinder>())
                  {
                      foreach (var frivillig in tempList)
                      {
                         
                            if (binder.frivillig_id == frivillig.frivillig_id){
                                foreach (var kompetence in tempKompetenceList.ToList<kompetenceDTO>())
                                {
                                    if (binder.kompetence_id == kompetence.kompetence_id)
                                    {
                                        
                                        frivillig.kompetencer.Add($"{kompetence.kompetence_navn}");
                                    }
                                }
                            }
                      }
                  } 
                
                return tempList;
            }
      }

        public List<vagtDTO> getVagterSortOmråde(){
           var sql = "SELECT v.vagt_id, v.vagt_start, v.vagt_slut, vo.område_navn, vo.område_id,status.status_id,status.status_navn from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status ON v.status_id = status.status_id ORDER BY vo.område_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
       }
    }

}