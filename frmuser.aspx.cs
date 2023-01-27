using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmuser : System.Web.UI.Page
{

    errorMessage msg = new errorMessage();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            //Change your condition here
            if (Session["Result"] != null)
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
            FillRoles();
        }
    }

    private void FillUsers()
    {
        try
        {

            FillLocation();
            FillDepartments();
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT * FROM AMS_vUsers;";

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

    private void FillLocation()
    {
        try
        {

            DataTable table = new BLL().Location();
            if (table.Rows.Count > 0)
            {
                ddlLocation.DataSource = table;
                ddlLocation.DataValueField = "LocCode";
                ddlLocation.DataTextField = "Location";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-----Select Location-----", "0"));
            }
            else
            {
                //gvEngineerWiseCalls.EmptyDataText = "No Records Found";
                ddlLocation.DataSource = null;
                ddlLocation.DataBind();
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

    private void FillDepartments()
    {
        try
        {

            DataTable table = new BLL().FillDepartments();
            if (table.Rows.Count > 0)
            {
                ddldepartment.DataSource = table;
                ddldepartment.DataValueField = "DepCode";
                ddldepartment.DataTextField = "DepName";
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, new ListItem("-----Select Department-----", "0"));
            }
            else
            {
                //gvEngineerWiseCalls.EmptyDataText = "No Records Found";
                ddldepartment.DataSource = null;
                ddldepartment.DataBind();
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

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{

        //    DataTable table = new BLL().FillLocationDepartments(ddlLocation.SelectedValue);
        //    if (table.Rows.Count > 0)
        //    {
        //        ddldepartment.Visible = true;
        //        ddldepartment.DataSource = table;
        //        ddldepartment.DataValueField = "DepCode";
        //        ddldepartment.DataTextField = "DepName";
        //        ddldepartment.DataBind();
        //        ddldepartment.Items.Insert(0, new ListItem("----------Select Department----------", "0"));
        //        // txtGrade.Text = table.Rows[0]["StoreEmail"].ToString();
        //    }
        //    else
        //    {
        //        ddldepartment.DataSource = table;
        //        ddldepartment.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg.ReportError1(ex.Message);
        //    lblerrorMsg.Text = msg.ms;
        //}
        //finally
        //{
        //    //objBLL = null;
        //}
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "#PCVISOR@DEVELOPER#";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_UsersPRC", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmailID", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Frm_UID", txtLoginName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pass", Encrypt(txtPass.Text));
                    cmd.Parameters.AddWithValue("@UserType", ddlUserRoles.SelectedValue);
                    cmd.Parameters.AddWithValue("@LocCode", ddlLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@DepCode", ddldepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Manager_UserID ", txtManager.Text.Trim()); ;
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@UserRemarks", txtUserRemarks.Text.Trim());
                    cmd.Parameters.AddWithValue("@InsertBy", 1);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@DomainType", ddlDomain.SelectedValue);
                    cmd.Parameters.AddWithValue("@Option", "InsertUser");
                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/frmuser.aspx");
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

    private void FillAllDepartments()
    {
        try
        {

            DataTable table = new BLL().FillDepartments();
            if (table.Rows.Count > 0)
            {
                ddldepartment.DataSource = table;
                ddldepartment.DataTextField = "DepName";
                ddldepartment.DataValueField = "DepCode";
                ddldepartment.DataBind();
            }
            else
            {
                //gvEngineerWiseCalls.EmptyDataText = "No Records Found";
                ddldepartment.DataSource = null;
                ddldepartment.DataBind();
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

    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "SelectU")
        //{ 
        //    //Determine the RowIndex of the Row whose Button was clicked.
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);

        //    //Reference the GridView Row.
        //  //  GridViewRow row = gvUsers.Rows[rowIndex];
        //    int id = Convert.ToInt32(gvUsers.DataKeys[rowIndex].Values[0]);
        //    //Fetch value of Country
        //    string country = gvUsers.Rows[rowIndex].Cells[1].Text;
        //}
        if (e.CommandName == "SelectUser")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            //  GridViewRow row = gvUsers.Rows[rowIndex];
            // int id = Convert.ToInt32(gvUsers.DataKeys[rowIndex].Values[0]);
            //Fetch value of Country
            // string country = gvUsers.Rows[rowIndex].Cells[1].Text;
            try
            {

                DataTable table = new BLL().FillUserDetails(Convert.ToInt32(gvUsers.DataKeys[rowIndex].Values[0]));
                if (table.Rows.Count > 0)
                {
                    FillAllDepartments();
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;

                    txtUsername.Text = table.Rows[0]["UserName"].ToString();
                    txtEmpID.Text = table.Rows[0]["EmpID"].ToString();
                    txtEmail.Text = table.Rows[0]["EmailID"].ToString();
                    txtLoginName.Text = table.Rows[0]["Frm_UID"].ToString();
                    txtLoginName.ReadOnly = true;
                    // txtPass.Text = table.Rows[0]["Pass"].ToString();
                    ddlUserRoles.SelectedValue = table.Rows[0]["UserType"].ToString();
                    if (ddlLocation.Items.FindByValue(table.Rows[0]["LocCode"].ToString().Trim()) != null)
                    {

                        ddlLocation.SelectedValue = table.Rows[0]["LocCode"].ToString().Trim();

                    }
                    if (ddldepartment.Items.FindByValue(table.Rows[0]["DepCode"].ToString().Trim()) != null)
                    {

                        ddldepartment.SelectedValue = table.Rows[0]["DepCode"].ToString().Trim();

                    }
                    if (ddlDomain.Items.FindByValue(table.Rows[0]["DomainType"].ToString().Trim()) != null)
                    {

                        ddlDomain.SelectedValue = table.Rows[0]["DomainType"].ToString().Trim();

                    }

                    txtManager.Text = table.Rows[0]["Manager_UserID"].ToString();
                    txtContactNo.Text = table.Rows[0]["ContactNo"].ToString();
                    ddlStatus.SelectedValue = table.Rows[0]["Status"].ToString();

                }

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

        if (txtPass.Text != "")
        {
            UpdateUser();
        }
        else
        {
            UpdateUserInfo();

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
                    cmd.Parameters.AddWithValue("@UserType", ddlUserRoles.SelectedValue);
                    cmd.Parameters.AddWithValue("@Frm_UID", txtLoginName.Text);
                    cmd.Parameters.AddWithValue("@Pass", Encrypt(txtPass.Text));
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.Text);
                    cmd.Parameters.AddWithValue("@Option", "UpdateUser");
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

    private void UpdateUserInfo()
    {
        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_UsersPRC", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmailID", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Frm_UID", txtLoginName.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Pass", Encrypt(txtPass.Text));
                    cmd.Parameters.AddWithValue("@UserType", ddlUserRoles.SelectedValue);
                    cmd.Parameters.AddWithValue("@LocCode", ddlLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@DepCode", ddldepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Manager_UserID ", txtManager.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserRemarks", txtUserRemarks.Text.Trim());
                    cmd.Parameters.AddWithValue("@InsertBy", 1);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@DomainType", ddlDomain.SelectedValue);
                    cmd.Parameters.AddWithValue("@Option", "UpdateUserInfo");
                    cmd.ExecuteNonQuery();
                    Session["Result"] = txtLoginName.Text.Trim() + " updated successfully";
                    Response.Redirect("~/frmuser.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(Regex.Replace(ex.Message, @"[!@#$%^&*;,+|{}~?\t\r\n:' ]", " "), MessageType.Error);
            //  ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            //objBLL = null;
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string connection = ConfigurationManager.ConnectionStrings["ADConnection"].ToString();
            DirectorySearcher dssearch = new DirectorySearcher(connection);
            dssearch.Filter = "(sAMAccountName=" + txtLoginName.Text + ")";
            SearchResult sresult = dssearch.FindOne();
            if (sresult != null)
            {
                DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                if (sresult.Properties["displayName"] != null && sresult.Properties["displayName"].Count > 0)
                {
                    txtUsername.Text = dsresult.Properties["displayName"][0].ToString();
                }

                if (sresult.Properties["mail"] != null && sresult.Properties["mail"].Count > 0)
                {
                    txtEmail.Text = dsresult.Properties["mail"][0].ToString();
                }
                if (sresult.Properties["telephoneNumber"] != null && sresult.Properties["telephoneNumber"].Count > 0)
                {
                    txtContactNo.Text = dsresult.Properties["telephoneNumber"][0].ToString();
                }
                //if (sresult.Properties["title"] != null && sresult.Properties["title"].Count > 0)
                //{
                //    txtGrade.Text = dsresult.Properties["title"][0].ToString();
                //}
                if (ddldepartment.Items.FindByValue(dsresult.Properties["department"][0].ToString().Trim()) != null && sresult.Properties["department"].Count > 0)
                {
                    ddldepartment.SelectedValue = dsresult.Properties["department"][0].ToString().Trim();
                }
                if (ddlLocation.Items.FindByValue(dsresult.Properties["physicalDeliveryOfficeName"][0].ToString().Trim()) != null && sresult.Properties["physicalDeliveryOfficeName"].Count > 0)
                {
                    ddlLocation.SelectedValue = dsresult.Properties["physicalDeliveryOfficeName"][0].ToString().Trim();
                }
            }

            else
            {
                lblerrorMsg.Text = "Error: Not found";
            }
        }

        catch (Exception ex)
        {
            ShowMessage(Regex.Replace(ex.Message, @"[!@#$%^&*;,+|{}~?\t\r\n:' ]", " "), MessageType.Error);
            // ExceptionLogging.SendErrorToText(ex);

        }
    }

    protected void ImgbtnSearchUser_Click(object sender, ImageClickEventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM AMS_vUsers where Frm_UID=@Frm_UID", con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@Frm_UID", txtSearch.Text);
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

    private void FillRoles()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select  distinct RoleName  from AMS_Role order by RoleName asc"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                ddlUserRoles.DataSource = cmd.ExecuteReader();
                ddlUserRoles.DataTextField = "RoleName";
                ddlUserRoles.DataValueField = "RoleName";
                ddlUserRoles.DataBind();
                con.Close();
            }
        }
        ddlUserRoles.Items.Insert(0, new ListItem("-----Select Role-----", "0"));
    }

    public enum MessageType { Success, Error, Info, Warning };
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

    }
}