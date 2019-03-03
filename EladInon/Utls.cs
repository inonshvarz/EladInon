using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EladInon
{
    public static class Utils
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            var userID = httpContext.User.Claims.FirstOrDefault(clm => clm.Type == "UserId");
            return userID is null ? -1 : int.Parse(userID.Value);
        }
    }
}
