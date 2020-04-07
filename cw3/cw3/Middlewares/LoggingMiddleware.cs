using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var httpPath = httpContext.Request.Path;
            var queryString = httpContext.Request.QueryString;
            var body = "";
            using (var sr = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024))
            {
                body += await sr.ReadToEndAsync();
            }

            using (var sw = File.AppendText("requestsLog.txt"))
            {
                sw.WriteLine($"Method: {httpMethod}");
                sw.WriteLine($"Path: {httpPath}");
                sw.WriteLine($"Body:\n{body}");
                sw.WriteLine($"QueryString: {queryString}\n");
            }

            await _next(httpContext);
        }
    }
}
