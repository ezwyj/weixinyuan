using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using DogNet.Repositories;


namespace Account.Entity
{
    [RepositoryEntity(DefaultConnName = "EHR")]
    [PetaPoco.TableName("yuangong")]
    [PetaPoco.PrimaryKey("gh")]
    public class UserEntity : Repository<UserEntity>
    {
        [PetaPoco.Column("gh")]
        public int UserId { get; set; }
        [PetaPoco.Column("xingming")]
        public string Name { get; set; }
        [PetaPoco.Column("CompanyMail")]
        public string Email { get; set; }

        [PetaPoco.Ignore]
        public string Badge
        {
            get
            {
                return UserId.ToString().PadLeft(6, '0');
            }
        }
        //[PetaPoco.Ignore]
        //public string dep
        //{
        //    get
        //    {
        //        Database db = new Database("EHR");
        //        string sql = "select b.mingcheng as dep from yuangong y, bm b, yg_bm yb where y.gh='" + UserId + "' and y.ygid=yb.ygid and b.id=yb.bmid";
        //        return db.Fetch<string>(sql).FirstOrDefault();
        //    }
        //}
    }
}
