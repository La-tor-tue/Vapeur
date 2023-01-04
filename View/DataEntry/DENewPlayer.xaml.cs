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
    /// Logique d'interaction pour DENewPlayer.xaml
    /// </summary>
    public partial class DENewPlayer : Window
    {
        public DENewPlayer()
        {
            InitializeComponent();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LogInController logInController = this.DataContext as LogInController;
            logInController.NewPlayer = null;
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LogInController logInController = this.DataContext as LogInController;
            if (logInController.NewPlayer.Username!="" && logInController.NewPlayer.Pseudo != "" && this.fieldNewPassword.Password!="")
            {
                if (logInController.NewPlayer.BirthDate.Date<DateTime.Today.Date)
                {
                    
                    logInController.NewPlayer.Password = this.fieldNewPassword.Password;
                    if (logInController.CreatePlayer())
                    {
                        MessageBox.Show($"Bienvenue! Vous avez reçu: {logInController.NewPlayer.Credit} crédits en cadeau!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                        logInController.NewPlayer = null;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username déjà utilisé!", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez choisir une date correct!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir les champs!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
