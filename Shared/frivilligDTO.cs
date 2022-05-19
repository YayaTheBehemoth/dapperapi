using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class frivilligDTO{
       
        public int frivillig_id {get; set;}
        public string frivillig_navn {get; set;}
        public int frivillig_tlf {get; set;}
        public string frivillig_email {get; set;}
        public string[] kompetencer{get; set;}
    }
}