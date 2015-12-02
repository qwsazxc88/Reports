using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Helpers
{
    public static class TableExtension
    {
        public static Table BeginTable(this HtmlHelper Html, string Class, params string[] htmlAttributes)
        {
            StringBuilder attributes = new StringBuilder();
            for (int i = 0; i < htmlAttributes.Length; i++)
                attributes.Append(htmlAttributes[i] + ' ');
            Table block = new Table(Html.ViewContext);
            Html.ViewContext.Writer.Write(String.Format("<Table class='{0}' {1}>", Class, attributes.ToString()));
            return block;
        }
        public static void EndTable(this HtmlHelper htmlHelper)
        {
            EndTable(htmlHelper.ViewContext);
        }

        internal static void EndTable(ViewContext viewContext)
        {
            viewContext.Writer.Write("</Table>");
        }
    }
    public class Table : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public Table(ViewContext viewContext)
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
                TableExtension.EndTable(_viewContext);
            }
        }

        public void EndBlock()
        {
            Dispose(true);
        }
    }
    public static class TableRowExtension
    {
        public static MvcHtmlString TR(this HtmlHelper Html, string content,string Class, params string[] htmlAttributes)
        {
            StringBuilder attributes = new StringBuilder();
            for (int i = 0; i < htmlAttributes.Length; i++)
                attributes.Append(htmlAttributes[i] + ' ');
            return new MvcHtmlString(String.Format("<tr class='{0}' {1}>{2}</tr>",Class,attributes,content));
        }
        public static MvcHtmlString TR(this HtmlHelper Html, MvcHtmlString content, string Class, params string[] htmlAttributes)
        {
            return Html.TR(content, Class, htmlAttributes);
        }
        public static TableRow BeginRow(this HtmlHelper Html, string Class, params string[] htmlAttributes)
        {
            StringBuilder attributes = new StringBuilder();
            for (int i = 0; i < htmlAttributes.Length; i++)
                attributes.Append(htmlAttributes[i] + ' ');
            TableRow block = new TableRow(Html.ViewContext);
            Html.ViewContext.Writer.Write(String.Format("<tr class='{0}' {1}>", Class, attributes.ToString()));
            return block;
        }
        public static void EndRow(this HtmlHelper htmlHelper)
        {
            EndRow(htmlHelper.ViewContext);
        }

        internal static void EndRow(ViewContext viewContext)
        {
            viewContext.Writer.Write("</tr>");
        }
    }
    public class TableRow : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public TableRow(ViewContext viewContext)
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
                TableRowExtension.EndRow(_viewContext);
            }
        }

        public void EndBlock()
        {
            Dispose(true);
        }
    }
    public static class TableCellExtension
    {
        public static MvcHtmlString TD(this HtmlHelper Html, string content, string Class, params string[] htmlAttributes)
        {
            StringBuilder attributes = new StringBuilder();
            for (int i = 0; i < htmlAttributes.Length; i++)
                attributes.Append(htmlAttributes[i] + ' ');
            return new MvcHtmlString(String.Format("<td class='{0}' {1}>{2}</td>", Class, attributes, content));
        }
        public static MvcHtmlString TD(this HtmlHelper Html, MvcHtmlString content, string Class, params string[] htmlAttributes)
        {
            return Html.TD(content.ToString(), Class, htmlAttributes);
        }
        public static TableCell BeginCell(this HtmlHelper Html, string Class, params string[] htmlAttributes)
        {
            StringBuilder attributes = new StringBuilder();
            for (int i = 0; i < htmlAttributes.Length; i++)
                attributes.Append(htmlAttributes[i] + ' ');
            TableCell block = new TableCell(Html.ViewContext);
            Html.ViewContext.Writer.Write(String.Format("<td class='{0}' {1}>", Class, attributes.ToString()));
            return block;
        }
        public static void EndCell(this HtmlHelper htmlHelper)
        {
            EndCell(htmlHelper.ViewContext);
        }

        internal static void EndCell(ViewContext viewContext)
        {
            viewContext.Writer.Write("</td>");
        }
    }
    public class TableCell : IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public TableCell(ViewContext viewContext)
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
                TableCellExtension.EndCell(_viewContext);
            }
        }

        public void EndBlock()
        {
            Dispose(true);
        }
    }
}