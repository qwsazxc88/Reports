using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Presenters.UI.ViewModel
{
    public class TerraGraphicsSetShortNameModel
    {
        public List<IdNameDto> Level1;
        public List<IdNameDto> Level2;
        public List<IdNameDto> Level3;
        public int Level1ID { get; set; }
        public int Level2ID { get; set; }
        public int Level3ID { get; set; }

        public string ShortName { get; set; }
        public bool IsShortNameEditable { get; set; }
    }
}