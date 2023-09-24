namespace EmailService.Services.Contracts;

public record EmailData
{
    public required string To { get; init; }
    public required string Subject { get; init; }
    public required string HtmlContent { get; init; }
}