using ContactForm.Application.ExternalServices.Interface;
using ContactForm.Domain.Repository;
using ContactForm.Infrastructure.DB;
using ContactForm.Infrastructure.ExternalServices.Email;
using ContactForm.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactForm.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IEmailService, MockSmtpEmailService>();

            return services;
        }
    }
}
