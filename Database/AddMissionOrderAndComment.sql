if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_MissionType]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_MissionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_MissionGoal]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_MissionGoal

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

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderTarget_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionOrderTarget_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderComment_User]') AND parent_object_id = OBJECT_ID('MissionOrderComment'))
alter table MissionOrderComment  drop constraint FK_MissionOrderComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderComment_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionOrderComment'))
alter table MissionOrderComment  drop constraint FK_MissionOrderComment_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_Mission]') AND parent_object_id = OBJECT_ID('MissionOrder'))
alter table MissionOrder  drop constraint FK_MissionOrder_Mission


if exists (select * from dbo.sysobjects where id = object_id(N'MissionOrder') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionOrder
if exists (select * from dbo.sysobjects where id = object_id(N'MissionOrderComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionOrderComment
GO

create table MissionOrder (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  Number INT not null,
  TypeId INT null,
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
  NeedToAcceptByChiefAsManager BIT not null,
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
  constraint PK_MissionOrder  primary key (Id)
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

create index MissionOrder_MissionType on MissionOrder (TypeId)
create index MissionOrder_MissionGoal on MissionOrder (MissionGoalId)
create index IX_MissionOrder_User_Id on MissionOrder (UserId)
create index IX_MissionOrder_CreatorUser_Id on MissionOrder (CreatorId)
create index IX_MissionOrder_AcceptUser on MissionOrder (AcceptUserId)
create index IX_MissionOrder_AcceptManager on MissionOrder (AcceptManagerId)
create index IX_MissionOrder_AcceptChief on MissionOrder (AcceptChiefId)
create index IX_MissionOrder_Mission on MissionOrder (MissionId)
alter table MissionOrder add constraint FK_MissionOrder_MissionType foreign key (TypeId) references MissionType
alter table MissionOrder add constraint FK_MissionOrder_MissionGoal foreign key (MissionGoalId) references MissionGoal
alter table MissionOrder add constraint FK_MissionOrder_User foreign key (UserId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_CreatorUser foreign key (CreatorId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptUser foreign key (AcceptUserId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_AcceptChief foreign key (AcceptChiefId) references [Users]
alter table MissionOrder add constraint FK_MissionOrder_Mission foreign key (MissionId) references Mission

create index IX_MissionOrderComment_User on MissionOrderComment (UserId)
create index IX_MissionOrderComment_MissionOrder on MissionOrderComment (MissionOrderId)
alter table MissionOrderComment add constraint FK_MissionOrderComment_User foreign key (UserId) references [Users]
alter table MissionOrderComment add constraint FK_MissionOrderComment_MissionOrder foreign key (MissionOrderId) references MissionOrder

GO