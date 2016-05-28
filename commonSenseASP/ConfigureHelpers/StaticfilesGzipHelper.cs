using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.ConfigureHelpers {
    public static class StaticfilesGzipHelper {
        /// <summary>
        /// uses static files, but handles gziped resources. 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStaticFilesWithGzipSupport(this IApplicationBuilder builder, int cacheControlAgeInSeconds = 60 * 60) {
            builder.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = context => {
                    var file = context.File;
                    var response = context.Context.Response;
                    if (file.Name.EndsWith(".css.gz")) {
                        response.setHeadersForResponse("gzip", "text/css", cacheControlAgeInSeconds);
                    } else if (file.Name.EndsWith(".js.gz")) {
                        response.setHeadersForResponse("gzip", "application/javascript", cacheControlAgeInSeconds);
                    } else if (file.Name.EndsWith(".png.gz")) {
                        response.setHeadersForResponse("gzip", "image/png", cacheControlAgeInSeconds);
                    } else if (file.Name.EndsWith(".svg.gz")) {
                        response.setHeadersForResponse("gzip", "image/svg+xml", cacheControlAgeInSeconds);
                    }else if (file.Name.EndsWith(".jpg.gz")) {
                        response.setHeadersForResponse("gzip", "image/jpeg", cacheControlAgeInSeconds);
                    }
                }
            });
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="contentType"></param>
        /// <param name="maxAgeCacheInSec"></param>
        private static void setHeadersForResponse(this HttpResponse resp, string contentEncoding, string contentType, int maxAgeCacheInSec) {
            resp.Headers[HeaderNames.CacheControl] = "max-age=" + maxAgeCacheInSec; //cache for 60 min
            resp.Headers[HeaderNames.ContentEncoding] = contentEncoding;
            resp.Headers[HeaderNames.ContentType] = contentType;
        }
    }
}
