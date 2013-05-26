if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_WorkingGraphicTypeToUser_WorkingGraphicType]') AND parent_object_id = OBJECT_ID('WorkingGraphicTypeToUser'))
	alter table WorkingGraphicTypeToUser  drop constraint FK_WorkingGraphicTypeToUser_WorkingGraphicType
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_WorkingGraphicType_User]') AND parent_object_id = OBJECT_ID('WorkingGraphicTypeToUser'))
	alter table WorkingGraphicTypeToUser  drop constraint FK_WorkingGraphicType_User
GO
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphicTypeToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table WorkingGraphicTypeToUser
GO
if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphicType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table WorkingGraphicType
GO


create table WorkingGraphicType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) null,
  FillDays BIT  null,
  constraint PK_WorkingGraphicType  primary key (Id)
)
GO
create table WorkingGraphicTypeToUser (
 Id INT IDENTITY NOT NULL,
  WorkingGraphicTypeId INT not null,
  UserId INT not null,
  constraint PK_WorkingGraphicTypeToUser  primary key (Id)
)
GO
alter table WorkingGraphicTypeToUser add constraint FK_WorkingGraphicTypeToUser_WorkingGraphicType foreign key (WorkingGraphicTypeId) references WorkingGraphicType
alter table WorkingGraphicTypeToUser add constraint FK_WorkingGraphicType_User foreign key (UserId) references [Users]
GO