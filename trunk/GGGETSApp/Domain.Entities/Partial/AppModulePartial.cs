//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        模块
// 作成者				hong.li
// 改版日				2011.02.16
// 改版内容				创建并且修改
//************************************************************************

using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace ETS.GGGETSApp.Domain.Application.Entities
{
    #region 模块以及权限枚举定义
    ///<summary>
    ///</summary>
    public enum NodeType
    {
        ///<summary>
        /// 父节点
        ///</summary>
        父节点=0,
        ///<summary>
        /// 子节点
        ///</summary>
        子节点=1,
        /// <summary>
        /// 所有
        /// </summary>
        所有=3
    }

    /// <summary>
    /// 权限
    /// </summary>
   public enum Privilege
   {
       ///<summary>
       /// 查询
       ///</summary>
       部分查询 = 512,

       ///<summary>
       /// 查询
       ///</summary>
       查询 = 256,
       /// <summary>
       /// 修改
       /// </summary>
       添加 = 128,
       
       ///<summary>
       ///</summary>
       修改 = 64,
       
       ///<summary>
       ///</summary>
       删除 = 32,

       ///<summary>
       ///</summary>
       导出 = 16,
     
       ///<summary>
       ///</summary>
       打印 = 8,
       ///
        ///
       解锁=4
   }

    #endregion
    /// <summary>
    /// 模块
    /// </summary>
   public  partial class AppModule
    {
    }

   /// <summary>
   /// 模块权限
   /// </summary>
   public class ModulePrivilege
   {

       private static readonly Dictionary<string, string> _array = new Dictionary<string, string>();

       /// <summary>
       /// 构造函数
       /// </summary>
       static ModulePrivilege()
       {
           if (_array.Count == 0)
           {
               _array.Add(Privilege.查询.ToString(), "QueryPrivilege");
               _array.Add(Privilege.部分查询.ToString(), "SearcherPartialPrivilege");
               _array.Add(Privilege.打印.ToString(), "PrintPrivilege");
               _array.Add(Privilege.导出.ToString(), "ExportPrivilege");
               _array.Add(Privilege.删除.ToString(), "DeletePrivilege");
               _array.Add(Privilege.添加.ToString(), "AddPrivilege");
               _array.Add(Privilege.修改.ToString(), "UpdatePrivilege");
               _array.Add(Privilege.解锁.ToString(), "DeblockingPrivilege");
           }
       }
       ///<summary>
       /// 索引
       ///</summary>
       ///<param name="name"></param>
       public bool? this[string name]
       {
           get
           {
              var names = Enum.GetNames(typeof(Privilege)).ToList();
              if (!names.Contains(name)) throw new ArgumentException("传入参数不对");
               bool? result = null;
               switch(name)
               {
                   case "查询":
                       result = QueryPrivilege;
                       break;
                   case "添加":
                       result = AddPrivilege;
                       break;
                   case "修改":
                       result = UpdatePrivilege;
                       break;
                   case "删除":
                       result = DeletePrivilege;
                       break;
                   case "导出":
                       result = ExportPrivilege;
                       break;
                   case "打印":
                       result = PrintPrivilege;
                       break;
                   case "部分查询":
                       result = SearcherPartialPrivilege;
                       break;
                   case "解锁":
                       result = DeblockingPrivilege;
                       break;

                   
               }
               return result;
           }
       }
       /// <summary>
       /// 模块名
       /// </summary>
       public string ModuleName
       {
           get;
           set;
       }

       /// <summary>
       /// Url地址
       /// </summary>
       public string Url
       {
           get;
           set;
       }

       /// <summary>
       /// 查询权限
       /// </summary>
       private bool? QueryPrivilege
       {
           get;
           set;
       }


       /// <summary>
       /// 删除权限
       /// </summary>
       private bool? DeletePrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 修改权限
       /// </summary>
       private bool? UpdatePrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 添加权限
       /// </summary>
       private bool? AddPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 导出权限
       /// </summary>
       private bool? ExportPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 打印权限
       /// </summary>
       private bool? PrintPrivilege
       {
           get;
           set;
       }


       /// <summary>
       /// 查询部分权限
       /// </summary>
       private bool? SearcherPartialPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 权限字符集
       /// </summary>
       public int? PrivilegeDesc
       {
           get;
           set;
       }
       /// <summary>
       /// 解锁权限
       /// </summary>
       private bool? DeblockingPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 设置权限的值
       /// </summary>
       /// <param name="value">值</param>
       /// <param name="name">名字</param>
       public void SetPrivilege(bool? value, string name)
       {
           if (!_array.ContainsKey(name)) return;
           var filedName = _array[name];
           var type = GetType();
           var property = type.GetProperty(filedName, BindingFlags.NonPublic | BindingFlags.IgnoreCase);
           property.SetValue(this, value, null);
       }
   }
}
