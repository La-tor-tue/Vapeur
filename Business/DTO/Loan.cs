using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.DTO
{
    public class Loan
    {
        private DateTime startDate, endDate;
        private bool ongoing;

        private Copy copy;
        private Player borrower, lender;

        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }

        public bool Ongoing { get { return ongoing; } set { ongoing = value; } }

        public Copy Copy { get { return copy; } set { copy = value; } }

        public Player Borrower { get { return borrower; } set { borrower = value; } }

        public Player Lender { get { return lender; } set { lender = value; } }
    }
}
