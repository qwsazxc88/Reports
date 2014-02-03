if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionPurchaseBookDocument]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionPurchaseBookDocument

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionReportCostType]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionReportCostType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_MissionReportCost]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_MissionReportCost

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_User]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookRecord_EditorUser]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookRecord'))
alter table MissionPurchaseBookRecord  drop constraint FK_MissionPurchaseBookRecord_EditorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookDocument_Contractor]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookDocument'))
alter table MissionPurchaseBookDocument  drop constraint FK_MissionPurchaseBookDocument_Contractor

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookDocument_EditorUser]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookDocument'))
alter table MissionPurchaseBookDocument  drop constraint FK_MissionPurchaseBookDocument_EditorUser

if exists (select * from dbo.sysobjects where id = object_id(N'MissionPurchaseBookRecord') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionPurchaseBookRecord
if exists (select * from dbo.sysobjects where id = object_id(N'MissionPurchaseBookDocument') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionPurchaseBookDocument
if exists (select * from dbo.sysobjects where id = object_id(N'Contractor') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Contractor


create table MissionPurchaseBookDocument (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  DocumentDate DATETIME not null,
  Number NVARCHAR(255) not null,
  ContractorId INT null,
  Sum DECIMAL(19,5) not null,
  EditorId INT not null,
  constraint PK_MissionPurchaseBookDocument  primary key (Id)
)
create table Contractor (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(256) not null,
  Account NVARCHAR(32) not null,
  constraint PK_Contractor  primary key (Id)
)

create table MissionPurchaseBookRecord (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  EditDate DATETIME not null,
  MissionPurchaseBookDocumentId INT not null,
  MissionOrderId INT not null,
  MissionReportCostTypeId INT not null,
  MissionReportCostId INT not null,
  Sum DECIMAL(19,5) not null,
  SumNds DECIMAL(19,5) not null,
  AllSum DECIMAL(19,5) not null,
  RequestNumber NVARCHAR(16) not null,
  UserId INT not null,
  EditorId INT not null,
  constraint PK_MissionPurchaseBookRecord  primary key (Id)
)

create index MissionPurchaseBookDocument_Contractor on MissionPurchaseBookDocument (ContractorId)
create index IX_MissionPurchaseBookDocument_EditorUser on MissionPurchaseBookDocument (EditorId)
alter table MissionPurchaseBookDocument add constraint FK_MissionPurchaseBookDocument_Contractor foreign key (ContractorId) references Contractor
alter table MissionPurchaseBookDocument add constraint FK_MissionPurchaseBookDocument_EditorUser foreign key (EditorId) references [Users]

create index MissionPurchaseBookRecord_MissionPurchaseBookDocument on MissionPurchaseBookRecord (MissionPurchaseBookDocumentId)
create index MissionPurchaseBookRecord_MissionOrder on MissionPurchaseBookRecord (MissionOrderId)
create index MissionPurchaseBookRecord_MissionReportCostType on MissionPurchaseBookRecord (MissionReportCostTypeId)
create index MissionPurchaseBookRecord_MissionReportCost on MissionPurchaseBookRecord (MissionReportCostId)
create index IX_MissionPurchaseBookRecord_User on MissionPurchaseBookRecord (UserId)
create index IX_MissionPurchaseBookRecord_EditorUser on MissionPurchaseBookRecord (EditorId)
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionPurchaseBookDocument foreign key (MissionPurchaseBookDocumentId) references MissionPurchaseBookDocument
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionOrder foreign key (MissionOrderId) references MissionOrder
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionReportCostType foreign key (MissionReportCostTypeId) references MissionReportCostType
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_MissionReportCost foreign key (MissionReportCostId) references MissionReportCost
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_User foreign key (UserId) references [Users]
alter table MissionPurchaseBookRecord add constraint FK_MissionPurchaseBookRecord_EditorUser foreign key (EditorId) references [Users]
