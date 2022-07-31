namespace EmployeesProject.Models
{
    using DbManager;
    using Oracle.ManagedDataAccess.Client;
    using System.Data;

    public class EmployeeOperation
    {
        DBConnection conn;
        OracleConnection aOracleConnection;
        OracleTransaction CmdTrans;

        private void Open()
        {
            conn = new DBConnection();
            aOracleConnection = conn.Get_con();
        }

        private void Close()
        {
            conn.Close(aOracleConnection);
        }
        private void Commit()
        {
            CmdTrans.Commit();
        }

        private void Rollback()
        {
            CmdTrans.Rollback();
        }
        public bool InsertNewEmployee(Employee data)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var cmdText = "INSERT INTO employee(pid,fname,lname,phone) " +
                          "VALUES" +
                          "(:pid, " +
                          ":fname, " +
                          ":lname, " +
                          ":phone)";


                // create command and set properties
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;

                cmd.Parameters.Add(":pid", OracleDbType.Int64).Value = data.Id;
                cmd.Parameters.Add(":fname", OracleDbType.NVarchar2).Value = data.Fname;
                cmd.Parameters.Add(":lname", OracleDbType.NVarchar2).Value = data.Lname;
                cmd.Parameters.Add(":phone", OracleDbType.NVarchar2).Value = data.phone;

                cmd.ExecuteNonQuery();

                CmdTrans.Commit();
                return true;

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                Close();
            }
        }


    }
}