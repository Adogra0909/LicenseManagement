using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for EmailBLL
/// </summary>
public class EmailBLL
{

    /// <summary>
    /// This helper class sends an email message using the System.Net.Mail namespace
    /// </summary>
    /// <param name="fromEmail">Sender email address</param>
    /// <param name="toEmail">Recipient email address</param>
    /// <param name="bcc">Blind carbon copy email address</param>
    /// <param name="cc">Carbon copy email address</param>
    /// <param name="subject">Subject of the email message</param>
    /// <param name="body">Body of the email message</param>
    /// <param name="attachment">File to attach</param>

    //List<string> attachmentFullPath
    // public static void SendMailMessage(string toEmail, string fromEmail, string bcc, string cc, string subject, string body)
    public static void SendMailMessage(string toEmail, string fromEmail, string bcc, string cc, string subject, string body)
    {
        //create the MailMessage object
        MailMessage mMailMessage = new MailMessage();

        //set the sender address of the mail message
        if (!string.IsNullOrEmpty(fromEmail))
        {
            mMailMessage.From = new MailAddress("resource.notification@hisysmc.com");
        }

        //set the recipient address of the mail message
        mMailMessage.To.Add(new MailAddress(toEmail));

        //set the blind carbon copy address
        if (!string.IsNullOrEmpty(bcc))
        {
            mMailMessage.Bcc.Add(new MailAddress(bcc));
        }

        //set the carbon copy address
        if (!string.IsNullOrEmpty(cc))
        {
            mMailMessage.CC.Add(new MailAddress(cc));
        }

        //set the subject of the mail message
        if (!string.IsNullOrEmpty(subject))
        {
            mMailMessage.Subject = subject;

        }
        else
        {
            mMailMessage.Subject = "Customer Assignment Notification";
        }

        //set the body of the mail message
        mMailMessage.Body = body;

        //set the format of the mail message body
        mMailMessage.IsBodyHtml = true;

        //set the priority
        mMailMessage.Priority = MailPriority.Normal;

        ////////add any attachments from the filesystem
        //foreach (var attachmentPath in attachmentFullPath)
        //{
        //    Attachment mailAttachment = new Attachment(attachmentPath);
        //    mMailMessage.Attachments.Add(mailAttachment);
        //}

        //create the SmtpClient instance
        SmtpClient mSmtpClient = new SmtpClient();
        //mSmtpClient.Host = "smtp.gmail.com";
        mSmtpClient.Host = "outlook.office365.com";
        System.Net.NetworkCredential ntwd = new NetworkCredential();


        ntwd.UserName = "resource.notification@hisysmc.com"; //Your Email ID  
        ntwd.Password = "zxc@2018"; // Your Password  
        mSmtpClient.UseDefaultCredentials = false;
        mSmtpClient.Credentials = ntwd;
        mSmtpClient.Port = 587;
        mSmtpClient.EnableSsl = true;
        //send the mail message
        mSmtpClient.Send(mMailMessage);
    }

    /// <summary>
    /// Determines whether an email address is valid.
    /// </summary>
    /// <param name="emailAddress">The email address to validate.</param>
    /// <returns>
    /// 	<c>true</c> if the email address is valid; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidEmailAddress(string emailAddress)
    {
        // An empty or null string is not valid
        if (String.IsNullOrEmpty(emailAddress))
        {
            return (false);
        }

        // Regular expression to match valid email address
        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        // Match the email address using a regular expression
        Regex re = new Regex(emailRegex);
        if (re.IsMatch(emailAddress))
            return (true);
        else
            return (false);
    }

}

