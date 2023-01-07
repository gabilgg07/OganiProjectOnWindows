using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Ogani.WebUI.Models.Entity
{
	public class Blog : BaseEntity
    {

		[Required]
		public string Title { get; set; }

        [Required]
		public string Body { get; set; }

        //[NotMapped] --> db de saxlama demekdir
        //public IFormFile ImagePath { get; set; }

        // asagidaki kimi yazarsaq -> db de bu adla saxlayir.
        //[Column("ImagePath")]
        //public string ImagePathTemp { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Linkedin { get; set; }

        public string Instagram { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public int BlogCategoryId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<BlogTagBlog> BlogTagBlogs { get; set; }
    }
}

