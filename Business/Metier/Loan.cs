﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.Metier
{
    public class Loan
    {
        private DateTime startDate, endDate;
        private bool ongoing,isNew;

        private Copy copy;
        private Player borrower, lender;

        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        public bool Ongoing { get { return ongoing; } set { ongoing = value; } }
        public bool IsNew { get { return isNew; } set { isNew = value; } }
        public Copy Copy { get { return copy; } set { copy = value; } }
        public Player Borrower { get { return borrower; } set { borrower = value; } }
        public Player Lender { get { return lender; } set { lender = value; } }
        

        public override string ToString()
        {
            StringBuilder stringBuilder= new StringBuilder();
            stringBuilder.Append("Location de: " + Copy.Game.Title + " sur " + Copy.Game.Console + " du " + StartDate.Date + " au " + EndDate.Date);
            if (Ongoing)
            {
                stringBuilder.Append(" en cours.");
            }
            else
            {
                stringBuilder.Append(" non commencé.");
            }
            return stringBuilder.ToString();
        }
    }
}
