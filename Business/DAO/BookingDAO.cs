using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.Metier;

namespace Vapeur.Business.DAO
{
    internal class BookingDAO : DAO<Booking>
    {
        public BookingDAO() { }
        public override bool Create(Booking obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.booking (idGame,idPlayer,bookingDate) VALUES ( @idG , @idP , @bookingDate)", connection);
                    cmd.Parameters.AddWithValue("idG", obj.Game.ID);
                    cmd.Parameters.AddWithValue("idP", obj.Booker.ID);
                    cmd.Parameters.AddWithValue("bookingDate", obj.BookingDate);

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

        public override bool Delete(Booking obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.booking WHERE idPlayer = @idP and idGame = @idG", connection);
                    cmd.Parameters.AddWithValue("idP", obj.Booker.ID);
                    cmd.Parameters.AddWithValue("idG", obj.Game.ID);

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

        public override List<Booking> GetAll()
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.booking", connection);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            Booking booking = new Booking
                            {
                                BookingDate = reader.GetDateTime(2),
                                Game = new VideoGame(),
                                Booker = new Player()
                            };

                            booking.Game.ID = reader.GetInt32(0);
                            booking.Booker.ID = reader.GetInt32(1);

                            booking = Read(booking.Game.ID, booking.Booker.ID);

                            bookings.Add(booking);

                        }
                    }
                }
            }
            catch (SqlException e)
            {

                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }

            return bookings;
        }

        // INUTILISABLE CAR ID CONCAT
        public override Booking Read(int id)
        {
            throw new NotImplementedException();
        }

        public override Booking Read(int idG,int idP)
        {
            Booking booking = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.booking WHERE idPlayer = @idP and idGame = @idG", connection);
                    cmd.Parameters.AddWithValue("idP", idP);
                    cmd.Parameters.AddWithValue("idG", idG);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            booking = new Booking
                            {
                                BookingDate = reader.GetDateTime(2),
                                Game = new VideoGame(),
                                Booker = new Player()
                            };

                            booking.Game.ID= reader.GetInt32(0);
                            booking.Booker.ID = reader.GetInt32(1);
                        }
                    }
                }
                if (booking!=null)
                {
                    //Completion de l'objet 
                    GameDAO gameDAO = new GameDAO();
                    VideoGame game = gameDAO.Read(booking.Game.ID);
                    booking.Game = game;

                    PlayerDAO playerDAO = new PlayerDAO();
                    Player player = playerDAO.Read(booking.Booker.ID);
                    booking.Booker= player;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            return booking;
        }
        //INUTILE POUR BOOKING
        public override bool Update(Booking obj)
        {
            throw new NotImplementedException();
        }
    }
}
