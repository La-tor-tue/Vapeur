using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.DAO;
using Vapeur.Business.Metier;

namespace Vapeur.Business.Controller
{
    internal class PlayerController
    {
        private DAO<Player> playerDAO;
        private DAO<Copy> copyDAO;
        private DAO<VideoGame> gameDAO;

        private Player player;
        private ObservableCollection<Copy> copies;
        
        public PlayerController(DAOFactory dAOFactory,int id) { 
            playerDAO = dAOFactory.GetPlayerDAO();
            copyDAO = dAOFactory.GetCopyDAO();
            gameDAO = dAOFactory.GetGameDAO();

            player = playerDAO.Read(id);
            copies = new ObservableCollection<Copy>();

            List<Copy> copiesTmp = copyDAO.GetAll();

            foreach (Copy copy in copiesTmp)
            {
                if (copy.Owner.ID == player.ID)
                {
                    copies.Add(copy);
                }
            }
            for (int i = 0; i < copies.Count; i++)
            {
                copies[i].Game= gameDAO.Read(copies[i].Game.ID);
            }
        }

        public ObservableCollection<Copy> MyCopies { get { return copies; } set { copies = value; } }
    }
}
