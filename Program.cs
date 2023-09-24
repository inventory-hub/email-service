using EmailService.Models;
using EmailService.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddTransient<IValidator<InvitationEmailMessage>, InvitationEmailMessageValidator>();
    })
    .Build();

host.Run();
