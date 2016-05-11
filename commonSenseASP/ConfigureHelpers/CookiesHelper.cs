using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.ConfigureHelpers {
    public static class CookiesHelper {
        public static IApplicationBuilder UseCookieAuthentication(this IApplicationBuilder builder, string loginPath, TimeSpan lifeTime) {
            builder.UseCookieAuthentication(options => {
                options.LoginPath = new PathString(loginPath);
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.ExpireTimeSpan = lifeTime;
                options.AuthenticationScheme = "Cookies";
            });
            return builder;
        }
    }
}
