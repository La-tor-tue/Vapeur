using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Vapeur.Business.Metier
{
    public class VideoGame
    {
        private int id, creditCost;
        private string title, console;

        private List<Copy> copies;

        public int ID { get { return id; } set { id = value; } }

        public int CreditCost { get { return creditCost; } set { creditCost = value; } }

        public string Console { get { return console; } set { console = value; } }
        public string Title { get { return title; } set { title = value; } }

        public List<Copy> Copies { get { return copies; } set { copies = value; } }

        public Copy CopyAvaible()
        {

            if (Copies.Count!=0)
            {
                for (int i = 0; i < Copies.Count; i++)
                {
                    if (Copies[i].IsAvaible())
                    {
                        return Copies[i];
                    }
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Title+"  sur  "+Console+" pour le prix: "+CreditCost+" crédit(s)";
        }

        #region selectBooking
        public Booking SelectBooking(List<Booking> bookings) {

            Booking selectedBooking= null;

            selectedBooking = moreCredit(bookings);
            if (selectedBooking != null)
            {
                return selectedBooking;
            }
            else
            {
                selectedBooking = oldestBooking(bookings);
                if (selectedBooking != null)
                {
                    return selectedBooking;
                }
                else
                {
                    selectedBooking = oldestRegistration(bookings);
                    if (selectedBooking != null)
                    {
                        return selectedBooking;
                    }
                    else
                    {
                        selectedBooking = oldestMember(bookings);
                        if (selectedBooking != null)
                        {
                            return selectedBooking;
                        }
                        else
                        {
                            selectedBooking = randomBooking(bookings);
                        }
                    }
                }
            }

            return selectedBooking;
            
        }

        public  Booking moreCredit(List<Booking> bookings)
        {
            List<Booking> possibleBooking = new List<Booking>();
            Booking selectedBooking= null;
            int biggestCreditCount=0;
            

            foreach (Booking booking in bookings)
            {
                if (booking.Booker.Credit>biggestCreditCount)
                {
                    biggestCreditCount = booking.Booker.Credit;
                }
            }

            foreach (Booking booking in bookings) {
                if (booking.Booker.Credit==biggestCreditCount)
                {
                    possibleBooking.Add(booking);
                }
            }

            if(possibleBooking.Count==1) {
                selectedBooking = possibleBooking[0];
            }

            return selectedBooking;
        }

        public Booking oldestBooking(List<Booking> bookings)
        {
            List<Booking> possibleBooking = new List<Booking>();
            Booking selectedBooking = null;
            DateTime oldest = new DateTime();

            foreach (Booking booking in bookings)
            {
                if (booking.BookingDate.Date>oldest.Date)
                {
                    oldest = booking.BookingDate;
                }
            }

            foreach (Booking booking in bookings)
            {
                if (booking.BookingDate.Date ==oldest.Date)
                {
                    possibleBooking.Add(booking);
                }
            }

            if (possibleBooking.Count == 1)
            {
                selectedBooking = possibleBooking[0];
            }

            return selectedBooking;
        }

        public Booking oldestRegistration(List<Booking> bookings)
        {
            List<Booking> possibleBooking = new List<Booking>();
            Booking selectedBooking = null;
            DateTime oldest = new DateTime();

            foreach (Booking booking in bookings)
            {
                if (booking.Booker.Registration.Date > oldest.Date)
                {
                    oldest = booking.Booker.Registration;
                }
            }

            foreach (Booking booking in bookings)
            {
                if (booking.Booker.Registration.Date == oldest.Date)
                {
                    possibleBooking.Add(booking);
                }
            }

            if (possibleBooking.Count == 1)
            {
                selectedBooking = possibleBooking[0];
            }

            return selectedBooking;
        }

        public Booking oldestMember(List<Booking> bookings)
        {
            List<Booking> possibleBooking = new List<Booking>();
            Booking selectedBooking = null;
            DateTime oldest = new DateTime();

            foreach (Booking booking in bookings)
            {
                if (booking.Booker.BirthDate.Date > oldest.Date)
                {
                    oldest = booking.Booker.BirthDate;
                }
            }

            foreach (Booking booking in bookings)
            {
                if (booking.Booker.BirthDate.Date == oldest.Date)
                {
                    possibleBooking.Add(booking);
                }
            }

            if (possibleBooking.Count == 1)
            {
                selectedBooking = possibleBooking[0];
            }

            return selectedBooking;

        }

        public Booking randomBooking (List<Booking> bookings) {

            Random random = new Random();
            
            int number = random.Next(0, bookings.Count);
            return bookings[number];
        }

        #endregion

    }
}
