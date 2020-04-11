using System.Collections.Generic;

namespace AppApi.Models
{
    public class Users
    {
         public int id {get; set;}
        public string UserName {get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PaswordSalt {get; set;}
        public List<Carts> Carts { get; set; }

    }
}