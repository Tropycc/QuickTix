using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickTix.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Contact Email")]
        [EmailAddress]
        public string ContactInfo { get; set; } = string.Empty;

        public List<Listing>? Listings { get; set; }
    }
}
