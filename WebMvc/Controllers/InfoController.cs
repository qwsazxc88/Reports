using System.Web.Mvc;
using Reports.Presenters.UI.ViewModel;
using Reports.Core;
using System;
using Reports.Core.Services;
using Reports.Presenters.Services;
using Reports.Core.Dao;
using Reports.Core.Domain;
using WebMvc.Attributes;
namespace WebMvc.Controllers
{
    [Authorize]
    public class InfoController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new InfoModel());
        }

       
        [HttpGet]
        public JsonResult News(int page=0)
        {
            var result = Ioc.Resolve<INewsDao>().GetNews(page, 10);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ReportAuthorize(UserRole.Admin )]
        public void AddNews(Reports.Core.Dto.NewsDto post)
        {
            News result = null;
            var dao = Ioc.Resolve<INewsDao>();
            if (post.id <= 0)
            {
                result = new News
                {
                    PostDate = DateTime.Now,
                    Header = post.Header,
                    Text = post.Text,
                    IsVisible = true,
                    Author = Ioc.Resolve<IUserDao>().Load(AuthenticationService.CurrentUser.Id)
                };
            }
            else
            {
                result = dao.Load(post.id);
                result.Text = post.Text;
                result.Header = post.Header;
            }
            dao.SaveAndFlush(result);
        }
    }
}