using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.Services;

namespace WebMvc.Controllers
{
    [HandleError(View = "Error")]
    public class BaseController : Controller
    {
        public const int MaxFileSize = 2 * 1024 * 1024;
        public const string StrFileSizeError = "Размер прикрепленного файла не может превышать {0} Мб.";
        #region Fields

        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected IAuthenticationService authenticationService;
        //protected IBaseBl baseBl;

        #endregion
        public const int MaxCommentLength = 256;

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
        public static string WriteErrorToLog(Exception ex,Uri url)
        {
            if(url != null)
                Log.ErrorFormat("Uri {0}",url.AbsoluteUri);
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
 
    }
}