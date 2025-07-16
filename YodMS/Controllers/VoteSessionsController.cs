using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YodMS.Extensions;
using YodMS.Models;
using YodMS.Models.DataBase_Manager;

namespace YodMS.Controllers
{
    [Authorize]
    public class VoteSessionsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _ctx;

        public VoteSessionsController(AppDbContext db, IHttpContextAccessor ctx)
            => (_db, _ctx) = (db, ctx);

        public async Task<IActionResult> Index() =>
            View(await _db.VoteSessions.OrderByDescending(v => v.CreatedAt).ToListAsync());

        [Authorize(Policy = "OpenVoteSession")]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "OpenVoteSession")]
        public async Task<IActionResult> Create(VoteSessions vm)
        {
            if (!ModelState.IsValid) return View(vm);

            vm.CreatedByUserId = _ctx.HttpContext!.User.GetUserId();
            vm.CreatedAt = DateTime.UtcNow;
            vm.Status = "Open";

            _db.VoteSessions.Add(vm);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var session = await _db.VoteSessions
                                   .Include(v => v.Votes)
                                   .ThenInclude(v => v.VoterUser)
                                   .FirstOrDefaultAsync(v => v.VoteSessionId == id);
            if (session == null) return NotFound();
            return View(session);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "OpenVoteSession")]
        public async Task<IActionResult> Close(int id)
        {
            var session = await _db.VoteSessions.FindAsync(id);
            if (session == null) return NotFound();

            session.Status = "Closed";
            session.ClosedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
