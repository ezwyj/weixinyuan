using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PetaPoco;
using Account.Entity;

namespace Account.Service
{
    public class UserService
    {
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        /// <returns></returns>
        public static UserEntity GetCurrUser()
        {
            string badge = HttpContext.Current.User.Identity.Name;

            return UserEntity.GetSingle(badge);
        }

        /// <summary>
        /// 获取员工部门
        /// </summary>
        /// <param name="badge"></param>
        /// <returns></returns>
        public static string getDep(string badge)
        {
            Database db = new Database("EHR");
            string sql = "select b.mingcheng as dep from yuangong y, bm b, yg_bm yb where y.gh='" + badge + "' and y.ygid=yb.ygid and b.id=yb.bmid";

            return db.Fetch<string>(sql).FirstOrDefault();
        }


        /// <summary>
        /// 获取用户邮件（多用户）
        /// </summary>
        /// <param name="badgeList"></param>
        /// <returns></returns>
        public static string GetUserMail(List<string> badgeList)
        {
            List<string> mailList = new List<string>();

            foreach (var badge in badgeList)
            {
                mailList.Add(UserEntity.GetSingle(badge).Email + "@mail.maipu.com");
            }

            return string.Join(",", mailList.ToArray());
        }

        /// <summary>
        /// 获取用户邮件（单用户）
        /// </summary>
        /// <param name="badgeList"></param>
        /// <returns></returns>
        public static string GetUserMail(string badge)
        {
            List<string> badgeList = new List<string>();
            badgeList.Add(badge);

            return GetUserMail(badgeList);
        }

        /// <summary>
        /// 根据工号和姓名搜索用户，模糊查询
        /// </summary>
        /// <param name="badge"></param>
        /// <param name="name"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static List<UserEntity> GetUserListByFilter(string badge, string name, int top = 10)
        {
            string sql = string.Empty;

            try
            {
                badge = int.Parse(badge).ToString();
            }
            catch (Exception e) { }

            if (top == 0)
            {
                sql = @"select * from yuangong where gh like '%{0}%' or xingming like '%{1}%'";
            }
            else
            {
                sql = @"select top " + top + " * from yuangong where gh like '%{0}%' or xingming like '%{1}%'";
            }


            //sql = string.Format(sql, badge, name);

            object[] sqlPara = new object[2] { badge, name };


            return UserEntity.DefaultDB.Fetch<UserEntity>(sql, sqlPara);
        }

        /// <summary>
        /// 获取多个工号对应的员工名称
        /// </summary>
        /// <param name="badges"></param>
        /// <returns></returns>
        public static string GetUserNameByBadge(string badges)
        {
            List<string> badgeList = badges.Split(',').ToList();
            List<string> nameList = new List<string>();

            foreach (var badge in badgeList)
            {
                var user = UserEntity.GetSingle(badge);

                if (user != null)
                {
                    nameList.Add(user.Name);
                }
            }

            return string.Join(",", nameList);
        }
    }
}
