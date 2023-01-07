using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Ogani.WebUI.AppCode.Extensions
{
	public static partial class Extension
	{
		public static bool SendEmail(this string email, string message, string subjectText = "Ogani Template")
		{
            try
            {
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(MailboxAddress.Parse("aaliyeva0790@yandex.com"));
                mailMessage.To.Add(MailboxAddress.Parse(email));
                mailMessage.Subject = subjectText.ToString();
                mailMessage.Body = new TextPart(TextFormat.Html) { Text = message.ToString() };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("aaliyeva0790@yandex.com", "1970@veyilA");
                smtp.Send(mailMessage);
                smtp.Disconnect(true);

                return true;
        }
            catch
            {
                return false;
            }
        }
	}
}

