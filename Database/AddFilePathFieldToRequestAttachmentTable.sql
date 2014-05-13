alter table [dbo].[RequestAttachment] add [FilePath] nvarchar(1024) null
alter table [dbo].[RequestAttachment] alter column [Context] varbinary(max) null