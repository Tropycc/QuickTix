namespace QuickTix.Models
{
    public class Listing
    {
        //Primary Key
        public int ListingId { get; set; }

        //Foreign Keys
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public DateTime ListingDate { get; set; }

        //Navigation Properties
        public Category? Categories { get; set; }
        public Owner? Owners { get; set; }

    }
}
