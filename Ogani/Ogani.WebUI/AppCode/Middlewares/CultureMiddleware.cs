using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Ogani.WebUI.AppCode.Providers;

namespace Ogani.WebUI.AppCode.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string lang = "az";

            //const string supportedLangs = "(?<lang>az|en|ru)";

            Match langMatch = Regex.Match(httpContext.Request.Path, @"^\/(?<lang>az|en|ru)\/?");

            if (langMatch.Success)
            {
                var opt = new CookieOptions
                {
                    Path = "/",
                    Expires = DateTime.Now.AddDays(7)
                };

                httpContext.Response.Cookies.Delete("lang");
                httpContext.Response.Cookies.Append("lang", langMatch.Groups["lang"].Value,opt);

                return _next(httpContext);
            }

            if (!(httpContext.Request.Cookies.TryGetValue("lang", out lang) &&
                Regex.IsMatch(lang, @"(?<lang>az|en|ru)")))
                lang = "az";

            httpContext.Response.Redirect($"{lang}{httpContext.Request.Path}" +
                $"{httpContext.Request.QueryString.Value}");

            return Task.CompletedTask;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseCultureMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CultureMiddleware>();
            builder.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedCultures("az", "en", "ru");
                cfg.AddSupportedUICultures("az", "en", "ru");

                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });


            return builder;
        }
    }
}

