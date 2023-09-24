namespace EmailService.Validators;

using FluentValidation;
using EmailService.Models;

public class InvitationEmailMessageValidator : AbstractValidator<InvitationEmailMessage>
{
    public InvitationEmailMessageValidator()
    {
        RuleFor(x => x.To).NotEmpty().EmailAddress();
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.CallbackUrl).NotEmpty().Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute));
    }
}