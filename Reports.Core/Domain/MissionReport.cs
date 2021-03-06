using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// ��������� �����
    /// </summary>
    public class MissionReport : AbstractEntityWithVersion
    {
        #region Fields

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        //public virtual DateTime BeginDate { get; set; }
        //public virtual DateTime EndDate { get; set; }

        public virtual int Number { get; set; }
        public virtual string Hotels { get; set; }
        public virtual IList<ManualDeduction> ManualDeductions { get; set; }
        public virtual User StornoAddedBy { get; set; }
        public virtual decimal StornoSum { get; set; }
        public virtual string StornoComment { get; set; }
        public virtual DateTime? StornoAddedDate { get; set; }
        //public virtual MissionType Type { get; set; }
        //public virtual int Kind { get; set; }

        //public virtual MissionGoal Goal { get; set; }

        //public virtual decimal? SumAir { get; set; }
        //public virtual decimal? SumDaily { get; set; }
        //public virtual decimal? SumResidence { get; set; }
        //public virtual decimal? SumTrain { get; set; }
        public virtual decimal AllSum { get; set; }
        //public virtual decimal? UserSumDaily { get; set; }
        //public virtual decimal? UserSumResidence { get; set; }
        //public virtual decimal? UserSumAir { get; set; }
        //public virtual decimal? UserSumTrain { get; set; }
        public virtual decimal UserAllSum { get; set; }
        public virtual decimal UserSumReceived { get; set; }
        public virtual decimal AccountantAllSum { get; set; }
        public virtual decimal PurchaseBookAllSum { get; set; }


        public virtual bool IsDocumentsSaveToArchive { get; set; }
        //public virtual decimal? UserSumCash { get; set; }
        //public virtual decimal? UserSumNotCash { get; set; }

        //public virtual bool IsResidencePaid { get; set; }
        //public virtual bool IsAirTicketsPaid { get; set; }
        //public virtual bool IsTrainTicketsPaid { get; set; }

        //public virtual string ResidenceRequestNumber { get; set; }
        //public virtual string AirTicketsRequestNumber { get; set; }
        //public virtual string TrainTicketsRequestNumber { get; set; }
        //public virtual User Secretary { get; set; }

        //public virtual bool NeedToAcceptByChief { get; set; }
        //public virtual bool NeedToAcceptByChiefAsManager { get; set; }


        public virtual User User { get; set; }
        public virtual User Creator { get; set; }

        public virtual DateTime? UserDateAccept { get; set; }
        public virtual User AcceptUser { get; set; }
        public virtual DateTime? ManagerDateAccept { get; set; }
        public virtual User AcceptManager { get; set; }
        public virtual DateTime? AccountantDateAccept { get; set; }
        public virtual User AcceptAccountant { get; set; }
        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

        public virtual DateTime? ArchiveDate { get; set; }
        public virtual string ArchiveNumber { get; set; }
        public virtual User Archivist { get; set; }

        public virtual MissionOrder MissionOrder { get; set; }
        public virtual MissionOrder AdditionalMissionOrder { get; set; }

        public virtual Deduction Deduction { get; set; }
        public virtual Deduction StornoDeduction { get; set; }
        public virtual IList<MissionReportCost> Costs { get; set; }

        public virtual IList<MissionReportComment> Comments { get; set; }

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion
    }
}