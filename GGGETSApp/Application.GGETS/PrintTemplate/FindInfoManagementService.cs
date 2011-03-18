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
using System.Reflection;
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
            IList<FindInfo> list = new List<FindInfo>();
            string SQLStr = string.Format("select * from findinfo where name='{0}'", tableName);
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, SQLStr);
            //反射
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    FindInfo info = GetEntityByDR<FindInfo>(reader);
                    if (info != null)
                        list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 通过表和字段获取对象
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public FindInfo FindInfoByTableAndFieldName(string fieldName, string tableName)
        {
            FindInfo info = new FindInfo();
            string SQLStr = string.Format("select * from findinfo where name='{0}' and fieldname='{1}'", tableName,
                                          fieldName);
            SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, SQLStr);
            //反射
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    info = GetEntityByDR<FindInfo>(reader);
                }
            }
            return info;
        }

        /// <summary>
        /// 反射
        /// </summary>
        /// <typeparam name="T">抽象类</typeparam>
        /// <param name="dr">sqlreader</param>
        /// <returns></returns>
        public static T GetEntityByDR<T>(SqlDataReader dr)
        {
            //获取该对象的类型
            Type type = typeof(T);
            T destObj = Activator.CreateInstance<T>();
            //获取该类型中存在属性名称
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if ((dr[prop.Name] != null) && (dr[prop.Name] != DBNull.Value))
                    {
                        SetPropertyValue(prop, destObj, dr[prop.Name]);
                    }
                }
                catch
                {
                }
            }
            return destObj;
        }

        private static void SetPropertyValue(PropertyInfo prop, object destObj, object value)
        {
            object temp = ChangeType(prop.PropertyType, value);
            prop.SetValue(destObj, temp, null);
        }

        private static object ChangeType(Type type, object value)
        {
            if ((value == null) && type.IsGenericType)
            {
                return Activator.CreateInstance(type);
            }
            if (value == null)
            {
                return null;
            }
            if (type != value.GetType())
            {
                if (type.IsEnum)
                {
                    if (value is string)
                    {
                        return Enum.Parse(type, value as string);
                    }
                    return Enum.ToObject(type, value);
                }
                if ((type == typeof(bool)) && typeof(int).IsInstanceOfType(value))
                {
                    return (int.Parse(value.ToString()) != 0);
                }
                if (!(type.IsInterface || !type.IsGenericType))
                {
                    Type type1 = type.GetGenericArguments()[0];
                    object obj1 = ChangeType(type1, value);
                    return Activator.CreateInstance(type, new object[] { obj1 });
                }
                if ((value is string) && (type == typeof(Guid)))
                {
                    return new Guid(value as string);
                }
                if ((value is string) && (type == typeof(Version)))
                {
                    return new Version(value as string);
                }
                if (value is IConvertible)
                {
                    return Convert.ChangeType(value, type);
                }
            }
            return value;
        }
    }
}
