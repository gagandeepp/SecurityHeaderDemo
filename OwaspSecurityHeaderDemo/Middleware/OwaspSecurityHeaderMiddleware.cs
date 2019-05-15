using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OwaspSecurityHeaderDemo.Middlewares
{
    public class OwaspSecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public OwaspSecurityHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The main task of the middleware. This will be invoked whenever
        /// the middleware fires
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext" /> for the current request or response</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            //CSP Header
            httpContext.Response.Headers.Add("Content-Security-Policy",
                              "script-src 'self'; " +
                "style-src 'self'; " +
                "img-src 'self'");
            //XContent Type Header
            httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //X frame header
            httpContext.Response.Headers.Add("X-FRAME-Options", "SAMEORIGIN"); // Various options can be set like DENY,ALLOW-FROM
            //Xss header
            httpContext.Response.Headers.Add("X-Xss-Protection", "1"); // 0:disable,1:Protection,mode=block,report=url
            //Cache Header
            httpContext.Response.Headers.Add("Cache-Control", "public");
            // Call the next middleware in the chain
            await _next.Invoke(httpContext);
        }
    }
}
