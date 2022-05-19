using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagtområder table
   public class vagt_statusDTO {
       public int status_id {get; set;}
       public string status_navn {get; set;}
   }
}