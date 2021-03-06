IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND name = N'IX_Department_Path')
	DROP INDEX [IX_Department_Path] ON [dbo].[Department] WITH ( ONLINE = OFF )
GO
/****** Object:  Index [IX_Department_Path]    Script Date: 04/07/2013 10:13:13 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Department_Path] ON [dbo].[Department] 
(
	[Path] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]