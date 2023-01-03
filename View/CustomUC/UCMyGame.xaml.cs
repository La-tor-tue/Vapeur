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
    /// Logique d'interaction pour UCMyGame.xaml
    /// </summary>
    public partial class UCMyGame : UserControl
    {
        public UCMyGame()
        {
            InitializeComponent();
        }

        private void btnCreateCopy_Click(object sender, RoutedEventArgs e)
        {
            DataEntry.DECreateCopy dECreateCopy= new DataEntry.DECreateCopy();

            dECreateCopy.DataContext= this.DataContext as MainController;

            dECreateCopy.ShowDialog();
        }
    }
}
