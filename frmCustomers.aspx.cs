using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Metadata;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmCustomers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtcontractAmt.Attributes.Add("OnFocus", "Javascript:multiplyBy()");
        if (!IsPostBack)
        {
            //if (Session["UserType"] != null && Session["UserName"] != null)
            //{
            //oobel.role = Session["UserType"].ToString();
            FillCustomers();
            FillProducts();
          
     
            FillServerLocation();
            FillLicenseType();
            FillContractType();
            FillPaymentTerm();
            //}
            //else
            //{
            //    Response.Redirect("Default.aspx");
            //}
            txtStart.Text = "";
            txtExpiry.Text = "";
        }
    }
    private void FillServerLocation()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT  ServerLocCode, ServerLocName FROM AMS_ServerLocation"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                ddlCloud_OnPremesis.DataSource = cmd.ExecuteReader();
                ddlCloud_OnPremesis.DataTextField = "ServerLocName";
                ddlCloud_OnPremesis.DataValueField = "ServerLocCode";
                ddlCloud_OnPremesis.DataBind();
                con.Close();
            }
        }
        ddlCloud_OnPremesis.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select Server Location-----", "NA"));
    }
    private void FillLicenseType()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT  LicTypeName, LicTypeCode FROM AMS_LicenseType"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                ddlLicense_Type.DataSource = cmd.ExecuteReader();
                ddlLicense_Type.DataTextField = "LicTypeCode";
                ddlLicense_Type.DataValueField = "LicTypeName";
                ddlLicense_Type.DataBind();
                con.Close();
            }
        }
        ddlLicense_Type.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select License Type-----", "NA"));
    }
    private void FillContractType()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT  ContractName, ContractCode FROM AMS_ContractType"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                ddlContractType.DataSource = cmd.ExecuteReader();
                ddlContractType.DataTextField = "ContractCode";
                ddlContractType.DataValueField = "ContractName";
                ddlContractType.DataBind();
                con.Close();
            }
        }
        ddlContractType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select Contract Type-----", "NA"));
    }
    private void FillPaymentTerm()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT  PaymentTermCode, PaymentTermName FROM AMS_PaymentTerm"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                ddlPaymentTerm.DataSource = cmd.ExecuteReader();
                ddlPaymentTerm.DataTextField = "PaymentTermName";
                ddlPaymentTerm.DataValueField = "PaymentTermCode";
                ddlPaymentTerm.DataBind();
                con.Close();
            }
        }
        ddlPaymentTerm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select Payment Type-----", "NA"));
    }
    private void FillBranchType()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("SELECT  BranchName, BranchCode FROM AMS_Brach where OrgID ='" + ddlOrg.SelectedValue + "'"))
            using (SqlCommand cmd = new SqlCommand("SELECT  BranchName, BranchCode FROM AMS_Brach order by BranchName asc "))
            {
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                ddlBranch.DataSource = cmd.ExecuteReader();
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchCode";
                ddlBranch.DataBind();
                con.Close();
            }
        }
        ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select Branch -----", "NA"));
    }
  
    BEL oobel = new BEL();

    private void FillProducts()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("Select distinct * from LMS_Products order by ProductName asc", con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddlProductName.DataSource = dt;
                            ddlProductName.DataTextField = "ProductName";
                            ddlProductName.DataValueField = "ProductID";
                            ddlProductName.DataBind();

                         

                        }
                    }
                }
            }
            ddlProductName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select Product -----", "NA"));
        }
        catch (Exception ex)
        {

        }
    }

    private void FillCustomers()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("Select distinct Concat( OrgName ,' ',OrgLocation) as  'OrgName',Orgid from AMS_Organization_Master order by OrgName asc", con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ddlOrg.DataSource = dt;
                            ddlOrg.DataTextField = "OrgName";
                            ddlOrg.DataValueField = "Orgid";
                            ddlOrg.DataBind();
                        }
                    }
                }
            }
            ddlOrg.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-----Select Organization-----", "NA"));
        }
        catch (Exception ex)
        {

        }
    }

 


   

    public static int srno;


    ///  AMCEX AMCex = new AMCEX();
    public void btnAdd_Click(object sender, EventArgs e)
    {
        AddedControl();

    }

    private void AddedControl()
    {
        int result;

        //try
        //{



        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();
            using (SqlCommand cmd = new SqlCommand("pcv_SPCustomersDetailsII", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Customer_Id", ddlOrg.SelectedValue);
                cmd.Parameters.AddWithValue("@Customer_Name", ddlOrg.SelectedItem.ToString().Trim());
                cmd.Parameters.AddWithValue("@Cloud_OnPremesis", ddlCloud_OnPremesis.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@License_Type", ddlLicense_Type.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Contract_Type", ddlContractType.Text.Trim());
                cmd.Parameters.AddWithValue("@Product", ddlProductName.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(txtQty.Text));
                cmd.Parameters.AddWithValue("@Technician", txtTechnician.Text.Trim());
                cmd.Parameters.AddWithValue("@Unit_Prices", Convert.ToDecimal(txtunitprice.Text));
                cmd.Parameters.AddWithValue("@Annual_Buy_Price", Convert.ToDecimal(txtAnnualBuyPrice.Text));
                cmd.Parameters.AddWithValue("@Contract_Amount", Convert.ToDecimal(txtcontractAmt.Text));
                cmd.Parameters.AddWithValue("@Monthly_Revenue", Convert.ToDecimal(txtmonthlyRev.Text));
                cmd.Parameters.AddWithValue("@TaxAmount", Convert.ToDecimal(txtTaxamount.Text));
                cmd.Parameters.AddWithValue("@Start", Convert.ToDateTime(txtStart.Text).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Expiry", Convert.ToDateTime(txtExpiry.Text).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Contract_years", txtContractPeriod.Text.Trim());
                cmd.Parameters.AddWithValue("@Account_Manager", txtAccount_Manager.Text.Trim());
                cmd.Parameters.AddWithValue("@Account_Manager_Email", txtAccountManagerEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@PO_No", txtpono.Text.Trim());
                cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());

                cmd.Parameters.AddWithValue("@Payment_Term", ddlPaymentTerm.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@License_Key", txtLicenceKey.Text.Trim());
                cmd.Parameters.AddWithValue("@Engineer_Name", "");
                cmd.Parameters.AddWithValue("@Category_Alm", txtcategoryALM.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", "1");
                cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text.Trim());
                cmd.Parameters.AddWithValue("@Option", "Insert");

                result = cmd.ExecuteNonQuery();
                //  cmd.Dispose();
                if (result > 0)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                    // string msg = "Changes has been Saved";

                    // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + msg + "');window.location.href='" + Request.RawUrl + "';", true);
                    //  Page.ClientScript.RegisterStartupScript(typeof(Page), "", "alert('Data Saved Sucessfully')", true);
                    // BindToGrid();
                    //  ClearAll();
                }
                else
                {
                    string msg = "Not Saved";

                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + msg + "');window.location.href='" + Request.RawUrl + "';", true);
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('Not Saved ! error found')", true);
                }
            }
        }
        //}

        //catch (Exception ex)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('" + ex.Message + "')", true);
        //}

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int result;
        //try
        //{

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_SpCustomersDetailsII", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                
                     cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Customer_Id", ddlOrg.SelectedValue);
                 //   cmd.Parameters.AddWithValue("@Customer_Name", ddlOrg.SelectedItem.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Cloud_OnPremesis", ddlCloud_OnPremesis.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@License_Type", ddlLicense_Type.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Contract_Type", ddlContractType.Text.Trim());
                    cmd.Parameters.AddWithValue("@Product", ddlProductName.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(txtQty.Text));
                    cmd.Parameters.AddWithValue("@Technician", txtTechnician.Text.Trim());
                    cmd.Parameters.AddWithValue("@Unit_Prices", Convert.ToDecimal(txtunitprice.Text));
                    cmd.Parameters.AddWithValue("@Annual_Buy_Price", Convert.ToDecimal(txtAnnualBuyPrice.Text));
                    cmd.Parameters.AddWithValue("@Contract_Amount", Convert.ToDecimal(txtcontractAmt.Text));
                    cmd.Parameters.AddWithValue("@Monthly_Revenue", Convert.ToDecimal(txtmonthlyRev.Text));
                    cmd.Parameters.AddWithValue("@TaxAmount", Convert.ToDecimal(txtTaxamount.Text));
                    cmd.Parameters.AddWithValue("@Start", Convert.ToDateTime(txtStart.Text).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Expiry", Convert.ToDateTime(txtExpiry.Text).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Contract_years", txtContractPeriod.Text.Trim());
                    cmd.Parameters.AddWithValue("@Account_Manager", txtAccount_Manager.Text.Trim());
                    cmd.Parameters.AddWithValue("@Account_Manager_Email", txtAccountManagerEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@PO_No", txtpono.Text.Trim());
                    cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedItem.ToString());

                    cmd.Parameters.AddWithValue("@Payment_Term", ddlPaymentTerm.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@License_Key", txtLicenceKey.Text.Trim());
                    cmd.Parameters.AddWithValue("@Engineer_Name", "");
                    cmd.Parameters.AddWithValue("@Category_Alm", txtcategoryALM.Text.Trim());
                    cmd.Parameters.AddWithValue("@IsActive", "1");
                    cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text.Trim());
                    cmd.Parameters.AddWithValue("@Option", "Update");
                    result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (result > 0)
                    {
                    Response.Redirect(Request.Url.AbsoluteUri);

                }
                    else
                    {
                    Response.Write("<script type=\"text/javascript\">alert('Record Insert Successfully!!!');</script>");
                }
                }
            }
        //}
        //catch (Exception ex)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.Page), "", "alert('" + ex.Message + "')", true);
        //}
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        FillOrders();
    }

    protected void FillOrders()
    {

        try
        {
        
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT * from pcv_CustomersDetails where Customer_Id='"+ddlOrg.SelectedValue.ToString()+"'  ", con))
                {
                  //  cmd.Parameters.AddWithValue("@userid", ddlOrg.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {


                                lblTotal.Text = dt.Rows.Count.ToString();
                                gvUsers.DataSource = dt;
                                gvUsers.DataBind();

                            }
                            else
                            {
                                //Empty DataTable to execute the “else-condition”  

                                gvUsers.EmptyDataText = "No Records Found";
                                gvUsers.DataSource = null;
                                gvUsers.DataBind();
                            }
                        }

                        // DataTable table = new BLL().FillUserDetails(Convert.ToInt32(gvUsers.DataKeys[rowIndex].Values[0]));
                      
                    }
                }
            }

        }
        catch (Exception ex)
        {
     
            ExceptionLogging.SendErrorToText(ex);
        }
        finally
        {
            //objBLL = null;
        }
    }
    static int id;
    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "SelectUser")
        {
          
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            id = Convert.ToInt32(gvUsers.DataKeys[rowIndex].Values["ID"].ToString());
                
                     btnAdd.Visible = false;
                       btnUpdate.Visible = true;
                  if (ddlOrg.Items.FindByValue(gvUsers.Rows[rowIndex].Cells[0].Text.Trim()) != null)
                 {

                    ddlOrg.SelectedValue = gvUsers.Rows[rowIndex].Cells[0].Text.Trim();
                }

            if (ddlCloud_OnPremesis.Items.FindByText(gvUsers.Rows[rowIndex].Cells[2].Text.Trim()) != null)
            {

                ddlCloud_OnPremesis.SelectedItem.Text = gvUsers.Rows[rowIndex].Cells[2].Text.Trim();

            }
            if (ddlLicense_Type.Items.FindByText(gvUsers.Rows[rowIndex].Cells[3].Text.Trim()) != null)
            {

                ddlLicense_Type.SelectedItem.Text = gvUsers.Rows[rowIndex].Cells[3].Text.Trim();

            }
            if (ddlContractType.Items.FindByText(gvUsers.Rows[rowIndex].Cells[4].Text.Trim()) != null)
            {

                ddlContractType.SelectedItem.Text = gvUsers.Rows[rowIndex].Cells[4].Text.Trim();

            }
            if (ddlProductName.Items.FindByText(gvUsers.Rows[rowIndex].Cells[5].Text.Trim()) != null)
            {

                ddlProductName.SelectedItem.Text = gvUsers.Rows[rowIndex].Cells[5].Text.Trim();
                MakeTextboxReadonly();
            }
            txtTechnician.Text = gvUsers.Rows[rowIndex].Cells[6].Text.Trim();
            txtQty.Text = gvUsers.Rows[rowIndex].Cells[7].Text.Trim();
            txtunitprice.Text = gvUsers.Rows[rowIndex].Cells[8].Text.Trim();
            txtAnnualBuyPrice.Text = gvUsers.Rows[rowIndex].Cells[9].Text.Trim();
            txtcontractAmt.Text = gvUsers.Rows[rowIndex].Cells[10].Text.Trim();
            txtmonthlyRev.Text = gvUsers.Rows[rowIndex].Cells[11].Text.Trim();
            txtTaxamount.Text = gvUsers.Rows[rowIndex].Cells[12].Text.Trim();
            txtStart.Text = gvUsers.Rows[rowIndex].Cells[13].Text.Trim();
            txtExpiry.Text = gvUsers.Rows[rowIndex].Cells[14].Text.Trim();
            txtContractPeriod.Text = gvUsers.Rows[rowIndex].Cells[15].Text.Trim();
            txtAccount_Manager.Text = gvUsers.Rows[rowIndex].Cells[16].Text.Trim();
            txtAccountManagerEmail.Text = gvUsers.Rows[rowIndex].Cells[17].Text.Trim();
            if (ddlRegion.Items.FindByText(gvUsers.Rows[rowIndex].Cells[18].Text.Trim()) != null)
            {

                ddlRegion.SelectedItem.Text = gvUsers.Rows[rowIndex].Cells[18].Text.Trim();

            }
            if (ddlPaymentTerm.Items.FindByText(gvUsers.Rows[rowIndex].Cells[19].Text.Trim()) != null)
            {

                ddlPaymentTerm.SelectedItem.Text = gvUsers.Rows[rowIndex].Cells[19].Text.Trim();

            }

            txtpono.Text = gvUsers.Rows[rowIndex].Cells[20].Text.Trim();                                            
            txtcategoryALM.Text = gvUsers.Rows[rowIndex].Cells[21].Text.Trim();     
            txttaxper.Text = "5";
            txtremarks.Text = gvUsers.Rows[rowIndex].Cells[22].Text.Trim();




                         }
                       }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        try
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=AMCExpiryDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvUsers.AllowPaging = false;
                // this.BindGrid();
                gvUsers.HeaderRow.Cells[8].Visible = false;

                // Loop through the rows and hide the cell in the first column
                for (int i = 0; i < gvUsers.Rows.Count; i++)
                {
                    GridViewRow row = gvUsers.Rows[i];
                    row.Cells[8].Visible = false;
                }

                foreach (System.Web.UI.WebControls.TableCell cell in gvUsers.HeaderRow.Cells)
                {
                    cell.BackColor = gvUsers.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvUsers.Rows)
                {

                    foreach (System.Web.UI.WebControls.TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvUsers.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvUsers.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvUsers.RenderControl(hw);

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


        }
    }
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {

        if (txtQty.Text == "")
        {
            txtQty.Text = "0";
        }
        else
        {
            GetTotal();

        }
    }

    private void GetTotal()
    {
        //  txtTotal_Price.Text = (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtUnit_Prices.Text)).ToString();
    }

    protected void txtUnit_Prices_TextChanged(object sender, EventArgs e)
    {
        //if (txtUnit_Prices.Text == "")
        //{
        //    txtUnit_Prices.Text = "0";

        //}
        //else
        //{
        //    GetTotal();

        //}

    }

    protected void txtTotal_Price_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranchType();
        FillOrders();
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select  * from AMS_Organization_Master  where  OrgId='" + ddlOrg.SelectedValue + "'", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Orgid", ddlOrg.SelectedValue.ToString());
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                //ddlCloud_OnPremesis.SelectedValue = dt.Rows[0]["Cloud_OnPremesis"].ToString();
                                // ddlLicense_Type.Text = dt.Rows[0]["License_Type"].ToString();
                                txtAccountManagerEmail.Text = dt.Rows[0]["AccManagerEmail"].ToString();
                                txtAccount_Manager.Text = dt.Rows[0]["AccntManager"].ToString();
                                //txtExpiry.Text = dt.Rows[0]["Expiry"].ToString();
                                //txtStart.Text = dt.Rows[0]["Start"].ToString();
                                ddlBranch.Text = dt.Rows[0]["OrgLocation"].ToString();
                                txtNumber.Text = dt.Rows[0]["ContactNo"].ToString();
                                txtCustomerAddress.Text = dt.Rows[0]["OrgAddress"].ToString();
                                //ddlBranch.Text = dt.Rows[0]["OrgLocation"].ToString();
                                txtContactPerson_Name.Text = dt.Rows[0]["ContactPerson"].ToString();
                                ddlRegion.SelectedValue = dt.Rows[0]["Region"].ToString();

                                // ddlPaymentTerm.SelectedValue = dt.Rows[0]["Payment_Term"].ToString();
                                // ddlCloud_OnPremesis.Text = dt.Rows[0]["Cloud_OnPremesis"].ToString();



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
    protected void MakeTextboxReadonly()
    {
        if (ddlProductName.SelectedItem.ToString() == "ALM")
        {
            txtcategoryALM.Enabled = true;
            txtTechnician.Enabled = false;

        }
        else if (ddlProductName.SelectedItem.ToString() == "VSA")
        {
            txtcategoryALM.Enabled = false;
            txtTechnician.Enabled = false;
        }
        else if (ddlProductName.SelectedItem.ToString() == "SD")
        {
            txtcategoryALM.Enabled = false;
            txtTechnician.Enabled = true;
        }
        else if (ddlProductName.SelectedItem.ToString().Contains("Add"))
        {
            txtcategoryALM.Enabled = false;
            txtTechnician.Enabled = true;
        }
        else
        {
            txtTechnician.Enabled = false;
            txtcategoryALM.Enabled = false;
        }
    }


    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        MakeTextboxReadonly();
    }


   

    string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;





    //called on row add new command
    private Boolean CheckEmptyBox()
    {
        if (string.IsNullOrWhiteSpace(txtpono.Text))

        {

            return true;
        }
        else
        {
            return false;
        }
    }


    //called on row update command
  
    //called on row delete command
   


   
  
    protected void txtQty_TextChanged1(object sender, EventArgs e)
    {
        CalculateMonthlyRevenue();
    }

    private void CalculateMonthlyRevenue()
    {
        decimal monthvalue = Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(txtContractPeriod.Text) )/12);


        //txtAnnualBuyPrice.Text = (Convert.ToDecimal(txtunitprice.Text) * Convert.ToDecimal(txtQty.Text)).ToString();
        //txtmonthlyRev.Text =( Convert.ToDecimal(txtunitprice.Text) * Convert.ToDecimal(txtQty.Text) / 12).ToString();
        //txtTaxamount.Text = (Convert.ToDecimal(txttaxper.Text) * Convert.ToDecimal(txtAnnualBuyPrice.Text) / Convert.ToDecimal(monthvalue) * Convert.ToDecimal(0.01)).ToString();
        //txtcontractAmt.Text = ((Convert.ToDecimal(txtAnnualBuyPrice.Text) / Convert.ToDecimal(monthvalue)) + Convert.ToDecimal(txtTaxamount.Text)).ToString();
        txtAnnualBuyPrice.Text = (Convert.ToDecimal(txtunitprice.Text) * Convert.ToDecimal(txtQty.Text)).ToString();
        txtmonthlyRev.Text = (Convert.ToDecimal(txtunitprice.Text) * Convert.ToDecimal(txtQty.Text) / 12).ToString();
        txtTaxamount.Text = (Convert.ToDecimal(txttaxper.Text) * Convert.ToDecimal(txtmonthlyRev.Text) * Convert.ToDecimal(txtContractPeriod.Text) * Convert.ToDecimal(0.01)).ToString();
        txtcontractAmt.Text = (Convert.ToDecimal((Convert.ToDecimal(txtunitprice.Text) * Convert.ToDecimal(txtQty.Text) / 12)* Convert.ToDecimal(txtContractPeriod.Text)) + Convert.ToDecimal(txtTaxamount.Text)).ToString();

    }

    protected void txtContractPeriod_TextChanged(object sender, EventArgs e)
    {
        
   //     txtcontractAmt.Text = (Convert.ToDecimal(txtAnnualBuyPrice.Text) / Math.Round(12/Convert.ToDecimal(txtContractPeriod.Text))).ToString();
        CalculateMonthlyRevenue();
    }

    protected void txttaxper_TextChanged(object sender, EventArgs e)
    {
        CalculateMonthlyRevenue();
    }

 
    protected void txtunitprice_TextChanged(object sender, EventArgs e)
    {
        CalculateMonthlyRevenue();
    }

    
}
