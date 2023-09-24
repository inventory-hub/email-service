using EmailService.Models;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmailService.Functions;

public class InvitationEmailHandler
{
    private readonly ILogger _logger;
    private readonly IValidator<InvitationEmailMessage> _validator;

    public InvitationEmailHandler(
        ILoggerFactory loggerFactory,
        IValidator<InvitationEmailMessage> validator
    )
    {
        _logger = loggerFactory.CreateLogger<InvitationEmailHandler>();
        _validator = validator;
    }

    [Function(nameof(InvitationEmailHandler))]
    public void Run([QueueTrigger("invitation-emails")] InvitationEmailMessage invitationEmailMessage)
    {
        _validator.ValidateAndThrow(invitationEmailMessage);
        _logger.LogInformation($"C# Queue trigger function processed: {invitationEmailMessage}");
    }
}
