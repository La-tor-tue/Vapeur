using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Globalization;

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
            SelectedPlayer = this.playerLogged;

            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;


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


        public Copy SelectedCopy { get; set; }
        public VideoGame SelectedGame { get; set; }
        public Player SelectedPlayer { get; set; }
        public Loan SelectedLoan { get; set; }
        public Booking SelectedBooking { get; set; }
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
        public int PriceCost { get; set; }


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

                AllLoans[i].Copy.Game = gameDAO.Read(AllLoans[i].Copy.Game.ID);
                AllLoans[i].Lender = playerDAO.Read(AllLoans[i].Copy.Owner.ID);


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

                AllGames[i].Copies = new List<Copy>();


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


        #region Function Methode

        #region Put in Loan Function
        public void CreateCopy()
        {
            Copy newCopy = new Copy
            {
                Owner = PlayerLogged,
                Game = SelectedGame
            };

            copyDAO.Create(newCopy);
            MyCopies.Add(newCopy);
            AllCopies.Add(newCopy);

            InitAll();
            foreach (VideoGame game in AllGames)
            {
                if (SelectedGame.ID==game.ID)
                {
                    SelectedGame = game;
                    break;
                }
            }
            newCopy = SelectedGame.CopyAvaible();

            List<Booking> bookings = new List<Booking>();

            foreach (Booking booking in AllBookings)
            {
                if (booking.Game.ID == newCopy.Game.ID)
                {
                    bookings.Add(booking);
                }
            }


            if (bookings.Count > 0)
            {
                SelectedBooking = newCopy.Game.SelectBooking(bookings);
                Loan newLoan = new Loan
                {
                    Borrower = SelectedBooking.Booker,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    Ongoing = false,
                    IsNew = true,
                    Copy = newCopy,
                };
                bookingDAO.Delete(SelectedBooking);
                loanDAO.Create(newLoan);
            }

            SelectedGame = null;
        }

        #endregion

        #region Edit Player Profile Function
        public void EditProfile()
        {
            if (SelectedPlayer.Pseudo!=PlayerLogged.Pseudo)
            {
                playerDAO.Update(SelectedPlayer);
                PlayerLogged = SelectedPlayer;
            }
            else
            {
                SelectedPlayer = PlayerLogged;
            }
        }

        #region Cancel a Booking Function

        public void CancelBooking()
        {
            if (bookingDAO.Delete(SelectedBooking))
            {
                AllBookings.Remove(SelectedBooking);
                MyBookings.Remove(SelectedBooking);
            }

            SelectedBooking = null;

        }

        #endregion

        #region Create a Loan Function

        public bool CanBorrow()
        {
            if (PlayerLogged.LoanAllowed())
            {
                return true;
            }
            return false;
        }

        public bool IsPossible()
        {
            Copy copy = SelectedGame.CopyAvaible();

            if (copy == null ||  SelectedGame.Copies.Count == 0)
            {

                return false;
            }

            return true;
        }

        public bool CreateLoan()
        {
            SelectedCopy = SelectedGame.CopyAvaible();

            Loan loan = new Loan
            {
                Copy = SelectedCopy,
                Borrower = PlayerLogged,
                StartDate = SelectedStartDate,
                EndDate = SelectedEndDate,
                IsNew = false,

            };

            loan.Lender = loan.Copy.Owner;

            if (SelectedStartDate.Date == DateTime.Now.Date)
            {
                loan.Ongoing = true;
            }

            foreach (Loan loanTMP in AllLoans)
            {
                if (loan.Copy.Game.ID==loanTMP.Copy.Game.ID && loan.Borrower.ID==loanTMP.Borrower.ID)
                {
                    return false;
                }
            }

            loanDAO.Create(loan);

            //Recherger les donées afin de prendre en compte les changements
            InitAllLoans();
            InitMyLoans();
            InitAllCopies();
            InitMyCopies();
            InitAllGames();

            SelectedStartDate= DateTime.Now;
            SelectedEndDate= DateTime.Now;
            SelectedCopy = null;
            SelectedGame = null;

            return true;

        }

        public bool CreateBooking()
        {
            Booking booking = new Booking {
                Booker= PlayerLogged,
                BookingDate= DateTime.Now,
                Game= SelectedGame,
            };

            if (AllBookings.Contains(booking))
            {
                return false;
            }

            bookingDAO.Create(booking);

            AllBookings.Add(booking);
            MyBookings.Add(booking);
            SelectedGame = null;

            return true;
        }

        #endregion

        #region Finish a Loan Function
        public void EndLoan()
        {
            PriceCost = GetPrice();

            PlayerLogged.Credit = PlayerLogged.Credit - PriceCost;
            SelectedLoan.Lender.Credit = SelectedLoan.Lender.Credit + PriceCost;

            if (PlayerLogged.ID!=SelectedLoan.Lender.ID)
            {
                playerDAO.Update(PlayerLogged);
                playerDAO.Update(SelectedLoan.Lender);
            }
          

            loanDAO.Delete(SelectedLoan);


            List<Booking> bookings= new List<Booking>();

            foreach (Booking booking in AllBookings)
            {
                if (booking.Game.ID==SelectedLoan.Copy.Game.ID)
                {
                    bookings.Add(booking);
                }
            }


            if (bookings.Count>0)
            {
                SelectedBooking = SelectedLoan.Copy.Game.SelectBooking(bookings);
                Loan newLoan = new Loan {
                    Borrower = SelectedBooking.Booker,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    Ongoing = false,
                    IsNew = true,
                    Copy=SelectedLoan.Copy,
                };
                bookingDAO.Delete(SelectedBooking);
                loanDAO.Create(newLoan);
            }


            InitAll();

            SelectedLoan = null;
            SelectedBooking = null;
        }



        public int GetPrice() {
            int price = 0;
            if (SelectedLoan.EndDate.Date>=DateTime.Today)
            {
                price = CalculatePrice(SelectedLoan.StartDate, DateTime.Today);
            }
            else
            {
                price = CalculatePrice(SelectedLoan.StartDate.Date, SelectedLoan.EndDate.Date);
                price = price + (DateTime.Today.DayOfYear - SelectedLoan.EndDate.DayOfYear) * 5;
            }

            return price;
        }

        public int CalculatePrice(DateTime startDate,DateTime endDate) {
            int price = 0;
   
            int nDays = endDate.DayOfYear - startDate.DayOfYear;

            if (nDays ==0)
            {
                price = SelectedLoan.Copy.Game.CreditCost;
            }
            else
            {
                if (nDays % 7 == 0)
                {
                    price = SelectedLoan.Copy.Game.CreditCost * (nDays / 7);
                }
                else
                {
                    price = SelectedLoan.Copy.Game.CreditCost * (((nDays - nDays % 7) / 7) + 1);
                }
            }
            

            return price;
        }

        #endregion

        #endregion

        #region Loan on Connection Function
        public bool HasNewLoan()
        {
            foreach (Loan loan in MyLoans)
            {
                if (loan.IsNew)
                {
                    SelectedLoan=loan;
                    return true;
                }
            }
            return false;
        }

        public void UpdateLoan()
        {
            SelectedLoan.StartDate = SelectedStartDate;
            SelectedLoan.EndDate = SelectedEndDate;
            SelectedLoan.IsNew = false;

            if (SelectedStartDate.Date == DateTime.Now.Date)
            {
                SelectedLoan.Ongoing = true;
            }
            else
            {
                SelectedLoan.Ongoing = false;
            }

            loanDAO.Update(SelectedLoan);

            InitAllLoans();
            InitMyLoans();
            InitAllCopies();
            InitMyCopies();
            InitAllGames();

            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            SelectedLoan = null;

        }

        public void CancelLoan()
        {
            loanDAO.Delete(SelectedLoan);
            InitAllLoans();
            InitMyLoans();
            InitAllCopies();
            InitMyCopies();
            InitAllGames();
        }

        #endregion

        #endregion

    }
}
