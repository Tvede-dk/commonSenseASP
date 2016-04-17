using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;

namespace commonSenseASP.security {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HttpsRemoteAttribute : Attribute, IAuthorizationFilter, IOrderedFilter {
        public int Order { get; set; }

        public void OnAuthorization(AuthorizationContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.HttpContext.Request.IsHttps && !context.IsLocalRequest()) {
                HandleNonHttpsRequest(context);
            }
        }


        /// <summary>
        /// Called from <see cref="OnAuthorization"/> if the request is not received over HTTPS. Expectation is
        /// <see cref="AuthorizationFilterContext.Result"/> will not be <c>null</c> after this method returns.
        /// </summary>
        /// <param name="filterContext">The <see cref="AuthorizationFilterContext"/> to update.</param>
        /// <remarks>
        /// If it was a GET request, default implementation sets <see cref="AuthorizationFilterContext.Result"/> to a
        /// result which will redirect the client to the HTTPS version of the request URI. Otherwise, default
        /// implementation sets <see cref="AuthorizationFilterContext.Result"/> to a result which will set the status
        /// code to <c>403</c> (Forbidden).
        /// </remarks>
        protected virtual void HandleNonHttpsRequest(FilterContext filterContext) {
            // only redirect for GET requests, otherwise the browser might not propagate the verb and request
            // body correctly.
            if (!string.Equals(filterContext.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase)) {
                filterContext.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            } else {
                var optionsAccessor = filterContext.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>();

                var request = filterContext.HttpContext.Request;

                var host = request.Host;

                var newUrl = string.Concat(
                    "https://",
                    host.ToUriComponent(),
                    request.PathBase.ToUriComponent(),
                    request.Path.ToUriComponent(),
                    request.QueryString.ToUriComponent());

                // redirect to HTTPS version of page
                filterContext.HttpContext.Response.Redirect(newUrl, false);
            }
        }
    }
}
