using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ogani.WebUI.Models.Entity
{
	public class Category : BaseEntity
    {
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}

