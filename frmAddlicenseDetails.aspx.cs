using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddlicenseDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Cleartextbox();
            BindSubjectData();
            FillProducts();
            //AddDefaultFirstRecord();
        }

    }
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

                            //DropDownList1.DataSource = dt;
                            //DropDownList1.DataTextField = "ProductName";
                            //DropDownList1.DataValueField = "ProductID";
                            //DropDownList1.DataBind();

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void Cleartextbox()
    {

        txtLicense.Text = string.Empty;
        this.txtExpiry.Text = string.Empty;
        this.txtRemarks.Text = string.Empty;
        this.txtSDadmin.Text = string.Empty;
        this.txtSdcount.Text = string.Empty;
        this.txtsdtech.Text = string.Empty;
        this.txtStart.Text = string.Empty;
        this.txtVSACount.Text = string.Empty;


    }





    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        WebClient req = new WebClient();
        HttpResponse response = HttpContext.Current.Response;
        string filePath = e.CommandArgument.ToString();
        response.Clear();
        response.ClearContent();
        response.ClearHeaders();
        response.Buffer = true;
        response.AddHeader("Content-Disposition", "attachment;filename=PO/Invoice Attachment.pdf");
        byte[] data = req.DownloadData(Server.MapPath(filePath));
        response.BinaryWrite(data);
        response.End();
    }
    protected void BindSubjectData()
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection sqlCon = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(@"Select * from AMS_License_Master"))
            {

                cmd.Connection = sqlCon;
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gvFiles.DataSource = dt;
                    gvFiles.DataBind();
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    gvFiles.DataSource = dt;
                    gvFiles.DataBind();
                    gvFiles.Rows[0].Visible = false;
                }
                sqlCon.Close();
            }
        }
    }
    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    bool IsDeleted = false;
    //    //getting key value, row id
    //    string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    //    int SubjectID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
    //    //getting row field subjectname
    //    Label SubjectName = (Label)GridView1.Rows[e.RowIndex].FindControl("lblNumber");
    //    using (SqlConnection sqlCon = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = "DELETE FROM AMS_Organization_Master WHERE ID = @Id";
    //            cmd.Parameters.AddWithValue("@Id", SubjectID);
    //            cmd.Connection = sqlCon;
    //            sqlCon.Open();
    //            IsDeleted = cmd.ExecuteNonQuery() > 0;
    //            sqlCon.Close();
    //        }
    //    }
    //    if (IsDeleted)
    //    {
    //        lblMsg.Text = "'" + SubjectName.Text + "' item details has been deleted successfully!";
    //        lblMsg.ForeColor = System.Drawing.Color.Green;
    //       BindSubjectData();
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Error while deleting '" + SubjectName.Text + "' item details";
    //        lblMsg.ForeColor = System.Drawing.Color.Red;
    //    }
    //}


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        // AddNewRecordRowToGrid();
        btnSave.Visible = true;

    }
    protected void InvoiceUpload(string IvoiceFileName, string Invoiceextension, string InvoiceFilePath)
    {
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO AMS_invoice_data(License,InvoiceName
                  ,InvoiceFileType
                  ,InvoiceFilePath) VALUES (@license,@Name, @ContentType, @Data)", con);
            //   cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@license", txtLicense.Text);
            cmd.Parameters.AddWithValue("@Name", IvoiceFileName);
            //  cmd.Parameters.AddWithValue("@FileSize", FileSize);
            cmd.Parameters.AddWithValue("@ContentType", GetFileExtension(Invoiceextension));
            cmd.Parameters.AddWithValue("@Data", InvoiceFilePath);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                lblMsg.Text = "File Uploaded Successfully ";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindSubjectData();
            }
        }


    }



    protected void ImgbtnInvoiceUpload_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void ImgbtnQuoteUpload_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ImgbtnSearchUser_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"select License
      ,Name
      ,Type,Data from AMS_invoice_data where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["Type"].ToString();
                    fileName = sdr["Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
    private string GetFileExtension(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".docx":
            case ".doc":
                return "Microsoft Word Document";
            case ".xlsx":
            case ".xls":
                return "Microsoft Excel Document";
            case ".txt":
                return "Text Document";
            case ".jpg":
            case ".png":
                return "Image";
            default:
                return "Unknown";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string IvoiceFileName = string.Empty;
        //string InvoiceFileSize = string.Empty;
        //string Invoiceextension = string.Empty;
        //string InvoiceFilePath = string.Empty;
        if (string.IsNullOrWhiteSpace(txtLicense.Text))
        {
            lblerrorMsg.Text = "Please Enter Required Details.";
            lblerrorMsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            string constr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("AMSsp_License_Master", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PONo", txtPono.Text.Trim());
                    cmd.Parameters.AddWithValue("@License", txtLicense.Text.Trim());
                    cmd.Parameters.AddWithValue("@VSA", txtVSACount.Text.Trim());
                    cmd.Parameters.AddWithValue("@SD", txtSdcount.Text.Trim());
                    cmd.Parameters.AddWithValue("@SD_Admin", txtSDadmin.Text.Trim());
                    cmd.Parameters.AddWithValue("@SD_Tech", txtsdtech.Text.Trim());
                    cmd.Parameters.AddWithValue("@Start", txtStart.Text.Trim());
                    cmd.Parameters.AddWithValue("@Expiry_date", txtExpiry.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductType", ddlProductName.SelectedValue);
                    cmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@Quantity", txtquantity.Text.Trim());
                    cmd.Parameters.AddWithValue("@InvoiceNo", txtinvoice.Text.Trim());
                    cmd.Parameters.AddWithValue("@QuoteNo", txtQuoteNo.Text.Trim());
                    if (FileUploadPO.HasFile == true)
                    {
                        string fileName = string.Empty;
                        string filePath = string.Empty;

                        filePath = Server.MapPath("POAttachment/" + FileUploadPO.FileName);
                        FileUploadPO.SaveAs(filePath);
                        cmd.Parameters.AddWithValue("@PoDocLoctn", "~/POAttachment/" + FileUploadPO.FileName);
                    }
                    //   cmd.Parameters.AddWithValue("@PoDocLoctn", .Text.Trim());

                    if (FileUploadInvoice.HasFile == true)
                    {
                        string fileName = string.Empty;
                        string filePath = string.Empty;

                        filePath = Server.MapPath("InvoiceAttachment/" + FileUploadInvoice.FileName);
						FileUploadInvoice.SaveAs(filePath);
                        cmd.Parameters.AddWithValue("@InvoiceDocLoctn", "~/InvoiceAttachment/" + FileUploadInvoice.FileName);
                    }
                    if (FileUploadQuote.HasFile == true)
                    {
                        string fileName = string.Empty;
                        string filePath = string.Empty;

                        filePath = Server.MapPath("QuoteAttachment/" + FileUploadQuote.FileName);
                        FileUploadPO.SaveAs(filePath);
                        cmd.Parameters.AddWithValue("@QuoteDocLoctn", "~/QuoteAttachment/" + FileUploadQuote.FileName);
                    }

                    cmd.Parameters.AddWithValue("@InsertBy", "1");

                    cmd.Parameters.AddWithValue("@Option", "Insert");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            //if (FileUploadInvoice.HasFile)
            //{
            //    Invoiceextension = Path.GetExtension(FileUploadInvoice.FileName);
            //    IvoiceFileName = FileUploadInvoice.PostedFile.FileName;
            //    InvoiceFileSize = IvoiceFileName.Length.ToString() + " Bytes";
            //    //strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;  
            //    FileUploadInvoice.PostedFile.SaveAs(Server.MapPath(@"~/Application/FileUploads/" + IvoiceFileName.Trim()));
            //    InvoiceFilePath = @"~/Application/FileUploads/" + IvoiceFileName.Trim().ToString();

            //}
            //else
            //{
            //    lblMsg.Text = "Plase upload the file";
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //    return;
            //}
            //  POUpload();
            //  InvoiceUpload(IvoiceFileName, Invoiceextension, InvoiceFilePath);
            //  QuoteUpload();
        }
    }

    protected void TrackLicCount()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("AMS_spLicKeyMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LicKeyCode", txtLicense.Text.Replace(" ", string.Empty));
                    cmd.Parameters.AddWithValue("@LicKeyName", txtLicense.Text);

                    cmd.Parameters.AddWithValue("@Qty", txtquantity.Text);
                    cmd.Parameters.AddWithValue("@Remaining", txtquantity.Text);
                    cmd.Parameters.AddWithValue("@InsertBy", Convert.ToInt32(Session["UserID"]));
                    cmd.Parameters.AddWithValue("@IsActive", '1');
                    cmd.Parameters.AddWithValue("@Option", "Insert");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
        catch (Exception ex)
        {
            // msg.ReportError1(ex.Message);
            // lblsuccess.Text = msg.ms; ;
        }
    }
}