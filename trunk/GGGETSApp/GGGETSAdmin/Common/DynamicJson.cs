using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace GGGETSAdmin.Common
{
    public class DynamicJson : DynamicObject
    {
        Newtonsoft.Json.Linq.JObject _json;
        public DynamicJson(Newtonsoft.Json.Linq.JObject json)
        {
            _json = json;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool ret = false;
            JToken value;
            if (_json.TryGetValue(binder.Name, out value))
            {
                result = (value as JValue).Value;
                ret = true;
            }
            else
            {
                result = null;
            }
            return ret;
        }
    }
}
