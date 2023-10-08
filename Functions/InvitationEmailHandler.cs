using EmailService.Models;
using EmailService.Services.EmailSender.Abstractions;
using EmailService.Services.EmailSender.Contracts;
using EmailService.Services.EmailTemplateFactory.Abstractions;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmailService.Functions;

public class InvitationEmailHandler
{
    private readonly ILogger _logger;
    private readonly IValidator<InvitationEmailMessage> _validator;
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateFactory _emailTemplateFactory;

    public InvitationEmailHandler(
        ILoggerFactory loggerFactory,
        IValidator<InvitationEmailMessage> validator,
        IEmailSender emailSender,
        IEmailTemplateFactory emailTemplateFactory)
    {
        _logger = loggerFactory.CreateLogger<InvitationEmailHandler>();
        _validator = validator;
        _emailSender = emailSender;
        _emailTemplateFactory = emailTemplateFactory;
    }

    private Task<string> GetHtmlData(InvitationEmailMessage invitationEmailMessage)
    {
        UriBuilder uriBuilder = new(invitationEmailMessage.CallbackUrl)
        {
            Query = $"token={invitationEmailMessage.Token}"
        };
        string invitationUrl = uriBuilder.Uri.ToString();

        return _emailTemplateFactory.GetHtmlTemplateAsync("invitation.html", new Dictionary<string, string>
        {
            { "InvitationUrl", invitationUrl},
            { "FullName", invitationEmailMessage.FullName}
        });
    }

    [Function(nameof(InvitationEmailHandler))]
    public async Task Run([QueueTrigger("invitation-messages")] InvitationEmailMessage invitationEmailMessage)
    {
        var validationResult = await _validator.ValidateAsync(invitationEmailMessage);
        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid invitation email message: {}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }
        try
        {
            string htmlContent = await GetHtmlData(invitationEmailMessage);
            await _emailSender.SendHtmlEmailAsync(new EmailData
            {
                To = invitationEmailMessage.To,
                Subject = "Invitation to Inventory Hub",
                HtmlContent = htmlContent
            });
        }
        catch
        {
            _logger.LogError("Failed to send invitation email to {}", invitationEmailMessage.To);
            throw;
        }
        _logger.LogInformation("Sent invitation email to {}", invitationEmailMessage.To);
    }
}
