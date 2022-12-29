using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.Metier
{
    public class Admin : User
    {
        public Admin() : base()
        {

        }

        public override User Login(User user)
        {
            //Code de connexion allant dans la BD et renvoyant le resultat
            throw new NotImplementedException();
        }
    }
}
