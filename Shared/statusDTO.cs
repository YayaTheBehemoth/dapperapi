using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{

   public class statusDTO {
       public int status_id {get; set;}
       public string status_navn {get; set;}
   }
}