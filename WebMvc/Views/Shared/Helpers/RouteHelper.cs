using System.Web.Mvc;
using System.Web.Routing;

namespace WebMvc.Views.Shared.Helpers
{
    public static class RouteHelper
    {
        const string Controller = "Controller";
        const string Action = "Action";
        const string ReplaceFormatString = "REPLACE{0}";

        public static string GetUrl(RequestContext requestContext, RouteValueDictionary routeValueDictionary)
        {
            RouteValueDictionary urlData = new RouteValueDictionary();
            UrlHelper urlHelper = new UrlHelper(requestContext);

            int i = 0;
            foreach (var item in routeValueDictionary)
            {
                if (string.Empty == item.Value)
                {
                    i++;
                    urlData.Add(item.Key, string.Format(ReplaceFormatString, i));
                }
                else
                {
                    urlData.Add(item.Key, item.Value);
                }
            }

            var url = urlHelper.RouteUrl(urlData);

            for (int index = 1; index <= i; index++)
            {
                url = url.Replace(string.Format(ReplaceFormatString, index), string.Empty);
            }

            return url;
        }
    }
}