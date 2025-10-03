using System.ComponentModel.DataAnnotations;

namespace QuickTix.Models
{
    public class Listing
    {
        //Primary Key
        public int ListingId { get; set; }

        //Foreign Keys

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Owner Number")]
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Owner Name")]
        public string Owner { get; set; } = string.Empty;

        [Display(Name = "Listing Date")]
        public DateTime ListingDate { get; set; }

        //Navigation Properties
        public Category? Categories { get; set; }
        public Owner? Owners { get; set; }

    }
}
