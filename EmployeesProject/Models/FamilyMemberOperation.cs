namespace EmployeesProject.Models
{
    using DbManager;
    using Oracle.ManagedDataAccess.Client;
    using System.Data;
    public class FamilyMemberOperation
    {
        DBConnection conn;
        OracleConnection aOracleConnection;
        OracleTransaction CmdTrans;

        private void Open()
        {
            conn = new DBConnection();
            aOracleConnection = conn.Get_con();
            CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
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
        public bool InsertNewFamilyMember(FamilyMember data, string USER_ID)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {

                var cmdText = @"{INSERT INTO employee(PID,fname,lname,phone)
                                 VALUES
                                    (PID  = :pid,
                                    fname = :fname,
                                    lname = :lname,
                                    phone = :phone)}";



                // create command and set properties
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;

                cmd.Parameters.Add(":PID", OracleDbType.Int64).Value = data.Id;
                cmd.Parameters.Add(":fname", OracleDbType.NVarchar2).Value = data.Fname;
                cmd.Parameters.Add(":lname", OracleDbType.NVarchar2).Value = data.Lname;
                cmd.Parameters.Add(":phone", OracleDbType.NVarchar2).Value = data.emp_id;



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
