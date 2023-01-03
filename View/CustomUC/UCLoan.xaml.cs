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
    /// Logique d'interaction pour UCLoan.xaml
    /// </summary>
    public partial class UCLoan : UserControl
    {
        public UCLoan()
        {
            InitializeComponent();
        }

        private void btnEndLoan_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController = this.DataContext as MainController;

            if (mainController.SelectedLoan!=null)
            {
                if (mainController.SelectedLoan.Ongoing)
                {
                    MessageBoxResult result = MessageBox.Show("Voulez vous mettre fin à votre location?", "Attention", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            mainController.EndLoan();
                            MessageBox.Show($"Prix de votre location : {mainController.PriceCost} crédit(s)");
                            break;
                        case MessageBoxResult.No:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Veulliez selectionner une location en cours!");
                }
            }
            else
            {
                MessageBox.Show("Veulliez selectionner une location!");
            }
        }
    }
}
