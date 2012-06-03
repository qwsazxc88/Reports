using System;

namespace Reports.Presenters.UI.ViewModel
{
    public interface IExportImport : IEmailDtoSupport
    {
        DateTime Month { get; }
        string Error { get; set; }
    }
}