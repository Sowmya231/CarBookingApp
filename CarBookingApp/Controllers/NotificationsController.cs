using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using NotificationAPI.Data;
//using NotificationAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarBookingApp.Data;
using CarBookingApp.Models;

namespace CarBookingApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/notifications - Send a new notification
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }

        // GET: api/notifications - List all notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/notifications/{id} - View details of a specific notification
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotificationById(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }
    }
}


