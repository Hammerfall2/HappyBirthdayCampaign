using HappyBirthdayCampain.UI.Models;
using System.Web.Mvc;

namespace HappyBirthdayCampain.UI.Controllers
{
    public class DispatcherController : Controller
    {
        private readonly IUiLogger _logger;
        public DispatcherController(UiLogger logger) { _logger = logger; }
            
       
        // GET: Dispatcher
        public ActionResult Index(Employee userLogin)
        {
            _logger.Debug("[HttpGet] DispatcherController:Index()");
            return View(userLogin);
        }

    }
}