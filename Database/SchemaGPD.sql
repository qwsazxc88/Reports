--use WebAppTest2
--go
--ВНИМАТЕЛЬНО ПРОЧИТАТЬ КОММЕНТАРИЙ
/*
	скрипт создает структуру для системы ГПД с занесением первичных данных в справочники.
	- Удаляется структура и создается новая (таблицы, ссылки, триггеры, представления, функции)
	- Также пересоздается справочник физических лиц (RefPeople)
	- заносятся только первичные данные в справочники ГПД, справочник физических лиц после запуска скрипта ОСТАНЕТСЯ ПУСТЫМ


--удаление всех таблиц ГПД включая справочник физических лиц
DROP TABLE [dbo].[GpdMagProlongation]
GO
DROP TABLE [dbo].[GpdМаgComments]
GO
DROP TABLE [dbo].[GpdAct]
GO
DROP TABLE [dbo].[GpdContract]
GO
DROP TABLE [dbo].[GpdDetailSets]
GO
DROP TABLE [dbo].[RefPeople]
GO
DROP TABLE [dbo].[GpdPermission]
GO
DROP TABLE [dbo].[GpdMenu]
GO
DROP TABLE [dbo].[GpdRefDetail]
GO
DROP TABLE [dbo].[GpdRefStatus]
GO
DROP TABLE [dbo].[GpdDetailType]
GO
DROP TABLE [dbo].[GpdChargingType]
GO
DROP TABLE [dbo].[GpdRefPaymentPeriod]
GO

*/

--ТАБЛИЦЫ
/****** Object:  Table [dbo].[GpdChargingType]    Script Date: 18.12.2014 12:20:37 ******/
--DROP TABLE [dbo].[GpdChargingType]
--GO

/****** Object:  Table [dbo].[GpdChargingType]    Script Date: 18.12.2014 12:20:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdRefPaymentPeriod](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_GpdRefPaymentPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefPaymentPeriod', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefPaymentPeriod', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник сроков оплат' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefPaymentPeriod'
GO








CREATE TABLE [dbo].[GpdChargingType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](10) NOT NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdChargingType_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_GpdChargingType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdChargingType', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название вида начисления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdChargingType', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdChargingType', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdChargingType', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник видов начисления (ГПД)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdChargingType'
GO








/****** Object:  Table [dbo].[GpdDetailType]    Script Date: 18.12.2014 12:20:52 ******/
--DROP TABLE [dbo].[GpdDetailType]
--GO

/****** Object:  Table [dbo].[GpdDetailType]    Script Date: 18.12.2014 12:20:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdDetailType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdDetailType_DateCreate]  DEFAULT (getdate()),
 CONSTRAINT [PK_GpdDetailType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailType', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Назвыание типа реквизитов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailType', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailType', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник типов реквизитов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailType'
GO







/****** Object:  Table [dbo].[GpdRefStatus]    Script Date: 18.12.2014 12:24:36 ******/
--DROP TABLE [dbo].[GpdRefStatus]
--GO

/****** Object:  Table [dbo].[GpdRefStatus]    Script Date: 18.12.2014 12:24:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdRefStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdRefStatus_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_GpdRefStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefStatus', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название статуса' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefStatus', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefStatus', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник статусов (ГПД)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefStatus'
GO







--ALTER TABLE [dbo].[GpdRefDetail] DROP CONSTRAINT [FK_GpdRefDetail_GpdDetailType]
--GO

/****** Object:  Table [dbo].[GpdRefDetail]    Script Date: 18.12.2014 12:25:44 ******/
--DROP TABLE [dbo].[GpdRefDetail]
--GO

/****** Object:  Table [dbo].[GpdRefDetail]    Script Date: 18.12.2014 12:25:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdRefDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL CONSTRAINT [DF_GpdRefDetail_Version]  DEFAULT ((1)),
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdRefDetail_CreateDate]  DEFAULT (getdate()),
	[CreatorID] [int] NULL,
	[EditDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[DTID] [int] NULL,
	[Name] [nvarchar](150) NULL,
	[ContractorName] [nvarchar](150) NULL,
	[INN] [nvarchar](12) NULL,
	[KPP] [nvarchar](9) NULL,
	[Account] [nvarchar](23) NULL,
	[PersonAccount] [nvarchar](23) NULL,
	[BankName] [nvarchar](100) NULL,
	[BankBIK] [nvarchar](9) NULL,
	[CorrAccount] [nvarchar](23) NULL,
	[Code] [nvarchar](9) NULL,
	[OldRecord] [bit] NULL,
 CONSTRAINT [PK_GpdRefDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdRefDetail]  WITH CHECK ADD  CONSTRAINT [FK_GpdRefDetail_GpdDetailType] FOREIGN KEY([DTID])
REFERENCES [dbo].[GpdDetailType] ([Id])
GO

ALTER TABLE [dbo].[GpdRefDetail] CHECK CONSTRAINT [FK_GpdRefDetail_GpdDetailType]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID создателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата последнего редактирования записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID редактора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID типа реквизитов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'DTID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование реквизита' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование контрагента' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'ContractorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИНН' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'INN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'КПП' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'KPP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Расчетный счет' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'Account'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Лицевой счет' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'PersonAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Банк' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'BankName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Банк БИК' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'BankBIK'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Банк кор/счет' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'CorrAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код банка' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник реквизитов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdRefDetail'
GO









/****** Object:  Table [dbo].[RefPeople]    Script Date: 18.12.2014 12:26:40 ******/
--DROP TABLE [dbo].[RefPeople]
--GO

/****** Object:  Table [dbo].[RefPeople]    Script Date: 18.12.2014 12:26:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RefPeople](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_RefPeople_CreateDate]  DEFAULT (getdate()),
	[Code] [nvarchar](10) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[SecondName] [nvarchar](50) NULL,
	[SNILS] [nvarchar](15) NULL,
	[DepCode] [nvarchar](10) NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_RefPeople_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_GpdRefPersons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Фамилия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'LastName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Имя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'FirstName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Отчество' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'SecondName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'СНИЛС' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'SNILS'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник физических лиц (ГПД)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак удаления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'IsDeleted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код подразделения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefPeople', @level2type=N'COLUMN',@level2name=N'DepCode'
GO





--DROP TABLE [dbo].[GpdDetailSets]
--GO

CREATE TABLE [dbo].[GpdDetailSets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL CONSTRAINT [DF_GpdDetailSets_Version]  DEFAULT ((1)),
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdDetailSets_CreateDate]  DEFAULT (getdate()),
	[CreatorID] [int] NULL,
	[EditDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[Name] [nvarchar](250) NULL,
	[PersonID] [int] NULL,
	[PayerID] [int] NULL,
	[PayeeID] [int] NULL,
	[Account] [nvarchar](25) NULL,
	[GUID] [nvarchar](40) NULL,
 CONSTRAINT [PK_GpdDetailSets] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdDetailSets]  WITH CHECK ADD  CONSTRAINT [FK_GpdDetailSets_GpdRefDetail] FOREIGN KEY([PayerID])
REFERENCES [dbo].[GpdRefDetail] ([Id])
GO

ALTER TABLE [dbo].[GpdDetailSets] CHECK CONSTRAINT [FK_GpdDetailSets_GpdRefDetail]
GO

ALTER TABLE [dbo].[GpdDetailSets]  WITH CHECK ADD  CONSTRAINT [FK_GpdDetailSets_GpdRefDetail1] FOREIGN KEY([PayeeID])
REFERENCES [dbo].[GpdRefDetail] ([Id])
GO

ALTER TABLE [dbo].[GpdDetailSets] CHECK CONSTRAINT [FK_GpdDetailSets_GpdRefDetail1]
GO

ALTER TABLE [dbo].[GpdDetailSets]  WITH CHECK ADD  CONSTRAINT [FK_GpdDetailSets_RefPeople] FOREIGN KEY([PersonID])
REFERENCES [dbo].[RefPeople] ([Id])
GO

ALTER TABLE [dbo].[GpdDetailSets] CHECK CONSTRAINT [FK_GpdDetailSets_RefPeople]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID создателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата последнего редактирования записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID редактора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID физического лица' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'PersonID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'PayerID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID получателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'PayeeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Расчетный/лицевой счет получателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets', @level2type=N'COLUMN',@level2name=N'Account'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник наборов реквизитов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdDetailSets'
GO






/****** Object:  Table [dbo].[GpdContract]    Script Date: 18.12.2014 12:37:03 ******/
--DROP TABLE [dbo].[GpdContract]
--GO

/****** Object:  Table [dbo].[GpdContract]    Script Date: 18.12.2014 12:37:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdContract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL CONSTRAINT [DF_GpdContract_Version]  DEFAULT ((1)),
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdContract_CreateDate]  DEFAULT (getdate()),
	[EditDate] [datetime] NULL,
	[CreatorID] [int] NULL,
	[EditorID] [int] NULL,
	[DepartmentId] [int] NULL,
	[PersonID] [int] NULL,
	[DSID] [int] NULL,
	[CTID] [int] NULL,
	[StatusID] [int] NULL,
	[NumContract] [nvarchar](30) NULL,
	[NameContract] [nvarchar](150) NULL,
	[DateBegin] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[GPDID] [nvarchar](20) NULL,
	[GPDContractID] [nvarchar](20) NULL,
	[PurposePayment] [nvarchar](600) NULL,
	[DateP] [datetime] NULL,
	[IsLong] [bit] NULL CONSTRAINT [DF_GpdContract_IsLong]  DEFAULT ((0)),
	[SendTo1C] [date] NULL,
	[PaymentPeriodID] [int] NULL,
	[Amount] [numeric](18, 2) NULL CONSTRAINT [DF_GpdContract_Amount]  DEFAULT ((0)),
	[GUID] [nvarchar](40) NULL,
	[PayerID] [int] NULL,
	[PayeeID] [int] NULL,
	[PAccountID] [int] NULL,
 CONSTRAINT [PK_GpdContract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdChargingType] FOREIGN KEY([CTID])
REFERENCES [dbo].[GpdChargingType] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdChargingType]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdDetailSets] FOREIGN KEY([DSID])
REFERENCES [dbo].[GpdDetailSets] ([ID])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdDetailSets]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdRefDetail] FOREIGN KEY([PayerID])
REFERENCES [dbo].[GpdRefDetail] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdRefDetail]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdRefDetail1] FOREIGN KEY([PayeeID])
REFERENCES [dbo].[GpdRefDetail] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdRefDetail1]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdRefDetail2] FOREIGN KEY([PAccountID])
REFERENCES [dbo].[GpdRefDetail] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdRefDetail2]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdRefPaymentPeriod] FOREIGN KEY([PaymentPeriodID])
REFERENCES [dbo].[GpdRefPaymentPeriod] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdRefPaymentPeriod]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_GpdRefStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[GpdRefStatus] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_GpdRefStatus]
GO

ALTER TABLE [dbo].[GpdContract]  WITH CHECK ADD  CONSTRAINT [FK_GpdContract_RefPeople] FOREIGN KEY([PersonID])
REFERENCES [dbo].[RefPeople] ([Id])
GO

ALTER TABLE [dbo].[GpdContract] CHECK CONSTRAINT [FK_GpdContract_RefPeople]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата последнего редактирования записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID создателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID редактора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID подразделения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID физического лица' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'PersonID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID набора реквизитов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'DSID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID вида начисления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'CTID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID статуса' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'StatusID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'NumContract'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'NameContract'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начала действия договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'DateBegin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата окончания дейстивя договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'DateEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID физического лица (ГПД) в ЭССД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'GPDID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID договора с физ. лицом (ГПД) в ЭССД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'GPDContractID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Назначение платежа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'PurposePayment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата пролонгации' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'DateP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак пролонгирования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'IsLong'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата выгрузки в 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'SendTo1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID срока оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'PaymentPeriodID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Стоимость' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'PayerID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID получателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'PayeeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID лицевого счета' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'COLUMN',@level2name=N'PAccountID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Договора (ГПД)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'плательщик' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'CONSTRAINT',@level2name=N'FK_GpdContract_GpdRefDetail'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'получатель' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'CONSTRAINT',@level2name=N'FK_GpdContract_GpdRefDetail1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'лицевой счет' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdContract', @level2type=N'CONSTRAINT',@level2name=N'FK_GpdContract_GpdRefDetail2'
GO









--ALTER TABLE [dbo].[GpdMagProlongation] DROP CONSTRAINT [FK_GpdMagProlongation_GpdContract]
--GO

/****** Object:  Table [dbo].[GpdMagProlongation]    Script Date: 18.12.2014 12:37:45 ******/
--DROP TABLE [dbo].[GpdMagProlongation]
--GO

/****** Object:  Table [dbo].[GpdMagProlongation]    Script Date: 18.12.2014 12:37:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdMagProlongation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GCID] [int] NULL,
	[DateP] [datetime] NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdMagProlongation_CreateDate]  DEFAULT (getdate()),
	[CreatorID] [int] NULL,
 CONSTRAINT [PK_GpdMagProlongation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdMagProlongation]  WITH CHECK ADD  CONSTRAINT [FK_GpdMagProlongation_GpdContract] FOREIGN KEY([GCID])
REFERENCES [dbo].[GpdContract] ([Id])
GO

ALTER TABLE [dbo].[GpdMagProlongation] CHECK CONSTRAINT [FK_GpdMagProlongation_GpdContract]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMagProlongation', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMagProlongation', @level2type=N'COLUMN',@level2name=N'GCID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата пролонгации' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMagProlongation', @level2type=N'COLUMN',@level2name=N'DateP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMagProlongation', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID создателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMagProlongation', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Журнал пролонгаций договоров (ГПД)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMagProlongation'
GO







--ALTER TABLE [dbo].[GpdAct] DROP CONSTRAINT [FK_GpdAct_GpdRefStatus]
--GO

--ALTER TABLE [dbo].[GpdAct] DROP CONSTRAINT [FK_GpdAct_GpdContract]
--GO

/****** Object:  Table [dbo].[GpdAct]    Script Date: 18.12.2014 12:38:39 ******/
--DROP TABLE [dbo].[GpdAct]
--GO

/****** Object:  Table [dbo].[GpdAct]    Script Date: 18.12.2014 12:38:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdAct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreatorID] [int] NULL,
	[EditDate] [datetime] NULL,
	[EditorID] [int] NULL,
	[ActNumber] [nvarchar](50) NULL,
	[ActDate] [datetime] NULL,
	[GCID] [int] NULL,
	[ChargingDate] [datetime] NULL,
	[DateBegin] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[Amount] [numeric](18, 2) NULL,
	[AmountPayment] [numeric](18, 2) NULL,
	[POrderDate] [datetime] NULL,
	[PurposePayment] [nvarchar](600) NULL,
	[ESSSNum] [nvarchar](20) NULL,
	[StatusID] [int] NULL,
	[SendTo1C] [datetime] NULL,
	[GUID] [nvarchar](40) NULL,
 CONSTRAINT [PK_GpdAct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdAct] ADD  CONSTRAINT [DF_GpdAct_Version]  DEFAULT ((1)) FOR [Version]
GO

ALTER TABLE [dbo].[GpdAct] ADD  CONSTRAINT [DF_GpdAct_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[GpdAct] ADD  CONSTRAINT [DF_GpdAct_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[GpdAct] ADD  CONSTRAINT [DF_GpdAct_AmountPayment]  DEFAULT ((0)) FOR [AmountPayment]
GO

ALTER TABLE [dbo].[GpdAct]  WITH CHECK ADD  CONSTRAINT [FK_GpdAct_GpdContract] FOREIGN KEY([GCID])
REFERENCES [dbo].[GpdContract] ([Id])
GO

ALTER TABLE [dbo].[GpdAct] CHECK CONSTRAINT [FK_GpdAct_GpdContract]
GO

ALTER TABLE [dbo].[GpdAct]  WITH CHECK ADD  CONSTRAINT [FK_GpdAct_GpdRefStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[GpdRefStatus] ([Id])
GO

ALTER TABLE [dbo].[GpdAct] CHECK CONSTRAINT [FK_GpdAct_GpdRefStatus]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID создателя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата последнего редактирования записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'EditDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID редактора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'EditorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ акта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'ActNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата акта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'ActDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'GCID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начисления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'ChargingDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начала периода оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'DateBegin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата конца периода оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'DateEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сумма в рублях' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'Amount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сумма к выплате (из 1С)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'AmountPayment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата платежного поручения (из 1С)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'POrderDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Назначение платежа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'PurposePayment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ заявки ЭССС' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'ESSSNum'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID статуса' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'StatusID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата выгрузки в 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct', @level2type=N'COLUMN',@level2name=N'SendTo1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Акты ГПД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdAct'
GO






--ALTER TABLE [dbo].[GpdМаgComments] DROP CONSTRAINT [FK_GpdМаgComments_Users]
--GO

--ALTER TABLE [dbo].[GpdМаgComments] DROP CONSTRAINT [FK_GpdМаgComments_GpdAct]
--GO

/****** Object:  Table [dbo].[GpdМаgComments]    Script Date: 18.12.2014 12:39:52 ******/
--DROP TABLE [dbo].[GpdМаgComments]
--GO

/****** Object:  Table [dbo].[GpdМаgComments]    Script Date: 18.12.2014 12:39:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdМаgComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL CONSTRAINT [DF_GpdМаgComments_Version]  DEFAULT ((1)),
	[UserId] [int] NULL,
	[ActId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Comment] [nvarchar](500) NULL,
 CONSTRAINT [PK_GpdМаgComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdМаgComments]  WITH CHECK ADD  CONSTRAINT [FK_GpdМаgComments_GpdAct] FOREIGN KEY([ActId])
REFERENCES [dbo].[GpdAct] ([Id])
GO

ALTER TABLE [dbo].[GpdМаgComments] CHECK CONSTRAINT [FK_GpdМаgComments_GpdAct]
GO

ALTER TABLE [dbo].[GpdМаgComments]  WITH CHECK ADD  CONSTRAINT [FK_GpdМаgComments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[GpdМаgComments] CHECK CONSTRAINT [FK_GpdМаgComments_Users]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID сотрудника' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID акта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments', @level2type=N'COLUMN',@level2name=N'ActId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Комментарий' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments', @level2type=N'COLUMN',@level2name=N'Comment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Журнал комментариев ГПД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdМаgComments'
GO






/****** Object:  Table [dbo].[GpdMenu]    Script Date: 18.12.2014 12:41:16 ******/
--DROP TABLE [dbo].[GpdMenu]
--GO

/****** Object:  Table [dbo].[GpdMenu]    Script Date: 18.12.2014 12:41:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdMenu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuItemName] [nvarchar](50) NULL,
 CONSTRAINT [PK_GpdMenu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMenu', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название пункта меню' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMenu', @level2type=N'COLUMN',@level2name=N'MenuItemName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник пунктов меню ГПД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdMenu'
GO








--ALTER TABLE [dbo].[GpdPermission] DROP CONSTRAINT [FK_GpdPermission_Role]
--GO

--ALTER TABLE [dbo].[GpdPermission] DROP CONSTRAINT [FK_GpdPermission_GpdMenu]
--GO

/****** Object:  Table [dbo].[GpdPermission]    Script Date: 18.12.2014 12:41:49 ******/
--DROP TABLE [dbo].[GpdPermission]
--GO

/****** Object:  Table [dbo].[GpdPermission]    Script Date: 18.12.2014 12:41:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GpdPermission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NULL,
	[RoleID] [int] NULL,
	[IsCreate] [bit] NULL CONSTRAINT [DF_GpdPermission_IsCreate]  DEFAULT ((0)),
	[IsDraft] [bit] NULL CONSTRAINT [DF_GpdPermission_IsDraft]  DEFAULT ((0)),
	[IsWrite] [bit] NULL CONSTRAINT [DF_GpdPermission_IsWrite]  DEFAULT ((0)),
	[IsCancel] [bit] NULL CONSTRAINT [DF_GpdPermission_IsCancel]  DEFAULT ((0)),
	[IsComment] [bit] NULL CONSTRAINT [DF_GpdPermission_IsComment]  DEFAULT ((0)),
	[IsCreateAct] [bit] NULL CONSTRAINT [DF_GpdPermission_IsCreateAct]  DEFAULT ((0)),
 CONSTRAINT [PK_GpdPermission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GpdPermission]  WITH CHECK ADD  CONSTRAINT [FK_GpdPermission_GpdMenu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[GpdMenu] ([ID])
GO

ALTER TABLE [dbo].[GpdPermission] CHECK CONSTRAINT [FK_GpdPermission_GpdMenu]
GO

ALTER TABLE [dbo].[GpdPermission]  WITH CHECK ADD  CONSTRAINT [FK_GpdPermission_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([Id])
GO

ALTER TABLE [dbo].[GpdPermission] CHECK CONSTRAINT [FK_GpdPermission_Role]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID пункта меню' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'MenuID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID роли пользователей' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'RoleID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Досту к созданию записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'IsCreate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Доступ к сохранению черновика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'IsDraft'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Доступ к сохранению записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'IsWrite'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Доступ к (отмене) отклонению' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'IsCancel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Доступ к созданию комментариев' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'IsComment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Доступ к созданию актов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission', @level2type=N'COLUMN',@level2name=N'IsCreateAct'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Служебная таблица для управления доступами ГПД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GpdPermission'
GO




--ТРИГГЕРЫ
/*
IF OBJECT_ID ('updGpdContractSetStatus', 'TR') IS NOT NULL
   DROP TRIGGER updGpdContractSetStatus;
GO

--при выгрузке дат из 1С проставляем статус
CREATE TRIGGER updGpdContractSetStatus
ON dbo.GpdContract
AFTER INSERT, UPDATE 
AS
	IF EXISTS(SELECT * FROM GpdContract as A
						INNER JOIN inserted as B ON B.id = A.id
						WHERE A.SendTo1C is not null and isnull(A.StatusID, 0) <> 1)
	BEGIN
		UPDATE GpdContract SET StatusID = 1 
		FROM GpdContract as A
		INNER JOIN inserted as B ON B.id = A.id
	END
	
GO


IF OBJECT_ID ('updGpdActSetStatus', 'TR') IS NOT NULL
   DROP TRIGGER updGpdActSetStatus;
GO

--при выгрузке дат из 1С проставляем статус
CREATE TRIGGER updGpdActSetStatus
ON dbo.GpdAct
AFTER INSERT, UPDATE 
AS
	IF EXISTS(SELECT * FROM GpdAct as A
						INNER JOIN inserted as B ON B.id = A.id
						WHERE A.SendTo1C is not null and isnull(A.StatusID, 0) <> 1)
	BEGIN
		UPDATE GpdAct SET StatusID = 1 
		FROM GpdAct as A
		INNER JOIN inserted as B ON B.id = A.id
	END
	
GO
*/




--ПЕРВИЧНЫЕ ДАННЫЕ
IF NOT EXISTS (SELECT * FROM dbo.GpdDetailType)
BEGIN
	insert into dbo.GpdDetailType ([Name]) values ('Получатель')
	insert into dbo.GpdDetailType ([Name]) values ('Плательщик')
	--insert into dbo.GpdDetailType ([Name]) values ('Фактический получатель')
END

IF NOT EXISTS (SELECT * FROM dbo.GpdRefStatus)
BEGIN
	insert into dbo.GpdRefStatus (name) values('Выгружено в 1С')
	insert into dbo.GpdRefStatus (name) values('Записано')
	insert into dbo.GpdRefStatus (name) values('Отклонено')
	insert into dbo.GpdRefStatus (name) values('Черновик')
END

IF NOT EXISTS (SELECT * FROM dbo.GpdChargingType)
BEGIN
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам подряда#3601', '3601')
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам подряда с ФСС#3602', '3602')
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам подряда (не уменьшающая НОБ)#3610', '3610')
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам аренды (ГПД) для возмещения найма жилья сотрудникам#3612', '3612')
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам аренды транспортного средства#3605', '3605')
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам аренды имущества#3607', '3607')
	insert into dbo.GpdChargingType (name, code) values ('Оплата по договорам аренды имущества (не уменьшающая НОБ)#3611', '3611')
	insert into dbo.GpdChargingType (name, code) values ('Подарки в денежной форме Ф.Л.(ГПД)#3613', '3613')
	insert into dbo.GpdChargingType (name, code) values ('Подарки в неденежной форме Ф.Л.(ГПД)#3614', '3614')
END


IF NOT EXISTS (SELECT * FROM dbo.GpdMenu)
BEGIN
	insert into dbo.GpdMenu (MenuItemName) values ('Справочник реквизитов')
	insert into dbo.GpdMenu (MenuItemName) values ('Договор')
	insert into dbo.GpdMenu (MenuItemName) values ('Акт')
END


IF NOT EXISTS (SELECT * FROM dbo.GpdPermission)
BEGIN
	--по количеству пунктов в меню
	--эти записи по просмотру
	insert into dbo.GpdPermission (MenuID, RoleID) SELECT 1, Id FROM Role WHERE Id in (2, 4, 8, 32)
	insert into dbo.GpdPermission (MenuID, RoleID) SELECT 2, Id FROM Role WHERE Id in (2, 4, 8, 32)
	insert into dbo.GpdPermission (MenuID, RoleID) SELECT 3, Id FROM Role WHERE Id in (2, 4, 8, 32)

	--бухгалтеры
	insert into dbo.GpdPermission (MenuID, RoleID, IsCreate, IsDraft, IsWrite, IsCancel, IsComment, IsCreateAct) 
	SELECT Id, 256, 1, 1, 1, 1, 1, 1 FROM dbo.GpdMenu 
END


IF NOT EXISTS (SELECT * FROM dbo.GpdRefPaymentPeriod)
BEGIN
	INSERT INTO dbo.GpdRefPaymentPeriod (Name) VALUES ('Ежемесячно')
	INSERT INTO dbo.GpdRefPaymentPeriod (Name) VALUES ('Однократно в конце срока')
END
GO



--ПРЕДСТАВЛЕНИЯ
IF OBJECT_ID ('vwGpdContractList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdContractList]
GO

CREATE VIEW [dbo].[vwGpdContractList]
AS
SELECT A.Id as [Id], A.CreatorID, A.DepartmentId, G.Name AS DepartmentName, 
			 A.PersonID, B.LastName + ' ' + B.FirstName + ' ' + B.SecondName AS Surname, A.CTID, 
       C.Name AS CTName, A.StatusID, E.Name AS StatusName, A.NumContract, A.NameContract, A.DateBegin, isnull(K.DateP, A.DateEnd) as DateEnd, 
			 cast(case when datediff(d, isnull(K.DateP, A.DateEnd), getdate()) >= -30 and datediff(d, isnull(K.DateP, A.DateEnd), getdate()) <= 1180 then 1 else 0 end as bit) as flgRed,
			 A.GPDID, A.GPDContractID, A.DSID, 
       A.PurposePayment, 'Договор ГПХ # ' + isnull(H.PersonAccount, J.Account) + ' ## ' + B.LastName + ' ' + B.FirstName + ' ' + B.SecondName + ' *' as PurposePaymentPart,
			 --A.DateP as DateP, 
			 null as DateP,
			 isnull(A.DateP, A.DateEnd) AS DatePOld, A.IsLong, F.Name AS CreatorName, A.CreateDate, F.Name AS Autor, dep3.Name AS DepLevel3Name, G.Name AS DepLevel7Name, 
			 --плательщик
			 A.PayerID, I.[Name] as PayerName, I.INN as PayerINN, I.KPP as PayerKPP, I.Account as PayerAccount, I.BankName as PayerBankName, I.BankBIK as PayerBankBIK, I.CorrAccount as PayerCorrAccount,
			 --null as PayerContractor,
			 I.ContractorName as PayerContractor,--новое поле
			 --получатель
			 A.PayeeID, J.[Name] as PayeeName, J.INN as PayeeINN, J.KPP as PayeeKPP, J.Account as PayeeAccount, J.BankName as PayeeBankName, J.BankBIK as PayeeBankBIK, J.CorrAccount as PayeeCorrAccount,
			 --null as PayeeContractor,
			 J.ContractorName as PayeeContractor,--новое поле
			 A.PaymentPeriodID, A.Amount, A.PAccountID, H.PersonAccount as Account, H.Name as PAName,
			 H.Name + case when H.PersonAccount is not null then ' (лицевой счет: ' + H.PersonAccount else ' (расчетный счет: ' + H.Account end + ')' as PersonAccount,
			 A.SendTo1C
FROM dbo.GpdContract AS A 
		 LEFT JOIN dbo.RefPeople AS B ON B.Id = A.PersonID 
		 LEFT JOIN dbo.GpdChargingType AS C ON C.Id = A.CTID 
		 LEFT JOIN dbo.GpdRefStatus AS E ON E.Id = A.StatusID 
		 LEFT JOIN dbo.Users AS F ON F.Id = A.CreatorID 
		 LEFT JOIN dbo.Department AS G ON G.Id = A.DepartmentId 
		 LEFT JOIN dbo.Department as dep3 ON G.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
		 --INNER JOIN dbo.GpdDetailSets as H ON H.ID = A.DSID
		 --INNER JOIN dbo.GpdRefDetail as I ON I.Id = H.PayerID
		 --INNER JOIN dbo.GpdRefDetail as J ON J.id = H.PayeeID
		 LEFT JOIN dbo.GpdRefDetail as I ON I.Id = A.PayerID
		 LEFT JOIN dbo.GpdRefDetail as J ON J.id = A.PayeeID
		 LEFT JOIN dbo.GpdRefDetail as H ON H.Id = A.PAccountID
		 LEFT JOIN (SELECT GCID, max(DateP) as DateP FROM dbo.GpdMagProlongation GROUP BY GCID) as K ON K.GCID = A.Id

GO


IF OBJECT_ID ('vwGpdActList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdActList]
GO

CREATE VIEW [dbo].[vwGpdActList]
AS
--список актов
SELECT A.Id, A.CreatorID, A.ActDate, A.ActNumber, isnull(C.Gcount, 0) as GCCount,
			 B.PersonID, D.LastName + ' ' + D.FirstName + ' ' + D.SecondName AS Surname,
			 B.NameContract, B.NumContract, B.DateBegin as ContractBeginDate, isnull(K.DateP, B.DateEnd) as ContractEndDate,
			 E.Name AS CreatorName, A.CreateDate, dep3.Name AS DepLevel3Name, A.ChargingDate, A.DateBegin, A.DateEnd,
			 A.Amount, isnull(A.AmountPayment, 0) as AmountPayment, A.POrderDate, A.PurposePayment, A.ESSSNum, A.StatusID, G.Name AS StatusName, B.Id as GCID, 
			 cast(case when datediff(d, isnull(K.DateP, B.DateEnd), getdate()) >= -30 and datediff(d, isnull(K.DateP, B.DateEnd), getdate()) <= 1180 then 1 else 0 end as bit) as flgRed,
			 H.[Name] as CTName, H.Id as CTType, A.SendTo1C, B.DateP, F.[Name] as DepLevel7Name, B.GPDID, B.DepartmentId,
			 --плательщик
			 B.PayerID, M.ContractorName as PayerName, M.INN as PayerINN, M.KPP as PayerKPP, M.Account as PayerAccount, M.BankName as PayerBankName, M.BankBIK as PayerBankBIK, M.CorrAccount as PayerCorrAccount,
			 --получатель
			 B.PayeeID, N.ContractorName as PayeeName, N.INN as PayeeINN, N.KPP as PayeeKPP, N.Account as PayeeAccount, N.BankName as PayeeBankName, N.BankBIK as PayeeBankBIK, N.CorrAccount as PayeeCorrAccount, 
			 B.PAccountID, L.PersonAccount as Account, L.Name as PAName
FROM dbo.GpdAct as A
INNER JOIN dbo.GpdContract as B ON B.Id = A.GCID
LEFT JOIN (SELECT GCID, count(GCID) as Gcount FROM dbo.GpdAct GROUP BY GCID) as C ON C.GCID = A.GCID
INNER JOIN dbo.RefPeople AS D ON D.Id = B.PersonID
INNER JOIN dbo.Users AS E ON E.Id = A.CreatorID 
INNER JOIN dbo.Department AS F ON F.Id = B.DepartmentId 
LEFT JOIN dbo.Department as dep3 ON F.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
INNER JOIN dbo.GpdRefStatus AS G ON G.Id = A.StatusID
INNER JOIN dbo.GpdChargingType as H ON H.Id = B.CTID
LEFT JOIN (SELECT GCID, max(DateP) as DateP FROM dbo.GpdMagProlongation GROUP BY GCID) as K ON K.GCID = B.Id
--INNER JOIN dbo.GpdDetailSets as L ON L.ID = A.DSID
INNER JOIN dbo.GpdRefDetail as M ON M.Id = B.PayerID
INNER JOIN dbo.GpdRefDetail as N ON N.Id = B.PayeeID
LEFT JOIN dbo.GpdRefDetail as L ON L.Id = B.PAccountID


GO


IF OBJECT_ID ('vwGpdActNew', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdActNew]
GO

CREATE VIEW [dbo].[vwGpdActNew]
AS
--для создания нового акта ГПД
SELECT A.Id, A.CreatorID, A.ActDate, A.ActNumber, isnull(C.Gcount, 0) + 1 as GCCount,
			 B.PersonID, D.LastName + ' ' + D.FirstName + ' ' + D.SecondName AS Surname,
			 B.NameContract, B.NumContract, B.DateBegin as ContractBeginDate, isnull(K.DateP, B.DateEnd) as ContractEndDate,
			 E.Name AS CreatorName, A.CreateDate, dep3.Name AS DepLevel3Name, A.ChargingDate, A.DateBegin, A.DateEnd,
			 A.Amount, 0 as AmountPayment, A.POrderDate, 
			 'Договор ГПХ # ' + isnull(L.PersonAccount, J.Account) + ' ## ' + D.LastName + ' ' + D.FirstName + ' ' + D.SecondName + ' * ' + B.PurposePayment as PurposePayment, 
			 A.ESSSNum, G.Id as StatusID, G.Name AS StatusName, B.Id as GCID, 
			 cast(case when datediff(d, isnull(K.DateP, B.DateEnd), getdate()) >= -30 and datediff(d, isnull(K.DateP, B.DateEnd), getdate()) <= 1180 then 1 else 0 end as bit) as flgRed,
			 H.[Name] as CTName, H.Id as CTType, A.SendTo1C, B.DateP, F.[Name] as DepLevel7Name, B.GPDID, B.DepartmentId,
			 --плательщик
			 B.PayerID, I.ContractorName as PayerName, I.INN as PayerINN, I.KPP as PayerKPP, I.Account as PayerAccount, I.BankName as PayerBankName, I.BankBIK as PayerBankBIK, I.CorrAccount as PayerCorrAccount,
			 --получатель
			 B.PayeeID, J.ContractorName as PayeeName, J.INN as PayeeINN, J.KPP as PayeeKPP, J.Account as PayeeAccount, J.BankName as PayeeBankName, J.BankBIK as PayeeBankBIK, J.CorrAccount as PayeeCorrAccount, 
			 B.PAccountID, L.PersonAccount as Account, L.Name as PAName
FROM dbo.GpdContract as B
LEFT JOIN dbo.GpdAct as A ON B.Id = A.GCID and B.Id = -1
LEFT JOIN (SELECT GCID, count(GCID) as Gcount FROM dbo.GpdAct GROUP BY GCID) as C ON C.GCID = B.ID
INNER JOIN dbo.RefPeople AS D ON D.Id = B.PersonID
LEFT JOIN dbo.Users AS E ON E.Id = A.CreatorID 
INNER JOIN dbo.Department AS F ON F.Id = B.DepartmentId 
LEFT JOIN dbo.Department as dep3 ON F.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
LEFT JOIN dbo.GpdRefStatus AS G ON G.Id = isnull(A.StatusID, 4)
INNER JOIN dbo.GpdChargingType as H ON H.Id = B.CTID
LEFT JOIN (SELECT GCID, max(DateP) as DateP FROM dbo.GpdMagProlongation GROUP BY GCID) as K ON K.GCID = B.Id
--INNER JOIN dbo.GpdDetailSets as J ON J.ID = B.DSID
--INNER JOIN dbo.GpdRefDetail as I ON I.Id = J.PayerID
--INNER JOIN dbo.GpdRefDetail as L ON L.Id = J.PayeeID
INNER JOIN dbo.GpdRefDetail as I ON I.Id = B.PayerID
INNER JOIN dbo.GpdRefDetail as J ON J.id = B.PayeeID
LEFT JOIN dbo.GpdRefDetail as L ON L.Id = B.PAccountID

GO



IF OBJECT_ID ('vwGpdStatus', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdStatus]
GO

CREATE VIEW [dbo].[vwGpdStatus]
AS
SELECT 0 as Id, 'Все' as Name
UNION ALL
SELECT Id as Id, Name as Name FROM dbo.GpdRefStatus WHERE Id <> 3
GO


IF OBJECT_ID ('vwGpdChargingType', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdChargingType]
GO

CREATE VIEW [dbo].[vwGpdChargingType]
AS
SELECT 0 as Id, 'Все' as Name
UNION ALL
SELECT Id as Id, Name as Name FROM dbo.GpdChargingType
GO



IF OBJECT_ID ('vwGpdRefPersons', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdRefPersons]
GO

CREATE VIEW [dbo].[vwGpdRefPersons]
AS
SELECT A.Id as Id, isnull(A.LastName, '') + ' ' + isnull(A.FirstName, '') + ' ' + isnull(A.SecondName, '') /*+ ' - ' + SNILS*/ as Name, A.SNILS, A.DepCode, A.IsDeleted,
			 isnull(A.LastName, '') + ' ' + isnull(A.FirstName, '') + ' ' + isnull(A.SecondName, '') + ' (' + isnull(B.Name, A.SNILS) + ')' as LongName
FROM RefPeople as A
LEFT JOIN dbo.Department as B ON B.CodeOld = A.DepCode
--ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS
GO


IF OBJECT_ID ('vwGpdRefDetail', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdRefDetail]
GO

CREATE VIEW [dbo].[vwGpdRefDetail]
AS

SELECT NULL AS Id, NULL AS Version, NULL AS CreateDate, NULL AS CreatorID, NULL AS EditDate, NULL AS EditorID, 2 AS DTID, N'Выберите плательщика' AS Name, NULL AS INN, NULL AS KPP, NULL AS Account, 
			 NULL AS BankName, NULL AS BankBIK, NULL AS CorrAccount, NULL AS code, null as Priority
UNION ALL
SELECT NULL AS Id, NULL AS Version, NULL AS CreateDate, NULL AS CreatorID, NULL AS EditDate, NULL AS EditorID, 1 AS DTID, N'Выберите получателя' AS Name, NULL AS INN, NULL AS KPP, NULL AS Account, 
			NULL AS BankName, NULL AS BankBIK, NULL AS CorrAccount, NULL AS code, null as Priority
UNION ALL
SELECT Id, Version, CreateDate, CreatorID, EditDate, EditorID, DTID, Name, INN, KPP, Account, BankName, BankBIK, CorrAccount, code, Name as Priority
FROM GpdRefDetail


--SELECT * FROM GpdRefDetail

GO


IF OBJECT_ID ('vwGpdDetailList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdDetailList]
GO

CREATE VIEW [dbo].[vwGpdDetailList]
AS
	SELECT Id, Name, Name + case when PersonAccount is not null then ' (лицевой счет: ' + PersonAccount else ' (расчетный счет: ' + Account end + ')' as LongName, 
				 ContractorName, INN, KPP, Account, PersonAccount, BankName, BankBIK, CorrAccount 
	FROM GpdRefDetail
	WHERE DTID is null or DTID <> 1

GO



IF OBJECT_ID ('vwGpdRefDetailList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdRefDetailList]
GO

CREATE VIEW [dbo].[vwGpdRefDetailList]
AS

SELECT A.Id, A.Name, A.DTID, A.INN, A.KPP, A.Account, A.BankName, A.BankBIK, A.CorrAccount --, A.Code, 
/*
			 A.CreatorID, A.CreateDate, B.[Name] as CreatorName, C.[Name] as CreatePositionName, D.[Name] as CrDep7Level, Crdep3.[Name] as CrDep3Level,
			 A.EditorID, A.EditDate, E.[Name] as EditorName, F.[Name] as EditPositionName, G.[Name] as EDep7Level, Edep3.[Name] as EDep3Level
			 */
FROM dbo.GpdRefDetail as A
/*
LEFT JOIN dbo.Users as B ON B.Id = A.CreatorID
LEFT JOIN dbo.Position as C ON C.Id = B.PositionId
LEFT JOIN dbo.Department AS D ON D.Id = B.DepartmentId 
LEFT JOIN dbo.Department as Crdep3 ON D.[Path] like Crdep3.[Path] + N'%' and Crdep3.ItemLevel = 3 
LEFT JOIN dbo.Users as E ON E.Id = A.EditorID
LEFT JOIN dbo.Position as F ON F.Id = E.PositionId
LEFT JOIN dbo.Department AS G ON G.Id = E.DepartmentId 
LEFT JOIN dbo.Department as Edep3 ON G.[Path] like Edep3.[Path] + N'%' and Edep3.ItemLevel = 3 
*/
GO

IF OBJECT_ID ('vwGpdDetailSetList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdDetailSetList]
GO

CREATE VIEW [dbo].[vwGpdDetailSetList]
AS

SELECT A.ID, A.Name, A.PersonID, B.Name as Surname, A.PayerID, C.Name as PayerName, A.PayeeID, D.Name as PayeeName, A.Account,
			 A.CreatorID, A.CreateDate, E.[Name] as CreatorName,
			 A.EditorID, A.EditDate, F.[Name] as EditorName, B.LongName, B.SNILS,--, D.Account as PayeeAccount
			 C.INN as PayerINN, C.KPP as PayerKPP, C.Account as PayerAccount, C.BankName as PayerBankName, C.BankBIK as PayerBankBIK, C.CorrAccount as PayerCorrAccount,
			 D.INN as PayeeINN, D.KPP as PayeeKPP, D.Account as PayeeAccount, D.BankName as PayeeBankName, D.BankBIK as PayeeBankBIK, D.CorrAccount as PayeeCorrAccount/*,
			 cast(case when exists (SELECT * FROM GpdContract WHERE DSID = A.ID and StatusID = 1) or
										  exists (SELECT * FROM GpdAct WHERE DSID = A.ID and StatusID = 1) 
								 then 0 else 1 end as bit) as AllowEdit*/
FROM dbo.GpdDetailSets as A
INNER JOIN dbo.vwGpdRefPersons as B ON B.Id = A.PersonID
INNER JOIN dbo.GpdRefDetail as C ON C.Id = A.PayerID
INNER JOIN dbo.GpdRefDetail as D ON D.Id = A.PayeeID
LEFT JOIN dbo.Users as E ON E.Id = A.CreatorID
LEFT JOIN dbo.Users as F ON F.Id = A.EditorID

GO


IF OBJECT_ID ('vwGpdActComments', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdActComments]
GO

CREATE VIEW [dbo].[vwGpdActComments]
AS
SELECT A.Id, A.UserId, A.ActId, A.Comment, A.CreateDate, B.Name as Creator
FROM dbo.GpdМаgComments as A
INNER JOIN Users as B ON B.Id = A.UserId

GO



IF OBJECT_ID ('vwGpdPermission', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdPermission]
GO

CREATE VIEW [dbo].[vwGpdPermission]
AS
SELECT A.RoleID, A.IsCreate, A.IsDraft, A.IsWrite, A.IsCancel, A.IsComment, A.IsCreateAct
FROM GpdPermission as A
INNER JOIN GpdMenu as B ON B.ID = A.MenuID

GO



IF OBJECT_ID ('vwGpdPaymentPeriod', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdPaymentPeriod]
GO

CREATE VIEW [dbo].[vwGpdPaymentPeriod]
AS
SELECT * FROM dbo.GpdRefPaymentPeriod

GO



