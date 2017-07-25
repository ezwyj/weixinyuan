using Common.Util;
using Core.Entity;
using Newtonsoft.Json;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValueSet.Service;

namespace Web.Controllers
{
    public class MobileController : Controller
    {
        
        private OAuthAccessTokenResult GetOAuthAccessTokenResult(string code, string state, out string msg)
        {
            if (string.IsNullOrEmpty(code))
            {
                msg = "no Code";
                return null;
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                msg = "no state";
                return null;
            }

            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                msg = ex.Message;

            }
            if (result.errcode != ReturnCode.请求成功)
            {
                msg = "错误：" + result.errmsg;

            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;
            msg = "OK";
            return result;
        }

        static int SQ = int.Parse(ConfigurationManager.AppSettings["sq"].ToString());
        static int Class = int.Parse(ConfigurationManager.AppSettings["class"].ToString());
        //
        // GET: /Mobile/
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];

        public ActionResult Index(string sq="00")
        {
            string msg = string.Empty;
            



            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            ViewBag.SelectSq = sq;
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            var retRecord = XinYuan.GetListByProperty(a => a.Status, (int)StatusEnum.报名中);
            if (sq != "00" && !string.IsNullOrEmpty(sq))
            {
                retRecord = retRecord.Where(b => b.SQ == sq).ToList();
            }
            return View(retRecord);
        }

        public ActionResult Index1(string sq)
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
            ViewBag.SelectSq = sq;
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            var retRecord = HuoDong.GetListByProperty(a => a.Status, (int)StatusEnum.报名中).ToList();
            if (sq != "00" && !string.IsNullOrEmpty(sq))
            {
                retRecord = retRecord.Where(b => b.SQ == sq).ToList();
            }
            return View(retRecord);
        }
        public ActionResult Index2(string sq)
        {
            var jsEvn = JSSDKHelper.GetJsSdkUiPackage(appId, secret, Request.Url.AbsoluteUri);
            ViewBag.AppId = jsEvn.AppId;
            ViewBag.Timestamp = jsEvn.Timestamp;
            ViewBag.NonceStr = jsEvn.NonceStr;
            ViewBag.Signature = jsEvn.Signature;
 
            ViewBag.SelectSq = sq;
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            var retRecord = ChangDi.GetList();
            if (sq != "00" && !string.IsNullOrEmpty(sq))
            {
                retRecord = retRecord.Where(b => b.SQ == sq).ToList();
            }
            return View(retRecord);
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
            HuoDong entity = new HuoDong();
            string type = "View";
            if (Id == 0)
            {
                type = "Edit";
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = HuoDong.GetSingle(Id);
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

        [HttpPost]
        public JsonResult HuoDonggz(int huodongid, string name, string telephone, string weixinOpenId)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                HuoDongAttion attion = new HuoDongAttion();
                attion.InputTime = DateTime.Now;
                attion.Name = name;
                attion.HuoDongId = huodongid;
                attion.WeixinOpenId = weixinOpenId;
                attion.Save(out msg);

            }
            catch (Exception e)
            {
                state = false;
                msg = e.Message;
            }

            return new JsonResult { Data = new { state = state, msg = msg } };
        }

        [HttpPost]
        public JsonResult HuoDongAdd(int huodongid, string name, string telephone, string weixinOpenId)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                HuoDongAdd huodong = new Core.Entity.HuoDongAdd();
                huodong.InputTime = DateTime.Now;
                huodong.Name = name;
                huodong.Telephone = telephone;
                huodong.weixinOpenId = weixinOpenId;
                huodong.HuoDongId = huodongid;
                huodong.Save(out msg);

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
