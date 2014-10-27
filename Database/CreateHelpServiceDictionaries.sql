if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpServicePeriod_Type]') AND parent_object_id = OBJECT_ID('HelpServicePeriod'))
	alter table HelpServicePeriod  drop constraint FK_HelpServicePeriod_Type
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServicePeriod') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpServicePeriod
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceTransferMethod') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpServiceTransferMethod
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceProductionTime') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpServiceProductionTime
if exists (select * from dbo.sysobjects where id = object_id(N'HelpServiceType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpServiceType
GO

create table HelpServiceType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  IsPeriodAvailable BIT not null,
  IsRequirementsAvailable BIT not null,
  IsAttachmentAvailable BIT not null,
  constraint PK_HelpServiceType  primary key (Id)
)

create table HelpServiceTransferMethod (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpServiceTransferMethod  primary key (Id)
)

create table HelpServiceProductionTime (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpServiceProductionTime  primary key (Id)
)


create table HelpServicePeriod (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  TypeId INT not null,
  SortOrder INT not null,
  constraint PK_HelpServicePeriod  primary key (Id)
)

create index HelpServicePeriod_Type on HelpServicePeriod (TypeId)
alter table HelpServicePeriod add constraint FK_HelpServicePeriod_Type foreign key (TypeId) references HelpServiceType
GO