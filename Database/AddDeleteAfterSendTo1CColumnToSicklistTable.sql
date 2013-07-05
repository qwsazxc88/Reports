ALTER TABLE [dbo].[Sicklist] add [DeleteAfterSendTo1C] bit not NULL default(0)
ALTER TABLE [dbo].[Sicklist] add [IsContinued] bit not NULL default(0)


ALTER TABLE [dbo].[Mission] add [DeleteAfterSendTo1C] bit not NULL default(0)

ALTER TABLE [dbo].[Absence] add [DeleteAfterSendTo1C] bit not NULL default(0)

ALTER TABLE  [dbo].[Vacation] add [DeleteAfterSendTo1C] bit not NULL default(0)

ALTER TABLE [dbo].[Dismissal] add [DeleteAfterSendTo1C] bit not NULL default(0)
ALTER TABLE [dbo].[Dismissal] add  [Reduction] decimal(19,2) NULL 