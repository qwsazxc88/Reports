if exists (select * from dbo.sysobjects where id = object_id(N'TerraGraphic') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table TerraGraphic
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TerraPointToUser]') AND name = N'IX_TerraPointToUserUnique')
	DROP INDEX [IX_TerraPointToUserUnique] ON [dbo].[TerraPointToUser] WITH ( ONLINE = OFF )
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TerraPointToUser_TerraPoint]') AND parent_object_id = OBJECT_ID('TerraPointToUser'))
	alter table TerraPointToUser  drop constraint FK_TerraPointToUser_TerraPoint
if exists (select * from dbo.sysobjects where id = object_id(N'TerraPointToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table TerraPointToUser
if exists (select * from dbo.sysobjects where id = object_id(N'TerraPoint') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table TerraPoint
GO

create table TerraGraphic (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  Day DATETIME not null,
  Hours INT null,
  PointId INT not null,
  IsCreditAvailable BIT null,
  constraint PK_TerraGraphic  primary key (Id)
)
	
create table TerraPointToUser (
 Id INT IDENTITY NOT NULL,
  TerraPointId INT not null,
  UserId INT not null,
  constraint PK_TerraPointToUser  primary key (Id)
)

create table TerraPoint (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(128) null,
  Name NVARCHAR(128) null,
  ShortName NVARCHAR(128) null,
  Code1C NVARCHAR(128) null,
  ParentId NVARCHAR(128) null,
  Path NVARCHAR(128) null,
  ItemLevel INT null,
  EndDate DATETIME null,
  constraint PK_TerraPoint  primary key (Id)
)
alter table TerraPointToUser add constraint FK_TerraPointToUser_TerraPoint foreign key (TerraPointId) references TerraPoint

CREATE UNIQUE NONCLUSTERED INDEX [IX_TerraPointToUserUnique] ON [dbo].[TerraPointToUser] 
(
	--[TerraPointId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


