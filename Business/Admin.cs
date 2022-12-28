using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business
{
    public class Admin : User
    {
        public Admin(int id, string username, string password) : base(id, username, password)
        {

        }

        public override User Login(User user)
        {
            //Code de connexion allant dans la BD et renvoyant le resultat
            throw new NotImplementedException();
        }
    }
}
