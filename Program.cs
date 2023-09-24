using Azure.Communication.Email;
using EmailService.Models;
using EmailService.Validators;
using FluentValidation;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddTransient<IValidator<InvitationEmailMessage>, InvitationEmailMessageValidator>();
        services.AddSingleton(x => new EmailClient(Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING")));
    })
    .Build();

host.Run();
