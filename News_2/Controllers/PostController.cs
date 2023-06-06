using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using News_2.Helpers;
using News_2.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace News_2.Controllers
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
            return View(newsDbContext.Posts.Include(x=> x.PostTags).ThenInclude(x=>x.Tag).Include(x=> x.Category));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Post post = newsDbContext.Posts.Include(x => x.PostTags).ThenInclude(x=>x.Tag).Include(x=> x.Category).FirstOrDefault(x => x.Id == id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Post post = newsDbContext.Posts.FirstOrDefault(x => x.Id == id);

            ViewBag.Categories = new SelectList(newsDbContext.Categories, "Id", "Name", newsDbContext.Categories.Find(id));
            var selectedTagsIds = newsDbContext.PostTags.Where(x => x.PostId == id).Select(x=>x.TagId);
            ViewBag.Tags = new MultiSelectList(newsDbContext.Tags, "Id", "Name", selectedTagsIds);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post, IFormFile ImageUrl, int[] tags)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    post.ImageUrl = await FileUploadHelper.UploadAsync(ImageUrl);
                }
                catch (Exception)
                {

                }

                post.Date = DateTime.Now;
                newsDbContext.Posts.Update(post);
                await newsDbContext.SaveChangesAsync();

               var postTags = newsDbContext.PostTags.Where(x => x.PostId == post.Id);
                newsDbContext.PostTags.RemoveRange(postTags);
                newsDbContext.PostTags.AddRange(tags.Select(x => new PostTag { TagId = x, PostId = post.Id }));
                await newsDbContext.SaveChangesAsync();
                //foreach (var id in tags)
                //{
                //    newsDbContext.PostTags.Add(new PostTag { TagId = id, PostId = post.Id });
                //}


                TempData["status"] = "   Post edit!";
                return RedirectToAction("Index", "Post");
            }
            return View("AddPost", post);
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            //ViewBag.Categories = newsDbContext.Categories;
            ViewBag.Categories = new SelectList(newsDbContext.Categories, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(newsDbContext.Tags, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Post post, IFormFile ImageUrl, int[] tags)
        {

            if (ModelState.IsValid)
            {
                post.ImageUrl = await FileUploadHelper.UploadAsync(ImageUrl);

                post.Date = DateTime.Now;
                await newsDbContext.AddAsync(post);
                await newsDbContext.SaveChangesAsync();

                //foreach (var id in tags)
                //{
                //    newsDbContext.PostTags.Add(new PostTag { TagId = id, PostId = post.Id });
                //}
                newsDbContext.PostTags.AddRange(tags.Select(x => new PostTag { TagId = x, PostId = post.Id }));

                await newsDbContext.SaveChangesAsync();

                TempData["status"] = "   New post added!";
                return RedirectToAction("Index", "Post");
            }
            return View("AddPost", post);
        }
    }
}
