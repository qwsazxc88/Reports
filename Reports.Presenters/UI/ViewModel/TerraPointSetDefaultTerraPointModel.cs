using System;

namespace Reports.Presenters.UI.ViewModel
{
    public class TerraPointSetDefaultTerraPointModel
    {
        public int PointToUserId { get; set; }
        public string Error { get; set; }
    }

    public class TerraPointSaveModel
    {
        public int Id { get; set; }
        public int PointId { get; set; }
        public int UserId { get; set; }
        public string Day { get; set; }
        public string Hours { get; set; }
        public int IsCreditAvailable { get; set; }
        public string Error { get; set; }

        public DateTime TpDay { get; set; }
        public decimal TpHours { get; set; }
    }
}