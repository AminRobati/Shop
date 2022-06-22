using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using System.Threading.Tasks;

namespace Shop.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region constractor
        private readonly IOrderService _orderService;
        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }
    
        #endregion

        #region dashboard
        public async Task<IActionResult > Index()
        {
            ViewData["ResultOrder"] = await _orderService.GetResultOrder(); 
            return View();
        }
        #endregion
    }
}
