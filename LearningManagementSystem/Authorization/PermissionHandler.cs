using LearningManagementSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly LMSContext _context;
        private readonly IUserContext _userContext;
        public PermissionHandler(LMSContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userId = await _userContext.GetId();
            if (userId == null)
            {
                context.Fail();
                return;
            }

            var userPermission = await (from ur in _context.UserRoles
                                        join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                                        join p in _context.Permissions on rp.PermissionId equals p.Id
                                        where ur.UserId == userId
                                        select p.Name).ToListAsync();

            if(userPermission.Contains(requirement.PermissionName))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
