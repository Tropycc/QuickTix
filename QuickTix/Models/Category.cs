namespace QuickTix.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Listing>? Listings { get; set; }
    }
}
