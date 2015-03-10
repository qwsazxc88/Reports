using System;

namespace Reports.Core.Dto
{
    public class MissionOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Number { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public int OrderNumber { get; set; }
        public DateTime EditDate { get; set; }
        public bool IsRecalculated { get; set; }
        public int? AdditionalOrderId { get; set; }
        public string AdditionalOrderNumber { get; set; }
        public DateTime? AdditionalOrderEditDate { get; set; }
        public string MissionType { get; set; }
        public string MissionGoal { get; set; }
        public string MissionKind { get; set; }
        public string Target { get; set; }
        public int Grade { get; set; }
        public decimal GradeSum { get; set; }
        public Decimal? GradeIncrease { get; set; }
        public decimal UserSum { get; set; }
        public string HasMission { get; set; }
        public string NeedSecretary { get; set; }
        public string State { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AdditionalOrderState { get; set; }
        public DateTime? AdditionalOrderBeginDate { get; set; }
        public DateTime? AdditionalOrderEndDate { get; set; }
        public string AirTicketType { get; set; }
        public string TrainTicketType { get; set; }
        public bool Flag { get; set; }
        public bool IsChecked { get; set; }        
        
        //public bool IsApproveFlagAvailable { get; set; }
    }
    public class MissionReportDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Number { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
        public string Dep3Name { get; set; }
        public string Dep7Name { get; set; }
        public DateTime EditDate { get; set; }
        public int ReportNumber { get; set; }
        public int OrderNumber { get; set; }
        public int Grade { get; set; }
        public decimal GradeSum { get; set; }
        public decimal UserSum { get; set; }
        public decimal UserSumReceived { get; set; }
        public decimal AccountantSum { get; set; }
        public Decimal? GradeIncrease { get; set; }
        public Decimal? Saldo { get; set; }
        public string State { get; set; }
        public string AccountantName { get; set; }
        public string IsDocumentsSaveToArchive { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public string ArchiveNumber { get; set; }
    }
}