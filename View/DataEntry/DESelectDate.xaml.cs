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
using System.Windows.Shapes;
using Vapeur.Business.Controller;

namespace Vapeur.View.DataEntry
{
    /// <summary>
    /// Logique d'interaction pour DESelectDate.xaml
    /// </summary>
    public partial class DESelectDate : Window
    {
        public DESelectDate()
        {
            InitializeComponent();
        }

        private void btnSaveCreate_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController = this.DataContext as MainController;

            if (mainController.SelectedStartDate<mainController.SelectedEndDate)
            {

                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une date de fin après la date de début!");
                mainController.SelectedStartDate = DateTime.Today;
                mainController.SelectedEndDate = DateTime.Today;
            } 
        }
    }
}
