using ChatApplication.AuthService;
using ChatApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.AuthenticateWithAdoNet(model.Email, model.Password);

                if (result.Success)
                {
                    HttpContext.Session.SetString("Key", $"{model.Email}");
                   // TempData["LoggedInUser"] = model.Email;
                    return RedirectToAction("Chat", "chat");
                }
                else 
                {
                    return RedirectToAction("Register", "User");
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage);
            }

            // If the model is not valid or login fails, return to the login page
            return View("Register", "user");
        }
    }
}
