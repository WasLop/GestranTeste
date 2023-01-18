using Newtonsoft.Json;

namespace GestranTeste.Models
{
    public class AddProvider
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

    }
}
