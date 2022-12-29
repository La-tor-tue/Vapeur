using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.Metier
{
    public class Booking
    {


        private DateTime bookingDate;

        private VideoGame game;
        private Player booker;


        public DateTime BookingDate { get { return bookingDate; } set { bookingDate = value; } }

        public VideoGame Game { get { return game; } set { game = value; } }

        public Player Booker { get { return booker; } set { booker = value; } }

    }
}
