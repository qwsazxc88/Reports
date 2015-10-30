
--Удаление кандидата из приема
begin transaction
select * from EmploymentCandidate where id in (3088)

update EmploymentCandidate set GeneralInfoId = null, PassportId = null, EducationId = null, FamilyId = null,  MilitaryServiceId = null, ExperienceId = null, ContactsId = null, 
			 BackgroundCheckId = null, OnsiteTrainingId = null, ManagersId = null, PersonnelManagersId = null, AppointmentId = null, AppointmentReportId = null,
			 UserId = null
where id in (3088)

update GeneralInfo set CandidateId = null where id = 3084
delete from GeneralInfo where id = 3084

update Passport set CandidateId = null where id = 3084
delete from Passport where id = 3084

update Education set CandidateId = null where id = 3084
delete from Education where id = 3084

update Family set CandidateId = null where id = 3084
delete from Family where id = 3084

update MilitaryService set CandidateId = null where id = 3084
delete from MilitaryService where id = 3084

update Experience set CandidateId = null where id = 3084
delete from Experience where id = 3084

update Contacts set CandidateId = null where id = 3084
delete from Contacts where id = 3084

update BackgroundCheck set CandidateId = null where id = 3084
delete from BackgroundCheck where id = 3084

update OnsiteTraining set CandidateId = null where id = 3084
delete from OnsiteTraining where id = 3084

update Managers set CandidateId = null where id = 3084
delete from Managers where id = 3084

update PersonnelManagers set CandidateId = null where id = 3084
delete from PersonnelManagers where id = 3084


delete from EmploymentCandidate where id in (3088)

delete from EmploymentCandidateComments where CandidateId = 25632

delete from users where id = 25632
--rollback transaction
--commit transaction