if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpQuestionSubtype_HelpQuestionType]') AND parent_object_id = OBJECT_ID('HelpQuestionSubtype'))
	alter table HelpQuestionSubtype  drop constraint FK_HelpQuestionSubtype_HelpQuestionType
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionSubtype') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpQuestionSubtype
if exists (select * from dbo.sysobjects where id = object_id(N'HelpQuestionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpQuestionType
GO

create table HelpQuestionSubtype (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  TypeId INT not null,
  constraint PK_HelpQuestionSubtype  primary key (Id)
)

create table HelpQuestionType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpQuestionType  primary key (Id)
)

create index HelpQuestionSubtype_HelpQuestionType on HelpQuestionSubtype (TypeId)
alter table HelpQuestionSubtype add constraint FK_HelpQuestionSubtype_HelpQuestionType foreign key (TypeId) references HelpQuestionType
GO

