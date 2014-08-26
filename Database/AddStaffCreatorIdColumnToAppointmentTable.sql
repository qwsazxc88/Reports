if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_StaffCreatorUser]') AND parent_object_id = OBJECT_ID('Appointment'))
	alter table Appointment  drop constraint FK_Appointment_StaffCreatorUser

alter table Appointment add StaffCreatorId INT null
create index IX_Appointment_StaffCreatorUser_Id on Appointment (StaffCreatorId)
alter table Appointment add constraint FK_Appointment_StaffCreatorUser foreign key (StaffCreatorId) references [Users]