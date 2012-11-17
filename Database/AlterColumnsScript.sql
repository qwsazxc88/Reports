
alter table [dbo].[Sicklist] alter column [TypeId] [int] NULL
alter table [dbo].[Dismissal] alter column [TypeId] [int] NULL
alter table [dbo].[Dismissal] alter column [Reason] [nvarchar](256) COLLATE Cyrillic_General_CI_AS NULL