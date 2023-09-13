using System;
using System.Net.Mail;
using System.Net;
using Experimental.System.Messaging;

namespace CommonLayer.Model
{
    public class MsmqModel
    {
        MessageQueue fundoQueue = new MessageQueue();
        public void SendData2Queue(string token)
        {
            fundoQueue.Path = @".\private$\Token";
            if (!MessageQueue.Exists(fundoQueue.Path))
            {
                MessageQueue.Create(fundoQueue.Path);
            }
            fundoQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            fundoQueue.ReceiveCompleted += FundoQueue_ReceiveCompleted;
            fundoQueue.Send(token);
            fundoQueue.BeginReceive();
            fundoQueue.Close();
        }
        public void FundoQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = fundoQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string body = $"<a style = \"color:#00802b; text-decoration: none; font-size:20px;\" href='http://localhost:4200/resetpassword/{token}'>Click me</a>\n";
                string subject = "Token for reset password";
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("somubridgelabz@gmail.com", "crauayaudvfyhumw"),
                    EnableSsl = true,
                };
                smtp.Send("somubridgelabz@gmail.com", "somubridgelabz@gmail.com", subject, body);
                fundoQueue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                throw ex;
            }
        }

    }
}
