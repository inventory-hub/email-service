namespace EmailService.Models;

public record InvitationEmailMessage(
    string To,
    string Token,
    string CallbackUrl
);