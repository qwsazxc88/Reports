using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reports.Core;
using Reports.Presenters.UI.Bl;
namespace WebMvc.Controllers
{
    public class MailListController : Controller
    {
        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }

        public EmptyResult Index()
        {
            RequestBl.SendMail();
            return new EmptyResult();
        }

    }
}
