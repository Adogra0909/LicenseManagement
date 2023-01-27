using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    public string orgcode;
    public string role;
    errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {


            RevenueByRegion();
            CustomerByRegion();
          //  Fillvsadata("Totallicense");
          
            // FillServiceDeskTech("SDTechCount");
            if (!IsPostBack)
            {

                //  FillGroups();

                if (ddlRegion.SelectedIndex == 0)
                {
                    Fillvsadata("Totallicense");
                    FillProductWiseLic();
                    DashboardCountsCustom("CustomerCount");
                    DashboardCountsRenewal("RenewalCount");
                }
                else
                {
                    FillvsadataByRegion("TotallicenseByRegion");
                    FillProductWiseLicByRegion();
                    DashboardCountsCustom("CountByRegion");
                    DashboardCountsRenewalByRegionFilter("RenewalCountByRegion");
                }




            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
        }
    }


    private void FillProductWiseLic()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select Product,sum(Qty) as 'Count' from pcv_CustomerDataOrg  group by Product", con))
                {
                    cmd.CommandType = CommandType.Text;
                 //   cmd.Parameters.AddWithValue("@LocationCode", Session["LocationCode"]);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //  lblTotalCount.Text = dt.Rows.Count.ToString();
                            GridAssetDetails.DataSource = dt;
                            GridAssetDetails.DataBind();
                        }
                        else
                        {
                            GridAssetDetails.DataSource = dt;
                            GridAssetDetails.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
           // lb.Text = msg.ms;
        }
        finally
        {
            //objBLL = null;
        }
    }
    private void FillProductWiseLicByRegion()
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select Product,sum(Qty) as 'Count' from pcv_CustomerDataOrg where Region like '"+ddlRegion.SelectedValue+"'  group by Product", con))
                {
                    cmd.CommandType = CommandType.Text;
                     // cmd.Parameters.AddWithValue("@LocationCode", Session["LocationCode"]);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //  lblTotalCount.Text = dt.Rows.Count.ToString();
                            GridAssetDetails.DataSource = dt;
                            GridAssetDetails.DataBind();
                        }
                        else
                        {
                            GridAssetDetails.DataSource = dt;
                            GridAssetDetails.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
            // lb.Text = msg.ms;
        }
        finally
        {
            //objBLL = null;
        }
    }
    private void RevenueByRegion()
    {
        string chartrevenue = "";
        string revenuecount = "";
        string revenuelabel = "";
        try
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_DashboardCounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "FillCustomerChartRevenue");
                    using (SqlDataAdapter adp = new SqlDataAdapter())
                    {
                        //  cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());
                        cmd.Connection = con;
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.CommandTimeout = 180;
                        using (DataTable dt = new DataTable())
                        {
                           // adp.Fill(dt);

                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                chartrevenue += "<script>";
                                foreach (DataRow row in dt.Rows)
                                {
                                    revenuecount += row[1] + ",";
                                    revenuelabel += "\"" + row[0] + "\",";
                                }
                                revenuecount = revenuecount.Substring(0, revenuecount.Length - 1);
                                revenuelabel = revenuelabel.Substring(0, revenuelabel.Length - 1);
                                chartrevenue += "chartrevenuelabel = [" + revenuelabel + "]; chartrevenuecount = [" + revenuecount + "]";
                                chartrevenue += "</script>";
                                ltrevenue.Text = chartrevenue.ToString();
                            }
                           
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
        }

    }
    private void CustomerByRegion()
    {

        string chartcustomerregion = "";
        string custcount = "";
        string custlabel = "";
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("AMS_DashboardCounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Option", "FillCustomerChart");
                    using (SqlDataAdapter adp = new SqlDataAdapter())
                    {
                        //  cmd.Parameters.AddWithValue("@Orgid", Session["Orgid"].ToString());
                        cmd.Connection = con;
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.CommandTimeout = 180;
                        using (DataTable dt = new DataTable())
                        {
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                chartcustomerregion += "<script>";
                                foreach (DataRow row in dt.Rows)
                                {
                                    custcount += row[1] + ",";
                                    custlabel += "\"" + row[0] + "\",";
                                }
                                custcount = custcount.Substring(0, custcount.Length - 1);
                                custlabel = custlabel.Substring(0, custlabel.Length - 1);
                                chartcustomerregion += "chartOffmachlabel = [" + custlabel + "]; charOffmachdata = [" + custcount + "]";
                                chartcustomerregion += "</script>";
                                ltrCustomerregion.Text = chartcustomerregion.ToString();
                            }
                            //  ChartRegionWiseCustomers.DataSource = dt;
                            // ChartRegionWiseCustomers.DataBind();
                            //ChartRegionWiseCustomers.Series[0].XValueMember = "Region";
                            //ChartRegionWiseCustomers.Series[0].YValueMembers = "Total";
                            //ChartRegionWiseCustomers.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                            //ChartRegionWiseCustomers.Series[0]["PieLabelStyle"] = "Outside";
                            //ChartRegionWiseCustomers.Series["Default"].Label = " #VALX" + " (#VAL)";
                            //ChartRegionWiseCustomers.Series["Default"].LegendText = "#VALX";
                            //ChartOfflineMachines.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
        }

    }


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

    private void DashboardCountsCustom(string Function)
    {

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
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                   
                        cmd.Parameters.AddWithValue("@Option", Function);
                        adp.Fill(dt);

                        lnktotalCustomers.Text = dt.Rows[0]["TotalCustomers"].ToString();
//Label7.Text=dt.Rows[0]["Word"].ToString();;
                        lbltotalrevenue.Text = dt.Rows[0]["TotalRevenue"].ToString();
                     //   lnkrenwealpend.Text = dt.Rows[0]["RenewalPending"].ToString();
                        lbllicense.Text = dt.Rows[0]["License"].ToString();


                    }
                }
            }
        }
    }
    private void DashboardCountsRenewal(string Function)
    {
        
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
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                     //   cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", Function);
                        adp.Fill(dt);

                        lnktotoalrenewal.Text = dt.Rows[0]["TotalRenewal"].ToString();
                        //  lblrenewalamount.Text = dt.Rows[0]["TotalRenewalPrice"].ToString();

                    }
                }
            }
        }
    }


    private void DashboardCountsRenewalByRegionFilter(string Function)
    {

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
                        //cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                        cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", Function);
                        adp.Fill(dt);

                        lnktotoalrenewal.Text = dt.Rows[0]["TotalRenewal"].ToString();
                        //  lblrenewalamount.Text = dt.Rows[0]["TotalRenewalPrice"].ToString();

                    }
                }
            }
        }
    }



    protected void imgbtnlbltotalCustomers_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillCustomer", "CustomerDetails");
        }
        else
        {
            FillDetailsCustomer("FillCustomerRegionWise", "CustomerRegionWise");
        }

    }
    private void FillDetailsCustomer(string Function, string Heading)
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
                            cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Option", Function);
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
        Thread.Sleep(1000);
       
        RevenueByRegion();
        CustomerByRegion();
        if (ddlRegion.SelectedIndex == 0)
        {
            FillProductWiseLic();
            Fillvsadata("Totallicense");
            DashboardCountsCustom("CustomerCount");
            DashboardCountsRenewal("RenewalCount");

        }
        else
        {
            FillvsadataByRegion("TotallicenseByRegion");
            FillProductWiseLicByRegion();
            DashboardCountsCustom("CountByRegion");
            DashboardCountsRenewal("RenewalCountByRegion");
        }
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

    protected void imgtbnproducts_Click(object sender, ImageClickEventArgs e)
    {
        FillDetailsCustomer("FillActiveCustomers", "ActiveCustomersDetails");
    }

    protected void imgbtntotalrevenue_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRevenue", "RevenueDetails");
        }
        else
        {
            FillDetailsCustomer("FillRevenueRegionWise", "RevenueDetailsRegionWise");
        }
    }
    protected void imgbtntotalrenewal_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRenewal", "RenewalDetails");
        }
        else
        {
            FillDetailsCustomer("FillRenewalRegionWise", "RenewalDetailsRegionWise");
        }
    }

    protected void imgbtnlblrenewalamount_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRenewal", "RenewalDetails");
        }
        else
        {
            FillDetailsCustomer("FillRenewalRegionWise", "RenewalDetailsRegionWise");
        }
    }
    protected void imgtbnlicense_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRevenue", "RevenueDetails");
        }
        else
        {
            FillDetailsCustomer("FillRevenueRegionWise", "RevenueDetailsRegionWise");
        }
    }
    protected void chartrevenue_Click(object sender, ImageMapEventArgs e)
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

    }

    protected void linktotal_Click(object sender, EventArgs e)
    {

    }

    public void Fillvsadata(string data)
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

                        lblvsapurchased.Text = datadt.Rows[0]["Purchased"].ToString();

                        lblvsaconsumed.Text = datadt.Rows[0]["Consumed"].ToString();
                        lblavailable.Text = datadt.Rows[0]["Available"].ToString();



                    }
                }
            }

        }
    }

    public void FillvsadataByRegion(string data)
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
                          cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Option", data);
                        sda.Fill(datadt);

                        lblvsapurchased.Text = datadt.Rows[0]["Purchased"].ToString();

                        lblvsaconsumed.Text = datadt.Rows[0]["Consumed"].ToString();
                        lblavailable.Text = datadt.Rows[0]["Available"].ToString();



                    }
                }
            }

        }
    }
    protected void lnkbtnSDconsumed_Click(object sender, EventArgs e)
    {

    }

    protected void link_Click(object sender, EventArgs e)
    {

    }

    protected void lbllicense_Click(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRevenue", "RevenueDetails");
        }
        else
        {
            FillDetailsCustomer("FillRevenueRegionWise", "RevenueDetailsRegionWise");
        }
    }

    protected void btncustomerregion_Click(object sender, EventArgs e)
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
                            cmd.Parameters.AddWithValue("@Region", hdnfldVariable.Value);
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

    protected void btnrevenue_Click(object sender, EventArgs e)
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
                            cmd.Parameters.AddWithValue("@Region", hdnfldVariable.Value);
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

    protected void lbltotalrevenue_Click(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRevenue", "RevenueDetails");
        }
        else
        {
            FillDetailsCustomer("FillRevenueRegionWise", "RevenueDetailsRegionWise");
        }
    }

    protected void lnktotoalrenewal_Click(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRenewal", "RenewalDetails");
        }
        else
        {
            FillDetailsCustomer("FillRenewalRegionWise", "RenewalDetailsRegionWise");
        }
    }

    protected void lnktotalCustomers_Click(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillCustomer", "CustomerDetails");
        }
        else
        {
            FillDetailsCustomer("FillCustomerRegionWise", "CustomerRegionWise");
        }
    }

    protected void lnkrenwealpend_Click(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex == 0)
        {
            FillDetailsCustomer("FillRenewal", "RenewalDetails");
        }
        else
        {
            FillDetailsCustomer("FillRenewalRegionWise", "RenewalDetailsRegionWise");
        }
    }



  

    protected void lnkprodcount_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        //int index = Convert.ToInt32(GridAssetDetails.DataKeys[0].Value.ToString());
        string prdouct = gvrow.Cells[1].Text;
        //  lblsofname.Text = Heading;
        if (ddlRegion.SelectedIndex == 0)
        {
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
                            //      cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                            //   cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Product", prdouct);
                            cmd.Parameters.AddWithValue("@Option", "VSAlicensedetails");
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
        else
        {
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
                            //      cmd.Parameters.AddWithValue("@Org_ID", Session["Orgid"].ToString());
                               cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Product", prdouct);
                            cmd.Parameters.AddWithValue("@Option", "VSAlicensedetailsRegionWise");
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
    }
}