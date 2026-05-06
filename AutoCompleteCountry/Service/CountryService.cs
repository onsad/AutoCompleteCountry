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

        public List<Country> GetCountries(string searchedCountry)
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CountryId, CountryName FROM Country WHERE CountryName LIKE @searchedCountry + '%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchedCountry", searchedCountry);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(new Country() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            return countries;
        }

        public Country GetCountryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CountryName, CountryCode, Currency FROM Country WHERE CountryId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Country
                            {
                                Name = reader.GetString(0),
                                Code = reader.GetString(1),
                                Currency = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return new Country();
        }
    }
}