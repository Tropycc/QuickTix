using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTix.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string BuyerName { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PricePerTicket { get; set; }

        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string BuyerEmail { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        // Foreign Key to Listing
        public int ListingId { get; set; }

        // Navigation property (one purchase → one listing)
        [ForeignKey("ListingId")]
        public Listing? Listing { get; set; }
    }
}
