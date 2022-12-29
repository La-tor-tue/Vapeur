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
    public class CatalogueController
    {

        private DAO<VideoGame> gameDAO;
        private ObservableCollection<VideoGame> games;
        public CatalogueController(DAOFactory dAOFactory)
        {
            this.gameDAO = dAOFactory.GetGameDAO();
            this.games = new ObservableCollection<VideoGame> (gameDAO.GetAll());
        }

        public ObservableCollection<VideoGame> Games { get { return games; } set { games = value; } }



    }
}
