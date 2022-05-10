using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagtområder table
   public class vagt_områder_DTO {
       public int id {get; set;}
       public string område {get; set;}
   }
}