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
using Vapeur.Business.DAO;
using Vapeur.View.DataEntry;

namespace Vapeur.View
{
    /// <summary>
    /// Logique d'interaction pour login.xaml
    /// </summary>
    public partial class login : Window
    {
        private LogInController logInController;
        private readonly DAOFactory adf = DAOFactory.GetFactory(DAOFactoryType.MS_SQL_FACTORY);
        public login()
        {

            InitializeComponent();
            logInController = new LogInController(adf);
            this.DataContext= logInController;
        }


        private void btnLogInConnexion_Click(object sender, RoutedEventArgs e)
        {
            if (logInController.PlayerLogged.Username!="" && this.fieldPass.Password!="")
            {
                if (logInController.Exist())
                {
                    logInController.Password= this.fieldPass.Password;
                    if (logInController.Match())
                    {
                        logInController.GetData();
                        MainWindow mainWindow = new MainWindow(logInController.PlayerLogged);
                        logInController.PlayerLogged = null;
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mot de pass incorrect!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Ce compte n'existe pas!", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir les champs!","Attention",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            DENewPlayer dENewPlayer = new DENewPlayer();
            dENewPlayer.DataContext= logInController;
            dENewPlayer.ShowDialog();

        }
    }
}
