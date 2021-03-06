﻿if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentComment_User]') AND parent_object_id = OBJECT_ID('AppointmentComment'))
alter table AppointmentComment  drop constraint FK_AppointmentComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentComment_Appointment]') AND parent_object_id = OBJECT_ID('AppointmentComment'))
alter table AppointmentComment  drop constraint FK_AppointmentComment_Appointment

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_Department]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_Position]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_Position

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AppointmentReason]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AppointmentReason

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_CreatorUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AcceptManager]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AcceptManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_AcceptChief]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_AcceptChief

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_PersonnelUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_PersonnelUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_StaffUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_StaffUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Appointment_DeleteUser]') AND parent_object_id = OBJECT_ID('Appointment'))
alter table Appointment  drop constraint FK_Appointment_DeleteUser

if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentComment
if exists (select * from dbo.sysobjects where id = object_id(N'Appointment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Appointment
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReason') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AppointmentReason

create table AppointmentReason (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_AppointmentReason  primary key (Id)
)

create table Appointment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  Number INT not null,
  DepartmentId INT not null,
  City NVARCHAR(64) not null,
  PositionId INT not null,
  VacationCount INT not null,
  ReasonId INT not null,
  ReasonBeginDate DATETIME null,
  ReasonPosition NVARCHAR(64) null,
  -- ReasonPersonnelStore NVARCHAR(64) null,
  -- ReasonUser NVARCHAR(64) null,
  Period NVARCHAR(128) not null,
  Schedule NVARCHAR(64) not null,
  Salary DECIMAL(19,5) not null,
  Bonus DECIMAL(19,5) not null,
  Type BIT not null,
  Compensation NVARCHAR(128) not null,
  EducationRequirements NVARCHAR(64) not null,
  ExperienceRequirements NVARCHAR(32) not null,
  OtherRequirements NVARCHAR(128) null,
  AdditionalRequirements NVARCHAR(128) null,
  Responsibility NVARCHAR(128) not null,
  DesirableBeginDate DATETIME null,
  IsVacationExists BIT not null,
  CreatorId INT not null,
  ManagerDateAccept DATETIME null,
  AcceptManagerId INT null,
  ChiefDateAccept DATETIME null,
  AcceptChiefId INT null,
  PersonnelDateAccept DATETIME null,
  AcceptPersonnelId INT null,
  StaffDateAccept DATETIME null,
  AcceptStaffId INT null,
  DeleteDate DATETIME null,
  DeleteUserId INT null,
  SendTo1C DATETIME null,
  constraint PK_Appointment  primary key (Id)
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

create index Appointment_Department on Appointment (DepartmentId)
create index Appointment_Position on Appointment (PositionId)
create index Appointment_AppointmentReason on Appointment (ReasonId)
create index IX_Appointment_CreatorUser_Id on Appointment (CreatorId)
create index IX_Appointment_AcceptManager on Appointment (AcceptManagerId)
create index IX_Appointment_AcceptChief on Appointment (AcceptChiefId)
create index IX_Appointment_PersonnelUser on Appointment (AcceptPersonnelId)
create index IX_Appointment_StaffUser on Appointment (AcceptStaffId)
create index IX_Appointment_DeleteUser on Appointment (DeleteUserId)
alter table Appointment add constraint FK_Appointment_Department foreign key (DepartmentId) references Department
alter table Appointment add constraint FK_Appointment_Position foreign key (PositionId) references Position
alter table Appointment add constraint FK_Appointment_AppointmentReason foreign key (ReasonId) references AppointmentReason
alter table Appointment add constraint FK_Appointment_CreatorUser foreign key (CreatorId) references [Users]
alter table Appointment add constraint FK_Appointment_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table Appointment add constraint FK_Appointment_AcceptChief foreign key (AcceptChiefId) references [Users]
alter table Appointment add constraint FK_Appointment_PersonnelUser foreign key (AcceptPersonnelId) references [Users]
alter table Appointment add constraint FK_Appointment_StaffUser foreign key (AcceptStaffId) references [Users]
alter table Appointment add constraint FK_Appointment_DeleteUser foreign key (DeleteUserId) references [Users]


create index IX_AppointmentComment_User on AppointmentComment (UserId)
create index IX_AppointmentComment_Appointment on AppointmentComment (AppointmentId)
alter table AppointmentComment add constraint FK_AppointmentComment_User foreign key (UserId) references [Users]
alter table AppointmentComment add constraint FK_AppointmentComment_Appointment foreign key (AppointmentId) references Appointment

GO