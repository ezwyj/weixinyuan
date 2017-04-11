using DogNet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using Common;
using Common.Entity;

namespace Core.Entity
{
    [RepositoryEntity(DefaultConnName = "DB")]
    [PetaPoco.TableName("HuoDong")]
    [PetaPoco.PrimaryKey("Id")]
    public class HuoDong : DogNet.Repositories.Repository<HuoDong>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Address { get; set; }
        public string Content { get; set; }
        public int Max { get; set; }
        public string Image { get; set; }
        public string SQ { get; set; }
        public StatusEnum Status { get; set; }
        public string InputName { get; set; }
        public DateTime InputTime { get; set; }
        [Ignore]
        public string StateExp
        {
            get
            {
                return Enum.GetName(typeof(StatusEnum), Status);
            }
        }
       
        [Ignore]
        public string InputTimeExp { get { return InputTime.ToString();  } }
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

        [Ignore]
        public List<Account.Entity.UserEntity> AddUser { get; set; }
        [Ignore]
        public List<Account.Entity.UserEntity> AttionUser { get; set; }
        [Ignore]
        public string SQExp
        {
            get
            {
                return !string.IsNullOrEmpty(SQ) ? ValueSet.Entity.ValueSetEntity.GetSingle(SQ).Text : "";
            }
        }

        [Ignore]
        public List<HuoDongAdd> Add
        {
            get { return HuoDongAdd.GetListByProperty(a => a.HuoDongId, Id); }
        }

        [Ignore]
        public List<HuoDongAttion> Attion
        {
            get { return HuoDongAttion.GetListByProperty(a => a.HuoDongId, Id); }
        }

    }
}
