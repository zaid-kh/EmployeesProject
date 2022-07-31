using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DbManager
{
    public class DBConnection
    {

        OracleConnection connection;


        public DBConnection()
        {
            connection = GetConnection();
        }

        public OracleConnection Get_con()
        {
            return connection;
        }

        public OracleConnection GetConnection()
        {

            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
            }
            else
            {

                string connString = ConnectionString();

                if (connection == null)
                    connection = new OracleConnection(connString);
                connection.Open();
            }


            return connection;


        }

        public void Close(OracleConnection aOracleConnection)
        {
            if (aOracleConnection != null && aOracleConnection.State == ConnectionState.Open)
            {
                aOracleConnection.Close();
            }
        }

        public string ConnectionString()
        {

            string json = "";
            using (StreamReader r = new("appsettings.json"))
            {
                json = r.ReadToEnd();
            }
            dynamic array = JsonConvert.DeserializeObject(json);
            return array["ConnectionString"]["DefaultConnection"];
        }
        public List<string> ExecSelect(String command)
        {
            OracleCommand cmd = connection.CreateCommand();
            try
            {
                cmd.BindByName = true;
                cmd.CommandText = command;
                OracleDataReader reader = cmd.ExecuteReader();
                List<string> rows = new();
                while (reader.Read())
                {
                    var thisString = reader.GetString(0);
                    rows.Add(thisString);
                }
                return rows;

                reader.Dispose();
                reader.Close();
            }
            catch (Exception)
            {
                return null;

            }
            return null;
        }
    }
}