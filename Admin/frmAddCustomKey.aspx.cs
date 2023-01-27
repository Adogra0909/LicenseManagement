using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddCustomKey : System.Web.UI.Page
{

    errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            FillLicenseKey();
        }
        FillServerLoc();
        btnTypeAdd.Visible = true;
        btnUpdate.Visible = false;
    }



    private void FillLicenseKey()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("Select distinct License from AMS_License_Master order by License asc", con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddlkey.DataSource = dt;
                            ddlkey.DataTextField = "License";
                            ddlkey.DataValueField = "License";
                            ddlkey.DataBind();
                        }
                    }
                }
            }
            ddlkey.Items.Insert(0, new ListItem("-----Select Key-----", "NA"));
        }
        catch (Exception ex)
        {

        }
    }
    private void FillServerLoc()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_spCustomerKeyCount", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "SelectAll");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);

                            gvstate.DataSource = dt;
                            gvstate.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            lblsuccess.Text = msg.ms; ;
        }
    }

    protected void btnTypeAdd_Click(object sender, EventArgs e)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_spCustomerKeyCount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MasterLicenseKey", ddlkey.SelectedValue);
                    cmd.Parameters.AddWithValue("@ChildLicKey ", txtchildlicKey.Text);
                    cmd.Parameters.AddWithValue("@ProductType", ddlproducttype.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assigned ", txtAssigned.Text);



                    cmd.Parameters.AddWithValue("@InsertBy", Convert.ToInt32(Session["UserID"]));

                    cmd.Parameters.AddWithValue("@Option", "Insert");
                    con.Open();
                    CustomerAssignedLic();
                    CustomerUpdateRemainLic();
                    cmd.ExecuteNonQuery();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }

        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            lblsuccess.Text = msg.ms; ;
        }
    }

    protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=State Details.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gvstate.AllowPaging = false;
            FillServerLoc();

            gvstate.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvstate.HeaderRow.Cells)
            {
                cell.BackColor = gvstate.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvstate.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvstate.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvstate.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvstate.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void gvstate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SelectState")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //Get the value of column from the DataKeys using the RowIndex.
                // txtbranchcode.Text = gvstate.DataKeys[rowIndex].Values["BranchCode"].ToString();
                // txtbranchname.Text = gvstate.Rows[rowIndex].Cells[1].Text.Trim();
                btnTypeAdd.Visible = false;
                btnUpdate.Visible = true;

            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        CustomerAssignedLic();
        CustomerUpdateRemainLic();
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_spBrach", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@BranchName", ddlOrg.SelectedValue);
                    //cmd.Parameters.AddWithValue("@BranchCode", txtbranchcode.Text.Replace(" ", string.Empty));
                    //cmd.Parameters.AddWithValue("@BranchName", txtbranchname.Text);
                    //cmd.Parameters.AddWithValue("@BranchRegion", ddlRegion.Text);
                    //cmd.Parameters.AddWithValue("@BranchAdd", txtbranchaddress.Text);
                    cmd.Parameters.AddWithValue("@UpdateBy", Convert.ToInt32(Session["UserID"]));
                    // cmd.Parameters.AddWithValue("@IsActive", ddlStatus.SelectedValue);



                    cmd.Parameters.AddWithValue("@Option", "Update");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            lblsuccess.Text = msg.ms; ;
        }
    }

    protected void CustomerAssignedLic()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_spCustomerKeyCount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LicKeyCode", ddlkey.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assigned", txtAssigned.Text);



                    cmd.Parameters.AddWithValue("@Option", "AddAssigned");
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            lblsuccess.Text = msg.ms; ;
        }
    }
    protected void CustomerUpdateRemainLic()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_spCustomerKeyCount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LicKeyCode", ddlkey.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assigned", txtAssigned.Text);



                    cmd.Parameters.AddWithValue("@Option", "UpdateRemain");
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            lblsuccess.Text = msg.ms; ;
        }
    }

    protected void ddlkey_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select  * from AMS_License_Master  where  License='" + ddlkey.SelectedValue + "'", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LicKeyCode", ddlkey.SelectedValue.ToString());
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();




                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}