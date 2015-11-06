using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.ViewModel
{
    public class StaffMovementsPrintModel
    {
        public string OrderNumber { get; set; }
        public string AdditionNumber { get; set; }
        public string UserName { get; set; }
        public string ShortUserName { get; set; }
        public string TargetPosition { get; set; }
        public string SourcePosition { get; set; }
        public string TargetDepartment { get; set; }
        public string SourceDepartment { get; set; }
        public string SourceManager { get; set; }
        public string TargetManager { get; set; }
        public string Chief { get; set; }
        public string ChiefDepartment { get; set; }
        public string ChiefPosition { get; set; }
        public string PersonnelManagerBank {get;set;}
        public string PersonnelManagerBankDateAccept { get; set; }
        public string TargetCasing { get; set; }
        public string HoursType { get; set; }
        public List<string> Additions { get; set; }
        public string MaterialDocNumber { get; set; }
        public string SignerName { get; set; }
        public string SignerShortName { get; set; }
        public string SignerAdditionData { get; set; }
        public string SignerPosition { get; set; }
        public string MovementDate { get; set; }
        public string CreateDate { get; set; }
        public string Dep2 { get; set; }
        public string Dep3 { get; set; }
        public string Dep4 { get; set; }
        public string Dep5 { get; set; }
        public string Dep6 { get; set; }
        public string Dep7 { get; set; }
        public string TargetDep2 { get; set; }
        public string TargetDep3 { get; set; }
        public string TargetDep4 { get; set; }
        public string TargetDep5 { get; set; }
        public string TargetDep6 { get; set; }
        public string TargetDep7 { get; set; }

        public string[] AgreementField1_2 { get; set; }
        public string[] AgreementField1_6 { get; set; }
        public string[] AgreementField5_1 { get; set; }
        public string[] AgreementField4_2 { get; set; }
        public string AgreementEntry1_2 { get; set; }
        public string AgreementEntry1_6 { get; set; }
        public string AgreementEntry2_2_1 { get; set; }
        public string AgreementEntry4_2 { get; set; }
        public string AgreementEntry5_1 { get; set; }
    }
}
