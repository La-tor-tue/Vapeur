using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.DAO;
using Vapeur.Business.Metier;

namespace Vapeur.Business.Controller
{
    public class LogInController
    {
        private DAO<Player> playerDAO;
        private Player playerLogged;
        private Player newPlayer;
        private string password;
        public LogInController(DAOFactory dAOFactory)
        {
            playerDAO = dAOFactory.GetPlayerDAO();
            this.playerLogged = new Player();
            this.newPlayer = new Player();
            this.password = "";
            PlayerLogged.Username = "Username";
            newPlayer.BirthDate= DateTime.Today;
        }

        public Player PlayerLogged { get { return playerLogged; } set { playerLogged = value; } }
        public string Password { get { return password; } set { password= value; } }
        public Player NewPlayer { get { return newPlayer; } set { newPlayer= value; } }



        public bool Exist()
        {
            
            PlayerLogged = playerDAO.Read(PlayerLogged.Username);
            if (PlayerLogged.ID!=0)
            {
                return true;
            }
            return false;
        }

        public bool Match()
        {
            if(PlayerLogged.Password == Password)
            {
                return true;
            }
            return false;
        }

        public void GetData()
        {
            PlayerLogged=playerDAO.Read(PlayerLogged.ID);
            PlayerLogged.Password = "";
        }

        public bool CreatePlayer()
        {
            NewPlayer.Registration = DateTime.Today;
            if (NewPlayer.BirthDate.Day < DateTime.Today.Day && NewPlayer.BirthDate.Month < DateTime.Today.Month)
            {
                NewPlayer.Credit = 12;
            }
            else
            {
                NewPlayer.Credit = 10;
            }

            if (playerDAO.Create(NewPlayer))
            {

                return true;
            }

            return false;
        }
    }
}
