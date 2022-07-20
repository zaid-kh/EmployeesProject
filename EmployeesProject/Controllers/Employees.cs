using Microsoft.AspNetCore.Mvc;

namespace EmployeesProject.Controllers
{
    public class Employees : Controller
    {
        public IActionResult Index() // display View Having Table
        {
            return View();
        }
        public IActionResult Create() // Display Form For Insert
        {
            return View();
        }

    }
}
