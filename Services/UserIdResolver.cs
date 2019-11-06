using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class UserIdResolver : IUserIdResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
        }
    }
}
