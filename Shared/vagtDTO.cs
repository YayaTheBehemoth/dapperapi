using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagt table
   public class vagtDTO {
       public int Id {get; set;}
       public DateTime start {get; set;}
       public DateTime slut{get; set;}
    
       public int område_id{get;set;}
   }
}