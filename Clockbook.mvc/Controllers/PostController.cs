using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clockbook.Domain.Extensions;
using Clockbook.Domain.Interfaces;
using Clockbook.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Clockbook.mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepo;
        private readonly ILikeRepository _likeRepo;
        private readonly ICommentRepository _commentRepo;


        public PostController(ILogger<PostController> logger, IPostRepository postRepo,
        // HttpContextAccessor httpContextAccessor, 
        ILikeRepository likeRepo, ICommentRepository commentRepo)
        {
            _logger = logger;
            _postRepo = postRepo;
            _likeRepo = likeRepo;
            _commentRepo = commentRepo;
        }

        public async Task<IActionResult> Index()
        {
            // get all the posts
            var posts = await _postRepo.GetAllPostsAsync();
            return View(posts);
        }

        #region LikeThePost
        [HttpPost]
        public async Task<IActionResult> Like(string postId)
        {
            // get the user id
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;


            // Check if the user already liked the post
            if (!_likeRepo.IsLiked(userId, postId))
            {
                // Create a new like
                var like = new Like
                {
                    UserId = userId,
                    PostId = postId
                };
                // add the like
                _likeRepo.Add(like);
            } else
            {
                // get the like id
                var like = await _likeRepo.GetTheLikeByPostIdAsync(postId, userId);
                // delete the like
                _likeRepo.Delete(like);
            }

            // Redirect to the post or wherever you want
            return RedirectToAction("Index");
        }
        #endregion

        #region CommentOnThePost
        [HttpPost]
        public IActionResult Comment(Comment comment)
        {
            if(ModelState.IsValid)
            {
                var newComment = new Comment
                {
                    Content     = comment.Content,
                    UserId      = comment.UserId,
                    PostId      = comment.PostId
                };
                _commentRepo.Add(newComment);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        } 
        #endregion
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}