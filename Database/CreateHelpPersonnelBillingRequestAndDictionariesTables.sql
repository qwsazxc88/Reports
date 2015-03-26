if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_HelpBillingTitle]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_HelpBillingTitle

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_HelpBillingUrgency]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_HelpBillingUrgency

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_Department]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_WorkerUser]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_WorkerUser
GO

if exists (select * from dbo.sysobjects where id = object_id(N'HelpPersonnelBillingRequest') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpPersonnelBillingRequest
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingTitle') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpBillingTitle
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingUrgency') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpBillingUrgency

GO



create table HelpBillingTitle (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpBillingTitle  primary key (Id)
)

create table HelpBillingUrgency (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpBillingUrgency  primary key (Id)
)
create table HelpPersonnelBillingRequest (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  SendDate DATETIME null,
  BeginWorkDate DATETIME null,
  EndWorkDate DATETIME null,
  Number INT not null,
  TitleId INT not null,
  UrgencyId INT not null,
  DepartmentId INT not null,
  UserName NVARCHAR(256) null,
  Question NVARCHAR(MAX) not null,
  Answer NVARCHAR(MAX) null,
  RecipientId INT not null,
  RecipientRoleId INT not null,
  CreatorId INT not null,
  CreatorRoleId INT not null,
  WorkerId INT null,
  constraint PK_HelpPersonnelBillingRequest  primary key (Id)
)

create index HelpPersonnelBillingRequest_HelpBillingTitle on HelpPersonnelBillingRequest (TitleId)
create index HelpPersonnelBillingRequest_HelpBillingUrgency on HelpPersonnelBillingRequest (UrgencyId)
create index HelpPersonnelBillingRequest_Department on HelpPersonnelBillingRequest (DepartmentId)
create index IX_HelpPersonnelBillingRequest_CreatorUser_Id on HelpPersonnelBillingRequest (CreatorId)
create index IX_HelpPersonnelBillingRequest_WorkerUser_Id on HelpPersonnelBillingRequest (WorkerId)
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_HelpBillingTitle foreign key (TitleId) references HelpBillingTitle
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_HelpBillingUrgency foreign key (UrgencyId) references HelpBillingUrgency
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_Department foreign key (DepartmentId) references Department
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_WorkerUser foreign key (WorkerId) references [Users]
GO

