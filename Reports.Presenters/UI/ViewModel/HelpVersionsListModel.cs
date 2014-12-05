using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class HelpTemplateListModel
    {
        public bool IsAddAvailable { get; set; }
        public List<HelpTemplateDto> Documents { get; set; }
    }
    public class HelpTemplateEditModel
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}