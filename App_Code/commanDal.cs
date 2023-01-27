using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for commanDal
/// </summary>
public class commanDal
{
    public DataTable FillUserDetails(int UserID)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * from AMS_User_Master where userid=@userid
                                                     ", con))
                {
                    cmd.Parameters.AddWithValue("@userid", UserID);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
}