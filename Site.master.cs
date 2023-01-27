using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

public partial class PCVMasterPage : System.Web.UI.MasterPage
{

    errorMessage msg = new errorMessage();
    DataTable allCategories = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            //if (Session["UserType"] != null && Session["UserName"] != null && Session["UserID"].ToString() != null)
            //{
            //string referer = Request.ServerVariables["HTTP_REFERER"];
            //if (string.IsNullOrEmpty(referer))
            //{
            //    Session["UserName"] = null;
            //    Response.Redirect("~/Default.aspx");
            //}

            //string role = Session["UserType"].ToString();
            //Label1.Text =Session["UserName"].ToString();
            //lblUserID.Text= Session["UserID"].ToString();

            //if (Session["UserName"].ToString() != "hsadmin")
            //{

            //}
            //if (Session["UserName"].ToString() == "hsadmin")
            //{
            if (Session["UserType"] != null && Session["UserName"] != null)
            {
                LoadCategories();
                lblUserID.Text = Session["UserName"].ToString();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            //  }
            //}
            //else
            //{
            //    Response.Redirect("~/Default.aspx");
            //}

        }
        catch (Exception ex)
        {
            msg.ReportError1(ex.Message);
        }
    }

    private void LoadCategories()
    {
        if (Session["UserName"].ToString() == "hsadmin")
        {
            allCategories = GetAllCategories();
            rptCategories.DataSource = GetCategories();
            rptCategories.DataBind();
        }
        else
        {
            allCategories = GetAllCategoriesNonAdmin();
            rptCategories.DataSource = GetCategoriesNonAdmin();
            rptCategories.DataBind();
        }
    }
    private DataTable GetCategories()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand selectCommand = new SqlCommand("select  MenuID,MenuName,MenuLocation,ParentID from AMS_Navigation WHERE ParentID = 0  and MenuStatus='Active'", connection);
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
            }
            reader.Close();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }
    private DataTable GetCategoriesNonAdmin()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand selectCommand = new SqlCommand(@"select distinct MR.[MenuID],MR.[MenuName],MR.[MenuLocation],MR.[ParentID],R.[UserName],R.[MenuStatus] from [dbo].[AMS_Navigation] as MR                  join             
AMS_roles as R                      on MR.MenuID = R.MenuID and  MR.MenuName = R.MenuName   where R.UserRole='" + this.Session["UserType"].ToString() + "' and R.MenuStatus='Active' and MR.ParentID=0", connection);
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
            }
            reader.Close();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }
    private DataTable GetAllCategories()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand selectCommand = new SqlCommand("select  MenuID,MenuName,MenuLocation,ParentID from AMS_Navigation where  MenuStatus='Active'", connection);
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
            }
            reader.Close();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }
    private DataTable GetAllCategoriesNonAdmin()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand selectCommand = new SqlCommand(@"select distinct MR.[MenuID],MR.[MenuName],MR.[MenuLocation],MR.[ParentID],R.[UserName],R.[MenuStatus] from [dbo].[AMS_Navigation] as MR                   
join                        AMS_roles as R                           on MR.MenuID = R.MenuID and  MR.MenuName = R.MenuName   where R.UserRole='" + this.Session["UserType"].ToString() + "' and R.MenuStatus='Active'", connection);
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                dt.Load(reader);
            }
            reader.Close();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }
    protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (allCategories != null)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                string ID = drv["MenuID"].ToString();
                DataRow[] rows = allCategories.Select("ParentID=" + ID, "MenuName");
                if (rows.Length > 0)
                {

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<ul class='nav child_menu'>");

                    foreach (var item in rows)
                    {
                        sb.Append("<li><a href=" + item["MenuLocation"] + ">" + item["MenuName"] + "</a></li>");

                    }
                    sb.Append("</ul>");

                    (e.Item.FindControl("ltrlSubMenu") as Literal).Text = sb.ToString();

                }
            }
        }
    }
}
