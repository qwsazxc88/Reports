if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionRequestHistoryEntity_HelpQuestionRequest]') AND parent_object_id = OBJECT_ID('HelpQuestionHistoryEntity'))
alter table HelpQuestionHistoryEntity  drop constraint FK_HelpQuestionRequestHistoryEntity_HelpQuestionRequest

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionHistoryEntity_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpQuestionHistoryEntity'))
alter table HelpQuestionHistoryEntity  drop constraint FK_HelpQuestionHistoryEntity_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionHistoryEntity_Consultant]') AND parent_object_id = OBJECT_ID('HelpQuestionHistoryEntity'))
alter table HelpQuestionHistoryEntity  drop constraint FK_HelpQuestionHistoryEntity_Consultant

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

if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionHistoryEntity') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpQuestionHistoryEntity
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionRequest') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpQuestionRequest
GO

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


create index IX_HelpQuestionRequestHistoryEntity_HelpQuestionRequest on HelpQuestionHistoryEntity (HelpQuestionRequestId)
create index IX_HelpQuestionHistoryEntity_CreatorUser_Id on HelpQuestionHistoryEntity (CreatorId)
create index IX_HelpQuestionHistoryEntity_Consultant_Id on HelpQuestionHistoryEntity (ConsultantId)
alter table HelpQuestionHistoryEntity add constraint FK_HelpQuestionRequestHistoryEntity_HelpQuestionRequest foreign key (HelpQuestionRequestId) references HelpQuestionRequest
alter table HelpQuestionHistoryEntity add constraint FK_HelpQuestionHistoryEntity_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpQuestionHistoryEntity add constraint FK_HelpQuestionHistoryEntity_Consultant foreign key (ConsultantId) references [Users]

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

GO

