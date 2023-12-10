using Azure;
using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Bloggie.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await bloggieDbContext.Tags.ToListAsync());
        }

        public async Task<IActionResult> AddOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
                return View(new Tag());
            else
                return View(await bloggieDbContext.Tags.FindAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Tag addTagRequest)
        {
            var tag = new Tag {
                Id = addTagRequest.Id,
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            if (addTagRequest.Id == Guid.Empty)
            {
                bloggieDbContext.Tags.Add(tag);
                await bloggieDbContext.SaveChangesAsync();             
            } else
            {
                bloggieDbContext.Update(tag);
                await bloggieDbContext.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            var tagId = await bloggieDbContext.Tags.FindAsync(Id);
            bloggieDbContext.Tags.Remove(tagId);
            await bloggieDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
