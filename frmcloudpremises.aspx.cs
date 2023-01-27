using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmcloudpremises : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCustomers();
            BindSubjectData();
            FillServerLocation();

            btnUpdate.Visible = false;
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
                ddlcloud.DataSource = cmd.ExecuteReader();
                ddlcloud.DataTextField = "ServerLocName";
                ddlcloud.DataValueField = "ServerLocCode";
                ddlcloud.DataBind();
                con.Close();
            }
        }
        ddlcloud.Items.Insert(0, new ListItem("-----Select Server Location-----", "0"));
    }

    private void FillCustomers()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("Select distinct OrgName,Orgid from AMS_Organization_Master order by OrgName asc", con))
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
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {

            using (SqlCommand cmd = new SqlCommand("pcv_SPClouddetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", ddlOrg.SelectedValue.Trim());
                //cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                cmd.Parameters.AddWithValue("@Hosting", ddlcloud.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@WindowUserName", txtwindusername.Text.Trim());
                cmd.Parameters.AddWithValue("@WindowPassword", txtwindpassword.Text.Trim());
                //  cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson_Name.Text.Trim());
                //cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@PortalUserName", txtportalusername.Text.Trim());
                cmd.Parameters.AddWithValue("@PortalPassword", txtportalpassword.Text.Trim());
                //cmd.Parameters.AddWithValue("@ContactPersonNumber", txtnumber.Text.Trim());
                //cmd.Parameters.AddWithValue("@Email", txtemailid.Text.Trim());
                cmd.Parameters.AddWithValue("@LocalIP", txtlocalip.Text.Trim());
                cmd.Parameters.AddWithValue("@PublicIP", txtpublicip.Text.Trim());
                //cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@URL", txturl.Text.Trim());
                cmd.Parameters.AddWithValue("@Owner", txtowner.Text.Trim());
                cmd.Parameters.AddWithValue("@SAUserName", txtsausername.Text.Trim());
                cmd.Parameters.AddWithValue("@SAPassword", txtsaPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Option", "Insert");


                // cmd.Parameters.AddWithValue("@Active", ddlStatus.SelectedValue.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
        BindSubjectData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int result;
        try
        {

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand("pcv_SpCustomersDetails", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Label10.Text);
                    cmd.Parameters.AddWithValue("@OrgName", ddlOrg.SelectedItem.Text.Trim());
                    //cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Cloudtype", ddlcloud.SelectedItem.Text.Trim());
                    cmd.Parameters.AddWithValue("@WindowUserName", txtwindusername.Text.Trim());
                    cmd.Parameters.AddWithValue("@WindowPassword", txtwindpassword.Text.Trim());
                    //cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson_Name.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                    cmd.Parameters.AddWithValue("@PortalUserName", txtportalusername.Text.Trim());
                    cmd.Parameters.AddWithValue("@PortalPassword", txtportalpassword.Text.Trim());
                    //cmd.Parameters.AddWithValue("@ContactPersonNumber", txtnumber.Text.Trim());
                    //cmd.Parameters.AddWithValue("@Email", txtemailid.Text.Trim());
                    cmd.Parameters.AddWithValue("@LocalIP", txtlocalip.Text.Trim());
                    cmd.Parameters.AddWithValue("@PublicIP", txtpublicip.Text.Trim());
                    cmd.Parameters.AddWithValue("@Region", ddlregion.Text.Trim());
                    cmd.Parameters.AddWithValue("@URL", txturl.Text.Trim());
                    cmd.Parameters.AddWithValue("@Owner", txtowner.Text.Trim());
                    cmd.Parameters.AddWithValue("@SAUsername", txtsausername.Text.Trim());
                    cmd.Parameters.AddWithValue("@SAPassword", txtsaPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@Option", "Update");
                    result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (result > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('Data Updated Sucessfully')", true);
                        BindSubjectData();
                        //  ClearAll();
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('Not Updated ! error found')", true);
                    }
                }
            }
            BindSubjectData();
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "", "alert('" + ex.Message + "')", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {


        }
    }
    protected void BindSubjectData()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection sqlCon = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * from AMS_Clouddetails";
                cmd.Connection = sqlCon;
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.Rows[0].Visible = false;
                }
                sqlCon.Close();
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool IsDeleted = false;
        //getting key value, row id
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        int SubjectID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        //getting row field subjectname
        Label SubjectName = (Label)GridView1.Rows[e.RowIndex].FindControl("lblNumber");
        using (SqlConnection sqlCon = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "DELETE FROM AMS_Clouddetails WHERE ID = @Id";
                cmd.Parameters.AddWithValue("@Id", SubjectID);
                cmd.Connection = sqlCon;
                sqlCon.Open();
                IsDeleted = cmd.ExecuteNonQuery() > 0;
                sqlCon.Close();
            }
        }
        if (IsDeleted)
        {
            lblMsg.Text = "'" + SubjectName.Text + "' item details has been deleted successfully!";
            lblMsg.ForeColor = System.Drawing.Color.Green;
            BindSubjectData();
        }
        else
        {
            lblMsg.Text = "Error while deleting '" + SubjectName.Text + "' item details";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCustomerData();
    }
    protected void FillCustomerData()
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select  * from AMS_Organization_Master  where  OrgId='" + ddlOrg.SelectedValue+"'", con))
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

                                txtAccountManager.Text = dt.Rows[0]["AccntManager"].ToString();

                                txtemailid.Text = dt.Rows[0]["ContactPrsnEmail"].ToString();

                                txtLocation.Text = dt.Rows[0]["OrgLocation"].ToString();
                                txtnumber.Text = dt.Rows[0]["ContactNo"].ToString();
                                ddlregion.SelectedValue = dt.Rows[0]["Region"].ToString();
                                // txtLocation.Text = dt.Rows[0]["OrgLocation"].ToString();
                                txtContactPerson_Name.Text = dt.Rows[0]["ContactPerson"].ToString();


                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }


        //using (DataTable dt = new DataTable())
        //{
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("AMS_FilterData"))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Option", "search");
        //            cmd.Parameters.AddWithValue("@Orgname", ddlOrg.SelectedItem.Text);
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader DR1 = cmd.ExecuteReader())
        //            {


        //                if (DR1.Read())
        //                {
        //                    if (ddlcloud.Items.FindByValue(DR1.GetValue(0).ToString().Trim()) != null)
        //                    {
        //                        ddlcloud.SelectedValue = DR1.GetValue(0).ToString().Trim();
        //                    }

        //                    txtCustomerName.Text = DR1.GetValue(1).ToString();
        //                    txtemailid.Text = DR1.GetValue(2).ToString();
        //                    txtwindusername.Text = DR1.GetValue(3).ToString();
        //                    txtwindpassword.Text = DR1.GetValue(4).ToString();
        //                    txtportalusername.Text = DR1.GetValue(5).ToString();
        //                    txtportalpassword.Text = DR1.GetValue(6).ToString();
        //                    txtLocation.Text = DR1.GetValue(7).ToString();
        //                    txtregion.Text = DR1.GetValue(8).ToString();
        //                    txtlocalip.Text = DR1.GetValue(9).ToString();
        //                    txtpublicip.Text = DR1.GetValue(10).ToString();
        //                    txtowner.Text = DR1.GetValue(11).ToString();
        //                    txtnumber.Text = DR1.GetValue(12).ToString();
        //                    txtsausername.Text = DR1.GetValue(13).ToString();
        //                    txtsaPassword.Text = DR1.GetValue(14).ToString();

        //                }


        //            }
        //            con.Close();
        //        }
        //    }
        //}
    }

}