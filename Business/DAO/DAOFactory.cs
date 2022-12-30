using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.Metier;

namespace Vapeur.Business.DAO
{

    public enum DAOFactoryType
        {
            MS_SQL_FACTORY,
            XML_FACTORY
        }
        public abstract class DAOFactory
        {
            public abstract DAO<VideoGame> GetGameDAO();
            public abstract DAO<Player> GetPlayerDAO();
            public abstract DAO<Booking> GetBookingDAO();
            public abstract DAO<Copy> GetCopyDAO();
            public abstract DAO<Loan> GetLoanDAO();
            public static DAOFactory GetFactory(DAOFactoryType type)
            {
                switch (type)
                {
                    case DAOFactoryType.MS_SQL_FACTORY:
                        return new MSSQLFactory();
                    case DAOFactoryType.XML_FACTORY:
                        return null;
                    default:
                        return null;
                }
            }
        }

  
}
