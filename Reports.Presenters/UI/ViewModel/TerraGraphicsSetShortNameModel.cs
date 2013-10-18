using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class TerraGraphicsEditPointModel
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public int UserId { get; set; }

        public List<IdNameDto> EpLevel1 { get; set; }
        public List<IdNameDto> EpLevel2 { get; set; }
        public List<IdNameDto> EpLevel3 { get; set; }
        public int EpLevel1ID { get; set; }
        public int EpLevel2ID { get; set; }
        public int EpLevel3ID { get; set; }

        
        [Display(Name = "Кредиты")]
        public int Credit { get; set; }
        public List<IdNameDto> Credits { get; set; }

        [Display(Name = "План")]
        public string Hours { get; set; }


       
        public bool IsEditable { get; set; }
        public bool IsCreditsEditable { get; set; }
    }
}