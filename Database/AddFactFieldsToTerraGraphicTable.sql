alter table [dbo].[TerraGraphic] alter column [PointId] int null
alter table [dbo].[TerraGraphic] alter column [Hours] DECIMAL(19,2) null
alter table [dbo].[TerraGraphic] add FactHours  DECIMAL(19,2) null
alter table [dbo].[TerraGraphic] add FactPointId int null
alter table [dbo].[TerraPoint] add IsHoliday bit not null default 0

ALTER TABLE [dbo].[TerraGraphic]  WITH CHECK ADD  CONSTRAINT [FK_TerraGraphic_TerraPoint1] FOREIGN KEY([FactPointId])
REFERENCES [dbo].[TerraPoint] ([Id])
GO

ALTER TABLE [dbo].[TerraGraphic] CHECK CONSTRAINT [FK_TerraGraphic_TerraPoint1]
GO

ALTER TABLE [dbo].[TerraGraphic]  WITH CHECK ADD  CONSTRAINT [FK_TerraGraphic_TerraPoint] FOREIGN KEY([PointId])
REFERENCES [dbo].[TerraPoint] ([Id])
GO

ALTER TABLE [dbo].[TerraGraphic] CHECK CONSTRAINT [FK_TerraGraphic_TerraPoint]
GO

