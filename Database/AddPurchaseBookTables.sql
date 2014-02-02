if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookDocument_Contractor]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookDocument'))
alter table MissionPurchaseBookDocument  drop constraint FK_MissionPurchaseBookDocument_Contractor

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionPurchaseBookDocument_EditorUser]') AND parent_object_id = OBJECT_ID('MissionPurchaseBookDocument'))
alter table MissionPurchaseBookDocument  drop constraint FK_MissionPurchaseBookDocument_EditorUser

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

create index MissionPurchaseBookDocument_Contractor on MissionPurchaseBookDocument (ContractorId)
create index IX_MissionPurchaseBookDocument_EditorUser on MissionPurchaseBookDocument (EditorId)
alter table MissionPurchaseBookDocument add constraint FK_MissionPurchaseBookDocument_Contractor foreign key (ContractorId) references Contractor
alter table MissionPurchaseBookDocument add constraint FK_MissionPurchaseBookDocument_EditorUser foreign key (EditorId) references [Users]
