USE [TestExpress]
GO

/****** Object:  Table [dbo].[Fingrad_csv]    Script Date: 27.08.2015 14:41:05 ******/
DROP TABLE [dbo].[Fingrad_csv]
GO

/****** Object:  Table [dbo].[Fingrad_csv]    Script Date: 27.08.2015 14:41:05 ******/
SET ANSI_NULLS ON
GO

--����� �������� ������� � ���������� ����

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fingrad_csv](
	[���_�������������] [nvarchar](20) NULL,
	[�������_��������_�_����������] [nvarchar](50) NULL,
	[�������_���_�������������] [nvarchar](50) NULL,
	[�����������_������������] [nvarchar](250) NULL,
	[������] [nvarchar](6) NULL,
	[����������_�����] [nvarchar](50) NULL,
	[�������_���������] [nvarchar](50) NULL,
	[�����_���] [nvarchar](100) NULL,
	--[�����_��_����_��_���������_�_�������_�����] [nvarchar](500) NULL,
	[����_��������_�����] [datetime] NULL,
	[����_��������_�����] [datetime] NULL,
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
	[���_��_����������_�������] [int] NULL,
	[����_�������_������_������] [datetime] NULL,
	[�������������_�������������_������] [nvarchar](250) NULL,
	[���_��_����������_����������_�����] [int] NULL,
	[���_��_����������_����������_�_��������_�����] [int] NULL,
	[���_��_����������_����������_�_��������_����������] [int] NULL,
	[����_�������_���������_������] [datetime] NULL,
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
	[���_������_�����] [nvarchar](7) NULL,
	[����_������_�������_�����] [datetime] NULL,
	[����_�������������_������_�����] [datetime] NULL,
	--[J_�����_���������] [nvarchar](50) NULL,
	[���_GE] [nvarchar](50) NULL,
	--[���_Super�����] [nvarchar](50) NULL,
	--[���_����������_���_�_�_�_���_������_��] [nvarchar](50) NULL,
	[������������_���������] [nvarchar](50) NULL,
	[�����_������������_�������] [numeric](18, 2) NULL,
	[�������_�������������] [numeric](18, 2) NULL,
	--[������������_������] [nvarchar](500) NULL,
	--[����_���������] [datetime] NULL,
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


