--более новая версия содержится в скрипте создания структуры для штатного расписания в файле CreateSchemaStaffList.sql
USE [WebAppSKB]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Code'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'StreetCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'SettlementCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'CityCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AreaCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'RegionCode'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AddressType'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AltName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Index'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'ShortName'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Name'

GO

/****** Object:  Table [dbo].[Kladr]    Script Date: 24.06.2015 12:42:39 ******/
DROP TABLE [dbo].[Kladr]
GO

/****** Object:  Table [dbo].[Kladr]    Script Date: 24.06.2015 12:42:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kladr](
	[Name] [nvarchar](52) NULL,
	[ShortName] [nvarchar](12) NULL,
	[Index] [nvarchar](6) NULL,
	[AltName] [nvarchar](50) NULL,
	[AddressType] [nvarchar](1) NULL,
	[RegionCode] [nvarchar](2) NULL,
	[AreaCode] [nvarchar](3) NULL,
	[CityCode] [nvarchar](3) NULL,
	[SettlementCode] [nvarchar](3) NULL,
	[StreetCode] [nvarchar](4) NULL,
	[Code] [nvarchar](50) NULL
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название объекта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Краткое название типа объекта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'ShortName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Почтовый индекс объекта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Index'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Альтернативные названия объекта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AltName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип объекта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AddressType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код региона' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код района' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'AreaCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код города' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'CityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код населенного пункта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'SettlementCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код улицы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'StreetCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код объекта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Классификатор адресов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Kladr'
GO


