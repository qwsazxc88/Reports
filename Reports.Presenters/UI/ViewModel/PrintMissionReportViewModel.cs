using System.Collections.Generic;

namespace Reports.Presenters.UI.ViewModel
{
    public class PrintMissionReportViewModel : UserInfoModel
    {
        public string OrderNumber { get; set; }
        public List<PrintCostModel> Costs { get; set; }
        public string Hotels { get; set; }
        public string UserPosition { get; set; }
        public string UserFio { get; set; }
        public string UserAcceptDate { get; set; }
        public string ManagerPosition { get; set; }
        public string ManagerFio { get; set; }
        public string ManagerAcceptDate { get; set; }
        public string AccountantPosition { get; set; }
        public string AccountantFio { get; set; }
        public string AccountantAcceptDate { get; set; }
    }

    public class PrintCostModel
    {
        public string Number { get; set; }
        public string CostTypeName { get; set; }
        public string GradeSum { get; set; }
        public string UserSum { get; set; }
        public string PurchaseBoolSum { get; set; }
        public string AccountantSum { get; set; }
        public List<PrintTransactionModel> Transactions { get; set; }
    }
    public class PrintTransactionModel
    {
        public string Sum { get; set; }
        public string Debet { get; set; }
        public string Credit { get; set; }
    }
    public class PrintMissionReportListViewModel
    {
        public string To { get; set; }
        public string Address { get; set; }
        public List<string> Costs { get; set; }
    }
   
}