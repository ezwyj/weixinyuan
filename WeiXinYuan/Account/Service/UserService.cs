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
            string name = HttpContext.Current.User.Identity.Name;

            return UserEntity.GetListByProperty(a => a.Name, name).SingleOrDefault();
        }

        


        


    }
}
