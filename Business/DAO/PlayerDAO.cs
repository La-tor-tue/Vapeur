using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.Metier;

namespace Vapeur.Business.DAO
{
    public class PlayerDAO : DAO<Player>
    {
        public PlayerDAO() { }
        public override bool Create(Player obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.player (username,password,isAdmin,credit,pseudo,registrationDate,birthDate) VALUES ( @username , @password , 0, @credit , @pseudo , @registration , @birth )", connection);
                    cmd.Parameters.AddWithValue("username", obj.Username);
                    cmd.Parameters.AddWithValue("password", obj.Password);
                    cmd.Parameters.AddWithValue("credit", obj.Credit);
                    cmd.Parameters.AddWithValue("pseudo", obj.Pseudo);
                    cmd.Parameters.AddWithValue("registration", obj.Registration.Date);
                    cmd.Parameters.AddWithValue("birth", obj.BirthDate.Date);
                    connection.Open();

                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("'UK_Username'"))
                {
                    return success;
                }
                else
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode+ " "+e.Message);
            }



            return success;
        }

        public override bool Delete(Player obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.player WHERE idPlayer=@id", connection);
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

        public override List<Player> GetAll()
        {
            List<Player> players = new List<Player>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.player", connection);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Player player = new Player
                            {
                                ID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Credit = reader.GetInt32(4),
                                Pseudo = reader.GetString(5),
                                Registration = reader.GetDateTime(6),
                                BirthDate = reader.GetDateTime(7)
                            };

                            players.Add(player);
                        }
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            
            return players;
        }

        public override Player Read(int id)
        {
            Player player = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.player WHERE idPlayer = @id", connection);
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            player = new Player
                            {
                                ID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Credit = reader.GetInt32(4),
                                Pseudo = reader.GetString(5),
                                Registration = reader.GetDateTime(6),
                                BirthDate = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            return player;
        }

        public override Player Read(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public override Player Read(string chain)
        {
            Player player = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.player WHERE username = @username", connection);
                    cmd.Parameters.AddWithValue("username", chain);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            player = new Player
                            {
                                ID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                            };
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            return player;
        }

        public override bool Update(Player obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.player SET credit = @credit , pseudo= @pseudo WHERE idPlayer= @id ", connection);

                    cmd.Parameters.AddWithValue("credit", obj.Credit);
                    cmd.Parameters.AddWithValue("pseudo", obj.Pseudo);
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
