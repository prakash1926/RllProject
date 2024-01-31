using Microsoft.AspNetCore.Mvc;
using JobsearchMvc.Models;

namespace JobsearchMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Registration model)
        {
            // Handle registration logic, e.g., save to the database
            // Redirect to login page or perform login after successful registration
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            // Handle login logic, e.g., validate credentials
            // Redirect to the dashboard or return an error message on failure
            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            // Display user-specific dashboard information
            return View();
        }

    }
}
