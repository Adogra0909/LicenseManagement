using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmrenewal : System.Web.UI.Page
{

    errorMessage msg = new errorMessage();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            //Change your condition here
            if ( Session["Result"] != null)
            {
                ShowMessage(Session["Result"].ToString(), MessageType.Success);
                Session.Remove("Result");
            }
            
        }
        catch (Exception ex)
        {
         //   ExceptionLogging.SendErrorToText(ex);
           
        }
    }

  

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillUsers();
         
            //FillCustomers();
        }
    }
    //private void FillCustomers()
    //{
    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
    //        {
    //            using (SqlDataAdapter sda = new SqlDataAdapter("Select distinct OrgName,Orgid from AMS_Organization_Master order by OrgName asc", con))
    //            {
    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        ddlOrg.DataSource = dt;
    //                        ddlOrg.DataTextField = "OrgName";
    //                        ddlOrg.DataValueField = "Orgid";
    //                        ddlOrg.DataBind();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}










    protected void btnSave_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new SqlCommand("AMS_UsersPRC", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text.Trim());
        //            cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
        //            cmd.Parameters.AddWithValue("@EmailID", txtEmail.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Frm_UID", txtLoginName.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Pass", Encrypt(txtPass.Text));
        //            cmd.Parameters.AddWithValue("@UserType", ddlUserRoles.SelectedValue);
        //            cmd.Parameters.AddWithValue("@LocCode", ddlLocation.SelectedValue);
        //            cmd.Parameters.AddWithValue("@DepCode", ddldepartment.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Manager_UserID ", txtManager.Text.Trim()); ;
        //            cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
        //            cmd.Parameters.AddWithValue("@UserRemarks", txtUserRemarks.Text.Trim());
        //            cmd.Parameters.AddWithValue("@InsertBy", 1);
        //            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
        //            cmd.Parameters.AddWithValue("@DomainType", ddlDomain.SelectedValue);
        //            cmd.Parameters.AddWithValue("@Option", "InsertUser");
        //            cmd.ExecuteNonQuery();
        //            Response.Redirect("~/frmuser.aspx");
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg.ReportError1(ex.Message);
        //    lblerrorMsg.Text = msg.ms;
        //}
        //finally
        //{
        //    objBLL = null;
        //}
    }

    public DataTable FillUserDetails(int UserID)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"   Select ID,Customer_Name,ContactPerson_Name,Number,CustomerAddress,Location,Cloud_OnPremesis,License_Type,Product,Qty 
			,Our_Buying_Prices_for_sales ,Unit_Prices ,Total_Price ,Start,Expiry,Remarks,AccountMangerEmail
			 ,PO_No,PO_Type,Payment_Term ,Account_Manager,Region,IsActive from pcv_CustomersDetails where  ID=@userid
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
    static string ID;
    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "SelectUser")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
           // int rowIndex = Convert.ToInt32(e.CommandArgument);
    
            ImageButton img = (ImageButton)e.CommandSource as ImageButton;
            GridViewRow row = img.NamingContainer as GridViewRow;
          //  ID = gvUsers.DataKeys[row.RowIndex].Value.ToString();
            try
            {

               
                txtCustomerName.Text = row.Cells[1].Text.Replace("&nbsp;", "");
                txtnumber.Text = row.Cells[2].Text.Trim();
                txtContctPrnsName.Text = row.Cells[3].Text.Trim();
                txtlocation.Text = row.Cells[4].Text.Trim();
                txtCustomerAddress.Text = row.Cells[5].Text.Trim();
                ddlCldtype.SelectedValue = row.Cells[6].Text.Trim();
                ddlLicenseType.SelectedValue = row.Cells[7].Text.Trim();
                ddlproduct.SelectedValue = row.Cells[8].Text.Trim();
                //txtTechnician.Text = row.Cells[9].Text.Trim();
                txtqnty.Text = row.Cells[9].Text.Trim();
               // txtoemprice.Text = row.Cells[11].Text.Trim();
                txtsaleprice.Text = row.Cells[10].Text.Trim();

                txtunitprice.Text = row.Cells[11].Text.Trim();
                txttotalPrice.Text = row.Cells[12].Text.Trim();

                txtStart.Text = row.Cells[13].Text.Trim();
                txtExpiry.Text = row.Cells[14].Text.Trim();
                txtacManagerEmail.Text = row.Cells[15].Text.Trim();
                txtpono.Text = row.Cells[16].Text.Trim();
                ddlPOType.SelectedValue = row.Cells[17].Text.Trim();

                txtpaymentterm.Text = row.Cells[18].Text.Trim();
                txtAccManager.Text = row.Cells[19].Text.Trim();
                ddlregion.SelectedValue = row.Cells[20].Text.Trim();
                ddlactive.SelectedValue = row.Cells[21].Text.Trim();
                ID =row.Cells[22].Text.ToString();
                //if (table.Rows.Count > 0)
                //{





            }
            catch (Exception ex)
            {
                //msg.ReportError1(ex.Message.Replace("'", "\'"));
                //lblerrorMsg.Text = msg.ms;

                //string[] replaceables = { "+", "&&", "||", "!", "(", ")", "{", "}", "[", "]", "^", "~", "*", "?", ":", "\\", "'",  "\n", "\r", "\"" };
                //string rxString = string.Join("|", replaceables.Select(s => Regex.Escape(s)));
                //string output = Regex.Replace(ex.Message, rxString, string.Empty);

                ShowMessage(Regex.Replace(ex.Message, @"[!@#$%^&*;,+|{}~?\t\r\n:' ]", " "), MessageType.Error);
             //   ExceptionLogging.SendErrorToText(ex);
            }
            finally
            {
                //objBLL = null;
            }

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {


        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("pcv_SpCustomersDetails", con))
            {


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
            
                cmd.Parameters.AddWithValue("@Customer_Name", txtCustomerName.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPerson_Name", txtContctPrnsName.Text.Trim());
                cmd.Parameters.AddWithValue("@Number", txtnumber.Text.Trim());
                cmd.Parameters.AddWithValue("@CustomerAddress", txtCustomerAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Location", txtlocation.Text.Trim());
                cmd.Parameters.AddWithValue("@Cloud_OnPremesis", ddlCldtype.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@License_Type", ddlLicenseType.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Product", ddlproduct.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(txtqnty.Text.Trim()));
                cmd.Parameters.AddWithValue("@Technician", Convert.ToInt32(txtTechnician.Text.Trim()));
              cmd.Parameters.AddWithValue("@Oem_Price", txtoemprice.Text.Trim());
                cmd.Parameters.AddWithValue("@Our_Buying_Prices_for_sales", txtsaleprice.Text.Trim());
                cmd.Parameters.AddWithValue("@Unit_Prices", txtunitprice.Text.Trim());
                cmd.Parameters.AddWithValue("@Total_Price", txttotalPrice.Text.Trim());

                cmd.Parameters.AddWithValue("@Start", txtStart.Text.Trim());
                cmd.Parameters.AddWithValue("@Expiry", txtExpiry.Text.Trim());
              
                cmd.Parameters.AddWithValue("@Account_Manager", txtAccManager.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountMangerEmail", txtacManagerEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", ddlactive.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@PO_No", txtpono.Text.Trim());
                cmd.Parameters.AddWithValue("@PO_Type", ddlPOType.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Payment_Term", txtpaymentterm.Text.Trim());

        
             
                cmd.Parameters.AddWithValue("@Option", "UpdateRenewal");

                con.Open();
             cmd.ExecuteNonQuery();
                con.Close();
                string msg = "Details has been Updated!!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + msg + "');window.location.href='" + Request.RawUrl + "';", true);
            }
        }




    }

    public static int SOFTID;
    private void UpdateUser()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "AMS_usersprc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", SOFTID);
                    //cmd.Parameters.AddWithValue("@UserType", ddlUserRoles.SelectedValue);
                    //cmd.Parameters.AddWithValue("@Frm_UID", txtLoginName.Text);
                    //cmd.Parameters.AddWithValue("@Pass", Encrypt(txtPass.Text));
                    //cmd.Parameters.AddWithValue("@Status", ddlStatus.Text);
                    //cmd.Parameters.AddWithValue("@Option", "UpdateUser");
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        cmd.ExecuteNonQuery();
                        string message = string.Format("User details updated sucessfully");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);

                        con.Close();
                        Response.Redirect("~/frmuser.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(Regex.Replace(ex.Message, @"[!@#$%^&*;,+|{}~?\t\r\n:' ]", " "), MessageType.Error);
           // ExceptionLogging.SendErrorToText(ex);
        }
    }

 

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void ImgbtnSearchUser_Click(object sender, ImageClickEventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
      
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(@"    Select Customer_Id,Customer_Name,b.ContactNo,b.OrgAddress as 'Address',b.OrgLocation,Cloud_OnPremesis,License_Type,Product,Qty 
			,a.Annual_Buy_Price ,Unit_Prices ,a.Contract_Amount ,Start,Expiry,Remarks,AccountMangerEmail
			 ,PO_No,a.Contract_Type,Payment_Term ,Account_Manager,a.Region,IsActive from pcv_CustomersDetails a
			 inner join AMS_Organization_Master  b
			 on a.Customer_Id=b.OrgId
              where DATEDIFF(day, cast( Expiry as date), GETDATE()) > 1 and Customer_Name like '%'+@Frm_UID+'%'", con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@Frm_UID",txtSearch.Text.Trim());
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblTotal.Text = ds.Tables[0].Rows.Count.ToString();
                            gvUsers.DataSource = ds.Tables[0];
                            gvUsers.DataBind();
                         
                        }
                        else
                        {
                            //Empty DataTable to execute the “else-condition”  

                            gvUsers.EmptyDataText = "No Records Found";
                            gvUsers.DataSource = ds;
                            gvUsers.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void gvUsers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        FillUsers();
    }
    private void FillUsers()
    {
        try
        {

            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = @"  Select Customer_Id,Customer_Name,b.ContactNo,b.OrgAddress as 'Address',b.OrgLocation,Cloud_OnPremesis,License_Type,Product,Qty 
			,a.Annual_Buy_Price ,Unit_Prices ,a.Contract_Amount ,Start,Expiry,Remarks,AccountMangerEmail
			 ,PO_No,a.Contract_Type,Payment_Term ,Account_Manager,a.Region,IsActive from pcv_CustomersDetails a
			 inner join AMS_Organization_Master  b
			 on a.Customer_Id=b.OrgId
              where DATEDIFF(day, cast( Expiry as date), GETDATE()) > 1 ";

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblTotal.Text = ds.Tables[0].Rows.Count.ToString();
                                gvUsers.DataSource = ds.Tables[0];
                                gvUsers.DataBind();
                                //gvUsers.DataSource = ds.Tables[1];
                                //gvUsers.DataBind();
                            }
                            else
                            {
                                //Empty DataTable to execute the “else-condition”  

                                gvUsers.EmptyDataText = "No Records Found";
                                gvUsers.DataSource = ds;
                                gvUsers.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            lblerrorMsg.Text = msg.ms;
        }
        finally
        {
            //objBLL = null;
        }
    }


    public enum MessageType { Success, Error, Info, Warning };
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

    }
}