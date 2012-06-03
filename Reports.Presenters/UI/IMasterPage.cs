using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;

namespace Reports.Presenters.UI
{
    public interface IMasterPage
    {
        TreeView MapTreeView { get;}
        Uri RequestUrl { get; }
        bool BirthdayControlVisible { set; }
    }
}
