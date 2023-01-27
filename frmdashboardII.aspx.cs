using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using System.Diagnostics;
using System.IO;
using System.Threading;

public partial class frmdashboardII : System.Web.UI.Page
{
    public string orgcode;
    public string role;
    errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {

	
        try
        {

           
         
            Fillvsadata("VSAlicensedetails");

            FillSdDeskdata("VSASDdeskdetails");
            FillSdTechData("SDTechdeskdetails");
            FillAllLicensea("Totallicense");
            if (!IsPostBack)
            {

                //  FillGroups();

        
           
             
                
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
        }
    }


 

 

    
  


    //Bind Book Records in GridView
  //  BLL objBLL = new BLL();
  
   

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int stock = int.Parse(e.Row.Cells[4].Text);

            if (stock < 0)
            {
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].BackColor = System.Drawing.Color.White;
                e.Row.Cells[4].Font.Bold = true;
            }
           
            else
            { }
        }

    }
 
    private void ScriptAppovalPending()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            con.Open();
            string cmdstr = "pcv_DashboardCounts";
            using (SqlCommand cmd = new SqlCommand(cmdstr, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    adp.SelectCommand.CommandTimeout = 180;
                    cmd.Parameters.AddWithValue("@Option", "ScriptsPendingApprovals");
                    adp.Fill(ds);
                    // lblScriptsPendingApprovals.Text = ds.Tables[0].Rows[0]["num"].ToString();
                }
            }
        }
    }
 



   
 
 


  

   

    private void FillDetailsCustomer(string Function,string Heading)
    {
        try
        {
            lblsofname.Text = Heading;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_DashboardCounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {

                            adp.SelectCommand.CommandTimeout = 180;
                            //      cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                        //   cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Option",Function);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                //FillGroups();
                                Modal();

                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();
                                Modal();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // lblError.Text = ex.Message;
            msg.ReportError1(ex.Message);
        }
    }
 






 









  




    protected void ImgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + lblsofname.Text + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //To Export all pages
                GridDashboardDetails.AllowPaging = false;
                // this.BindGrid();
                foreach (TableCell cell in GridDashboardDetails.HeaderRow.Cells)
                {
                    cell.BackColor = GridDashboardDetails.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridDashboardDetails.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridDashboardDetails.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridDashboardDetails.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridDashboardDetails.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void ChartOfflineMachines_Click(object sender, ImageMapEventArgs e)
    {
    //    try
    //    {
    //        lblsofname.Text = "Offline Machines";
    //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
    //        {

    //            con.Open();
    //            using (SqlCommand cmd = new SqlCommand("pcv_HardInvDash", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
    //                {
    //                    using (DataTable dt = new DataTable())
    //                    {
    //                        adp.SelectCommand.CommandTimeout = 180;
    //                        cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());
    //                        cmd.Parameters.AddWithValue("@Search", e.PostBackValue.ToString());
    //                        cmd.Parameters.AddWithValue("@Option", "OfflineMachinesDetails");
    //                        adp.Fill(dt);
    //                        if (dt.Rows.Count > 0)
    //                        {

    //                            lblTotal.Text = dt.Rows.Count.ToString();
    //                            GridDashboardDetails.DataSource = dt;
    //                            GridDashboardDetails.DataBind();
    //                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //                            sb.Append(@"<script type='text/javascript'>");
    //                            sb.Append("$('#basicModal').modal('show');");
    //                            sb.Append(@"</script>");
    //                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
    //                        }
    //                        else
    //                        {
    //                            GridDashboardDetails.DataSource = null;
    //                            GridDashboardDetails.DataBind();

    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg.ReportError(ex.Message);
    //    }
    }

    

    private void Modal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#basicModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);


    }



    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ChartRegionWiseCustomers_Click(object sender, ImageMapEventArgs e)
    {
        try
        {
            lblsofname.Text = "Customer Region Wise";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_DashboardCounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());
                            cmd.Parameters.AddWithValue("@Region", e.PostBackValue.ToString());
                            cmd.Parameters.AddWithValue("@Option", "FillCustomChartClick");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

 
 
    
    protected void lnkpurchasedlicense_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "Customer Region Wise";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_DashboardCounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());
                           
                            cmd.Parameters.AddWithValue("@Option", "FillCustomChartClick");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

  
  public void  Fillvsadata(string data)
    {
        using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {

            mycon.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", mycon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable datadt = new DataTable())
                    {
                        sda.SelectCommand.CommandTimeout = 180;
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                      //  cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", data);
                        sda.Fill(datadt);

                     //   lblvsapurchased.Text = datadt.Rows[0]["VSAPurchased"].ToString();

                     //   lblvsaconsumed.Text = datadt.Rows[0]["VSAConsumed"].ToString();
                   



                    }
                }
            }
            
        }
    }

    public void FillSdDeskdata(string data)
    {
        using (SqlConnection mycon1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {

            mycon1.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", mycon1))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable datadt = new DataTable())
                    {
                        sda.SelectCommand.CommandTimeout = 180;
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                        //  cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", data);
                        sda.Fill(datadt);

                     //   lblSddeskPurchased.Text = datadt.Rows[0]["SDPurchased"].ToString();

                       // lblSDdeskConsumed.Text = datadt.Rows[0]["SDConsumed"].ToString();




                    }
                }
            }

        }
    }
    public void FillSdTechData(string data)
    {
        using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {

            mycon.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", mycon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable datadt = new DataTable())
                    {
                        sda.SelectCommand.CommandTimeout = 180;
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                        //  cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", data);
                        sda.Fill(datadt);

                      //  lbSDtechpurchased.Text = datadt.Rows[0]["SDTechPurchased"].ToString();

                      //  lblSdtechConsumed.Text = datadt.Rows[0]["SdTechConsumed"].ToString();




                    }
                }
            }

        }
    }
    protected void lnkbtnSDconsumed_Click(object sender, EventArgs e)
    {

    }

  

 

    protected void lnktbnVsaConsumed_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "VSA Consumed";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "FillVSAConsumed");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void linkSddeskPurchased_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "VSA Purchased";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "FillSDPurchased");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void lnkSDdeskConsumed_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "SD Consumed";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "FillSDConsumed");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void lnkSdtechConsumed_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "SD Tech Consumed";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "FillSDTechConsumed");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void lnksdtechlicense_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "SD Tech Purchased ";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "FillSDTechPurchased");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void lnkbtnVSapurchased_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "VSA Purchased";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "FillVSAPurchased");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }
    public void FillAllLicensea(string data)
    {
        using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {

            mycon.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", mycon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable datadt = new DataTable())
                    {
                        sda.SelectCommand.CommandTimeout = 180;
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                        //  cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", data);
                        sda.Fill(datadt);

                      //  lblvsapurchased.Text = datadt.Rows[0]["Purchased"].ToString();

                     //   lblvsaconsumed.Text = datadt.Rows[0]["Consumed"].ToString();
                     //   lblavailable.Text = datadt.Rows[0]["Available"].ToString();



                    }
                }
            }

        }
    }
    protected void lnkpurchasedlicense_Click1(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "PcVisor Purchased License";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "AllLicenseDetails");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void linkconsumed_Click(object sender, EventArgs e)
    {
        try
        {
            lblsofname.Text = "PcVisor Consumed License";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_sp_ProductInventory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.SelectCommand.CommandTimeout = 180;
                            //cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());

                            cmd.Parameters.AddWithValue("@Option", "AllConsumedLicDetails");
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                lblTotal.Text = dt.Rows.Count.ToString();
                                GridDashboardDetails.DataSource = dt;
                                GridDashboardDetails.DataBind();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#basicModal').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                GridDashboardDetails.DataSource = null;
                                GridDashboardDetails.DataBind();

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
        }
    }

    protected void lnlavailable_Click(object sender, EventArgs e)
    {

    }
}