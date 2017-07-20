using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogNet.Repositories;
using PetaPoco;
using Account.Entity;
using Common.Entity;

namespace Core.Entity
{
    [RepositoryEntity(DefaultConnName = "DB")]
    [PetaPoco.TableName("XinYuan")]
    [PetaPoco.PrimaryKey("Id")]
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
        public int Number { get; set; }
        /// <summary>
        /// 认领人数
        /// </summary>
        [Ignore]
        public int RLRS { get
            {
                return RLR.Count;
            } }

        public string Image { get; set; }
        [Ignore]
        public List<Common.Entity.AttachmentEntity> Images
        {
            get
            {
                if (!string.IsNullOrEmpty(Image))
                {
                    List<AttachmentEntity> attachmentList = new List<AttachmentEntity>();
                    List<string> fileIdList = Image.Split(',').ToList();

                    foreach (var fileId in fileIdList)
                    {
                        AttachmentEntity file = AttachmentEntity.GetSingle(int.Parse(fileId));

                        if (file != null)
                        {
                            attachmentList.Add(file);
                        }
                    }
                    return attachmentList;
                }
                else
                {
                    return null;
                }
            }

        }

        public string Name { get; set; }

        //发布人帐号
        public string PublishWeixinOpenId { get; set; }

        public string Telephone { get; set; } 

        public StatusEnum Status { get; set; }
        [Ignore]
        public string StateExp { get
            {
                return Enum.GetName(typeof(StatusEnum), Status);
            }
        }
        public DateTime InputTime { get; set; }

        [Ignore]
        public string InputTimeExp
        {
            get { return InputTime.ToString(); }
        }

        [Ignore]
        public string SQExp { get {
                return !string.IsNullOrEmpty(SQ)?ValueSet.Entity.ValueSetEntity.GetSingle(SQ).Text:"";
            } }
        /// <summary>
        /// 认领人
        /// </summary>
        [Ignore]
        public List<XinYuanRenLing> RLR
        {
            get
            {
                return XinYuanRenLing.GetListByProperty(a => a.XinyuanId, Id);
                

            }
        }
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

