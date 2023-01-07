using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ogani.WebUI.Models.Entity
{
	public class ProductUnit : BaseEntity
    {
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(300)]
		public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}

