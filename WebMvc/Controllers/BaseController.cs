using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.Services;
using Newtonsoft.Json;
namespace WebMvc.Controllers
{
    [HandleError(View = "Error")]
    public class BaseController : Controller
    {
        public const int MaxFileSize = 20 * 1024 * 1024;//исправил с 5 на 10 Загрязкин О.
        public const string StrFileSizeError = "Размер прикрепленного файла не может превышать {0} Мб.";
        #region Fields

        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected IAuthenticationService authenticationService;
        //protected IBaseBl baseBl;

        #endregion

        protected virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //https://msdn.microsoft.com/ru-ru/library/system.web.mvc.controller.onactionexecuting%28v=vs.118%29.aspx
            //setMainActiveMenuItem( "li" + "@this.ViewContext.RouteData.Values["controller"].ToString()" )
            //if ( ( (dto.UserRole & UserRole.Accountant) == 0 ) || ( (dto.UserRole & UserRole.Archivist) == 0 ) )
            //http://sqlperformance.com/2012/08/t-sql-queries/dry-principle-bitwise-operations
            var controllerName = filterContext.RouteData.Values["controller"] ;
            // var dto = UserDto.Deserialize(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData);
            var dto = UserDto.Deserialize(filterContext.HttpContext.User.Identity.Ticket.UserData);
            int userRole = dto.
            var menuList = GetMenuForRole();
        }
        /*
            public IList<GpdPermissionDto> GetPermission(UserRole role)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[GpdPermission] WHERE RoleID = " + (int)role + " and MenuID = 3";
            IQuery query = CreatePermissionQuery(sqlQuery);
            IList<GpdPermissionDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdPermissionDto))).List<GpdPermissionDto>();
            return documentList;
        }
        public virtual IQuery CreatePermissionQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("IsCreate", NHibernateUtil.Boolean).
                AddScalar("IsDraft", NHibernateUtil.Boolean).
                AddScalar("IsWrite", NHibernateUtil.Boolean).
                AddScalar("IsCancel", NHibernateUtil.Boolean).
                AddScalar("IsComment", NHibernateUtil.Boolean).
                AddScalar("IsCreateAct", NHibernateUtil.Boolean);
        }
        */
        
        protected IList<MenuDto/*TODO: MenuDto*/> GetMenuForRole(int roleId)
        {   /*  SELECT Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent 
                FORM MenuForRole 
                JOIN Menu ON MenuForRole.MenuId=Menu.id 
                WHERE (RoleId=1 AND Menu.IsVisible=1 AND MenuForRole.NotAllow=0);
            */
            string sqlQuery = @"SELECT Menu.Name, Menu.LinkController, Menu.LinkAction, Menu.Parent FORM MenuForRole JOIN Menu ON MenuForRole.MenuId=Menu.id "
            IQuery query = Session.CreateSQLQuery(sqlQuery).AddScalar(AAAAAAAAAA) //TODO: FIXME //WHERE (RoleId=1 AND Menu.IsVisible=1 AND MenuForRole.NotAllow=0);"
            IList<MenuDto> menuList = query.SetResultTransformer(Transformers.AliasToBean(typeof(MenuDto))).List<MenuDto>();
            return menuList;
        }

        public const int MaxCommentLength = 256;
        public ActionResult GetComments(int PlaceTypeId, int PlaceId)
        {
            var basebl = Ioc.Resolve<Reports.Presenters.UI.Bl.IRequestBl>();
            var model = basebl.GetComments(PlaceTypeId, PlaceId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddComment(MessagesDto message)
        {
            var basebl = Ioc.Resolve<Reports.Presenters.UI.Bl.IRequestBl>();
            basebl.AddComment(message);
            return Json("ok");
        }

        public ContentResult GetAllDepartmentsTree()
        {
            var basebl = Ioc.Resolve<Reports.Presenters.UI.Bl.IBaseBl>();
            var tree = basebl.GetDepartmentTree();
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(tree);
            return Content('['+result+']');
        }

        public IAuthenticationService AuthenticationService
        {
            get
            {
                authenticationService = Ioc.Resolve<IAuthenticationService>();
                return Validate.Dependency(authenticationService);
            }

            set { authenticationService = value; }
        }

        //public IBaseBl BaseBl
        //{
        //    get
        //    {
        //        baseBl = Ioc.Resolve<IBaseBl>();
        //        return Validate.Dependency(baseBl);
        //    }
        //}
        public static string WriteErrorToLog(Exception ex, Uri url)
        {
            if(url != null)
                Log.ErrorFormat("Uri {0}", url.AbsoluteUri);
            if (ex != null)
                Log.Error("Error(MVC error page):", ex);
            else
                Log.Error("Error(MVC error page), exception = null");
            return string.Empty;
        }

        protected UploadFileDto GetFileContext()
        {
            if (Request.Files.Count == 0)
                return null;
            string file = Request.Files.GetKey(0);
            return GetFileContext(file);
        }

        public static UploadFileDto GetFileContext(HttpRequestBase request, ModelStateDictionary modelState)
        {
            if (request.Files.Count == 0)
                return null;
            string file = request.Files.GetKey(0);
            return GetFileContext(request, modelState, file);
        }

        protected UploadFileDto GetFileContext(string file)
        {
            //if (Request.Files.Count == 0)
            //    return null;
            //string file = Request.Files.GetKey(0);
            HttpPostedFileBase hpf = Request.Files[file];
            if ((hpf == null) || (hpf.ContentLength == 0))
                return null;
            if (hpf.ContentLength > MaxFileSize)
            {
                ModelState.AddModelError("", string.Format(StrFileSizeError, MaxFileSize / (1024 * 1024)));
                return null;
            }

            byte[] context = GetFileData(hpf);
            return new UploadFileDto
            {
                Context = context,
                ContextType = hpf.ContentType,
                FileName = Path.GetFileName(hpf.FileName),
            };
        }

        protected static UploadFileDto GetFileContext(HttpRequestBase request, ModelStateDictionary modelState, string file)
        {
            HttpPostedFileBase hpf = request.Files[file];
            if ((hpf == null) || (hpf.ContentLength == 0))
                return null;
            if (hpf.ContentLength > MaxFileSize)
            {
                modelState.AddModelError("", string.Format(StrFileSizeError, MaxFileSize / (1024 * 1024)));
                return null;
            }

            byte[] context = GetFileData(hpf);
            return new UploadFileDto
            {
                Context = context,
                ContextType = hpf.ContentType,
                FileName = Path.GetFileName(hpf.FileName),
            };
        }

        protected static byte[] GetFileData(HttpPostedFileBase file)
        {
            var length = file.ContentLength;
            var fileContent = new byte[length];
            file.InputStream.Read(fileContent, 0, length);
            return fileContent;
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Excel(string table)
        {
            string template = "<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{0}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv=\"content-type\" content=\"text/plain; charset=UTF-8\"/></head><body><table>{1}</table></body></html>";

            var data = String.Format(template, "Таблица", table);

            return File(System.Text.Encoding.UTF8.GetBytes(data), "application/vnd.ms-excel", "table.xls");
        }
    }
}