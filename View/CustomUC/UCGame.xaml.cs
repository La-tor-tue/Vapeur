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
using Vapeur.View.DataEntry;

namespace Vapeur.View.CustomUC
{
    /// <summary>
    /// Logique d'interaction pour UCGame.xaml
    /// </summary>
    public partial class UCGame : UserControl
    {
        public UCGame()
        {
            InitializeComponent();
        }

        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController = this.DataContext as MainController;

            if (mainController.SelectedGame != null)
            {
                if (mainController.CanBorrow())
                {
                    if (mainController.IsPossible()==true)
                    {
                        DESelectDate dESelectDate = new DESelectDate();
                        dESelectDate.DataContext = mainController;
                        dESelectDate.ShowDialog();

                        if (mainController.CreateLoan())
                        {
                            MessageBox.Show("Location créée.");
                        }
                        else
                        {
                            MessageBox.Show("Location déjà en cours!");
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Aucune copy du jeux n'est disponible. Voulez-vous faire une réservation ?", "Attention", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                if (mainController.CreateBooking())
                                {
                                    MessageBox.Show("Réservation créée.");
                                }
                                else
                                {
                                    MessageBox.Show("Réservation déjà en cours!");
                                }
                                break;
                            case MessageBoxResult.No:
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Votre compte doit avoir plus de crédit pour emprunter, vous pouvez uniquement mettre en location.");
                }
            }
            else
            {
                MessageBox.Show("Selectionnez un jeu.");
            }


        }
    }
}
