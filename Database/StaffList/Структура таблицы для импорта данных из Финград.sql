USE [TestExpress]
GO

/****** Object:  Table [dbo].[Fingrad_csv]    Script Date: 27.08.2015 12:24:56 ******/
DROP TABLE [dbo].[Fingrad_csv]
GO

/****** Object:  Table [dbo].[Fingrad_csv]    Script Date: 27.08.2015 12:24:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Fingrad_csv](
	[���_�������������] [nvarchar](50) NULL,
	[�������_��������_�_����������] [nvarchar](50) NULL,
	[�������_���_�������������] [nvarchar](50) NULL,
	[�����������_������������] [nvarchar](250) NULL,
	[������] [nvarchar](50) NULL,
	[����������_�����] [nvarchar](50) NULL,
	[�������_���������] [nvarchar](50) NULL,
	[�����_���] [nvarchar](100) NULL,
	--[�����_��_����_��_���������_�_�������_�����] [nvarchar](500) NULL,
	[����_��������_�����] [nvarchar](50) NULL,
	[����_��������_�����] [nvarchar](50) NULL,
	[���_�������������] [nvarchar](50) NULL,
	[���_��������] [nvarchar](50) NULL,
	--[���_���] [nvarchar](50) NULL,
	[���_���] [nvarchar](50) NULL,
	--[���_��������] [nvarchar](50) NULL,
	[��_��������] [nvarchar](250) NULL,
	[��_��������_��������] [nvarchar](50) NULL,
	[������_������] [nvarchar](250) NULL,
	[������_������_����������_��������] [nvarchar](250) NULL,
	[������_������_���_���] [nvarchar](250) NULL,
	[�������] [nvarchar](250) NULL,
	[������������_��] [nvarchar](max) NULL,
	[���_��_����������_�������] [nvarchar](50) NULL,
	[����_�������_������_������] [nvarchar](50) NULL,
	[�������������_�������������_������] [nvarchar](250) NULL,
	[���_��_����������_����������_�����] [nvarchar](50) NULL,
	[���_��_����������_����������_�_��������_�����] [nvarchar](50) NULL,
	[���_��_����������_����������_�_��������_����������] [nvarchar](50) NULL,
	[����_�������_���������_������] [nvarchar](50) NULL,
	[�������������_�������������_���������] [nvarchar](250) NULL,
	--[������������_��] [nvarchar](50) NULL,
	[����������] [nvarchar](50) NULL,
	[�������������_��������_��������] [nvarchar](50) NULL,
	[�_��������] [nvarchar](250) NULL,
	[�����_������_�����_�������_�_��] [nvarchar](500) NULL,
	[�����_������_�����] [nvarchar](500) NULL,
	[���������_�������_�����] [nvarchar](max) NULL,
	[���������_���������_����������] [nvarchar](max) NULL,
	[���������_��������_�������] [nvarchar](max) NULL,
	[���������_��������_������] [nvarchar](max) NULL,
	[���������_�����_������] [nvarchar](max) NULL,
	[�������_�����] [nvarchar](50) NULL,
	[��������] [nvarchar](max) NULL,
	[������������_��] [nvarchar](50) NULL,
	--[����������] [nvarchar](250) NULL,
	[���_������_�����] [nvarchar](50) NULL,
	[����_������_�������_�����] [nvarchar](50) NULL,
	[����_�������������_������_�����] [nvarchar](50) NULL,
	--[J_�����_���������] [nvarchar](50) NULL,
	[���_GE] [nvarchar](50) NULL,
	--[���_Super�����] [nvarchar](50) NULL,
	--[���_����������_���_�_�_�_���_������_��] [nvarchar](50) NULL,
	[������������_���������] [nvarchar](50) NULL,
	[�����_������������_�������] [nvarchar](50) NULL,
	[�������_�������������] [nvarchar](50) NULL,
	--[������������_������] [nvarchar](500) NULL,
	--[����_���������] [nvarchar](50) NULL,
	--[���_���������] [nvarchar](100) NULL,
	--[������_������������] [nvarchar](250) NULL,
	--[��������_��_��������] [nvarchar](50) NULL,
	--[����������_��������_������_������] [nvarchar](250) NULL,
	--[���������_��������] [nvarchar](250) NULL,
	--[�������������_�����] [nvarchar](250) NULL,
	--[���_��] [nvarchar](50) NULL,
	--[���_1�] [nvarchar](50) NULL,
	--[������_������_�����_���] [nvarchar](50) NULL,
	--[����������] [nvarchar](250) NULL,
	--[������������_�_���_��] [nvarchar](250) NULL,
	[�������������_��_�_���] [nvarchar](250) NULL,
	[�����_���_������_������] [nvarchar](50) NULL,
	[�����_������������_����������_��������] [nvarchar](50) NULL,
	[�����_������_���������] [nvarchar](250) NULL,
	[�����_������_Cash_in] [nvarchar](250) NULL,
	[���_����������_�����] [nvarchar](50) NULL,
	[Front_Back1] [nvarchar](50) NULL,
	[ID_��������] [nvarchar](50) NULL,
	--[���_���������] [nvarchar](50) NULL,
	[ID_����������_��������_����������_��������] [nvarchar](50) NULL,
	[��_��������_���_��_�_�������] [nvarchar](50) NULL,
	[������_������_ID_������_������] [nvarchar](50) NULL,
	[������_������_���_����������_���_�_�_�_���_������_��] [nvarchar](250) NULL,
	[�������������_�������������_������_��������] [nvarchar](50) NULL,
	[�������������_�������������_���������_��������] [nvarchar](50) NULL,
	[�������������_�������������_���������_���_��_�_�������] [nvarchar](50) NULL,
	[�������������_�������������_������_���_��_�_�������] [nvarchar](50) NULL
	--[�������������_��_��������_�����] [nvarchar](50) NULL,
	--[����������2] [nvarchar](50) NULL,
	--[���_��_�_�������_��_��������] [nvarchar](50) NULL,
	--[������_������_����������_��������1] [nvarchar](250) NULL,
	--[ID_������_������_������_������] [nvarchar](50) NULL,
	--[ID_��������_��������] [nvarchar](50) NULL,
	--[������_�������������] [nvarchar](50) NULL,
	--[���_�������������_Front_Back] [nvarchar](50) NULL,
	--[����������_����������_����������] [nvarchar](250) NULL,
	--[����������_���] [nvarchar](50) NULL,
	--[����������_���] [nvarchar](50) NULL,
	--[����������_�����] [nvarchar](250) NULL,
	--[������_������_��������] [nvarchar](50) NULL,
	
	
	
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


