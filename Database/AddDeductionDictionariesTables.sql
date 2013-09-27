if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_DeductionType]') AND parent_object_id = OBJECT_ID('Deduction'))
	alter table Deduction  drop constraint FK_Deduction_DeductionType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_DeductionKind]') AND parent_object_id = OBJECT_ID('Deduction'))
	alter table Deduction  drop constraint FK_Deduction_DeductionKind

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_User]') AND parent_object_id = OBJECT_ID('Deduction'))
	alter table Deduction  drop constraint FK_Deduction_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_Deduction_EditorUser]') AND parent_object_id = OBJECT_ID('Deduction'))
	alter table Deduction  drop constraint FK_Deduction_EditorUser

if exists (select * from dbo.sysobjects where id = object_id(N'Deduction') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table Deduction

if exists (select * from dbo.sysobjects where id = object_id(N'DeductionKind') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table DeductionKind

if exists (select * from dbo.sysobjects where id = object_id(N'DeductionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table DeductionType

GO

create table DeductionKind (
 Id INT NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  CalculationStyle NVARCHAR(128) not null,
  constraint PK_DeductionKind  primary key (Id)
)
--SET IDENTITY_INSERT [dbo].[DeductionKind] ON


create table DeductionType (
 Id INT NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) not null,
  constraint PK_DeductionType  primary key (Id)
)

create table Deduction (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  EditDate DATETIME not null,
  DeductionDate DATETIME not null,
  Number INT not null,
  TypeId INT not null,
  KindId INT not null,
  Sum DECIMAL(19, 2) not null,
  DismissalDate DATETIME null,
  IsFastDismissal BIT null,
  UserId INT not null,
  CreatorId INT not null,
  SendTo1C DATETIME null,
  DeleteDate DATETIME null,
  DeleteAfterSendTo1C BIT not null,
  EmailSendToUserDate DATETIME null,
  constraint PK_Deduction  primary key (Id)
)
create index Deduction_DeductionType on Deduction (TypeId)
create index Deduction_DeductionKind on Deduction (KindId)
create index IX_Deduction_User_Id on Deduction (UserId)
create index IX_Deduction_EditorUser_Id on Deduction (CreatorId)
alter table Deduction add constraint FK_Deduction_DeductionType foreign key (TypeId) references DeductionType
alter table Deduction add constraint FK_Deduction_DeductionKind foreign key (KindId) references DeductionKind
alter table Deduction add constraint FK_Deduction_User foreign key (UserId) references [Users]
alter table Deduction add constraint FK_Deduction_EditorUser foreign key (CreatorId) references [Users]
GO




insert into [dbo].[DeductionKind] (Id, Version, Code, Name, CalculationStyle)
values (1,1,N'00003',N'Удержание по исп. листу процентом до предела#5110',N'Исполнительный лист процентом до предела')
insert into [dbo].[DeductionKind] (Id, Version, Code, Name, CalculationStyle)
values (2,1,N'00001',N'Удержание по исп. листу процентом#5108',N'Исполнительный лист процентом')
insert into [dbo].[DeductionKind] (Id, Version, Code, Name, CalculationStyle)
values (3,1,N'00006',N'Удержание по исп. листу фикс. суммой до предела#5113',N'Исполнительный лист фиксированной суммой до предела')

-- SET IDENTITY_INSERT [dbo].[DeductionType] ON
insert into [dbo].[DeductionType] (Id, Version, Name) values (1,1,N'Удержание')
insert into [dbo].[DeductionType] (Id, Version, Name) values (2,1,N'Удержание ПРИ увольнении')
insert into [dbo].[DeductionType] (Id, Version, Name) values (3,1,N'Удержание ПОСЛЕ увольнения')
--SET IDENTITY_INSERT [dbo].[DeductionType] OFF


GO