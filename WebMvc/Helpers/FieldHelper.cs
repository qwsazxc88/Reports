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
using Reports.Core;
namespace WebMvc.Helpers
{
    public static class FieldHelper
    {
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, object HtmlAttributes = null, bool inline=false)
        {
            string block = "div";
            if (inline) block = "span";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label=Html.LabelFor(expression);
            var editor = Html.TextBoxFor(expression, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,block,label,editor,validation));
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, Dictionary<string,object> HtmlAttributes, bool inline =false)
        {
            string block="div";
            if (inline) block = "span";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextBoxFor(expression, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,block, label, editor, validation));
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, bool Enabled)
        {
            return Html.CreateField(expression, Enabled ? null : (object)new { disabled = "disabled" });
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, bool Enabled, Dictionary<string, object> HtmlAttributes)
        {
            if (!Enabled)
                    HtmlAttributes.Add("disabled", "disabled");            
            return Html.CreateField(expression, HtmlAttributes);
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, string format ,Expression<Func<TModel, TValue>> expression, bool Enabled)
        {
            return Html.CreateField( format, expression, Enabled ? null : (object)new { disabled = "disabled" });
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, string format, Expression<Func<TModel, TValue>> expression, object HtmlAttributes = null, bool inline=false)
        {
            string block = inline ? "span" : "div";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextBoxNewFor( expression, format, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,block, label, editor, validation));
        }
        public static MvcHtmlString CreateField<TModel, TValue>(this HtmlHelper<TModel> Html, string format, Expression<Func<TModel, TValue>> expression, Dictionary<string,object> HtmlAttributes = null, bool inline=false)
        {
            string block = inline ? "span" : "div";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextBoxNewFor(expression, format, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,block, label, editor, validation));
        }
        public static MvcHtmlString CreateDropdown<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, SelectList Items,object HtmlAttributes = null, bool inline=false)
        {
            string block = inline ? "span" : "div";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.DropDownListFor(expression, Items, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,block, label, editor, validation));
        }
        public static MvcHtmlString CreateDropdown<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, SelectList Items, bool Enabled)
        {
            return Html.CreateDropdown(expression, Items, Enabled ? null : (object)new { disabled="disabled" });
        }
        public static MvcHtmlString CreateCheckBox<TModel>(this HtmlHelper<TModel> Html, Expression<Func<TModel, bool>> expression, bool Enabled, bool hasLabel, Dictionary<string, object> HtmlAttributes = null, bool inline=false)
        {
            string block = inline ? "span" : "div";
            if (HtmlAttributes == null) HtmlAttributes = new Dictionary<string, object>();
            if (!Enabled)
                HtmlAttributes.Add("disabled", "disabled");
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}&nbsp;</span>{2}</{0}>";
            string label = hasLabel ? Html.LabelFor(expression) + ":" : "";             
            var editor = Html.CheckBoxFor(expression,HtmlAttributes);            
            return new MvcHtmlString(String.Format(template,block, label, editor));
        }
            
        public static MvcHtmlString CreateDisplay<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, bool inline=false)
        {
            string block = inline ? "span" : "div";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.DisplayFor(expression);
            return new MvcHtmlString(String.Format(template,block, label, editor));
        }

        public static MvcHtmlString CreateFileInput<TModel, TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel, TValue>> expression, bool inline=false)
        {
            string block = inline ? "span" : "div";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextBoxFor(expression, new { type = "file" });
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template, label, editor, validation));
        }

        public static MvcHtmlString CreateApprove<TModel>(this HtmlHelper<TModel> Html, Expression<Func<TModel,ApprovalViewModel>> element, Dictionary<string,object> HtmlAttributes = null, bool inline=false)
        {
            string block = inline ? "span" : "div";
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";

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
            return new MvcHtmlString(String.Format(template,block, label, editor, approve));
        }
        
        public static MvcHtmlString CreateButton(this HtmlHelper Html, bool IsSubmit, bool Enabled, string Content, string OnClick = "")
        {
            string action = String.IsNullOrEmpty(OnClick) ? "" : String.Format(" onclick='{0}' ", OnClick);

            return new MvcHtmlString(String.Format("<input type='{0}' value='{1}' {2} {3}></input>", IsSubmit ? "submit" : "button", Content, action,Enabled?"":"disabled='disabled'"));
        }
        public static MvcHtmlString CreateButton(this HtmlHelper Html, bool IsSubmit, bool Enabled, string Content, Dictionary<string,object> HtmlAttributes, string OnClick = "")
        {
            string action = String.IsNullOrEmpty(OnClick) ? "" : String.Format(" onclick='{0}' ", OnClick);
            StringBuilder attributes = new StringBuilder();
            foreach (var attr in HtmlAttributes)
                attributes.Append(String.Format(" {0}={1} ", attr.Key, attr.Value));
            return new MvcHtmlString(String.Format("<input type='{0}' value='{1}' {2} {3} {4}></input>", IsSubmit ? "submit" : "button", Content, action, Enabled ? "" : "disabled='disabled'", attributes.ToString()));
        }

        public static MvcHtmlString CreateLink(this HtmlHelper Html, string href, string content, string Class)
        {
            string template = "<a href='{0}' class='{1}'>{2}</a>";
            return new MvcHtmlString(String.Format(template, href, Class, content));
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
        public static MvcHtmlString AddStyles(this HtmlHelper Html, params string[] styles)
        {
            
            string template = "<link href='{0}' rel='stylesheet' type='text/css' />"+Environment.NewLine;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < styles.Length; i++)
            {
                result.Append(String.Format(template, styles[i]));
            }
            return new MvcHtmlString(result.ToString());
        }

        public static MvcHtmlString CreateHeader(this HtmlHelper Html, string content)
        {
            string template = "<h2>{0}</h2><br/>";
            return new MvcHtmlString(String.Format(template, content));
        }
        public static MvcHtmlString CreateTextArea<TModel,TValue>(this HtmlHelper<TModel> Html, Expression<Func<TModel,TValue>> expression, bool Enabled, Dictionary<string,object> HtmlAttributes=null, bool inline=false)
        {
            string block = inline ? "span" : "div";
            if (HtmlAttributes == null) HtmlAttributes = new Dictionary<string, object>();
            if (!Enabled)
                HtmlAttributes.Add("disabled", "disabled");
            string template = "<{0} class='field-wrap'><span style='" + (inline ? "" : "width:200px;") + "display:inline-block;'>{1}:&nbsp;</span>{2}{3}</{0}>";
            var label = Html.LabelFor(expression);
            var editor = Html.TextAreaFor(expression, HtmlAttributes);
            var validation = Html.ValidationMessageFor(expression);
            return new MvcHtmlString(String.Format(template,block, label, editor, validation));
        }

        #region Angular
        public static MvcHtmlString ngAutoComplete(this HtmlHelper html,string ngModel, string Collection, string element, string selectOption)
        {
            string template = "<oi-select oi-options='{0} for {1} in {2}' ng-model='{3}' ></oi-select>";
            return new MvcHtmlString(String.Format(template,selectOption,element,Collection,ngModel));

        }        
        #endregion
    }
}