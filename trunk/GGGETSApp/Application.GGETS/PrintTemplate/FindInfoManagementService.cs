//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模板参数维护BLL
// 作成者				ZhiWei.Shen
// 改版日				2011.03.18
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Domain.GGGETS;
using ETS.GGGETSApp.Domain.Core;
using ETS.GGGETSApp.Domain.Application.Entities;
using ETS.GGGETSApp.Domain.Core.Specification;

namespace Application.GGETS
{
    public class FindInfoManagementService:IFindInfoManagementService
    {
        /// <summary>
        /// 通过表名获取所有的列信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public IList<FindInfo> FindAllByTableName(string tableName)
        {
            string SQLStr = string.Format("select * from findinfo where name='{0}'", tableName);
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, SQLStr);
            //todo 反射
        }
    }
}
