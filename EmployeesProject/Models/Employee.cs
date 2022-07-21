using System.ComponentModel.DataAnnotations;
namespace EmployeesProject.Models
{
    public class Employee
    {
        int Id { get; set; }
        string? Fname { get; set; }
        string? Lname { get; set; }
        string? phone { get; set; }
        double Salary { get; set; }
        [DataType(DataType.Date)]
        DateTime ReleaseDate { get; set; }

    }
}
