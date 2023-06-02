using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Helpers;
using News.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace News.Controllers
{
    public class PostController: Controller
    {
        private readonly NewsDbContext newsDbContext;

        public PostController(NewsDbContext newsDbContext)
        {
           this.newsDbContext = newsDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(newsDbContext.Posts);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Post post = newsDbContext.Posts.FirstOrDefault(x => x.Id == id);
            return View(post);
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Post post, IFormFile ImageUrl)
        {

            if (ModelState.IsValid)
            {
                post.ImageUrl = await FileUploadHelper.UploadAsync(ImageUrl);

                post.Date = DateTime.Now;
                await newsDbContext.AddAsync(post);
                await newsDbContext.SaveChangesAsync();
                TempData["status"] = "   New post added!";
                return RedirectToAction("Index", "Post");
            }
            return View("AddPost", post);
        }
    }
}
