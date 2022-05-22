using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class vagtCount
    {
       
      
        public int vagt_id {get; set;}
        public int count {get; set;}
    }
}