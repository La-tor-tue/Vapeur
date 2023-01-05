using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vapeur.Business.Metier;

namespace Vapeur.Business.DAO
{
    public class GameDAO : DAO<VideoGame>
    {
        public GameDAO() { }

        public override bool Create(VideoGame obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Game(title,cost,console) VALUES( @title , @cost , @console)", connection);
                    cmd.Parameters.AddWithValue("title", obj.Title);
                    cmd.Parameters.AddWithValue("cost", obj.CreditCost);
                    cmd.Parameters.AddWithValue("console", obj.Console);
                    cmd.Parameters.AddWithValue("id", obj.ID);

                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;

                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }

            return success;

        }

        public override bool Delete(VideoGame obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.game WHERE idGame=@id", connection);
                    cmd.Parameters.AddWithValue("id", obj.ID);
                    connection.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }

            return success;
        }

        public override List<VideoGame> GetAll()
        {
            List<VideoGame> games = new List<VideoGame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.game", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoGame game = new VideoGame
                        {
                            ID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            CreditCost = reader.GetInt32(2),
                            Console = reader.GetString(3),
                            Copies = new List<Copy>()
                    };

                        games.Add(game);
                    }
                }
            }
            
            return games;
        }

        public override VideoGame Read(int id)
        {
            VideoGame game = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.game WHERE idGame = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new VideoGame
                            {
                                ID = reader.GetInt32(0),
                                Title= reader.GetString(1),
                                CreditCost= reader.GetInt32(2),
                                Console= reader.GetString(3),
                                Copies=new List<Copy>()

                            };
                        }
                    }
                }
                
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! "+e.ErrorCode);
            }
            return game;

        }

        public override VideoGame Read(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public override VideoGame Read(string chain)
        {
            throw new NotImplementedException();
        }

        public override bool Update(VideoGame obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.game SET title = @title ,cost = @cost ,console = @console WHERE idGame= @id", connection);

                    cmd.Parameters.AddWithValue("title", obj.Title);
                    cmd.Parameters.AddWithValue("cost", obj.CreditCost);
                    cmd.Parameters.AddWithValue("console", obj.Console);
                    cmd.Parameters.AddWithValue("id", obj.ID);

                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            


            return success;
        }
    }
}
