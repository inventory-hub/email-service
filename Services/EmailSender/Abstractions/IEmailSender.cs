
using EmailService.Services.EmailSender.Contracts;

namespace EmailService.Services.EmailSender.Abstractions;

public interface IEmailSender
{
    Task SendHtmlEmailAsync(EmailData emailData);
}