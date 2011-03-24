//************************************************************************
// 用户名				GGGETS国际综合快递
// 系统名				管理后台
// 子系统名		        页面数据绑定
// 作成者				hong.li
// 改版日				2011.02.25
// 改版内容				创建并且修改
//************************************************************************
using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GGGETSAdmin.Common
{
    public class DataBindHelper<T> where T : class
    {
        public static void Bind(Control bindArea,T eneity)
        {
            var type = eneity.GetType();
            if(!bindArea.HasControls())
            {
                var id = bindArea.ID;
                var filedName = FileName(id);
                if(!String.IsNullOrEmpty(filedName))
                {
                    var currentPropertyInfo = type.GetProperty(filedName);
                    var value = currentPropertyInfo.GetValue(eneity, null);
                    if(bindArea is TextBox)
                    {
                        var control = (TextBox)bindArea;
                        control.Text = Convert.ToString(value);
                    }
                    else if(bindArea is RadioButtonList)
                    {
                        var control = (RadioButtonList)bindArea;
                        control.SelectedValue = Convert.ToString(value);
                    }
                    else if (bindArea is DropDownList)
                    {
                        var control = (DropDownList)bindArea;
                        control.SelectedValue = Convert.ToString(value);
                    }
                }
            }
            else
            {
                var controls = bindArea.Controls;
                foreach(Control item in controls)
                {
                    Bind(item, eneity);
                }
            }

        }

        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string FileName(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return "";
            var type = typeof (T);
            var items = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var feildName = "";
            var currentName = name.ToUpper();
            foreach (PropertyInfo item in items)
            {
                var currentType = item.PropertyType;
                if (currentType.IsValueType
                    ||item.PropertyType.Name.ToLower().Contains("string"))
                {
                    var tempName = item.Name.ToUpper();
                    if (currentName.Contains(tempName))
                    {
                        feildName = item.Name;
                        break;
                    }
                }
            }
            return feildName;
        }

        /// <summary>
        /// 得到页面数据
        /// </summary>
        /// <param name="bindArea"></param>
        /// <param name="eneity"></param>
        public static void GetDaTa(Control bindArea, T eneity)
        {
            var type = eneity.GetType();
            if (!bindArea.HasControls())
            {
                var id = bindArea.ID;
                var filedName = FileName(id);
                if (!String.IsNullOrEmpty(filedName))
                {
                    var currentPropertyInfo = type.GetProperty(filedName);
                    var propertyType = currentPropertyInfo.PropertyType;
                    if (propertyType.IsValueType
                        || propertyType.Name.ToLower().Contains("string"))
                    {
                        if (bindArea is TextBox)
                        {
                            var control = (TextBox)bindArea;
                            var tempValue = control.Text;
                            try
                            {
                                var tempvalue = ChangeType(propertyType, tempValue);
                                currentPropertyInfo.SetValue(eneity, tempvalue, null);
                            }
                            catch
                            {
                                throw new Exception("填写数据不正确");
                            }

                        }
                        else if (bindArea is RadioButtonList)
                        {
                            var control = (RadioButtonList)bindArea;
                            var tempValue = control.SelectedValue;
                            try
                            {
                                var tempvalue = ChangeType(propertyType, tempValue);
                                currentPropertyInfo.SetValue(eneity, tempvalue, null);
                            }
                            catch
                            {
                                throw new Exception("数据不正确");
                            }
                        }
                        else if (bindArea is DropDownList)
                        {
                            var control = (DropDownList)bindArea;
                            var tempValue = control.SelectedValue;
                            try
                            {
                                var tempvalue = ChangeType(propertyType, tempValue);
                                currentPropertyInfo.SetValue(eneity, tempvalue, null);
                            }
                            catch
                            {
                                throw new Exception("数据不正确");
                            }
                        }
                    }
                    
                }
            }
            else
            {
                var controls = bindArea.Controls;
                foreach (Control item in controls)
                {
                    GetDaTa(item, eneity);
                }
            }

        }

        /// <summary>
        /// 改变值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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