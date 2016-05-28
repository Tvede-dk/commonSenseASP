using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.security {
    /// <summary>
    /// Represents an <see cref="ActionResult"/> that when executed will
    /// produce an HTTP response with the given response status code.
    /// </summary>
    public class StatusCodeResult : ActionResult {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCodeResult"/> class
        /// with the given <paramref name="statusCode"/>.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        public StatusCodeResult(int statusCode) {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public int StatusCode { get; private set; }

        /// <inheritdoc />
        public override void ExecuteResult(ActionContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }
            context.HttpContext.Response.StatusCode = StatusCode;
        }
    }
}
