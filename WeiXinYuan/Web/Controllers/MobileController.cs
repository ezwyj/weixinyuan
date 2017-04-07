using Common.Util;
using Core.Entity;
using Newtonsoft.Json;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];

        public ActionResult Index()
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            return View(XinYuan.GetList() );
        }

        public ActionResult Index1()
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            return View(HuoDong.GetList());
        }
        public ActionResult Index2()
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            return View(ChangDi.GetList());
        }
        public ActionResult Index3()
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
           
            return View();
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
