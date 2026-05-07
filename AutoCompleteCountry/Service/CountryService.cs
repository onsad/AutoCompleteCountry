using AutoCompleteCountry.Models;
using Microsoft.Data.SqlClient;

namespace AutoCompleteCountry.Service
{
    public class CountryService
    {
        private readonly string connectionString;

        public CountryService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string 'DBConnection not found.");
        }

        public async Task<List<Country>> GetCountries(string searchedCountry)
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT CountryId, CountryName FROM Country WHERE CountryName LIKE @searchedCountry + '%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchedCountry", searchedCountry);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            countries.Add(new Country() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            return countries;
        }

        public async Task<Country> GetCountryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT CountryName, CountryCode, Currency, CapitalCity FROM Country WHERE CountryId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Country
                            {
                                Name = reader.GetString(0),
                                Code = reader.GetString(1),
                                Currency = reader.GetString(2),
                                CapitalCity = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return new Country();
        }
    }
}