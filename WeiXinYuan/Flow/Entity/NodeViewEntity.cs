using DogNet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Entity
{
    [RepositoryEntity(DefaultConnName = "Flow")]
    [PetaPoco.TableName("Flow_node_view")]
    [PetaPoco.PrimaryKey("Id")]
    public class NodeViewEntity : Repository<NodeViewEntity>
    {
        public int Id { get; set; }
        public string FlowId { get; set; }
        public string NodeId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public static NodeViewEntity GetView(string flowId, string nodeId)
        {
            string sql = "select * from Flow_node_view where FlowId = '{0}' and NodeId = '{1}'";
            sql = string.Format(sql, flowId, nodeId);

            return DefaultDB.Fetch<NodeViewEntity>(sql).FirstOrDefault();
        }
    }
}
