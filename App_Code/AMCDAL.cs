using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public class AMCDAL
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
    public Int32 AMCInsert(BEL objBEL)
    {
        int result;
        try
        {

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into pcv_redAMCEX (Device,Description,Quantity,RenewalType,CurrentVendor,RenewalTenure,NextRenewal,Price) 
                                              values (@Device,@Description,@Quantity,@RenewalType,@CurrentVendor,@RenewalTenure,@NextRenewal,@Price)", con))
                {
                    cmd.Parameters.AddWithValue("@Device", objBEL.Device);
                    cmd.Parameters.AddWithValue("@Description", objBEL.Description);
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(objBEL.Quantity));
                    cmd.Parameters.AddWithValue("@RenewalType", objBEL.RenewalType);
                    cmd.Parameters.AddWithValue("@CurrentVendor", objBEL.CurrentVendor);
                    cmd.Parameters.AddWithValue("@RenewalTenure", objBEL.RenewalTenure);
                    cmd.Parameters.AddWithValue("@NextRenewal", Convert.ToDateTime(objBEL.NextRenewal).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDouble(objBEL.Price).ToString());
                    result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public Int32 DeleteAMCDetails(BEL objBEL)
    {
        int result;
        try
        {

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand(@"delete from pcv_redAMCEX where SrNo=@SrNo", con))
                {
                    cmd.Parameters.AddWithValue("@SrNo", objBEL.srno);
                    result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet SearchAMCDetails(BEL objBEL)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlCommand cmd = new SqlCommand("Select * from pcv_redAMCEX where Device=@Device", con))
            {
                cmd.Parameters.AddWithValue("@Device", objBEL.Device);
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.Fill(ds);

                    cmd.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }
    public DataSet GetAMCRecords()
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlCommand cmd = new SqlCommand("Select * from pcv_redAMCEX", con))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.Fill(ds);
                    cmd.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }
    public Int32 UpdateAMC(BEL objBEL)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(@"Update pcv_redAMCEX  SET Device=@Device,Description=@Description,
                    Quantity=@Quantity,RenewalType=@RenewalType,CurrentVendor=@CurrentVendor,RenewalTenure=@RenewalTenure,NextRenewal=@NextRenewal,Price=@Price 
                        where SrNo=@SrNo", con))
                {
                    cmd.Parameters.AddWithValue("@SrNo", objBEL.srno);
                    cmd.Parameters.AddWithValue("@Device", objBEL.Device);
                    cmd.Parameters.AddWithValue("@Description", objBEL.Description);
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(objBEL.Quantity));
                    cmd.Parameters.AddWithValue("@RenewalType", objBEL.RenewalType);
                    cmd.Parameters.AddWithValue("@CurrentVendor", objBEL.CurrentVendor);
                    cmd.Parameters.AddWithValue("@RenewalTenure", objBEL.RenewalTenure);
                    cmd.Parameters.AddWithValue("@NextRenewal", Convert.ToDateTime(objBEL.NextRenewal).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDouble(objBEL.Price).ToString());
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
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
