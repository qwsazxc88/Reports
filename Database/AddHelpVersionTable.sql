if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpVersion_User]') AND parent_object_id = OBJECT_ID('HelpVersion'))
	alter table HelpVersion  drop constraint FK_HelpVersion_User
if exists (select * from dbo.sysobjects where id = object_id(N'HelpVersion') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpVersion
GO

create table HelpVersion (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  ReleaseDate DATETIME not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(MAX) not null,
  constraint PK_HelpVersion  primary key (Id)
)
create index IX_HelpVersion_User_Id on HelpVersion (UserId)
alter table HelpVersion add constraint FK_HelpVersion_User foreign key (UserId) references [Users]
GO
