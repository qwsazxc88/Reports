if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AccountingTransaction_MissionReportCost]') AND parent_object_id = OBJECT_ID('AccountingTransaction'))
alter table AccountingTransaction  drop constraint FK_AccountingTransaction_MissionReportCost

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AccountingTransaction_DebitAccount]') AND parent_object_id = OBJECT_ID('AccountingTransaction'))
alter table AccountingTransaction  drop constraint FK_AccountingTransaction_DebitAccount

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AccountingTransaction_CreditAccount]') AND parent_object_id = OBJECT_ID('AccountingTransaction'))
alter table AccountingTransaction  drop constraint FK_AccountingTransaction_CreditAccount

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportCost_MissionReport]') AND parent_object_id = OBJECT_ID('MissionReportCost'))
alter table MissionReportCost  drop constraint FK_MissionReportCost_MissionReport

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportCost_MissionReportCostType]') AND parent_object_id = OBJECT_ID('MissionReportCost'))
alter table MissionReportCost  drop constraint FK_MissionReportCost_MissionReportCostType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportComment_User]') AND parent_object_id = OBJECT_ID('MissionReportComment'))
alter table MissionReportComment  drop constraint FK_MissionReportComment_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReportComment_MissionReport]') AND parent_object_id = OBJECT_ID('MissionReportComment'))
alter table MissionReportComment  drop constraint FK_MissionReportComment_MissionReport

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_User]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_CreatorUser]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AcceptUser]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AcceptUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AcceptManager]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AcceptManager

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AcceptAccountant]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_AcceptAccountant

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionReport'))
alter table MissionReport  drop constraint FK_MissionReport_MissionOrder

if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportComment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportComment
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReport') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReport
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportCost') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportCost
if exists (select * from dbo.sysobjects where id = object_id(N'AccountingTransaction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AccountingTransaction
if exists (select * from dbo.sysobjects where id = object_id(N'MissionReportCostType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionReportCostType
if exists (select * from dbo.sysobjects where id = object_id(N'Account') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Account


create table MissionReportCostType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  SortOrder int not null,
  constraint PK_MissionReportCostType  primary key (Id)
)

create table Account (
 Id INT IDENTITY NOT NULL,
  Number NVARCHAR(128) not null,
  IsDebitAccount BIT not null,
  constraint PK_Account  primary key (Id)
)

create table AccountingTransaction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CostId INT not null,
  DebitAccountId INT not null,
  CreditAccountId INT not null,
  Sum DECIMAL(19,5) not null,
  constraint PK_AccountingTransaction  primary key (Id)
)

create table MissionReportCost (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  ReportId INT not null,
  TypeId INT not null,
  Cnt INT null,
  Sum DECIMAL(19,5) null,
  UserSum DECIMAL(19,5) null,
  BookOfPurchaseSum DECIMAL(19,5) null,
  AccountantSum DECIMAL(19,5) null,
  constraint PK_MissionReportCost  primary key (Id)
)
create table MissionReport (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  -- BeginDate DATETIME not null,
  -- EndDate DATETIME not null,
  Number INT not null,
  AllSum DECIMAL(19,5) not null,
  UserAllSum DECIMAL(19,5) not null,
  UserSumReceived DECIMAL(19,5) not null,
  AccountantAllSum DECIMAL(19,5) not null,
  UserId INT not null,
  CreatorId INT not null,
  UserDateAccept DATETIME null,
  AcceptUserId INT null,
  ManagerDateAccept DATETIME null,
  AcceptManagerId INT null,
  AccountantDateAccept DATETIME null,
  AcceptAccountant INT null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  -- DeleteAfterSendTo1C BIT not null,
  MissionOrderId INT null,
  constraint PK_MissionReport  primary key (Id)
)
create table MissionReportComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  MissionReportId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_MissionReportComment  primary key (Id)
)


create index MissionReportCost_MissionReport on MissionReportCost (ReportId)
create index MissionReportCost_MissionReportCostType on MissionReportCost (TypeId)
alter table MissionReportCost add constraint FK_MissionReportCost_MissionReport foreign key (ReportId) references MissionReport
alter table MissionReportCost add constraint FK_MissionReportCost_MissionReportCostType foreign key (TypeId) references MissionReportCostType

create index IX_MissionReport_User_Id on MissionReport (UserId)
create index IX_MissionReport_CreatorUser_Id on MissionReport (CreatorId)
create index IX_MissionReport_AcceptUser on MissionReport (AcceptUserId)
create index IX_MissionReport_AcceptManager on MissionReport (AcceptManagerId)
create index IX_MissionReport_AcceptAccountant on MissionReport (AcceptAccountant)
create index IX_MissionReport_MissionOrder on MissionReport (MissionOrderId)
alter table MissionReport add constraint FK_MissionReport_User foreign key (UserId) references [Users]
alter table MissionReport add constraint FK_MissionReport_CreatorUser foreign key (CreatorId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptUser foreign key (AcceptUserId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptManager foreign key (AcceptManagerId) references [Users]
alter table MissionReport add constraint FK_MissionReport_AcceptAccountant foreign key (AcceptAccountant) references [Users]
alter table MissionReport add constraint FK_MissionReport_MissionOrder foreign key (MissionOrderId) references MissionOrder

create index IX_MissionReportComment_User on MissionReportComment (UserId)
create index IX_MissionReportComment_MissionReport on MissionReportComment (MissionReportId)
alter table MissionReportComment add constraint FK_MissionReportComment_User foreign key (UserId) references [Users]
alter table MissionReportComment add constraint FK_MissionReportComment_MissionReport foreign key (MissionReportId) references MissionReport


create index AccountingTransaction_MissionReportCost on AccountingTransaction (CostId)
create index AccountingTransaction_DebitAccount on AccountingTransaction (DebitAccountId)
create index AccountingTransaction_CreditAccount on AccountingTransaction (CreditAccountId)
alter table AccountingTransaction add constraint FK_AccountingTransaction_MissionReportCost foreign key (CostId) references MissionReportCost
alter table AccountingTransaction add constraint FK_AccountingTransaction_DebitAccount foreign key (DebitAccountId) references Account
alter table AccountingTransaction add constraint FK_AccountingTransaction_CreditAccount foreign key (CreditAccountId) references Account


