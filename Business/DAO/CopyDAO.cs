using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.Metier;

namespace Vapeur.Business.DAO
{
    public class CopyDAO : DAO<Copy>
    {
        public override bool Create(Copy obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.copy (idGame,idPlayer) VALUES ( @idG , @idP)", connection);
                    cmd.Parameters.AddWithValue("idG", obj.Game.ID);
                    cmd.Parameters.AddWithValue("idP", obj.Owner.ID);

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

        public override bool Delete(Copy obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.copy WHERE idCopy=@id", connection);
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

        public override List<Copy> GetAll()
        {
            List<Copy> copies = new List<Copy>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.copy", connection);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Copy copy = new Copy
                            {
                                ID = reader.GetInt32(0),
                                Game = new VideoGame(),
                                Owner = new Player(),
<<<<<<< HEAD
                                Loan = new Loan
                                {
                                    Ongoing = false
                                },
=======
                                Loan = new Loan()
>>>>>>> 2e7684875e1c639324fd7c68fca1e1f4a90a2473
                            };

                            copy.Game.ID = reader.GetInt32(1);
                            copy.Owner.ID = reader.GetInt32(2);
                            copies.Add(copy);
                        }
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }

            return copies;
        }

        public override Copy Read(int id)
        {
            Copy copy = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.copy where idCopy=@id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            copy = new Copy
                            {
                                ID = reader.GetInt32(0),
                                Game = new VideoGame(),
                                Owner = new Player(),
                                Loan = new Loan {
                                    Ongoing = false
                                },

                            };

                            copy.Game.ID = reader.GetInt32(1);
                            copy.Owner.ID = reader.GetInt32(2);


                        }
                    }
                }
                /*
                if (copy!=null)
                {
                    GameDAO gameDAO = new GameDAO();
                    VideoGame game = gameDAO.Read(copy.Game.ID);
                    copy.Game = game;

                    PlayerDAO playerDAO = new PlayerDAO();
                    Player player = playerDAO.Read(copy.Owner.ID);
                    copy.Owner = player;
                }
                */
            }
            catch (SqlException e)
            {

                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            return copy;
        }

        //INUTILE
        public override Copy Read(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        //INUTILE
        public override bool Update(Copy obj)
        {
            throw new NotImplementedException();
        }
    }
}
