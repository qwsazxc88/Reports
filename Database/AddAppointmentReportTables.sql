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

if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table AppointmentReport
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentEducationType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table AppointmentEducationType

GO

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

GO

