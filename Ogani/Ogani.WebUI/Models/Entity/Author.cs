using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Ogani.WebUI.Models.Entity
{
    public class Author : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        public string Role { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }

        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}

