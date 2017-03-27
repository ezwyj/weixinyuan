using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogNet.Repositories;

namespace Core.Entity
{
    public class XinYuan :Repository<XinYuan> 
    {
        public int Id { get; set; }
        /// <summary>
        /// 理由
        /// </summary>
        public string LY { get; set; }

        public string Title { get; set; }
        public string Class { get; set; }

        public string SQ { get; set; }
        public int ShowNumber { get; set; }
        /// <summary>
        /// 认领人数
        /// </summary>
        public int RLRS { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        //发布人帐号
        public int PublishId { get; set; }

        public string Telephone { get; set; } 

        public StatusEnum Status { get; set; }

        public string StateExp { get
            {
                return Enum.GetName(typeof(StatusEnum), Status);
            }
        }
        public DateTime BegingTime { get; set; }
        public DateTime EndTime { get; set; }

    }

    public enum StatusEnum
    {
        草稿 =0,
        提交 =1,
        确认中=2,
        已确认=3,
        报名中,
        已报名,
        结束


    }
}

