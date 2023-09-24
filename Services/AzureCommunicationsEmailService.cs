namespace EmailService.Services;

using Azure;
using Azure.Communication.Email;
using EmailService.Services.Abstractions;
using EmailService.Services.Contracts;

public class AzureCommunicationsEmailService : IEmailService
{
    private readonly EmailClient _emailClient;

    public AzureCommunicationsEmailService(EmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public async Task SendHtmlEmailAsync(EmailData emailData)
    {
        EmailMessage emailMessage = new(
            senderAddress: "",
            recipientAddress: emailData.To,
            content: new EmailContent(emailData.Subject)
            {
                Html = emailData.HtmlContent
            }
        );

        await _emailClient.SendAsync(
            WaitUntil.Completed,
            emailMessage
        );
    }
}