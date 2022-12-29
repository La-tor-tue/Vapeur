﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Vapeur.Business.Metier;

namespace Vapeur.Business.DAO
{
    public class LoanDAO : DAO<Loan>
    {
        public override bool Create(Loan obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.loan (idPlayer,idCopy,startDate,endDate,ongoing) VALUES ( @idP,@idC, @start,@end,@ongoing)", connection);
                    
                    cmd.Parameters.AddWithValue("idP", obj.Borrower.ID);
                    cmd.Parameters.AddWithValue("idC", obj.Copy.ID);
                    cmd.Parameters.AddWithValue("start", obj.StartDate);
                    cmd.Parameters.AddWithValue("end", obj.EndDate);
                    if (obj.Ongoing)
                    {
                        cmd.Parameters.AddWithValue("ongoing", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("ongoing", 0);
                    }

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

        public override bool Delete(Loan obj)
        {
            bool success = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.loan WHERE idPlayer = @idP and idCopy = @idC", connection);

                    cmd.Parameters.AddWithValue("idP", obj.Borrower.ID);
                    cmd.Parameters.AddWithValue("idC", obj.Copy.ID);

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

        public override List<Loan> GetAll()
        {
            List<Loan> loans = new List<Loan>();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.loan", connection);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            Loan loan = new Loan
                            {
                                StartDate = reader.GetDateTime(2),
                                EndDate = reader.GetDateTime(3),
                                Ongoing = reader.GetInt32(4) == 1,
                                Copy = new Copy(),
                                Borrower = new Player(),
                            };

                            loan.Copy.ID = reader.GetInt32(1);
                            loan.Borrower.ID = reader.GetInt32(0);

                            loan = Read(loan.Copy.ID, loan.Borrower.ID);

                            loans.Add(loan);

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            return loans;
        }

        //Inutile car 2 ids
        public override Loan Read(int id)
        {
            throw new NotImplementedException();
        }

        public override Loan Read(int idG, int idP)
        {
            Loan loan = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.loan WHERE idPlayer = @idP and idCopy = @idG", connection);
                    cmd.Parameters.AddWithValue("idP", idP);
                    cmd.Parameters.AddWithValue("idG", idG);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            loan = new Loan
                            {
                                StartDate = reader.GetDateTime(2),
                                EndDate= reader.GetDateTime(3),
                                Ongoing= reader.GetInt32(4) == 1,
                                Copy = new Copy(),
                                Borrower = new Player(),
                            };
                            
                            loan.Copy.ID = reader.GetInt32(1);
                            loan.Borrower.ID = reader.GetInt32(0);


                        }
                    }
                }
                if (loan != null)
                {
                    //Completion de l'objet 
                    CopyDAO CopyDAO = new CopyDAO();
                    Copy copy = CopyDAO.Read(loan.Copy.ID);
                    loan.Copy = copy;
                    loan.Lender = copy.Owner;

                    PlayerDAO playerDAO = new PlayerDAO();
                    Player player = playerDAO.Read(loan.Borrower.ID);
                    loan.Borrower = player;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Une erreur sql s'est produite! " + e.ErrorCode);
            }
            return loan;
        }

        public override bool Update(Loan obj)
        {
            bool success = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.loan SET ongoing = @ongoing WHERE idPlayer = @idP and idCopy = @idC", connection);

                    if (obj.Ongoing)
                    {
                        cmd.Parameters.AddWithValue("ongoing", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("ongoing", 0);
                    }
                    cmd.Parameters.AddWithValue("idP", obj.Borrower.ID);
                    cmd.Parameters.AddWithValue("idC", obj.Copy.ID);

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
