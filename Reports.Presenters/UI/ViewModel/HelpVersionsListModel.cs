using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class HelpVersionsListModel
    {
        public bool IsAddAvailable { get; set; }
        public List<HelpVersionDto> Versions { get; set; }
    }
    public class HelpFaqListModel
    {
        public bool IsAddAvailable { get; set; }
        public List<HelpFaqDto> Questions { get; set; }
    }
}