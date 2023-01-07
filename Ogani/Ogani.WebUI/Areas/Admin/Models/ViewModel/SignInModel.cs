using System;
using System.ComponentModel.DataAnnotations;

namespace Ogani.WebUI.Areas.Admin.Models.ViewModel
{
	public class SignInModel
	{
		[Required]
		[MinLength(3)]
		public string UserName { get; set; }

		[Required]
		//[DataType(DataType.Password)]
		public string Password { get; set; }
    }
}

