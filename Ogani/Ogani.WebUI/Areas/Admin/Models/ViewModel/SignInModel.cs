using System;
using System.ComponentModel.DataAnnotations;
using Resource;

namespace Ogani.WebUI.Areas.Admin.Models.ViewModel
{
	public class SignInModel
	{
		[Required]
		[MinLength(3)]
        [Display(ResourceType = typeof(AccountResource), Name = "UserName")]
        public string UserName { get; set; }

		[Required]
        [Display(ResourceType = typeof(AccountResource), Name = "Password")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

