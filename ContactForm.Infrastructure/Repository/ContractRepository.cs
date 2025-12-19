using ContactForm.Domain.Models;
using ContactForm.Domain.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ContactForm.Infrastructure.Repository
{
    public class ContactRepository(IConfiguration config) : IContactRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<bool> ExistsWithinLastMinuteAsync(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("""
                SELECT COUNT(1)
                FROM Contacts
                WHERE Email = @Email AND CreatedAt >= DATEADD(MINUTE, -1, GETUTCDATE())
            """, conn);

            cmd.Parameters.AddWithValue("@Email", email);
            await conn.OpenAsync();

            return (int)await cmd.ExecuteScalarAsync() > 0;
        }

        public async Task SaveAsync(ContactModel submission)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("""
                INSERT INTO Contacts
                (Id, FirstName, LastName, Email, CreatedAt, Phone, Website, Company, Address)
                VALUES
                (@Id, @FirstName, @LastName, @Email, @CreatedAt, @Phone, @Website, @Company, @Address)
            """, conn);

            cmd.Parameters.AddWithValue("@Id", submission.Id);
            cmd.Parameters.AddWithValue("@FirstName", submission.FirstName);
            cmd.Parameters.AddWithValue("@LastName", submission.LastName);
            cmd.Parameters.AddWithValue("@Email", submission.Email);
            cmd.Parameters.AddWithValue("@CreatedAt", submission.CreatedAt);
            cmd.Parameters.AddWithValue("@Phone", (object?)submission.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Website", (object?)submission.Website ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Company", (object?)submission.Company ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)submission.Address ?? DBNull.Value);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<ContactModel> GetUserContactAsync(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("""
                SELECT Id, FirstName, LastName, Email, CreatedAt, Phone, Website, Company, Address
                FROM Contacts
                WHERE Email = @Email
            """, conn);

            cmd.Parameters.AddWithValue("@Email", email);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ContactModel
                {
                    Id = reader.GetGuid(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    CreatedAt = reader.GetDateTime(4),
                    Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Website = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Company = reader.IsDBNull(7) ? null : reader.GetString(7),
                    Address = reader.IsDBNull(8) ? null : reader.GetString(8)
                };
            }

            return null!;
        }

        public async Task DeleteContactAsync(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("""
                DELETE FROM Contacts
                WHERE Email = @Email
            """, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

    }
}
