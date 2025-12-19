using ContactForm.Application.ExternalServices.DTOs;
using ContactForm.Application.ExternalServices.Interface;
using ContactForm.Infrastructure.ExternalServices.JsonPlaceHolder.DTOs;
using System.Net.Http.Json;

namespace ContactForm.Infrastructure.ExternalServices.JsonPlaceHolder
{
    public class JsonPlaceholderUserService(HttpClient http) : IUserEnrichmentService
    {
        public async Task<UserEnrichmentResult?> GetByEmailAsync(string email)
        {
            var users = await http.GetFromJsonAsync<List<JsonUser>>($"users?email={email}");
            var user = users?.FirstOrDefault();

            if (user == null) return null;

            return new UserEnrichmentResult
            {
                Phone = user.Phone,
                Website = user.Website,
                Company = user.Company.Name,
                Address = $"{user.Address.Street}, {user.Address.City}"
            };
        }
    }
}
