using System.Collections.Generic;
using System;
using System.Text;
using System.Reflection;
using System.Web;
namespace WebMvc.Helpers
{
    public static class ObjectExtension
    {
        public static string ToParamsString(this object obj)
        {
            StringBuilder param = new StringBuilder();
            Type myType = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            var su = HttpContext.Current.Server;
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(obj, null);
                if (param.Length > 0) param.Append("&");
                param.Append(prop.Name + "=" + su.HtmlEncode(propValue != null ? (propValue is DateTime) ? ((DateTime)propValue).ToString("MM.dd.yyyy") : propValue.ToString() : ""));
                // Do something with propValue
            }
            return (param.ToString());
        }
        public static string ToHtmlAttributeString(this object obj)
        {
            string result = " ";
            var props = obj.GetType().GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                result+=String.Format(" {0} ='{1}' ",props[i].Name,props[i].GetValue(obj, null).ToString());
            }
            return result;
        }
    }
}