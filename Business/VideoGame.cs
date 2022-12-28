using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business
{
    public class VideoGame
    {
        private int id,creditCost;
        private string console;

        public int ID { get { return id; } set { id = value; } }

        public int CreditCost { get { return creditCost; } set { creditCost= value; } }

        public string Console { get { return console; } set { console = value; } }

    }
}
