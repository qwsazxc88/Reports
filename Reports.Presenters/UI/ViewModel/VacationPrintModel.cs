namespace Reports.Presenters.UI.ViewModel
{
    public class VacationPrintModel
    {
        public string OrgName { get; set; }
        public string DocNumber { get; set; }
        public string CreateDate { get; set; }
        public string FIO { get; set; }
        public string TabNumber { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string DaysNumber { get; set; }

        public string PeriodBeginDateDay { get; set; }
        public string PeriodBeginDateMonth { get; set; }
        public string PeriodBeginDateYear { get; set; }
        public string PeriodEndDateDay { get; set; }
        public string PeriodEndDateMonth { get; set; }
        public string PeriodEndDateYear { get; set; }


        public string BeginDateDay { get; set; }
        public string BeginDateMonth { get; set; }
        public string BeginDateYear { get; set; }
        public string EndDateDay { get; set; }
        public string EndDateMonth { get; set; }
        public string EndDateYear { get; set; }

        public string AllDaysNumber { get; set; }
        public string AllBeginDateDay { get; set; }
        public string AllBeginDateMonth { get; set; }
        public string AllBeginDateYear { get; set; }
        public string AllEndDateDay { get; set; }
        public string AllEndDateMonth { get; set; }
        public string AllEndDateYear { get; set; }

        public string ManagerPosition { get; set; }
        public string ManagerFIO { get; set; }
    }
}