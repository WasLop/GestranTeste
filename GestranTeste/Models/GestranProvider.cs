using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestranTeste.Models
{
    public class GestranProvider
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public List<Address> AddressesProvider { get; set; }
    }
}
