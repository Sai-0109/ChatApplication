using ChatApplication.Models;
using ChatApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            // Fetch user data from the service
            List<UserModel> users = _userRepository.GetAllUsers();

            // Pass the user data to the view
            return View(users);
        }
        public IActionResult EditUser(int userId)
        {
            // Retrieve the user by Id
            UserModel user = _userRepository.GetUserById(userId);

            if (user == null)
            {
                // Return a 404 Not Found if the user is not found
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/EditUser/5
        [HttpPost]
        public IActionResult EditUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Edit user in the database
                _userRepository.EditUser(user);

                // Redirect to the Users action to show the updated user list
                return RedirectToAction("Users");
            }

            // If the model is not valid, return to the EditUser view with validation errors
            return View(user);
        }

        // GET: Admin/DeleteUser/5
        public IActionResult DeleteUser(int userId)
        {
            // Retrieve the user by Id
            UserModel user = _userRepository.GetUserById(userId);

            if (user == null)
            {
                // Return a 404 Not Found if the user is not found
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteUserConfirmed(int userId)
        {
            // Delete user from the database
            _userRepository.DeleteUser(userId);

            // Redirect to the Users action to show the updated user list
            return RedirectToAction("Users");
        }
    }

}
