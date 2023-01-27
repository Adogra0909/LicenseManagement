using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "#PCVISOR@DEVELOPER#";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {

            using (SqlCommand cmd = new SqlCommand(" select *  FROM  AMS_vUsers where Frm_UID=@Frm_UID", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Frm_UID", txtuser.Text);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["DomainType"].ToString() == "Domain")
                    {

                        DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["ADConnection"].ToString(), txtuser.Text, txtpassword.Text);
                        try
                        {
                            object obj = entry.NativeObject;
                            DirectorySearcher search = new DirectorySearcher(entry);
                            search.Filter = "(SAMAccountName=" + txtuser.Text + ")";
                            //search.PropertiesToLoad.Add("cn");
                            SearchResult result = search.FindOne();


                            if (null != result)
                            {
                                Session["UserType"] = dr["UserType"].ToString();
                                Session["UserID"] = dr["UserID"].ToString();
                                Session["UserName"] = txtuser.Text;
                                Session["LocationCode"] = dr["loccode"].ToString();
                                Session["Orgid"] = dr["Org_ID"].ToString(); 
                                Response.Redirect("Home.aspx");

                            }
                            else
                            {
                                string message = string.Format("Invalid username and password");
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                                txtuser.Text = "";
                                txtpassword.Text = "";
                            }


                        }
                        catch (Exception ex)
                        {
                            // message = ex.Message;
                            // return false;
                            //throw new Exception("Error authenticating user. " + ex.Message);
                        }

                    }
                    if (dr["DomainType"].ToString() == "NonDomain")
                    {
                        CheckNonDomain();
                    }
                }
                con.Close();
            }
        }


    }
    private void AuthenticateAD(string userName, string password, string domain)
    {

        DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, userName, password);
        try
        {
            object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + userName + ")";
            //search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();


            if (null != result)
            {
                Response.Redirect("Home.aspx");

            }
            else
            {
                string message = string.Format("Invalid username and password");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                txtuser.Text = "";
                txtpassword.Text = "";
            }


        }
        catch (Exception ex)
        {
            // message = ex.Message;
            // return false;
            //throw new Exception("Error authenticating user. " + ex.Message);
        }
    }
    private void CheckNonDomain()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "AMS_UsersPRC";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Frm_UID", txtuser.Text);
                    cmd.Parameters.AddWithValue("@Pass", Encrypt(txtpassword.Text));
                    cmd.Parameters.AddWithValue("@Option", "UserLogin");
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        //string SessionUserType = ;                      
                        Session["LocationCode"] = dr["LocCode"].ToString();
                        Session["UserType"] = dr["UserType"].ToString();
                        Session["UserID"] = dr["UserID"].ToString();
                        Session["UserName"] = txtuser.Text;
                        Session["Orgid"] = dr["Org_ID"].ToString();
                        if (dr["UserType"].ToString() == "HR")
                        {
                            Response.Redirect("HomeHR.aspx");
                        }
                        else
                        {
                            Response.Redirect("Home.aspx");
                        }

                    }
                    else
                    {
                        string message = string.Format("Invalid username and password");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
                        txtuser.Text = "";
                        txtpassword.Text = "";

                    }
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            string message = string.Format("Message: {0}\\n\\n", ex.Message);
            message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
        }
    }

}