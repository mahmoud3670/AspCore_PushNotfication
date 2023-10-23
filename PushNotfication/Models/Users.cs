using System.ComponentModel.DataAnnotations;

namespace PushNotfication.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FmcToken { get; set; } = null!;
        public ICollection<NotificationHistory>? NotificationHistories { get; set; }
    }
}
