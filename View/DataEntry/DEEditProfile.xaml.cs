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
    /// Logique d'interaction pour DEEditProfile.xaml
    /// </summary>
    public partial class DEEditProfile : Window
    {
        public DEEditProfile()
        {
            InitializeComponent();

        }

        private void btnSaveCreate_Click(object sender, RoutedEventArgs e)
        {
            MainController mainController = this.DataContext as MainController;
            mainController.EditProfile();
            this.Close();
        }

        private void btnCancelCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
