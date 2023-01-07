using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.AppCode.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(System.IO.Path.GetExtension(httpContext.Request.Path)))
            {
                using (var scope = httpContext.RequestServices.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<OganiDbContext>();

                    //before
                    var auditLog = new Audit();
                    auditLog.RequestDate = DateTime.Now;
                    auditLog.CreatedDate = DateTime.Now;
                    auditLog.Method = httpContext.Request.Method;
                    auditLog.Path = httpContext.Request.Path;
                    auditLog.QueryString = httpContext.Request.QueryString.Value;
                    auditLog.Action = httpContext.Request.RouteValues["action"]?.ToString();
                    auditLog.Controller = httpContext.Request.RouteValues["controller"]?.ToString();
                    auditLog.Area = httpContext.Request.RouteValues["area"]?.ToString();

                    if (httpContext.User.Identity.IsAuthenticated)
                    {
                        int userId = Convert.ToInt32(httpContext.User.Claims
                            .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "0");

                        if (userId > 0)
                        {
                            auditLog.CreatedByUserId = userId;
                        }
                    }

                    await _next(httpContext);
                    //return _next(httpContext);

                    //after
                    auditLog.StatusCode = httpContext.Response.StatusCode;
                    auditLog.ResponseDate = DateTime.Now;

                    db.AuditLogs.Add(auditLog);
                    await db.SaveChangesAsync();
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuditMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditMiddleware>();
        }
    }
}

