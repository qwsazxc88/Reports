alter table EmploymentCandidate add AppointmentCreatorId INT null
create index Candidate_AppointmentCreator on EmploymentCandidate (AppointmentCreatorId)
alter table EmploymentCandidate add constraint FK_Candidate_AppointmentCreator foreign key (AppointmentCreatorId) references [Users]