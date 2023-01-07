using System;
using Microsoft.AspNetCore.Identity;

namespace Ogani.WebUI.Models.Entity.Membership
{
	public class OganiUserToken:IdentityUserToken<int>
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }
    }
}

