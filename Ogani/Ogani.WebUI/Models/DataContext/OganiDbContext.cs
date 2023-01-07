using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.Entity;
using Ogani.WebUI.Models.Entity.Membership;

namespace Ogani.WebUI.Models.DataContext
{
	public class OganiDbContext
		: IdentityDbContext<OganiUser,OganiRole,int,OganiUserClaim,OganiUserRole,
			OganiUserLogin,OganiRoleClaim,OganiUserToken>
    {
		public OganiDbContext(DbContextOptions options)
			:base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<ProductUnit> ProductUnits { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<ProductImage> ProductImages { get; set; }

		public DbSet<ContactPost> ContactPosts { get; set; }

		public DbSet<AppInfo> AppInfos { get; set; }

		public DbSet<Author> Authors { get; set; }

        public DbSet<BlogTag> BlogTags { get; set; }

		public DbSet<BlogCategory> BlogCategories { get; set; }

        public DbSet<Blog> Blogs { get; set; }

		//public DbSet<Comment> Comments { get; set; }

        public DbSet<BlogTagBlog> BlogTagBlogs { get; set; }

		public DbSet<Subscribe> Subscribes { get; set; }

		public DbSet<Audit> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OganiUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });
            builder.Entity<OganiRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });
            builder.Entity<OganiUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");
            });
            builder.Entity<OganiUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");
            });
            builder.Entity<OganiRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });
            builder.Entity<OganiUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });
            builder.Entity<OganiUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });
        }
    }
}

