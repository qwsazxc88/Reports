using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Reports.Core;
using System.Security.Cryptography;
using System.Text;
namespace Reports.Presenters.Utils
{
    public class WebUtils
    {
        public const string ChangePasswordPageName = "ChangePassword.aspx";
        public const string UserPropertiesPageName = "UserProperties.aspx";
        private static string ControlIdInMasterPage = "hMakePageExpired";
        public static string ControlTrueValue = "true";
        private static string ControlFalseValue = "false";

        //public static void DisableMasterPageStamp(Page page)
        //{
        //    Validate.NotNull(page, "page");
        //    if (page.Master != null)
        //    {
        //        Control ctrl = page.Master.FindControl(DoubleSubmitStamp.MasterPageControlName);
        //        if (ctrl != null)
        //            ((DoubleSubmitStamp) ctrl).RenderStamp = false;
        //    }
        //}
        public static void MakePageExpired(HttpResponse response)
        {
            Validate.NotNull(response, "response");
            response.Expires = 0;
            response.Cache.SetNoStore();
            response.AppendHeader("Pragma", "no-cache");
        }

        public static void MakePageUnexpired(Page page)
        {
            Validate.NotNull(page, "page");
            if (page.Master != null)
            {
                Control ctrl = page.Master.FindControl(ControlIdInMasterPage);
                if (ctrl != null)
                    ((HiddenField) ctrl).Value = ControlFalseValue;
            }
        }

        //public static void SetSortParams(ISortedGridView view, GridViewSortEventArgs e)
        //{
        //    Validate.NotNull(view, "view");
        //    Validate.NotNull(e, "e");
        //    if (view.SortExpression == e.SortExpression)
        //    {
        //        if (view.SortAscending && (e.SortDirection == SortDirection.Ascending))
        //        {
        //            view.SortAscending = false;
        //        }
        //        else if (!view.SortAscending && (e.SortDirection == SortDirection.Descending))
        //        {
        //            view.SortAscending = true;
        //        }
        //        else
        //            view.SortAscending = (e.SortDirection == SortDirection.Ascending);
        //    }
        //    else
        //    {
        //        view.SortAscending = (e.SortDirection == SortDirection.Ascending);
        //        view.SortExpression = e.SortExpression;
        //    }
        //}
        //public static bool GetSortAscendingField(ISortedGridView view, string sessionKey)
        //{
        //    Validate.NotNull(view, "view");
        //    Validate.NotNull(sessionKey, "sessionKey");
        //    HttpSessionState session = ((Page)view).Session;
        //    bool ascending;
        //    if ((session[sessionKey] == null) || !bool.TryParse(session[sessionKey].ToString(), out ascending))
        //    {
        //        view.SortAscending = true;
        //        ascending = true;
        //    }
        //    return ascending;
        //}

        public static string GetMd5Hash( string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}