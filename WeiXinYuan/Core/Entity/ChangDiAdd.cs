using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using DogNet.Repositories;

namespace Core.Entity
{
    [RepositoryEntity(DefaultConnName = "DB")]
    [PetaPoco.TableName("ChangDiADD")]
    [PetaPoco.PrimaryKey("Id")]
    public class ChangDiAdd :Repository<ChangDiAdd>
    {
        public int Id { get; set; }
        public string WeixinOpenId { get; set; }
        public string Name { get; set; }
        public DateTime InputTime { get; set; }
        public int ChangDiId { get; set; }
        [Ignore]
        public string InputTimeExp { get { return InputTime.ToString();  } }
    }
}
