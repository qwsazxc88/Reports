using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Mvc.Html;
using Reports.Presenters.UI.ViewModel;
namespace WebMvc.Helpers
{
    public static class FieldHelper
    {
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, object HtmlAttributes = null)
        {            
            string template = "<div class='field-wrap'><span style='width:200px;display:inline-block;'>{0}:</span>{1}{2}</div>";
            var label=Html.LabelFor(expression);
            var editor = Html.TextBoxFor(expression, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,label,editor,validation));
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, bool Enabled)
        {
            return Html.CreateField(expression, Enabled ? null : (object)new { disabled = "disabled" });
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, string format ,Expression<Func<TModel, TValue>> expression, bool Enabled)
        {
            return Html.CreateField( format, expression, Enabled ? null : (object)new { disabled = "disabled" });
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, string format, Expression<Func<TModel, TValue>> expression, object HtmlAttributes = null)
        {
            string template = "<div class='field-wrap'><span style='width:200px;display:inline-block;'>{0}:</span>{1}{2}</div>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextBoxNewFor( expression, format, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template, label, editor, validation));
        }

        public static MvcHtmlString CreateDropdown<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, SelectList Items,object HtmlAttributes = null)
        {
            string template = "<div class='field-wrap'><span style='width:200px;display:inline-block;'>{0}:</span>{1}{2}</div>";
            var label = Html.LabelFor(expression);
            var editor = Html.DropDownListFor(expression, Items, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template, label, editor, validation));
        }
        public static MvcHtmlString CreateDropdown<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, SelectList Items, bool Enabled)
        {
            return Html.CreateDropdown(expression, Items, Enabled ? null : (object)new { disabled="disabled" });
        }

        public static MvcHtmlString CreateDisplay<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression)
        {
            string template = "<div class='field-wrap'><span style='width:200px;display:inline-block;'>{0}:</span>{1}</div>";
            var label = Html.LabelFor(expression);
            var editor = Html.DisplayFor(expression);
            return new MvcHtmlString(String.Format(template, label, editor));
        }

        public static MvcHtmlString CreateFileInput<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression)
        {
            string template = "<div class='field-wrap'><span style='width:200px;display:inline-block;'>{0}:</span>{1}{2}</div>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextBoxFor(expression, new { type = "file" });
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template, label, editor, validation));
        }

        public static MvcHtmlString CreateApprove<TModel>(this HtmlHelper<TModel> Html, Expression<Func<TModel,ApprovalViewModel>> element, Dictionary<string,object> HtmlAttributes = null)
        {
            string template = "<div class='field-wrap'><span style='width:200px;display:inline-block;'>{0}:</span>{1}{2}</div>";

            var e = (element.Body as MemberExpression);            
            var param = element.Parameters;
            var expressionProperty = Expression.Property(element.Body, "IsChecked");
            var expression = Expression.Lambda<Func<TModel, bool>>(expressionProperty, param);
            
            var el = element.Compile()(Html.ViewData.Model);
            if (!el.IsEditable)
            {
                if (HtmlAttributes == null) HtmlAttributes = new Dictionary<string, object>();
                HtmlAttributes.Add("disabled", "disabled");
            }
            var label = Html.LabelFor(element);
            var editor = Html.CheckBoxFor(expression, HtmlAttributes);
            
            var approve = String.Format("{0} {1}",el.CheckDate.HasValue?el.CheckDate.Value.ToShortDateString():"",el.Name);
            return new MvcHtmlString(String.Format(template, label, editor, approve));
        }
        
        public static MvcHtmlString CreateButton(this HtmlHelper Html, bool IsSubmit, bool Enabled, string Content, string OnClick = "")
        {
            string action = String.IsNullOrEmpty(OnClick) ? "" : String.Format(" onclick='{0}' ", OnClick);

            return new MvcHtmlString(String.Format("<input type='{0}' value='{1}' {2} {3}></input>", IsSubmit ? "submit" : "button", Content, action,Enabled?"":"disabled='disabled'"));
        }
        public static MvcHtmlString AddScripts(this HtmlHelper Html, params string[] scripts)
        {
            string template = "<script type='text/javascript' src='{0}'></script>"+Environment.NewLine;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < scripts.Length; i++)
            {
                result.Append(String.Format(template, scripts[i]));
            }
            return new MvcHtmlString(result.ToString());
        }
    }
}