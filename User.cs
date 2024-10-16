using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_Platform
{
    public  class Users
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Registered { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }

        //public User(int userid, string name, string email, string password, DateTime registered, string role, string username)
        //{
        //    UserID = userid;
        //    Name = name;
        //    Email = email;
        //    Password = password;
        //    Registered = registered;
        //    Role = role;
        //    Username = username;
        //}
        //public User ()
        //{

        //}

        //public override string ToString()
        //{
        //    return $"{UserID}\t\t|{Name}\t\t|{Email}\t\t|{Password}\t\t|{Registered}\t\t|{Role}\t\t|{Username}";
        //}
    }
}
