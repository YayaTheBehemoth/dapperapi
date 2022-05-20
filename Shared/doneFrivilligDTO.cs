using System; 
using Dapper; 
using Npgsql; 
using System.Collections.Generic;
namespace festivalbooking.Shared{

    public class doneFrivilligDTO{
       
        public int frivillig_id {get; set;}
        public string frivillig_navn {get; set;}
        public int frivillig_tlf {get; set;}
        public string frivillig_email {get; set;}
        public List<string> kompetencer {get; set;}
    }
}