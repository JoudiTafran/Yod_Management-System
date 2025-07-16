using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YodMS.Extensions;          // ‎GetUserId()
using YodMS.Models;
using YodMS.Models.DataBase_Manager;

namespace YodMS.Controllers
{
    // كل عضو إداري (ما عدا هيئة الرقابة) يستطيع التصويت
    [Authorize(Policy = "CanVote")]
    // نجعل المسار متداخلًا داخل جلسة التصويت
    // 例: /VoteSessions/3/Votes
    [Route("VoteSessions/{voteSessionId:int}/[controller]")]
    public class VotesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _ctx;

        public VotesController(AppDbContext db, IHttpContextAccessor ctx)
            => (_db, _ctx) = (db, ctx);

        // -----------------------------------------------------------
        // GET: /VoteSessions/3/Votes
        // عرض كل الأصوات في الجلسة (اختياري ‑ يمكن الاكتفاء بعرضها في Details)
        // -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Index(int voteSessionId)
        {
            var session = await _db.VoteSessions
                                   .Include(v => v.Votes)
                                   .ThenInclude(v => v.VoterUser)
                                   .FirstOrDefaultAsync(v => v.VoteSessionId == voteSessionId);

            if (session is null) return NotFound();

            return View(session);   // مرّر الجلسة في الـ View لعرض الأصوات
        }

        // -----------------------------------------------------------
        // POST: /VoteSessions/3/Votes
        // الإدلاء بالصوت
        // -----------------------------------------------------------
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CastVote(
            int voteSessionId,
            [Bind("Choice,Comment")] Votes vm)   // نحصل فقط على Choice/Comment من النموذج
        {
            // 1) تأكّد أن الجلسة موجودة ومفتوحة
            var session = await _db.VoteSessions
                                   .Include(s => s.Votes)
                                   .FirstOrDefaultAsync(s => s.VoteSessionId == voteSessionId);

            if (session is null) return NotFound();
            if (session.Status != "Open")        // لا يُسمح بالتصويت بعد الإغلاق
            {
                ModelState.AddModelError(string.Empty, "جلسة التصويت مغلقة.");
                return RedirectToAction("Details", "VoteSessions", new { id = voteSessionId });
            }

            // 2) امنع العضو نفسه من التصويت مرتين
            int userId = _ctx.HttpContext!.User.GetUserId();
            bool alreadyVoted = session.Votes.Any(v => v.VoterUserId == userId);
            if (alreadyVoted)
            {
                ModelState.AddModelError(string.Empty, "لقد أدليت بصوتك مسبقًا.");
                return RedirectToAction("Details", "VoteSessions", new { id = voteSessionId });
            }

            // 3) أنشئ الصوت الجديد
            var vote = new Votes
            {
                VoteSessionId = voteSessionId,
                VoterUserId = userId,
                Choice = vm.Choice,          // “Yes” أو “No” أو “Abstain” مثلًا
                Comment = vm.Comment,
                VotedAt = DateTime.UtcNow
            };

            _db.Votes.Add(vote);
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", "VoteSessions", new { id = voteSessionId });
        }
    }
}
