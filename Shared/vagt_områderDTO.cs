using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagtområder table
   public class vagt_områderDTO {
       public int område_id {get; set;}
       public string område_navn {get; set;}
   }
}