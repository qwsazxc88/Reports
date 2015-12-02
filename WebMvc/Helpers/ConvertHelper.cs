using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebMvc.Helpers
{
    public static class ConvertHelper
    {        

        public static MvcHtmlString ConvertNumberSumToText(this HtmlHelper html, decimal sum)
        {
            var result = Reports.Presenters.UI.Bl.Impl.RequestBl.GetSummString(sum);
            return new MvcHtmlString(result);
        }
    }

}