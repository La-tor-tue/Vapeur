using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.Metier
{
    public class Player : User
    {
        private int credit;
        private string pseudo;
        private DateTime registrationDate, dateOfBirth;

        public int Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }

        public DateTime Registration
        {
            get { return registrationDate; }
            set
            {
                registrationDate = value;
            }
        }

        public DateTime BirthDate
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public Player() : base()
        {
        }

        public override User Login(User user)
        {
            throw new NotImplementedException();
        }

        public bool LoanAllowed()
        {
            if (credit>0)
            {
                return true;
            }
            return false;
        }

        public void AddBirthdayBonus()
        {

        }


    }
}
