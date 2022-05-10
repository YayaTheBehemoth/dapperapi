using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class testDTO{
       
        public int id {get; set;}
        public string navn {get; set;}
        public double tal {get; set;}
    }
}