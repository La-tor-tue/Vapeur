using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using Vapeur.View.DataEntry;

namespace Vapeur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {


        private MainController mainController;
        private readonly DAOFactory adf = DAOFactory.GetFactory(DAOFactoryType.MS_SQL_FACTORY);

        public MainWindow()
        {
            InitializeComponent();

            mainController = new MainController(adf,new Player {
                ID=4,
                Username= "username",
                Password= "username",
                Credit=10,
                Pseudo="Pseudo",
                Registration=new DateTime(2022,12,28),
                BirthDate=new DateTime(2022, 12, 28)
            });;


            while(mainController.HasNewLoan())
            {
                MessageBoxResult result = MessageBox.Show($"Une copie est disponible pour un jeux que vous aviez reservé: {mainController.SelectedLoan.Copy.Game.ToString}\n Voulez-vous le louer?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        DESelectDate dESelectDate = new DESelectDate();
                        dESelectDate.DataContext= mainController;
                        dESelectDate.ShowDialog();

                        mainController.UpdateLoan();

                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            UCGame uCGame = new UCGame();

            uCGame.DataContext = mainController;
            mainController = new MainController(adf,new Player {
                ID=4,
                Username= "username",
                Password= "username",
                Credit=10,
                Pseudo="Pseudo",
                Registration=new DateTime(2022,12,28),
                BirthDate=new DateTime(2022, 12, 28)
            });;

            uCGame.DataContext= mainController;

            grContent.Children.Clear();
            grContent.Children.Add(uCGame);
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            UCGame uCGame = new UCGame();

            uCGame.DataContext = mainController;

            grContent.Children.Clear();
            grContent.Children.Add(uCGame);
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            UCBooking uCBooking= new UCBooking();

            uCBooking.DataContext = mainController;
            mainController.SelectedBooking = null;
            grContent.Children.Clear();
            grContent.Children.Add(uCBooking);
        }

        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {
            UCLoan uCLoan =new UCLoan();

            uCLoan.DataContext = mainController;
            grContent.Children.Clear();
            grContent.Children.Add(uCLoan);
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            UCMyGame uCMyGame=new UCMyGame();

            uCMyGame.DataContext = mainController;

            grContent.Children.Clear();
            grContent.Children.Add(uCMyGame);
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            UCProfile uCProfile=new UCProfile();
            uCProfile.DataContext = mainController;

            grContent.Children.Clear();
            grContent.Children.Add(uCProfile);
        }
    }
}
