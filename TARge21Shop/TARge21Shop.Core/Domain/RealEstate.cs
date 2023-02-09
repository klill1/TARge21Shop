using System.ComponentModel.DataAnnotations;

namespace TARge21Shop.Core.Domain
{
    public class RealEstate
    {
        [Key]
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public double Size { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }
        public int RoomCount { get; set; }

        public IEnumerable<FileToApi> FileToApis { get; set; } = new List<FileToApi>();

        // only in database
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
