namespace EventApi.Models
{
    public partial class User
    {
        public User()
        {
            Tokens = new HashSet<Token>();
            Events = new HashSet<Event>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
