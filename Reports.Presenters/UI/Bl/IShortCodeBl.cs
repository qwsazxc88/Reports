using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace Reports.Presenters.UI.Bl
{
    public class RouteVal
    {
        public string Action { get; private set; }
        public string Controller { get; private set; }
        public object Values { get; private set; }
        public RouteVal(string _action, string _controller, object _values=null)
        {
            this.Action = _action;
            this.Controller = _controller;
            this.Values = _values;
        }
    }
   
    public interface IShortCodeBl
    {
        RouteVal GetRouteValues(int id, string type);
        void SetUserRole(int roleId);
    }
}
