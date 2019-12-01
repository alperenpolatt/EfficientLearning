using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
namespace EfLearning.Api.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private const string SendGridKey = "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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@efficientLearning.com", "Efficient Learing"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
