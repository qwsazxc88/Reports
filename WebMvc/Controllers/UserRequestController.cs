﻿using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.Bl.Impl;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Controllers
{
    public class UserRequestController : BaseController
    {
        public const int MaxCommentLength = 256;

        protected IRequestBl requestBl;
        public IRequestBl RequestBl
        {
            get
            {
                requestBl = Ioc.Resolve<IRequestBl>();
                return Validate.Dependency(requestBl);
            }
        }
        //
        // GET: /UserRequestController/
         [HttpGet]
         public ActionResult CreateRequest()
         {
             int? userId = new int?();
             CreateRequestModel model = RequestBl.GetCreateRequestModel(userId);
             return View(model);
         }

         [HttpGet]
         public ActionResult VacationList()
         {
             //int? userId = new int?();
             VacationListModel model = RequestBl.GetVacationListModel();
             return View(model);
         }
         [HttpPost]
         public ActionResult VacationList(VacationListModel model)
         {
             RequestBl.SetVacationListModel(model);
             return View(model);
         }
         [HttpGet]
         public ActionResult VacationEdit(int id,int userId)
         {
             //int? userId = new int?();
             VacationEditModel model = RequestBl.GetVacationEditModel(id,userId);
             return View(model);
         }

         [HttpGet]
         public ActionResult RenderComments(int id,int typeId)
         {
             //IContractRequest bo = Ioc.Resolve<IContractRequest>();
             RequestCommentsModel model = RequestBl.GetCommentsModel(id,typeId);
             return PartialView("RequestCommentPartial", model);
         }

         [HttpGet]
         public ActionResult AddCommentDialog(int id, int typeId)
         {
             try
             {
                 AddCommentModel model = new AddCommentModel { DocumentId = id };
                 return PartialView(model);
             }
             catch (Exception ex)
             {
                 Log.Error("Exception", ex);
                 string error = "Ошибка при загрузке данных: " + ex.GetBaseException().Message;
                 return PartialView("DialogError", new DialogErrorModel { Error = error });
             }
         }

         [HttpPost]
         public ContentResult SaveComment(int id, int typeId, string comment)
         {
             bool saveResult = false;
             string error;
             try
             {
                 if (comment == null || string.IsNullOrEmpty(comment.Trim()))
                 {
                     error = "Комментарий - обязательное поле";
                 }
                 else if (comment.Trim().Length > MaxCommentLength)
                 {
                     error = string.Format("Длина поля 'Комментарий' не может превышать {0} символов.", MaxCommentLength);
                 }
                 else
                 {
                     var model = new SaveCommentModel
                     {
                         DocumentId = id,
                         TypeId = typeId,
                         Comment = comment.Trim(),
                     };
                     saveResult = RequestBl.SaveComment(model);
                     error = model.Error;
                 }
             }
             catch (Exception ex)
             {
                 Log.Error("Exception on SaveComment:", ex);
                 error = ex.GetBaseException().Message;
                 saveResult = false;
             }
             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
             return Content(jsonString);
         }

         
     }
}