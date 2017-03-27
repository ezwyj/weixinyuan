using DogNet.Flow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Model
{
    public class FlowNodeModel : Dictionary<string, object>
    {
        public string FlowId { get; set; }
        public string NodeId { get; set; }
        public string NodeName { get; set; }
        public bool IsCurrentNode { get; set; }
        public string Approvers { get; set; }
        public List<QueryWorkItemHistoryEntity> ApproveHistoryList { get; set; }

        public new object this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
