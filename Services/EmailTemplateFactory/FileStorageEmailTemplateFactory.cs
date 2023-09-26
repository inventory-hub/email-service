using System.Text;
using System.Text.RegularExpressions;
using EmailService.Services.EmailTemplateFactory.Abstractions;
using Microsoft.Extensions.Logging;

namespace EmailService.Services.EmailTemplateFactory;

public class FileStorageEmailTemplateFactory : IEmailTemplateFactory
{
    private const string _templateDirectory = "./Templates";
    private readonly Dictionary<string, string> _cachedTemplates = new();
    private readonly ILogger<FileStorageEmailTemplateFactory> _logger;

    public FileStorageEmailTemplateFactory(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<FileStorageEmailTemplateFactory>();
    }

    private async Task<string> GetRawTemplate(string templateName)
    {
        string templatePath = Path.Combine(_templateDirectory, templateName);
        if (_cachedTemplates.ContainsKey(templatePath))
        {
            return _cachedTemplates[templatePath];
        }

        try
        {
            string template = await File.ReadAllTextAsync(templatePath);
            _cachedTemplates.Add(templatePath, template);
            return template;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to read template file {templatePath}", templatePath);
            throw;
        }
    }

    public async Task<string> GetHtmlTemplateAsync(string templateName, IDictionary<string, string> templateData)
    {
        string rawTemplate = await GetRawTemplate(templateName);
        string htmlData = templateData
            .Aggregate(rawTemplate, (current, kv) => Regex.Replace(current, $"{{{{\\s*{kv.Key}\\s*}}}}", kv.Value, RegexOptions.IgnoreCase));
        return htmlData;
    }
}