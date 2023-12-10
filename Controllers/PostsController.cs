using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Controllers
{
    public class PostsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public PostsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        // View all blogs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await bloggieDbContext.BlogPosts.ToListAsync());
        }

        // View a specific blog
        public async Task<IActionResult> AddOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
                return View(new BlogPost());
            else
                return View(await bloggieDbContext.BlogPosts.FindAsync(Id));
        }

        public async Task<IActionResult> ViewBlog(Guid Id)
        {
                return View(await bloggieDbContext.BlogPosts.FindAsync(Id));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddOrEdit(BlogPost blogPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var blog = new BlogPost
        //        {
        //            Id = blogPost.Id,
        //            Heading = blogPost.Heading,
        //            PageTitle = blogPost.PageTitle,
        //            Content = blogPost.Content,
        //            ShortDescription = blogPost.ShortDescription,
        //            FeaturedImageUrl = blogPost.FeaturedImageUrl,
        //            UrlHandle = blogPost.UrlHandle,
        //            PublishedDate = DateTime.Now,
        //            Author = blogPost.Author
        //        };
        //        if (blogPost.Id == Guid.Empty)
        //        {
        //            bloggieDbContext.BlogPosts.Add(blog);
        //        }
        //        else
        //        {
        //            bloggieDbContext.Update(blog);
        //        }
        //        await bloggieDbContext.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(blogPost);
        //}
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Create a blog post
        public async Task<IActionResult> AddOrEdit(AddPostRequest addPostRequest)
        {
            if (ModelState.IsValid)
            {
                var blog = new BlogPost
                {
                    Id = addPostRequest.Id,
                    Heading = addPostRequest.Heading,
                    PageTitle = addPostRequest.PageTitle,
                    Content = addPostRequest.Content,
                    ShortDescription = addPostRequest.ShortDescription,
                    FeaturedImageUrl = addPostRequest.FeaturedImageUrl,
                    UrlHandle = addPostRequest.UrlHandle,
                    PublishedDate = DateTime.Now,
                    Author = addPostRequest.Author,
                    Visible = addPostRequest.Visible
                };
                if (addPostRequest.Id == Guid.Empty)
                {
                    bloggieDbContext.BlogPosts.Add(blog);
                }
                else
                {
                    bloggieDbContext.Update(blog);
                }
                await bloggieDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addPostRequest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            var blogId = await bloggieDbContext.BlogPosts.FindAsync(Id);
            bloggieDbContext.BlogPosts.Remove(blogId);
            await bloggieDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
