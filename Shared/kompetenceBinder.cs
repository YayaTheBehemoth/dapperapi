using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class kompetenceBinder
    {
       
        public int fk_bridge_id {get; set;}
        public int kompetence_id {get; set;}
        public int frivillig_id {get; set;}
    }
}