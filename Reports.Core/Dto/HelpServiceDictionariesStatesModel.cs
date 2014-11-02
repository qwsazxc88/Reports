using System.Collections.Generic;

namespace Reports.Core.Dto
{
    public interface IHelpServiceDictionariesStates
    {
        bool IsAttachmentVisible { get; set; }
        bool IsRequirementsVisible { get; set; }
        bool IsPeriodVisible { get; set; }
        IList<IdNameDto> Periods { get; set; }
        int PeriodId { get; set; }
    }

    public class HelpServiceDictionariesStatesModel : IHelpServiceDictionariesStates
    {
        public string Error { get; set; }

        #region IHelpServiceDictionariesStates Members

        public bool IsAttachmentVisible { get; set; }
        public bool IsRequirementsVisible { get; set; }
        public bool IsPeriodVisible { get; set; }
        public IList<IdNameDto> Periods { get; set; }
        public int PeriodId { get; set; }
        #endregion
    }
}