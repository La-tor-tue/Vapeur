using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business
{
    public class Booking
    {
        private int id;
        private DateTime bookingDate;

        private VideoGame game;
        private Player booker;

        public int ID { get { return id; } set { id = value; } }

        public DateTime BookingDate { get { return bookingDate; } set { bookingDate= value; } }

        public VideoGame Game { get { return game;} set { game = value; } }

        public Player Booker { get { return booker; } set { booker= value; } }
    }
}
