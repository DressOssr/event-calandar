namespace EventApi.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual User User { get; set; }
    }
}
