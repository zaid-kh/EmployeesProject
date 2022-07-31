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
            if (!IdExists(data))
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
            else return false;
        }

        public bool UpdateEmployee(Employee data) // update employee if such pid exists
        {
            if (IdExists(data))
            {
                //Open Connection
                Open();
                OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    var cmdText = "UPDATE employee SET" +
                                  "fname = :fname, " +
                                  "lname = :lname, " +
                                  "phone = :phone," +
                                  "salary = :salary," +
                                  "bdate = :bdate)" +
                                  "WHERE " +
                                  "pid = :pid ";

                    // create command and set properties
                    OracleCommand cmd = aOracleConnection.CreateCommand();
                    cmd.Transaction = CmdTrans;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = cmdText;

                    cmd.Parameters.Add(":fname", OracleDbType.NVarchar2).Value = data.Fname;
                    cmd.Parameters.Add(":lname", OracleDbType.NVarchar2).Value = data.Lname;
                    cmd.Parameters.Add(":phone", OracleDbType.NVarchar2).Value = data.phone;
                    cmd.Parameters.Add(":salary", OracleDbType.Int32).Value = data.Salary;
                    cmd.Parameters.Add(":bdate", OracleDbType.Date).Value = data.BirthDate;
                    cmd.Parameters.Add(":pid", OracleDbType.Int64).Value = data.Id;

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
            else return false;
        }
        public bool DeleteEmployee(Employee data) // delete employee from table if such pid exists
        {
            if (IdExists(data))
            {
                //Open Connection
                Open();
                OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    var cmdText = "DELETE FROM employee " +
                                   "WHERE" +
                                    "pid = :pid";


                    // create command and set properties
                    OracleCommand cmd = aOracleConnection.CreateCommand();
                    cmd.Transaction = CmdTrans;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = cmdText;

                    cmd.Parameters.Add(":pid", OracleDbType.Int64).Value = data.Id;

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
            else return false;

        }

        /* a method to check the occurrance of passed pid */
        private bool IdExists(Employee data)
        {

            DataTable dt = QueryReader("SELECT E.pid FROM employee E WHERE E.pid = :pid");

            if (dt != null) // db is empty
            {
                bool hasRows = dt.Rows.GetEnumerator().MoveNext(); // datatable has at least one row
                if (hasRows)
                    return true;
            }
            return false;
        }

        public DataTable QueryReader(string QUERY)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                // Set the command
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd = new OracleCommand(QUERY, aOracleConnection);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                // Bind 
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CmdTrans.Commit();
                return dt;
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