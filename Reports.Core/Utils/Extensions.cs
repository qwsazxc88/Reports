using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace Reports.Core.Utils
{
    public static class Extensions
    {
        public static Result NotNullAndAny<T>(this IList<T> obj)
        {
            if (obj != null && obj.Any())
                return new Result(true, "Ok");
            else return new Result(false, "Error");
        }
        public static T DefaultIfNull<T>(this object element,string property, T defaultResult)
        {
            if (element==null) return defaultResult;
            var elementExpr = Expression.Parameter(element.GetType(), "element");
            var propertyExpr = elementExpr.GetProperty(property);
            var lambda = Expression.Lambda<Func<object,T>>(propertyExpr, elementExpr);
            T Result = defaultResult;
            try
            {
                Result = lambda.Compile()(element);
            }
            catch (Exception e) { }
            return Result;
            
        }
        public static Expression GetProperty(this Expression expression, string property)
        {
            string[] props = property.Split('.');
            Expression result = Expression.Property(expression, props[0]);
            for (int i = 1; i < props.Length; i++)
                result = Expression.Property(result, props[i]);
            return result;
        }
    }
}
