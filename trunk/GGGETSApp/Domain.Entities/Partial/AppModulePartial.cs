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
       打印 = 8
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
       public bool? QueryPrivilege
       {
           get;
           set;
       }


       /// <summary>
       /// 删除权限
       /// </summary>
       public bool? DeletePrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 修改权限
       /// </summary>
       public bool? UpdatePrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 添加权限
       /// </summary>
       public bool? AddPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 导出权限
       /// </summary>
       public bool? ExportPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 打印权限
       /// </summary>
       public bool? PrintPrivilege
       {
           get;
           set;
       }

       /// <summary>
       /// 权限字符集
       /// </summary>
       public int? Privilege
       {
           get;
           set;
       }
   }
}
