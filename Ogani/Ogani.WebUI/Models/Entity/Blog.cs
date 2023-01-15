using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Resource;

namespace Ogani.WebUI.Models.Entity
{
	public class Blog : BaseEntity
    {

		[Required]
        [Display(ResourceType =typeof(BlogResource),Name = "Title")]
		public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(BlogResource), Name = "Body")]
        public string Body { get; set; }

        //[NotMapped] --> db de saxlama demekdir
        //public IFormFile ImagePath { get; set; }

        // asagidaki kimi yazarsaq -> db de bu adla saxlayir.
        //[Column("ImagePath")]
        //public string ImagePathTemp { get; set; }

        [Display(ResourceType = typeof(BlogResource), Name = "BlogImage")]
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Display(ResourceType = typeof(BlogResource), Name = "PublishedDate")]
        public DateTime? PublishedDate { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Linkedin { get; set; }

        public string Instagram { get; set; }

        [Display(ResourceType = typeof(BlogResource), Name = "AuthorFullName")]
        public int AuthorId { get; set; }

        [Display(ResourceType = typeof(BlogResource), Name = "AuthorFullName")]
        public virtual Author Author { get; set; }

        [Display(ResourceType = typeof(BlogResource), Name = "CategoryName")]
        public int BlogCategoryId { get; set; }

        [Display(ResourceType = typeof(BlogResource), Name = "CategoryName")]
        public virtual BlogCategory BlogCategory { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<BlogTagBlog> BlogTagBlogs { get; set; }
    }
}

