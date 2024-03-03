using ChatApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ChatApplication.Repository
{
    public class UserRepository: IUserRepository
    {
        private  string _connectionString= "Server=.\\SQLEXPRESS;Database=ChatApplicationDB;Trusted_Connection=True;TrustServerCertificate=True";

       

        public void EditUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Call stored procedure to edit user
                string storedProcedureName = "EditUser";
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Call stored procedure to delete user
                string storedProcedureName = "DeleteUser";
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "GetAllUsers"; // Stored Procedure Name
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                UserId = (int)reader["UserId"],
                                Email = reader["Email"].ToString(),
                                // Add other properties as needed
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public UserModel GetUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "GetUserById"; // Stored Procedure Name
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                UserId = (int)reader["UserId"],
                                Email = reader["Email"].ToString(),
                                // Add other properties as needed
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }
        // Implement other repository methods based on your requirements
    }

  
}

