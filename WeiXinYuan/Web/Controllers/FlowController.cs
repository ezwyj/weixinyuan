using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.Controllers
{
    public class FlowController : Controller
    {
        // GET: 审批页
        public ActionResult Approve(string instanceId)
        {
            string badge = HttpContext.User.Identity.Name;
            var nodeList = Flow.Service.FlowService.GetNodeHistoryList(instanceId);

            ViewBag.instanceId = instanceId;

            return View(nodeList);
        }

        // GET: 审批进度
        public ActionResult ApproveProcess(string instanceId)
        {
            return View(Flow.Service.FlowService.GetAllNodeList(instanceId));
        }

    }
}
