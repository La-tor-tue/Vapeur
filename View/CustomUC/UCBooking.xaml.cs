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

namespace Vapeur.View.CustomUC
{
    /// <summary>
    /// Logique d'interaction pour UCBooking.xaml
    /// </summary>
    public partial class UCBooking : UserControl
    {
        public UCBooking()
        {
            InitializeComponent();
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController = this.DataContext as MainController;

            if (mainController.SelectedBooking!=null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez vous vraiment annuler votre réservation","Attention", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        mainController.CancelBooking();
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Selectionner une réservation");
            }
        }
    }
}
