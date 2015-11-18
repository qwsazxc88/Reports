--”даление кандидата из приема
--сканы, если были не удал€ютс€
declare @userid int, @Candidateid int, @id int

set @userid = 25579
set @Candidateid = 3037
set @id = 3033

begin transaction
select * from EmploymentCandidate where id in (@Candidateid)

update EmploymentCandidate set GeneralInfoId = null, PassportId = null, EducationId = null, FamilyId = null,  MilitaryServiceId = null, ExperienceId = null, ContactsId = null, 
			 BackgroundCheckId = null, OnsiteTrainingId = null, ManagersId = null, PersonnelManagersId = null, AppointmentId = null, AppointmentReportId = null,
			 UserId = null
where id in (@Candidateid)

update GeneralInfo set CandidateId = null where id = @id
delete from GeneralInfo where id = @id

update Passport set CandidateId = null where id = @id
delete from Passport where id = @id

update Education set CandidateId = null where id = @id
delete from Education where id = @id

update Family set CandidateId = null where id = @id
delete from Family where id = @id

update MilitaryService set CandidateId = null where id = @id
delete from MilitaryService where id = @id

update Experience set CandidateId = null where id = @id
delete from Experience where id = @id

update Contacts set CandidateId = null where id = @id
delete from Contacts where id = @id

update BackgroundCheck set CandidateId = null where id = @id
delete from BackgroundCheck where id = @id

update OnsiteTraining set CandidateId = null where id = @id
delete from OnsiteTraining where id = @id

update Managers set CandidateId = null where id = @id
delete from Managers where id = @id

update PersonnelManagers set CandidateId = null where id = @id
delete from PersonnelManagers where id = @id


delete from EmploymentCandidate where id in (@Candidateid)

delete from EmploymentCandidateComments where CandidateId = @userid

delete from UserLogin where UserId = @userid

delete from users where id = @userid
--rollback transaction
--commit transaction