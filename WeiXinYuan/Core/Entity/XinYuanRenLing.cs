using DogNet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Account.Entity;

namespace Core.Entity
{
    [RepositoryEntity(DefaultConnName = "DB")]
    [PetaPoco.TableName("XinYuanRenLing")]
    [PetaPoco.PrimaryKey("Id")]
    public class XinYuanRenLing : DogNet.Repositories.Repository<XinYuanRenLing>
    {
        public int Id { get; set; }
        public int XinyuanId { get; set; }
        public string UserWeixinOpenId { get; set; }
        public DateTime InputTime { get; set; }

        public string Name 
        {
            get;set;
        }
        public string Telephone { get; set; }

        public bool State { get; set; }
        [Ignore]
        public string StateExp
        {
            get {
                if (State != null)
                {
                    return State == true ? "已发放" : "未发放";
                }
                else
                {
                    return "";
                }
                

            }
        }
    }
}
