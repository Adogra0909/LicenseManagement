using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class frmAddUsersRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillUsers();
        }
    }

    private void FillUsers()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select  distinct RoleName  from AMS_Role order by RoleName asc"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        ddlUsers.DataSource = dt;
                        ddlUsers.DataTextField = "RoleName";
                        ddlUsers.DataBind();
                    }
                }
            }
            ddlUsers.Items.Insert(0, new ListItem("--Select Role--", "0"));
        }
    }


    private void FillScopes()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            //using (SqlCommand cmd = new SqlCommand("SELECT MenuID,MenuName,UserName,MenuStatus FROM pcvnp_roletest")
            using (SqlCommand cmd = new SqlCommand("Select  distinct [MenuID],[MenuName]  from AMS_Navigation where MenuID not in (select  MenuID from AMS_roles where UserRole=@UserRole)  order by MenuID asc", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserRole", ddlUsers.SelectedItem.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {


                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvMasterRoles.DataSource = dt;
                        gvMasterRoles.DataBind();
                    }
                }
            }
        }
    }



    protected void btnRoleApply_Click(object sender, EventArgs e)
    {
        int MenuID;
        string strname = string.Empty;
        foreach (GridViewRow gvrow in gvMasterRoles.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                MenuID = Convert.ToInt32(gvMasterRoles.DataKeys[gvrow.RowIndex].Value);

                string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    // using (SqlCommand cmd = new SqlCommand("Update pcvnp_roletest SET MenuStatus='Inactive' where MenuID='" + MenuID + "' "))
                    using (SqlCommand cmd = new SqlCommand("insert into dbo.AMS_roles(MenuID,MenuName,UserName,MenuStatus)values(@MenuID,@MenuName,@UserName,@MenuStatus)", con))
                    {
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
                        cmd.Parameters.AddWithValue("@MenuName", gvrow.Cells[1].Text);
                        cmd.Parameters.AddWithValue("@UserName", ddlUsers.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MenuStatus", "Active");
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            }
        }
        FillScopes();
        FillAllRoles();

    }

    protected void btnMasterRoleApply_Click(object sender, EventArgs e)
    {
        int MenuID;
        foreach (GridViewRow gvrow in gvMasterRoles.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                MenuID = Convert.ToInt32(gvMasterRoles.DataKeys[gvrow.RowIndex].Value);
                string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("insert into dbo.AMS_roles( MenuID,MenuName,UserRole,MenuStatus)values(@MenuID,@MenuName,@UserRole,@MenuStatus)", con))
                    {

                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
                        cmd.Parameters.AddWithValue("@MenuName", gvrow.Cells[1].Text);
                        cmd.Parameters.AddWithValue("@UserRole", ddlUsers.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MenuStatus", "Active");
                        con.Open();
                        cmd.ExecuteNonQuery();
                        FillAllRoles();
                        FillScopes();
                    }
                }
            }
            // else
            // { 
            // //CheckBox Inactive = (CheckBox)gvrow.FindControl("chkSelect");
            // //if (Inactive != null & Inactive.Checked)
            // //{
            //     //MenuID = Convert.ToInt32(gvRoles.DataKeys[gvrow.RowIndex].Value);
            //     string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            //     using (SqlConnection con = new SqlConnection(constr))
            //     {
            //         //using (SqlCommand cmd = new SqlCommand(@"insert into dbo.pcvnp_roletest( MenuName,MenuLocation,ParentID,UserName,MenuStatus)values(@MenuName,@MenuLocation,@ParentID,@UserName,@MenuStatus)"))
            //         using (SqlCommand cmd = new SqlCommand("Update pcv_roles SET MenuStatus='Inctive' where UserName='Admin' and MenuName='" + gvrow.Cells[1].Text + "'", con))
            //         {
            //             //cmd.Parameters.AddWithValue("@MenuName", gvrow.Cells[1].Text);
            //             //cmd.Parameters.AddWithValue("@MenuLocation", gvrow.Cells[2].Text);
            //             //cmd.Parameters.AddWithValue("@ParentID", gvrow.Cells[3].Text);
            //             //cmd.Parameters.AddWithValue("@UserName", gvrow.Cells[4].Text);
            //             //cmd.Parameters.AddWithValue("@MenuStatus", gvrow.Cells[5].Text);
            //             cmd.Parameters.AddWithValue("@MenuName", gvrow.Cells[1].Text);
            //             cmd.Parameters.AddWithValue("@UserName", "Admin");
            //             cmd.Parameters.AddWithValue("@MenuStatus", gvrow.Cells[3].Text);
            //             con.Open();
            //             cmd.ExecuteNonQuery();
            //         }
            //     }
            //}


        }

    }
    protected void gvAllRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleterRole")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int RowIndex = Convert.ToInt32(e.CommandArgument);

            ////Get the value of column from the DataKeys using the RowIndex.
            //int id = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
            //string group = GridView1.DataKeys[rowIndex].Values[1].ToString();

            //int customerId = Convert.ToInt32(gvAllRoles.DataKeys[RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM AMS_roles WHERE ID=@ID "))
                {
                    cmd.Parameters.AddWithValue("@ID", RowIndex);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    FillAllRoles();
                    FillScopes();
                }
            }
        }
    }
    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUsers.SelectedIndex == 0)
        {
            gvMasterRoles.DataSource = null;
            gvMasterRoles.DataBind();

            gvAllRoles.DataSource = null;
            gvAllRoles.DataBind();
        }
        else

        {

            // FillScopes();
            FillAllRoles();
            FillScopes();
        }
    }

    private void FillAllRoles()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {

            using (SqlCommand cmd = new SqlCommand(@"select * from 
AMS_roles  where UserRole  = '" + ddlUsers.SelectedItem + "'"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvAllRoles.DataSource = dt;
                        gvAllRoles.DataBind();
                    }
                }
            }
        }
    }

    protected void btnUpdateRoles_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gvrow in gvAllRoles.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                int ID = Convert.ToInt32(gvAllRoles.DataKeys[gvrow.RowIndex].Value);
                string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("Update dbo.AMS_roles SET MenuStatus=@MenuStatus where ID=@ID and UserName=@UserName ", con))
                    {

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@UserName", ddlUsers.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MenuStatus", "Active");
                        con.Open();
                        cmd.ExecuteNonQuery();

                        FillScopes();
                        FillAllRoles();
                    }
                }
            }
            else

            {
                int ID = Convert.ToInt32(gvAllRoles.DataKeys[gvrow.RowIndex].Value);
                string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {

                    using (SqlCommand cmd = new SqlCommand("Update dbo.AMS_roles SET MenuStatus=@MenuStatus where ID=@ID and UserName=@UserName ", con))
                    {

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@UserName", ddlUsers.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MenuStatus", "Inactive");
                        con.Open();
                        cmd.ExecuteNonQuery();

                        FillScopes();
                        FillAllRoles();
                    }
                }

            }


        }
    }

    protected void btnSaveRole_Click(object sender, EventArgs e)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Insert into AMS_Role (RoleName,InsertBy,InsertDt,IsActive) values(@RoleName,@InsertBy,Getdate(),1)", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@RoleName", txtRoleName.Text.Trim());
                    cmd.Parameters.AddWithValue("@InsertBy", Session["UserName"].ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
