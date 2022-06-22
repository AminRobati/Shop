using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Web.Extentions;
using System.Threading.Tasks;

namespace Shop.Web.Areas.User.ViewComponents
{
    public class UserSideBarViewComponent : ViewComponent
    {
        #region constractor
        private readonly IUserService _userService;
        public UserSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _userService.GetUserById(User.GetUserId());
                return View("UserSideBar", result);
            }


            return View("UserSideBar");
        }

        #endregion
    }


    public class UserInformationViewComponent : ViewComponent
    {
        #region constrator
        private readonly IUserService _userService;
        public UserInformationViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                return View("UserInformation", user);
            }

            return View("UserInformation");
        }
        #endregion
    }


}
