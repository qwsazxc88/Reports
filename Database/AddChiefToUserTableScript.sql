if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChiefToUser_Chief]') AND parent_object_id = OBJECT_ID('ChiefToUser'))
	alter table ChiefToUser  drop constraint FK_ChiefToUser_Chief
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ChiefToUser_User]') AND parent_object_id = OBJECT_ID('ChiefToUser'))
	alter table ChiefToUser  drop constraint FK_ChiefToUser_User
GO

if exists (select * from dbo.sysobjects where id = object_id(N'ChiefToUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table ChiefToUser
GO

create table ChiefToUser (
 Id INT IDENTITY NOT NULL,
  ChiefId INT not null,
  UserId INT not null,
  constraint PK_ChiefToUser  primary key (Id)
)
GO
create index IX_ChiefToUser_Chief_Id on ChiefToUser (ChiefId)
create index IX_ChiefToUser_User_Id on ChiefToUser (UserId)
alter table ChiefToUser add constraint FK_ChiefToUser_Chief foreign key (ChiefId) references [Users]
alter table ChiefToUser add constraint FK_ChiefToUser_User foreign key (UserId) references [Users]
GO

if not exists (select * from [Role] where Id = 128)
begin
	set identity_insert  [Role] on
	INSERT INTO [Role] (Id,[Name],Version) values (128,N'Начальник',1)
	set identity_insert  [Role] off 
end 
GO

--if not exists (select * from [dbo].[Position] where [Code] = '6')
--	INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values ('6',N'Начальник',1)		
