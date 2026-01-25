namespace UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset DOB { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
