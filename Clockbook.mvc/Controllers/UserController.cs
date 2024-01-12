using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clockbook.Domain.Interfaces;
using Clockbook.Domain.Models;
using Clockbook.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Clockbook.mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepo;
        private readonly IPostRepository _postRepo;

        public UserController(ILogger<UserController> logger, IUserRepository userRepo, IPostRepository postRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _postRepo = postRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region GetAllUser

        public async Task<IActionResult> Users()
        {

            // TODO: make the logic for desplaying user by state then by city then by country
            var users = await _userRepo.GetAllUsersAsync();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id              = user.Id,
                    FirstName       = user.FirstName,
                    LastName        = user.LastName,
                    ImageUrl        = user.ImageUrl ?? "images/profile.jpg",
                    Job             = user.Job
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        #endregion

        #region PersonalPage

        public async Task<IActionResult> PersonalPage(string id)
        {
            // get user info 
            var user =  await _userRepo.GetUserByIdAsync(id);

            // get all the posts related to that user
            var posts = await _postRepo.GetPostByUserIdAsync(id);

            // get the number of followers
            var userFollowers = await _userRepo.GetAllFollowers(id);

            // get the number of followings
            var UserFollowings = await _userRepo.GetAllFollowings(id);

            // check if the the current user follow that user
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var follow = await _userRepo.IsUserFollow(userId, id);
            bool following;
            if(follow is null)
            {
                following = false;
            }
            else
            {
                following = true;
            }

            // add them to the dto
            var pageInfo = new PersonalPageViewModel
            {
                User           = user,
                Posts          = posts,
                UserFollowers  = userFollowers,
                UserFollowing  = UserFollowings,
                Following      = following
            };

            // return the view
            return View(pageInfo);
        }

        #endregion
        

        #region Follow
        [HttpPost]
        public async Task<IActionResult> Follow(Follow followModel)
        {
            // check if the user is already follow 
            var follow = await _userRepo.IsUserFollow(followModel.UserId!, followModel.FollowingId!);
            if(ModelState.IsValid)
            {
                // if follow remove the follow
                if(follow is not null)
                {
                    _userRepo.DeleteFollow(follow);
                }
                else
                {
                    // if not add the follow
                    _userRepo.AddFollow(followModel);
                }
            }
            return RedirectToAction("Index","Home");
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}