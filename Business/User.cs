using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business
{
    public abstract class User
    {
        private int id;
        private string username,password;

        public int ID { 
            get {
            return id;
            }
            set
            {
                id = value;
            }
        }

        public string Username {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public User(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }

        abstract public User Login(User user);
    }
}
