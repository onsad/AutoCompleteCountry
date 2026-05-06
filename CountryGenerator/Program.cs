using System.Reflection;
using System.Text;
using System.Text.Json;

string jsonFilePath = Path.Combine(
    Environment.CurrentDirectory,
    "Source",
    "countries.json"
);
string jsonString = File.ReadAllText(jsonFilePath);
var countries = JsonSerializer.Deserialize<Class1[]>(jsonString);

var insertSql = new StringBuilder("INSERT INTO Country\r\n  ( CountryName, CountryCode, Currency )\r\nVALUES");

foreach (var country in countries)
{
    var currency = string.Empty;
    var curr = country.currencies.GetType().GetProperties().Where(val => val != null);

    foreach (PropertyInfo pi in country.currencies.GetType().GetProperties())
    {
        object value = pi.GetValue(country.currencies, null);

        if (value != null && !string.IsNullOrEmpty(value.ToString()))
        {
            currency = value.ToString();
        }
    }

    insertSql.AppendLine($"  ( '{country.name.common}', '{country.cca3}', '{currency}' ),");
}


File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "export.sql"), insertSql.ToString());
