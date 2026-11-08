using CustomerVoice.Application.Interfaces.Services;
using CustomerVoice.Application.Services;
using CustomerVoice.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace CustomerVoice.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IComplaintService, ComplaintService>();

            services.AddValidatorsFromAssemblyContaining<CreateComplaintValidator>();

            return services;
        }
    }
}
