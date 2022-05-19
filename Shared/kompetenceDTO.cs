using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{

   public class kompetenceDTO {
       public int kompetence_id {get; set;}
       public string kompetence_navn {get; set;}
   }
}