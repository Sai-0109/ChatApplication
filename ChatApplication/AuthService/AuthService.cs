using ChatApplication.Models;
using System.Data.SqlClient;

namespace ChatApplication.AuthService
{
    public class AuthService: IAuthService
    {
         
        private  string _connectionString = "Server=.\\SQLEXPRESS;Database=ChatApplicationDB;Trusted_Connection=True;TrustServerCertificate=True";

        public AuthResult AuthenticateWithAdoNet(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        return new AuthResult { Success = true };

                    }
                    else
                    {
                        return new AuthResult { Success = false, ErrorMessage = "Invalid login attempt" };
                    }
                }
            }
        }
    }
}

