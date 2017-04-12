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
using ValueSet.Service;

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
            return View(XinYuan.GetListByProperty(a => a.Status, (int)StatusEnum.报名中).ToList());
        }

        public ActionResult Index1()
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            return View(HuoDong.GetListByProperty(a=>a.Status,(int)StatusEnum.报名中).ToList());
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
        static int SQ = int.Parse(ConfigurationManager.AppSettings["sq"].ToString());
        static int Class = int.Parse(ConfigurationManager.AppSettings["class"].ToString());
        public ActionResult Index3()
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
           
            return View();
        }
        public ActionResult Index4(int Id)
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            ViewBag.SqList = ValueSetService.GetValueList(SQ, true);
            XinYuan entity = new XinYuan();
            string type = "View";
            if (Id == 0)
            {
                type = "Edit";
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = XinYuan.GetSingle(Id);
                entity.ShowNumber = entity.ShowNumber + 1;
                string msg = string.Empty;
                entity.Save(out msg);
            }
            ViewBag.Type = type;

            return View(entity);
        }

        public ActionResult Index5(int Id)
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            ViewBag.SqList = ValueSetService.GetValueList(SQ, true);
            XinYuan entity = new XinYuan();
            string type = "View";
            if (Id == 0)
            {
                type = "Edit";
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = XinYuan.GetSingle(Id);
                entity.ShowNumber = entity.ShowNumber + 1;
                string msg = string.Empty;
                entity.Save(out msg);
            }
            ViewBag.Type = type;

            return View(entity);
        }

        public ActionResult Index6(int Id)
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            ViewBag.SqList = ValueSetService.GetValueList(SQ, true);
            ChangDi entity = new ChangDi();
            string type = "View";
            if (Id == 0)
            {
                type = "Edit";
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = ChangDi.GetSingle(Id);

            }
            ViewBag.Type = type;

            return View(entity);
        }

        public ActionResult Index7(int Id)
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            ViewBag.SqList = ValueSetService.GetValueList(SQ, true);
            HuoDong entity = new HuoDong();
            entity = HuoDong.GetSingle(Id);
            return View(entity);
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


        [HttpPost]
        public JsonResult XinYuanRenLing(int xinyuanid,string name,string telephone,string weixinOpenId)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                XinYuanRenLing renling = new Core.Entity.XinYuanRenLing();
                renling.InputTime = DateTime.Now;
                renling.Name = name;
                renling.Telephone = telephone;
                renling.UserWeixinOpenId = weixinOpenId;
                renling.XinyuanId = xinyuanid;
                renling.Save(out msg);
                
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
