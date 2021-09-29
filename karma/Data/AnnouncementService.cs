using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karma.Data
{
    public class AnnouncementService
    {
        public static Task<List<Announcement>> GetAnnouncementsAsync()
        {
            List<Announcement> announcements = new List<Announcement>();

            try
            {
                SqlConnection mysqlconnection = new(Startup.DBConnectionString);

                StringBuilder sb = new();
                sb.Append("SELECT * from announcements");
                String mysql = sb.ToString();

                SqlCommand command = new(mysql, mysqlconnection);
                mysqlconnection.Open();
                SqlDataReader datareader = command.ExecuteReader();

                while (datareader.Read())
                {
                    announcements.Add(new Announcement(datareader.GetInt32(0), datareader.GetString(1), datareader.GetString(2), datareader.GetDateTime(3)));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return Task.FromResult(announcements);
        }
        public static void AddAnnouncementToDataBase(Announcement announcement)
        {
            if (announcement != null)
            {
                try
                {
                    SqlConnection mysqlconnection = new(Startup.DBConnectionString);
                    mysqlconnection.Open();
                    string sql = "INSERT INTO announcements(name, description, added) VALUES(@param1,@param2,GETDATE())";

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new(sql, mysqlconnection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.NVarChar).Value = announcement.Name;
                        cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = announcement.Description;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

                System.Diagnostics.Debug.WriteLine(announcement.Name + " " + announcement.Description);
            }
        }
    }
}
