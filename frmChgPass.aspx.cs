using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmChgPass : System.Web.UI.Page
{
    errorMessage msg = new errorMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                if (Session["UserType"] != null && Session["UserName"] != null)
                {
                   
                  
                   
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }

            }
        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);

        }
    }

    protected void btnChgpass_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AMS_UsersPRC", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pass", Encrypt(txtpassword.Text));
                    cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@UserType", Session["UserType"].ToString());
                    cmd.Parameters.AddWithValue("@Option", "UpdateUser");
                    con.Open();
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        lblmessge.ForeColor = System.Drawing.Color.Green;
                        lblmessge.Text = "Password changed successfully";
                    }
                    con.Close();
                    txtpassword.Text = "";
                  
                  
                }
            }

        }
        catch (Exception ex)
        {
            msg.ReportError(ex.Message);
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
}