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
    /// Logique d'interaction pour DECreateCopy.xaml
    /// </summary>
    public partial class DECreateCopy : Window
    {
        public DECreateCopy()
        {
            InitializeComponent();
            
        }

        private void btnCancelCreate_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController = this.DataContext as MainController;
            mainController.SelectedGame = null;
            this.Close();
        }

        private void btnSaveCreate_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController= this.DataContext as MainController;

            if (mainController.SelectedGame!=null)
            {
                mainController.CreateCopy();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selectionnez un jeux");
            }
        }
    }
}
