using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogNet.Repositories;
using PetaPoco;

namespace Core.Entity
{
    [RepositoryEntity(DefaultConnName = "DB")]
    [PetaPoco.TableName("HuoDongAdd")]
    [PetaPoco.PrimaryKey("Id")]
    public class HuoDongAdd : Repository<HuoDongAdd>
    {
        public int Id { get; set; }
        public int HuoDongId { get; set; }
        public string weixinOpenId { get; set; }
        public string Name { get; set; }
        public DateTime InputTime { get; set; }
        public string Telephone { get; set; }
        [Ignore]
        public string InputTimeExp { get; set; }

    }
}
