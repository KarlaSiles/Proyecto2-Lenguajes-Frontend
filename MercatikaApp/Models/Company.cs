namespace MercatikaApp.Models
{
    public class Company
    {
        public int Idsetup { get; set; }
        public double Sale_tax { get; set; }
        public string Name_company { get; set; }
        public string Address_company { get; set; }
        public string City_company { get; set; }
        public string State_or_province { get; set; }
        public int Zip_code_company { get; set; }
        public string Country_company { get; set; }
        public int Phone_company { get; set; }
        public int Fax_number_company { get; set; }
        public string Payments_terms { get; set; }
        public string Message { get; set; }

        public Company() { }

        public Company(int idsetup, double sale_tax, string name_company, string address_company,
                     string city_company, string state_or_province, int zip_code_company,
                     string country_company, int phone_company, int fax_number_company,
                     string payments_terms, string message)
        {
            Idsetup = idsetup;
            Sale_tax = sale_tax;
            Name_company = name_company;
            Address_company = address_company;
            City_company = city_company;
            State_or_province = state_or_province;
            Zip_code_company = zip_code_company;
            Country_company = country_company;
            Phone_company = phone_company;
            Fax_number_company = fax_number_company;
            Payments_terms = payments_terms;
            Message = message;
        }
    }
}