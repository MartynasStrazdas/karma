using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karma.Data
{
    public class CharityService
    {
        // Returns a database connection string
        private static string GetDatabaseConnectionString()
        {
            SqlConnectionStringBuilder strngbuilder = new();
            strngbuilder.DataSource = "db-karma.database.windows.net";
            strngbuilder.UserID = "db-karma";
            strngbuilder.Password = Environment.GetEnvironmentVariable("db-password", EnvironmentVariableTarget.Machine);
            strngbuilder.InitialCatalog = "db-karma";
            return strngbuilder.ConnectionString;
        }

        public static Task<List<Charity>> GetCharitiesAsync()
        {
            List<Charity> charities = new List<Charity>();

            try
            {
                SqlConnection mysqlconnection = new(GetDatabaseConnectionString());

                StringBuilder sb = new();
                sb.Append("SELECT * from charity");
                String mysql = sb.ToString();

                SqlCommand command = new(mysql, mysqlconnection);
                mysqlconnection.Open();
                SqlDataReader datareader = command.ExecuteReader();

                while (datareader.Read())
                {
                    if (datareader.IsDBNull(4)) // Website can be null
                    {
                        charities.Add(new Charity(datareader.GetInt32(0), datareader.GetString(1), datareader.GetString(2), datareader.GetDateTime(3), ""));
                    }
                    else
                    {
                        charities.Add(new Charity(datareader.GetInt32(0), datareader.GetString(1), datareader.GetString(2), datareader.GetDateTime(3), datareader.GetString(4)));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Task.FromResult(charities);
        }
        
        public static void AddCharityToDataBase(Charity charity)
        {
            if (charity != null)
            {
                try
                {
                    SqlConnection mysqlconnection = new(GetDatabaseConnectionString());
                    mysqlconnection.Open();
                    string sql = "INSERT INTO charity(name, description, added, website) VALUES(@param1,@param2,GETDATE(),@param3)";

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new(sql, mysqlconnection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.NVarChar).Value = charity.Name;
                        cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = charity.Description;
                        cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = charity.Website;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                } 
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

                System.Diagnostics.Debug.WriteLine(charity.Name + " " + charity.Description + " " + charity.Website);
            }
        }



    }
}
