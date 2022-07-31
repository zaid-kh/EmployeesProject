using System.ComponentModel.DataAnnotations;
namespace EmployeesProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? phone { get; set; }
        public double Salary { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }
}
