using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Clockbook.Domain.Interfaces;
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
            // add them to the dto
            var pageInfo = new PersonalPageViewModel
            {
                User        = user,
                Posts       = posts
            };
            // return the view
            return View(pageInfo);
        }

        #endregion
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}