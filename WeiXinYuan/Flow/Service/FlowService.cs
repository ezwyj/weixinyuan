using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogNet.Flow.Model;
using DogNet.Flow.H3;
using Flow.Model;
using System.Configuration;
using Log.Service;
using Log.Entity;


namespace Flow.Service
{
    public class FlowService
    {
        public static string flowCode = ConfigurationManager.AppSettings["FlowCode"];
        public static H3FlowService h3FlowService = new H3FlowService(flowCode);
        /// <summary>
        /// 获取节点审批历史信息
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static List<FlowNodeModel> GetNodeHistoryList(string instanceId)
        {
            H3Api h3Api = new H3Api();
            string flowCode = h3Api.GetInstance(instanceId).WorkflowCode;
            H3FlowService h3Service = new H3FlowService(flowCode);

            List<FlowNodeModel> historyList = new List<FlowNodeModel>();
            var currentNode = h3Api.CurrentActivity(instanceId).FirstOrDefault();
            var activityList = h3Api.GetActivties(instanceId);

            foreach (var activity in activityList)
            {
                if (activity.IsApproveActivity)
                {
                    FlowNodeModel flowNode = new FlowNodeModel();

                    flowNode.FlowId = flowCode;
                    flowNode.NodeId = activity.ActivityCode;
                    flowNode.NodeName = activity.DisplayName;
                    flowNode.ApproveHistoryList = h3Service.GetWorkItemHistoryList(instanceId, flowNode.NodeName);
                    flowNode.Approvers = h3Service.GetApproves(instanceId);

                    if (activity.ActivityCode == currentNode)
                    {
                        flowNode.IsCurrentNode = true;
                    }

                    historyList.Add(flowNode);
                }

                if (activity.ActivityCode == currentNode)
                {
                    break;
                }
            }

            //foreach (var node in nodeList)
            //{

            //    if (node != "Activity2")
            //    {
            //        FlowNodeModel flowNode = new FlowNodeModel();

            //        flowNode.FlowId = flowCode;
            //        flowNode.NodeId = node;
            //        flowNode.NodeName = h3Api.GetNodeName(instanceId, node);
            //        flowNode.ApproveHistoryList = h3Service.GetWorkItemHistoryList(instanceId, flowNode.NodeName);
            //        flowNode.Approvers = h3Service.GetApproves(instanceId);

            //        if (node == currentNode)
            //        {
            //            flowNode.IsCurrentNode = true;
            //        }

            //        historyList.Add(flowNode);   
            //    }          
            //}

            return historyList;
        }



        /// <summary>
        /// 获取所有节点信息
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static List<FlowNodeModel> GetAllNodeList(string instanceId)
        {
            H3Api h3Api = new H3Api();

            List<FlowNodeModel> nodeList = new List<FlowNodeModel>();
            var activityList = h3Api.GetActivties(instanceId);
            var currentActivity = h3Api.CurrentActivity(instanceId).FirstOrDefault();

            //将结束节点放到最后
            var endActivity = activityList[1];
            activityList.RemoveAt(1);
            activityList.Add(endActivity);

            bool isApproved = true;

            foreach (var activity in activityList)
            {
                FlowNodeModel node = new FlowNodeModel();

                node.NodeId = activity.ActivityCode;
                node.NodeName = activity.DisplayName;
                node["ActivityType"] = activity.ActivityType;

                if (activity.ActivityCode == currentActivity)
                {
                    node.IsCurrentNode = true;
                    isApproved = false;
                }

                node["IsApproved"] = isApproved;

                nodeList.Add(node);
            }

            return nodeList;
        }

        /// <summary>
        /// 获取流程对象的属性值
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetProperty(string instanceId, string key)
        {
            H3Api h3Api = new H3Api();
            string flowCode = h3Api.GetInstance(instanceId).WorkflowCode;
            H3FlowService h3Service = new H3FlowService(flowCode);

            return h3Service.GetPropertyList(instanceId)[key];
        }


    }
}
