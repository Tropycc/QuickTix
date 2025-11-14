using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTix.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerEmail { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int ListingId { get; set; }
        public List<Listing>? Listings { get; set; }

        [NotMapped]
        [Display(Name = "Puchase")]
        public Purchase? Purchases { get; set; }
    }
}
