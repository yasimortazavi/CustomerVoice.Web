using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerVoice.Application.Interfaces.Repositories;
using CustomerVoice.Application.Interfaces.Persistence;
using CustomerVoice.Infrastructure.Repositories;
using CustomerVoice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CustomerVoice.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IComplaintRepository, ComplaintRepository>();

            services.AddScoped<
                IComplaintReferenceDataRepository,
                ComplaintReferenceDataRepository>();
            services.AddScoped<IUnitOfWork>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}