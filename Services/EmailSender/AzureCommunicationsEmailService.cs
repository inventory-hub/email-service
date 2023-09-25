namespace EmailService.Services.EmailSender;

using Azure;
using Azure.Communication.Email;
using EmailService.Services.EmailSender.Abstractions;
using EmailService.Services.EmailSender.Contracts;
using Microsoft.Extensions.Configuration;

public class AzureCommunicationsEmailService : IEmailSender
{
    private readonly EmailClient _emailClient;
    private readonly string _senderAddress;

    public AzureCommunicationsEmailService(EmailClient emailClient, IConfiguration configuration)
    {
        _emailClient = emailClient;
        _senderAddress = configuration.GetValue<string>("SENDER_ADDRESS");
    }

    public async Task SendHtmlEmailAsync(EmailData emailData)
    {
        EmailMessage emailMessage = new(
            senderAddress: _senderAddress,
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