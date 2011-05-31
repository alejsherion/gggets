using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace GGGETSAdmin.Common
{
    public static class UtilityJson
    {

        public static T GetValueByKey<T>(this object obj, string name)
        {
            T retValue = default(T);
            object objValue = null;
            try
            {
                if (obj is Newtonsoft.Json.Linq.JObject)
                {
                    var jObject = obj as Newtonsoft.Json.Linq.JObject;
                    var property = jObject.Property(name);
                    if (property != null)
                    {
                        var value = property.Value as Newtonsoft.Json.Linq.JValue;
                        if (value != null)
                        {
                            objValue = value.Value;
                        }
                    }
                }
                else
                {
                    var property = obj.GetType().GetProperty(name);
                    if (property != null)
                    {
                        objValue = property.GetValue(obj, null);
                    }
                }

                if (objValue != null)
                {
                    retValue = (T)objValue;
                }
            }
            catch (System.Exception)
            {
                retValue = default(T);
            }
            return retValue;
        }

        public static object GetValueByKey(this object obj, string name)
        {
            object objValue = null;
            if (obj is Newtonsoft.Json.Linq.JObject)
            {
                var jObject = obj as Newtonsoft.Json.Linq.JObject;
                var property = jObject.Property(name);
                if (property != null)
                {
                    var value = property.Value as Newtonsoft.Json.Linq.JValue;
                    if (value != null)
                    {
                        objValue = value.Value;
                    }
                }
            }
            else
            {
                var property = obj.GetType().GetProperty(name);
                if (property != null)
                {
                    objValue = property.GetValue(obj, null);
                }
            }
            return objValue;
        }

        public static string GetAppConfig(string keyname)
        {
            var config = System.Configuration.ConfigurationManager.AppSettings[keyname];
            try
            {
                if (string.IsNullOrEmpty(config))
                {
                    string filePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Config");
                    TextReader reader = new StreamReader(filePath);
                    XElement xml = XElement.Load(filePath);
                    if (xml != null)
                    {
                        var element = xml.Elements().SingleOrDefault(e => e.Attribute("key") != null && e.Attribute("key").Value.Equals(keyname));
                        if (element != null)
                        {
                            config = element.Attribute("value").Value;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                config = string.Empty;
            }
            return config;
        }

        public static T ParseEnum<T>(string val)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), val);
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }
       
        public static string ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


        public static dynamic ToDynamicObject(string json)
        {
            DynamicJson dj = null;
            var jsonObj = UtilityJson.ToObject<JObject>(json);
            if (jsonObj != null)
            {
                dj = new DynamicJson(jsonObj);
            }
            return dj;
        }

        public static List<dynamic> ToDynamicObjects(string json)
        {
            List<dynamic> djs = null;
            var jsonObj = UtilityJson.ToObject<JArray>(json);
            if (jsonObj != null)
            {
                djs = new List<dynamic>();
                foreach (var j in jsonObj)
                {
                    djs.Add(new DynamicJson(j as JObject));
                }
            }
            return djs;
        }

        public static T ToObject<T>(string json)
            where T : class
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        public static object ToObject(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            }
            catch (System.Exception)
            {
                return null;
            }

        }

    }
}
