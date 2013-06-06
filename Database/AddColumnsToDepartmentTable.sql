begin tran 
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Department_Department]') AND parent_object_id = OBJECT_ID(N'[dbo].[Department]'))
	return
ALTER TABLE [dbo].[Department] add [Code1C] int NULL
GO
update [dbo].[Department] set Code1C = cast(Code as int) 
GO
ALTER TABLE [dbo].[Department] add [ParentId] int NULL
ALTER TABLE [dbo].[Department] add [Path] nvarchar(128) COLLATE Cyrillic_General_CI_AS NULL 
ALTER TABLE [dbo].[Department] add [ItemLevel] int NULL
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Department_1CCode] ON [dbo].[Department] 
(
	[Code1C] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Code1C])

ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
--end
commit