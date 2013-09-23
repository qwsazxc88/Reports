IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ROLE]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
	ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_USER_ROLE]
GO


