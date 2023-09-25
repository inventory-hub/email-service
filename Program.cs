using Azure.Communication.Email;
using EmailService.Models;
using EmailService.Services.EmailSender;
using EmailService.Services.EmailSender.Abstractions;
using EmailService.Services.EmailTemplateFactory;
using EmailService.Services.EmailTemplateFactory.Abstractions;
using EmailService.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services
            .AddTransient<IValidator<InvitationEmailMessage>, InvitationEmailMessageValidator>()
            .AddSingleton(x => new EmailClient(Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING")))
            .AddSingleton<IEmailSender, AzureCommunicationsEmailService>()
            .AddSingleton<IEmailTemplateFactory, FileStorageEmailTemplateFactory>();
    })
    .Build();

host.Run();
