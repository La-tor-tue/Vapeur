using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.Metier
{
    public class Copy
    {
        private int id;

        private VideoGame game;
        private Player owner;

        private Loan loan;

        public int ID { get { return id; } set { id = value; } }
        public VideoGame Game { get { return game; } set { game = value; } }
        public Player Owner { get { return owner; } set { owner = value; } }
        public Loan Loan { get { return loan; } set { loan=value; } }

        public override string ToString()
        {
            return Game.Title+"  sur  "+Game.Console;
        }

        public bool IsAvaible()
        {
            if (Loan.Ongoing)
            {
                return false;
            }

            return true;
        }

        public void Borrow(Player borrower,Loan newLoan) { 
            newLoan.Borrower = borrower;
            Loan=newLoan;
        }

        public void ReleaseCopy()
        {
            Loan = new Loan
            {
                Ongoing= false,
            };
        }
    }
}
