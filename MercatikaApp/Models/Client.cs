namespace MercatikaApp.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public string ContractName { get; set; }
        public string ContractLastname { get; set; }
        public string ContractPosition { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public int Phone { get; set; }
        public int FaxNumber { get; set; }
    }
}