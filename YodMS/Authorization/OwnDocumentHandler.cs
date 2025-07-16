using Microsoft.AspNetCore.Authorization;
using YodMS.Extensions;
using YodMS.Models.DataBase_Manager;

namespace YodMS.Authorization
{
    public class OwnDocumentRequirement : IAuthorizationRequirement { }

    public class OwnDocumentHandler : AuthorizationHandler<OwnDocumentRequirement, int>
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _ctx;

        public OwnDocumentHandler(AppDbContext db, IHttpContextAccessor ctx)
            => (_db, _ctx) = (db, ctx);

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OwnDocumentRequirement requirement,
                                                       int resource)
        {
            var userId = _ctx.HttpContext!.User.GetUserId();
            bool isOwner = _db.Documents.Any(d => d.DocId == resource && d.OwnerUserId == userId);
            if (isOwner || context.User.IsInRole("Secretary-General"))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
