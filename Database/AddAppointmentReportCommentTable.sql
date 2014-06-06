if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReportComment_User]') AND parent_object_id = OBJECT_ID('AppointmentReportComment'))
alter table AppointmentReportComment  drop constraint FK_AppointmentReportComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentReportComment_AppointmentReport]') AND parent_object_id = OBJECT_ID('AppointmentReportComment'))
alter table AppointmentReportComment  drop constraint FK_AppointmentReportComment_AppointmentReport

if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentReportComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table AppointmentReportComment
GO

create table AppointmentReportComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  AppointmentReportId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_AppointmentReportComment  primary key (Id)
)
create index IX_AppointmentReportComment_User on AppointmentReportComment (UserId)
create index IX_AppointmentReportComment_AppointmentReport on AppointmentReportComment (AppointmentReportId)
alter table AppointmentReportComment add constraint FK_AppointmentReportComment_User foreign key (UserId) references [Users]
alter table AppointmentReportComment add constraint FK_AppointmentReportComment_AppointmentReport foreign key (AppointmentReportId) references AppointmentReport
GO