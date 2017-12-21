using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ERPPlugIn.Class
{
    public class SendMail
    {
        public static string ExecuteSendMail(string _smtpIP, decimal _smtpPort, string _fromUser, string _fromPassword, string _mailList, string ccList, string _subject, string _body, string _sendFileName, string _isSendMailSuccess)
        {
            System.Net.WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                MailAddress fromAddress = new MailAddress(_fromUser);
                message.From = fromAddress;
                //收件人
                if (_mailList.IndexOf(";") != -1)
                {
                    string[] sArray = _mailList.Split(';');
                    foreach (string mailAddress in sArray)
                    {
                        if (mailAddress.Trim() != string.Empty)
                        {

                            message.To.Add(mailAddress);
                        }
                    }
                }
                else
                {
                    message.To.Add(_mailList);
                }
                //抄送
                if (ccList != "")
                {
                    if (ccList.IndexOf(";") != -1)
                    {
                        string[] sArray = ccList.Split(';');
                        foreach (string mailAddress in sArray)
                        {
                            if (mailAddress.Trim() != string.Empty)
                            {

                                message.CC.Add(mailAddress);
                            }
                        }
                    }
                    else
                    {
                        message.CC.Add(ccList);
                    }
                }

                //增加附件
                if (_sendFileName != string.Empty)
                {
                    message.Attachments.Add(new System.Net.Mail.Attachment(_sendFileName));
                }
                message.Subject = _subject;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = _body;
                smtpClient.Host = _smtpIP;
                smtpClient.Port = Convert.ToInt32(_smtpPort);
                message.Priority = MailPriority.Normal;
                smtpClient.Credentials = new System.Net.NetworkCredential(_fromUser, _fromPassword);
                smtpClient.Send(message);
                _isSendMailSuccess = "Successful";
                message.Dispose();
            }
            catch (Exception ex)
            {
                _isSendMailSuccess = ex.Message;
            }
            return _isSendMailSuccess;
        }
    }
}