using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class frivilligDTO{
       
        public int frivillig_id {get; set;}
        public string frivillig_navn {get; set;}
        public int frivillig_tlf {get; set;}

        public string pw {get; set;}

        public int? role_id {get; set;}
        
       
    }
}