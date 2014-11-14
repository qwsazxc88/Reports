CREATE TABLE [dbo].[ClearanceChecklistComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ClearanceChecklistId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Comment] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_ClearanceChecklistComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ClearanceChecklistComment]  WITH CHECK ADD  CONSTRAINT [FK_ClearanceChecklistComment_ClearanceChecklist] FOREIGN KEY([ClearanceChecklistId])
REFERENCES [dbo].[Dismissal] ([Id])
GO

ALTER TABLE [dbo].[ClearanceChecklistComment] CHECK CONSTRAINT [FK_ClearanceChecklistComment_ClearanceChecklist]
GO

ALTER TABLE [dbo].[ClearanceChecklistComment]  WITH CHECK ADD  CONSTRAINT [FK_ClearanceChecklistComment_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[ClearanceChecklistComment] CHECK CONSTRAINT [FK_ClearanceChecklistComment_User]
GO