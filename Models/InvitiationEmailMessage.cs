namespace EmailService.Models;

public record InvitationEmailMessage(
    string To,
    string FullName,
    string Token,
    string CallbackUrl
);