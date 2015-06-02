if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_HelpBillingTitle]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_HelpBillingTitle

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_HelpBillingUrgency]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_HelpBillingUrgency

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_Department]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_Department

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_CreatorUser]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_CreatorUser

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpPersonnelBillingRequest_WorkerUser]') AND parent_object_id = OBJECT_ID('HelpPersonnelBillingRequest'))
alter table HelpPersonnelBillingRequest  drop constraint FK_HelpPersonnelBillingRequest_WorkerUser
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingRoleRecord_User]') AND parent_object_id = OBJECT_ID('HelpBillingRoleRecord'))
ALTER TABLE [dbo].[HelpBillingRoleRecord] DROP CONSTRAINT [FK_HelpBillingRoleRecord_User]
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingRoleRecord_TargetUser]') AND parent_object_id = OBJECT_ID('HelpBillingRoleRecord'))
ALTER TABLE [dbo].[HelpBillingRoleRecord] DROP CONSTRAINT [FK_HelpBillingRoleRecord_TargetUser]
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingRoleRecord_TargetDepartment]') AND parent_object_id = OBJECT_ID('HelpBillingRoleRecord'))
ALTER TABLE [dbo].[HelpBillingRoleRecord] DROP CONSTRAINT [FK_HelpBillingRoleRecord_TargetDepartment]
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingRoleRecord_HelpBillingRole]') AND parent_object_id = OBJECT_ID('HelpBillingRoleRecord'))
ALTER TABLE [dbo].[HelpBillingRoleRecord] DROP CONSTRAINT [FK_HelpBillingRoleRecord_HelpBillingRole]
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingExecutorTasks_Users]') AND parent_object_id = OBJECT_ID('HelpBillingExecutorTasks'))
ALTER TABLE [dbo].[HelpBillingExecutorTasks] DROP CONSTRAINT [FK_HelpBillingExecutorTasks_Users]
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingExecutorTasks_HelpPersonnelBillingRequest]') AND parent_object_id = OBJECT_ID('HelpBillingExecutorTasks'))
ALTER TABLE [dbo].[HelpBillingExecutorTasks] DROP CONSTRAINT [FK_HelpBillingExecutorTasks_HelpPersonnelBillingRequest]
GO

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingComments_Users]') AND parent_object_id = OBJECT_ID('HelpBillingComments'))
ALTER TABLE [dbo].[HelpBillingComments] DROP CONSTRAINT [FK_HelpBillingComments_Users]
GO
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_HelpBillingComments_HelpPersonnelBillingRequest]') AND parent_object_id = OBJECT_ID('HelpBillingComments'))
ALTER TABLE [dbo].[HelpBillingComments] DROP CONSTRAINT [FK_HelpBillingComments_HelpPersonnelBillingRequest]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'HelpPersonnelBillingRequest') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpPersonnelBillingRequest
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingTitle') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpBillingTitle
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingUrgency') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpBillingUrgency
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingRoleRecord') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpBillingRoleRecord
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingRole') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table HelpBillingRole
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingExecutorTasks') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	DROP TABLE [dbo].[HelpBillingExecutorTasks]
if exists (select * from dbo.sysobjects where id = object_id(N'HelpBillingComments') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	DROP TABLE [dbo].[HelpBillingComments]
GO



create table HelpBillingTitle (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpBillingTitle  primary key (Id)
)

create table HelpBillingUrgency (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  SortOrder INT not null,
  constraint PK_HelpBillingUrgency  primary key (Id)
)
create table HelpPersonnelBillingRequest (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  CreateDate DATETIME not null,
  EditDate DATETIME not null,
  SendDate DATETIME null,
  BeginWorkDate DATETIME null,
  EndWorkDate DATETIME null,
  Number INT not null,
  TitleId INT not null,
  UrgencyId INT not null,
  DepartmentId INT null,
  UserName NVARCHAR(256) null,
  Question NVARCHAR(MAX) not null,
  Answer NVARCHAR(MAX) null,
  CreatorId INT not null,
  CreatorRoleId INT not null,
  WorkerId INT null,
  constraint PK_HelpPersonnelBillingRequest  primary key (Id)
)


--HelpBillingRole
CREATE TABLE [dbo].[HelpBillingRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Description] [nvarchar](256) NULL,
	[Code] [nvarchar](32) NULL,
	[DaysForApproval] [int] NULL,
 CONSTRAINT [PK_HelpBillingRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--HelpBillingRoleRecord
CREATE TABLE [dbo].[HelpBillingRoleRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[TargetUserId] [int] NULL,
	[TargetDepartmentId] [int] NULL,
 CONSTRAINT [PK_HelpBillingRoleRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--HelpBillingExecutorTasks
CREATE TABLE [dbo].[HelpBillingExecutorTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[UserId] [int] NULL,
	[HelpBillingId] [int] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_HelpBillingExecutorTasks_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_HelpBillingExecutorTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



--HelpBillingComments
CREATE TABLE [dbo].[HelpBillingComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[HelpBillingId] [int] NULL,
	[IsQuestion] [bit] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_HelpBillingComments_CreatedDate]  DEFAULT (getdate()),
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_HelpBillingComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

create index HelpPersonnelBillingRequest_HelpBillingTitle on HelpPersonnelBillingRequest (TitleId)
create index HelpPersonnelBillingRequest_HelpBillingUrgency on HelpPersonnelBillingRequest (UrgencyId)
create index HelpPersonnelBillingRequest_Department on HelpPersonnelBillingRequest (DepartmentId)
create index IX_HelpPersonnelBillingRequest_CreatorUser_Id on HelpPersonnelBillingRequest (CreatorId)
create index IX_HelpPersonnelBillingRequest_WorkerUser_Id on HelpPersonnelBillingRequest (WorkerId)
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_HelpBillingTitle foreign key (TitleId) references HelpBillingTitle
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_HelpBillingUrgency foreign key (UrgencyId) references HelpBillingUrgency
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_Department foreign key (DepartmentId) references Department
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_CreatorUser foreign key (CreatorId) references [Users]
alter table HelpPersonnelBillingRequest add constraint FK_HelpPersonnelBillingRequest_WorkerUser foreign key (WorkerId) references [Users]
GO
ALTER TABLE [dbo].[HelpBillingRoleRecord]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingRoleRecord_HelpBillingRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[HelpBillingRole] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord] CHECK CONSTRAINT [FK_HelpBillingRoleRecord_HelpBillingRole]
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingRoleRecord_TargetDepartment] FOREIGN KEY([TargetDepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord] CHECK CONSTRAINT [FK_HelpBillingRoleRecord_TargetDepartment]
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingRoleRecord_TargetUser] FOREIGN KEY([TargetUserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord] CHECK CONSTRAINT [FK_HelpBillingRoleRecord_TargetUser]
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingRoleRecord_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingRoleRecord] CHECK CONSTRAINT [FK_HelpBillingRoleRecord_User]
GO

ALTER TABLE [dbo].[HelpBillingExecutorTasks]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingExecutorTasks_HelpPersonnelBillingRequest] FOREIGN KEY([HelpBillingId])
REFERENCES [dbo].[HelpPersonnelBillingRequest] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingExecutorTasks] CHECK CONSTRAINT [FK_HelpBillingExecutorTasks_HelpPersonnelBillingRequest]
GO

ALTER TABLE [dbo].[HelpBillingExecutorTasks]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingExecutorTasks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingExecutorTasks] CHECK CONSTRAINT [FK_HelpBillingExecutorTasks_Users]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingExecutorTasks', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingExecutorTasks', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id сотрудника' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingExecutorTasks', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id задачи внутреннего биллинга' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingExecutorTasks', @level2type=N'COLUMN',@level2name=N'HelpBillingId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingExecutorTasks', @level2type=N'COLUMN',@level2name=N'CreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Связь исполнителей с задачами биллинга' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingExecutorTasks'
GO


ALTER TABLE [dbo].[HelpBillingComments]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingComments_HelpPersonnelBillingRequest] FOREIGN KEY([HelpBillingId])
REFERENCES [dbo].[HelpPersonnelBillingRequest] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingComments] CHECK CONSTRAINT [FK_HelpBillingComments_HelpPersonnelBillingRequest]
GO

ALTER TABLE [dbo].[HelpBillingComments]  WITH CHECK ADD  CONSTRAINT [FK_HelpBillingComments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[HelpBillingComments] CHECK CONSTRAINT [FK_HelpBillingComments_Users]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id пользователя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id задачи в внутреннем биллинге' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'HelpBillingId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Вопрос/ответ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'IsQuestion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'CreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Переписка сотрудников в задачах биллинга' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments'
GO


IF OBJECT_ID ('fnGetHelpBillingExecutorGroupForTask', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetHelpBillingExecutorGroupForTask]
GO
--функция по выдает список групп с пометками для заданной задачи
CREATE FUNCTION dbo.fnGetHelpBillingExecutorGroupForTask 
(	
	@HelpBillingId int	--ID задачи биллинга
	
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT cast(case when B.RoleId is not null then 1 else 0 end as bit) as IsRecipient, cast(case when B.RoleId is not null then 1 else 0 end as bit) as IsRecipientOld, A.Id as RoleId, A.[Description]
	FROM HelpBillingRole as A
	LEFT JOIN (SELECT distinct B.RoleId
				FROM HelpBillingExecutorTasks as A 
				INNER JOIN HelpBillingRoleRecord as B ON B.UserId = A.UserId
				WHERE A.HelpBillingId = @HelpBillingId) as B ON B.RoleId = A.Id

)
GO


IF OBJECT_ID ('fnGetHelpBillingExecutorForTask', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetHelpBillingExecutorForTask]
GO
--функция по заданному Id задачи внутреннего биллинга выдает список сотрудников 
CREATE FUNCTION dbo.fnGetHelpBillingExecutorForTask 
(	
	@HelpBillingId int,	--ID задачи биллинга
	@IsNew bit					--признак, создается задача или отправлена на выполнение
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT D.Id, --cast(case when exists (SELECT * FROM HelpBillingExecutorTasks WHERE HelpBillingId = @HelpBillingId and UserId = A.UserId) then 1 else 0 end as bit) as IsRecipient, 
				 cast(case when D.Id is not null then 1 else 0 end as bit) as IsRecipient, 
				 A.UserId, B.Name, A.RoleId, C.[Description]
	FROM HelpBillingRoleRecord as A
	INNER JOIN Users as B ON B.id = A.UserId
	INNER JOIN HelpBillingRole as C ON C.Id = A.RoleId
	LEFT JOIN HelpBillingExecutorTasks as D ON D.UserId = A.UserId and D.HelpBillingId = @HelpBillingId
	WHERE (case when D.Id is not null then 1 else 0 end) = (case when @IsNew = 1 then (case when D.Id is not null then 1 else 0 end) else 1 end)
)
GO




IF OBJECT_ID ('fnGetBillingTaskExecutorNames', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetBillingTaskExecutorNames]
GO


CREATE FUNCTION dbo.fnGetBillingTaskExecutorNames
(
	@HelpBillingId int
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @Names nvarchar(max)
	DECLARE @Tbl table (UserId int, Name nvarchar(150))

	INSERT INTO @Tbl 
	SELECT B.Id, B.Name
	FROM HelpBillingExecutorTasks as A
	INNER JOIN Users as B ON B.id = A.UserId
	WHERE A.HelpBillingId = @HelpBillingId 

	UPDATE @Tbl SET @Names = isnull(@Names, '') + case when @Names is not null then N', ' else N'' end + B.Name
	FROM @Tbl as A
	INNER JOIN Users as B ON B.id = A.UserId
	--WHERE A.HelpBillingId = @HelpBillingId 

	
	RETURN @Names

END
GO







IF OBJECT_ID ('vwHelpBillingComments', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwHelpBillingComments]
GO

--достаем комментарии к разделам приема
CREATE VIEW [dbo].[vwHelpBillingComments]
AS
SELECT B.Name as Creator, A.CreatedDate, A.Comment, A.IsQuestion, A.UserId, A.HelpBillingId
FROM HelpBillingComments as A
INNER JOIN Users as B ON B.Id = A.UserId





GO

 
delete from RequestNextNumber where RequestTypeId = 18


insert into [dbo].[HelpBillingUrgency] (Name, SortOrder) values (N'В текущем режиме',1)
insert into [dbo].[HelpBillingUrgency] (Name, SortOrder) values (N'Срочно',2)
insert into [dbo].[HelpBillingUrgency] (Name, SortOrder) values (N'Очень срочно',3)

-- select * from [dbo].[HelpBillingTitle]
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'2-НДФЛ Запрос',1)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Аванс',2)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Беременность ранние сроки_справка',3)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Больничный лист',4)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'ГПД  Акт',5)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'ДМС_Служ.записка',6)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Доход за два года Запрос/Справка',7)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Информация',8)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Исполнительный лист',9)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Командировка приказ',10)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Лицевые счета сотрудников',11)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Мат_помощь Приказ/Выплата',12)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Налоговая проверка',13)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'НДФЛ Возврат/заявление',14)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'НДФЛ Вычет Имущественный/Стандартный',15)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Отмена_Приказ',16)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Отпуск_Очередной/Другой/Приказ',17)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Отчет',18)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Отчетность',19)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Перерасчет/Удержания/СлужебнаяЗаписка',20)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Пособия',21)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Премия_Приказ/Выплата/Отмена',22)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Приказ изменение штатного',23)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Приказ Отпуск по беременности/По уходу',24)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Р/лист прошедшие года запрос',25)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Р/лист текущий год запрос',26)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Распровести документ',27)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Реестр  ',28)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Реестр отмена',29)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Реквизиты Банка',30)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'РПВ выложена/отмена',31)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'СВОД выложен/отмена',32)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Служба Занятости Запрос/Справка',33)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Сокращение Уведомление_Приказ',34)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Сотовая связь Служебная записка',35)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Справка по месту требования',36)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Справка ПредМестаРаботыСотр',37)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Справки уволенному',38)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Табель за месяц на ОП',39)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Тех.поддержка',40)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Увольнение_Приказ/Расчет/Табель',41)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Удержания',42)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Фитнес_Служ.Записка',43)




INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Аналитики')
INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Аудиторы')
INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Бухгалтерия')
INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Кадровики')
INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Программисты')
INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Расчетчики')
INSERT INTO HelpBillingRole(Version, [Description] ) VALUES(1, N'Юристы')

INSERT INTO HelpBillingRoleRecord(Version, UserId, RoleId)
select 1, id, case when id = 10 then 6 else 4 end from users where RoleId & 1048576 > 0 or id = 10
