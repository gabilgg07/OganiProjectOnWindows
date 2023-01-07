using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ogani.WebUI.Models.Entity
{
	public class Product : BaseEntity
    {
		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		public decimal Price { get; set; }

		public string ShortDescription { get; set; }

		public decimal Weight { get; set; }

		public int UnitId { get; set; }

		//[ForeignKey("UnitId")]
		public virtual ProductUnit Unit { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public string Information { get; set; }

        public string Reviews { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

    }
}

