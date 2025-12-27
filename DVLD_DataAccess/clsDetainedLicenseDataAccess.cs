using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseDataAccess
    {
        public static bool FindDetainByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees,
            ref int CreatedByUserID , ref bool IsReleased, ref DateTime ReleasDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "Select * From DetainedLicenses Where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleasDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    }
                    else
                        ReleasDate = Convert.ToDateTime(0);

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                    else
                        ReleasedByUserID = 0;

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
                    else
                        ReleaseApplicationID = 0;
                }

                reader.Close();
            }
            catch { }

            finally { connection.Close(); }

            return IsFound;
        }

        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
       ref int DetainID, ref DateTime DetainDate,
       ref float FineFees, ref int CreatedByUserID,
       ref bool IsReleased, ref DateTime ReleaseDate,
       ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)

                        ReleaseDate = DateTime.MaxValue;
                    else
                        ReleaseDate = (DateTime)reader["ReleaseDate"];


                    if (reader["ReleasedByUserID"] == DBNull.Value)

                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)

                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewDetain(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
           int ReleasedByUserID, int ReleaseApplicationID)
        {
            int detainID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID ,IsReleased, ReleaseDate,
              ReleasedByUserID, ReleaseApplicationID)
            VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased, @ReleaseDate,
              @ReleasedByUserID, @ReleaseApplicationID) 
            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query,connection);

           
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (ReleaseDate == DateTime.MinValue)
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

            if (ReleasedByUserID == -1)
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

            if (ReleaseApplicationID == -1)
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                    
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsetedID))
                {
                    detainID = InsetedID;
                }
            }
            catch { }

            finally { connection.Close(); }

            return detainID;
        }

        public static bool UpdateDetain(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate,
           int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"Update DetainedLicenses Set LicenseID = @LicenseID, DetainDate = @DetainDate, FineFees = @FineFees,
                CreatedByUserID = @CreatedByUserID, IsReleased = @IsReleased, ReleaseDate = @ReleaseDate,
                ReleasedByUserID = @ReleasedByUserID, ReleaseApplicationID = @ReleaseApplicationID
                WHERE DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (ReleaseDate == DateTime.MinValue)
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

            if (ReleasedByUserID == -1)
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

            if (ReleaseApplicationID == -1)
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch { }
            finally { connection.Close(); }

            return rowsAffected > 0;
        }

        public static bool IsLicenseDetained(int LicneseID)
        {
            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string querey = "select 1 from DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(querey, connection);

            command.Parameters.AddWithValue("@LicenseID", LicneseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsDetained = true;
                }

                reader.Close();
            }
            catch {}

            finally { connection.Close(); }

            return IsDetained;
        }

        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"Update DetainedLicenses Set 
                IsReleased = 1,
                ReleasedByUserID = @ReleasedByUserID,
                ReleaseDate = @ReleaseDate,
                ReleaseApplicationID = @ReleaseApplicationID
                WHERE DetainID = @DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
           
            catch { }
            
            finally { connection.Close(); }

            return rowsAffected > 0;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "select * from DetainedLicenses_View";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch { }

            finally { connection.Close(); }

            return dt;
        }
    }
}
