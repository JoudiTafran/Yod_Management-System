using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using YodMS.Extensions;
using YodMS.Models.DataBase_Manager;
using Document = YodMS.Models.Documents;

namespace YodMS.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _ctx;

        public DocumentsController(AppDbContext db, IHttpContextAccessor ctx)
            => (_db, _ctx) = (db, ctx);

        // GET: /Documents?userId=5
        public async Task<IActionResult> Index(int? userId)
        {
            int targetId = userId ?? _ctx.HttpContext!.User.GetUserId();
            var docs = await _db.Documents
                                .Where(d => d.OwnerUserId == targetId)
                                .OrderByDescending(d => d.CreatedAt)
                                .ToListAsync();

            ViewBag.TargetUserId = targetId;
            ViewBag.IsOwner = targetId == _ctx.HttpContext!.User.GetUserId();
            return View(docs);
        }

        // GET: /Documents/Create
        [Authorize(Policy = "OwnDocWrite")]
        public IActionResult Create() => View();

        // POST: /Documents/Create
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "OwnDocWrite")]
        public async Task<IActionResult> Create(Document vm, IFormFile file)
        {
            if (!ModelState.IsValid) return View(vm);

            // حفظ الملف اختياري
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine("uploads", file.FileName);
                using var stream = System.IO.File.Create(path);
                await file.CopyToAsync(stream);
                vm.FilePath = path;
            }

            vm.OwnerUserId = _ctx.HttpContext!.User.GetUserId();
            vm.Status = "Under Review";
            vm.CreatedAt = DateTime.UtcNow;

            _db.Add(vm);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Documents/Edit/5
        [Authorize(Policy = "OwnDocWrite")]
        public async Task<IActionResult> Edit(int id)
        {
            var doc = await _db.Documents.FindAsync(id);
            if (doc == null) return NotFound();

            // السماح للمالك أو الأمين العام فقط
            var isOwner = doc.OwnerUserId == _ctx.HttpContext!.User.GetUserId();
            if (!isOwner && !User.IsInRole("Secretary-General"))
                return Forbid();

            return View(doc);
        }

        // POST: /Documents/Edit/5
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "OwnDocWrite")]
        public async Task<IActionResult> Edit(int id, Document updated, IFormFile file)
        {
            var doc = await _db.Documents.FindAsync(id);
            if (doc == null) return NotFound();

            var isOwner = doc.OwnerUserId == _ctx.HttpContext!.User.GetUserId();
            if (!isOwner && !User.IsInRole("Secretary-General"))
                return Forbid();

            doc.Title = updated.Title;
            doc.DocTypeId = updated.DocTypeId;
            doc.EventDate = updated.EventDate;

            if (file != null && file.Length > 0)
            {
                var path = Path.Combine("uploads", file.FileName);
                using var stream = System.IO.File.Create(path);
                await file.CopyToAsync(stream);
                doc.FilePath = path;
            }

            _db.Update(doc);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Documents/Delete/5
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "OwnDocWrite")]
        public async Task<IActionResult> Delete(int id)
        {
            var doc = await _db.Documents.FindAsync(id);
            if (doc == null) return NotFound();

            var isOwner = doc.OwnerUserId == _ctx.HttpContext!.User.GetUserId();
            if (!isOwner && !User.IsInRole("Secretary-General"))
                return Forbid();

            _db.Documents.Remove(doc);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
