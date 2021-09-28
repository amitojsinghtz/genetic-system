using HISSystem.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace HISSystem.Filters
{
    public class AuthorizationFilter
    {

    }

    public class CustomAuth : TypeFilterAttribute
    {
        public CustomAuth(Page item, ActionButton action) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { item, action };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly Page _item;
        private readonly ActionButton _action;
        public AuthorizeActionFilter(Page item, ActionButton action)
        {
            _item = item;
            _action = action;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User.Claims.Where(x => x.Type == "User").Select(x=>x.Value).FirstOrDefault();
            bool isAuthorized = PermissionHelper.ViewPermission(_item, Convert.ToInt32(user));

            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
