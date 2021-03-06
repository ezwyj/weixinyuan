﻿using Common.Util;
using Core.Entity;
using PetaPoco;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValueSet.Service;

namespace Web.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/
        static int SQ = int.Parse(ConfigurationManager.AppSettings["sq"].ToString());
        static int Class = int.Parse(ConfigurationManager.AppSettings["class"].ToString());
        public ActionResult XinYuanIndex()
        {
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);

            return View();
        }
        public JsonResult XinYuanSearchList(int pageIndex, int pageSize,string sq,string name)
        {
            bool state = true;
            string msg = string.Empty;
            long total = 0;
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            List<XinYuan> serachList = null;
            //if (HttpContext.Current.User.Identity.Name!="admin")
            //{
            //  //  sq =  
            //}
            msg = "";
            Page<XinYuan> Page = null;
            try
            {
                Sql sql = new Sql();
                sql.Append("select * from xinyuan with(nolock) where 1=1 ");
                if (!string.IsNullOrEmpty(sq)) sql.Append(" and sq like @0", "%"+sq + "%");
                if (!string.IsNullOrEmpty(name)) sql.Append(" and name like @0", "%" + name + "%");
                Page = XinYuan.DefaultDB.Page<XinYuan>(pageIndex, pageSize, sql);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            total = Page.Items == null ? 0 : Page.TotalItems;
            serachList = Page.Items;


            return new JsonResult { Data = new { state = state, msg = msg, data = serachList, total = total }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult XinYuanDetail(int id)
        {
            XinYuan entity = null;
            if (id == 0)
            {
                entity = new XinYuan();
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = Core.Entity.XinYuan.GetSingle(id);
            }
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            List<ValueSet.Entity.ValueSetEntity> statelist = new List<ValueSet.Entity.ValueSetEntity>();
            int i = 0;
            foreach(var item in Enum.GetValues (typeof(StatusEnum ))){
                ValueSet.Entity.ValueSetEntity itemValueSet = new ValueSet.Entity.ValueSetEntity();
                itemValueSet.Value = i.ToString();
                itemValueSet.Text = item.ToString();
                statelist.Add(itemValueSet);
                i++;
            }
            ViewBag.stateList = statelist;
            return View(entity);
            
        }
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];
        private bool SavePicture(string name, out string msg)
        {
            try
            {
                MemoryStream front = new MemoryStream();
                Senparc.Weixin.MP.AdvancedAPIs.MediaApi.Get(AccessTokenContainer.TryGetAccessToken(appId, secret), name, front);


                var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
                string fileName = name + ".jpg";
                string savePath = Server.MapPath("~/Download/") + fileName;
                FileStream writer = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                front.WriteTo(writer);
                writer.Close();
                writer.Dispose();
                msg = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }
        }

        [HttpPost]
        public JsonResult XinYuanDetail(string dataJson)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                XinYuan postModel = Serializer.ToObject<XinYuan>(dataJson);
                
                postModel.Save(out msg);
                if (!string.IsNullOrEmpty(postModel.Image))
                {
                    string s = string.Empty;
                    SavePicture(postModel.Image, out s);
                }
            }
            catch (Exception e)
            {
                state = false;
                msg = e.Message;
            }

            return new JsonResult { Data = new { state = state, msg = msg } };
        }

        [HttpPost]
        public JsonResult XinYuanDelete(int id)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                XinYuan postModel = XinYuan.GetSingle(id);

                state=  postModel.Delete (out msg);
                
            }
            catch (Exception e)
            {
                state = false;
                msg = e.Message;
            }
            return new JsonResult { Data = new { state = state, msg = msg } };
        }

        public ActionResult HuoDongIndex()
        {
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            return View();
        }
        private Page<T> getSearch<T>(int pageIndex,int pageSize,string table ,string sq,string name,out string msg)
        {
            

            msg = "";
            Page<T> Page = null;
            try
            {
                Sql sql = new Sql();
                sql.Append("select * from "+ table +" with(nolock) where 1=1 ");
                if (!string.IsNullOrEmpty(sq)) sql.Append(" and sq like @0", "%" + sq + "%");
                if (!string.IsNullOrEmpty(name)) sql.Append(" and name like @0", "%" + name + "%");
                Page = XinYuan.DefaultDB.Page<T>(pageIndex, pageSize, sql);
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
           
            return Page;

        }

        

        public JsonResult HuoDongSearchList(int pageIndex, int pageSize, string sq, string name)
        {
            bool state = true;
            string msg = string.Empty;
            long total = 0;
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            Page<HuoDong> Page = getSearch<HuoDong>(pageIndex, pageSize,"HuoDong", sq, name,out msg );
            total = Page.Items == null ? 0 : Page.TotalItems;
            List<HuoDong> serachList = Page.Items;

            return new JsonResult { Data = new { state = state, msg = msg, data = serachList, total = total }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult HuoDongDetail(int id)
        {
            HuoDong entity = null;
            if (id == 0)
            {
                entity = new HuoDong();
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = Core.Entity.HuoDong.GetSingle(id);
            }
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            List<ValueSet.Entity.ValueSetEntity> statelist = new List<ValueSet.Entity.ValueSetEntity>();
            int i = 0;
            foreach (var item in Enum.GetValues(typeof(StatusEnum)))
            {
                ValueSet.Entity.ValueSetEntity itemValueSet = new ValueSet.Entity.ValueSetEntity();
                itemValueSet.Value = i.ToString();
                itemValueSet.Text = item.ToString();
                statelist.Add(itemValueSet);
                i++;
            }
            ViewBag.stateList = statelist;
            return View(entity);

        }

        [HttpPost]
        public JsonResult HuodongDelete(int id)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                HuoDong postModel = HuoDong.GetSingle(id);

                state = postModel.Delete(out msg);

            }
            catch (Exception e)
            {
                state = false;
                msg = e.Message;
            }
            return new JsonResult { Data = new { state = state, msg = msg } };
        }
        [HttpPost]
        public JsonResult HuoDongDetail(string dataJson)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                HuoDong postModel = Serializer.ToObject<HuoDong>(dataJson);
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
        public JsonResult ChangeDiDelete(int id)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                ChangDi postModel = ChangDi.GetSingle(id);

                state = postModel.Delete(out msg);

            }
            catch (Exception e)
            {
                state = false;
                msg = e.Message;
            }
            return new JsonResult { Data = new { state = state, msg = msg } };
        }
        public ActionResult ChangDiIndex()
        {
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            
            return View();
        }
        public JsonResult ChangDiSearchList(int pageIndex, int pageSize, string sq, string name)
        {
            bool state = true;
            string msg = string.Empty;
            long total = 0;
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            ViewBag.ClassList = ValueSetService.GetValueList(Class, true);
            Page<ChangDi> Page = getSearch<ChangDi>(pageIndex, pageSize,"ChangDi", sq, name, out msg);
            total = Page.Items == null ? 0 : Page.TotalItems;
            List<ChangDi> serachList = Page.Items;

            return new JsonResult { Data = new { state = state, msg = msg, data = serachList, total = total }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult ChangDiDetail(int id)
        {
            ChangDi entity = null;
            if (id == 0)
            {
                entity = new ChangDi();
                entity.InputTime = DateTime.Now;
            }
            else
            {
                entity = Core.Entity.ChangDi.GetSingle(id);
            }
            ViewBag.SQList = ValueSetService.GetValueList(SQ, true);
            ViewBag.ClassList = ValueSetService.GetValueList(Class, true);
            List<ValueSet.Entity.ValueSetEntity> statelist = new List<ValueSet.Entity.ValueSetEntity>();
           
            return View(entity);

        }

        [HttpPost]
        public JsonResult ChangDiDetail(string dataJson)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                string badge = HttpContext.User.Identity.Name;
                ChangDi postModel = Serializer.ToObject<ChangDi>(dataJson);
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
        public JsonResult ChangeXinYuanRenLingState(int id)
        {
            bool state = true;
            string msg = string.Empty;

            try
            {
                XinYuanRenLing entity = XinYuanRenLing.GetSingle(id);
                entity.State = true;
                entity.Save(out msg);
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
