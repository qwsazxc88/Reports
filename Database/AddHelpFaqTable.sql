if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpFaq_User]') AND parent_object_id = OBJECT_ID('HelpFaq'))
	alter table HelpFaq  drop constraint FK_HelpFaq_User
if exists (select * from dbo.sysobjects where id = object_id(N'HelpFaq') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpFaq
GO
create table HelpFaq (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  DateCreated DATETIME not null,
  Question NVARCHAR(MAX) not null,
  Answer NVARCHAR(MAX) not null,
  constraint PK_HelpFaq  primary key (Id)
)

create index IX_HelpFaq_User_Id on HelpFaq (UserId)
alter table HelpFaq add constraint FK_HelpFaq_User foreign key (UserId) references [Users]
GO