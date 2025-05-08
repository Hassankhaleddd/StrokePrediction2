using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrokePrediction.DTOs;
using System;

namespace StrokePrediction.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class RemindersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RemindersController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddReminder([FromBody] ReminderDto dto)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var reminder = new Medication
            {
                UserId = userId,
                Name = dto.MedicineName,
                Time = TimeSpan.Parse(dto.Time),
                IsDaily = dto.IsDaily,
                CreatedAt = DateTime.UtcNow
            };

            _context.Medications.Add(reminder);
            await _context.SaveChangesAsync();

            return Ok("Reminder added successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetReminders()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var reminders = await _context.Medications
                .Where(r => r.UserId == userId)
                .ToListAsync();

            var result = reminders.Select(r => new
            {
                r.Name,
                Time = r.Time.ToString(@"hh\:mm"),
                r.IsDaily
            });

            return Ok(result);
        }
    }
}
