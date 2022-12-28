using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.DTO;
using static Vapeur.Business.DAO.DAOFactory;

namespace Vapeur.Business.DAO
{
    public class MSSQLFactory : DAOFactory
    {
        public override DAO<Booking> GetBookingDAO()
        {
            return new BookingDAO();
        }

        public override DAO<Copy> GetCopyDAO()
        {
            return new CopyDAO();
        }

        public override DAO<VideoGame> GetGameDAO()
        {
            return new GameDAO();
        }

        public override DAO<Loan> GetLoanDAO()
        {
            return new LoanDAO();
        }

        public override DAO<Player> GetPlayerDAO()
        {
            return new PlayerDAO();
        }
    }

}
