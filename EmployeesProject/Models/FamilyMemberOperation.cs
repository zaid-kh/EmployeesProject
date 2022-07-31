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

    }
}
