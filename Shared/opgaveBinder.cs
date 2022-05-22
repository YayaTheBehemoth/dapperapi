using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class opgaveBinder
    {
       
        
        public int opgave_id {get; set;}
        public int vagt_id {get; set;}
    }
}