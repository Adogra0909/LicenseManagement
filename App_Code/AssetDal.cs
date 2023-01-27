using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for AssetDal
/// </summary>
public class AssetDal
{
    int res = 0;
    string ERROR;
    private string PopulateBody(string EmpName, string EmpID, string EmpDepartment)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{EmpName}", EmpName);
        body = body.Replace("{EmpID}", EmpID);
        body = body.Replace("{EmpDepartment}", EmpDepartment);
        return body;
    }

    public int NewAssetRequest(BEL oobel)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMSSP_AssetRequest", con))
                {
                    cmd.Parameters.AddWithValue("@EmpName", oobel.Empname);
                    cmd.Parameters.AddWithValue("@EmpID", oobel.EmpCode);
                    cmd.Parameters.AddWithValue("@EmpGrade", oobel.EmpGrade);
                    cmd.Parameters.AddWithValue("@EmpEmailID", oobel.EmpEmail);
                    cmd.Parameters.AddWithValue("@EmpContactNo", oobel.ContactNo);
                    cmd.Parameters.AddWithValue("@Quantity", oobel.Quantity);
                    cmd.Parameters.AddWithValue("@Site", oobel.Site);
                    cmd.Parameters.AddWithValue("@AssetTypeCode", oobel.AssetTypeCode);
                    cmd.Parameters.AddWithValue("@Category", oobel.Category);
                    cmd.Parameters.AddWithValue("@SubProductID", oobel.SubProductID);
                    cmd.Parameters.AddWithValue("@ManufacturerID", oobel.ManufacturerID);
                    cmd.Parameters.AddWithValue("@ModelCode", oobel.ModelCode);
                    cmd.Parameters.AddWithValue("@Location", oobel.Location);
                    cmd.Parameters.AddWithValue("@Department", oobel.Department);
                    cmd.Parameters.AddWithValue("@FApproverEmail", oobel.Approver1);
                    cmd.Parameters.AddWithValue("@SApproverEmail", oobel.Approver2);
                    cmd.Parameters.AddWithValue("@Reason", oobel.Reason);
                    cmd.Parameters.AddWithValue("@RequestType", oobel.RequestType);
                    cmd.Parameters.AddWithValue("@SerialNo", oobel.SerialNo);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    // cmd.Parameters.AddWithValue("@MailBody", oobel.Notes);
                    cmd.Parameters.Add("@RequestNumber", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@Option", "Request");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    BEL.Encry = cmd.Parameters["@RequestNumber"].Value.ToString();
                    //oobel.Notes = this.PopulateBody(oobel.Empname, oobel.Encry, oobel.Department);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }

    public DataTable FillAllRequests()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetRequest", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AllRequest");
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

    public DataTable FillAllRequestsCustom()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetRequest", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AllRequestCustom");
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

    public DataTable FillRequestDetails(string RequestNo)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from  AMS_Asset_RequestView  where RequestNumber=@RequestNumber ", con))
                {
                    cmd.Parameters.AddWithValue("@RequestNumber", RequestNo);
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



    public string AddLocation(BEL oobel)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Location", con))
                {

                    cmd.Parameters.AddWithValue("@LocCode", oobel.LocationCode);
                    cmd.Parameters.AddWithValue("@Location", oobel.Location);
                    cmd.Parameters.AddWithValue("@LocAddress", oobel.address);
                    cmd.Parameters.AddWithValue("@LocAddress2", oobel.address2);
                    cmd.Parameters.AddWithValue("@ITHeadEmail", oobel.ITHeadEmail);
                    cmd.Parameters.AddWithValue("@ITHeadEmail2", oobel.Approver2);
                    cmd.Parameters.AddWithValue("@StoreEmail", oobel.StoreEmail);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 100);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@Option", "Insert");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ERROR = (string)cmd.Parameters["@ERROR"].Value;
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ERROR;

    }

    public int UpdateLocation(BEL oobel)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Location", con))
                {
                    cmd.Parameters.AddWithValue("@LocCode", oobel.LocationCode);
                    cmd.Parameters.AddWithValue("@Location", oobel.Location);
                    cmd.Parameters.AddWithValue("@LocAddress", oobel.address);
                    cmd.Parameters.AddWithValue("@LocAddress2", oobel.address2);
                    cmd.Parameters.AddWithValue("@ITHeadEmail", oobel.ITHeadEmail);
                    cmd.Parameters.AddWithValue("@ITHeadEmail2", oobel.Approver2);
                    cmd.Parameters.AddWithValue("@StoreEmail", oobel.StoreEmail);
                    cmd.Parameters.AddWithValue("@UpdateBy", 1);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "UpdateLocation");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }

    public DataTable FillLocation()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from  AMS_Asset_Location_Master ", con))
                {
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
    public DataTable FillActiveLocation()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from  AMS_Asset_Location_Master where IsActive='True'", con))
                {
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

    public DataTable FillLocationStoreEmail(string LocationCode)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from  AMS_Asset_Location_Master  where LocCode=@LocCode", con))
                {
                    cmd.Parameters.AddWithValue("@LocCode", LocationCode);
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

    public DataTable FillLocationDepartments(string LocationCode)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                //DepCode,DepName
                using (SqlCommand cmd = new SqlCommand(" select  * from  AMS_Asset_Dep_Master  where LocCode=@LocCode  and IsActive='True'", con))
                {
                    cmd.Parameters.AddWithValue("@LocCode", LocationCode);
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

    public DataTable FillProducts()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from  AMS_Asset_Category ", con))
                {
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

    public DataTable FillActiveProducts()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Category", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "FillActiveCategory");
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

    public DataTable FillAllSuppliers()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Suppliers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AllSuppliers");
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable FillAllActiveSuppliers()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from AMS_Asset_Suppliers where IsActive='True'", con))
                {
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

    public DataTable FillAllSuppliersDetails(string SupplierID)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from AMS_Asset_Suppliers  where Supplierid=@Supplierid", con))
                {
                    cmd.Parameters.AddWithValue("@Supplierid", SupplierID);
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

    public DataTable FillDepartments()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Dep", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AllDepartment");
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
    public DataTable FillActiveDepartments()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Dep", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "ActiveDepartment");
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

    public Int32 UpdateDepartment(BEL oobel)
    {


        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Dep", con))
                {
                    // cmd.Parameters.AddWithValue("@LocCode", oobel.LocationCode);
                    cmd.Parameters.AddWithValue("@DepCode", oobel.DepartmentCode);
                    cmd.Parameters.AddWithValue("@DepName", oobel.Department);
                    cmd.Parameters.AddWithValue("@Approver1", oobel.Approver1);
                    cmd.Parameters.AddWithValue("@Approver2", oobel.Approver2);
                    cmd.Parameters.AddWithValue("@Approver3", oobel.Approver3);
                    cmd.Parameters.AddWithValue("@UpdateBy", oobel.UpdatedBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);

                    cmd.Parameters.AddWithValue("@Option", "UpdateDepartment");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;


    }
    public int AddDepartments(BEL oobel)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Dep", con))
                {
                    // cmd.Parameters.AddWithValue("@LocCode", oobel.LocationCode);
                    cmd.Parameters.AddWithValue("@DepCode", oobel.DepartmentCode);
                    cmd.Parameters.AddWithValue("@DepName", oobel.Department);
                    cmd.Parameters.AddWithValue("@Approver1", oobel.Approver1);
                    cmd.Parameters.AddWithValue("@Approver2", oobel.Approver2);
                    cmd.Parameters.AddWithValue("@Approver3", oobel.Approver3);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "Insert");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }

    public DataTable FillDepartmentsApprovers(string DepartmentCode)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select Approver1,Approver2,Approver3 from  AMS_Asset_Dep_Master  where DepCode=@DepCode ", con))
                {
                    cmd.Parameters.AddWithValue("@DepCode", DepartmentCode);
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

    public DataTable FillAllProducts()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from AMS_Asset_vInventory order by Asset_Tag_ID,CategoryID Asc", con))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;

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
    public DataTable FillAllProductsOut()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AssetOutAll");
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
    public DataTable FillAllProductsIn()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AssetInAll");
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
    public DataTable FillProductDetails(string SerialNo)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SerialNo", SerialNo);
                    cmd.Parameters.AddWithValue("@Option", "FillProductDetails");
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

    public DataTable FillProductDetailsLocationWise(string SerialNo, string LocationCode)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SerialNo", SerialNo);
                    cmd.Parameters.AddWithValue("@LocationCode", LocationCode);
                    cmd.Parameters.AddWithValue("@Option", "FillProductDetailsLocationWise");
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
    public DataTable FillProductByStatus()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT AMS_AssetInventory.*,AMS_Asset_Dep_Master.DepName,AMS_Asset_Category.Productname,AMS_Asset_Location_Master.Location,AMS_Asset_Location_Master.LocAddress
                                    FROM [ksubscribers].[dbo].[AMS_AssetInventory]
                                    left join AMS_Asset_Dep_Master on AMS_Asset_Dep_Master.DepCode=AMS_AssetInventory.DepartmentID
                                    left join AMS_Asset_Category on AMS_Asset_Category.ProductCode=AMS_AssetInventory.CategoryID
	                                left join AMS_Asset_Location_Master on AMS_Asset_Location_Master.LocCode=AMS_AssetInventory.LocationID where AssetStatus='InStock'
                                                     ", con))
                {
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
    public int AssetOut(BEL oobel)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetInventory", con))

                {
                    cmd.Parameters.AddWithValue("@SerialNo", oobel.SerialNo);
                    //  cmd.Parameters.AddWithValue("@Department", oobel.DepartmentCode);
                    cmd.Parameters.AddWithValue("@Check_out_Date", oobel.Check_out_Date);
                    cmd.Parameters.AddWithValue("@Assigned_to_person", oobel.Assigned_to_person);
                    cmd.Parameters.AddWithValue("@Assigned_to_person_Email", oobel.Assigned_to_person_Email);
                    cmd.Parameters.AddWithValue("@Assigned_to_person_EMPCode", oobel.EmpCode);
                    cmd.Parameters.AddWithValue("@Assigned_to_site", oobel.Assigned_to_site);
                    cmd.Parameters.AddWithValue("@Assigned_to_location", oobel.LocationCode);
                    cmd.Parameters.AddWithValue("@CategoryID", oobel.Category);
                    cmd.Parameters.AddWithValue("@Due_Date", oobel.Due_Date);
                    cmd.Parameters.AddWithValue("@Notes", oobel.Due_Date);
                    cmd.Parameters.AddWithValue("@Option", "AssetOut");

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }
    public int AssetIn(BEL oobel)
    {
        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMSsp_AssetInventory", con))

                {
                    cmd.Parameters.AddWithValue("@SerialNo", oobel.SerialNo);
                    cmd.Parameters.AddWithValue("@AssetStatus", oobel.AssetStatus);
                    cmd.Parameters.AddWithValue("@LocationCode", oobel.LocationCode);
                    cmd.Parameters.AddWithValue("@Remarks", oobel.Notes);
                    cmd.Parameters.AddWithValue("@Option", "AssetIn");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }
    public int AddCompany(BEL oobel)

    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Companies", con))
                {
                    cmd.Parameters.AddWithValue("@Company_name", oobel.CompanyName);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "InsertCompany");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }
    public int AddManufacturer(BEL oobel)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Manufacturer", con))
                {
                    cmd.Parameters.AddWithValue("@ManufacturerID", oobel.ManufacturerID);
                    cmd.Parameters.AddWithValue("@Manufacturer_name", oobel.ManufacturerName);
                    cmd.Parameters.AddWithValue("@url", oobel.url);
                    cmd.Parameters.AddWithValue("@support_url", oobel.support_url);
                    cmd.Parameters.AddWithValue("@support_phone", oobel.support_phone);
                    cmd.Parameters.AddWithValue("@support_email", oobel.support_email);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    cmd.Parameters.AddWithValue("@IsActive", "1");
                    cmd.Parameters.AddWithValue("@Option", "InsertManufacturer");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }
    public int UpdateManufacturer(BEL oobel)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Manufacturer", con))
                {
                    cmd.Parameters.AddWithValue("@ManufacturerID", oobel.ManufacturerID);
                    cmd.Parameters.AddWithValue("@Manufacturer_name", oobel.ManufacturerName);
                    cmd.Parameters.AddWithValue("@url", oobel.url);
                    cmd.Parameters.AddWithValue("@support_url", oobel.support_url);
                    cmd.Parameters.AddWithValue("@support_phone", oobel.support_phone);
                    cmd.Parameters.AddWithValue("@support_email", oobel.support_email);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "UpdateManufacturer");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }
    public int AddSuppliers(BEL oobel)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Suppliers", con))
                {
                    cmd.Parameters.AddWithValue("@Supplierid", oobel.Supplierid);
                    cmd.Parameters.AddWithValue("@Name", oobel.Name);
                    cmd.Parameters.AddWithValue("@address", oobel.address);
                    cmd.Parameters.AddWithValue("@City", oobel.City);
                    cmd.Parameters.AddWithValue("@State", oobel.State);
                    cmd.Parameters.AddWithValue("@Country", oobel.Country);
                    cmd.Parameters.AddWithValue("@Phone", oobel.Phone);
                    cmd.Parameters.AddWithValue("@Fax", oobel.Fax);
                    cmd.Parameters.AddWithValue("@Email", oobel.Email);
                    cmd.Parameters.AddWithValue("@Contact", oobel.Contact);
                    cmd.Parameters.AddWithValue("@Notes", oobel.Notes);
                    cmd.Parameters.AddWithValue("@Zip", oobel.Zip);
                    cmd.Parameters.AddWithValue("@Url", oobel.url);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "AddSupplier");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }
    public int UpdateSupplier(BEL oobel)
    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Suppliers", con))
                {

                    cmd.Parameters.AddWithValue("@Supplierid", oobel.Supplierid);
                    cmd.Parameters.AddWithValue("@Name", oobel.Name);
                    cmd.Parameters.AddWithValue("@address", oobel.address);
                    cmd.Parameters.AddWithValue("@City", oobel.City);
                    cmd.Parameters.AddWithValue("@State", oobel.State);
                    cmd.Parameters.AddWithValue("@Country", oobel.Country);
                    cmd.Parameters.AddWithValue("@Phone", oobel.Phone);
                    cmd.Parameters.AddWithValue("@Fax", oobel.Fax);
                    cmd.Parameters.AddWithValue("@Email", oobel.Email);
                    cmd.Parameters.AddWithValue("@Contact", oobel.Contact);
                    cmd.Parameters.AddWithValue("@Notes", oobel.Notes);
                    cmd.Parameters.AddWithValue("@Zip", oobel.Zip);
                    cmd.Parameters.AddWithValue("@Url", oobel.url);
                    cmd.Parameters.AddWithValue("@UpdateBy", oobel.UpdatedBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "UpdateSupplier");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }
    public int AddStatus(BEL oobel)

    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Status_Proc", con))
                {
                    cmd.Parameters.AddWithValue("@StatusName", oobel.StatusName);
                    cmd.Parameters.AddWithValue("@InsertBy", oobel.InsertBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "AddStatus");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }
    public int UpdateStatus(BEL oobel)

    {

        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_Asset_Status_Proc", con))
                {
                    cmd.Parameters.AddWithValue("@StatusID", oobel.ID);
                    cmd.Parameters.AddWithValue("@StatusName", oobel.StatusName);
                    cmd.Parameters.AddWithValue("@UpdateBy", oobel.UpdatedBy);
                    cmd.Parameters.AddWithValue("@IsActive", oobel.IsActive);
                    cmd.Parameters.AddWithValue("@Option", "UpdateStatus");
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }
    public DataTable FillAllStatus()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from  AMS_Asset_Status ", con))
                {
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
    public DataTable FillAllManufacturer()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_Asset_Manufacturer", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "AllManufacturers");
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public DataTable FillAllActiveManufacturer()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from  AMS_Asset_Manufacturer where IsActive='True' ", con))
                {
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
    public DataTable FillAllManufacturerDetails(string ManufacturerDetails)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(" select * from  AMS_Asset_Manufacturer  where ManufacturerID=@ManufacturerID", con))
                {
                    cmd.Parameters.AddWithValue("@ManufacturerID", ManufacturerDetails);
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
    public DataTable GetAllMachines()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select machName,agentGuid from vMachine order by machName asc  ", con))
                {
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
    public DataTable GetSingleMachineDetails(string machineGuid)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from vAuditMachineSummary where agentGuid=@agentGuid ", con))
                {
                    cmd.Parameters.AddWithValue("@agentGuid", machineGuid);
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
    public DataTable FillLocationDetails(string LocationCode)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from  AMS_Asset_Location_Master  where LocCode=@Code ", con))
                {
                    cmd.Parameters.AddWithValue("@Code", LocationCode);
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



