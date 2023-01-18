using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GestranTeste.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public int ProviderId { get; set; }
        public GestranProvider Provider { get; set; }
    }
}
