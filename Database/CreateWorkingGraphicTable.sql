if exists (select * from dbo.sysobjects where id = object_id(N'WorkingGraphic') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table WorkingGraphic
GO
create table WorkingGraphic (
 Id INT IDENTITY NOT NULL,
  UserId INT not null,
  Day DATETIME not null,
  Hours REAL null,
  constraint PK_WorkingGraphic  primary key (Id)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkingGraphic_Unique] ON [dbo].[WorkingGraphic]
(
	[UserId] ASC,
	[Day] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO