--USE [WebAppTest]
--GO

/****** Object:  Table [dbo].[HelpBillingComments]    Script Date: 19.05.2015 12:01:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HelpBillingComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[HelpBillingId] [int] NULL,
	[IsQuestion] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_HelpBillingComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[HelpBillingComments] ADD  CONSTRAINT [DF_HelpBillingComments_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id пользовател€' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id задачи в внутреннем биллинге' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'HelpBillingId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¬опрос/ответ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'IsQuestion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ƒата создани€' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments', @level2type=N'COLUMN',@level2name=N'CreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ѕереписка сотрудников в задачах биллинга' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HelpBillingComments'
GO


