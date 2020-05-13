using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Shop.Web.ViewModels;

namespace Shop.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private IMapper _mapper;

        public AdminController(UserManager<IdentityUser> manager, IMapper mapper)
        {
            _userManager = manager;
            _mapper = mapper;
        }

        public ActionResult  Index()
        {
            var users = this._userManager.Users.ToList();
            return View(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }
    }
}
