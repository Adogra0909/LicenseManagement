using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddPaymentTermCode : System.Web.UI.Page
{

    errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        FillPaymentTerm();
        btnTypeAdd.Visible = true;
        btnUpdate.Visible = false;
    }



    private void FillPaymentTerm()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_spPaymentTerm", con))
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

                using (SqlCommand cmd = new SqlCommand("AMS_spPaymentTerm", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaymentTermCode", txtpaymentcode.Text.Replace(" ", string.Empty));
                    cmd.Parameters.AddWithValue("@PaymentTermName", txtpaymentName.Text);

                    cmd.Parameters.AddWithValue("@InsertBy", Convert.ToInt32(Session["UserID"]));
                    cmd.Parameters.AddWithValue("@IsActive", '1');
                    cmd.Parameters.AddWithValue("@Option", "Insert");
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
            FillPaymentTerm();

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
            if (e.CommandName == "DeleteEx")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //Get the value of column from the DataKeys using the RowIndex.
                string PaymentTermCode = gvstate.DataKeys[rowIndex].Values["PaymentTermCode"].ToString();
                // txtsoftwarename.Text = gvstate.Rows[rowIndex].Cells[1].Text;
                try
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("AMS_spPaymentTerm", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PaymentTermCode", PaymentTermCode);
                            cmd.Parameters.AddWithValue("@Option", "Delete");
                            cmd.CommandTimeout = 180;
                            cmd.ExecuteNonQuery();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#myModal').modal('show');");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            lblsuccess.ForeColor = System.Drawing.Color.Green;
                            lblsuccess.Text = PaymentTermCode + " Deleted successfully";
                            //   txtsoftwarename.Text = "";
                            con.Close();
                            FillPaymentTerm();

                        }
                    }
                    //
                }
                catch (Exception ex)
                {
                    msg.ReportError(ex.Message);

                }

            }
            if (e.CommandName == "SelectState")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //Get the value of column from the DataKeys using the RowIndex.
                txtpaymentcode.Text = gvstate.DataKeys[rowIndex].Values["PaymentTermCode"].ToString();
                txtpaymentName.Text = gvstate.Rows[rowIndex].Cells[2].Text.Trim();
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

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_spPaymentTerm", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaymentTermCode", txtpaymentcode.Text);
                    cmd.Parameters.AddWithValue("@PaymentTermName", txtpaymentName.Text);

                    cmd.Parameters.AddWithValue("@UpdateBy", Convert.ToInt32(Session["UserID"]));
                    cmd.Parameters.AddWithValue("@IsActive", '1');
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
}