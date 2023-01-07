using System;
using System.Collections.Generic;

namespace Ogani.WebUI.Models.Entity
{
	public class BlogCategory : BaseEntity
    {
		public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}

