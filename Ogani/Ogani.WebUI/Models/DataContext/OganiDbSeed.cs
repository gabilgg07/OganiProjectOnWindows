using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ogani.WebUI.Models.Entity;
using Ogani.WebUI.Models.Entity.Membership;

namespace Ogani.WebUI.Models.DataContext
{
    public static class OganiDbSeed
    {
        internal static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<OganiDbContext>();

                db.Database.Migrate();

                InitCategories(db);
                InitProductUnits(db);
                InitProducts(db);
                InitAppInfo(db);

                InitAuthors(db);
                //InitUsers(db);
                InitBlogCategory(db);
                InitBlogs(db);
                InitBlogTags(db);
                //InitBlogTagBlogs(db);
            }

            return app;
        }

        internal static IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            string superAdminRoleName = "SuperAdmin";
            string superAdminEmail = "aaliyeva0791@gmail.com";
            string superAdminPassword = "!2023@QabilCoder0707#";
            string superAdminName = "sadmin";

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<OganiUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<OganiRole>>();

                var hasRole = roleManager.RoleExistsAsync(superAdminRoleName).Result;

                if (!hasRole)
                {
                    roleManager.CreateAsync(new OganiRole
                    {
                        Name = superAdminRoleName
                    }).Wait();
                }

                var admin = userManager.FindByEmailAsync(superAdminEmail).Result;

                if (admin == null)
                {
                    admin = new OganiUser
                    {
                        Email = superAdminEmail,
                        UserName = superAdminName,
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(admin, superAdminPassword).Wait();
                }

                var isAdmin = userManager.IsInRoleAsync(admin, superAdminRoleName).Result;

                if (!isAdmin)
                {
                    userManager.AddToRoleAsync(admin, superAdminRoleName).Wait();
                }

            }

            return app;
        }

        //private static void InitBlogTagBlogs(OganiDbContext db)
        //{
        //    if (!db.BlogTagBlogs.Any())
        //    {
        //        var blog = db.Blogs.FirstOrDefault();
        //        var blogtag = db.BlogTags.FirstOrDefault();

        //        if (blog != null && blogtag != null)
        //        {
        //            BlogTagBlog blogTagBlog = new BlogTagBlog
        //            {
        //                Blog = blog,
        //                BlogTag = blogtag
        //            };

        //            db.BlogTagBlogs.Add(blogTagBlog);
        //        }

        //        db.SaveChanges();
        //    }
        //}

        private static void InitBlogTags(OganiDbContext db)
        {
            if (!db.BlogTags.Any())
            {

                var blog = db.Blogs.FirstOrDefault();

                if (blog != null)
                {
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "All",
                        BlogTagBlogs = new List<BlogTagBlog> { new BlogTagBlog { Blog = blog } }

                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Trending",
                        BlogTagBlogs = new List<BlogTagBlog> { new BlogTagBlog { Blog = blog } }
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Cooking",
                        BlogTagBlogs = new List<BlogTagBlog> { new BlogTagBlog { Blog = blog } }
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Healthy Food",
                        BlogTagBlogs = new List<BlogTagBlog> { new BlogTagBlog { Blog = blog } }
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Life Style",
                        BlogTagBlogs = new List<BlogTagBlog> { new BlogTagBlog { Blog = blog } }
                    });
                }
                else
                {
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "All"
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Trending"
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Cooking"
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Healthy Food"
                    });
                    db.BlogTags.Add(new BlogTag
                    {
                        Name = "Life Style"
                    });
                }

                db.SaveChanges();
            }
        }

        private static void InitBlogs(OganiDbContext db)
        {
            if (!db.Blogs.Any())
            {
                var author = db.Authors.FirstOrDefault();
                var blogsCategory = db.BlogCategories.FirstOrDefault();

                if (author != null && blogsCategory != null)
                {
                    Blog blog = new Blog
                    {
                        Title = "The Moment You Need To Remove Garlic From The Menu",
                        Body = @"<p>
                            Sed porttitor lectus nibh. Vestibulum ac diam sit amet quam vehicula elementum sed sit amet
                            dui. Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Mauris blandit
                            aliquet elit, eget tincidunt nibh pulvinar a. Vivamus magna justo, lacinia eget consectetur
                            sed, convallis at tellus. Sed porttitor lectus nibh. Donec sollicitudin molestie malesuada.
                            Curabitur non nulla sit amet nisl tempus convallis quis ac lectus. Proin eget tortor risus.
                            Donec rutrum congue leo eget malesuada. Curabitur non nulla sit amet nisl tempus convallis
                            quis ac lectus. Donec sollicitudin molestie malesuada. Nulla quis lorem ut libero malesuada
                            feugiat. Curabitur arcu erat, accumsan id imperdiet et, porttitor at sem.
                        </p>
                        <h3>
                            The corner window forms a place within a place that is a resting point within the large
                            space.
                        </h3>
                        <p>
                            The study area is located at the back with a view of the vast nature. Together with the other
                            buildings, a congruent story has been managed in which the whole has a reinforcing effect on
                            the components. The use of materials seeks connection to the main house, the adjacent
                            stables
                        </p>",
                        ImagePath = "blog-1.jpg",
                        PublishedDate = new DateTime(2019, 1, 14),
                        Facebook = "https://www.facebook.com/",
                        Twitter = "https://twitter.com/",
                        Linkedin = "https://www.linkedin.com/",
                        Instagram = "https://www.instagram.com/",
                        BlogCategory = blogsCategory,
                        Author = author
                    };

                    //var user = db.Users.FirstOrDefault();
                    //if (user != null)
                    //{
                    //    blog.Comments = new List<Comment>();
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Blogunuz cox gozeldir",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Gozel blogunuz var",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Cox gozel Blogunuz var",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Maraqlidir",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Maraqli blogdur",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Nece de maaraqlidir",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Blog cox maraqli yazilib",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //    blog.Comments.Add(new Comment
                    //    {
                    //        Text = "Cox maraqlidir",
                    //        User = user,
                    //        Blog = blog
                    //    });
                    //}

                    db.Blogs.Add(blog);
                }

                db.SaveChanges();
            }
        }

        private static void InitBlogCategory(OganiDbContext db)
        {

            if (!db.BlogCategories.Any())
            {
                db.BlogCategories.Add(new BlogCategory
                {
                    Name = "Beuty"
                });
                db.BlogCategories.Add(new BlogCategory
                {
                    Name = "Food"
                });
                db.BlogCategories.Add(new BlogCategory
                {
                    Name = "Travel"
                });

                db.SaveChanges();
            }
        }

        //private static void InitUsers(OganiDbContext db)
        //{
        //    if (!db.Users.Any())
        //    {
        //        db.Users.Add(new User
        //        {
        //            Name = "Tony",
        //            Surname = "Cruse",
        //        });

        //        db.SaveChanges();
        //    }
        //}

        private static void InitAuthors(OganiDbContext db)
        {
            if (!db.Authors.Any())
            {
                db.Authors.Add(new Author
                {
                    FullName = "Michael Scofield",
                    Role = "Admin",
                    ImagePath = "author-1.jpg"
                });

                db.SaveChanges();
            }
        }

        private static void InitAppInfo(OganiDbContext db)
        {
            if (!db.AppInfos.Any())
            {
                db.AppInfos.Add(new AppInfo
                {
                    Title = "Ogani",
                    HashTag = "#təzə-tər #məhsullar",
                    Phone = "+99412-345-67-89",
                    ActivityHashTag = "support 24/7 time",
                    Email = "ogani@box.az",
                    Address = "60-49 Road 11378 New York"
                });

                db.SaveChanges();
            }
        }

        private static void InitProducts(OganiDbContext db)
        {
            if (!db.Products.Any())
            {
                var category = db.Categories.FirstOrDefault();
                var unit = db.ProductUnits.FirstOrDefault();

                if (category != null && unit != null)
                {
                    Product product = new Product
                    {
                        Name = "Vetgetable’s Package",
                        ShortDescription = "Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a. Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Sed porttitor lectus nibh. Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Proin eget tortor risus.",
                        Price = 50.99M,
                        Weight = 0.5M,
                        Description = @"<h6>Products Infomation</h6>
                                    <p>
                                        Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui.
                                        Pellentesque in ipsum id orci porta dapibus. Proin eget tortor risus. Vivamus
                                        suscipit tortor eget felis porttitor volutpat. Vestibulum ac diam sit amet quam
                                        vehicula elementum sed sit amet dui. Donec rutrum congue leo eget malesuada.
                                        Vivamus suscipit tortor eget felis porttitor volutpat. Curabitur arcu erat,
                                        accumsan id imperdiet et, porttitor at sem. Praesent sapien massa, convallis a
                                        pellentesque nec, egestas non nisi. Vestibulum ac diam sit amet quam vehicula
                                        elementum sed sit amet dui. Vestibulum ante ipsum primis in faucibus orci luctus
                                        et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam
                                        vel, ullamcorper sit amet ligula. Proin eget tortor risus.
                                    </p>
                                    <p>
                                        Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Lorem
                                        ipsum dolor sit amet, consectetur adipiscing elit. Mauris blandit aliquet
                                        elit, eget tincidunt nibh pulvinar a. Cras ultricies ligula sed magna dictum
                                        porta. Cras ultricies ligula sed magna dictum porta. Sed porttitor lectus
                                        nibh. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a.
                                        Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Sed
                                        porttitor lectus nibh. Vestibulum ac diam sit amet quam vehicula elementum
                                        sed sit amet dui. Proin eget tortor risus.
                                    </p>",
                        Information = @"<h6>Products Infomation</h6>
                                    <p>
                                        Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui.
                                        Pellentesque in ipsum id orci porta dapibus. Proin eget tortor risus. Vivamus
                                        suscipit tortor eget felis porttitor volutpat. Vestibulum ac diam sit amet quam
                                        vehicula elementum sed sit amet dui. Donec rutrum congue leo eget malesuada.
                                        Vivamus suscipit tortor eget felis porttitor volutpat. Curabitur arcu erat,
                                        accumsan id imperdiet et, porttitor at sem. Praesent sapien massa, convallis a
                                        pellentesque nec, egestas non nisi. Vestibulum ac diam sit amet quam vehicula
                                        elementum sed sit amet dui. Vestibulum ante ipsum primis in faucibus orci luctus
                                        et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam
                                        vel, ullamcorper sit amet ligula. Proin eget tortor risus.
                                    </p>
                                    <p>
                                        Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Lorem
                                        ipsum dolor sit amet, consectetur adipiscing elit. Mauris blandit aliquet
                                        elit, eget tincidunt nibh pulvinar a. Cras ultricies ligula sed magna dictum
                                        porta. Cras ultricies ligula sed magna dictum porta. Sed porttitor lectus
                                        nibh. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a.
                                        Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Sed
                                        porttitor lectus nibh. Vestibulum ac diam sit amet quam vehicula elementum
                                        sed sit amet dui. Proin eget tortor risus.
                                    </p>",
                        Reviews = @"<h6>Products Reviews</h6>
                                    <p>
                                        Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui.
                                        Pellentesque in ipsum id orci porta dapibus. Proin eget tortor risus. Vivamus
                                        suscipit tortor eget felis porttitor volutpat. Vestibulum ac diam sit amet quam
                                        vehicula elementum sed sit amet dui. Donec rutrum congue leo eget malesuada.
                                        Vivamus suscipit tortor eget felis porttitor volutpat. Curabitur arcu erat,
                                        accumsan id imperdiet et, porttitor at sem. Praesent sapien massa, convallis a
                                        pellentesque nec, egestas non nisi. Vestibulum ac diam sit amet quam vehicula
                                        elementum sed sit amet dui. Vestibulum ante ipsum primis in faucibus orci luctus
                                        et ultrices posuere cubilia Curae; Donec velit neque, auctor sit amet aliquam
                                        vel, ullamcorper sit amet ligula. Proin eget tortor risus.
                                    </p>
                                    <p>
                                        Praesent sapien massa, convallis a pellentesque nec, egestas non nisi. Lorem
                                        ipsum dolor sit amet, consectetur adipiscing elit. Mauris blandit aliquet
                                        elit, eget tincidunt nibh pulvinar a. Cras ultricies ligula sed magna dictum
                                        porta. Cras ultricies ligula sed magna dictum porta. Sed porttitor lectus
                                        nibh. Mauris blandit aliquet elit, eget tincidunt nibh pulvinar a.
                                        Vestibulum ac diam sit amet quam vehicula elementum sed sit amet dui. Sed
                                        porttitor lectus nibh. Vestibulum ac diam sit amet quam vehicula elementum
                                        sed sit amet dui. Proin eget tortor risus.
                                    </p>",
                        Unit = unit,
                        Category = category
                    };

                    product.Images = new List<ProductImage>();

                    product.Images.Add(new ProductImage
                    {
                        ImagePath = "product-details-1.jpg",
                        IsMain = true
                    });

                    product.Images.Add(new ProductImage
                    {
                        ImagePath = "thumb-1.jpg",
                        IsMain = false
                    });

                    product.Images.Add(new ProductImage
                    {
                        ImagePath = "thumb-2.jpg",
                        IsMain = false
                    });

                    product.Images.Add(new ProductImage
                    {
                        ImagePath = "thumb-3.jpg",
                        IsMain = false
                    });

                    product.Images.Add(new ProductImage
                    {
                        ImagePath = "thumb-4.jpg",
                        IsMain = false
                    });

                    db.Products.Add(product);

                    db.SaveChanges();
                }
            }
        }

        private static void InitProductUnits(OganiDbContext db)
        {
            if (!db.ProductUnits.Any())
            {
                db.ProductUnits.Add(new ProductUnit
                {
                    Name = "kq",
                    Description = "Kiloqram"
                });

                db.ProductUnits.Add(new ProductUnit
                {
                    Name = "q",
                    Description = "Qram"
                });

                db.ProductUnits.Add(new ProductUnit
                {
                    Name = "l",
                    Description = "Litr"
                });

                db.ProductUnits.Add(new ProductUnit
                {
                    Name = "ml",
                    Description = "Millilitr"
                });

                db.ProductUnits.Add(new ProductUnit
                {
                    Name = "m",
                    Description = "Metr"
                });

                db.ProductUnits.Add(new ProductUnit
                {
                    Name = "sm",
                    Description = "Santimetr"
                });

                db.SaveChanges();
            }

        }

        private static void InitCategories(OganiDbContext db)
        {
			if (!db.Categories.Any())
            {
                db.Categories.Add(new Category
                {
                    Name = "Fresh Onion"
                });
                db.Categories.Add(new Category
                {
                    Name = "Fresh Berries"
                });
                db.Categories.Add(new Category
                {
                    Name = "Vegetables"
                });
                db.Categories.Add(new Category
                {
                    Name = "Fresh Bananas"
                });
                db.Categories.Add(new Category
                {
                    Name = "Fresh Meat"
                });

                db.SaveChanges();
            }
        }
    }
}

