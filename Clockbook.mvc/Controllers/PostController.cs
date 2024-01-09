using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Clockbook.mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepo;

        public PostController(ILogger<PostController> logger, IPostRepository postRepo)
        {
            _logger = logger;
            _postRepo = postRepo;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postRepo.GetAllPostsAsync();
            return View(posts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}