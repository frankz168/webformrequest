using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Net.Mail;

public static class MailHelper
{
    public static ExecutionResult LastExecutionResult = new ExecutionResult();

    public static ExecutionResult SendMail(string MailTo, string MailSubject, string MailMessage, string MailAttachmentPath, string MailFrom, string MailSmtp, string MailSmtpPort, int MailTimeOut, string SenderMailAddress, string SenderMailPassword)
    {
        ExecutionResult ExRe = new ExecutionResult();

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SenderMailAddress);
            mail.To.Add(MailTo);
            mail.Subject = MailSubject;
            mail.Body = MailMessage;
            mail.IsBodyHtml = true;
            if (MailAttachmentPath != "")
                mail.Attachments.Add(new Attachment(MailAttachmentPath));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = MailSmtp;
            smtp.Port = Convert.ToInt16(MailSmtpPort);
            smtp.Timeout = MailTimeOut;
            smtp.UseDefaultCredentials = true;

            smtp.EnableSsl = (smtp.Port == 587);
            smtp.Credentials = new System.Net.NetworkCredential(SenderMailAddress, SenderMailPassword);
            smtp.Send(mail);

            ExRe.Result = RESULTTYPE.SUCCESS;
        }
        catch (Exception ex)
        {
            ExRe.MessageException = ex.Message;
            ExRe.Result = RESULTTYPE.FAILED;
        }

        LastExecutionResult = ExRe;
        return ExRe;
    }

}
