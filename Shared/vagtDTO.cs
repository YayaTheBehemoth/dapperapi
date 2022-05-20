using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagt table
   public class vagtDTO {
       public int vagt_id {get; set;}
       public DateTime vagt_start {get; set;}
       public DateTime vagt_slut {get; set;}

       public int område_id {get; set;}
    
       public string område_navn {get;set;}

       public int status_id {get; set;}

       public string status_navn {get; set;}
   }
}