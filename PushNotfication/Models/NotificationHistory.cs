namespace PushNotfication.Models
{
    public class NotificationHistory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsResevid { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }

    }
}
