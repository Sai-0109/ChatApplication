using ChatApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ChatApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly string connectionString = "Server=.\\SQLEXPRESS;Database=ChatApplicationDB;Trusted_Connection=True;TrustServerCertificate=True";

        public IActionResult Register()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string storedProcedureName = "sp_RegisterUser";
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Email", userModel.Email);
                        command.Parameters.AddWithValue("@Password", userModel.Password);

                        command.ExecuteNonQuery();
                    }
                }


                return RedirectToAction("Index", "Login"); 
            }

            return View();
        }
    }

}
