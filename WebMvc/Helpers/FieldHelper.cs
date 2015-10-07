using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Reports.Presenters.UI.ViewModel;
namespace WebMvc.Helpers
{
    public static class FieldHelper
    {
        public static MvcHtmlString CreateField<TModel, TValue, T>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
            where TValue : FieldViewModel<T>
            where TModel : class
        {
            var _expression = expression.Compile();

            Expression<Func<TModel, T>> valueexpression = x => _expression(x).Value;
            var element = _expression(html.ViewData.Model);
            var editor = html.EditorFor(
                valueexpression,
                element.IsEditable ?
                    (object)new { @class = element.EditorClass } :
                    (object)new { disabled = "disabled", @class = element.EditorClass }
                );
            var label = html.LabelFor(expression);
            var validation = html.ValidationMessageFor(expression);
            string template = "<div class='formfield clear'><span>{0}</span><span>{1}</span><span>{2}</span></div>";
            return new MvcHtmlString(String.Format(template, label, editor, validation));
        }
    }
}