using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vapeur.Business.DAO;
using Vapeur.Business.DTO;
using Vapeur.View.CustomUC;

namespace Vapeur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<VideoGame> games = new List<VideoGame>();

        public MainWindow()
        {
            InitializeComponent();

            UCGame uCGame = new UCGame();

            grContent.Children.Clear();
            grContent.Children.Add(uCGame);

            DAOFactory adf = DAOFactory.GetFactory(DAOFactoryType.MS_SQL_FACTORY);
            DAO<VideoGame> gameDAO = adf.GetGameDAO();
            DAO<Player> playerDAO = adf.GetPlayerDAO();
            DAO<Booking> bookingDAO = adf.GetBookingDAO();
            DAO<Copy> copyDAO = adf.GetCopyDAO();
            DAO<Loan> loanDAO = adf.GetLoanDAO();

            /*
            Player player = new Player
            {
                Username= "username",
                Password= "password",
                Pseudo="Pseudo",
                Registration=DateTime.Now,
                BirthDate=DateTime.Now,
            };

            playerDAO.Create(player);
            */

            /*
            Player player = playerDAO.Read(4);
            VideoGame game = gameDAO.Read(1);

            Booking booking = new Booking {
                Booker = player,
                Game = game,
                BookingDate = DateTime.Now,
            };
            
            bookingDAO.Create(booking);
            */
            /*
           Booking  booking = bookingDAO.Read(1, 4);
            bookingDAO.Delete(booking);
            */

            /*
            Player player = playerDAO.Read(4);
            VideoGame game = gameDAO.Read(1);

            Copy copy = new Copy {
                Owner= player,
                Game=game,
            };

            copyDAO.Create(copy);
            */

            /*
            Copy copy = copyDAO.Read(1);
            copyDAO.Delete(copy);
            */
            /*
            Player player = playerDAO.Read(4);
            Copy copy = copyDAO.Read(2);

            Loan loan = new Loan {
                Borrower = player,
                Copy = copy,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Ongoing = false,
                
            };
            */
            /*
            Loan loan = loanDAO.Read(2, 4);
            loanDAO.Delete(loan);
            */


        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            UCGame uCGame = new UCGame();

            grContent.Children.Clear();
            grContent.Children.Add(uCGame);
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            UCBooking uCBooking= new UCBooking();

            grContent.Children.Clear();
            grContent.Children.Add(uCBooking);
        }

        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {
            UCLoan uCLoan =new UCLoan();

            grContent.Children.Clear();
            grContent.Children.Add(uCLoan);
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            UCMyGame uCMyGame=new UCMyGame();

            grContent.Children.Clear();
            grContent.Children.Add(uCMyGame);
        }
    }
}
