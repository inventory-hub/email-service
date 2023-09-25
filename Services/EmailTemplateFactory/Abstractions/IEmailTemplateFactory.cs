namespace EmailService.Services.EmailTemplateFactory.Abstractions;

public interface IEmailTemplateFactory
{
    Task<string> GetHtmlTemplateAsync(string templateName, IDictionary<string, string> templateData);
}