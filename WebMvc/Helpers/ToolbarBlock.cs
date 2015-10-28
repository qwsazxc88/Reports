﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Helpers
{
    public static class BlockExtension
    {
        public static ToolbarBlock BeginBlock(this HtmlHelper Html, string Class)
        {
            ToolbarBlock block = new ToolbarBlock(Html.ViewContext);
            Html.ViewContext.Writer.Write(String.Format("<div class='{0}'>",Class));
            return block;
        }
        public static void EndBlock(this HtmlHelper htmlHelper)
        {
            EndBlock(htmlHelper.ViewContext);
        }

        internal static void EndBlock(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
        }
    }
    public class ToolbarBlock: IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public ToolbarBlock(ViewContext viewContext)
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
                BlockExtension.EndBlock(_viewContext);
            }
        }

        public void EndBlock()
        {
            Dispose(true);
        }
    }
}