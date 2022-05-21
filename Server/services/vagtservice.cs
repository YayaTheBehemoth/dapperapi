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
            public List<opgaveDTO> getVagtOpgaver(){
           var sql = "SELECT o.opgave_id, o.opgave_navn, o.opgave_beskrivelse, o.status_id, s.status_navn, o.er_team_opgave FROM  opgaver AS o JOIN status AS s ON o.status_id = s.status_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<opgaveDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<opgaveDTO>();
           }
       }
       public List<vagtDTO> getVagter(){
           var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id ";
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

        public async Task deleteOpgave(int id){
           var parameters = new {ID = id};
           var sql ="DELETE FROM opgaver WHERE opgave_id = @ID";
           var sql1 ="DELETE FROM vagter WHERE opgave_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql1, parameters);
               await connection.ExecuteAsync(sql, parameters);
              
               //Console.WriteLine("service nået");   
               
           }
       }

      public async Task postVagt(vagtDTO vagt){
          var parameters = new {START = vagt.vagt_start, SLUT = vagt.vagt_slut, OID = vagt.opgave_id, LÅST = false};
          var sql ="INSERT INTO vagter (vagt_start, vagt_slut, opgave_id, er_last) VALUES (@START, @SLUT, @OID, @LÅST)";
          using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }

      public async Task postOpgave(opgaveDTO opgave){
          var parameters = new {NAVN = opgave.opgave_navn, SID = opgave.status_id, TEAM = opgave.er_team_opgave, BESKRIVELSE = opgave.opgave_beskrivelse};
          var sql ="INSERT INTO opgaver (opgave_navn,opgave_beskrivelse,er_team_opgave,status_id) VALUES (@NAVN, @BESKRIVELSE, @TEAM,@SID)";
          using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }

      public async Task putVagt(vagtDTO vagt){
             var parameters = new {START = vagt.vagt_start, SLUT = vagt.vagt_slut, OID = vagt.opgave_id, ID = vagt.vagt_id, ON = vagt.opgave_navn};
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
             var sql ="UPDATE vagter SET vagt_start = @START, vagt_slut = @SLUT, opgave_id = @OID WHERE vagt_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }

        public async Task putOpgave(opgaveDTO opgave){
             var parameters =new {NAVN = opgave.opgave_navn, SID = opgave.status_id, TEAM = opgave.er_team_opgave, BESKRIVELSE = opgave.opgave_beskrivelse, ID = opgave.opgave_id};
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
             var sql ="UPDATE opgaver SET opgave_navn = @NAVN, status_id = @SID, er_team_opgave = @TEAM, opgave_beskrivelse = @BESKRIVELSE WHERE opgave_id = @ID  ";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }
      public List<vagtDTO> filterByStatus(int id)
      {
          var parameters = new {ID = id};
   var sql = "SELECT v.vagt_id, v.vagt_start, v.vagt_slut, vo.område_navn, vo.område_id,s.status_id,s.status_navn, v.er_last from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status AS s ON v.status_id = s.status_id WHERE v.status_id = @ID";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql,parameters);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
      }

            public List<vagtDTO> filterByOpgave(int id)
      {
          var parameters = new {ID = id};
   var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id where v.opgave_id = @ID group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id ";
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

        public List<vagtDTO> getVagterSortOpgave(){
           var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id order by v.opgave_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
       }

         public List<vagtDTO> getVagterSortStatus(){
           var sql = "SELECT v.vagt_id, v.vagt_start, v.vagt_slut, vo.område_navn, vo.område_id,status.status_id,status.status_navn, count(fvb.frivillig_id) AS antal_personer_skrevet_på, v.er_last from vagter AS v JOIN vagt_områder AS vo ON v.område_id = vo.område_id JOIN status ON v.status_id = status.status_id  LEFT JOIN frivillig_vagt_bridge AS fvb ON v.vagt_id = fvb.vagt_id GROUP BY v.vagt_id, v.vagt_start, v.vagt_slut,v.område_id,v.status_id, vo.område_navn,status.status_navn, vo.område_id,status.status_id  ORDER BY status.status_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
       }

         public List<vagtDTO> getVagterSortAntal(){
           var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id order by antal_personer_skrevet_pa";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   //Console.WriteLine("service nået");
               return vagtList.ToList<vagtDTO>();
           }
       }

       public async Task låsVagt(vagtDTO vagt){
           if(vagt.er_last == false){
             var parameters = new {ID = vagt.vagt_id,LÅS = true};
               var sql ="UPDATE vagter SET er_last = @LÅS WHERE vagt_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }
           
           else {
            var parameters = new {ID = vagt.vagt_id,LÅS = false};
              var sql ="UPDATE vagter SET er_last = @LÅS WHERE vagt_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
          }
      }
           }
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
      }
    }

