using System.Data.OracleClient;
using System.Data;
using System.Linq;
namespace EmployeesProject.Controllers
{
    
    public class DBConnection
    {
        OracleConnection con = new OracleConnection(orcl);   
    }
}
