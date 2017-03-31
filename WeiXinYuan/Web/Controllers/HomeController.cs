using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var user = Account.Service.UserService.GetCurrUser();
            if (user == null)
            {
                ViewBag.errorMsg = "用户名、密码错误！";
                string backPage = Request["backPage"];
                if (!string.IsNullOrEmpty(backPage))
                {
                    return RedirectToAction("Login", "Account", new { BackPage = backPage });

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

            }

            return View();
        }

    }
}
