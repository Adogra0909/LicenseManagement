using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmOrg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCustomers();
            btnUpdate.Visible = false;
        }
    }

    private void FillCustomers()
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from AMS_Organization_Master", con))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {

                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write("Oops! error occured :" + ex.Message.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(@"insert into AMS_Organization_Master (ERPID,OrgName,OrgAddress,OrgLocation,ContactPerson,ContactPrsnEmail,ContactNo,AccntManager,
AccManagerEmail,Region,ContactPersonII,ContactPersNoII,ContactPerEmailII,Active) values(@ERPID,@OrgName,@OrgAddress,@OrgLocation,@ContactPerson,@ContactPersonEmail,@ContactNo,@AccountManger,@AccountMangerEmail,@Region,@ContactPersonII,@ContactPersonContII,@ContactPersonEmailII,@Active)", con))
            {
                cmd.Parameters.AddWithValue("@ERPID", txtERPID.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgName", txtCustomerName.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgAddress", txtCustomerAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgLocation", txtLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNo", txtContactno.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonEmail", txtCustomerEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonII", txtcontactpersonII.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonContII", txtContPerII.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonEmailII", txtContPersEmailI.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountManger", txtAccount_Manager.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountMangerEmail", txtAccountManager_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@Active", "1");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(@"update AMS_Organization_Master set OrgName=@OrgName,OrgAddress=@OrgAddress,
OrgLocation=@OrgLocation,ContactPerson=@ContactPerson,ContactPrsnEmail=@ContactPersonEmail,ContactPersonII=@ContactPersonII,ContactPersNoII=@ContactPersonContII,ContactPerEmailII=@ContactPersonEmailII,ContactNo=@ContactNo,AccntManager=@AccountManger,AccManagerEmail=@AccountMangerEmail,Region=@Region,Active=@Active where OrgID=@ERPID", con))
            {
                cmd.Parameters.AddWithValue("@ERPID", txtERPID.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgName", txtCustomerName.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgAddress", txtCustomerAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@OrgLocation", txtLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNo", txtContactno.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonEmail", txtCustomerEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonII", txtcontactpersonII.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonContII", txtContPerII.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactPersonEmailII", txtContPersEmailI.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountManger", txtAccount_Manager.Text.Trim());
                cmd.Parameters.AddWithValue("@AccountMangerEmail", txtAccountManager_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue.Trim());

                cmd.Parameters.AddWithValue("@Active", "1");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteEx")
            {
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //Get the value of column from the DataKeys using the RowIndex.
            //    string branchcode = gvstate.DataKeys[rowIndex].Values["BranchCode"].ToString();
               // txtsoftwarename.Text = gvstate.Rows[rowIndex].Cells[1].Text;
string orgid=GridView1.Rows[rowIndex].Cells[1].Text.Trim();
string orgname=GridView1.Rows[rowIndex].Cells[2].Text.Trim();
                try
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM AMS_Organization_Master WHERE OrgId = @OrgID", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@OrgID", orgid);
                        //    cmd.Parameters.AddWithValue("@Option", "Delete");
                            cmd.CommandTimeout = 180;
                            cmd.ExecuteNonQuery();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#myModal').modal('show');");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
                            lblsuccess.ForeColor = System.Drawing.Color.Green;
                            lblsuccess.Text = orgname + " Deleted successfully";
                         //   txtsoftwarename.Text = "";
                            con.Close();
                           FillCustomers();

                        }
                    }
                    //
                }
                catch (Exception ex)
                {
                   // msg.ReportError(ex.Message);

                }

            }
        if (e.CommandName == "Select")
        {
              int rowIndex = Convert.ToInt32(e.CommandArgument);
            //  //Get the value of column from the DataKeys using the RowIndex.
            //txtERPID.Text = GridView1.DataKeys[rowIndex].Values["OrgId"].ToString();
            //ImageButton img = (ImageButton)e.CommandSource as ImageButton;
            //GridViewRow row = img.NamingContainer as GridViewRow;
            //  // ID = gridview100.DataKeys[row.RowIndex].Value.ToString();
   txtERPID.Text = GridView1.Rows[rowIndex].Cells[1].Text.Trim();
            txtCustomerName.Text = GridView1.Rows[rowIndex].Cells[2].Text.Trim();
            txtCustomerAddress.Text = GridView1.Rows[rowIndex].Cells[3].Text.Trim();
       

           txtLocation.Text= GridView1.Rows[rowIndex].Cells[4].Text.Trim();
            if (ddlregion.Items.FindByValue(GridView1.Rows[rowIndex].Cells[13].Text.Trim()) != null)
            {
                ddlregion.SelectedValue = GridView1.Rows[rowIndex].Cells[13].Text.Trim();
            }
            txtContactPerson_Name.Text = GridView1.Rows[rowIndex].Cells[5].Text.Trim();
            txtContactno.Text = GridView1.Rows[rowIndex].Cells[6].Text.Trim();
            txtCustomerEmail.Text = GridView1.Rows[rowIndex].Cells[7].Text.Trim();
            txtcontactpersonII.Text = GridView1.Rows[rowIndex].Cells[8].Text.Trim();
            txtContPerII.Text = GridView1.Rows[rowIndex].Cells[9].Text.Trim();
            txtContPersEmailI.Text= GridView1.Rows[rowIndex].Cells[10].Text.Trim();
            txtAccount_Manager.Text= GridView1.Rows[rowIndex].Cells[11].Text.Trim();
            txtAccountManager_Email.Text= GridView1.Rows[rowIndex].Cells[12].Text.Trim();

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
        }
    }
    protected void BindSubjectData()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection sqlCon = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * from AMS_Organization_Master";
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
                cmd.CommandText = "DELETE FROM AMS_Organization_Master WHERE ID = @Id";
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindSubjectData();

    }
}