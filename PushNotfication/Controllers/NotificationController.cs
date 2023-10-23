using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using PushNotfication.Models;
using SQLitePCL;

namespace PushNotfication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotficationDB _notficationDB;

        public NotificationController(NotficationDB notficationDB)
        {
            _notficationDB=notficationDB;
        }

        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(MessageRequest request)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = request.Title,
                    Body = request.Body
                },
                Data = new Dictionary<string, string>()
                {
                    ["FirstName"] = "John",
                    ["LastName"] = "Doe"
                },
                
            };

            await SendMessage(message, await GetUsers());


            return Ok("Message sent successfully!");
        }
        [HttpPost("setToken")]
        public async Task<IActionResult> SetToken([FromBody] Users user)
        {
            if(await _notficationDB.Users.AsNoTracking().FirstOrDefaultAsync(x => x.FmcToken==user.FmcToken)==null)
            {
                await _notficationDB.Users.AddAsync(new Users() { FmcToken=user.FmcToken });
                await _notficationDB.SaveChangesAsync();
            }
              
            CookieOptions option = new();
            option.Expires = DateTimeOffset.UtcNow.AddMonths(12);
            HttpContext?.Response.Cookies.Append("token", user.FmcToken, option);
            return Ok(new { user.FmcToken });
        }
        private async Task<List<Users>> GetUsers()
        {
            var users =await _notficationDB.Users.Where(x=>!string.IsNullOrEmpty( x.FmcToken)).AsNoTracking().ToListAsync();
            if (users.Any())
                return users;
            return new List<Users>();
        }
        private async Task SendMessage(Message message, List<Users> users)
        {
            var messaging = FirebaseMessaging.DefaultInstance;
          

            foreach (var item in users)
            {
                try
                {
                    message.Token=item.FmcToken;

                    var result = await messaging.SendAsync(message);
                    NotificationHistory history = new()
                    {
                        Message=message.Notification.Body,
                        Title=message.Notification.Title,
                        UserId = item.Id
                    };

                    if (!string.IsNullOrEmpty(result))
                    {
                        history.IsResevid= true;
                    }
                    else
                    {
                        history.IsResevid= false;
                    }
                    await _notficationDB.AddAsync(history);
                    await _notficationDB.SaveChangesAsync();
                }
                catch 
                {
                    NotificationHistory history = new()
                    {
                        Message=message.Notification.Body,
                        Title=message.Notification.Title,
                        UserId = item.Id,
                        IsResevid= false
                    };
                    await _notficationDB.AddAsync(history);
                    await _notficationDB.SaveChangesAsync();
                }
               
            }
        }


    }
}
