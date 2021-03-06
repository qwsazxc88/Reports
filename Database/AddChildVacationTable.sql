IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacationComment_ChildVacation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]'))
ALTER TABLE [dbo].[ChildVacationComment] DROP CONSTRAINT [FK_ChildVacationComment_ChildVacation]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacationComment_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]'))
ALTER TABLE [dbo].[ChildVacationComment] DROP CONSTRAINT [FK_ChildVacationComment_User]
GO
--IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_ChildVacationType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
--ALTER TABLE [dbo].[ChildVacation] DROP CONSTRAINT [FK_ChildVacation_ChildVacationType]
--GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation] DROP CONSTRAINT [FK_ChildVacation_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation] DROP CONSTRAINT [FK_ChildVacation_TimesheetStatus]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_CreatorUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation] DROP CONSTRAINT [FK_ChildVacation_CreatorUser]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__ChildVacation__Delete__Default]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChildVacation] DROP CONSTRAINT [DF__ChildVacation__Delete__0E39E7EC]
END

GO
/****** Object:  Index [IX_ChildVacationComment_ChildVacation_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]') AND name = N'IX_ChildVacationComment_ChildVacation_Id')
DROP INDEX [IX_ChildVacationComment_ChildVacation_Id] ON [dbo].[ChildVacationComment]
GO
/****** Object:  Index [IX_ChildVacationComment_User_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]') AND name = N'IX_ChildVacationComment_User_Id')
DROP INDEX [IX_ChildVacationComment_User_Id] ON [dbo].[ChildVacationComment]
GO
/****** Object:  Index [ChildVacation_ChildVacationType]    Script Date: 15.07.2013 9:27:36 ******/
--IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'ChildVacation_ChildVacationType')
--DROP INDEX [ChildVacation_ChildVacationType] ON [dbo].[ChildVacation]
--GO
/****** Object:  Index [ChildVacation_TimesheetStatus]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'ChildVacation_TimesheetStatus')
DROP INDEX [ChildVacation_TimesheetStatus] ON [dbo].[ChildVacation]
GO
/****** Object:  Index [IX_ChildVacation_User_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'IX_ChildVacation_User_Id')
DROP INDEX [IX_ChildVacation_User_Id] ON [dbo].[ChildVacation]
GO
/****** Object:  Index [IX_ChildVacation_CreatorUser_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'IX_ChildVacation_CreatorUser_Id')
DROP INDEX [IX_ChildVacation_CreatorUser_Id] ON [dbo].[ChildVacation]
GO
/****** Object:  Table [dbo].[ChildVacationComment]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]') AND type in (N'U'))
DROP TABLE [dbo].[ChildVacationComment]
GO
/****** Object:  Table [dbo].[ChildVacation]    Script Date: 15.07.2013 9:27:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND type in (N'U'))
DROP TABLE [dbo].[ChildVacation]
GO
/****** Object:  Table [dbo].[ChildVacation]    Script Date: 15.07.2013 9:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChildVacation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[PaidToDate] [datetime] NULL,
	[FreeRate] [bit] NOT NULL,
	[IsPreviousPaymentCounted] [bit] NOT NULL,
	[ChildrenCount] [int] NULL,
	[IsFirstChild] [bit] NOT NULL,
	[PaidToDate1] [datetime] NULL,
	-- [DaysCount] [int] NOT NULL,
	[Number] [int] NOT NULL,
	-- [TypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[UserDateAccept] [datetime] NULL,
	[ManagerDateAccept] [datetime] NULL,
	[PersonnelManagerDateAccept] [datetime] NULL,
	[SendTo1C] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[TimesheetStatusId] [int] NULL,
	[DeleteAfterSendTo1C] [bit] NOT NULL,
	[ExportFrom1C] [bit] NULL,
 CONSTRAINT [PK_ChildVacation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ChildVacationComment]    Script Date: 15.07.2013 9:27:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChildVacationComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ChildVacationId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Comment] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_ChildVacationComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_ChildVacation_CreatorUser_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'IX_ChildVacation_CreatorUser_Id')
CREATE NONCLUSTERED INDEX [IX_ChildVacation_CreatorUser_Id] ON [dbo].[ChildVacation]
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChildVacation_User_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'IX_ChildVacation_User_Id')
CREATE NONCLUSTERED INDEX [IX_ChildVacation_User_Id] ON [dbo].[ChildVacation]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ChildVacation_TimesheetStatus]    Script Date: 15.07.2013 9:27:36 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'ChildVacation_TimesheetStatus')
CREATE NONCLUSTERED INDEX [ChildVacation_TimesheetStatus] ON [dbo].[ChildVacation]
(
	[TimesheetStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ChildVacation_ChildVacationType]    Script Date: 15.07.2013 9:27:36 ******/
--IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacation]') AND name = N'ChildVacation_ChildVacationType')
--CREATE NONCLUSTERED INDEX [ChildVacation_ChildVacationType] ON [dbo].[ChildVacation]
--(
--	[TypeId] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--GO
/****** Object:  Index [IX_ChildVacationComment_User_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]') AND name = N'IX_ChildVacationComment_User_Id')
CREATE NONCLUSTERED INDEX [IX_ChildVacationComment_User_Id] ON [dbo].[ChildVacationComment]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChildVacationComment_ChildVacation_Id]    Script Date: 15.07.2013 9:27:36 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]') AND name = N'IX_ChildVacationComment_ChildVacation_Id')
CREATE NONCLUSTERED INDEX [IX_ChildVacationComment_ChildVacation_Id] ON [dbo].[ChildVacationComment]
(
	[ChildVacationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__ChildVacation__Delete__Default]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChildVacation] ADD  DEFAULT ((0)) FOR [DeleteAfterSendTo1C]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_CreatorUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation]  WITH CHECK ADD  CONSTRAINT [FK_ChildVacation_CreatorUser] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_CreatorUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation] CHECK CONSTRAINT [FK_ChildVacation_CreatorUser]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation]  WITH CHECK ADD  CONSTRAINT [FK_ChildVacation_TimesheetStatus] FOREIGN KEY([TimesheetStatusId])
REFERENCES [dbo].[TimesheetStatus] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_TimesheetStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation] CHECK CONSTRAINT [FK_ChildVacation_TimesheetStatus]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation]  WITH CHECK ADD  CONSTRAINT [FK_ChildVacation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
ALTER TABLE [dbo].[ChildVacation] CHECK CONSTRAINT [FK_ChildVacation_User]
GO
--IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_ChildVacationType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
--ALTER TABLE [dbo].[ChildVacation]  WITH CHECK ADD  CONSTRAINT [FK_ChildVacation_ChildVacationType] FOREIGN KEY([TypeId])
--REFERENCES [dbo].[ChildVacationType] ([Id])
--GO
--IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacation_ChildVacationType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacation]'))
--ALTER TABLE [dbo].[ChildVacation] CHECK CONSTRAINT [FK_ChildVacation_ChildVacationType]
--GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacationComment_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]'))
ALTER TABLE [dbo].[ChildVacationComment]  WITH CHECK ADD  CONSTRAINT [FK_ChildVacationComment_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacationComment_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]'))
ALTER TABLE [dbo].[ChildVacationComment] CHECK CONSTRAINT [FK_ChildVacationComment_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacationComment_ChildVacation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]'))
ALTER TABLE [dbo].[ChildVacationComment]  WITH CHECK ADD  CONSTRAINT [FK_ChildVacationComment_ChildVacation] FOREIGN KEY([ChildVacationId])
REFERENCES [dbo].[ChildVacation] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChildVacationComment_ChildVacation]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChildVacationComment]'))
ALTER TABLE [dbo].[ChildVacationComment] CHECK CONSTRAINT [FK_ChildVacationComment_ChildVacation]
GO
