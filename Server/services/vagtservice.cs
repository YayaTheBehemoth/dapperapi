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
            public List<opgaveDTO> getAllOpgaver(){
                var sql_bind_opgave = "select vagt_id, opgave_id from vagter";
                var sql_count ="select v.vagt_id, count(fvb.frivillig_id) from frivillig_vagt_bridge as fvb  right join vagter as v on fvb.vagt_id = v.vagt_id group by v.vagt_id";
                
           var sql = "SELECT o.er_last,o.opgave_id, o.opgave_navn, o.opgave_beskrivelse, o.status_id, s.status_navn, o.er_team_opgave FROM  opgaver AS o JOIN status AS s ON o.status_id = s.status_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<opgaveDTO>(sql);
               var binderList = connection.Query<opgaveBinder>(sql_bind_opgave); 
               var countList = connection.Query<vagtCount>(sql_count);   
               foreach (var binder in binderList) 
               {
                   foreach (var count in countList)
                   {
                       if (count.vagt_id == binder.vagt_id)
                       {
                           foreach (var vagt in vagtList)
                           {
                               if (vagt.opgave_id == binder.opgave_id)
                               {
                                   vagt.antal_personer_skrevet_på = vagt.antal_personer_skrevet_på + count.count;
                               }
                           }
                       }
                   }
               }
              var sortedList = vagtList.OrderBy(x => x.antal_personer_skrevet_på);
               return sortedList.ToList<opgaveDTO>();
           }
       }
       public async Task updateNavn(frivilligDTO user){
           var parameters = new {NAVN = user.frivillig_navn, ID = user.frivillig_id};
           var sql="update frivillig set frivillig_navn = @NAVN where frivillig_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql, parameters);
                  
               
           }
       }
        public async Task updatepw(frivilligDTO user){
           var parameters = new {PW = user.pw, ID = user.frivillig_id};
           var sql="update frivillig set pw_hash = crypt(@PW, gen_salt('bf')) where frivillig_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql, parameters);
                  
               
           }
       }
       public async Task updatetlf(frivilligDTO user){
           var parameters = new {TLF = user.frivillig_tlf, ID = user.frivillig_id};
           var sql="update frivillig set frivillig_tlf = @TLF where frivillig_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql, parameters);
                  
               
           }
       }
       public List<vagtDTO> getVagter(){
           var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id ";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   
               return vagtList.ToList<vagtDTO>();
           }
       }
       
       public async Task deleteVagt(int id){
           var parameters = new {ID = id};
           var sql ="DELETE FROM vagter WHERE vagt_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql, parameters);
                  
               
           }
       }

        public async Task deleteOpgave(int id){
           var parameters = new {ID = id};
           var sql ="DELETE FROM opgaver WHERE opgave_id = @ID";
           var sql1 ="DELETE FROM vagter WHERE opgave_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql1, parameters);
               await connection.ExecuteAsync(sql, parameters);
              
                  
               
           }
       }
        public async Task tilmelding(tilmelding tilmelding){
          var parameters = new {FID = tilmelding.frivillig_id, VID = tilmelding.vagt_id };
          var sql ="INSERT INTO frivillig_vagt_bridge (frivillig_id, vagt_id) VALUES (@FID, @VID)";
          using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
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
       public async Task opret(frivilligDTO user){
        
          var parameters = new {NAVN = user.frivillig_navn, TLF = user.frivillig_tlf, PW_hash = user.pw,RID = user.role_id};
          var sql ="INSERT INTO frivillig (frivillig_navn, frivillig_tlf, pw_hash, role_id ) VALUES (@NAVN, @TLF, crypt(@PW_hash, gen_salt('bf')),@RID)";
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
      public frivilligDTO getvagtbyid(string password, string username){
         var parameters =new {PASSWORD = password, USERNAME = username};
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
             var sql ="select crypt(@PASSWORD,pw_hash)=pw_hash as pwCheck,frivillig_navn,frivillig_id,role_id,frivillig_tlf from frivillig where frivillig_navn = @USERNAME";
              using (var connection = connecter.Connect()){
                  
            var check = connection.QueryFirst<frivilligDTO>(sql,parameters);
            if (check.pwCheck == false){
              
             throw new NpgsqlException();
            }
            else{
             
                return  connection.QueryFirst<frivilligDTO>(sql,parameters);
            }
          } 
      }
      public frivilligDTO getfrivilligbyid(int id){
         var parameters =new {ID = id};
             
             var sql ="select frivillig_navn,frivillig_tlf, role_id from frivillig where frivillig_id = @ID";
              using (var connection = connecter.Connect()){
            return connection.QueryFirst<frivilligDTO>(sql,parameters);
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
      public List<opgaveDTO> filterByStatus(int id)
      {
          var parameters = new {ID = id};
          var sql_bind_opgave = "select vagt_id, opgave_id from vagter";
                var sql_count ="select v.vagt_id, count(fvb.frivillig_id) from frivillig_vagt_bridge as fvb  right join vagter as v on fvb.vagt_id = v.vagt_id group by v.vagt_id";
   var sql = "SELECT o.er_last, o.opgave_id, o.opgave_navn, o.opgave_beskrivelse, o.status_id, s.status_navn, o.er_team_opgave FROM  opgaver AS o JOIN status AS s ON o.status_id = s.status_id WHERE o.status_id = @ID";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<opgaveDTO>(sql,parameters);
               var binderList = connection.Query<opgaveBinder>(sql_bind_opgave); 
               var countList = connection.Query<vagtCount>(sql_count);  
               foreach (var binder in binderList)
               {
                   foreach (var count in countList)
                   {
                       if (count.vagt_id == binder.vagt_id)
                       {
                           foreach (var vagt in vagtList)
                           {
                               if (vagt.opgave_id == binder.opgave_id)
                               {
                                   vagt.antal_personer_skrevet_på = vagt.antal_personer_skrevet_på + count.count;
                               }
                           }
                       }
                   }
               }
              var sortedList = vagtList.OrderBy(x => x.antal_personer_skrevet_på);
                   
               return vagtList.ToList<opgaveDTO>();
           }
      }

            public List<vagtDTO> filterByOpgave(int id)
      {
          var parameters = new {ID = id};
   var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id where v.opgave_id = @ID group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id ";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql,parameters);
                   
               return vagtList.ToList<vagtDTO>();
           }
      }

           public List<vagtDTO> filterByUlåstOpgave(int id, int uid)
      {
          //Console.WriteLine($"{uid}");
          var parameters = new {ID = id, UID = uid};
          List<vagtDTO> templist = new List<vagtDTO>();
        var sql = "select v.vagt_id,v.vagt_start, v.vagt_slut, v.opgave_id, o.opgave_navn,v.er_last from vagter as v left join frivillig_vagt_bridge as fvb on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id where v.opgave_id = @ID and fvb.frivillig_id not in (@UID) and v.er_last = false ";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql,parameters);
              
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
                   
               return vagtList.ToList<vagtDTO>();
           }
       }

         public List<opgaveDTO> getOpgaverSortStatus(){
             var sql_bind_opgave = "select vagt_id, opgave_id from vagter";
                var sql_count ="select v.vagt_id, count(fvb.frivillig_id) from frivillig_vagt_bridge as fvb  right join vagter as v on fvb.vagt_id = v.vagt_id group by v.vagt_id";
           var sql = "SELECT o.er_last, o.opgave_id, o.opgave_navn, o.opgave_beskrivelse, o.status_id, o.er_team_opgave, s.status_navn FROM  opgaver AS o JOIN status AS s ON o.status_id = s.status_id ORDER BY s.status_id";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<opgaveDTO>(sql);
               var binderList = connection.Query<opgaveBinder>(sql_bind_opgave); 
               var countList = connection.Query<vagtCount>(sql_count);  
               foreach (var binder in binderList)
               {
                   foreach (var count in countList)
                   {
                       if (count.vagt_id == binder.vagt_id)
                       {
                           foreach (var vagt in vagtList)
                           {
                               if (vagt.opgave_id == binder.opgave_id)
                               {
                                   vagt.antal_personer_skrevet_på = vagt.antal_personer_skrevet_på + count.count;
                               }
                           }
                       }
                   }
               }
              var sortedList = vagtList.OrderBy(x => x.antal_personer_skrevet_på);
                   
               return vagtList.ToList<opgaveDTO>();
           }
       }

         public List<vagtDTO> getVagterSortAntal(){
           var sql = "select v.vagt_start, v.vagt_slut, o.opgave_navn,o.opgave_id, count(fvb.frivillig_id) as antal_personer_skrevet_pa, v.vagt_id, v.er_last  from frivillig_vagt_bridge as fvb right join  vagter as v on fvb.vagt_id = v.vagt_id join opgaver as o on v.opgave_id = o.opgave_id group by fvb.vagt_id,v.vagt_id,o.opgave_navn,o.opgave_id order by antal_personer_skrevet_pa";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql);
                   
               return vagtList.ToList<vagtDTO>();
           }
       }


         public List<roletest> getroles(){
           var sql = "select * from roles";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<roletest>(sql);
                   
               return vagtList.ToList<roletest>();
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
            public async Task låsOpgave(opgaveDTO opgave){
           if(opgave.er_last == false){
             var parameters = new {ID = opgave.opgave_id,LÅS = true};
               var sql ="UPDATE vagter SET er_last = @LÅS WHERE opgave_id = @ID";
               var sql1 ="UPDATE opgaver SET er_last = @LÅS WHERE opgave_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
            await connection.ExecuteAsync(sql1,parameters);
          }
      }
           
           else {
            var parameters = new {ID = opgave.opgave_id,LÅS = false};
              var sql ="UPDATE vagter SET er_last = @LÅS WHERE opgave_id = @ID";
              var sql1 ="UPDATE opgaver SET er_last = @LÅS WHERE opgave_id = @ID";
              using (var connection = connecter.Connect()){
            await connection.ExecuteAsync(sql,parameters);
            await connection.ExecuteAsync(sql1,parameters);
          }
      }
           }

            public List<vagtDTO> getVagterbyfrivilligid(int id){
                var parameters = new{ID = id};
           var sql = "select v.vagt_start,v.vagt_slut,o.opgave_navn from vagter as v join frivillig_vagt_bridge as fvb on v.vagt_id = fvb.vagt_id join frivillig as f on fvb.frivillig_id = f.frivillig_id join opgaver as o on v.opgave_id = o.opgave_id where f.frivillig_id = @ID";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<vagtDTO>(sql,parameters);
                   
               return vagtList.ToList<vagtDTO>();
           }
       }
             //Console.WriteLine($"{vagt.navn},{vagt.id},{vagt.tal}");
         public List<opgaveDTO> getOpgaverUlåst(){
             var sql_bind_opgave = "select vagt_id, opgave_id from vagter";
                var sql_count ="select v.vagt_id, count(fvb.frivillig_id) from frivillig_vagt_bridge as fvb  right join vagter as v on fvb.vagt_id = v.vagt_id group by v.vagt_id";
           var sql = "SELECT o.er_last, o.opgave_id, o.opgave_navn, o.opgave_beskrivelse, o.status_id, o.er_team_opgave, s.status_navn FROM  opgaver AS o JOIN status AS s ON o.status_id = s.status_id WHERE er_last IS false ";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<opgaveDTO>(sql);
               var binderList = connection.Query<opgaveBinder>(sql_bind_opgave); 
               var countList = connection.Query<vagtCount>(sql_count);  
               foreach (var binder in binderList)
               {
                   foreach (var count in countList)
                   {
                       if (count.vagt_id == binder.vagt_id)
                       {
                           foreach (var vagt in vagtList)
                           {
                               if (vagt.opgave_id == binder.opgave_id)
                               {
                                   vagt.antal_personer_skrevet_på = vagt.antal_personer_skrevet_på + count.count;
                               }
                           }
                       }
                   }
               }
              var sortedList = vagtList.OrderBy(x => x.antal_personer_skrevet_på);
                   
               return vagtList.ToList<opgaveDTO>();
           }
       } 
       public List<kompetenceDTO> getmykompetencer(int id){
           var parameters = new{ID = id};
           var sql = "select k.kompetence_id, k.kompetence_navn from kompetence as k join frivillig_kompetence_bridge as fkb on k.kompetence_id = fkb.kompetence_id where fkb.frivillig_id = @ID";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<kompetenceDTO>(sql,parameters);
                   
               return vagtList.ToList<kompetenceDTO>();
           }
       }
       public List<kompetenceDTO> getkompetencer(){
           
           var sql = "select * from kompetence";
           using (var connection = connecter.Connect()){
               var vagtList = connection.Query<kompetenceDTO>(sql);
                   
               return vagtList.ToList<kompetenceDTO>();
           }
       }  
         public async Task addmykompetencer(int uid, int kid){
           var parameters = new {UID = uid, KID = kid};
           var sql = "insert into frivillig_kompetence_bridge (frivillig_id, kompetence_id) VALUES (@UID, @KID)";
           using (var connection = connecter.Connect()){
              await connection.ExecuteAsync(sql, parameters);
                   
           }
       }
        public async Task removemykompetencer(int uid, int kid){
      
           var parameters = new {UID = uid, KID = kid};
           var sql = "delete from frivillig_kompetence_bridge where frivillig_id = @UID and kompetence_id = @KID";
           using (var connection = connecter.Connect()){
              await connection.ExecuteAsync(sql, parameters);
                   
           }
       }
        public async Task removefromkompetencer(int id){
          
           var parameters = new {ID = id};
           var sql = "delete from kompetence where kompetence_id = @ID";
            var sql1 = "delete from frivillig_kompetence_bridge where kompetence_id = @ID";
           using (var connection = connecter.Connect()){
               await connection.ExecuteAsync(sql1, parameters);  
              await connection.ExecuteAsync(sql, parameters);
                
           }
       }
          public async Task addtokompetencer(kompetenceDTO kompetence){
          
           var parameters = new {NAVN = kompetence.kompetence_navn};
           
            var sql = "insert into kompetence (kompetence_navn)values(@NAVN)";
           using (var connection = connecter.Connect()){
              await connection.ExecuteAsync(sql, parameters);
             
           }
       }
       }   
      }
    

