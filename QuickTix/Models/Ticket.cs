namespace QuickTix.Models
{
    public class Ticket
    {
        //Primary Key
        public int TicketId { get; set; }
        //Foreign Key
        public int EventId { get; set; }

        //Ticket Owner
        public string Owner { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        //Navigation Property
        public List <Event>? Events { get; set; }

    }
}
