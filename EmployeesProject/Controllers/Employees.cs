using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

using EmployeesProject.Models;
using System.Data.Common;
using DbManager;

namespace EmployeesProject.Controllers
{
    public class Employees : Controller
    {
        public IActionResult Index(string name) // display View Having Table
        {
            DBConnection conn = new();
            OracleConnection connectionOracle;
            connectionOracle = conn.Get_con();
            ViewData["Message"] = "Server Version " + name + connectionOracle.ServerVersion;
            int id = 457;
            List<String> emps = conn.ExecSelect("SELECT fname FROM employee");
            ViewData["eSize"] = emps.Count;
            ViewBag.List = emps;
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm] Employee employee) // Display Form For Insert
        {
            EmployeeOperation employeeOperation = new EmployeeOperation();
            employeeOperation.InsertNewEmployee(employee);

            return View();

        }
        [HttpPost]
        public void InsertEmployee([FromForm] Employee employee)
        {
            var emp = new EmployeeOperation();
            emp.InsertNewEmployee(employee);

        }

        [HttpPost]
        public void UpdateEmployee([FromForm] Employee employee)
        {
            var emp = new EmployeeOperation();
            emp.UpdateEmployee(employee);

        }
        [HttpPost]
        public void DeleteEmployee([FromForm] Employee employee)
        {
            var emp = new EmployeeOperation();
            emp.DeleteEmployee(employee);

        }
        [HttpPost]
        public void existsEmployee([FromForm] Employee employee)
        {
            var emp = new EmployeeOperation();
           
            bool exists =  emp.IdExists(employee);


        }
    }
}
