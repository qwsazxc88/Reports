using System;
using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class PrintMissionOrderViewModel:UserInfoModel
    {
        /*public int Number { get; set; }
        public string Date { get; set; }
        public string Organization { get; set; }
        public string UserName { get; set; }
        public string TabNumber { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Manager { get; set; }
        public string Personnel { get; set; }*/


        public string Goal { get; set; }

        public List<PrintMissionTargetViewModel> Targets { get; set; }

        public string GradeDailySum { get; set; }
        public string GradeResSum { get; set; }
        public string GradeAviaSum { get; set; }
        public string GradeTrainSum { get; set; }
        public string GradeAllSum { get; set; }

        public string UserDailySum { get; set; }
        public string UserResSum { get; set; }
        public string UserAviaSum { get; set; }
        public string UserTrainSum { get; set; }
        public string UserAllSum { get; set; }

        public string UserCashSum { get; set; }
        public string UserNotCashSum { get; set; }

        public string ManagerPosition { get; set; }
        public string ManName { get; set; }
        public string ManagerDate { get; set; }

        public bool IsLongOrder { get; set; }

        public string ChiefPosition { get; set; }
        public string ChiefName { get; set; }
        public string ChiefDate { get; set; }

    }
    public class PrintMissionTargetViewModel
    {
        public int Number { get; set; }
        public string Country { get; set; }
        public string Organization { get; set; }
        public string BeginDay { get; set; }
        public string BeginMonth { get; set; }
        public string BeginYear { get; set; }
        public string EndDay { get; set; }
        public string EndMonth { get; set; }
        public string EndYear { get; set; }
        public int Days { get; set; }
        public int RealDays { get; set; }

       
    }
}
