using EmailService.Services.Contracts;

namespace EmailService.Services.Abstractions;

public interface IEmailService
{
    Task SendHtmlEmailAsync(EmailData emailData);
}