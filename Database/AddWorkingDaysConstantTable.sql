IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[WorkingDaysConstant]') AND name = N'IX_WorkingDaysConstantMonth')
	DROP INDEX [IX_WorkingDaysConstantMonth] ON [dbo].[WorkingDaysConstant] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkingDaysConstant]') AND type in (N'U'))
	DROP TABLE [dbo].[WorkingDaysConstant]
GO
/****** Object:  Table [dbo].[WorkingDaysConstant]    Script Date: 05/10/2013 09:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingDaysConstant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[Month] [datetime] NOT NULL,
	[Days] [int] NOT NULL,
	[Hours] [int] NOT NULL,
 CONSTRAINT [PK_WorkingDaysConstant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkingDaysConstantMonth] ON [dbo].[WorkingDaysConstant] 
(
	[Month] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO