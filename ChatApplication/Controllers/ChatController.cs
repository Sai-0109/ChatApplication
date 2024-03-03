using ChatApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace ChatApplication.Controllers
{
    public class ChatController : Controller
    {
        private readonly string connectionString = "Server=.\\SQLEXPRESS;Database=ChatApplicationDB;Trusted_Connection=True;TrustServerCertificate=True";

        public IActionResult Chat()
        {
            UserChatViewModel viewModel = new UserChatViewModel
            {
                AvailableUsers = GetUserListFromDatabase()
            };
            string names = HttpContext.Session.GetString("Key");
            return View(viewModel);
        }
        private List<UserModel> GetUserListFromDatabase()
        {
            List<UserModel> userList = new List<UserModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string storedProcedureName = "sp_GetAllUsers";
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                UserId = (int)reader["UserId"],
                                Email = reader["Email"].ToString()
                            };

                            userList.Add(user);
                        }
                    }
                }
            }

            return userList;
        }
    


        [HttpPost]
        public IActionResult SendMessage(int receiverId, string message)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO ChatMessages (SenderId, ReceiverId, Message, Timestamp) VALUES (@SenderId, @ReceiverId, @Message, GETDATE())";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ReceiverId", receiverId);
                    command.Parameters.AddWithValue("@Message", message);

                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Chat", new { userId = receiverId });
        }
        public IActionResult OpenChatWindow()
        {
           
            return View();
        }
    }


}
