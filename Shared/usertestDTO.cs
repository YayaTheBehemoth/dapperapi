using System; 
using Dapper; 
using Npgsql; 
namespace festivalbooking.Shared{
    public class usertest{
        public int id {get; set; }
        public string username {get; set;}
        public string pwhash {get; set;}
    }
}