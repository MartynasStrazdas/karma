using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karma.Data
{
    public class ListingService
    {
        public static Task<List<Listing>> GetListingsAsync()
        {
            List<Listing> listings = new List<Listing>();

            try
            {
                SqlConnection mysqlconnection = new(Startup.DBConnectionString);

                StringBuilder sb = new();
                sb.Append("SELECT * FROM listings ORDER BY added DESC");
                String mysql = sb.ToString();

                SqlCommand command = new(mysql, mysqlconnection);
                mysqlconnection.Open();
                SqlDataReader datareader = command.ExecuteReader();

                while (datareader.Read())
                {
                    listings.Add(new Listing(datareader.GetInt32(0), datareader.GetString(1), datareader.GetString(2), datareader.GetDateTime(3)));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Task.FromResult(listings);
        }
        public static void AddListingToDataBase(Listing listing)
        {
            if (listing != null)
            {
                try
                {
                    SqlConnection mysqlconnection = new(Startup.DBConnectionString);
                    mysqlconnection.Open();
                    string sql = "INSERT INTO listings(title, description, added) VALUES(@param1,@param2,GETDATE())";

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new(sql, mysqlconnection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.NVarChar).Value = listing.Title;
                        cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = listing.Description;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

                System.Diagnostics.Debug.WriteLine(listing.Title + " " + listing.Description);
            }
        }
    }
}

