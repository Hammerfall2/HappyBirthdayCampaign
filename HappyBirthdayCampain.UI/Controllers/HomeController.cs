using HappyBirthdayCampain.BLL;
using HappyBirthdayCampain.BOL;
using System.Web.Mvc;
using System;
using HappyBirthdayCampain.UI.ExceptionUI;
using HappyBirthdayCampain.UI.Models;
using HappyBirthdayCampain.DAL;

namespace HappyBirthdayCampain.UI.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUiLogger _logger;
        

        public HomeController(IUserManager userManager, IUiLogger logger) { _userManager = userManager; _logger = logger; }

        public ActionResult Index()
        {
            _logger.Debug("[HttpGet] HommeController:Index()");
            _logger.Debug("Strat Application");
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeLogin emp)
        {
            _logger.Debug("[httpPost] HommeController:Index()");
            EmployeeDTO user = null;
             try
             {
     
                _logger.Info("Log in user verifiacation");

                
                var base64EncodedBytes = System.Convert.FromBase64String(emp.Password);
                var decodePassword = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                if ( _userManager.LogInVerification(emp.UserName, decodePassword, out user))
                    {
                        var userLogin = Maps.EmployeeMapper.Map(user);

                        _logger.Debug("Return [HttpPost] HommeController:Index()");
                        return RedirectToAction("Index", "Dispatcher", userLogin);

                    }
             }
            catch (BllException e)
            {
                var ex = new UiException("User and Password are invalid");
                _logger.Error($"ERROR HomeController::Index", ex);
                throw ex;

            }

            ModelState.AddModelError("Password", "Your Username or Password are incorrect");
            return View();
        }

      

    }
}