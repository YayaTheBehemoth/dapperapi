using System;
namespace festivalbooking.Shared{
    public class User
    {
        public int? id {get; set;}

        public string navn {get; set;}

        public int? role_id  {get; set;}
        public string password{get; set;}
    }
}