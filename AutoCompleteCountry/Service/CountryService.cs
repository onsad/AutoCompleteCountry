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

        public List<string> GetCountries(string searchedCountry)
        {
            List<string> countries = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CountryName FROM Countries WHERE CountryName LIKE @searchedCountry + '%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchedCountry", searchedCountry);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(reader.GetString(0));
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
                string query = "SELECT Id, CountryName FROM Countries WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Country
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return new Country();
        }
    }
}