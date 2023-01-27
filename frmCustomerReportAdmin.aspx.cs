using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmCustomerReportAdmin : System.Web.UI.Page
{
    //  errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] != null)
        {
            if (!IsPostBack)
            {

                if (Session["UserType"].ToString().Contains("Admin"))
                {
                    FillCustomers("CustomerReportAdmin");

                }
                else
                {
                    FillCustomers("CustomerReportNonAdmin");
                }

            }

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

    }

    protected void FillCustomers(string Function)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_FilterData"))
                {


                    using (SqlDataAdapter adp = new SqlDataAdapter())
                    {
                        cmd.Connection = sqlCon;
                        adp.SelectCommand = cmd;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Option", Function);
                        using (DataTable dt = new DataTable())
                        {


                            adp.Fill(dt);
                            lblTotalCount.Text = dt.Rows.Count.ToString();
                            gvAllAssets.DataSource = dt;
                            gvAllAssets.DataBind();
                        }
                        //if (dt.Rows.Count > 0)
                        //{
                        //    lblTotalCount.Text = dt.Rows.Count.ToString();
                        //    gvAllAssets.DataSource = dt;
                        //    gvAllAssets.DataBind();
                        //}
                        //else
                        //{
                        //ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        //GridView1.DataSource = ds;
                        //GridView1.DataBind();
                        //Empty DataTable to execute the “else-condition”  
                        // DataTable dt = new DataTable();
                        // GridView1.DataSource = dt;
                        // GridView1.DataBind();
                        //// Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('No Data Found')", true);

                        // }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            // Response.Write("Oops! error occured :" + ex.Message.ToString());
        }


    }


    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["UserType"].ToString() == "Admin")
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        string sql = "AMS_FilterData";
                        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ColumnName", ddlSearchItems.SelectedValue);
                            cmd.Parameters.AddWithValue("@SearchItem", txtSearch.Text.Trim());
                            cmd.Parameters.AddWithValue("@Option", "FillCustomerDetailsAdmin");
                        }
                        cmd.CommandText = sql;
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            lblTotalCount.Text = dt.Rows.Count.ToString();
                            gvAllAssets.DataSource = dt;
                            gvAllAssets.DataBind();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                lblerrorMsg.Text = ex.Message;
            }
        }
        else
        {
            try
            {
                string constr1 = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr1))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        string sql = "AMS_FilterData";
                        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ColumnName", ddlSearchItems.SelectedValue);
                            cmd.Parameters.AddWithValue("@SearchItem", txtSearch.Text.Trim());
                            cmd.Parameters.AddWithValue("@Option", "FillCustomerDetailsNonAdmin");
                        }
                        cmd.CommandText = sql;
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            lblTotalCount.Text = dt.Rows.Count.ToString();
                            gvAllAssets.DataSource = dt;
                            gvAllAssets.DataBind();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                lblerrorMsg.Text = ex.Message;
            }
        }

    }
    public override void VerifyRenderingInServerForm(Control control) { }
    protected void ImageBtnExport_Click(object sender, ImageClickEventArgs e)
    {


        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AllCustomers.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gvAllAssets.AllowPaging = false;
            if (Session["UserType"].ToString() == "Admin")
            {
                FillCustomers("CustomerReportAdmin");
            }
            else
            {
                FillCustomers("CustomerReportNonAdmin");
            }


            gvAllAssets.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvAllAssets.HeaderRow.Cells)
            {
                cell.BackColor = gvAllAssets.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvAllAssets.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvAllAssets.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvAllAssets.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvAllAssets.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }

    protected void gvAllAssets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllAssets.PageIndex = e.NewPageIndex;
        if (Session["UserType"].ToString() == "Admin")
        {
            FillCustomers("CustomerReportAdmin");
           
            gvAllAssets.DataBind();
        }
        else
        {
            FillCustomers("CustomerReportNonAdmin");
        
            gvAllAssets.DataBind();
        }

    }
}