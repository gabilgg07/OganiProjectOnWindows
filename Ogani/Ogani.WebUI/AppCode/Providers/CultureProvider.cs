using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Ogani.WebUI.AppCode.Providers
{
    public class CultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (Regex.IsMatch(httpContext.Request.Path, @".*\.(jpg|jpeg|png)$"))
                return Task.FromResult(new ProviderCultureResult("az"));

            string ln = "az";

            Match langMatch = Regex.Match(httpContext.Request.Path, @"/?(?<lang>en|az|ru)/?");

            if (langMatch.Success)
            {
                ln = langMatch.Groups["lang"].Value;
            }

            var cu = new ProviderCultureResult(ln);

            return Task.FromResult(cu);
        }
    }
}

