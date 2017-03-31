using Common.Util;
using Core.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/

        public ActionResult Index()
        {

            return View(XinYuan.GetList() );
        }
        public ActionResult Index4()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index4(string dataJson)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                XinYuan postModel = Serializer.ToObject<XinYuan>(dataJson);
                postModel.Save(out msg);
            }
            catch (Exception e)
            {
                state = false;
                msg = e.Message;
            }

            return new JsonResult { Data = new { state = state, msg = msg } };
        }

       
    }
}
