using System;
using System.ComponentModel.DataAnnotations;

namespace Ogani.WebUI.Models.Entity
{
	public class AppInfo
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

        [Required]
        public string HashTag { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string ActivityHashTag { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public string Linkedin { get; set; }

        public string Pinterest { get; set; }
    }
}

