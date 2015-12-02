using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
namespace WebMvc.Helpers
{
    public static class FieldsetExtension
    {
        public static Fieldset BeginFieldset(this HtmlHelper Html, string Class, string legend, params string[] htmlAttributes)
        {
            StringBuilder attributes = new StringBuilder();
            for (int i = 0; i < htmlAttributes.Length; i++)
                attributes.Append(htmlAttributes[i] + ' ');
            Fieldset block = new Fieldset(Html.ViewContext);
            Html.ViewContext.Writer.Write(String.Format("<fieldset class='{0}' {1}><legend title='{2}'>{2}</legend>", Class, attributes.ToString(),legend));
            return block;
        }
        public static void EndFieldest(this HtmlHelper htmlHelper)
        {
            EndFieldest(htmlHelper.ViewContext);
        }

        internal static void EndFieldest(ViewContext viewContext)
        {
            viewContext.Writer.Write("</fieldset>");
        }
    }
    public class Fieldset : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public Fieldset(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }
            this._viewContext = viewContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                FieldsetExtension.EndFieldest(_viewContext);
            }
        }

        public void EndFieldset()
        {
            Dispose(true);
        }
    }
}