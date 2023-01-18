namespace GestranTeste.Models
{
    public class ProviderFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
    }
}
