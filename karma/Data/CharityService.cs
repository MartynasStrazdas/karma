using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karma.Data
{
    public class CharityService
    {
        public Task<Charity[]> GetCharitiesAsync(DateTime startDate)
        {
            List<Charity> charities = new List<Charity>();

            try
            {
                SqlConnectionStringBuilder strngbuilder = new();
                strngbuilder.DataSource = Environment.GetEnvironmentVariable("db-url", EnvironmentVariableTarget.Machine);
                strngbuilder.UserID = Environment.GetEnvironmentVariable("db-username", EnvironmentVariableTarget.Machine);
                strngbuilder.Password = Environment.GetEnvironmentVariable("db-password", EnvironmentVariableTarget.Machine);
                strngbuilder.InitialCatalog = Environment.GetEnvironmentVariable("db-username", EnvironmentVariableTarget.Machine);

                SqlConnection mysqlconnection = new(strngbuilder.ConnectionString);

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

            return Task.FromResult(Enumerable.Range(0, charities.Count).Select(index => charities[index]).ToArray());
        }
    }
}
