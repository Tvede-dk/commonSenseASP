using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.ConfigureHelpers {
    public static class CookiesHelper {
        public static IApplicationBuilder UseCookieAuthentication(this IApplicationBuilder builder, string loginPath, TimeSpan lifeTime) {
            var options = new CookieAuthenticationOptions {
                LoginPath = new PathString(loginPath),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                ExpireTimeSpan = lifeTime,
                AuthenticationScheme = "Cookies"
            };

            builder.UseCookieAuthentication(options);
            return builder;
        }
    }
}
