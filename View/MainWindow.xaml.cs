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
using Vapeur.Business.Controller;
using Vapeur.Business.DAO;
using Vapeur.Business.Metier;
using Vapeur.View.CustomUC;

namespace Vapeur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        private readonly DAOFactory adf = DAOFactory.GetFactory(DAOFactoryType.MS_SQL_FACTORY);
        public MainWindow()
        {
            InitializeComponent();


            UCGame uCGame = new UCGame();

            CatalogueController catalogueController = new CatalogueController(adf);

            uCGame.DataContext = catalogueController;

            grContent.Children.Clear();
            grContent.Children.Add(uCGame);

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

            PlayerController playerController = new PlayerController(adf, 4);

            uCMyGame.DataContext = playerController;


            grContent.Children.Clear();
            grContent.Children.Add(uCMyGame);
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
