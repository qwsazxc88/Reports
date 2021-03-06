if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Certification_Education]') AND parent_object_id = OBJECT_ID('Certification'))
alter table Certification  drop constraint FK_Certification_Education

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Family_Candidate]') AND parent_object_id = OBJECT_ID('Family'))
alter table Family  drop constraint FK_Family_Candidate

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServicePeriod_Type]') AND parent_object_id = OBJECT_ID('HelpServicePeriod'))
alter table HelpServicePeriod  drop constraint FK_HelpServicePeriod_Type

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_SupplementaryAgreement_PersonnelManagers]') AND parent_object_id = OBJECT_ID('SupplementaryAgreement'))
alter table SupplementaryAgreement  drop constraint FK_SupplementaryAgreement_PersonnelManagers

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequestHistoryEntity_HelpQuestionRequest]') AND parent_object_id = OBJECT_ID('HelpQuestionHistoryEntity'))
alter table HelpQuestionHistoryEntity  drop constraint FK_HelpQuestionRequestHistoryEntity_HelpQuestionRequest

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionHistoryEntity_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpQuestionHistoryEntity'))
alter table HelpQuestionHistoryEntity  drop constraint FK_HelpQuestionHistoryEntity_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionHistoryEntity_Consultant]') AND parent_object_id = OBJECT_ID('HelpQuestionHistoryEntity'))
alter table HelpQuestionHistoryEntity  drop constraint FK_HelpQuestionHistoryEntity_Consultant

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionSubtype_HelpQuestionType]') AND parent_object_id = OBJECT_ID('HelpQuestionSubtype'))
alter table HelpQuestionSubtype  drop constraint FK_HelpQuestionSubtype_HelpQuestionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PersonnelManagers_Candidate]') AND parent_object_id = OBJECT_ID('PersonnelManagers'))
alter table PersonnelManagers  drop constraint FK_PersonnelManagers_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PersonnelManagers_PersonalAccountContractor]') AND parent_object_id = OBJECT_ID('PersonnelManagers'))
alter table PersonnelManagers  drop constraint FK_PersonnelManagers_PersonalAccountContractor

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PersonnelManagers_AccessGroup]') AND parent_object_id = OBJECT_ID('PersonnelManagers'))
alter table PersonnelManagers  drop constraint FK_PersonnelManagers_AccessGroup

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PersonnelManagers_Signer]') AND parent_object_id = OBJECT_ID('PersonnelManagers'))
alter table PersonnelManagers  drop constraint FK_PersonnelManagers_Signer

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PersonnelManagers_ApprovedByPersonnelManagerUser]') AND parent_object_id = OBJECT_ID('PersonnelManagers'))
alter table PersonnelManagers  drop constraint FK_PersonnelManagers_ApprovedByPersonnelManagerUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AcceptRequestDate_User]') AND parent_object_id = OBJECT_ID('AcceptRequestDate'))
alter table AcceptRequestDate  drop constraint FK_AcceptRequestDate_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_DocumentSubType_EmployeeDocumentType]') AND parent_object_id = OBJECT_ID('EmployeeDocumentSubType'))
alter table EmployeeDocumentSubType  drop constraint FK_DocumentSubType_EmployeeDocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Experience_Candidate]') AND parent_object_id = OBJECT_ID('Experience'))
alter table Experience  drop constraint FK_Experience_Candidate

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Contacts_Candidate]') AND parent_object_id = OBJECT_ID('Contacts'))
alter table Contacts  drop constraint FK_Contacts_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_DeductionType]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_DeductionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_DeductionKind]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_DeductionKind

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_User]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_EditorUser]') AND parent_object_id = OBJECT_ID('Deduction'))
alter table Deduction  drop constraint FK_Deduction_EditorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Reference_BackgroundCheck]') AND parent_object_id = OBJECT_ID('Reference'))
alter table Reference  drop constraint FK_Reference_BackgroundCheck

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PostGraduateEducationDiploma_Education]') AND parent_object_id = OBJECT_ID('PostGraduateEducationDiploma'))
alter table PostGraduateEducationDiploma  drop constraint FK_PostGraduateEducationDiploma_Education

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_Department]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AppointmentReason]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AppointmentReason

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_CreatorUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_StaffCreatorUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_StaffCreatorUser

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MilitaryService_Candidate]') AND parent_object_id = OBJECT_ID('MilitaryService'))
alter table MilitaryService  drop constraint FK_MilitaryService_Candidate

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_Archivist]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_Archivist

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AdditionalMissionOrder]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AdditionalMissionOrder

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistComment_User]') AND parent_object_id = OBJECT_ID('ClearanceChecklistComment'))
alter table ClearanceChecklistComment  drop constraint FK_ClearanceChecklistComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ClearanceChecklistComment_ClearanceChecklist]') AND parent_object_id = OBJECT_ID('ClearanceChecklistComment'))
alter table ClearanceChecklistComment  drop constraint FK_ClearanceChecklistComment_ClearanceChecklist

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HigherEducationDiploma_Education]') AND parent_object_id = OBJECT_ID('HigherEducationDiploma'))
alter table HigherEducationDiploma  drop constraint FK_HigherEducationDiploma_Education

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OnsiteTraining_Candidate]') AND parent_object_id = OBJECT_ID('OnsiteTraining'))
alter table OnsiteTraining  drop constraint FK_OnsiteTraining_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OnsiteTraining_Approver]') AND parent_object_id = OBJECT_ID('OnsiteTraining'))
alter table OnsiteTraining  drop constraint FK_OnsiteTraining_Approver

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_MainMissionOrder]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_MainMissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_EmploymentComment_User]') AND parent_object_id = OBJECT_ID('EmploymentComment'))
alter table EmploymentComment  drop constraint FK_EmploymentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_EmploymentComment_Employment]') AND parent_object_id = OBJECT_ID('EmploymentComment'))
alter table EmploymentComment  drop constraint FK_EmploymentComment_Employment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpFaq_User]') AND parent_object_id = OBJECT_ID('HelpFaq'))
alter table HelpFaq  drop constraint FK_HelpFaq_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Education_Candidate]') AND parent_object_id = OBJECT_ID('Education'))
alter table Education  drop constraint FK_Education_Candidate

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_HelpQuestionType]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_HelpQuestionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_HelpQuestionSubtype]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_HelpQuestionSubtype

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_User]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_ConsultantOutsourcing]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_ConsultantOutsourcing

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_ConsultantPersonnel]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_ConsultantPersonnel

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequest_ConsultantAccountant]') AND parent_object_id = OBJECT_ID('HelpQuestionRequest'))
alter table HelpQuestionRequest  drop constraint FK_HelpQuestionRequest_ConsultantAccountant

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_User]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_GeneralInfo]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_GeneralInfo

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_Passport]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_Passport

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_Education]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_Education

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_Family]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_Family

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_MilitaryService]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_MilitaryService

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_Experience]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_Experience

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_Contacts]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_Contacts

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_BackgroundCheck]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_BackgroundCheck

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_OnsiteTraining]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_OnsiteTraining

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_Managers]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_Managers

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_PersonnelManagers]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_PersonnelManagers

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Candidate_AppointmentCreator]') AND parent_object_id = OBJECT_ID('EmploymentCandidate'))
alter table EmploymentCandidate  drop constraint FK_Candidate_AppointmentCreator

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequestComment_User]') AND parent_object_id = OBJECT_ID('HelpServiceRequestComment'))
alter table HelpServiceRequestComment  drop constraint FK_HelpServiceRequestComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequestComment_HelpServiceRequest]') AND parent_object_id = OBJECT_ID('HelpServiceRequestComment'))
alter table HelpServiceRequestComment  drop constraint FK_HelpServiceRequestComment_HelpServiceRequest

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_FamilyMember_Family]') AND parent_object_id = OBJECT_ID('FamilyMember'))
alter table FamilyMember  drop constraint FK_FamilyMember_Family

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ExperienceItem_Experience]') AND parent_object_id = OBJECT_ID('ExperienceItem'))
alter table ExperienceItem  drop constraint FK_ExperienceItem_Experience

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_GeneralInfo_Candidate]') AND parent_object_id = OBJECT_ID('GeneralInfo'))
alter table GeneralInfo  drop constraint FK_GeneralInfo_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_GeneralInfo_Citizenship]') AND parent_object_id = OBJECT_ID('GeneralInfo'))
alter table GeneralInfo  drop constraint FK_GeneralInfo_Citizenship

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_GeneralInfo_InsuredPersonType]') AND parent_object_id = OBJECT_ID('GeneralInfo'))
alter table GeneralInfo  drop constraint FK_GeneralInfo_InsuredPersonType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_GeneralInfo_DisabilityDegree]') AND parent_object_id = OBJECT_ID('GeneralInfo'))
alter table GeneralInfo  drop constraint FK_GeneralInfo_DisabilityDegree

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentComment_User]') AND parent_object_id = OBJECT_ID('AppointmentComment'))
alter table AppointmentComment  drop constraint FK_AppointmentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentComment_Appointment]') AND parent_object_id = OBJECT_ID('AppointmentComment'))
alter table AppointmentComment  drop constraint FK_AppointmentComment_Appointment

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_NameChange_GeneralInfo]') AND parent_object_id = OBJECT_ID('NameChange'))
alter table NameChange  drop constraint FK_NameChange_GeneralInfo

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ForeignLanguage_GeneralInfo]') AND parent_object_id = OBJECT_ID('ForeignLanguage'))
alter table ForeignLanguage  drop constraint FK_ForeignLanguage_GeneralInfo

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpVersion_User]') AND parent_object_id = OBJECT_ID('HelpVersion'))
alter table HelpVersion  drop constraint FK_HelpVersion_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReportComment_User]') AND parent_object_id = OBJECT_ID('AppointmentReportComment'))
alter table AppointmentReportComment  drop constraint FK_AppointmentReportComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReportComment_AppointmentReport]') AND parent_object_id = OBJECT_ID('AppointmentReportComment'))
alter table AppointmentReportComment  drop constraint FK_AppointmentReportComment_AppointmentReport

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentCreateManager2ToDepartment2_User]') AND parent_object_id = OBJECT_ID('AppointmentCreateManager2ToDepartment2'))
alter table AppointmentCreateManager2ToDepartment2  drop constraint FK_AppointmentCreateManager2ToDepartment2_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentCreateManager2ToDepartment2_Department]') AND parent_object_id = OBJECT_ID('AppointmentCreateManager2ToDepartment2'))
alter table AppointmentCreateManager2ToDepartment2  drop constraint FK_AppointmentCreateManager2ToDepartment2_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_VacationType]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_VacationType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Vacation_AdditionalVacationType]') AND parent_object_id = OBJECT_ID('Vacation'))
alter table Vacation  drop constraint FK_Vacation_AdditionalVacationType

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServiceType]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServiceType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServiceProductionTime]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServiceProductionTime

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServiceTransferMethod]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServiceTransferMethod

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_HelpServicePeriod]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_HelpServicePeriod

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_User]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServiceRequest_Consultant]') AND parent_object_id = OBJECT_ID('HelpServiceRequest'))
alter table HelpServiceRequest  drop constraint FK_HelpServiceRequest_Consultant

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_Candidate]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_Position]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_Position

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_Department]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_Schedule]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_Schedule

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_ApprovingManager]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_ApprovingManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_ApprovingHigherManager]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_ApprovingHigherManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Managers_RejectingChief]') AND parent_object_id = OBJECT_ID('Managers'))
alter table Managers  drop constraint FK_Managers_RejectingChief

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Passport_Candidate]') AND parent_object_id = OBJECT_ID('Passport'))
alter table Passport  drop constraint FK_Passport_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Passport_DocumentType]') AND parent_object_id = OBJECT_ID('Passport'))
alter table Passport  drop constraint FK_Passport_DocumentType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Training_Education]') AND parent_object_id = OBJECT_ID('Training'))
alter table Training  drop constraint FK_Training_Education

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_BackgroundCheck_Candidate]') AND parent_object_id = OBJECT_ID('BackgroundCheck'))
alter table BackgroundCheck  drop constraint FK_BackgroundCheck_Candidate

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_BackgroundCheck_Approver]') AND parent_object_id = OBJECT_ID('BackgroundCheck'))
alter table BackgroundCheck  drop constraint FK_BackgroundCheck_Approver

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager23ToDepartment3_User]') AND parent_object_id = OBJECT_ID('AppointmentManager23ToDepartment3'))
alter table AppointmentManager23ToDepartment3  drop constraint FK_AppointmentManager23ToDepartment3_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager23ToDepartment3_Department]') AND parent_object_id = OBJECT_ID('AppointmentManager23ToDepartment3'))
alter table AppointmentManager23ToDepartment3  drop constraint FK_AppointmentManager23ToDepartment3_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ManualRoleRecord_User]') AND parent_object_id = OBJECT_ID('[ManualRoleRecord]'))
alter table [ManualRoleRecord]  drop constraint FK_ManualRoleRecord_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ManualRoleRecord_ManualRole]') AND parent_object_id = OBJECT_ID('[ManualRoleRecord]'))
alter table [ManualRoleRecord]  drop constraint FK_ManualRoleRecord_ManualRole

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ManualRoleRecord_TargetUser]') AND parent_object_id = OBJECT_ID('[ManualRoleRecord]'))
alter table [ManualRoleRecord]  drop constraint FK_ManualRoleRecord_TargetUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ManualRoleRecord_TargetDepartment]') AND parent_object_id = OBJECT_ID('[ManualRoleRecord]'))
alter table [ManualRoleRecord]  drop constraint FK_ManualRoleRecord_TargetDepartment

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

if exists (select * from dbo.sysobjects where id = object_id(N'Certification') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Certification
if exists (select * from dbo.sysobjects where id = object_id(N'Family') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Family
if exists (select * from dbo.sysobjects where id = object_id(N'DocumentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DocumentType
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
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServicePeriod') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpServicePeriod
if exists (select * from dbo.sysobjects where id = object_id(N'Contractor') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Contractor
if exists (select * from dbo.sysobjects where id = object_id(N'PersonalAccountContractor') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PersonalAccountContractor
if exists (select * from dbo.sysobjects where id = object_id(N'Mission') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Mission
if exists (select * from dbo.sysobjects where id = object_id(N'Sicklist') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Sicklist
if exists (select * from dbo.sysobjects where id = object_id(N'RequestNextNumber') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestNextNumber
if exists (select * from dbo.sysobjects where id = object_id(N'Information') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Information
if exists (select * from dbo.sysobjects where id = object_id(N'Settings') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Settings
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistPaymentPercent') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistPaymentPercent
if exists (select * from dbo.sysobjects where id = object_id(N'SupplementaryAgreement') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SupplementaryAgreement
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionHistoryEntity') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpQuestionHistoryEntity
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionSubtype') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpQuestionSubtype
if exists (select * from dbo.sysobjects where id = object_id(N'Schedule') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Schedule
if exists (select * from dbo.sysobjects where id = object_id(N'PersonnelManagers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PersonnelManagers
if exists (select * from dbo.sysobjects where id = object_id(N'DeductionKind') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DeductionKind
if exists (select * from dbo.sysobjects where id = object_id(N'AcceptRequestDate') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AcceptRequestDate
if exists (select * from dbo.sysobjects where id = object_id(N'RequestPrintForm') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestPrintForm
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentHoursType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentHoursType
if exists (select * from dbo.sysobjects where id = object_id(N'Department') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Department
if exists (select * from dbo.sysobjects where id = object_id(N'EmployeeDocumentSubType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmployeeDocumentSubType
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpQuestionType
if exists (select * from dbo.sysobjects where id = object_id(N'Experience') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Experience
if exists (select * from dbo.sysobjects where id = object_id(N'AccountingTransaction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AccountingTransaction
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportComment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionOrderComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionOrderComment
if exists (select * from dbo.sysobjects where id = object_id(N'ChildVacationComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ChildVacationComment
if exists (select * from dbo.sysobjects where id = object_id(N'Contacts') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Contacts
if exists (select * from dbo.sysobjects where id = object_id(N'InsuredPersonType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table InsuredPersonType
if exists (select * from dbo.sysobjects where id = object_id(N'Deduction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Deduction
if exists (select * from dbo.sysobjects where id = object_id(N'Signer') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Signer
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceProductionTime') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpServiceProductionTime
if exists (select * from dbo.sysobjects where id = object_id(N'Reference') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Reference
if exists (select * from dbo.sysobjects where id = object_id(N'PostGraduateEducationDiploma') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table PostGraduateEducationDiploma
if exists (select * from dbo.sysobjects where id = object_id(N'Appointment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Appointment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportCostType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportCostType
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrectionComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrectionComment
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistPaymentRestrictType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistPaymentRestrictType
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpServiceType
if exists (select * from dbo.sysobjects where id = object_id(N'MilitaryService') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MilitaryService
if exists (select * from dbo.sysobjects where id = object_id(N'MissionPurchaseBookRecord') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionPurchaseBookRecord
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReport
if exists (select * from dbo.sysobjects where id = object_id(N'MissionGoal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionGoal
if exists (select * from dbo.sysobjects where id = object_id(N'ChiefToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ChiefToUser
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphic') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingGraphic
if exists (select * from dbo.sysobjects where id = object_id(N'DismissalComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DismissalComment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionComment
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistType
if exists (select * from dbo.sysobjects where id = object_id(N'RequestStatus') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RequestStatus
if exists (select * from dbo.sysobjects where id = object_id(N'ClearanceChecklistComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ClearanceChecklistComment
if exists (select * from dbo.sysobjects where id = object_id(N'HigherEducationDiploma') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HigherEducationDiploma
if exists (select * from dbo.sysobjects where id = object_id(N'OnsiteTraining') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OnsiteTraining
if exists (select * from dbo.sysobjects where id = object_id(N'MissionOrder') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionOrder
if exists (select * from dbo.sysobjects where id = object_id(N'MissionDailyAllowance') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionDailyAllowance
if exists (select * from dbo.sysobjects where id = object_id(N'TerraGraphic') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TerraGraphic
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentComment
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWorkType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWorkType
if exists (select * from dbo.sysobjects where id = object_id(N'DBVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DBVersion
if exists (select * from dbo.sysobjects where id = object_id(N'HelpFaq') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpFaq
if exists (select * from dbo.sysobjects where id = object_id(N'Education') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Education
if exists (select * from dbo.sysobjects where id = object_id(N'MissionPurchaseBookDocument') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionPurchaseBookDocument
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTarget') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTarget
if exists (select * from dbo.sysobjects where id = object_id(N'TerraPointToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TerraPointToUser
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphicType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingGraphicType
if exists (select * from dbo.sysobjects where id = object_id(N'InspectorToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table InspectorToUser
if exists (select * from dbo.sysobjects where id = object_id(N'DocumentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DocumentComment
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionRequest') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpQuestionRequest
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentCandidate') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentCandidate
if exists (select * from dbo.sysobjects where id = object_id(N'Country') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Country
if exists (select * from dbo.sysobjects where id = object_id(N'MissionDailyAllowanceGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionDailyAllowanceGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrection') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrection
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentAddition') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentAddition
if exists (select * from dbo.sysobjects where id = object_id(N'SicklistComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SicklistComment
if exists (select * from dbo.sysobjects where id = object_id(N'AbsenceType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AbsenceType
if exists (select * from dbo.sysobjects where id = object_id(N'[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Users]
if exists (select * from dbo.sysobjects where id = object_id(N'UserToPersonnel') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserToPersonnel
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceRequestComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpServiceRequestComment
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager2ToManager3') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentManager2ToManager3
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTrainTicketTypeGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTrainTicketTypeGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionCountry') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionCountry
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphicTypeToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingGraphicTypeToUser
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWorkComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWorkComment
if exists (select * from dbo.sysobjects where id = object_id(N'Absence') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Absence
if exists (select * from dbo.sysobjects where id = object_id(N'VacationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VacationType
if exists (select * from dbo.sysobjects where id = object_id(N'ExportImportAction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ExportImportAction
if exists (select * from dbo.sysobjects where id = object_id(N'FamilyMember') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table FamilyMember
if exists (select * from dbo.sysobjects where id = object_id(N'ExperienceItem') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ExperienceItem
if exists (select * from dbo.sysobjects where id = object_id(N'GeneralInfo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table GeneralInfo
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentComment
if exists (select * from dbo.sysobjects where id = object_id(N'[ManualRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [ManualRole]
if exists (select * from dbo.sysobjects where id = object_id(N'[ClearanceChecklistRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [ClearanceChecklistRole]
if exists (select * from dbo.sysobjects where id = object_id(N'ChildVacation') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ChildVacation
if exists (select * from dbo.sysobjects where id = object_id(N'Employment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Employment
if exists (select * from dbo.sysobjects where id = object_id(N'EmploymentType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EmploymentType
if exists (select * from dbo.sysobjects where id = object_id(N'MissionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionType
if exists (select * from dbo.sysobjects where id = object_id(N'Position') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Position
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetDay') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetDay
if exists (select * from dbo.sysobjects where id = object_id(N'DisabilityDegree') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table DisabilityDegree
if exists (select * from dbo.sysobjects where id = object_id(N'AccessGroup') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AccessGroup
if exists (select * from dbo.sysobjects where id = object_id(N'NameChange') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table NameChange
if exists (select * from dbo.sysobjects where id = object_id(N'ForeignLanguage') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ForeignLanguage
if exists (select * from dbo.sysobjects where id = object_id(N'[ClearanceChecklistRoleRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [ClearanceChecklistRoleRecord]
if exists (select * from dbo.sysobjects where id = object_id(N'ClearanceChecklistApproval') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table ClearanceChecklistApproval
if exists (select * from dbo.sysobjects where id = object_id(N'MissionGraid') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionGraid
if exists (select * from dbo.sysobjects where id = object_id(N'MissionResidence') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionResidence
if exists (select * from dbo.sysobjects where id = object_id(N'Role') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Role
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceTransferMethod') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpServiceTransferMethod
if exists (select * from dbo.sysobjects where id = object_id(N'HelpVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpVersion
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReportComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentReportComment
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentCreateManager2ToDepartment2') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentCreateManager2ToDepartment2
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingCalendar') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table WorkingCalendar
if exists (select * from dbo.sysobjects where id = object_id(N'TimesheetCorrectionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table TimesheetCorrectionType
if exists (select * from dbo.sysobjects where id = object_id(N'Vacation') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Vacation
if exists (select * from dbo.sysobjects where id = object_id(N'Timesheet') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Timesheet
if exists (select * from dbo.sysobjects where id = object_id(N'UserLogin') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table UserLogin
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceRequest') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HelpServiceRequest
if exists (select * from dbo.sysobjects where id = object_id(N'AdditionalVacationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AdditionalVacationType
if exists (select * from dbo.sysobjects where id = object_id(N'Managers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Managers
if exists (select * from dbo.sysobjects where id = object_id(N'Passport') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Passport
if exists (select * from dbo.sysobjects where id = object_id(N'Training') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Training
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReason') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentReason
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
if exists (select * from dbo.sysobjects where id = object_id(N'BackgroundCheck') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table BackgroundCheck
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager23ToDepartment3') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentManager23ToDepartment3
if exists (select * from dbo.sysobjects where id = object_id(N'[ManualRoleRecord]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [ManualRoleRecord]
if exists (select * from dbo.sysobjects where id = object_id(N'HolidayWork') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HolidayWork
if exists (select * from dbo.sysobjects where id = object_id(N'Attachment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Attachment
if exists (select * from dbo.sysobjects where id = object_id(N'Document') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Document

create table Certification (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CertificationDate DATETIME null,
  CertificateNumber NVARCHAR(20) null,
  CertificateDateOfIssue DATETIME null,
  InitiatingOrder NVARCHAR(20) null,
  EducationId INT null,
  constraint PK_Certification  primary key (Id)
)
create table Family (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  Cohabitants NVARCHAR(250) null,
  IsFinal BIT not null,
  constraint PK_Family  primary key (Id)
)
create table DocumentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_DocumentType  primary key (Id)
)
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
  IsColloquyPassed BIT null,
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
  IsHoliday BIT not null,
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
  IsOriginalReceived BIT not null,
  IsPersonnelFileSentToArchive BIT not null,
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
create table HelpServicePeriod (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  TypeId INT not null,
  SortOrder INT not null,
  PeriodMonth INT not null,
  constraint PK_HelpServicePeriod  primary key (Id)
)
create table Contractor (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(255) not null,
  Account NVARCHAR(255) not null,
  Code NVARCHAR(16) null,
  constraint PK_Contractor  primary key (Id)
)
create table PersonalAccountContractor (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_PersonalAccountContractor  primary key (Id)
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
  IsAdditionalOrderExists BIT not null,
  AdditionalOrderRecalculateDate DATETIME null,
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
create table SupplementaryAgreement (
 Id INT IDENTITY NOT NULL,
  CreateDate DATETIME null,
  Number INT null,
  OrderCreateDate DATETIME null,
  OrderNumber INT null,
  PersonnelManagersId INT not null,
  constraint PK_SupplementaryAgreement  primary key (Id)
)
create table HelpQuestionHistoryEntity (
 Id INT IDENTITY NOT NULL,
  CreateDate DATETIME not null,
  SendDate DATETIME null,
  BeginWorkDate DATETIME null,
  EndWorkDate DATETIME null,
  Type INT not null,
  HelpQuestionRequestId INT not null,
  Question NVARCHAR(MAX) null,
  Answer NVARCHAR(MAX) null,
  CreatorId INT not null,
  ConsultantId INT null,
  CreatorRoleId INT not null,
  RecipientRoleId INT null,
  constraint PK_HelpQuestionHistoryEntity  primary key (Id)
)
create table HelpQuestionSubtype (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  TypeId INT not null,
  constraint PK_HelpQuestionSubtype  primary key (Id)
)
create table Schedule (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Schedule  primary key (Id)
)
create table PersonnelManagers (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  EmploymentOrderDate DATETIME null,
  EmploymentOrderNumber NVARCHAR(20) null,
  EmploymentDate DATETIME null,
  ContractDate DATETIME null,
  ContractEndDate DATETIME null,
  ContractNumber NVARCHAR(20) null,
  NorthernAreaAddition DECIMAL(19, 2) null,
  AreaMultiplier DECIMAL(19, 2) null,
  AreaAddition DECIMAL(19, 2) null,
  TravelRelatedAddition DECIMAL(15, 2) null,
  CompetenceAddition DECIMAL(15, 2) null,
  FrontOfficeExperienceAddition DECIMAL(15, 2) null,
  OverallExperienceYears INT null,
  OverallExperienceMonths INT null,
  OverallExperienceDays INT null,
  InsurableExperienceYears INT null,
  InsurableExperienceMonths INT null,
  InsurableExperienceDays INT null,
  PersonalAccount NVARCHAR(23) null,
  PersonalAccountContractorId INT null,
  AccessGroupId INT null,
  SignerId INT null,
  ApprovedByPersonnelManagerId INT null,
  constraint PK_PersonnelManagers  primary key (Id)
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
  Context VARBINARY(MAX) null,
  RequestId INT not null,
  RequestTypeId INT not null,
  FilePath NVARCHAR(1024) null,
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
create table HelpQuestionType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpQuestionType  primary key (Id)
)
create table Experience (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  WorkBookSeries NVARCHAR(20) null,
  WorkBookNumber NVARCHAR(20) null,
  WorkBookDateOfIssue DATETIME null,
  WorkBookSupplementSeries NVARCHAR(20) null,
  WorkBookSupplementNumber NVARCHAR(20) null,
  WorkBookSupplementDateOfIssue DATETIME null,
  IsFinal BIT not null,
  constraint PK_Experience  primary key (Id)
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
create table Contacts (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  ZipCode NVARCHAR(6) null,
  Region NVARCHAR(50) null,
  District NVARCHAR(50) null,
  City NVARCHAR(50) null,
  Street NVARCHAR(50) null,
  StreetNumber NVARCHAR(10) null,
  Building NVARCHAR(10) null,
  Apartment NVARCHAR(10) null,
  WorkPhone NVARCHAR(10) null,
  HomePhone NVARCHAR(10) null,
  Mobile NVARCHAR(10) null,
  Email NVARCHAR(50) null,
  IsFinal BIT not null,
  constraint PK_Contacts  primary key (Id)
)
create table InsuredPersonType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_InsuredPersonType  primary key (Id)
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
create table Signer (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(50) null,
  PreamblePartyTemplate NVARCHAR(500) null,
  constraint PK_Signer  primary key (Id)
)
create table HelpServiceProductionTime (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpServiceProductionTime  primary key (Id)
)
create table Reference (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LastName NVARCHAR(50) null,
  FirstName NVARCHAR(50) null,
  Patronymic NVARCHAR(50) null,
  WorksAt NVARCHAR(50) null,
  Position NVARCHAR(50) null,
  Phone NVARCHAR(10) null,
  Relation NVARCHAR(50) null,
  BackgroundCheckId INT null,
  constraint PK_Reference  primary key (Id)
)
create table PostGraduateEducationDiploma (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IssuedBy NVARCHAR(150) null,
  Series NVARCHAR(10) null,
  Number NVARCHAR(10) null,
  AdmissionYear NVARCHAR(4) null,
  GraduationYear NVARCHAR(4) null,
  Speciality NVARCHAR(50) null,
  EducationId INT null,
  constraint PK_PostGraduateEducationDiploma  primary key (Id)
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
  StaffCreatorId INT null,
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
create table HelpServiceType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  IsPeriodAvailable BIT not null,
  IsRequirementsAvailable BIT not null,
  IsAttachmentAvailable BIT not null,
  constraint PK_HelpServiceType  primary key (Id)
)
create table MilitaryService (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  IsLiableForMilitaryService BIT not null,
  MilitaryCardNumber NVARCHAR(20) null,
  MilitaryCardDate DATETIME null,
  ReserveCategory NVARCHAR(20) null,
  RankId INT null,
  SpecialityCategory NVARCHAR(50) null,
  MilitarySpecialityCode NVARCHAR(7) null,
  CombatFitness NVARCHAR(20) null,
  Commissariat NVARCHAR(100) null,
  MilitaryServiceRegistrationInfo NVARCHAR(250) null,
  CommonMilitaryServiceRegistrationInfo NVARCHAR(250) null,
  SpecialMilitaryServiceRegistrationInfo NVARCHAR(250) null,
  RegistrationExpirationId INT null,
  IsReserved BIT not null,
  MobilizationTicketNumber NVARCHAR(20) null,
  PersonnelCategoryId INT null,
  PersonnelTypeId INT null,
  IsAssigned BIT not null,
  ConscriptionStatusId INT null,
  IsFinal BIT not null,
  constraint PK_MilitaryService  primary key (Id)
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
  SumNds DECIMAL(19,5) null,
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
  IsDocumentsSaveToArchive BIT not null,
  ArchiveDate DATETIME null,
  ArchiveNumber NVARCHAR(128) null,
  Archivist INT null,
  MissionOrderId INT null,
  AdditionalMissionOrderId INT null,
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
create table ClearanceChecklistComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  ClearanceChecklistId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_ClearanceChecklistComment  primary key (Id)
)
create table HigherEducationDiploma (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  IssuedBy NVARCHAR(150) null,
  Series NVARCHAR(10) null,
  Number NVARCHAR(10) null,
  AdmissionYear NVARCHAR(4) null,
  GraduationYear NVARCHAR(4) null,
  Qualification NVARCHAR(50) null,
  Speciality NVARCHAR(50) null,
  Profession NVARCHAR(50) null,
  Department NVARCHAR(50) null,
  EducationId INT null,
  constraint PK_HigherEducationDiploma  primary key (Id)
)
create table OnsiteTraining (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  Type NVARCHAR(200) null,
  Description NVARCHAR(200) null,
  BeginningDate DATETIME null,
  EndDate DATETIME null,
  IsComplete BIT null,
  ReasonsForIncompleteTraining NVARCHAR(200) null,
  Results NVARCHAR(200) null,
  Comments NVARCHAR(200) null,
  ApproverId INT null,
  IsFinal BIT not null,
  constraint PK_OnsiteTraining  primary key (Id)
)
create table MissionOrder (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  BeginDate DATETIME null,
  EndDate DATETIME null,
  Number INT not null,
  IsAdditional BIT null,
  IsRecalculated BIT null,
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
  LongTermReason NVARCHAR(128) null,
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
  MainOrderId INT null,
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
  Hours DECIMAL(19,5) null,
  PointId INT null,
  FactHours DECIMAL(19,5) null,
  FactPointId INT null,
  IsCreditAvailable BIT null,
  IsFactCreditAvailable BIT null,
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
create table HelpFaq (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  DateCreated DATETIME not null,
  Question NVARCHAR(MAX) not null,
  Answer NVARCHAR(MAX) not null,
  constraint PK_HelpFaq  primary key (Id)
)
create table Education (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  IsFinal BIT not null,
  constraint PK_Education  primary key (Id)
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
create table HelpQuestionRequest (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  SendDate DATETIME null,
  BeginWorkDate DATETIME null,
  EndWorkDate DATETIME null,
  ConfirmWorkDate DATETIME null,
  Number INT not null,
  TypeId INT not null,
  SubtypeId INT not null,
  Question NVARCHAR(MAX) null,
  Answer NVARCHAR(MAX) null,
  UserId INT not null,
  CreatorId INT not null,
  CreatorRoleId INT not null,
  ConsultantOutsourcingId INT null,
  ConsultantPersonnelId INT null,
  ConsultantAccountantId INT null,
  ConsultantRoleId INT null,
  constraint PK_HelpQuestionRequest  primary key (Id)
)
create table EmploymentCandidate (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT null,
  GeneralInfoId INT null,
  PassportId INT null,
  EducationId INT null,
  FamilyId INT null,
  MilitaryServiceId INT null,
  ExperienceId INT null,
  ContactsId INT null,
  BackgroundCheckId INT null,
  OnsiteTrainingId INT null,
  ManagersId INT null,
  PersonnelManagersId INT null,
  Status INT null,
  QuestionnaireDate DATETIME null,
  AppointmentCreatorId INT null,
  constraint PK_EmploymentCandidate  primary key (Id)
)
create table Country (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Country  primary key (Id)
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
  Address NVARCHAR(256) null,
  RoleId INT not null,
  LoginAd NVARCHAR(32) null,
  Rate DECIMAL(19,5) null,
  GivesCredit BIT not null,
  Level INT null,
  Grade INT null,
  ExperienceIn1C BIT null,
  IsFixedTermContract BIT null,
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
create table HelpServiceRequestComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  HelpServiceRequestId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_HelpServiceRequestComment  primary key (Id)
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
create table FamilyMember (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  RelationshipId INT null,
  Name NVARCHAR(50) null,
  PassportData NVARCHAR(150) null,
  DateOfBirth DATETIME null,
  PlaceOfBirth NVARCHAR(150) null,
  WorksAt NVARCHAR(50) null,
  Contacts NVARCHAR(100) null,
  FamilyId INT null,
  constraint PK_FamilyMember  primary key (Id)
)
create table ExperienceItem (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  BeginningDate DATETIME null,
  EndDate DATETIME null,
  Company NVARCHAR(50) null,
  Position NVARCHAR(50) null,
  CompanyContacts NVARCHAR(250) null,
  ExperienceId INT null,
  constraint PK_ExperienceItem  primary key (Id)
)
create table GeneralInfo (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  LastName NVARCHAR(50) null,
  FirstName NVARCHAR(50) null,
  Patronymic NVARCHAR(50) null,
  IsPatronymicAbsent BIT not null,
  IsMale BIT null,
  CitizenshipId INT null,
  InsuredPersonTypeId INT null,
  DateOfBirth DATETIME null,
  RegionOfBirth NVARCHAR(50) null,
  DistrictOfBirth NVARCHAR(50) null,
  CityOfBirth NVARCHAR(50) null,
  INN NVARCHAR(12) null,
  SNILS NVARCHAR(14) null,
  DisabilityCertificateSeries NVARCHAR(20) null,
  DisabilityCertificateNumber NVARCHAR(20) null,
  DisabilityCertificateDateOfIssue DATETIME null,
  DisabilityDegreeId INT null,
  DisabilityCertificateExpirationDate DATETIME null,
  Status INT null,
  AgreedToPersonalDataProcessing BIT not null,
  IsFinal BIT not null,
  constraint PK_GeneralInfo  primary key (Id)
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
create table [ManualRole] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(32) null,
  Description NVARCHAR(256) null,
  DaysForApproval INT null,
  constraint PK_ManualRole primary key (Id)
)
create table [ClearanceChecklistRole] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(32) null,
  Description NVARCHAR(256) null,
  DaysForApproval INT null,
  DeleteDate DATETIME null,
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
create table DisabilityDegree (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_DisabilityDegree  primary key (Id)
)
create table AccessGroup (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_AccessGroup  primary key (Id)
)
create table NameChange (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  PreviousName NVARCHAR(50) null,
  Date DATETIME null,
  Place NVARCHAR(50) null,
  Reason NVARCHAR(50) null,
  GeneralInfoId INT null,
  constraint PK_NameChange  primary key (Id)
)
create table ForeignLanguage (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  LanguageName NVARCHAR(50) null,
  Level NVARCHAR(20) null,
  GeneralInfoId INT null,
  constraint PK_ForeignLanguage  primary key (Id)
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
create table HelpServiceTransferMethod (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpServiceTransferMethod  primary key (Id)
)
create table HelpVersion (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  ReleaseDate DATETIME not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(MAX) not null,
  constraint PK_HelpVersion  primary key (Id)
)
create table AppointmentReportComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  AppointmentReportId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_AppointmentReportComment  primary key (Id)
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
  AdditionalVacationBeginDate DATETIME null,
  DaysCount INT not null,
  AdditionalVacationDaysCount INT not null,
  Number INT not null,
  TypeId INT not null,
  AdditionalVacationTypeId INT null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  ManagerDateAccept DATETIME null,
  PersonnelManagerDateAccept DATETIME null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  IsOriginalReceived BIT not null,
  PrincipalVacationDaysLeft DECIMAL(19, 2) null,
  AdditionalVacationDaysLeft DECIMAL(19, 2) null,
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
create table HelpServiceRequest (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  SendDate DATETIME null,
  BeginWorkDate DATETIME null,
  EndWorkDate DATETIME null,
  ConfirmWorkDate DATETIME null,
  RejectWorkDate DATETIME null,
  Number INT not null,
  Address NVARCHAR(255) null,
  TypeId INT not null,
  ProductionTimeId INT not null,
  TransferMethodId INT not null,
  PeriodId INT null,
  Requirements NVARCHAR(256) null,
  UserId INT not null,
  CreatorId INT not null,
  ConsultantId INT null,
  constraint PK_HelpServiceRequest  primary key (Id)
)
create table AdditionalVacationType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  constraint PK_AdditionalVacationType  primary key (Id)
)
create table Managers (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  PositionId INT null,
  DepartmentId INT null,
  EmploymentConditions NVARCHAR(250) null,
  ScheduleId INT null,
  ProbationaryPeriod NVARCHAR(50) null,
  DailySalaryBasis DECIMAL(15, 2) null,
  HourlySalaryBasis DECIMAL(15, 2) null,
  SalaryMultiplier DECIMAL(3, 2) null,
  WorkCity NVARCHAR(100) null,
  PersonalAddition DECIMAL(19, 2) null,
  PositionAddition DECIMAL(19, 2) null,
  IsFront BIT not null,
  Bonus DECIMAL(19, 2) null,
  IsLiable BIT not null,
  RequestNumber NVARCHAR(50) null,
  ManagerApprovalStatus BIT null,
  ApprovingManagerId INT null,
  ManagerApprovalDate DATETIME null,
  ManagerRejectionReason NVARCHAR(50) null,
  HigherManagerApprovalStatus BIT null,
  ApprovingHigherManagerId INT null,
  HigherManagerApprovalDate DATETIME null,
  HigherManagerRejectionReason NVARCHAR(50) null,
  RejectingChiefId INT null,
  ChiefRejectionReason NVARCHAR(50) null,
  constraint PK_Managers  primary key (Id)
)
create table Passport (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  DocumentTypeId INT null,
  InternalPassportSeries NVARCHAR(4) null,
  InternalPassportNumber NVARCHAR(6) null,
  InternalPassportDateOfIssue DATETIME null,
  InternalPassportIssuedBy NVARCHAR(250) null,
  InternalPassportSubdivisionCode NVARCHAR(15) null,
  RegistrationDate DATETIME null,
  ZipCode NVARCHAR(6) null,
  Region NVARCHAR(50) null,
  District NVARCHAR(50) null,
  City NVARCHAR(50) null,
  Street NVARCHAR(50) null,
  StreetNumber NVARCHAR(6) null,
  Building NVARCHAR(3) null,
  Apartment NVARCHAR(5) null,
  InternationalPassportSeries NVARCHAR(10) null,
  InternationalPassportNumber NVARCHAR(10) null,
  InternationalPassportDateOfIssue DATETIME null,
  InternationalPassportIssuedBy NVARCHAR(150) null,
  IsFinal BIT not null,
  constraint PK_Passport  primary key (Id)
)
create table Training (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CertificateIssuedBy NVARCHAR(50) null,
  Series NVARCHAR(10) null,
  Number NVARCHAR(10) null,
  BeginningDate DATETIME null,
  EndDate DATETIME null,
  Speciality NVARCHAR(50) null,
  EducationId INT null,
  constraint PK_Training  primary key (Id)
)
create table AppointmentReason (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_AppointmentReason  primary key (Id)
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
  Context VARBINARY(MAX) null,
  RequestId INT not null,
  RequestType INT not null,
  DateCreated DATETIME not null,
  Description NVARCHAR(256) null,
  CreatorRoleId INT not null,
  FilePath NVARCHAR(1024) null,
  constraint PK_RequestAttachment  primary key (Id)
)
create table EmployeeDocumentType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(100) not null,
  constraint PK_EmployeeDocumentType  primary key (Id)
)
create table BackgroundCheck (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CandidateId INT not null,
  AverageSalary DECIMAL(19, 2) null,
  Liabilities NVARCHAR(250) null,
  PreviousDismissalReason NVARCHAR(250) null,
  PreviousSuperior NVARCHAR(250) null,
  PositionSought NVARCHAR(50) null,
  MilitaryOperationsExperience NVARCHAR(50) null,
  DriversLicenseNumber NVARCHAR(12) null,
  DriversLicenseDateOfIssue DATETIME null,
  DriversLicenseCategories NVARCHAR(255) null,
  DrivingExperience INT null,
  AutomobileMake NVARCHAR(50) null,
  AutomobileLicensePlateNumber NVARCHAR(15) null,
  IsReadyForBusinessTrips BIT not null,
  Sports NVARCHAR(250) null,
  Hobbies NVARCHAR(250) null,
  ImportantEvents NVARCHAR(250) null,
  ChronicalDiseases NVARCHAR(250) null,
  Penalties NVARCHAR(250) null,
  PsychiatricAndAddictionTreatment NVARCHAR(250) null,
  Smoking NVARCHAR(250) null,
  Drinking NVARCHAR(250) null,
  IsApprovalSkipped BIT not null,
  ApprovalStatus BIT null,
  ApproverId INT null,
  IsFinal BIT not null,
  constraint PK_BackgroundCheck  primary key (Id)
)
create table AppointmentManager23ToDepartment3 (
 Id INT IDENTITY NOT NULL,
  ManagerId INT not null,
  DepartmentId INT not null,
  constraint PK_AppointmentManager23ToDepartment3  primary key (Id)
)
create table [ManualRoleRecord] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  RoleId INT not null,
  TargetUserId INT null,
  TargetDepartmentId INT null,
  constraint PK_ManualRoleRecord primary key (Id)
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
alter table Certification add constraint FK_Certification_Education foreign key (EducationId) references Education
create index Family_Candidate on Family (CandidateId)
alter table Family add constraint FK_Family_Candidate foreign key (CandidateId) references EmploymentCandidate
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
create index HelpServicePeriod_Type on HelpServicePeriod (TypeId)
alter table HelpServicePeriod add constraint FK_HelpServicePeriod_Type foreign key (TypeId) references HelpServiceType
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
create index IX_SupplementaryAgreement_PersonnelManagers on SupplementaryAgreement (PersonnelManagersId)
alter table SupplementaryAgreement add constraint FK_SupplementaryAgreement_PersonnelManagers foreign key (PersonnelManagersId) references PersonnelManagers
create index IX_HelpQuestionRequestHistoryEntity_HelpQuestionRequest on HelpQuestionHistoryEntity (HelpQuestionRequestId)
create index IX_HelpQuestionHistoryEntity_CreatorUser_Id on HelpQuestionHistoryEntity (CreatorId)
create index IX_HelpQuestionHistoryEntity_Consultant_Id on HelpQuestionHistoryEntity (ConsultantId)
alter table HelpQuestionHistoryEntity add constraint FK_HelpQuestionRequestHistoryEntity_HelpQuestionRequest foreign key (HelpQuestionRequestId) references HelpQuestionRequest
alter table HelpQuestionHistoryEntity add constraint FK_HelpQuestionHistoryEntity_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpQuestionHistoryEntity add constraint FK_HelpQuestionHistoryEntity_Consultant foreign key (ConsultantId) references [Users]
create index HelpQuestionSubtype_HelpQuestionType on HelpQuestionSubtype (TypeId)
alter table HelpQuestionSubtype add constraint FK_HelpQuestionSubtype_HelpQuestionType foreign key (TypeId) references HelpQuestionType
create index PersonnelManagers_Candidate on PersonnelManagers (CandidateId)
create index PersonnelManagers_PersonalAccountContractor on PersonnelManagers (PersonalAccountContractorId)
create index PersonnelManagers_AccessGroup on PersonnelManagers (AccessGroupId)
create index PersonnelManagers_Signer on PersonnelManagers (SignerId)
create index IX_PersonnelManagers_ApprovedByPersonnelManagerUser_Id on PersonnelManagers (ApprovedByPersonnelManagerId)
alter table PersonnelManagers add constraint FK_PersonnelManagers_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table PersonnelManagers add constraint FK_PersonnelManagers_PersonalAccountContractor foreign key (PersonalAccountContractorId) references PersonalAccountContractor
alter table PersonnelManagers add constraint FK_PersonnelManagers_AccessGroup foreign key (AccessGroupId) references AccessGroup
alter table PersonnelManagers add constraint FK_PersonnelManagers_Signer foreign key (SignerId) references Signer
alter table PersonnelManagers add constraint FK_PersonnelManagers_ApprovedByPersonnelManagerUser foreign key (ApprovedByPersonnelManagerId) references [Users]
create index IX_AcceptRequestDate_User_Id on AcceptRequestDate (UserId)
alter table AcceptRequestDate add constraint FK_AcceptRequestDate_User foreign key (UserId) references [Users]
create index IX_DocumentSubType_EmployeeDocumentType_Id on EmployeeDocumentSubType (TypeId)
alter table EmployeeDocumentSubType add constraint FK_DocumentSubType_EmployeeDocumentType foreign key (TypeId) references EmployeeDocumentType
create index Experience_Candidate on Experience (CandidateId)
alter table Experience add constraint FK_Experience_Candidate foreign key (CandidateId) references EmploymentCandidate
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
create index Contacts_Candidate on Contacts (CandidateId)
alter table Contacts add constraint FK_Contacts_Candidate foreign key (CandidateId) references EmploymentCandidate
create index Deduction_DeductionType on Deduction (TypeId)
create index Deduction_DeductionKind on Deduction (KindId)
create index IX_Deduction_User_Id on Deduction (UserId)
create index IX_Deduction_EditorUser_Id on Deduction (CreatorId)
alter table Deduction add constraint FK_Deduction_DeductionType foreign key (TypeId) references DeductionType
alter table Deduction add constraint FK_Deduction_DeductionKind foreign key (KindId) references DeductionKind
alter table Deduction add constraint FK_Deduction_User foreign key (UserId) references [Users]
alter table Deduction add constraint FK_Deduction_EditorUser foreign key (CreatorId) references [Users]
alter table Reference add constraint FK_Reference_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table PostGraduateEducationDiploma add constraint FK_PostGraduateEducationDiploma_Education foreign key (EducationId) references Education
create index Appointment_Department on Appointment (DepartmentId)
create index Appointment_AppointmentReason on Appointment (ReasonId)
create index IX_Appointment_CreatorUser_Id on Appointment (CreatorId)
create index IX_Appointment_StaffCreatorUser_Id on Appointment (StaffCreatorId)
create index IX_Appointment_AcceptManager on Appointment (AcceptManagerId)
create index IX_Appointment_AcceptChief on Appointment (AcceptChiefId)
create index IX_Appointment_StaffUser on Appointment (AcceptStaffId)
create index IX_Appointment_DeleteUser on Appointment (DeleteUserId)
alter table Appointment add constraint FK_Appointment_Department foreign key (DepartmentId) references Department
alter table Appointment add constraint FK_Appointment_AppointmentReason foreign key (ReasonId) references AppointmentReason
alter table Appointment add constraint FK_Appointment_CreatorUser foreign key (CreatorId) references [Users]
alter table Appointment add constraint FK_Appointment_StaffCreatorUser foreign key (StaffCreatorId) references [Users]
alter table Appointment add constraint FK_Appointment_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table Appointment add constraint FK_Appointment_AcceptChief foreign key (AcceptChiefId) references [Users]
alter table Appointment add constraint FK_Appointment_StaffUser foreign key (AcceptStaffId) references [Users]
alter table Appointment add constraint FK_Appointment_DeleteUser foreign key (DeleteUserId) references [Users]
create index IX_TimesheetCorrectionComment_User_Id on TimesheetCorrectionComment (UserId)
create index IX_TimesheetCorrectionComment_TimesheetCorrection_Id on TimesheetCorrectionComment (TimesheetCorrectionId)
alter table TimesheetCorrectionComment add constraint FK_TimesheetCorrectionComment_User foreign key (UserId) references [Users]
alter table TimesheetCorrectionComment add constraint FK_TimesheetCorrectionComment_TimesheetCorrection foreign key (TimesheetCorrectionId) references TimesheetCorrection
create index MilitaryService_Candidate on MilitaryService (CandidateId)
alter table MilitaryService add constraint FK_MilitaryService_Candidate foreign key (CandidateId) references EmploymentCandidate
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
create index IX_MissionReport_Archivist on MissionReport (Archivist)
create index IX_MissionReport_MissionOrder on MissionReport (MissionOrderId)
create index IX_MissionReport_AdditionalMissionOrder on MissionReport (AdditionalMissionOrderId)
alter table MissionReport add constraint FK_MissionReport_User foreign key (UserId) references [Users]
alter table MissionReport add constraint FK_MissionReport_CreatorUser foreign key (CreatorId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptUser foreign key (AcceptUserId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptAccountant foreign key (AcceptAccountant) references [Users]
alter table MissionReport add constraint FK_MissionReport_Archivist foreign key (Archivist) references [Users]
alter table MissionReport add constraint FK_MissionReport_MissionOrder foreign key (MissionOrderId) references MissionOrder
alter table MissionReport add constraint FK_MissionReport_AdditionalMissionOrder foreign key (AdditionalMissionOrderId) references MissionOrder
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
create index IX_ClearanceChecklistComment_User_Id on ClearanceChecklistComment (UserId)
create index IX_ClearanceChecklistComment_ClearanceChecklist_Id on ClearanceChecklistComment (ClearanceChecklistId)
alter table ClearanceChecklistComment add constraint FK_ClearanceChecklistComment_User foreign key (UserId) references [Users]
alter table ClearanceChecklistComment add constraint FK_ClearanceChecklistComment_ClearanceChecklist foreign key (ClearanceChecklistId) references Dismissal
alter table HigherEducationDiploma add constraint FK_HigherEducationDiploma_Education foreign key (EducationId) references Education
create index OnsiteTraining_Candidate on OnsiteTraining (CandidateId)
create index OnsiteTraining_Approver on OnsiteTraining (ApproverId)
alter table OnsiteTraining add constraint FK_OnsiteTraining_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table OnsiteTraining add constraint FK_OnsiteTraining_Approver foreign key (ApproverId) references [Users]
create index MissionOrder_MissionType on MissionOrder (TypeId)
create index MissionOrder_MissionGoal on MissionOrder (MissionGoalId)
create index IX_MissionOrder_Secretary_Id on MissionOrder (SecretaryId)
create index IX_MissionOrder_User_Id on MissionOrder (UserId)
create index IX_MissionOrder_CreatorUser_Id on MissionOrder (CreatorId)
create index IX_MissionOrder_AcceptUser on MissionOrder (AcceptUserId)
create index IX_MissionOrder_AcceptManager on MissionOrder (AcceptManagerId)
create index IX_MissionOrder_AcceptChief on MissionOrder (AcceptChiefId)
create index IX_MissionOrder_Mission on MissionOrder (MissionId)
create index IX_MissionOrder_MainMissionOrder on MissionOrder (MainOrderId)
alter table MissionOrder add constraint FK_MissionOrder_MissionType foreign key (TypeId) references MissionType
alter table MissionOrder add constraint FK_MissionOrder_MissionGoal foreign key (MissionGoalId) references MissionGoal
alter table MissionOrder add constraint FK_MissionOrder_Secretary foreign key (SecretaryId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_User foreign key (UserId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_CreatorUser foreign key (CreatorId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptUser foreign key (AcceptUserId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptChief foreign key (AcceptChiefId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_Mission foreign key (MissionId) references Mission
alter table MissionOrder add constraint FK_MissionOrder_MainMissionOrder foreign key (MainOrderId) references MissionOrder
create index IX_EmploymentComment_User_Id on EmploymentComment (UserId)
create index IX_EmploymentComment_Employment_Id on EmploymentComment (EmploymentId)
alter table EmploymentComment add constraint FK_EmploymentComment_User foreign key (UserId) references [Users]
alter table EmploymentComment add constraint FK_EmploymentComment_Employment foreign key (EmploymentId) references Employment
create index IX_HelpFaq_User_Id on HelpFaq (UserId)
alter table HelpFaq add constraint FK_HelpFaq_User foreign key (UserId) references [Users]
create index Education_Candidate on Education (CandidateId)
alter table Education add constraint FK_Education_Candidate foreign key (CandidateId) references EmploymentCandidate
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
create index HelpQuestionRequest_HelpQuestionType on HelpQuestionRequest (TypeId)
create index HelpQuestionRequest_HelpQuestionSubtype on HelpQuestionRequest (SubtypeId)
create index IX_HelpQuestionRequest_User_Id on HelpQuestionRequest (UserId)
create index IX_HelpQuestionRequest_CreatorUser_Id on HelpQuestionRequest (CreatorId)
create index IX_HelpQuestionRequest_ConsultantOutsourcing_Id on HelpQuestionRequest (ConsultantOutsourcingId)
create index IX_HelpQuestionRequest_ConsultantPersonnel_Id on HelpQuestionRequest (ConsultantPersonnelId)
create index IX_HelpQuestionRequest_ConsultantAccountant_Id on HelpQuestionRequest (ConsultantAccountantId)
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_HelpQuestionType foreign key (TypeId) references HelpQuestionType
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_HelpQuestionSubtype foreign key (SubtypeId) references HelpQuestionSubtype
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_User foreign key (UserId) references [Users]
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_ConsultantOutsourcing foreign key (ConsultantOutsourcingId) references [Users]
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_ConsultantPersonnel foreign key (ConsultantPersonnelId) references [Users]
alter table HelpQuestionRequest add constraint FK_HelpQuestionRequest_ConsultantAccountant foreign key (ConsultantAccountantId) references [Users]
create index Candidate_User on EmploymentCandidate (UserId)
create index Candidate_GeneralInfo on EmploymentCandidate (GeneralInfoId)
create index Candidate_Passport on EmploymentCandidate (PassportId)
create index Candidate_Education on EmploymentCandidate (EducationId)
create index Candidate_Family on EmploymentCandidate (FamilyId)
create index Candidate_MilitaryService on EmploymentCandidate (MilitaryServiceId)
create index Candidate_Experience on EmploymentCandidate (ExperienceId)
create index Candidate_Contacts on EmploymentCandidate (ContactsId)
create index Candidate_BackgroundCheck on EmploymentCandidate (BackgroundCheckId)
create index Candidate_OnsiteTraining on EmploymentCandidate (OnsiteTrainingId)
create index Candidate_Managers on EmploymentCandidate (ManagersId)
create index Candidate_PersonnelManagers on EmploymentCandidate (PersonnelManagersId)
create index Candidate_AppointmentCreator on EmploymentCandidate (AppointmentCreatorId)
alter table EmploymentCandidate add constraint FK_Candidate_User foreign key (UserId) references [Users]
alter table EmploymentCandidate add constraint FK_Candidate_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
alter table EmploymentCandidate add constraint FK_Candidate_Passport foreign key (PassportId) references Passport
alter table EmploymentCandidate add constraint FK_Candidate_Education foreign key (EducationId) references Education
alter table EmploymentCandidate add constraint FK_Candidate_Family foreign key (FamilyId) references Family
alter table EmploymentCandidate add constraint FK_Candidate_MilitaryService foreign key (MilitaryServiceId) references MilitaryService
alter table EmploymentCandidate add constraint FK_Candidate_Experience foreign key (ExperienceId) references Experience
alter table EmploymentCandidate add constraint FK_Candidate_Contacts foreign key (ContactsId) references Contacts
alter table EmploymentCandidate add constraint FK_Candidate_BackgroundCheck foreign key (BackgroundCheckId) references BackgroundCheck
alter table EmploymentCandidate add constraint FK_Candidate_OnsiteTraining foreign key (OnsiteTrainingId) references OnsiteTraining
alter table EmploymentCandidate add constraint FK_Candidate_Managers foreign key (ManagersId) references Managers
alter table EmploymentCandidate add constraint FK_Candidate_PersonnelManagers foreign key (PersonnelManagersId) references PersonnelManagers
alter table EmploymentCandidate add constraint FK_Candidate_AppointmentCreator foreign key (AppointmentCreatorId) references [Users]
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
create index IX_HelpServiceRequestComment_User on HelpServiceRequestComment (UserId)
create index IX_HelpServiceRequestComment_HelpServiceRequest on HelpServiceRequestComment (HelpServiceRequestId)
alter table HelpServiceRequestComment add constraint FK_HelpServiceRequestComment_User foreign key (UserId) references [Users]
alter table HelpServiceRequestComment add constraint FK_HelpServiceRequestComment_HelpServiceRequest foreign key (HelpServiceRequestId) references HelpServiceRequest
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
alter table FamilyMember add constraint FK_FamilyMember_Family foreign key (FamilyId) references Family
alter table ExperienceItem add constraint FK_ExperienceItem_Experience foreign key (ExperienceId) references Experience
create index GeneralInfo_Candidate on GeneralInfo (CandidateId)
create index GeneralInfo_Citizenship on GeneralInfo (CitizenshipId)
create index GeneralInfo_InsuredPersonType on GeneralInfo (InsuredPersonTypeId)
create index GeneralInfo_DisabilityDegree on GeneralInfo (DisabilityDegreeId)
alter table GeneralInfo add constraint FK_GeneralInfo_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table GeneralInfo add constraint FK_GeneralInfo_Citizenship foreign key (CitizenshipId) references Country
alter table GeneralInfo add constraint FK_GeneralInfo_InsuredPersonType foreign key (InsuredPersonTypeId) references InsuredPersonType
alter table GeneralInfo add constraint FK_GeneralInfo_DisabilityDegree foreign key (DisabilityDegreeId) references DisabilityDegree
create index IX_AppointmentComment_User on AppointmentComment (UserId)
create index IX_AppointmentComment_Appointment on AppointmentComment (AppointmentId)
alter table AppointmentComment add constraint FK_AppointmentComment_User foreign key (UserId) references [Users]
alter table AppointmentComment add constraint FK_AppointmentComment_Appointment foreign key (AppointmentId) references Appointment
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
alter table NameChange add constraint FK_NameChange_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
alter table ForeignLanguage add constraint FK_ForeignLanguage_GeneralInfo foreign key (GeneralInfoId) references GeneralInfo
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
create index IX_HelpVersion_User_Id on HelpVersion (UserId)
alter table HelpVersion add constraint FK_HelpVersion_User foreign key (UserId) references [Users]
create index IX_AppointmentReportComment_User on AppointmentReportComment (UserId)
create index IX_AppointmentReportComment_AppointmentReport on AppointmentReportComment (AppointmentReportId)
alter table AppointmentReportComment add constraint FK_AppointmentReportComment_User foreign key (UserId) references [Users]
alter table AppointmentReportComment add constraint FK_AppointmentReportComment_AppointmentReport foreign key (AppointmentReportId) references AppointmentReport
create index IX_AppointmentCreateManager2ToDepartment2_User on AppointmentCreateManager2ToDepartment2 (ManagerId)
create index IX_AppointmentCreateManager2ToDepartment2_Department on AppointmentCreateManager2ToDepartment2 (DepartmentId)
alter table AppointmentCreateManager2ToDepartment2 add constraint FK_AppointmentCreateManager2ToDepartment2_User foreign key (ManagerId) references [Users]
alter table AppointmentCreateManager2ToDepartment2 add constraint FK_AppointmentCreateManager2ToDepartment2_Department foreign key (DepartmentId) references Department
create index Vacation_VacationType on Vacation (TypeId)
create index Vacation_AdditionalVacationType on Vacation (AdditionalVacationTypeId)
create index IX_Vacation_User_Id on Vacation (UserId)
create index IX_Vacation_CreatorUser_Id on Vacation (CreatorId)
create index Vacation_TimesheetStatus on Vacation (TimesheetStatusId)
alter table Vacation add constraint FK_Vacation_VacationType foreign key (TypeId) references VacationType
alter table Vacation add constraint FK_Vacation_AdditionalVacationType foreign key (AdditionalVacationTypeId) references AdditionalVacationType
alter table Vacation add constraint FK_Vacation_User foreign key (UserId) references [Users]
alter table Vacation add constraint FK_Vacation_CreatorUser foreign key (CreatorId) references [Users]
alter table Vacation add constraint FK_Vacation_TimesheetStatus foreign key (TimesheetStatusId) references TimesheetStatus
create index IX_Timesheet_User_Id on Timesheet (UserId)
alter table Timesheet add constraint FK_Timesheet_User foreign key (UserId) references [Users]
alter table UserLogin add constraint FK_UserLogin_User foreign key (UserId) references [Users]
create index HelpServiceRequest_HelpServiceType on HelpServiceRequest (TypeId)
create index HelpServiceRequest_HelpServiceProductionTime on HelpServiceRequest (ProductionTimeId)
create index HelpServiceRequest_HelpServiceTransferMethod on HelpServiceRequest (TransferMethodId)
create index HelpServiceRequest_HelpServicePeriod on HelpServiceRequest (PeriodId)
create index IX_HelpServiceRequest_User_Id on HelpServiceRequest (UserId)
create index IX_HelpServiceRequest_CreatorUser_Id on HelpServiceRequest (CreatorId)
create index IX_HelpServiceRequest_Consultant_Id on HelpServiceRequest (ConsultantId)
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServiceType foreign key (TypeId) references HelpServiceType
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServiceProductionTime foreign key (ProductionTimeId) references HelpServiceProductionTime
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServiceTransferMethod foreign key (TransferMethodId) references HelpServiceTransferMethod
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_HelpServicePeriod foreign key (PeriodId) references HelpServicePeriod
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_User foreign key (UserId) references [Users]
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpServiceRequest add constraint FK_HelpServiceRequest_Consultant foreign key (ConsultantId) references [Users]
create index Managers_Candidate on Managers (CandidateId)
create index Managers_Position on Managers (PositionId)
create index Managers_Department on Managers (DepartmentId)
create index Managers_Schedule on Managers (ScheduleId)
create index Managers_ApprovingManager on Managers (ApprovingManagerId)
create index Managers_ApprovingHigherManager on Managers (ApprovingHigherManagerId)
create index Managers_RejectingChief on Managers (RejectingChiefId)
alter table Managers add constraint FK_Managers_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table Managers add constraint FK_Managers_Position foreign key (PositionId) references Position
alter table Managers add constraint FK_Managers_Department foreign key (DepartmentId) references Department
alter table Managers add constraint FK_Managers_Schedule foreign key (ScheduleId) references Schedule
alter table Managers add constraint FK_Managers_ApprovingManager foreign key (ApprovingManagerId) references [Users]
alter table Managers add constraint FK_Managers_ApprovingHigherManager foreign key (ApprovingHigherManagerId) references [Users]
alter table Managers add constraint FK_Managers_RejectingChief foreign key (RejectingChiefId) references [Users]
create index Passport_Candidate on Passport (CandidateId)
create index Passport_DocumentType on Passport (DocumentTypeId)
alter table Passport add constraint FK_Passport_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table Passport add constraint FK_Passport_DocumentType foreign key (DocumentTypeId) references DocumentType
alter table Training add constraint FK_Training_Education foreign key (EducationId) references Education
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
create index BackgroundCheck_Candidate on BackgroundCheck (CandidateId)
create index BackgroundCheck_Approver on BackgroundCheck (ApproverId)
alter table BackgroundCheck add constraint FK_BackgroundCheck_Candidate foreign key (CandidateId) references EmploymentCandidate
alter table BackgroundCheck add constraint FK_BackgroundCheck_Approver foreign key (ApproverId) references [Users]
create index IX_AppointmentManager23ToDepartment3_User on AppointmentManager23ToDepartment3 (ManagerId)
create index IX_AppointmentManager23ToDepartment3_Department on AppointmentManager23ToDepartment3 (DepartmentId)
alter table AppointmentManager23ToDepartment3 add constraint FK_AppointmentManager23ToDepartment3_User foreign key (ManagerId) references [Users]
alter table AppointmentManager23ToDepartment3 add constraint FK_AppointmentManager23ToDepartment3_Department foreign key (DepartmentId) references Department
create index IX_ManualRoleRecord_User_Id on [ManualRoleRecord] (UserId)
create index IX_ManualRoleRecord_ManualRole_Id on [ManualRoleRecord] (RoleId)
create index IX_ManualRoleRecord_TargetUser_Id on [ManualRoleRecord] (TargetUserId)
create index IX_ManualRoleRecord_TargetDepartment_Id on [ManualRoleRecord] (TargetDepartmentId)
alter table [ManualRoleRecord] add constraint FK_ManualRoleRecord_User foreign key (UserId) references [Users]
alter table [ManualRoleRecord] add constraint FK_ManualRoleRecord_ManualRole foreign key (RoleId) references [ManualRole]
alter table [ManualRoleRecord] add constraint FK_ManualRoleRecord_TargetUser foreign key (TargetUserId) references [Users]
alter table [ManualRoleRecord] add constraint FK_ManualRoleRecord_TargetDepartment foreign key (TargetDepartmentId) references Department
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
INSERT INTO [Role] (Id,[Name],Version) values (1,'�������������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'���������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'������������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (8,'��������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (16,'������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (32,'����������',1)
INSERT INTO [Role] (Id,[Name],Version) values (64,'���������',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'�� �������� �����������',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'�������� �����������',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'�������� �������������',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'�������� ����������',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'��������� � 1�',1)
set identity_insert  [RequestStatus] off 

declare @OrganizationId int
declare @Organization1Id int
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values ('1','�������� �����������',1)	
set @OrganizationId = @@Identity
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values ('2','�������� ����������� 1',1)	
set @Organization1Id = @@Identity	
	

declare @DepartmentId int
declare @Department1Id int
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values ('1','�������� �����������',1)		
set @DepartmentId = @@Identity	
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values ('2','�������� ����������� 1',1)		
set @Department1Id = @@Identity	


declare @PositionId int
declare @Position1Id int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('1','�������� ���������',1)		
set @PositionId = @@Identity
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('2','�������� ��������� 1',1)		
set @Position1Id = @@Identity
declare @ManPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('3','������������',1)		
set @ManPositionId = @@Identity	
declare @PerPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('4','��������',1)		
set @PerPositionId = @@Identity	
declare @InsPositionId int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('5','���������',1)		
set @InsPositionId = @@Identity	
	

INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('51','�������������� ������� ������ ��� ������ #1203',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('31','������ ��� ����� ����� � ���. ��� ������ ������� #1125',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('41','������ ��������������� ������� �� ����������� ���� #1207',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('32','������ �������������� �������� ���� �� ����� �� ������ - ���������� #1504',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('41','������ ������� �� ����������� ���� #1201',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('42','������ ������� �� ����������� #1202',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('41','������ �������� ������� �� ����������� ���� #1204',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('53','������ ��� ������ �������� �� �� #1205',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('54','������ �� ���� ���� #1206',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('23','������ �� ������������ � ����� #1501',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values ('52','������ �� ����� �� �������� ��� ������ #1802',1)		


INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('51','�������������� ������� ������ ��� ������ #1203',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('53','������ ��� ������ �������� �� �� #1205',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('54','������ �� ���� ����#1206',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('10024','���������� �� ������� (�� ������������ � �����) #1804',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('10023','���������� �� ������� #1803',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('55','���������� �� ������������ ������� #1806',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values ('56','������, ������� �� ���� ��������� #1807',1)	

INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('25','�� �� ������ � ���� (�� ������������) #1805',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('26','������� �� ���������� ������ #1402',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('22','������ �� �� ������ �� ������������ #1403',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('25','������ ���������� ������ �� ���� ������������ #1426',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('21','������ ���������� ������ #1469',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('10024','���������� �� ������� (�� ������������ � �����) #1804',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('10023','���������� �� ������� #1803',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values ('55','���������� �� ������������ ������� #1806',1)
set identity_insert  [dbo].[SicklistType] on
INSERT INTO [dbo].[SicklistType]  (Id,[Code],[Name],Version) values (9,'71','������� �� ����� �� ������� �� 1.5 ��� #1502',1)
INSERT INTO [dbo].[SicklistType]  (Id,[Code],[Name],Version) values (10,'72','������� �� ����� �� ������� �� 3 ��� #1503',1)
set identity_insert [dbo].[SicklistType] off 

INSERT INTO [dbo].[SicklistBabyMindingType]  ([Code],[Name],Version) values ('1','�������� ���� �� �������� 1',1)
INSERT INTO [dbo].[SicklistBabyMindingType]  ([Code],[Name],Version) values ('2','�������� ���� �� �������� 2',1)

INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values ('13','������� �� ������ � ��������� � �������� #1107',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values ('12','������ ����������� � �������� ���� #1106',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (null,'�����',1)

INSERT INTO [dbo].[MissionType]  ([Code],[Name],Version) values ('1','������������ �� ������',1)
INSERT INTO [dbo].[MissionType]  ([Code],[Name],Version) values ('2','������������ �� ���',1)
INSERT INTO [dbo].[MissionType]  ([Code],[Name],Version) values ('3','������������  �� �����',1)

INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('1','�� ����������������',1)
INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('2','�� �������� ����� ������',1)
INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('3','�� �������� ��������� ��������',1)
INSERT INTO [dbo].[EmploymentType]  ([Code],[Name],Version) values ('4','���������',1)

INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('1','����������   0,2 ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('2','0,001',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('3','0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('4','0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('5','0,1',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('6','0,16',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('7','0,2 ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('8','0,55 ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('9','0,68 ������ (27,2 ����)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('10','0,7 ������ (28 �����)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('11','0,8 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('12','0.8',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('13','1 ���',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('14','10 ����� (0,25)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('15','10-�������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('16','12 � (0,3������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('17','14� (0,35 ������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('18','16 � (0,4������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('19','16,8 � (0,42 ������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('20','20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('21','20 ����� (0,5 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('22','24 �(0,6��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('23','25 (0,625)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('24','29,2 ���� (0.73 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('25','30 (0,75��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('26','32 ���� ( 0.8��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('27','33.6 ���� ( 0.84��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('28','34,8 ���� ( 0,87 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('29','35 ����� (0,87 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('30','35,6 ���� (0,89��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('31','36,4 ���� ( 0,91��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('32','36.8 ����  (0,92��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('33','37,5 ����� (0,94 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('34','38,4 ���� (0,96��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('35','39.6 ���� ( 0.99��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('36','4 ���� (0,1 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('37','6,4 ���� (0,16 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('38','6,8 ���� (0,17 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('39','������ 15�/� (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('40','������ 2 ����� 2 2�����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('41','������ 2 ����� 2 �� 11 ����� 1�����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('42','������ 25�/� (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('43','��������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('44','�������� ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('45','�������� ������ (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('46','�������� ������ 20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('47','�������� ������ 20����� ������ (�������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('48','�������� ������ 40����� ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('49','����������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('50','���������� (30 �����)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('51','���������� 0.4��',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('52','���������� 10����� (�������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('53','���������� 20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('54','���������� 20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('55','���������� 30 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('56','���������� 32 ����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('57','���������� 35',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('58','���������� 35 ����� (�������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('59','���������� 37,5',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('60','�������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('61','����������� ���� �� 7 ����� (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('61','���. ���� ���. �������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Code],[Name],Version) values ('63','������������� ����',1)

INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� �� ������� ��� ������� � �������� #1114','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� �� ������������ #1115','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� �� ���������� �������� ������ #1116','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� ������������ #1117','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� ��������������� #1123','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','�������� ����������� #1301','��������� ��������� ������� ������',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values (null,'���������� ���� ������������ #1120','�� �������� �������� ������ �� ����� ���������',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','�������� �������� 1 #1302','��������� ��������� ������� ������',1)

--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('��������� ���������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('��������� �������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('������������ ������������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('����������� ����������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('������ ������������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('������� � ������',1)

INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('1','�. 1 ��. 81 ��','����������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('2','�. 1 ����� 1 ��. 77 ��','���������� ������.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('3','�. 2 ��. 77 ��','��������� ����� ��������� ��������.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('4','�. 2 ����� 1 ��. 81 ��','���������� ����������� �����',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('5','�. 3 ��. 77 ��','����������� ��������� �������� �� ���������� ���������.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('6','�. 4 ��. 77 ��','����������� ��������� �������� �� ���������� ������������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('7','�. 5 ��. 77 ��','������� ��������� �� ������ ������ � ������� ������������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('8','�. 5 ����� ������ ��. 77','������� ��������� �� �������� ���������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('9','�. 6 ��. 81 ��','����������� ��������� �������� �� ���������� ������������ �� ������.',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('10','�. 6 ��.83 ��','������  ���������  ���� ������������ - ���. ����, � ����� ��������� ����� ��������� ���� ������������ - ���. ���� �������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('11','�. 7 �. 1 ��.81 �� ��','�������� ������� ���������� � ����� � ����������� �������� �������� ����������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('12','�. 7 ����� 1 ��. 77','����� ��������� �� ����������� ������ � ����� � ���������� ������������ ��������� ������� ��',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('13','�.3 ����� 1 ��. 77 �� ��','����������� �������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('14','�.5 ����� ������ ��. 83 �� ��','�������� ������� ��������� � ����� � ���������� ��������� ��������� ����������� � �������� ������������ � ������������ � ����������� �����������',1)
INSERT INTO [dbo].[DismissalType]  ([Code],[Name],[Reason],Version) values ('15','�.6 �.1 ��. 83','�������� ������� ��������� � ����� �� ������� ���������',1)

INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('2','����� �� ����� #1102','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('3','������ �� �������� ������ #1103','�� ������� �������� ������',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('66','������ ���������� ������� �� ������ �� ����� #1707','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('67','������ ���������� ������� �� �������� ������# 1708','�� ������� �������� ������',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values ('68','��������� ������� �� ���� ������������ #1702','�� �������� ���������',1)



--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('���� 1',1)
--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('���� 2',1)


INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Code],[Name],Version) values ('1','� ������� ����',1)
INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Code],[Name],Version) values ('2','�����, � ������������ � ������� � ������� ���',1)

--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'�� �� ������ � ���� (�� ������������) #1805','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (26,'������� �� ���������� ������ #1402','������� �� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (22,'������ �� �� ������ �� ������������ #1403','�� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'������ ���������� ������ �� ���� ������������ #1426','�� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (21,'������ ���������� ������ #1469','�� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10024,'���������� �� ������� (�� ������������ � �����) #1804','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10023,'���������� �� ������� #1803','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (55,'���������� �� ������������ ������� #1806','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (71,'������� �� ����� �� ������� �� 1.5 ��� #1502','������� �� ����� �� �������� �� 1.5 ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (72,'������� �� ����� �� ������� �� 3 ��� #1503','������� �� ����� �� �������� �� 3 ���',1)

INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (60,3,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (80,2,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (100,1,1)



set identity_insert  [TimesheetStatus] on
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (1,1,'�','����')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (2,1,'�','��������� ������������������ � ����������� ������� �������� ����������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (3,1,'�','��������� ������������������ ��� ������. ������� � �������, �������. �����������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (4,1,'��','�������� ����')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (5,1,'�','������ ����')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (6,1,'�','�������� � ��������� ���')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (7,1,'�','������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (8,1,'��','������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (9,1,'��','������ ��� ���������� ���������� ����� � �������, ��������������� �����������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (10,1,'��','������ ��� ���������� ���������� �����, ��������������� ����. �� ������. ������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (11,1,'�','������ �� ������������ � ����� (������ � ����� � ������������ ��������. �������)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (12,1,'��','������ �� ����� �� �������� �� ���������� �� �������� ���� ���')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (13,1,'��','����������������� ������ � �������� � ���������, ����������� ���')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (14,1,'�','����������������� ������������ ������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (15,1,'��','������� (���������� �� ������� ����� ��� ����. ������ � ���. �������, ���. �����������������)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (16,1,'��','������ �� ������������ ��������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (17,1,'��','������� �� ���� ����������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (18,1,'��','����� ������� �� ���� ������������')
set identity_insert  [TimesheetStatus] off


/*declare @typeId int
declare @firstTypeId int
declare @firstSubTypeId int
INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������',1) 
set @typeId = @@Identity
set @firstTypeId = @typeId
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('���������',1,@typeId) 
set @firstSubTypeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ����� �� ��������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��� ���������� ��',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� ���',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ��',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� �����',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('����������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ����� �� ��������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������ �� ������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� � ���������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������ � �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ����� �� ��������',1,@typeId) 


INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('���������� �� ������� �����',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������ �������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('����������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������ �������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���������� �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���������� ������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���������� ������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('� ����� � ���������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('�����������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� ������ �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����� ������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('���������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �� ���� ������������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('���. ������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����������� ����� (������)',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������, �����������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �� ����� �� ���. 3� ���',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �� ����� �� ���. 1,5� ���',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����������� ��������� �� ����� ���������������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������� �� ���� ���',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� � ����� �� �������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� ��� ���������� �� ���� � ������ ����� ������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� ��� �������� �������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� ��� ����������� �������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('���������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�������������� ����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������������ ����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� �����',1,@typeId) 
*/

-- User
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]		, [IsNew]	 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'�������������',             1,		  null ,          1,           '��0000000001' , 0)
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'������������',              1,         null								, 4,		   '��0000000001' , 0, @ManPositionId)
set @managerId = @@Identity

declare @managerId1 int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'manager1' ,'manager1'  ,	'2008-12-01 15:13:25:000',  N'������������ 1',              1,         null								, 4,		   '��0000000002' , 0, @ManPositionId)
set @managerId1 = @@Identity

--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'��������',                    1,         null								, 8,		   '��0000000001' , 0, @PerPositionId)
set @personnelId = @@Identity

declare @personnel1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'personnel1' ,'personnel1'  ,	'2008-12-01 15:13:25:000',    N'��������1',                    1,         null								, 8,		   '��0000000002' , 0, @PerPositionId)
set @personnel1Id = @@Identity

insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@managerId)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnelId,@managerId1)
insert into [dbo].[UserToPersonnel] (PersonnelId,UserId) values (@personnel1Id,@managerId)


declare @inspectorId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'inspector' ,'inspector'  ,	'2008-12-01 15:13:25:000',    N'���������',                    1,         null								, 64,		   '��0000000001' , 0, @InsPositionId)
set @inspectorId = @@Identity

declare @inspector1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew], PositionId) 
VALUES			   (1,       	0              ,'inspector1' ,'inspector1'  ,	'2008-12-01 15:13:25:000',    N'��������� 1',                    1,         null								, 64,		   '��0000000001' , 0, @InsPositionId)
set @inspector1Id = @@Identity


--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
/*declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'������',           1,         null								        , 16,		   '��0000000001' , 0)
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew] ) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'����������',                        1,         null,              32,		       '��0000000001' , 0)
set @outsorsingId = @@Identity*/
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         /*PersonnelManagerId,*/ OrganizationId,DepartmentId,PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'������������',                   1,         null            , 2,		   '��0000000001' ,  @managerId,       /*@personnelId,*/       @OrganizationId,@DepartmentId,@PositionId , 0)
set @userId = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,/*PersonnelManagerId,*/            OrganizationId,DepartmentId,PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'������ ���� ��������',            1,         null            , 2,		   '��0000000001' ,  @managerId,       /*@personnelId,*/@Organization1Id,@Department1Id,@Position1Id , 0)
set @user1Id = @@Identity
--INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

declare @user2Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,/*PersonnelManagerId ,*/ [IsNew], OrganizationId,DepartmentId,PositionId) 
VALUES			   (1,       	0              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'������ ���� ��������',         1,         null            , 2,		   '��0000000001' ,  @managerId,       /*@personnelId ,*/ 0 ,  @OrganizationId,@DepartmentId, @PositionId )
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
--values (1,'2011-10-22',     '��������',  @firstTypeId, @firstSubTypeId, @userId, 0)

