using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTix.Models
{
    public class Listing
    {
        public int ListingId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Owner Number")]
        public int OwnerId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Listing Date")]
        public DateTime ListingDate { get; set; }

        [Display(Name = "Photo File Name")]
        public string? PhotoFileName { get; set; }

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? Photo { get; set; }

        [NotMapped]
        public string? ExistingPhotoPath { get; set; }

        // Navigation Properties
        public Category? Categories { get; set; }
        public Owner? Owners { get; set; }
    }
}
