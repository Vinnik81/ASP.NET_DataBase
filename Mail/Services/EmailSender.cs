using MailKit.Net.Smtp;
using MimeKit;

namespace Mail.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendAsync(string email, string subject, string content)
        {
             var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Eshop Admin", "postmaster@sandbox74a50547558141b98d0fb6f0ef74919d.mailgun.org"));
            emailMessage.To.Add(new MailboxAddress("Eshop Customer", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = content
            };


            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mailgun.org", 587,false);
                await client.AuthenticateAsync("postmaster@sandbox74a50547558141b98d0fb6f0ef74919d.mailgun.org", "4ce624034462fba57940bba7bfd9b800-6d8d428c-85da1642");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }


            //            [16:45] Артёмов Артём Владимирович

            //Grab your SMTP credentials:

            //            SMTP hostname: smtp.mailgun.org
            //            Port: 587(recommended)
            //Username: postmaster @sandbox81068146947148e4969229e119014551.mailgun.org
            //Default password:

            //            2f1c5c09e4e070397e095f2a5c8d31be - e5475b88 - 8986cc2a
        }
    }
}
