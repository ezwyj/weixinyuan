using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using DogNet.Repositories;
using Common.Entity;

namespace Core.Entity
{
    [RepositoryEntity(DefaultConnName = "DB")]
    [PetaPoco.TableName("ChangDi")]
    [PetaPoco.PrimaryKey("Id")]
    public class ChangDi : Repository<ChangDi>
    {
        public int Id { get; set; }
        public string SQ { get; set; }
        [Ignore]
        public string SQExp { get {
                 return !string.IsNullOrEmpty(SQ) ? ValueSet.Entity.ValueSetEntity.GetSingle(SQ).Text : "";
            } }
        public string Title { get; set; }
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

        public int Max { get; set; }
        public string CanUseTime { get; set; }
        public string Class { get; set; }
        [Ignore]
        public string ClassExp
        {
            get { return !string.IsNullOrEmpty(Class) ? ValueSet.Entity.ValueSetEntity.GetSingle(Class).Text : ""; }
        }

        public string Money { get; set; }
        public string Address { get; set; }

        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Content { get; set; }
        public DateTime InputTime { get; set; }
        [Ignore]
        public string InputTimeExp { get { return InputTime.ToString(); } }
        public string InputName { get; set; }
        public int UseTime { get; set; }

        [Ignore]
        public List<ChangDiAdd> Add
        {
            get { return ChangDiAdd.GetListByProperty(a => a.ChangDiId, Id); }
        }
    }
}
