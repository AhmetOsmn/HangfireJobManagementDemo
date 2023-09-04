namespace HangfireJobManagementDemo.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
