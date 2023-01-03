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
    /// Logique d'interaction pour UCProfile.xaml
    /// </summary>
    public partial class UCProfile : UserControl
    {
        public UCProfile()
        {
            InitializeComponent();

        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            DEEditProfile dEEditProfile= new DEEditProfile();

            dEEditProfile.DataContext= this.DataContext as MainController;
            dEEditProfile.ShowDialog();
        }
    }
}
