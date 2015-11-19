--более новая версия содержится в скрипте создания структуры для штатного расписания в файле CreateSchemaStaffList.sql
USE [WebAppSKB]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'DateEdit'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'EditorId'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'DateCreate'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CreatorId'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'IsUsed'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatNumber'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatType'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildNumber'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildType'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseNumber'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseType'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'PostIndex'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Address'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Version'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Id'

GO

ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [FK_RefAddresses_Editors]
GO

ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [FK_RefAddresses_Creators]
GO

ALTER TABLE [dbo].[RefAddresses] DROP CONSTRAINT [DF_RefAddresses_IsUsed]
GO

/****** Object:  Table [dbo].[RefAddresses]    Script Date: 24.06.2015 12:43:29 ******/
DROP TABLE [dbo].[RefAddresses]
GO

/****** Object:  Table [dbo].[RefAddresses]    Script Date: 24.06.2015 12:43:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RefAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Address] [nvarchar](400) NULL,
	[PostIndex] [nvarchar](6) NULL,
	[RegionCode] [nvarchar](30) NULL,
	[RegionName] [nvarchar](50) NULL,
	[AreaCode] [nvarchar](30) NULL,
	[AreaName] [nvarchar](50) NULL,
	[CityCode] [nvarchar](30) NULL,
	[CityName] [nvarchar](50) NULL,
	[SettlementCode] [nvarchar](30) NULL,
	[SettlementName] [nvarchar](50) NULL,
	[StreetCode] [nvarchar](30) NULL,
	[StreetName] [nvarchar](50) NULL,
	[HouseType] [int] NULL,
	[HouseNumber] [nvarchar](10) NULL,
	[BuildType] [int] NULL,
	[BuildNumber] [nvarchar](10) NULL,
	[FlatType] [int] NULL,
	[FlatNumber] [nvarchar](5) NULL,
	[IsUsed] [bit] NULL,
	[CreatorId] [int] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[EditorId] [int] NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_RefAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RefAddresses] ADD  CONSTRAINT [DF_RefAddresses_IsUsed]  DEFAULT ((0)) FOR [IsUsed]
GO

ALTER TABLE [dbo].[RefAddresses]  WITH CHECK ADD  CONSTRAINT [FK_RefAddresses_Creators] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[RefAddresses] CHECK CONSTRAINT [FK_RefAddresses_Creators]
GO

ALTER TABLE [dbo].[RefAddresses]  WITH CHECK ADD  CONSTRAINT [FK_RefAddresses_Editors] FOREIGN KEY([EditorId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[RefAddresses] CHECK CONSTRAINT [FK_RefAddresses_Editors]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Адрес' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Почтовый индекс' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'PostIndex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код региона' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название региона' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'RegionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код района' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название района' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'AreaName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код города' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название города' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CityName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код населенного пункта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название населенного пункта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'SettlementName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код улицы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название улицы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'StreetName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип дом/владение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер дома/владения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'HouseNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип корпус/строение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер корпуса/строения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'BuildNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип квартира/офис' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер квартиры/офиса' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'FlatNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак использования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'IsUsed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id автора записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания записи' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'DateCreate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id редактора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'EditorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата последнего редактирования' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses', @level2type=N'COLUMN',@level2name=N'DateEdit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Справочник адресов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RefAddresses'
GO


