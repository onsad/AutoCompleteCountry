namespace AutoCompleteCountry.Models
{
    public class CountryViewModel
    {
        public string Name
        {
            get;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    field = "No country";
                }
                else
                {
                    field = value;
                }
            }
        }
        public string CountryCode { get; set; } = string.Empty;
        public string Currency
        {
            get;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    field = "No official currency";
                }
                else
                {
                    field = value;
                }
            }
        }

        public string CapitalCity
        {
            get;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    field = "No official capital city";
                }
                else
                {
                    field = value;
                }
            }
        }
    }
}