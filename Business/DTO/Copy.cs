using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.DTO
{
    public class Copy
    {
        private int id;

        private VideoGame game;
        private Player owner;

        public int ID { get { return id; } set { id = value; } }
        public VideoGame Game { get { return game; } set { game = value; } }
        public Player Owner { get { return owner; } set { owner = value; } }
    }
}
