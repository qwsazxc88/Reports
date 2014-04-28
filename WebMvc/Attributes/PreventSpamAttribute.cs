using System;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using System.Web.Mvc;
using log4net;
using Reports.Presenters.UI.ViewModel;

namespace WebMvc.Attributes
{
    public class PreventSpamAttribute : ActionFilterAttribute
    {
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //This stores the time between Requests (in seconds)
        public int DelayRequest = 600;
        //The Error Message that will be displayed in case of excessive Requests
        public string ErrorMessage = "Форма отправлена повторно.";
        //This will store the URL to Redirect errors to
        public string redirectURL = @"~/Error/DoubleSubmitError";
        //public string CookieName = "Unique";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Cache cache = filterContext.HttpContext.Cache;
            if (filterContext.ActionParameters == null || filterContext.ActionParameters.Count() == 0 ||
                !filterContext.ActionParameters.ContainsKey("model"))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            var model = filterContext.ActionParameters["model"] as IPreventDCModel;
            var idModel = filterContext.ActionParameters["model"] as IContainId;
            if (model == null || idModel == null)
            {
                base.OnActionExecuting(filterContext);
                // The action didn't have an argument called "model" or this argument
                // wasn't of the expected type => no need to continue any further
                return;
            }
            if (model.Guid != null)
            {
                string guid = model.Guid;
                if (cache[guid] != null)
                {
                    var count = (int) cache[guid];
                    count++;
                    cache[guid] = count;
                    if (count > 1)
                    {
                        Log.ErrorFormat("Double submit error for request {0}, user {1}", idModel.Id, idModel.UserId);
                        //filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, ErrorMessage););
                        if (idModel.Id == 0)
                        {
                            filterContext.Result = new RedirectResult(redirectURL);
                            return;
                        }
                    }
                    model.Guid = (Guid.NewGuid()).ToString();
                    cache.Add(model.Guid, 0, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration,
                              CacheItemPriority.Default, null);
                }
                else
                {
                    model.Guid = (Guid.NewGuid()).ToString();
                    cache.Add(model.Guid, 0, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration,
                              CacheItemPriority.Default, null);
                }
            }
            else
            {
                model.Guid = (Guid.NewGuid()).ToString();
                cache.Add(model.Guid, 0, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration,
                          CacheItemPriority.Default, null);
            }
            base.OnActionExecuting(filterContext);
            // modify some property value
            //string formGuid = filterContext.HttpContext.Request.Headers.Get(CookieName);
            //if (formGuid != null)
            //{
            //    if(cache[formGuid] != null)
            //    {
            //        int count = (int)cache[formGuid];
            //        count++;
            //        cache[formGuid] = count;
            //        if(count > 1)
            //            filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, ErrorMessage);
            //    }
            //}
            //else
            //{
            //    string newGuid = (Guid.NewGuid()).ToString();
            //    //HttpCookie cookie = new HttpCookie(CookieName, newGuid);
            //    filterContext.HttpContext.Response.AddHeader(CookieName,newGuid);
            //    cache.Add(newGuid, 0, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            //}

            //////Store our HttpContext (for easier reference and code brevity)
            ////var request = filterContext.HttpContext.Request;
            //////Store our HttpContext.Cache (for easier reference and code brevity)
            ////var cache = filterContext.HttpContext.Cache;

            //////Grab the IP Address from the originating Request (very simple implementation for example purposes)
            ////var originationInfo = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;

            //////Append the User Agent
            ////originationInfo += request.UserAgent;

            //////Now we just need the target URL Information
            ////var targetInfo = request.RawUrl + request.QueryString;

            //////Generate a hash for your strings (this appends each of the bytes of the value into a single hashed string
            ////var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(originationInfo + targetInfo)).Select(s => s.ToString("x2")));

            //////Checks if the hashed value is contained in the Cache (indicating a repeat request)
            ////if (cache[hashValue] != null)
            ////{
            ////    //Adds the Error Message to the Model and Redirect
            ////    filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty, ErrorMessage);
            ////}
            ////else
            ////{
            ////    //Adds an empty object to the cache using the hashValue to a key (This sets the expiration that will determine
            ////    //if the Request is valid or not
            ////    cache.Add(hashValue, new Guid(), null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            ////}
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Cache cache = filterContext.HttpContext.Cache;
            var result = filterContext.Result as ViewResultBase;
            if (result == null)
            {
                base.OnActionExecuted(filterContext);
                // The controller action didn't return a view result 
                // => no need to continue any further);
                return;
            }
            var model = result.Model as IPreventDCModel;
            if (model == null)
            {
                // there's no model or the model was not of the expected type 
                // => no need to continue any further
                base.OnActionExecuted(filterContext);
                return;
            }

            // modify some property value
            if (model.Guid == null)
            {
                model.Guid = (Guid.NewGuid()).ToString();
                cache.Add(model.Guid, 0, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration,
                          CacheItemPriority.Default, null);
            }
            base.OnActionExecuted(filterContext);
        }
    }
}