//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        XMl帮助文档
// 作成者				hong.li
// 改版日				2011.02.15
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using ETS.GGGETSApp.Domain.Application.Entities;

namespace GGGETSWeb
{
    public class XmlHelp
    {
        #region 字段
        private XmlDocument _xmlDoc;//XMl操作
        private readonly string _directoryPath;//放用户登录的XML文件目录
        private readonly string _filePath;//文件路径
        #endregion

        #region 构造函数
        public XmlHelp(string filePath)
        {
            if ( string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("文件地址不能空");
            }
            _filePath = filePath;
            try
            {
                if (!File.Exists(_filePath))
                {
                    var length = filePath.LastIndexOf("\\") + 1;
                    _directoryPath = filePath.Substring(0, length);
                    if (!Directory.Exists(_directoryPath))
                    {
                        Directory.CreateDirectory(_directoryPath);
                    }
                    CreateXml(_filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 创建用户登录菜单文件
        /// </summary>
        /// <param name="filePath"></param>
        private void CreateXml(string filePath)
        {
            _xmlDoc = new XmlDocument();
            if (File.Exists(filePath)) return;
            var xmlelem = _xmlDoc.CreateElement("", "ul", "");
            xmlelem.SetAttribute("Create", DateTime.Now.ToString("yyyy-MM-dd"));
            xmlelem.SetAttribute("id", "menu2");
            xmlelem.SetAttribute("class", "menu");
            _xmlDoc.AppendChild(xmlelem);
            _xmlDoc.Save(filePath);
        }

        /// <summary>
        /// 加载XML文件
        /// </summary>
        private void LoadXml()
        {
            if (_xmlDoc == null) _xmlDoc = new XmlDocument();
            _xmlDoc.Load(_filePath);
        }

        /// <summary>
        /// 保存Xml
        /// </summary>
        private void SaveXml()
        {
            _xmlDoc.Save(_filePath);
        }

        /// <summary>
        /// 创建模块节点
        /// </summary>
        /// <param name="appModuleList"></param>
        private void CreateModele(IList<AppModule> appModuleList)
        {
            if (appModuleList == null || appModuleList.Count == 0) return;
            var root = _xmlDoc.SelectSingleNode("/ul");
            foreach (var module in appModuleList)
            {
                var item = _xmlDoc.CreateElement("li");
                var subItem = _xmlDoc.CreateElement("a");
                subItem.SetAttribute("href", "#");
                subItem.SetAttribute("title", module.Description);
                subItem.InnerText = module.Description;
                item.AppendChild(subItem);
                root.AppendChild(item);
            }
            SaveXml();
        }

        /// <summary>
        /// 获取子菜单数据
        /// </summary>
        /// <param name="subMenu">子菜单数据</param>
        /// <param name="moduleArry"></param>
        /// <param name="privilegList"></param>
        private static void GetSubMenu(Hashtable subMenu, IEnumerable<AppModule> moduleArry, IEnumerable<AppModule> privilegList)
        {
            if (subMenu == null || moduleArry == null || privilegList == null) return;
            foreach (var appModule in moduleArry)
            {
                var module = appModule;
                var subMenuArry = privilegList.Where(p => p.ParentId == module.ModuleID).ToList();
                if (!subMenu.ContainsKey(module.Description))
                {
                    subMenu.Add(module.Description, subMenuArry);
                }
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 生成菜单XML
        /// </summary>
        /// <param name="privilegList">权限列表</param>
        public void CreateUserMenuForXml(IList<AppModule> privilegList)
        {
            if (privilegList == null || privilegList.Count == 0) return;
            LoadXml();
            var moduleArry = privilegList.Where(it=>it.IsLeft==false).ToList();
            if (moduleArry.Count == 0) return;
            CreateModele(moduleArry);
            var subMenu = new Hashtable();
            GetSubMenu(subMenu, moduleArry, privilegList);
            if (subMenu.Count == 0) return;
            foreach (var key in subMenu.Keys)
            {
                var currentArry = (IList<AppModule>)subMenu[key];
                var currentNode = _xmlDoc.SelectSingleNode(string.Format("//a[@title='{0}']", key));
                if (currentNode == null) continue;
                currentNode = currentNode.ParentNode;
                var item = _xmlDoc.CreateElement("ul");
                currentNode.AppendChild(item);
                foreach (var privilege in currentArry)
                {
                    var subItem = _xmlDoc.CreateElement("li");
                    var otherItem = _xmlDoc.CreateElement("a");
                    otherItem.SetAttribute("href", privilege.URL);
                    otherItem.InnerText = privilege.Description;
                    otherItem.SetAttribute("Target", "_blank");
                    subItem.AppendChild(otherItem);
                    item.AppendChild(subItem);
                }
            }
            SaveXml();
        }

        /// <summary>
        /// 判断是否更新过
        /// </summary>
        /// <param name="lastUpdateDate"></param>
        /// <returns></returns>
        public bool JudgeXml(DateTime lastUpdateDate)
        {
            LoadXml();
            var createNode = _xmlDoc.SelectSingleNode("//ul[@Create]");
            if(createNode!=null)
            {
                DateTime tryCreateTime;
                if (DateTime.TryParse(createNode.Attributes["Create"].Value, out tryCreateTime))
                {
                    if(tryCreateTime<lastUpdateDate)
                    {
                        File.Delete(_filePath);
                        CreateXml(_filePath);
                        return true;
                    }
                    return false;
                }
                File.Delete(_filePath);
                CreateXml(_filePath);
                return true;
            }
             return false;
        }

        /// <summary>
        /// 读取XML文档
        /// </summary>
        /// <returns></returns>
        public string ReadXML()
        {
            LoadXml();
            var bodyStr=_xmlDoc.InnerXml;
            return bodyStr;
        }
        #endregion
    }
}