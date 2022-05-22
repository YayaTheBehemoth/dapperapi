using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    //model klasse til vagtområder table
   public class opgaveDTO {
       public int opgave_id {get; set;}
       public string opgave_navn {get; set;}

       public string opgave_beskrivelse {get; set;}

       public int status_id {get; set;}

       public string status_navn{get ; set;}

       public bool er_team_opgave {get; set;}

       public int antal_personer_skrevet_på {get; set;}

       public bool er_last {get; set;}
   }
}