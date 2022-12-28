using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.DAO;
using Vapeur.Business.DTO;

namespace Vapeur.Business.POCO
{
    public class GamePOCO
    {
        private GameDAO dbAcces;

        private ObservableCollection<VideoGame> games;

        public ObservableCollection<VideoGame> Games { get { return games; } set { games = value; } }

        public GamePOCO(GameDAO dbAcces)
        {
            this.dbAcces = dbAcces;
        }

        public void AddGame(VideoGame game)
        {
            games.Add(game);
        }
    }
}
