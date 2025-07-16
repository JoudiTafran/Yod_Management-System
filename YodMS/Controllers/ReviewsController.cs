using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YodMS.Extensions;
using YodMS.Models;
using YodMS.Models.DataBase_Manager;

namespace YodMS.Controllers
{
    [Authorize(Policy = "FinanceReview")]
    [Route("Documents/{docId:int}/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _ctx;

        public ReviewsController(AppDbContext db, IHttpContextAccessor ctx)
            => (_db, _ctx) = (db, ctx);

        // POST: /Documents/5/Reviews
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int docId, string decision, string? comment)
        {
            var doc = await _db.Documents.FindAsync(docId);
            if (doc == null) return NotFound();

            int reviewerId = _ctx.HttpContext!.User.GetUserId();

            var existing = await _db.Reviews
                .FirstOrDefaultAsync(r => r.DocId == docId && r.ReviewerUserId == reviewerId);

            if (existing != null)             // منع التكرار
                _db.Reviews.Remove(existing);

            var review = new Reviews
            {
                DocId = docId,
                ReviewerUserId = reviewerId,
                Decision = decision,
                Comment = comment,
                ReviewedAt = DateTime.UtcNow
            };
            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            // حساب النتيجة النهائية
            var three = await _db.Reviews.Where(r => r.DocId == docId).ToListAsync();
            if (three.Count == 3)
            {
                doc.Status = three.Any(r => r.Decision == "Reject") ? "Rejected" : "Approved";
                await _db.SaveChangesAsync();
                // TODO: استدعاء خدمة واتساب لإرسال إشعار
            }

            return RedirectToAction("Index", "Documents", new { userId = doc.OwnerUserId });
        }
    }

}
