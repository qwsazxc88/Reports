if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReport_Appointment]') AND parent_object_id = OBJECT_ID('AppointmentReport'))
alter table AppointmentReport  drop constraint FK_AppointmentReport_Appointment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReport_AppointmentEducationType]') AND parent_object_id = OBJECT_ID('AppointmentReport'))
alter table AppointmentReport  drop constraint FK_AppointmentReport_AppointmentEducationType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReport_CreatorUser]') AND parent_object_id = OBJECT_ID('AppointmentReport'))
alter table AppointmentReport  drop constraint FK_AppointmentReport_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReport_StaffUser]') AND parent_object_id = OBJECT_ID('AppointmentReport'))
alter table AppointmentReport  drop constraint FK_AppointmentReport_StaffUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReport_AcceptManager]') AND parent_object_id = OBJECT_ID('AppointmentReport'))
alter table AppointmentReport  drop constraint FK_AppointmentReport_AcceptManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReport_DeleteUser]') AND parent_object_id = OBJECT_ID('AppointmentReport'))
alter table AppointmentReport  drop constraint FK_AppointmentReport_DeleteUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionResidenceGradeValue_Residence]') AND parent_object_id = OBJECT_ID('MissionResidenceGradeValue'))
alter table MissionResidenceGradeValue  drop constraint FK_MissionResidenceGradeValue_Residence

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionResidenceGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionResidenceGradeValue'))
alter table MissionResidenceGradeValue  drop constraint FK_MissionResidenceGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_DismissalType]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_DismissalType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_User]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_CreatorUser]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Dismissal_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Dismissal'))
alter table Dismissal  drop constraint FK_Dismissal_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AbsenceComment_User]') AND parent_object_id = OBJECT_ID('AbsenceComment'))
alter table AbsenceComment  drop constraint FK_AbsenceComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AbsenceComment_Absence]') AND parent_object_id = OBJECT_ID('AbsenceComment'))
alter table AbsenceComment  drop constraint FK_AbsenceComment_Absence

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_VacationComment_User]') AND parent_object_id = OBJECT_ID('VacationComment'))
alter table VacationComment  drop constraint FK_VacationComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_VacationComment_Vacation]') AND parent_object_id = OBJECT_ID('VacationComment'))
alter table VacationComment  drop constraint FK_VacationComment_Vacation

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_MissionType]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_MissionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_User]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_CreatorUser]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Mission_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Mission'))
alter table Mission  drop constraint FK_Mission_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_SicklistType]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_SicklistType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_BabyMindingType]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_BabyMindingType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_SicklistPaymentPercent]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_SicklistPaymentPercent

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_SicklistPaymentRestrictType]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_SicklistPaymentRestrictType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_User]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_CreatorUser]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_ApprovedByManagerUser]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_ApprovedByManagerUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_ApprovedByPersonnelManagerUser]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_ApprovedByPersonnelManagerUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Sicklist_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Sicklist'))
alter table Sicklist  drop constraint FK_Sicklist_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AcceptRequestDate_User]') AND parent_object_id = OBJECT_ID('AcceptRequestDate'))
alter table AcceptRequestDate  drop constraint FK_AcceptRequestDate_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentSubType_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('EmployeeDocumentSubType'))
alter table EmployeeDocumentSubType  drop constraint FK_DocumentSubType_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AccountingTransaction_MissionReportCost]') AND parent_object_id = OBJECT_ID('AccountingTransaction'))
alter table AccountingTransaction  drop constraint FK_AccountingTransaction_MissionReportCost

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AccountingTransaction_DebitAccount]') AND parent_object_id = OBJECT_ID('AccountingTransaction'))
alter table AccountingTransaction  drop constraint FK_AccountingTransaction_DebitAccount

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AccountingTransaction_CreditAccount]') AND parent_object_id = OBJECT_ID('AccountingTransaction'))
alter table AccountingTransaction  drop constraint FK_AccountingTransaction_CreditAccount

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportComment_User]') AND parent_object_id = OBJECT_ID('MissionReportComment'))
alter table MissionReportComment  drop constraint FK_MissionReportComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportComment_MissionReport]') AND parent_object_id = OBJECT_ID('MissionReportComment'))
alter table MissionReportComment  drop constraint FK_MissionReportComment_MissionReport

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderComment_User]') AND parent_object_id = OBJECT_ID('MissionOrderComment'))
alter table MissionOrderComment  drop constraint FK_MissionOrderComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderComment_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionOrderComment'))
alter table MissionOrderComment  drop constraint FK_MissionOrderComment_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChildVacationComment_User]') AND parent_object_id = OBJECT_ID('ChildVacationComment'))
alter table ChildVacationComment  drop constraint FK_ChildVacationComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChildVacationComment_ChildVacation]') AND parent_object_id = OBJECT_ID('ChildVacationComment'))
alter table ChildVacationComment  drop constraint FK_ChildVacationComment_ChildVacation

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_DeductionType]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_DeductionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_DeductionKind]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_DeductionKind

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_User]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_EditorUser]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_EditorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_Department]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AppointmentReason]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AppointmentReason

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_CreatorUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AcceptManager]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AcceptManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AcceptChief]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AcceptChief

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_StaffUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_StaffUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_DeleteUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_DeleteUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrectionComment_User]') AND parent_object_id = OBJECT_ID('TimesheetCorrectionComment'))
alter table TimesheetCorrectionComment  drop constraint FK_TimesheetCorrectionComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrectionComment_TimesheetCorrection]') AND parent_object_id = OBJECT_ID('TimesheetCorrectionComment'))
alter table TimesheetCorrectionComment  drop constraint FK_TimesheetCorrectionComment_TimesheetCorrection

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionPurchaseBookDocument]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionPurchaseBookDocument

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionReportCostType]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionReportCostType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionReportCost]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionReportCost

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_User]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_EditorUser]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_EditorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_User]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_CreatorUser]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AcceptUser]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AcceptUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AcceptManager]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AcceptManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AcceptAccountant]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AcceptAccountant

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChiefToUser_Chief]') AND parent_object_id = OBJECT_ID('ChiefToUser'))
alter table ChiefToUser  drop constraint FK_ChiefToUser_Chief

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChiefToUser_User]') AND parent_object_id = OBJECT_ID('ChiefToUser'))
alter table ChiefToUser  drop constraint FK_ChiefToUser_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DismissalComment_User]') AND parent_object_id = OBJECT_ID('DismissalComment'))
alter table DismissalComment  drop constraint FK_DismissalComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DismissalComment_Dismissal]') AND parent_object_id = OBJECT_ID('DismissalComment'))
alter table DismissalComment  drop constraint FK_DismissalComment_Dismissal

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionComment_User]') AND parent_object_id = OBJECT_ID('MissionComment'))
alter table MissionComment  drop constraint FK_MissionComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionComment_Mission]') AND parent_object_id = OBJECT_ID('MissionComment'))
alter table MissionComment  drop constraint FK_MissionComment_Mission

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_MissionType]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_MissionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_MissionGoal]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_MissionGoal

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_Secretary]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_Secretary

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_User]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_CreatorUser]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_AcceptUser]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_AcceptUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_AcceptManager]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_AcceptManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_AcceptChief]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_AcceptChief

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_Mission]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_Mission

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_EmploymentComment_User]') AND parent_object_id = OBJECT_ID('EmploymentComment'))
alter table EmploymentComment  drop constraint FK_EmploymentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_EmploymentComment_Employment]') AND parent_object_id = OBJECT_ID('EmploymentComment'))
alter table EmploymentComment  drop constraint FK_EmploymentComment_Employment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookDocument_Contractor]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookDocument'))
alter table MissionPurchaseBookDocument  drop constraint FK_MissionPurchaseBookDocument_Contractor

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookDocument_EditorUser]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookDocument'))
alter table MissionPurchaseBookDocument  drop constraint FK_MissionPurchaseBookDocument_EditorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderTarget_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionOrderTarget_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_Country]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_Country

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_DailyAllowance]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_DailyAllowance

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_Residence]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_Residence

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_AirTicketType]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_AirTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_TrainTicketType]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_TrainTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TerraPointToUser_TerraPoint]') AND parent_object_id = OBJECT_ID('TerraPointToUser'))
alter table TerraPointToUser  drop constraint FK_TerraPointToUser_TerraPoint

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_InspectorToUser_Inspector]') AND parent_object_id = OBJECT_ID('InspectorToUser'))
alter table InspectorToUser  drop constraint FK_InspectorToUser_Inspector

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_InspectorToUser_User]') AND parent_object_id = OBJECT_ID('InspectorToUser'))
alter table InspectorToUser  drop constraint FK_InspectorToUser_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentComment_User]') AND parent_object_id = OBJECT_ID('DocumentComment'))
alter table DocumentComment  drop constraint FK_DocumentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentComment_Document]') AND parent_object_id = OBJECT_ID('DocumentComment'))
alter table DocumentComment  drop constraint FK_DocumentComment_Document

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionDailyAllowanceGradeValue_DailyAllowance]') AND parent_object_id = OBJECT_ID('MissionDailyAllowanceGradeValue'))
alter table MissionDailyAllowanceGradeValue  drop constraint FK_MissionDailyAllowanceGradeValue_DailyAllowance

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionDailyAllowanceGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionDailyAllowanceGradeValue'))
alter table MissionDailyAllowanceGradeValue  drop constraint FK_MissionDailyAllowanceGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_TimesheetCorrectionType]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_TimesheetCorrectionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_User]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_CreatorUser]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetCorrection_TimesheetStatus]') AND parent_object_id = OBJECT_ID('TimesheetCorrection'))
alter table TimesheetCorrection  drop constraint FK_TimesheetCorrection_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_SicklistComment_User]') AND parent_object_id = OBJECT_ID('SicklistComment'))
alter table SicklistComment  drop constraint FK_SicklistComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_SicklistComment_Sicklist]') AND parent_object_id = OBJECT_ID('SicklistComment'))
alter table SicklistComment  drop constraint FK_SicklistComment_Sicklist

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_UserManager]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_UserManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_Organization]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_Organization

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_Position]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_Position

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_User_Department]') AND parent_object_id = OBJECT_ID('[Users]'))
alter table [Users]  drop constraint FK_User_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserToPersonnel_Personnel]') AND parent_object_id = OBJECT_ID('UserToPersonnel'))
alter table UserToPersonnel  drop constraint FK_UserToPersonnel_Personnel

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserToPersonnel_User]') AND parent_object_id = OBJECT_ID('UserToPersonnel'))
alter table UserToPersonnel  drop constraint FK_UserToPersonnel_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ToManager3_Manager2]') AND parent_object_id = OBJECT_ID('AppointmentManager2ToManager3'))
alter table AppointmentManager2ToManager3  drop constraint FK_AppointmentManager2ToManager3_Manager2

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ToManager3_Manager3]') AND parent_object_id = OBJECT_ID('AppointmentManager2ToManager3'))
alter table AppointmentManager2ToManager3  drop constraint FK_AppointmentManager2ToManager3_Manager3

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTrainTicketTypeGradeValue_TrainTicketType]') AND parent_object_id = OBJECT_ID('MissionTrainTicketTypeGradeValue'))
alter table MissionTrainTicketTypeGradeValue  drop constraint FK_MissionTrainTicketTypeGradeValue_TrainTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTrainTicketTypeGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionTrainTicketTypeGradeValue'))
alter table MissionTrainTicketTypeGradeValue  drop constraint FK_MissionTrainTicketTypeGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_WorkingGraphicTypeToUser_WorkingGraphicType]') AND parent_object_id = OBJECT_ID('WorkingGraphicTypeToUser'))
alter table WorkingGraphicTypeToUser  drop constraint FK_WorkingGraphicTypeToUser_WorkingGraphicType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_WorkingGraphicType_User]') AND parent_object_id = OBJECT_ID('WorkingGraphicTypeToUser'))
alter table WorkingGraphicTypeToUser  drop constraint FK_WorkingGraphicType_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWorkComment_User]') AND parent_object_id = OBJECT_ID('HolidayWorkComment'))
alter table HolidayWorkComment  drop constraint FK_HolidayWorkComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWorkComment_HolidayWork]') AND parent_object_id = OBJECT_ID('HolidayWorkComment'))
alter table HolidayWorkComment  drop constraint FK_HolidayWorkComment_HolidayWork

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_AbsenceType]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_AbsenceType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_User]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_CreatorUser]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Absence_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Absence'))
alter table Absence  drop constraint FK_Absence_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentComment_User]') AND parent_object_id = OBJECT_ID('AppointmentComment'))
alter table AppointmentComment  drop constraint FK_AppointmentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentComment_Appointment]') AND parent_object_id = OBJECT_ID('AppointmentComment'))
alter table AppointmentComment  drop constraint FK_AppointmentComment_Appointment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderRoleRecord_User]') AND parent_object_id = OBJECT_ID('[MissionOrderRoleRecord]'))
alter table [MissionOrderRoleRecord]  drop constraint FK_MissionOrderRoleRecord_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderRoleRecord_MissionOrderRole]') AND parent_object_id = OBJECT_ID('[MissionOrderRoleRecord]'))
alter table [MissionOrderRoleRecord]  drop constraint FK_MissionOrderRoleRecord_MissionOrderRole

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderRoleRecord_TargetUser]') AND parent_object_id = OBJECT_ID('[MissionOrderRoleRecord]'))
alter table [MissionOrderRoleRecord]  drop constraint FK_MissionOrderRoleRecord_TargetUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderRoleRecord_TargetDepartment]') AND parent_object_id = OBJECT_ID('[MissionOrderRoleRecord]'))
alter table [MissionOrderRoleRecord]  drop constraint FK_MissionOrderRoleRecord_TargetDepartment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChildVacation_User]') AND parent_object_id = OBJECT_ID('ChildVacation'))
alter table ChildVacation  drop constraint FK_ChildVacation_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChildVacation_CreatorUser]') AND parent_object_id = OBJECT_ID('ChildVacation'))
alter table ChildVacation  drop constraint FK_ChildVacation_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChildVacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID('ChildVacation'))
alter table ChildVacation  drop constraint FK_ChildVacation_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_EmploymentType]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_EmploymentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_EmploymentHoursType]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_EmploymentHoursType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_Addition]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_Addition

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_Position]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_Position

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_User]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_CreatorUser]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Employment_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Employment'))
alter table Employment  drop constraint FK_Employment_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetDay_Status]') AND parent_object_id = OBJECT_ID('TimesheetDay'))
alter table TimesheetDay  drop constraint FK_TimesheetDay_Status

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TimesheetDay_Timesheet]') AND parent_object_id = OBJECT_ID('TimesheetDay'))
alter table TimesheetDay  drop constraint FK_TimesheetDay_Timesheet

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistRoleRecord_User]') AND parent_object_id = OBJECT_ID('[ClearanceChecklistRoleRecord]'))
alter table [ClearanceChecklistRoleRecord]  drop constraint FK_ClearanceChecklistRoleRecord_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistRoleRecord_ClearanceChecklistRole]') AND parent_object_id = OBJECT_ID('[ClearanceChecklistRoleRecord]'))
alter table [ClearanceChecklistRoleRecord]  drop constraint FK_ClearanceChecklistRoleRecord_ClearanceChecklistRole

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistRoleRecord_TargetUser]') AND parent_object_id = OBJECT_ID('[ClearanceChecklistRoleRecord]'))
alter table [ClearanceChecklistRoleRecord]  drop constraint FK_ClearanceChecklistRoleRecord_TargetUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistRoleRecord_TargetDepartment]') AND parent_object_id = OBJECT_ID('[ClearanceChecklistRoleRecord]'))
alter table [ClearanceChecklistRoleRecord]  drop constraint FK_ClearanceChecklistRoleRecord_TargetDepartment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistApproval_Dismissal]') AND parent_object_id = OBJECT_ID('ClearanceChecklistApproval'))
alter table ClearanceChecklistApproval  drop constraint FK_ClearanceChecklistApproval_Dismissal

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistApproval_ClearanceChecklistRole]') AND parent_object_id = OBJECT_ID('ClearanceChecklistApproval'))
alter table ClearanceChecklistApproval  drop constraint FK_ClearanceChecklistApproval_ClearanceChecklistRole

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistApproval_ApprovedBy]') AND parent_object_id = OBJECT_ID('ClearanceChecklistApproval'))
alter table ClearanceChecklistApproval  drop constraint FK_ClearanceChecklistApproval_ApprovedBy

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentCreateManager2ToDepartment2_User]') AND parent_object_id = OBJECT_ID('AppointmentCreateManager2ToDepartment2'))
alter table AppointmentCreateManager2ToDepartment2  drop constraint FK_AppointmentCreateManager2ToDepartment2_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentCreateManager2ToDepartment2_Department]') AND parent_object_id = OBJECT_ID('AppointmentCreateManager2ToDepartment2'))
alter table AppointmentCreateManager2ToDepartment2  drop constraint FK_AppointmentCreateManager2ToDepartment2_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_VacationType]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_VacationType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_User]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_CreatorUser]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Timesheet_User]') AND parent_object_id = OBJECT_ID('Timesheet'))
alter table Timesheet  drop constraint FK_Timesheet_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_UserLogin_User]') AND parent_object_id = OBJECT_ID('UserLogin'))
alter table UserLogin  drop constraint FK_UserLogin_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ParentToManager2Child_Parent]') AND parent_object_id = OBJECT_ID('AppointmentManager2ParentToManager2Child'))
alter table AppointmentManager2ParentToManager2Child  drop constraint FK_AppointmentManager2ParentToManager2Child_Parent

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ParentToManager2Child_Child]') AND parent_object_id = OBJECT_ID('AppointmentManager2ParentToManager2Child'))
alter table AppointmentManager2ParentToManager2Child  drop constraint FK_AppointmentManager2ParentToManager2Child_Child

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportCost_MissionReport]') AND parent_object_id = OBJECT_ID('MissionReportCost'))
alter table MissionReportCost  drop constraint FK_MissionReportCost_MissionReport

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportCost_MissionReportCostType]') AND parent_object_id = OBJECT_ID('MissionReportCost'))
alter table MissionReportCost  drop constraint FK_MissionReportCost_MissionReportCostType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionAirTicketTypeGradeValue_AirTicketType]') AND parent_object_id = OBJECT_ID('MissionAirTicketTypeGradeValue'))
alter table MissionAirTicketTypeGradeValue  drop constraint FK_MissionAirTicketTypeGradeValue_AirTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionAirTicketTypeGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionAirTicketTypeGradeValue'))
alter table MissionAirTicketTypeGradeValue  drop constraint FK_MissionAirTicketTypeGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ATTACHMENT_USER_ROLE]') AND parent_object_id = OBJECT_ID('RequestAttachment'))
alter table RequestAttachment  drop constraint FK_ATTACHMENT_USER_ROLE

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager23ToDepartment3_User]') AND parent_object_id = OBJECT_ID('AppointmentManager23ToDepartment3'))
alter table AppointmentManager23ToDepartment3  drop constraint FK_AppointmentManager23ToDepartment3_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager23ToDepartment3_Department]') AND parent_object_id = OBJECT_ID('AppointmentManager23ToDepartment3'))
alter table AppointmentManager23ToDepartment3  drop constraint FK_AppointmentManager23ToDepartment3_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_HolidayWorkType]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_HolidayWorkType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_User]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_CreatorUser]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HolidayWork_TimesheetStatus]') AND parent_object_id = OBJECT_ID('HolidayWork'))
alter table HolidayWork  drop constraint FK_HolidayWork_TimesheetStatus

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Attachment_Document]') AND parent_object_id = OBJECT_ID('Attachment'))
alter table Attachment  drop constraint FK_Attachment_Document

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_EmployeeDocumentSubType]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_EmployeeDocumentSubType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Document_User]') AND parent_object_id = OBJECT_ID('Document'))
alter table Document  drop constraint FK_Document_User

if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentEducationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentEducationType
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentReport
if exists (select * from dbo.sysobjects where id = object_id(N'Account') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Account
if exists (select * from dbo.sysobjects where id = object_id(N'MissionResidenceGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionResidenceGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTrainTicketType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTrainTicketType
if exists (select * from dbo.sysobjects where id = object_id(N'TerraPoint') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TerraPoint
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingDaysConstant') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingDaysConstant
if exists (select * from dbo.sysobjects where id = object_id(N'Dismissal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Dismissal
if exists (select * from dbo.sysobjects where id = object_id(N'AbsenceComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AbsenceComment
if exists (select * from dbo.sysobjects where id = object_id(N'VacationComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationComment
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetStatus
if exists (select * from dbo.sysobjects where id = object_id(N'Mission') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Mission
if exists (select * from dbo.sysobjects where id = object_id(N'Sicklist') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Sicklist
if exists (select * from dbo.sysobjects where id = object_id(N'RequestNextNumber') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestNextNumber
if exists (select * from dbo.sysobjects where id = object_id(N'Information') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Information
if exists (select * from dbo.sysobjects where id = object_id(N'Settings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Settings
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistPaymentPercent') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistPaymentPercent
if exists (select * from dbo.sysobjects where id = object_id(N'DeductionKind') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DeductionKind
if exists (select * from dbo.sysobjects where id = object_id(N'AcceptRequestDate') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AcceptRequestDate
if exists (select * from dbo.sysobjects where id = object_id(N'RequestPrintForm') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestPrintForm
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentHoursType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentHoursType
if exists (select * from dbo.sysobjects where id = object_id(N'Department') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Department
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentSubType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentSubType
if exists (select * from dbo.sysobjects where id = object_id(N'AccountingTransaction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AccountingTransaction
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportComment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionOrderComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionOrderComment
if exists (select * from dbo.sysobjects where id = object_id(N'ChildVacationComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ChildVacationComment
if exists (select * from dbo.sysobjects where id = object_id(N'Contractor') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Contractor
if exists (select * from dbo.sysobjects where id = object_id(N'Deduction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Deduction
if exists (select * from dbo.sysobjects where id = object_id(N'Appointment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Appointment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportCostType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportCostType
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrectionComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrectionComment
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistPaymentRestrictType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistPaymentRestrictType
if exists (select * from dbo.sysobjects where id = object_id(N'MissionPurchaseBookRecord') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionPurchaseBookRecord
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReport
if exists (select * from dbo.sysobjects where id = object_id(N'MissionGoal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionGoal
if exists (select * from dbo.sysobjects where id = object_id(N'ChiefToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ChiefToUser
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphic') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingGraphic
if exists (select * from dbo.sysobjects where id = object_id(N'DismissalComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DismissalComment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionComment
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistType
if exists (select * from dbo.sysobjects where id = object_id(N'RequestStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestStatus
if exists (select * from dbo.sysobjects where id = object_id(N'MissionOrder') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionOrder
if exists (select * from dbo.sysobjects where id = object_id(N'MissionDailyAllowance') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionDailyAllowance
if exists (select * from dbo.sysobjects where id = object_id(N'TerraGraphic') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TerraGraphic
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentComment
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWorkType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWorkType
if exists (select * from dbo.sysobjects where id = object_id(N'DBVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DBVersion
if exists (select * from dbo.sysobjects where id = object_id(N'MissionPurchaseBookDocument') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionPurchaseBookDocument
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTarget') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTarget
if exists (select * from dbo.sysobjects where id = object_id(N'TerraPointToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TerraPointToUser
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphicType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingGraphicType
if exists (select * from dbo.sysobjects where id = object_id(N'InspectorToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table InspectorToUser
if exists (select * from dbo.sysobjects where id = object_id(N'DocumentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DocumentComment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionDailyAllowanceGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionDailyAllowanceGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrection') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrection
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentAddition') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentAddition
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistComment
if exists (select * from dbo.sysobjects where id = object_id(N'AbsenceType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AbsenceType
if exists (select * from dbo.sysobjects where id = object_id(N'[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Users]
if exists (select * from dbo.sysobjects where id = object_id(N'UserToPersonnel') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserToPersonnel
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager2ToManager3') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentManager2ToManager3
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTrainTicketTypeGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTrainTicketTypeGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionCountry') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionCountry
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphicTypeToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingGraphicTypeToUser
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWorkComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWorkComment
if exists (select * from dbo.sysobjects where id = object_id(N'Absence') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Absence
if exists (select * from dbo.sysobjects where id = object_id(N'VacationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationType
if exists (select * from dbo.sysobjects where id = object_id(N'ExportImportAction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ExportImportAction
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentComment
if exists (select * from dbo.sysobjects where id = object_id(N'[MissionOrderRoleRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [MissionOrderRoleRecord]
if exists (select * from dbo.sysobjects where id = object_id(N'[ClearanceChecklistRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [ClearanceChecklistRole]
if exists (select * from dbo.sysobjects where id = object_id(N'ChildVacation') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ChildVacation
if exists (select * from dbo.sysobjects where id = object_id(N'Employment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Employment
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentType
if exists (select * from dbo.sysobjects where id = object_id(N'MissionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionType
if exists (select * from dbo.sysobjects where id = object_id(N'Position') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Position
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetDay') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetDay
if exists (select * from dbo.sysobjects where id = object_id(N'[ClearanceChecklistRoleRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [ClearanceChecklistRoleRecord]
if exists (select * from dbo.sysobjects where id = object_id(N'ClearanceChecklistApproval') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ClearanceChecklistApproval
if exists (select * from dbo.sysobjects where id = object_id(N'MissionGraid') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionGraid
if exists (select * from dbo.sysobjects where id = object_id(N'MissionResidence') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionResidence
if exists (select * from dbo.sysobjects where id = object_id(N'Role') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Role
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentCreateManager2ToDepartment2') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentCreateManager2ToDepartment2
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingCalendar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingCalendar
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrectionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrectionType
if exists (select * from dbo.sysobjects where id = object_id(N'Vacation') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Vacation
if exists (select * from dbo.sysobjects where id = object_id(N'Timesheet') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Timesheet
if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserLogin
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReason') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentReason
if exists (select * from dbo.sysobjects where id = object_id(N'[MissionOrderRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [MissionOrderRole]
if exists (select * from dbo.sysobjects where id = object_id(N'Organization') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Organization
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager2ParentToManager2Child') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentManager2ParentToManager2Child
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportCost') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportCost
if exists (select * from dbo.sysobjects where id = object_id(N'MissionAirTicketTypeGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionAirTicketTypeGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionAirTicketType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionAirTicketType
if exists (select * from dbo.sysobjects where id = object_id(N'DeductionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DeductionType
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistBabyMindingType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistBabyMindingType
if exists (select * from dbo.sysobjects where id = object_id(N'DismissalType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DismissalType
if exists (select * from dbo.sysobjects where id = object_id(N'RequestAttachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestAttachment
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentType
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager23ToDepartment3') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentManager23ToDepartment3
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWork') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWork
if exists (select * from dbo.sysobjects where id = object_id(N'Attachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Attachment
if exists (select * from dbo.sysobjects where id = object_id(N'Document') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Document

create table AppointmentEducationType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_AppointmentEducationType  primary key (Id)
)
create table AppointmentReport (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  Number INT not null,
  AppointmentId INT not null,
  Name NVARCHAR(64) not null,
  Phone NVARCHAR(32) not null,
  Email NVARCHAR(32) not null,
  ColloquyDate DATETIME null,
  TypeId INT not null,
  EducationTime NVARCHAR(32) null,
  TempLogin NVARCHAR(32) null,
  TempPassword NVARCHAR(32) null,
  RejectReason NVARCHAR(128) null,
  IsEducationExists BIT null,
  DateAccept DATETIME null,
  CreatorId INT not null,
  StaffDateAccept DATETIME null,
  AcceptStaffId INT null,
  ManagerDateAccept DATETIME null,
  AcceptManagerId INT null,
  DeleteDate DATETIME null,
  DeleteUserId INT null,
  SendTo1C DATETIME null,
  constraint PK_AppointmentReport  primary key (Id)
)
create table Account (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Number NVARCHAR(128) not null,
  IsDebitAccount BIT not null,
  constraint PK_Account  primary key (Id)
)
create table MissionResidenceGradeValue (
 Id INT IDENTITY NOT NULL,
  ResidenceId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionResidenceGradeValue  primary key (Id)
)
create table MissionTrainTicketType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionTrainTicketType  primary key (Id)
)
create table TerraPoint (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  ShortName NVARCHAR(128) null,
  Code1C NVARCHAR(128) null,
  ParentId NVARCHAR(128) null,
  Path NVARCHAR(128) null,
  ItemLevel INT null,
  EndDate DATETIME null,
  constraint PK_TerraPoint  primary key (Id)
)
create table WorkingDaysConstant (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Month DATETIME not null,
  Days INT not null,
  Hours INT not null,
  constraint PK_WorkingDaysConstant  primary key (Id)
)
create table Dismissal (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EndDate DATETIME not null,
  Number INT not null,
  TypeId INT null,
  Compensation DECIMAL(19, 2) null,
  Reduction DECIMAL(19, 2) null,
  Reason NVARCHAR(256) null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  TimesheetStatusId INT null,
  RegistryNumber INT null,
  PersonalIncomeTax DECIMAL(19, 2) null,
  OKTMO NVARCHAR(8) null,
  constraint PK_Dismissal  primary key (Id)
)
create table AbsenceComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  AbsenceId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_AbsenceComment  primary key (Id)
)
create table VacationComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  VacationId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_VacationComment  primary key (Id)
)
create table TimesheetStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  ShortName NVARCHAR(2) not null,
  Name NVARCHAR(255) not null,
  constraint PK_TimesheetStatus  primary key (Id)
)
create table Mission (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  Country NVARCHAR(255) not null,
  Organization NVARCHAR(255) not null,
  Goal NVARCHAR(255) not null,
  FinancesSource NVARCHAR(255) not null,
  Reason NVARCHAR(255) null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  TimesheetStatusId INT null,
  constraint PK_Mission  primary key (Id)
)
create table Sicklist (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  SicklistNumber NVARCHAR(255) not null,
  TypeId INT null,
  BabyMindingTypeId INT null,
  PaymentPercentId INT null,
  PaymentRestrictTypeId INT null,
  PaymentBeginDate DATETIME null,
  ExperienceYears INT null,
  ExperienceMonthes INT null,
  PaymentDecreaseDate DATETIME null,
  IsPreviousPaymentCounted BIT not null,
  Is2010Calculate BIT not null,
  IsAddToFullPayment BIT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ApprovedByManagerId INT null,
  ManagerDateAccept DATETIME null,
  ApprovedByPersonnelManagerId INT null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  IsContinued BIT not null,
  IsOriginalReceived BIT not null,
  TimesheetStatusId INT null,
  constraint PK_Sicklist  primary key (Id)
)
create table RequestNextNumber (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  RequestTypeId INT not null,
  NextNumber INT not null,
  constraint PK_RequestNextNumber  primary key (Id)
)
create table Information (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Subject NVARCHAR(1024) not null,
  Message NVARCHAR(MAX) not null,
  constraint PK_Information  primary key (Id)
)
create table Settings (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  DownloadDictionaryFilePath NVARCHAR(512) not null,
  UploadTimesheetFilePath NVARCHAR(512) not null,
  ReleaseEmployeeDeleteDate DATETIME not null,
  ExportImportEmail NVARCHAR(128) not null,
  SendEmailToManagerAboutNew BIT not null,
  NotificationEmail NVARCHAR(128) not null,
  NotificationSmtp NVARCHAR(128) not null,
  NotificationPort INT not null,
  NotificationLogin NVARCHAR(32) not null,
  NotificationPassword NVARCHAR(32) not null,
  BillingEmail NVARCHAR(128) not null,
  BillingSmtp NVARCHAR(128) not null,
  BillingPort INT not null,
  BillingLogin NVARCHAR(32) not null,
  BillingPassword NVARCHAR(32) not null,
  constraint PK_Settings  primary key (Id)
)
create table SicklistPaymentPercent (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  SicklistPercent INT null,
  SortOrder INT null,
  constraint PK_SicklistPaymentPercent  primary key (Id)
)
create table DeductionKind (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  CalculationStyle NVARCHAR(128) not null,
  constraint PK_DeductionKind  primary key (Id)
)
create table AcceptRequestDate (
 Id INT IDENTITY NOT NULL,
  DateAccept DATETIME null,
  DateCreate DATETIME null,
  UserId INT not null,
  constraint PK_AcceptRequestDate  primary key (Id)
)
create table RequestPrintForm (
 Id INT IDENTITY NOT NULL,
  Context VARBINARY(MAX) not null,
  RequestId INT not null,
  RequestTypeId INT not null,
  constraint PK_RequestPrintForm  primary key (Id)
)
create table EmploymentHoursType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  constraint PK_EmploymentHoursType  primary key (Id)
)
create table Department (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  Code1C INT null,
  ParentId INT null,
  Path NVARCHAR(128) null,
  ItemLevel INT null,
  constraint PK_Department  primary key (Id)
)
create table EmployeeDocumentSubType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  TypeId INT not null,
  constraint PK_EmployeeDocumentSubType  primary key (Id)
)
create table AccountingTransaction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CostId INT not null,
  DebitAccountId INT not null,
  CreditAccountId INT not null,
  Sum DECIMAL(19,5) not null,
  constraint PK_AccountingTransaction  primary key (Id)
)
create table MissionReportComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  MissionReportId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_MissionReportComment  primary key (Id)
)
create table MissionOrderComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  MissionOrderId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_MissionOrderComment  primary key (Id)
)
create table ChildVacationComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  ChildVacationId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_ChildVacationComment  primary key (Id)
)
create table Contractor (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(256) not null,
  Account NVARCHAR(32) not null,
  constraint PK_Contractor  primary key (Id)
)
create table Deduction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  EditDate DATETIME not null,
  DeductionDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  KindId INT not null,
  Sum DECIMAL(19, 2) not null,
  DismissalDate DATETIME null,
  IsFastDismissal BIT null,
  UserId INT not null,
  CreatorId INT not null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  EmailSendToUserDate DATETIME null,
  constraint PK_Deduction  primary key (Id)
)
create table Appointment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  Number INT not null,
  DepartmentId INT not null,
  City NVARCHAR(64) not null,
  PositionName NVARCHAR(64) not null,
  VacationCount INT not null,
  ReasonId INT not null,
  ReasonBeginDate DATETIME null,
  ReasonPosition NVARCHAR(64) null,
  Schedule NVARCHAR(64) not null,
  Salary DECIMAL(19,5) not null,
  Bonus DECIMAL(19,5) not null,
  Type BIT not null,
  Compensation NVARCHAR(128) not null,
  EducationRequirements NVARCHAR(64) not null,
  ExperienceRequirements NVARCHAR(32) not null,
  OtherRequirements NVARCHAR(128) null,
  Responsibility NVARCHAR(128) not null,
  DesirableBeginDate DATETIME null,
  IsVacationExists BIT not null,
  CreatorId INT not null,
  ManagerDateAccept DATETIME null,
  AcceptManagerId INT null,
  ChiefDateAccept DATETIME null,
  AcceptChiefId INT null,
  StaffDateAccept DATETIME null,
  AcceptStaffId INT null,
  DeleteDate DATETIME null,
  DeleteUserId INT null,
  SendTo1C DATETIME null,
  constraint PK_Appointment  primary key (Id)
)
create table MissionReportCostType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_MissionReportCostType  primary key (Id)
)
create table TimesheetCorrectionComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  TimesheetCorrectionId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_TimesheetCorrectionComment  primary key (Id)
)
create table SicklistPaymentRestrictType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) null,
  constraint PK_SicklistPaymentRestrictType  primary key (Id)
)
create table MissionPurchaseBookRecord (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  EditDate DATETIME not null,
  MissionPurchaseBookDocumentId INT not null,
  MissionOrderId INT not null,
  MissionReportCostTypeId INT not null,
  MissionReportCostId INT not null,
  Sum DECIMAL(19,5) not null,
  SumNds DECIMAL(19,5) not null,
  AllSum DECIMAL(19,5) not null,
  RequestNumber NVARCHAR(16) not null,
  UserId INT not null,
  EditorId INT not null,
  constraint PK_MissionPurchaseBookRecord  primary key (Id)
)
create table MissionReport (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  Number INT not null,
  Hotels NVARCHAR(1024) null,
  AllSum DECIMAL(19,5) not null,
  UserAllSum DECIMAL(19,5) not null,
  UserSumReceived DECIMAL(19,5) not null,
  AccountantAllSum DECIMAL(19,5) not null,
  PurchaseBookAllSum DECIMAL(19,5) not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  AcceptUserId INT null,
  ManagerDateAccept DATETIME null,
  AcceptManagerId INT null,
  AccountantDateAccept DATETIME null,
  AcceptAccountant INT null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  MissionOrderId INT null,
  constraint PK_MissionReport  primary key (Id)
)
create table MissionGoal (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionGoal  primary key (Id)
)
create table ChiefToUser (
 Id INT IDENTITY NOT NULL,
  ChiefId INT not null,
  UserId INT not null,
  constraint PK_ChiefToUser  primary key (Id)
)
create table WorkingGraphic (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  Day DATETIME not null,
  Hours REAL null,
  constraint PK_WorkingGraphic  primary key (Id)
)
create table DismissalComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  DismissalId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_DismissalComment  primary key (Id)
)
create table MissionComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  MissionId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_MissionComment  primary key (Id)
)
create table SicklistType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) null,
  constraint PK_SicklistType  primary key (Id)
)
create table RequestStatus (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) null,
  constraint PK_RequestStatus  primary key (Id)
)
create table MissionOrder (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  Number INT not null,
  TypeId INT null,
  Kind INT not null,
  MissionGoalId INT null,
  AllSum DECIMAL(19,5) not null,
  SumDaily DECIMAL(19,5) null,
  SumResidence DECIMAL(19,5) null,
  SumAir DECIMAL(19,5) null,
  SumTrain DECIMAL(19,5) null,
  UserSumDaily DECIMAL(19,5) null,
  UserSumResidence DECIMAL(19,5) null,
  UserSumAir DECIMAL(19,5) null,
  UserSumTrain DECIMAL(19,5) null,
  UserAllSum DECIMAL(19,5) not null,
  UserSumCash DECIMAL(19,5) null,
  UserSumNotCash DECIMAL(19,5) null,
  NeedToAcceptByChief BIT not null,
  NeedToAcceptByChiefAsManager BIT not null,
  IsResidencePaid BIT not null,
  IsAirTicketsPaid BIT not null,
  IsTrainTicketsPaid BIT not null,
  ResidenceRequestNumber NVARCHAR(16) null,
  AirTicketsRequestNumber NVARCHAR(16) null,
  TrainTicketsRequestNumber NVARCHAR(16) null,
  SecretaryId INT null,
  AirTicketType INT not null,
  TrainTicketType INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  AcceptUserId INT null,
  ManagerDateAccept DATETIME null,
  AcceptManagerId INT null,
  ChiefDateAccept DATETIME null,
  AcceptChiefId INT null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  MissionId INT null,
  constraint PK_MissionOrder  primary key (Id)
)
create table MissionDailyAllowance (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionDailyAllowance  primary key (Id)
)
create table TerraGraphic (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  Day DATETIME not null,
  Hours INT not null,
  PointId INT not null,
  IsCreditAvailable BIT null,
  constraint PK_TerraGraphic  primary key (Id)
)
create table EmploymentComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  EmploymentId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_EmploymentComment  primary key (Id)
)
create table HolidayWorkType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_HolidayWorkType  primary key (Id)
)
create table DBVersion (
 Id INT IDENTITY NOT NULL,
  Version NVARCHAR(20) not null,
  constraint PK_DBVersion  primary key (Id)
)
create table MissionPurchaseBookDocument (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  DocumentDate DATETIME null,
  Number NVARCHAR(255) null,
  CfDate DATETIME null,
  CfNumber NVARCHAR(255) null,
  ContractorId INT null,
  Sum DECIMAL(19,5) not null,
  EditorId INT not null,
  constraint PK_MissionPurchaseBookDocument  primary key (Id)
)
create table MissionTarget (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  MissionOrderId INT not null,
  CountryId INT not null,
  City NVARCHAR(255) not null,
  Organization NVARCHAR(32) not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  RealDaysCount INT not null,
  DailyAllowanceId INT null,
  ResidenceId INT null,
  AirTicketTypeId INT null,
  TrainTicketTypeId INT null,
  constraint PK_MissionTarget  primary key (Id)
)
create table TerraPointToUser (
 Id INT IDENTITY NOT NULL,
  TerraPointId INT not null,
  UserId INT not null unique,
  constraint PK_TerraPointToUser  primary key (Id)
)
create table WorkingGraphicType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) null,
  FillDays BIT null,
  constraint PK_WorkingGraphicType  primary key (Id)
)
create table InspectorToUser (
 Id INT IDENTITY NOT NULL,
  InspectorId INT not null,
  UserId INT not null,
  constraint PK_InspectorToUser  primary key (Id)
)
create table DocumentComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  DocumentId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_DocumentComment  primary key (Id)
)
create table MissionDailyAllowanceGradeValue (
 Id INT IDENTITY NOT NULL,
  DailyAllowanceId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionDailyAllowanceGradeValue  primary key (Id)
)
create table TimesheetCorrection (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EventDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  Hours INT null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_TimesheetCorrection  primary key (Id)
)
create table EmploymentAddition (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  CalculationMethod NVARCHAR(255) null,
  constraint PK_EmploymentAddition  primary key (Id)
)
create table SicklistComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  SicklistId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_SicklistComment  primary key (Id)
)
create table AbsenceType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) null,
  constraint PK_AbsenceType  primary key (Id)
)
create table [Users] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IsFirstTimeLogin BIT not null,
  IsActive BIT not null,
  IsNew BIT not null,
  Login NVARCHAR(64) not null unique,
  Password NVARCHAR(64) null,
  Code NVARCHAR(32) null,
  DateAccept DATETIME null,
  Name NVARCHAR(512) not null,
  Email NVARCHAR(512) null,
  DateRelease DATETIME null,
  Comment NVARCHAR(512) null,
  Cnilc NVARCHAR(512) null,
  RoleId INT not null,
  LoginAd NVARCHAR(32) null,
  Rate DECIMAL(19,5) null,
  GivesCredit BIT not null,
  Level INT null,
  Grade INT null,
  ExperienceIn1C BIT null,
  IsMainManager BIT not null,
  ManagerId INT null,
  OrganizationId INT null,
  PositionId INT null,
  DepartmentId INT null,
  constraint PK_Users primary key (Id)
)
create table UserToPersonnel (
 UserId INT not null,
  PersonnelId INT not null
)
create table AppointmentManager2ToManager3 (
 Id INT IDENTITY NOT NULL,
  Manager2Id INT not null,
  Manager3Id INT not null,
  constraint PK_AppointmentManager2ToManager3  primary key (Id)
)
create table MissionTrainTicketTypeGradeValue (
 Id INT IDENTITY NOT NULL,
  TrainTicketTypeId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionTrainTicketTypeGradeValue  primary key (Id)
)
create table MissionCountry (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionCountry  primary key (Id)
)
create table WorkingGraphicTypeToUser (
 Id INT IDENTITY NOT NULL,
  WorkingGraphicTypeId INT not null,
  UserId INT not null,
  constraint PK_WorkingGraphicTypeToUser  primary key (Id)
)
create table HolidayWorkComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  HolidayWorkId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_HolidayWorkComment  primary key (Id)
)
create table Absence (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  TimesheetStatusId INT null,
  constraint PK_Absence  primary key (Id)
)
create table VacationType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  constraint PK_VacationType  primary key (Id)
)
create table ExportImportAction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Type INT not null,
  Date DATETIME not null,
  Month DATETIME null,
  constraint PK_ExportImportAction  primary key (Id)
)
create table AppointmentComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  AppointmentId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_AppointmentComment  primary key (Id)
)
create table [MissionOrderRoleRecord] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  RoleId INT not null,
  TargetUserId INT null,
  TargetDepartmentId INT null,
  constraint PK_MissionOrderRoleRecord primary key (Id)
)
create table [ClearanceChecklistRole] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(32) null,
  Description NVARCHAR(256) null,
  DaysForApproval INT null,
  constraint PK_ClearanceChecklistRole primary key (Id)
)
create table ChildVacation (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  FreeRate BIT not null,
  PaidToDate DATETIME null,
  IsPreviousPaymentCounted BIT not null,
  ChildrenCount INT null,
  IsFirstChild BIT not null,
  PaidToDate1 DATETIME null,
  Number INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  ExportFrom1C BIT null,
  IsOriginalReceived BIT not null,
  TimesheetStatusId INT null,
  constraint PK_ChildVacation  primary key (Id)
)
create table Employment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  HoursTypeId INT not null,
  AdditionId INT null,
  PositionId INT not null,
  Salary DECIMAL(19, 2) not null,
  Probaion INT null,
  Reason NVARCHAR(256) null,
  RegionFactor DECIMAL(19, 2) null,
  NorthFactor DECIMAL(19, 2) null,
  RegionAddition INT null,
  PersonalAddition INT null,
  TravelWorkAddition INT null,
  SkillAddition INT null,
  LongWorkAddition INT null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_Employment  primary key (Id)
)
create table EmploymentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  constraint PK_EmploymentType  primary key (Id)
)
create table MissionType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) null,
  constraint PK_MissionType  primary key (Id)
)
create table Position (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Position  primary key (Id)
)
create table TimesheetDay (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Number INT not null,
  Hours REAL not null,
  StatusId INT not null,
  TimesheetId INT not null,
  constraint PK_TimesheetDay  primary key (Id)
)
create table [ClearanceChecklistRoleRecord] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  RoleId INT not null,
  TargetUserId INT null,
  TargetDepartmentId INT null,
  constraint PK_ClearanceChecklistRoleRecord primary key (Id)
)
create table ClearanceChecklistApproval (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  DismissalId INT not null,
  RoleId INT not null,
  ApprovedById INT null,
  ApprovalDate DATETIME null,
  Comment NVARCHAR(255) null,
  constraint PK_ClearanceChecklistApproval  primary key (Id)
)
create table MissionGraid (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionGraid  primary key (Id)
)
create table MissionResidence (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionResidence  primary key (Id)
)
create table Role (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_Role  primary key (Id)
)
create table AppointmentCreateManager2ToDepartment2 (
 Id INT IDENTITY NOT NULL,
  ManagerId INT not null,
  DepartmentId INT not null,
  constraint PK_AppointmentCreateManager2ToDepartment2  primary key (Id)
)
create table WorkingCalendar (
 Id INT IDENTITY NOT NULL,
  Date DATETIME null,
  IsWorkingHours INT not null,
  constraint PK_WorkingCalendar  primary key (Id)
)
create table TimesheetCorrectionType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  Reason NVARCHAR(512) not null,
  constraint PK_TimesheetCorrectionType  primary key (Id)
)
create table Vacation (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  IsOriginalReceived BIT not null,
  TimesheetStatusId INT null,
  constraint PK_Vacation  primary key (Id)
)
create table Timesheet (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Month DATETIME not null,
  UserNotAcceptDate DATETIME null,
  PersonnelNotAcceptDate DATETIME null,
  UserId INT not null,
  constraint PK_Timesheet  primary key (Id)
)
create table UserLogin (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT null,
  Date DATETIME not null,
  RoleId INT null,
  constraint PK_UserLogin  primary key (Id)
)
create table AppointmentReason (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_AppointmentReason  primary key (Id)
)
create table [MissionOrderRole] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(32) null,
  Description NVARCHAR(256) null,
  DaysForApproval INT null,
  constraint PK_MissionOrderRole primary key (Id)
)
create table Organization (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(256) null,
  constraint PK_Organization  primary key (Id)
)
create table AppointmentManager2ParentToManager2Child (
 Id INT IDENTITY NOT NULL,
  ParentId INT not null,
  ChildId INT not null,
  constraint PK_AppointmentManager2ParentToManager2Child  primary key (Id)
)
create table MissionReportCost (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  ReportId INT not null,
  TypeId INT not null,
  Sum DECIMAL(19,5) null,
  UserSum DECIMAL(19,5) null,
  BookOfPurchaseSum DECIMAL(19,5) null,
  AccountantSum DECIMAL(19,5) null,
  IsCostFromOrder BIT not null,
  IsCostFromPurchaseBook BIT not null,
  constraint PK_MissionReportCost  primary key (Id)
)
create table MissionAirTicketTypeGradeValue (
 Id INT IDENTITY NOT NULL,
  AirTicketTypeId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionAirTicketTypeGradeValue  primary key (Id)
)
create table MissionAirTicketType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionAirTicketType  primary key (Id)
)
create table DeductionType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) not null,
  constraint PK_DeductionType  primary key (Id)
)
create table SicklistBabyMindingType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) null,
  constraint PK_SicklistBabyMindingType  primary key (Id)
)
create table DismissalType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  Reason NVARCHAR(512) not null,
  constraint PK_DismissalType  primary key (Id)
)
create table RequestAttachment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  FileName NVARCHAR(64) not null,
  ContextType NVARCHAR(64) not null,
  Context VARBINARY(MAX) not null,
  RequestId INT not null,
  RequestType INT not null,
  DateCreated DATETIME not null,
  Description NVARCHAR(256) null,
  CreatorRoleId INT not null,
  constraint PK_RequestAttachment  primary key (Id)
)
create table EmployeeDocumentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_EmployeeDocumentType  primary key (Id)
)
create table AppointmentManager23ToDepartment3 (
 Id INT IDENTITY NOT NULL,
  ManagerId INT not null,
  DepartmentId INT not null,
  constraint PK_AppointmentManager23ToDepartment3  primary key (Id)
)
create table HolidayWork (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  WorkDate DATETIME null,
  Hours INT not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  TimesheetStatusId INT null,
  constraint PK_HolidayWork  primary key (Id)
)
create table Attachment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  FileName NVARCHAR(64) not null,
  ContextType NVARCHAR(64) not null,
  Context VARBINARY(MAX) not null,
  DocumentId INT not null,
  constraint PK_Attachment  primary key (Id)
)
create table Document (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LastModifiedDate DATETIME not null,
  Comment NVARCHAR(MAX) null,
  TypeId INT not null,
  SubTypeId INT null,
  UserId INT not null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  BudgetManagerDateAccept DATETIME null,
  OutsourcingManagerDateAccept DATETIME null,
  SendEmailToBilling BIT not null,
  constraint PK_Document  primary key (Id)
)
create index AppointmentReport_Appointment on AppointmentReport (AppointmentId)
create index AppointmentReport_AppointmentEducationType on AppointmentReport (TypeId)
create index IX_AppointmentReport_CreatorUser_Id on AppointmentReport (CreatorId)
create index IX_AppointmentReport_StaffUser on AppointmentReport (AcceptStaffId)
create index IX_AppointmentReport_AcceptManager on AppointmentReport (AcceptManagerId)
create index IX_AppointmentReport_DeleteUser on AppointmentReport (DeleteUserId)
alter table AppointmentReport add constraint FK_AppointmentReport_Appointment foreign key (AppointmentId) references Appointment
alter table AppointmentReport add constraint FK_AppointmentReport_AppointmentEducationType foreign key (TypeId) references AppointmentEducationType
alter table AppointmentReport add constraint FK_AppointmentReport_CreatorUser foreign key (CreatorId) references [Users]
alter table AppointmentReport add constraint FK_AppointmentReport_StaffUser foreign key (AcceptStaffId) references [Users]
alter table AppointmentReport add constraint FK_AppointmentReport_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table AppointmentReport add constraint FK_AppointmentReport_DeleteUser foreign key (DeleteUserId) references [Users]
create index IX_MissionResidenceGradeValue_Residence_Id on MissionResidenceGradeValue (ResidenceId)
create index IX_MissionResidenceGradeValue_Grade_Id on MissionResidenceGradeValue (GradeId)
alter table MissionResidenceGradeValue add constraint FK_MissionResidenceGradeValue_Residence foreign key (ResidenceId) references MissionResidence
alter table MissionResidenceGradeValue add constraint FK_MissionResidenceGradeValue_Grade foreign key (GradeId) references MissionGraid
create index Dismissal_DismissalType on Dismissal (TypeId)
create index IX_Dismissal_User_Id on Dismissal (UserId)
create index IX_Dismissal_CreatorUser_Id on Dismissal (CreatorId)
create index Dismissal_TimesheetStatus on Dismissal (TimesheetStatusId)
alter table Dismissal add constraint FK_Dismissal_DismissalType foreign key (TypeId) references DismissalType
alter table Dismissal add constraint FK_Dismissal_User foreign key (UserId) references [Users]
alter table Dismissal add constraint FK_Dismissal_CreatorUser foreign key (CreatorId) references [Users]
alter table Dismissal add constraint FK_Dismissal_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_AbsenceComment_User_Id on AbsenceComment (UserId)
create index IX_AbsenceComment_Absence_Id on AbsenceComment (AbsenceId)
alter table AbsenceComment add constraint FK_AbsenceComment_User foreign key (UserId) references [Users]
alter table AbsenceComment add constraint FK_AbsenceComment_Absence foreign key (AbsenceId) references Absence
create index IX_VacationComment_User_Id on VacationComment (UserId)
create index IX_VacationComment_Vacation_Id on VacationComment (VacationId)
alter table VacationComment add constraint FK_VacationComment_User foreign key (UserId) references [Users]
alter table VacationComment add constraint FK_VacationComment_Vacation foreign key (VacationId) references Vacation
create index Mission_MissionType on Mission (TypeId)
create index IX_Mission_User_Id on Mission (UserId)
create index IX_Mission_CreatorUser_Id on Mission (CreatorId)
create index Mission_TimesheetStatus on Mission (TimesheetStatusId)
alter table Mission add constraint FK_Mission_MissionType foreign key (TypeId) references MissionType
alter table Mission add constraint FK_Mission_User foreign key (UserId) references [Users]
alter table Mission add constraint FK_Mission_CreatorUser foreign key (CreatorId) references [Users]
alter table Mission add constraint FK_Mission_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index Sicklist_SicklistType on Sicklist (TypeId)
create index Sicklist_BabyMindingType on Sicklist (BabyMindingTypeId)
create index Sicklist_SicklistPaymentPercent on Sicklist (PaymentPercentId)
create index Sicklist_SicklistPaymentRestrictType on Sicklist (PaymentRestrictTypeId)
create index IX_Sicklist_User_Id on Sicklist (UserId)
create index IX_Sicklist_CreatorUser_Id on Sicklist (CreatorId)
create index IX_Sicklist_ApprovedByManagerUser_Id on Sicklist (ApprovedByManagerId)
create index IX_Sicklist_ApprovedByPersonnelManagerUser_Id on Sicklist (ApprovedByPersonnelManagerId)
create index Sicklist_TimesheetStatus on Sicklist (TimesheetStatusId)
alter table Sicklist add constraint FK_Sicklist_SicklistType foreign key (TypeId) references SicklistType
alter table Sicklist add constraint FK_Sicklist_BabyMindingType foreign key (BabyMindingTypeId) references SicklistBabyMindingType
alter table Sicklist add constraint FK_Sicklist_SicklistPaymentPercent foreign key (PaymentPercentId) references SicklistPaymentPercent
alter table Sicklist add constraint FK_Sicklist_SicklistPaymentRestrictType foreign key (PaymentRestrictTypeId) references SicklistPaymentRestrictType
alter table Sicklist add constraint FK_Sicklist_User foreign key (UserId) references [Users]
alter table Sicklist add constraint FK_Sicklist_CreatorUser foreign key (CreatorId) references [Users]
alter table Sicklist add constraint FK_Sicklist_ApprovedByManagerUser foreign key (ApprovedByManagerId) references [Users]
alter table Sicklist add constraint FK_Sicklist_ApprovedByPersonnelManagerUser foreign key (ApprovedByPersonnelManagerId) references [Users]
alter table Sicklist add constraint FK_Sicklist_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_AcceptRequestDate_User_Id on AcceptRequestDate (UserId)
alter table AcceptRequestDate add constraint FK_AcceptRequestDate_User foreign key (UserId) references [Users]
create index IX_DocumentSubType_EmployeeDocumentType_Id on EmployeeDocumentSubType (TypeId)
alter table EmployeeDocumentSubType add constraint FK_DocumentSubType_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
create index AccountingTransaction_MissionReportCost on AccountingTransaction (CostId)
create index AccountingTransaction_DebitAccount on AccountingTransaction (DebitAccountId)
create index AccountingTransaction_CreditAccount on AccountingTransaction (CreditAccountId)
alter table AccountingTransaction add constraint FK_AccountingTransaction_MissionReportCost foreign key (CostId) references MissionReportCost
alter table AccountingTransaction add constraint FK_AccountingTransaction_DebitAccount foreign key (DebitAccountId) references Account
alter table AccountingTransaction add constraint FK_AccountingTransaction_CreditAccount foreign key (CreditAccountId) references Account
create index IX_MissionReportComment_User on MissionReportComment (UserId)
create index IX_MissionReportComment_MissionReport on MissionReportComment (MissionReportId)
alter table MissionReportComment add constraint FK_MissionReportComment_User foreign key (UserId) references [Users]
alter table MissionReportComment add constraint FK_MissionReportComment_MissionReport foreign key (MissionReportId) references MissionReport
create index IX_MissionOrderComment_User on MissionOrderComment (UserId)
create index IX_MissionOrderComment_MissionOrder on MissionOrderComment (MissionOrderId)
alter table MissionOrderComment add constraint FK_MissionOrderComment_User foreign key (UserId) references [Users]
alter table MissionOrderComment add constraint FK_MissionOrderComment_MissionOrder foreign key (MissionOrderId) references MissionOrder
create index IX_ChildVacationComment_User_Id on ChildVacationComment (UserId)
create index IX_ChildVacationComment_ChildVacation_Id on ChildVacationComment (ChildVacationId)
alter table ChildVacationComment add constraint FK_ChildVacationComment_User foreign key (UserId) references [Users]
alter table ChildVacationComment add constraint FK_ChildVacationComment_ChildVacation foreign key (ChildVacationId) references ChildVacation
create index Deduction_DeductionType on Deduction (TypeId)
create index Deduction_DeductionKind on Deduction (KindId)
create index IX_Deduction_User_Id on Deduction (UserId)
create index IX_Deduction_EditorUser_Id on Deduction (CreatorId)
alter table Deduction add constraint FK_Deduction_DeductionType foreign key (TypeId) references DeductionType
alter table Deduction add constraint FK_Deduction_DeductionKind foreign key (KindId) references DeductionKind
alter table Deduction add constraint FK_Deduction_User foreign key (UserId) references [Users]
alter table Deduction add constraint FK_Deduction_EditorUser foreign key (CreatorId) references [Users]
create index Appointment_Department on Appointment (DepartmentId)
create index Appointment_AppointmentReason on Appointment (ReasonId)
create index IX_Appointment_CreatorUser_Id on Appointment (CreatorId)
create index IX_Appointment_AcceptManager on Appointment (AcceptManagerId)
create index IX_Appointment_AcceptChief on Appointment (AcceptChiefId)
create index IX_Appointment_StaffUser on Appointment (AcceptStaffId)
create index IX_Appointment_DeleteUser on Appointment (DeleteUserId)
alter table Appointment add constraint FK_Appointment_Department foreign key (DepartmentId) references Department
alter table Appointment add constraint FK_Appointment_AppointmentReason foreign key (ReasonId) references AppointmentReason
alter table Appointment add constraint FK_Appointment_CreatorUser foreign key (CreatorId) references [Users]
alter table Appointment add constraint FK_Appointment_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table Appointment add constraint FK_Appointment_AcceptChief foreign key (AcceptChiefId) references [Users]
alter table Appointment add constraint FK_Appointment_StaffUser foreign key (AcceptStaffId) references [Users]
alter table Appointment add constraint FK_Appointment_DeleteUser foreign key (DeleteUserId) references [Users]
create index IX_TimesheetCorrectionComment_User_Id on TimesheetCorrectionComment (UserId)
create index IX_TimesheetCorrectionComment_TimesheetCorrection_Id on TimesheetCorrectionComment (TimesheetCorrectionId)
alter table TimesheetCorrectionComment add constraint FK_TimesheetCorrectionComment_User foreign key (UserId) references [Users]
alter table TimesheetCorrectionComment add constraint FK_TimesheetCorrectionComment_TimesheetCorrection foreign key (TimesheetCorrectionId) references TimesheetCorrection
create index MissionPurchaseBookRecord_MissionPurchaseBookDocument on MissionPurchaseBookRecord (MissionPurchaseBookDocumentId)
create index MissionPurchaseBookRecord_MissionOrder on MissionPurchaseBookRecord (MissionOrderId)
create index MissionPurchaseBookRecord_MissionReportCostType on MissionPurchaseBookRecord (MissionReportCostTypeId)
create index MissionPurchaseBookRecord_MissionReportCost on MissionPurchaseBookRecord (MissionReportCostId)
create index IX_MissionPurchaseBookRecord_User on MissionPurchaseBookRecord (UserId)
create index IX_MissionPurchaseBookRecord_EditorUser on MissionPurchaseBookRecord (EditorId)
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionPurchaseBookDocument foreign key (MissionPurchaseBookDocumentId) references MissionPurchaseBookDocument
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionOrder foreign key (MissionOrderId) references MissionOrder
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionReportCostType foreign key (MissionReportCostTypeId) references MissionReportCostType
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionReportCost foreign key (MissionReportCostId) references MissionReportCost
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_User foreign key (UserId) references [Users]
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_EditorUser foreign key (EditorId) references [Users]
create index IX_MissionReport_User_Id on MissionReport (UserId)
create index IX_MissionReport_CreatorUser_Id on MissionReport (CreatorId)
create index IX_MissionReport_AcceptUser on MissionReport (AcceptUserId)
create index IX_MissionReport_AcceptManager on MissionReport (AcceptManagerId)
create index IX_MissionReport_AcceptAccountant on MissionReport (AcceptAccountant)
create index IX_MissionReport_MissionOrder on MissionReport (MissionOrderId)
alter table MissionReport add constraint FK_MissionReport_User foreign key (UserId) references [Users]
alter table MissionReport add constraint FK_MissionReport_CreatorUser foreign key (CreatorId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptUser foreign key (AcceptUserId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptAccountant foreign key (AcceptAccountant) references [Users]
alter table MissionReport add constraint FK_MissionReport_MissionOrder foreign key (MissionOrderId) references MissionOrder
create index IX_ChiefToUser_Chief_Id on ChiefToUser (ChiefId)
create index IX_ChiefToUser_User_Id on ChiefToUser (UserId)
alter table ChiefToUser add constraint FK_ChiefToUser_Chief foreign key (ChiefId) references [Users]
alter table ChiefToUser add constraint FK_ChiefToUser_User foreign key (UserId) references [Users]
create index IX_DismissalComment_User_Id on DismissalComment (UserId)
create index IX_DismissalComment_Dismissal_Id on DismissalComment (DismissalId)
alter table DismissalComment add constraint FK_DismissalComment_User foreign key (UserId) references [Users]
alter table DismissalComment add constraint FK_DismissalComment_Dismissal foreign key (DismissalId) references Dismissal
create index IX_MissionComment_User_Id on MissionComment (UserId)
create index IX_MissionComment_Mission_Id on MissionComment (MissionId)
alter table MissionComment add constraint FK_MissionComment_User foreign key (UserId) references [Users]
alter table MissionComment add constraint FK_MissionComment_Mission foreign key (MissionId) references Mission
create index MissionOrder_MissionType on MissionOrder (TypeId)
create index MissionOrder_MissionGoal on MissionOrder (MissionGoalId)
create index IX_MissionOrder_Secretary_Id on MissionOrder (SecretaryId)
create index IX_MissionOrder_User_Id on MissionOrder (UserId)
create index IX_MissionOrder_CreatorUser_Id on MissionOrder (CreatorId)
create index IX_MissionOrder_AcceptUser on MissionOrder (AcceptUserId)
create index IX_MissionOrder_AcceptManager on MissionOrder (AcceptManagerId)
create index IX_MissionOrder_AcceptChief on MissionOrder (AcceptChiefId)
create index IX_MissionOrder_Mission on MissionOrder (MissionId)
alter table MissionOrder add constraint FK_MissionOrder_MissionType foreign key (TypeId) references MissionType
alter table MissionOrder add constraint FK_MissionOrder_MissionGoal foreign key (MissionGoalId) references MissionGoal
alter table MissionOrder add constraint FK_MissionOrder_Secretary foreign key (SecretaryId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_User foreign key (UserId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_CreatorUser foreign key (CreatorId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptUser foreign key (AcceptUserId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptChief foreign key (AcceptChiefId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_Mission foreign key (MissionId) references Mission
create index IX_EmploymentComment_User_Id on EmploymentComment (UserId)
create index IX_EmploymentComment_Employment_Id on EmploymentComment (EmploymentId)
alter table EmploymentComment add constraint FK_EmploymentComment_User foreign key (UserId) references [Users]
alter table EmploymentComment add constraint FK_EmploymentComment_Employment foreign key (EmploymentId) references Employment
create index MissionPurchaseBookDocument_Contractor on MissionPurchaseBookDocument (ContractorId)
create index IX_MissionPurchaseBookDocument_EditorUser on MissionPurchaseBookDocument (EditorId)
alter table MissionPurchaseBookDocument add constraint FK_MissionPurchaseBookDocument_Contractor foreign key (ContractorId) references Contractor
alter table MissionPurchaseBookDocument add constraint FK_MissionPurchaseBookDocument_EditorUser foreign key (EditorId) references [Users]
create index IX_MissionOrderTarget_MissionOrder on MissionTarget (MissionOrderId)
create index IX_MissionTarget_Country_Id on MissionTarget (CountryId)
create index IX_MissionTarget_DailyAllowance_Id on MissionTarget (DailyAllowanceId)
create index IX_MissionTarget_Residence_Id on MissionTarget (ResidenceId)
create index IX_MissionTarget_AirTicketType_Id on MissionTarget (AirTicketTypeId)
create index IX_MissionTarget_TrainTicketType_Id on MissionTarget (TrainTicketTypeId)
alter table MissionTarget add constraint FK_MissionOrderTarget_MissionOrder foreign key (MissionOrderId) references MissionOrder
alter table MissionTarget add constraint FK_MissionTarget_Country foreign key (CountryId) references MissionCountry
alter table MissionTarget add constraint FK_MissionTarget_DailyAllowance foreign key (DailyAllowanceId) references MissionDailyAllowance
alter table MissionTarget add constraint FK_MissionTarget_Residence foreign key (ResidenceId) references MissionResidence
alter table MissionTarget add constraint FK_MissionTarget_AirTicketType foreign key (AirTicketTypeId) references MissionAirTicketType
alter table MissionTarget add constraint FK_MissionTarget_TrainTicketType foreign key (TrainTicketTypeId) references MissionTrainTicketType
alter table TerraPointToUser add constraint FK_TerraPointToUser_TerraPoint foreign key (TerraPointId) references TerraPoint
create index IX_InspectorToUser_Inspector_Id on InspectorToUser (InspectorId)
create index IX_InspectorToUser_User_Id on InspectorToUser (UserId)
alter table InspectorToUser add constraint FK_InspectorToUser_Inspector foreign key (InspectorId) references [Users]
alter table InspectorToUser add constraint FK_InspectorToUser_User foreign key (UserId) references [Users]
create index IX_DocumentComment_User_Id on DocumentComment (UserId)
create index IX_DocumentComment_Document_Id on DocumentComment (DocumentId)
alter table DocumentComment add constraint FK_DocumentComment_User foreign key (UserId) references [Users]
alter table DocumentComment add constraint FK_DocumentComment_Document foreign key (DocumentId) references Document
create index IX_MissionDailyAllowanceGradeValue_DailyAllowance_Id on MissionDailyAllowanceGradeValue (DailyAllowanceId)
create index IX_MissionDailyAllowanceGradeValue_Grade_Id on MissionDailyAllowanceGradeValue (GradeId)
alter table MissionDailyAllowanceGradeValue add constraint FK_MissionDailyAllowanceGradeValue_DailyAllowance foreign key (DailyAllowanceId) references MissionDailyAllowance
alter table MissionDailyAllowanceGradeValue add constraint FK_MissionDailyAllowanceGradeValue_Grade foreign key (GradeId) references MissionGraid
create index TimesheetCorrection_TimesheetCorrectionType on TimesheetCorrection (TypeId)
create index IX_TimesheetCorrection_User_Id on TimesheetCorrection (UserId)
create index IX_TimesheetCorrection_CreatorUser_Id on TimesheetCorrection (CreatorId)
create index TimesheetCorrection_TimesheetStatus on TimesheetCorrection (TimesheetStatusId)
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_TimesheetCorrectionType foreign key (TypeId) references TimesheetCorrectionType
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_User foreign key (UserId) references [Users]
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_CreatorUser foreign key (CreatorId) references [Users]
alter table TimesheetCorrection add constraint FK_TimesheetCorrection_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_SicklistComment_User_Id on SicklistComment (UserId)
create index IX_SicklistComment_Sicklist_Id on SicklistComment (SicklistId)
alter table SicklistComment add constraint FK_SicklistComment_User foreign key (UserId) references [Users]
alter table SicklistComment add constraint FK_SicklistComment_Sicklist foreign key (SicklistId) references Sicklist
create index IX_User_UserManager_Id on [Users] (ManagerId)
create index IX_User_Organization_Id on [Users] (OrganizationId)
create index IX_User_Position_Id on [Users] (PositionId)
create index IX_User_Department_Id on [Users] (DepartmentId)
alter table [Users] add constraint FK_User_UserManager foreign key (ManagerId) references [Users]
alter table [Users] add constraint FK_User_Organization foreign key (OrganizationId) references Organization
alter table [Users] add constraint FK_User_Position foreign key (PositionId) references Position
alter table [Users] add constraint FK_User_Department foreign key (DepartmentId) references Department
alter table UserToPersonnel add constraint FK_UserToPersonnel_Personnel foreign key (PersonnelId) references [Users]
alter table UserToPersonnel add constraint FK_UserToPersonnel_User foreign key (UserId) references [Users]
create index IX_AppointmentManager2ToManager3_Manager2 on AppointmentManager2ToManager3 (Manager2Id)
create index IX_AppointmentManager2ToManager3_Manager3 on AppointmentManager2ToManager3 (Manager3Id)
alter table AppointmentManager2ToManager3 add constraint FK_AppointmentManager2ToManager3_Manager2 foreign key (Manager2Id) references [Users]
alter table AppointmentManager2ToManager3 add constraint FK_AppointmentManager2ToManager3_Manager3 foreign key (Manager3Id) references [Users]
create index IX_MissionTrainTicketTypeGradeValue_TrainTicketType_Id on MissionTrainTicketTypeGradeValue (TrainTicketTypeId)
create index IX_MissionTrainTicketTypeGradeValue_Grade_Id on MissionTrainTicketTypeGradeValue (GradeId)
alter table MissionTrainTicketTypeGradeValue add constraint FK_MissionTrainTicketTypeGradeValue_TrainTicketType foreign key (TrainTicketTypeId) references MissionTrainTicketType
alter table MissionTrainTicketTypeGradeValue add constraint FK_MissionTrainTicketTypeGradeValue_Grade foreign key (GradeId) references MissionGraid
create index IX_WorkingGraphicTypeToUser_WorkingGraphicType_Id on WorkingGraphicTypeToUser (WorkingGraphicTypeId)
create index IX_WorkingGraphicType_User_Id on WorkingGraphicTypeToUser (UserId)
alter table WorkingGraphicTypeToUser add constraint FK_WorkingGraphicTypeToUser_WorkingGraphicType foreign key (WorkingGraphicTypeId) references WorkingGraphicType
alter table WorkingGraphicTypeToUser add constraint FK_WorkingGraphicType_User foreign key (UserId) references [Users]
create index IX_HolidayWorkComment_User_Id on HolidayWorkComment (UserId)
create index IX_HolidayWorkComment_HolidayWork_Id on HolidayWorkComment (HolidayWorkId)
alter table HolidayWorkComment add constraint FK_HolidayWorkComment_User foreign key (UserId) references [Users]
alter table HolidayWorkComment add constraint FK_HolidayWorkComment_HolidayWork foreign key (HolidayWorkId) references HolidayWork
create index Absence_AbsenceType on Absence (TypeId)
create index IX_Absence_User_Id on Absence (UserId)
create index IX_Absence_CreatorUser_Id on Absence (CreatorId)
create index Absence_TimesheetStatus on Absence (TimesheetStatusId)
alter table Absence add constraint FK_Absence_AbsenceType foreign key (TypeId) references AbsenceType
alter table Absence add constraint FK_Absence_User foreign key (UserId) references [Users]
alter table Absence add constraint FK_Absence_CreatorUser foreign key (CreatorId) references [Users]
alter table Absence add constraint FK_Absence_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_AppointmentComment_User on AppointmentComment (UserId)
create index IX_AppointmentComment_Appointment on AppointmentComment (AppointmentId)
alter table AppointmentComment add constraint FK_AppointmentComment_User foreign key (UserId) references [Users]
alter table AppointmentComment add constraint FK_AppointmentComment_Appointment foreign key (AppointmentId) references Appointment
create index IX_MissionOrderRoleRecord_User_Id on [MissionOrderRoleRecord] (UserId)
create index IX_MissionOrderRoleRecord_MissionOrderRole_Id on [MissionOrderRoleRecord] (RoleId)
create index IX_MissionOrderRoleRecord_TargetUser_Id on [MissionOrderRoleRecord] (TargetUserId)
create index IX_MissionOrderRoleRecord_TargetDepartment_Id on [MissionOrderRoleRecord] (TargetDepartmentId)
alter table [MissionOrderRoleRecord] add constraint FK_MissionOrderRoleRecord_User foreign key (UserId) references [Users]
alter table [MissionOrderRoleRecord] add constraint FK_MissionOrderRoleRecord_MissionOrderRole foreign key (RoleId) references [MissionOrderRole]
alter table [MissionOrderRoleRecord] add constraint FK_MissionOrderRoleRecord_TargetUser foreign key (TargetUserId) references [Users]
alter table [MissionOrderRoleRecord] add constraint FK_MissionOrderRoleRecord_TargetDepartment foreign key (TargetDepartmentId) references Department
create index IX_ChildVacation_User_Id on ChildVacation (UserId)
create index IX_ChildVacation_CreatorUser_Id on ChildVacation (CreatorId)
create index ChildVacation_TimesheetStatus on ChildVacation (TimesheetStatusId)
alter table ChildVacation add constraint FK_ChildVacation_User foreign key (UserId) references [Users]
alter table ChildVacation add constraint FK_ChildVacation_CreatorUser foreign key (CreatorId) references [Users]
alter table ChildVacation add constraint FK_ChildVacation_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index Employment_EmploymentType on Employment (TypeId)
create index Employment_EmploymentHoursType on Employment (HoursTypeId)
create index Employment_Addition on Employment (AdditionId)
create index Employment_Position on Employment (PositionId)
create index IX_Employment_User_Id on Employment (UserId)
create index IX_Employment_CreatorUser_Id on Employment (CreatorId)
create index Employment_TimesheetStatus on Employment (TimesheetStatusId)
alter table Employment add constraint FK_Employment_EmploymentType foreign key (TypeId) references EmploymentType
alter table Employment add constraint FK_Employment_EmploymentHoursType foreign key (HoursTypeId) references EmploymentHoursType
alter table Employment add constraint FK_Employment_Addition foreign key (AdditionId) references EmploymentAddition
alter table Employment add constraint FK_Employment_Position foreign key (PositionId) references Position
alter table Employment add constraint FK_Employment_User foreign key (UserId) references [Users]
alter table Employment add constraint FK_Employment_CreatorUser foreign key (CreatorId) references [Users]
alter table Employment add constraint FK_Employment_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_TimesheetDay_Status_Id on TimesheetDay (StatusId)
create index IX_TimesheetDay_Timesheet_Id on TimesheetDay (TimesheetId)
alter table TimesheetDay add constraint FK_TimesheetDay_Status foreign key (StatusId) references TimesheetStatus
alter table TimesheetDay add constraint FK_TimesheetDay_Timesheet foreign key (TimesheetId) references Timesheet
create index IX_ClearanceChecklistRoleRecord_User_Id on [ClearanceChecklistRoleRecord] (UserId)
create index IX_ClearanceChecklistRoleRecord_ClearanceChecklistRole_Id on [ClearanceChecklistRoleRecord] (RoleId)
create index IX_ClearanceChecklistRoleRecord_TargetUser_Id on [ClearanceChecklistRoleRecord] (TargetUserId)
create index IX_ClearanceChecklistRoleRecord_TargetDepartment_Id on [ClearanceChecklistRoleRecord] (TargetDepartmentId)
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_User foreign key (UserId) references [Users]
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_ClearanceChecklistRole foreign key (RoleId) references [ClearanceChecklistRole]
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_TargetUser foreign key (TargetUserId) references [Users]
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_TargetDepartment foreign key (TargetDepartmentId) references Department
create index IX_ClearanceChecklistApproval_Dismissal_Id on ClearanceChecklistApproval (DismissalId)
create index IX_ClearanceChecklistApproval_ClearanceChecklistRole_Id on ClearanceChecklistApproval (RoleId)
create index IX_ClearanceChecklistApproval_ApprovedBy_Id on ClearanceChecklistApproval (ApprovedById)
alter table ClearanceChecklistApproval add constraint FK_ClearanceChecklistApproval_Dismissal foreign key (DismissalId) references Dismissal
alter table ClearanceChecklistApproval add constraint FK_ClearanceChecklistApproval_ClearanceChecklistRole foreign key (RoleId) references [ClearanceChecklistRole]
alter table ClearanceChecklistApproval add constraint FK_ClearanceChecklistApproval_ApprovedBy foreign key (ApprovedById) references [Users]
create index IX_AppointmentCreateManager2ToDepartment2_User on AppointmentCreateManager2ToDepartment2 (ManagerId)
create index IX_AppointmentCreateManager2ToDepartment2_Department on AppointmentCreateManager2ToDepartment2 (DepartmentId)
alter table AppointmentCreateManager2ToDepartment2 add constraint FK_AppointmentCreateManager2ToDepartment2_User foreign key (ManagerId) references [Users]
alter table AppointmentCreateManager2ToDepartment2 add constraint FK_AppointmentCreateManager2ToDepartment2_Department foreign key (DepartmentId) references Department
create index Vacation_VacationType on Vacation (TypeId)
create index IX_Vacation_User_Id on Vacation (UserId)
create index IX_Vacation_CreatorUser_Id on Vacation (CreatorId)
create index Vacation_TimesheetStatus on Vacation (TimesheetStatusId)
alter table Vacation add constraint FK_Vacation_VacationType foreign key (TypeId) references VacationType
alter table Vacation add constraint FK_Vacation_User foreign key (UserId) references [Users]
alter table Vacation add constraint FK_Vacation_CreatorUser foreign key (CreatorId) references [Users]
alter table Vacation add constraint FK_Vacation_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_Timesheet_User_Id on Timesheet (UserId)
alter table Timesheet add constraint FK_Timesheet_User foreign key (UserId) references [Users]
alter table UserLogin add constraint FK_UserLogin_User foreign key (UserId) references [Users]
create index IX_AppointmentManager2ParentToManager2Child_Parent on AppointmentManager2ParentToManager2Child (ParentId)
create index IX_AppointmentManager2ParentToManager2Child_Child on AppointmentManager2ParentToManager2Child (ChildId)
alter table AppointmentManager2ParentToManager2Child add constraint FK_AppointmentManager2ParentToManager2Child_Parent foreign key (ParentId) references [Users]
alter table AppointmentManager2ParentToManager2Child add constraint FK_AppointmentManager2ParentToManager2Child_Child foreign key (ChildId) references [Users]
create index MissionReportCost_MissionReport on MissionReportCost (ReportId)
create index MissionReportCost_MissionReportCostType on MissionReportCost (TypeId)
alter table MissionReportCost add constraint FK_MissionReportCost_MissionReport foreign key (ReportId) references MissionReport
alter table MissionReportCost add constraint FK_MissionReportCost_MissionReportCostType foreign key (TypeId) references MissionReportCostType
create index IX_MissionAirTicketTypeGradeValue_AirTicketType_Id on MissionAirTicketTypeGradeValue (AirTicketTypeId)
create index IX_MissionAirTicketTypeGradeValue_Grade_Id on MissionAirTicketTypeGradeValue (GradeId)
alter table MissionAirTicketTypeGradeValue add constraint FK_MissionAirTicketTypeGradeValue_AirTicketType foreign key (AirTicketTypeId) references MissionAirTicketType
alter table MissionAirTicketTypeGradeValue add constraint FK_MissionAirTicketTypeGradeValue_Grade foreign key (GradeId) references MissionGraid
create index IX_ATTACHMENT_USER_ROLE_ID on RequestAttachment (CreatorRoleId)
alter table RequestAttachment add constraint FK_ATTACHMENT_USER_ROLE foreign key (CreatorRoleId) references Role
create index IX_AppointmentManager23ToDepartment3_User on AppointmentManager23ToDepartment3 (ManagerId)
create index IX_AppointmentManager23ToDepartment3_Department on AppointmentManager23ToDepartment3 (DepartmentId)
alter table AppointmentManager23ToDepartment3 add constraint FK_AppointmentManager23ToDepartment3_User foreign key (ManagerId) references [Users]
alter table AppointmentManager23ToDepartment3 add constraint FK_AppointmentManager23ToDepartment3_Department foreign key (DepartmentId) references Department
create index HolidayWork_HolidayWorkType on HolidayWork (TypeId)
create index IX_HolidayWork_User_Id on HolidayWork (UserId)
create index IX_HolidayWork_CreatorUser_Id on HolidayWork (CreatorId)
create index HolidayWork_TimesheetStatus on HolidayWork (TimesheetStatusId)
alter table HolidayWork add constraint FK_HolidayWork_HolidayWorkType foreign key (TypeId) references HolidayWorkType
alter table HolidayWork add constraint FK_HolidayWork_User foreign key (UserId) references [Users]
alter table HolidayWork add constraint FK_HolidayWork_CreatorUser foreign key (CreatorId) references [Users]
alter table HolidayWork add constraint FK_HolidayWork_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_Attachment_Document_Id on Attachment (DocumentId)
alter table Attachment add constraint FK_Attachment_Document foreign key (DocumentId) references Document
create index IX_Document_EmployeeDocumentType_Id on Document (TypeId)
create index IX_Document_EmployeeDocumentSubType_Id on Document (SubTypeId)
create index IX_Document_User_Id on Document (UserId)
alter table Document add constraint FK_Document_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
alter table Document add constraint FK_Document_EmployeeDocumentSubType foreign key (SubTypeId) references EmployeeDocumentSubType
alter table Document add constraint FK_Document_User foreign key (UserId) references [Users]

set identity_insert  [Role] on
INSERT INTO [Role] (Id,[Name],Version) values (1,'Администратор',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'Сотрудник',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'Руководитель',1) 
INSERT INTO [Role] (Id,[Name],Version) values (8,'Кадровик',1) 
INSERT INTO [Role] (Id,[Name],Version) values (16,'Бюджет',1) 
INSERT INTO [Role] (Id,[Name],Version) values (32,'Отусорсинг',1)
INSERT INTO [Role] (Id,[Name],Version) values (64,'Контролер',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'Не одобрена сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'Одобрена сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'Одобрена руководителем',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'Одобрена кадровиком',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'Выгружена в 1С',1)
set identity_insert  [RequestStatus] off 

declare @OrganizationId int
declare @Organization1Id int
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values ('1','Тестовая организация',1)	
set @OrganizationId = @@Identity
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values ('2','Тестовая организация 1',1)	
set @Organization1Id = @@Identity	
	

declare @DepartmentId int
declare @Department1Id int
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values ('1','Тестовый департамент',1)		
set @DepartmentId = @@Identity	
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values ('2','Тестовый департамент 1',1)		
set @Department1Id = @@Identity	


declare @PositionId int
declare @Position1Id int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('1','Тестовая должность',1)		
set @PositionId = @@Identity
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('2','Тестовая должность 1',1)		
set @Position1Id = @@Identity
declare @ManPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('3','Руководитель',1)		
set @ManPositionId = @@Identity	
declare @PerPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('4','Кадровик',1)		
set @PerPositionId = @@Identity	
declare @InsPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('5','Контролер',1)		
set @InsPositionId = @@Identity	
	

INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('51','Дополнительный учебный отпуск без оплаты #1203',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('31','Оплата дня сдачи крови и доп. дня отдыха донорам #1125',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('41','Оплата дополнительного отпуска по календарным дням #1207',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('32','Оплата дополнительных выходных дней по уходу за детьми - инвалидами #1504',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('41','Оплата отпуска по календарным дням #1201',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('42','Оплата отпуска по шестидневке #1202',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('41','Оплата учебного отпуска по календарным дням #1204',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('53','Отпуск без оплаты согласно ТК РФ #1205',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('54','Отпуск за свой счет #1206',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('23','Отпуск по беременности и родам #1501',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('52','Отпуск по уходу за ребенком без оплаты #1802',1)		


INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('51','Дополнительный учебный отпуск без оплаты #1203',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('53','Отпуск без оплаты согласно ТК РФ #1205',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('54','Отпуск за свой счет#1206',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('10024','Отсутствие по болезни (по беременности и родам) #1804',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('10023','Отсутствие по болезни #1803',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('55','Отсутствие по невыясненной причине #1806',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('56','Прогул, простой по вине работника #1807',1)	

INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('25','БЛ по травме в быту (не оплачивается) #1805',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('26','Доплата по больничным листам #1402',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('22','Оплата БЛ по травме на производстве #1403',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('25','Оплата больничных листов за счет работодателя #1426',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('21','Оплата больничных листов #1469',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('10024','Отсутствие по болезни (по беременности и родам) #1804',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('10023','Отсутствие по болезни #1803',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('55','Отсутствие по невыясненной причине #1806',1)
set identity_insert  [dbo].[SicklistType] on
INSERT INTO [dbo].[SicklistType]  (Id,[Code],[Name],Version) values (9,'71','Пособие по уходу за ребёнком до 1.5 лет #1502',1)
INSERT INTO [dbo].[SicklistType]  (Id,[Code],[Name],Version) values (10,'72','Пособие по уходу за ребёнком до 3 лет #1503',1)
set identity_insert [dbo].[SicklistType] off 

INSERT INTO [dbo].[SicklistBabyMindingType]  ([Code],[Name],Version) values ('1','Тестовый уход за ребенком 1',1)
INSERT INTO [dbo].[SicklistBabyMindingType]  ([Code],[Name],Version) values ('2','Тестовый уход за ребенком 2',1)

INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values ('13','Доплата за работу в праздники и выходные #1107',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values ('12','Оплата праздничных и выходных дней #1106',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (null,'Отгул',1)

INSERT INTO [dbo].[MissionType]  ([Code],[Name],Version) values ('1','Командировку по России',1)
INSERT INTO [dbo].[MissionType]  ([Code],[Name],Version) values ('2','Командировку по СНГ',1)
INSERT INTO [dbo].[MissionType]  ([Code],[Name],Version) values ('3','Командировку  за рубеж',1)

INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('1','По совместительству',1)
INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('2','На основное место работы',1)
INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('3','По срочному трудовому договору',1)
INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('4','Переводом',1)

INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('1','Пятидневка   0,2 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('2','0,001',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('3','0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('4','0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('5','0,1',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('6','0,16',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('7','0,2 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('8','0,55 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('9','0,68 ставки (27,2 часа)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('10','0,7 ставки (28 часов)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('11','0,8 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('12','0.8',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('13','1 час',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('14','10 часов (0,25)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('15','10-часовая',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('16','12 ч (0,3ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('17','14ч (0,35 ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('18','16 ч (0,4ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('19','16,8 ч (0,42 ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('20','20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('21','20 часов (0,5 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('22','24 ч(0,6ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('23','25 (0,625)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('24','29,2 часа (0.73 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('25','30 (0,75ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('26','32 часа ( 0.8ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('27','33.6 часа ( 0.84ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('28','34,8 часа ( 0,87 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('29','35 часов (0,87 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('30','35,6 часа (0,89ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('31','36,4 часа ( 0,91ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('32','36.8 часа  (0,92ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('33','37,5 часов (0,94 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('34','38,4 часа (0,96ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('35','39.6 часа ( 0.99ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('36','4 часа (0,1 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('37','6,4 часа (0,16 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('38','6,8 часа (0,17 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('39','График 15ч/н (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('40','график 2 через 2 2смена',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('41','график 2 через 2 по 11 часов 1смена',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('42','График 25ч/н (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('43','Основной',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('44','Основной график',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('45','Основной график (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('46','Основной график 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('47','Основной график 20часов неделя (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('48','Основной график 40часов неделя',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('49','Пятидневка',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('50','Пятидневка (30 часов)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('51','Пятидневка 0.4ст',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('52','пятидневка 10часов (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('53','Пятидневка 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('54','Пятидневка 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('55','Пятидневка 30 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('56','Пятидневка 32 часа',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('57','Пятидневка 35',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('58','Пятидневка 35 часов (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('59','Пятидневка 37,5',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('60','Сменный',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('61','Сокращенный день по 7 часов (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('61','Сум. учет раб. времени',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('63','Суммированный учет',1)

INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за выслугу лет рабочим и служащим #1114','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за квалификацию #1115','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за разъездной характер работы #1116','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка Персональная #1117','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка территориальная #1123','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','Районный коэффициент #1301','Процентом Зависимое второго уровня',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values (null,'Расширение зоны обслуживания #1120','По месячной тарифной ставке по часам Первичное',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','Северная надбавка 1 #1302','Процентом Зависимое второго уровня',1)

--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Заявление работника',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Служебная записка',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Распоряжение руководителя',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Медицинское заключение',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Приказ руководителя',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Справка о смерти',1)

INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('1','п. 1 ст. 81 ТК','Ликвидация',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('2','п. 1 части 1 ст. 77 ТК','Соглашение сторон.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('3','п. 2 ст. 77 ТК','Истечение срока трудового договора.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('4','п. 2 части 1 ст. 81 ТК','Сокращение численности штата',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('5','п. 3 ст. 77 ТК','Расторжение трудового договора по инициативе работника.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('6','п. 4 ст. 77 ТК','Расторжение трудового договора по инициативе работодателя',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('7','п. 5 ст. 77 ТК','Перевод работника на другую работу к другому работодателю',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('8','п. 5 части первой ст. 77','Перевод работника на выборную должность',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('9','п. 6 ст. 81 ТК','Расторжение трудового договора по инициативе работодателя за прогул.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('10','п. 6 ст.83 ТК','Смерть  работника  либо работодателя - физ. лица, а также признание судом работника либо работодателя - физ. лица умершим',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('11','п. 7 ч. 1 ст.81 ТК РФ','Трудовой договор расторгнут в связи с совершением виновных действий работником',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('12','п. 7 части 1 ст. 77','Отказ работника от продолжения работы в связи с изменением определенных сторонами условий ТД',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('13','п.3 части 1 ст. 77 ТК РФ','Собственное желание',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('14','п.5 части первой ст. 83 ТК РФ','Трудовой договор прекращен в связи с признанием работника полностью неспособным к трудовой деятельности в соответствии с медицинским заключением',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('15','п.6 ч.1 ст. 83','Трудовой договор прекращен в связи со смертью работника',1)

INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('2','Оклад по часам #1102','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('3','Оплата по часовому тарифу #1103','По часовой тарифной ставке',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('66','Оплата почасового простоя от оклада по часам #1707','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('67','Оплата почасового простоя по часовому тарифу# 1708','По часовой тарифной ставке',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('68','Почасовой простой по вине работодателя #1702','По среднему заработку',1)



--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('Тест 1',1)
--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('Тест 2',1)


INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Code],[Name],Version) values ('1','В размере ММОТ',1)
INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Code],[Name],Version) values ('2','Общее, в соответствии с Законом о бюджете ФСС',1)

--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'БЛ по травме в быту (не оплачивается) #1805','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (26,'Доплата по больничным листам #1402','Доплата до среднего заработка ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (22,'Оплата БЛ по травме на производстве #1403','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'Оплата больничных листов за счет работодателя #1426','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (21,'Оплата больничных листов #1469','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10023,'Отсутствие по болезни #1803','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (55,'Отсутствие по невыясненной причине #1806','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (71,'Пособие по уходу за ребёнком до 1.5 лет #1502','Пособие по уходу за ребенком до 1.5 лет',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (72,'Пособие по уходу за ребёнком до 3 лет #1503','Пособие по уходу за ребенком до 3 лет',1)

INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (60,3,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (80,2,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (100,1,1)



set identity_insert  [TimesheetStatus] on
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (1,1,'Я','Явка')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (2,1,'Б','Временная нетрудоспособность с назначением пособия согласно законодательству')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (3,1,'Т','Временная нетрудоспособность без назнач. пособия в случаях, предусм. законодательством')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (4,1,'ВЧ','Вечерние часы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (5,1,'Н','Ночные часы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (6,1,'В','Выходные и нерабочие дни')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (7,1,'К','Командировка')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (8,1,'ОТ','Отпуск')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (9,1,'ОЗ','Отпуск без сохранения заработной платы в случаях, предусмотренных законодательством')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (10,1,'ДО','Отпуск без сохранения заработной платы, предоставляемый сотр. по разреш. работодателя')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (11,1,'Р','Отпуск по беременности и родам (отпуск в связи с усыновлением новорожд. ребенка)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (12,1,'ОЖ','Отпуск по уходу за ребенком до достижения им возраста трех лет')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (13,1,'РВ','Продолжительность работы в выходные и нерабочие, праздничные дни')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (14,1,'С','Продолжительность сверхурочной работы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (15,1,'ПР','Прогулы (отсутствие на рабочем месте без уваж. причин в теч. времени, уст. законодательством)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (16,1,'НН','Неявки по невыясненным причинам')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (17,1,'ВП','Простои по вине сотрудника')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (18,1,'РП','Время простоя по вине работодателя')
set identity_insert  [TimesheetStatus] off


/*declare @typeId int
declare @firstTypeId int
declare @firstSubTypeId int
INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Отпуск',1) 
set @typeId = @@Identity
set @firstTypeId = @typeId
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Ежегодный',1,@typeId) 
set @firstSubTypeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Дополнительный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Учебный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за ребенком',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Без сохранения ЗП',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Донорские дни',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Командировка',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По РФ',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По СНГ',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('За рубеж',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Больничный',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По нетрудоспособности',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за ребенком',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Травма на производстве',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Санитарно — курортный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По беременности и родам',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за взрослым',1,@typeId) 


INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Отсутствие на рабочем месте',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Прогул',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По невыясненной причине',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Увольнение',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По собственному желанию',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По сокращению штата',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По соглашению сторон',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По инициативе работодателя',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('В связи с переводом',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Перемещение',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Изменение оплаты труда',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('места работы',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('должности',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Прочее',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Простой по вине работодателя',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Мат. помощь',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Погребение',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Регистрация брака (прочее)',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Рождение, усыновление',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие по уходу за реб. 3х лет',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие по уходу за реб. 1,5х лет',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Сохраняемый заработок на время трудоустройства',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Выплата за счет ФСС',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС в связи со смертью',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при постановке на учет в ранние сроки беременности',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при рождении ребенка',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при усыновлении ребенка',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Удержание',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Сотовая связь',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Исполнительный лист',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Обслуживание авто',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Налоговый вычет',1,@typeId) 
*/

-- User
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]		, [IsNew]	 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'Администратор',             1,		  null ,          1,           'АА0000000001' , 0)
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'Руководитель',              1,         null								, 4,		   'АВ0000000001' , 0, @ManPositionId)
set @managerId = @@Identity

declare @managerId1 int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'manager1' ,'manager1'  ,	'2008-12-01 15:13:25:000',  N'Руководитель 1',              1,         null								, 4,		   'АВ0000000002' , 0, @ManPositionId)
set @managerId1 = @@Identity

--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'Кадровик',                    1,         null								, 8,		   'АГ0000000001' , 0, @PerPositionId)
set @personnelId = @@Identity

declare @personnel1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'personnel1' ,'personnel1'  ,	'2008-12-01 15:13:25:000',    N'Кадровик1',                    1,         null								, 8,		   'АГ0000000002' , 0, @PerPositionId)
set @personnel1Id = @@Identity

insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@managerId)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@managerId1)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnel1Id,@managerId)


declare @inspectorId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'inspector' ,'inspector'  ,	'2008-12-01 15:13:25:000',    N'Контролер',                    1,         null								, 64,		   'АЕ0000000001' , 0, @InsPositionId)
set @inspectorId = @@Identity

declare @inspector1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'inspector1' ,'inspector1'  ,	'2008-12-01 15:13:25:000',    N'Контролер 1',                    1,         null								, 64,		   'АЖ0000000001' , 0, @InsPositionId)
set @inspector1Id = @@Identity


--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
/*declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'Бюджет',           1,         null								        , 16,		   'АГ0000000001' , 0)
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew] ) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'Аутсорсинг',                        1,         null,              32,		       'АД0000000001' , 0)
set @outsorsingId = @@Identity*/
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         /*PersonnelManagerId,*/ OrganizationId,DepartmentId,PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'Пользователь',                   1,         null            , 2,		   'АБ0000000001' ,  @managerId,       /*@personnelId,*/       @OrganizationId,@DepartmentId,@PositionId , 0)
set @userId = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,/*PersonnelManagerId,*/            OrganizationId,DepartmentId,PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'Иванов Иван Иванович',            1,         null            , 2,		   'АЕ0000000001' ,  @managerId,       /*@personnelId,*/@Organization1Id,@Department1Id,@Position1Id , 0)
set @user1Id = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

declare @user2Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,/*PersonnelManagerId ,*/ [IsNew], OrganizationId,DepartmentId,PositionId) 
VALUES			   (1,       	0              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'Петров Петр Петрович',         1,         null            , 2,		   'АЖ0000000001' ,  @managerId,       /*@personnelId ,*/ 0 ,  @OrganizationId,@DepartmentId, @PositionId )
set @user2Id = @@Identity

insert into [dbo].[InspectorToUser] (InspectorId,UserId) values (@inspectorId,@userId)
insert into [dbo].[InspectorToUser] (InspectorId,UserId) values (@inspectorId,@user1Id)
insert into [dbo].[InspectorToUser] (InspectorId,UserId) values (@inspectorId,@user2Id)

insert into [dbo].[InspectorToUser] (InspectorId,UserId) values (@inspector1Id,@userId)
insert into [dbo].[InspectorToUser] (InspectorId,UserId) values (@inspector1Id,@user1Id)

insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@userId)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@user1Id)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@user2Id)

insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnel1Id,@userId)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnel1Id,@user1Id)

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     'Тестовый',  @firstTypeId, @firstSubTypeId, @userId, 0)

