using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAlmkeydetails : System.Web.UI.Page
{
    errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserType"] != null)
        {
            if (!IsPostBack)
            {
              
                    FillAllAssets();
               
                ddlSearchItems.Items.Insert(0, new ListItem("--------------------Select All--------------------", "0"));
            }

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

  


    private void FillAllAssets()
    {
        try
        { 
                string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from ALM_LicenceMaster order by  CustomerName ASC", con))
                    {
                        // cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblTotalCount.Text = dt.Rows.Count.ToString();
                            gvAllAssets.DataSource = dt;
                            gvAllAssets.DataBind();
                        }
                        else
                        {
                            gvAllAssets.DataSource = dt;
                            gvAllAssets.DataBind();
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

    protected void gvAllAssets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PathUpdate")
        {
            Session["SerialNo"] = e.CommandArgument.ToString();
            // do you what you need to do
            Response.Redirect("frmAssetOut.aspx");
            
        }
    }

   


    protected void ddlSearchItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        if (ddlSearchItems.SelectedIndex == 0)
        {
            FillAllAssets();
            txtSearch.Text = string.Empty;
        }
        else
        {
            txtSearch.Text = string.Empty;
        }

    }





    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
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
                            cmd.Parameters.AddWithValue("@Option", "FillKeyDetails");
                        }
                        cmd.CommandText = sql;
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
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
    public override void VerifyRenderingInServerForm(Control control) { }
    protected void ImageBtnExport_Click(object sender, ImageClickEventArgs e)
    {


        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AllAssets.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gvAllAssets.AllowPaging = false;
          
                FillAllAssets();
            
        

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
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", "attachment;filename=AllAssets.csv");
        //Response.ContentType = "application/text";
        //StringBuilder strBr = new StringBuilder();
        //gvAllAssets.AllowPaging = false;

        //if (Session["UserType"].ToString() == "Admin")
        //{
        //    FillAllAssets();
        //}
        //else
        //{
        //    FillAllAssetsByLocation();
        //}
        //for (int i = 0; i < gvAllAssets.Columns.Count; i++)
        //{
        //    strBr.Append(gvAllAssets.Columns[i].HeaderText + ',');
        //}
        //strBr.Append("\n");
        //for (int j = 0; j < gvAllAssets.Rows.Count; j++)
        //{
        //    for (int k = 0; k < gvAllAssets.Columns.Count; k++)
        //    {
        //        strBr.Append(gvAllAssets.Rows[j].Cells[k].Text + ',');
        //    }
        //    strBr.Append("\n");
        //}
        //Response.Write(strBr.ToString());
        //Response.Flush();
        //Response.End();
    }
}