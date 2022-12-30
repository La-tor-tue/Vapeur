using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.DAO;
using Vapeur.Business.Metier;

namespace Vapeur.Business.Controller
{
    public class MainController
    {
        #region DATA
        // Objetcs
        #region DAO
        //Data acces object

        private DAO<Player> playerDAO;
        private DAO<VideoGame> gameDAO;
        private DAO<Copy> copyDAO;
        private DAO<Booking> bookingDAO;
        private DAO<Loan> loanDAO;

        #endregion

        #region Player Logged Infos

        //Infos relative au joueur connecté
        private Player playerLogged;

        private ObservableCollection<Loan> myLoans;
        private ObservableCollection<Booking> myBookings;
        private ObservableCollection<Copy> myCopies;

        #endregion

        #region All infos
        //Bank de donnée

        private ObservableCollection<Loan> allLoans;
        private ObservableCollection<Booking> allBookings;
        private ObservableCollection<Copy> allCopies;
        private ObservableCollection<VideoGame> allGames;
        private ObservableCollection<Player> allPlayers;

        #endregion

        #endregion

        //CTOR

        #region Constructor
        public MainController(DAOFactory dAOFactory, Player playerLogged)
        {

            this.playerDAO =dAOFactory.GetPlayerDAO();
            this.gameDAO=dAOFactory.GetGameDAO();
            this.copyDAO =dAOFactory.GetCopyDAO();
            this.bookingDAO =dAOFactory.GetBookingDAO();
            this.loanDAO =dAOFactory.GetLoanDAO();


            
            this.playerLogged = playerLogged;

            InitAll();
        }
        #endregion

        #region Props

        public Player PlayerLogged { get { return playerLogged; } set { playerLogged = value;} }
        public ObservableCollection<Loan> MyLoans { get { return myLoans; } set { myLoans = value; } }
        public ObservableCollection<Booking> MyBookings { get { return myBookings; } set { myBookings = value; } }
        public ObservableCollection<Copy> MyCopies { get { return myCopies; } set { myCopies = value; } }



        public ObservableCollection<Player> AllPlayers { get { return allPlayers; } set { allPlayers = value; } }
        public ObservableCollection<Loan> AllLoans { get { return allLoans; } set { allLoans = value; } }
        public ObservableCollection<Booking> AllBookings { get { return allBookings; } set { allBookings = value; } }
        public ObservableCollection<Copy> AllCopies { get { return allCopies; } set { allCopies = value; } }
        public ObservableCollection<VideoGame> AllGames { get { return allGames; } set { allGames = value; } }

        #endregion

        #region INIT Methodes

        public void InitAll()
        {
            InitAllLoans();
            InitMyLoans();

            InitAllBookings();
            InitMyBookings();

            InitAllCopies();
            InitMyCopies();

            InitAllGames();
            InitAllPlayers();
        }

        #region Init Loans

        public void InitAllLoans()
        {
            List<Loan> loans = new List<Loan>();
            loans = loanDAO.GetAll();

            AllLoans = new ObservableCollection<Loan>(loans);

            for (int i = 0; i < AllLoans.Count; i++)
            {
                AllLoans[i].Copy = copyDAO.Read(AllLoans[i].Copy.ID);
                AllLoans[i].Lender = AllLoans[i].Copy.Owner;

                AllLoans[i].Borrower = playerDAO.Read(AllLoans[i].Borrower.ID);
            }
        }

        public void InitMyLoans()
        {
            MyLoans = new ObservableCollection<Loan>();
            foreach (Loan loan in AllLoans)
            {
                if (loan.Borrower.ID==PlayerLogged.ID)
                {
                    MyLoans.Add(loan);
                }
            }
        }

        #endregion

        #region Init Bookings
        public void InitAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            bookings = bookingDAO.GetAll();

            AllBookings = new ObservableCollection<Booking>(bookings);

            for (int i = 0; i < AllBookings.Count; i++)
            {
                AllBookings[i].Booker = playerDAO.Read(AllBookings[i].Booker.ID);
                AllBookings[i].Game = gameDAO.Read(AllBookings[i].Game.ID);
            }
        }

        public void InitMyBookings()
        {
            MyBookings= new ObservableCollection<Booking>();
            foreach (Booking booking in AllBookings)
            {
                if (booking.Booker.ID == PlayerLogged.ID)
                {
                    MyBookings.Add(booking);
                }
            }
        }
        #endregion

        #region Init Copies
        public void InitAllCopies()
        {
            List<Copy> copies= new List<Copy>();
            copies = copyDAO.GetAll();

            AllCopies = new ObservableCollection<Copy>(copies);
            for (int i = 0; i < AllCopies.Count; i++)
            {
                AllCopies[i].Game = gameDAO.Read(AllCopies[i].Game.ID);
                AllCopies[i].Owner = playerDAO.Read(AllCopies[i].Owner.ID);

                foreach (Loan loan in AllLoans)
                {
                    if (AllCopies[i].ID==loan.Copy.ID)
                    {
                        AllCopies[i].Loan = loan;
                    }
                }
            }
        }

        public void InitMyCopies()
        {
            MyCopies = new ObservableCollection<Copy>();

            foreach (Copy copy in AllCopies)
            {
                if (copy.Owner.ID == PlayerLogged.ID)
                {
                    MyCopies.Add(copy);
                }
            }
        }
        #endregion

        #region Init Games
        public void InitAllGames()
        {
            List<VideoGame> games = new List<VideoGame>();
            games= gameDAO.GetAll();

            AllGames= new ObservableCollection<VideoGame>(games);

            for (int i = 0; i < AllGames.Count; i++)
            {
                foreach (Copy copy in AllCopies)
                {
                    if (copy.Game.ID == AllGames[i].ID)
                    {
                        AllGames[i].Copies.Add(copy);
                    }
                }
            }
        }

        #endregion

        #region Init Players
        public void InitAllPlayers()
        {
            List<Player> players = new List<Player>();
            players = playerDAO.GetAll();

            AllPlayers= new ObservableCollection<Player>(players);
        }
        #endregion

        #endregion

    }
}
