using ContactForm.Application.Contacts.Commands.Create;
using ContactForm.Application.Contacts.Commands.Delete;
using ContactForm.Application.Contacts.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ContactForm.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateContactCommandHandler, CreateContactCommandHandler>();
            services.AddScoped<IGetUserContactQueryHandler, GetUserContactQueryHandler>();
            services.AddScoped<IDeleteContactCommandHandler, DeleteContactCommandHandler>();

            return services;
        }
    }
}
