using System;
using System.Web;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
        this.Session.Abandon();
        this.Session.RemoveAll();
        this.Session.Remove("UserType");
        this.Session.Remove("UserName");
        this.Session.Clear();
        this.Response.Redirect("Default.aspx");
    }
}