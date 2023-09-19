using Appointment.Data;
using Appointment.Models;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace Appointment.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILog _logger;

        public AppointmentController(AppointmentDbContext context, UserManager<IdentityUser> userManager, ILog logger)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var userAppointment = await _context.Appointment
                    .Where(a => a.UserId == user.Id)
                    .ToListAsync();

                ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                return View(userAppointment);

            }

            return Problem("User not found");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        // Log detailed information about the ModelState error
                        _logger.Error($"Property: {entry.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var userData = await _context.Appointment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'Appointment'  is null.");
            }

            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
