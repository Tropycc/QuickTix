namespace QuickTix.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public List<Listing>? Listings { get; set; }
    }
}
