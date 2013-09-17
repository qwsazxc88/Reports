if exists (select * from dbo.sysobjects where id = object_id(N'DeductionKind') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table DeductionKind
create table DeductionKind (
 Id INT NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  CalculationStyle NVARCHAR(128) not null,
  constraint PK_DeductionKind  primary key (Id)
)
--SET IDENTITY_INSERT [dbo].[DeductionKind] ON
insert into [dbo].[DeductionKind] (Id, Version, Code, Name, CalculationStyle)
values (1,1,N'00003',N'Удержание по исп. листу процентом до предела#5110',N'Исполнительный лист процентом до предела')
insert into [dbo].[DeductionKind] (Id, Version, Code, Name, CalculationStyle)
values (2,1,N'00001',N'Удержание по исп. листу процентом#5108',N'Исполнительный лист процентом')
insert into [dbo].[DeductionKind] (Id, Version, Code, Name, CalculationStyle)
values (3,1,N'00006',N'Удержание по исп. листу фикс. суммой до предела#5113',N'Исполнительный лист фиксированной суммой до предела')

if exists (select * from dbo.sysobjects where id = object_id(N'DeductionType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table DeductionType

create table DeductionType (
 Id INT NOT NULL,
  Version INT not null,
  Name NVARCHAR(128) not null,
  constraint PK_DeductionType  primary key (Id)
)
-- SET IDENTITY_INSERT [dbo].[DeductionType] ON
insert into [dbo].[DeductionType] (Id, Version, Name) values (1,1,N'Удержание')
insert into [dbo].[DeductionType] (Id, Version, Name) values (2,1,N'Удержание ПРИ увольнении')
insert into [dbo].[DeductionType] (Id, Version, Name) values (3,1,N'Удержание ПОСЛЕ увольнения')
--SET IDENTITY_INSERT [dbo].[DeductionType] OFF


