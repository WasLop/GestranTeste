namespace GestranTeste.Models
{
    public class AddressFilter
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}
