using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
