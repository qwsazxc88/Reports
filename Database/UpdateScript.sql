/****** Object:  Table [dbo].[UserLogin]    Script Date: 01/21/2009 12:45:45 ******/
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogin]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[UserLogin](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Version] [int] NOT NULL,
		[UserId] [int] NULL,
		[Date] [datetime] NOT NULL,
		CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[Users] ([Id])
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question]') AND type in (N'U'))
BEGIN
/****** Object:  Table [dbo].[Question]    Script Date: 01/23/2009 13:09:57 ******/
CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[UserId] [int] NULL,
	[Date] [datetime] NOT NULL,
	[Text] [nvarchar](512) COLLATE Cyrillic_General_CI_AS NOT NULL,
	[AnswerUserId] [int] NULL,
	[AnswerDate] [datetime] NULL,
	[AnswerText] [nvarchar](512) COLLATE Cyrillic_General_CI_AS NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_QuestionUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])

ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_QuestionUserAnswer_User] FOREIGN KEY([AnswerUserId])
REFERENCES [dbo].[Users] ([Id])
END
GO

IF NOT EXISTS (SELECT 1 FROM SYSCOLUMNS WHERE NAME = N'CanAnswer' AND ID = OBJECT_ID('[Users]'))
	ALTER TABLE dbo.[Users] ADD CanAnswer BIT not null default(0)
GO

IF NOT EXISTS (SELECT 1 FROM SYSCOLUMNS WHERE NAME = N'Birthday' AND ID = OBJECT_ID('[Users]'))
	ALTER TABLE dbo.[Users] ADD Birthday DATETIME null
GO

UPDATE DBVERSION set Version = '1.0.1.81'
GO