--скрипт создает структуру для групп и пользователей работающих с внутренним биллингом

IF OBJECT_ID ('HelpBillingRoleRecord', 'U') IS NOT NULL
	DROP TABLE [dbo].[HelpBillingRoleRecord]
GO

IF OBJECT_ID ('HelpBillingRole', 'U') IS NOT NULL
	DROP TABLE [dbo].[HelpBillingRole]
GO

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


