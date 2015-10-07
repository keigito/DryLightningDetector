using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace DryLightning.Services
{
    public class SendEmail
    {
        // A method to send a dry lightning alert 
        public async Task SendEmailAsync(string email, string userLocation)
        {
            SendGridMessage mail = new SendGridMessage();

            mail.From = new MailAddress("keigito@gmail.com");
            mail.AddTo(email);
            mail.Subject = "Dry lightning alert";
            mail.Text = "A dry lightning was detected near the specified location (" + userLocation + "). Check your local weather map, and the lightning strike map (http://www.lightningmaps.org/realtime), grab your gears, and go!" + System.Environment.NewLine + System.Environment.NewLine + "Please take all necessary safety precautions to protect yourself." + System.Environment.NewLine + System.Environment.NewLine + "Stay safe, and happy shooting!";

            Web transportWeb = new Web("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");

            await transportWeb.DeliverAsync(mail);
        }
    }
}