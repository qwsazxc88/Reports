using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Reports.Core;
using Reports.Core.Dto;
using Reports.Presenters.UI.Bl;
using Reports.Presenters.UI.ViewModel;
using WebMvc.Attributes;
namespace WebMvc.Controllers
{

    public class AccountController : BaseController
    {
        public const int MinPasswordLength = 7;
        public const int MaxSupportMessageLength = 512;
        protected ILoginBl loginBl;
        protected IBaseBl baseBl;
        #region Properties

        public ILoginBl LoginBl
        {
            get
            {
                loginBl = Ioc.Resolve<ILoginBl>();
                return Validate.Dependency(loginBl);
            }
        }
        public IBaseBl BaseBl
        {
            get
            {
                baseBl = Ioc.Resolve<IBaseBl>();
                return Validate.Dependency(baseBl);
            }
        }
        #endregion

        //public IFormsAuthenticationService FormsService
        //{
        //    get; 
        //    set;
        //}

        //protected override void Initialize(RequestContext requestContext)
        //{
        //    if (FormsService == null)
        //    {
        //        FormsService = new FormsAuthenticationService();
        //    }
        //    base.Initialize(requestContext);
        //}

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            var model = new LogOnModel();
            LoginBl.InitForm(model);
            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError("", model.ErrorMessage);
                model.IsButtonHidden = true;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                LoginBl.OnLogin(model);
                if (!string.IsNullOrEmpty(model.ErrorMessage))
                    ModelState.AddModelError("", model.ErrorMessage);
                else
                {
                    if(model.IsFirstTimeLogin)
                        return RedirectToAction("ChangePassword");
                    if(model.NeedToSelectRole)
                        return RedirectToAction("ChangeRole", new { returnUrl=returnUrl});
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************
        public JsonResult AddAlternativeEmail(string Email)
        {
            BaseBl.AddAlternativeMail(Email);
            return Json(new {result="Ok.",message="На указанный адрес отправлено письмо с подтверждением."});
        }
        public JsonResult AddAlternativeEmailToUser(int UserId, string Email)
        {
            BaseBl.AddAlternativeMail(UserId, Email);
            return Json(new { result = "Ok.", message = "На указанный адрес отправлено письмо с подтверждением." });
        }
        [HttpGet]
        public ContentResult Confirm(Guid key)
        {
            string result ="";
            BaseBl.ConfirmMail(key)
                .OnSuccess(x => result = x.Message)
                .OnError(x => result = x.Message);
            return Content(result);
        }
        public ActionResult LogOff()
        {
            LoginBl.LogOff();
            return RedirectToAction("LogOn");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        //public ActionResult Register()
        //{
        //    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

        //        if (createStatus == MembershipCreateStatus.Success)
        //        {
        //            FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
        //    return View(model);
        //}

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        //[Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
          
            ChangePasswordModel model = new ChangePasswordModel();
            LoginBl.CheckAndSetUserId(model);
            return View(model);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ValidateModel(model))
                return View(model);
            LoginBl.OnChangePassword(model);
            if(!string.IsNullOrEmpty(model.Error))
            {
                ModelState.AddModelError("",model.Error);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        protected bool ValidateModel(ChangePasswordModel model)
        {
            return ModelState.IsValid;
        }

        [HttpGet]
        public ActionResult ChangePwd()
        {
            ChangePwdModel model = new ChangePwdModel();
            return View(model);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult ChangePwd(ChangePwdModel model)
        {
            if (!ValidateModel(model))
                return View(model);
            LoginBl.OnChangePwd(model);
            if (!string.IsNullOrEmpty(model.Error))
                ModelState.AddModelError("", model.Error);
            else
                model.Success = "Пароль успешно изменен.";
            return View(model);
        }
        protected bool ValidateModel(ChangePwdModel model)
        {
            if(!string.IsNullOrEmpty(model.NewPassword))
            {
                bool containDigit = false;
                bool containLetter = false;
                string password = model.NewPassword;
                foreach (char c in password)
                {
                    if (Char.IsDigit(c))
                        containDigit = true;
                    else if (Char.IsLetter(c))
                        containLetter = true;
                }
                if(!containDigit || !containLetter)
                    ModelState.AddModelError("NewPassword", "Поле 'Новый пароль' должен содержать буквы и цифры");
            }
            return ModelState.IsValid;
        }

        [HttpGet]
        public ActionResult LoginRecovery()
        {
            LoginRecoveryModel model = new LoginRecoveryModel{Subject = string.Empty,Text = string.Empty};
            return View(model);
        }
        [HttpPost]
        public ActionResult LoginRecovery(LoginRecoveryModel model)
        {
            if (!ValidateModel(model))
                return View(model);
            LoginBl.OnLoginRecovery(model);
            //if (model.EmailDto != null)
            //{
            //    if (string.IsNullOrEmpty(model.EmailDto.Error))
            //        SendEmail(model.EmailDto);
            //    if (!string.IsNullOrEmpty(model.EmailDto.Error))
            //        ModelState.AddModelError("",
            //            string.Format(@"Пароль не был отправлен на электронную почту пользователя.Ошибка - {0}.", model.EmailDto.Error));
            //    return View(model);
            //}

            if (!string.IsNullOrEmpty(model.Error))
            {
                ModelState.AddModelError("", model.Error);
                return View(model);
            }
            model.IsRecoverySuccess = true;
            return View(model);
        }
        protected bool ValidateModel(LoginRecoveryModel model)
        {
            return ModelState.IsValid;
        }
        [HttpPost]
        public ContentResult SendToSupport(string subject, string text)
        {

            bool saveResult = false;
            string error;
            try
            {

                if (string.IsNullOrEmpty(text))
                    error = "Не заполнено поле 'Текст сообщения'.";
                else if (text.Length > MaxSupportMessageLength)
                    error = string.Format("Поле 'Текст сообщения' не может быть более {0} символов.",MaxSupportMessageLength);
                else
                {
                    var model = new SendToSupportModel { Subject = subject, Text = text };
                    saveResult = LoginBl.SendToSupport(model);
                    error = model.Error;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception on SendToSupport:", ex);
                error = ex.GetBaseException().Message;
                saveResult = false;
            }
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var jsonString = jsonSerializer.Serialize(new SaveTypeResult { Error = error, Result = saveResult });
            return Content(jsonString);
        }

        [HttpGet]
        [AuthorizeAttribute]
        public ActionResult ChangeRole(string returnUrl="")
        {
            return View(LoginBl.GetChangeRoleModel());
        }
        [HttpPost]
        [AuthorizeAttribute]
        public ActionResult ChangeRole(ChangeRoleModel model, string returnUrl="")
        {
            LoginBl.SetUserRole(model);
            if (string.IsNullOrEmpty(returnUrl.Trim()))
                return RedirectToAction("Index", "Home");
            else return Redirect(returnUrl);
            
        }


        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}
        //private void CheckDbVesion()
        //{
        //    Version dbVersion;
        //    try
        //    {
        //        dbVersion = DBVersionDao.GetDatabaseVersion();
        //        if ((dbVersion.Major != AppVersion.Major) ||
        //               (dbVersion.Build != AppVersion.Build))
        //            ModelState.AddModelError("", string.Format("Версия приложения {0} не соответствует версии базы данных {1}", AppVersion, dbVersion));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //        ModelState.AddModelError("", string.Format("Exception on login:{0}", ex.GetBaseException().Message));
        //        return;
        //    }
        //}
        //public Version AppVersion
        //{
        //    get
        //    {
        //        Assembly assembly = Assembly.Load("Reports.Core");//Assembly.GetExecutingAssembly(); 
        //        return assembly.GetName().Version;
        //    }
        //}
    }
}