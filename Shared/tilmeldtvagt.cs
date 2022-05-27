using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagt table
   public class tilmeldtvagt{
       public int? vagt_id {get; set;}
       public DateTime vagt_start {get; set;}
       public DateTime vagt_slut {get; set;}

       public int opgave_id {get; set;}
    
       public string opgave_navn {get;set;}

       public int antal_personer_skrevet_pa {get; set;}

       public bool er_last {get; set;}

       public int frivillig_id {get; set;}
   }
}