//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        数据转换
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GGGETSAdmin.Common
{
    /// <summary>
    /// 数据转换
    /// </summary>
    public  sealed  class DataConversion
    {
        /// <summary>
        /// 把枚举转换为数组
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static IList ListTypeForEnum(Type enumType)
        {
            var list = new ArrayList();
            foreach (int i in Enum.GetValues(enumType))
            {
                var listitem = new ListItem(Enum.GetName(enumType, i), i.ToString());
                list.Add(listitem);
            }
            return list;
        }

        /// <summary>
        /// 根据权限数组获取当前权限
        /// </summary>
        /// <param name="privileges">权限数组</param>
        /// <returns></returns>
        public static int GetCurrentPrivilege(int[] privileges)
        {
            var count = privileges.Count();
            var value = 0;
            for(var k=0;k<count;k++)
            {
                value = value | privileges[k];
            }
            return value;
        }

        /// <summary>
        /// 根据名字得到值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="name">名字</param>
        /// <returns></returns>
        public static int? GetValue(Type enumType, string name)
        {
            int? value=null;
            foreach (int i in Enum.GetValues(enumType))
            {
                var tempName = Enum.GetName(enumType, i);
                if (name != tempName) continue;
                value = i;
                break;
            }
            return value;
        }

    }
}