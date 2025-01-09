using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace NoteApi__.NET_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Welcome, Admin!";
            return View();
        }
    }
}

