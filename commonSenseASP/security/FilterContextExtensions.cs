using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.security {
    public static class FilterContextExtensions {

        public static bool IsLocalRequest(this FilterContext context) {
            return context.HttpContext.Connection.IsLocal ||
                (context.HttpContext.Connection.RemoteIpAddress == null && context.HttpContext.Connection.LocalIpAddress == null);
        }

    }
}
