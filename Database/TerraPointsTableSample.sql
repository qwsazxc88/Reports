delete from [dbo].[TerraPoint]
insert into [dbo].[TerraPoint]
(Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate)
values (1,	N'101',N'Дирекция по Центральной России',null,N'101',N'101',null,1,null)
insert into [dbo].[TerraPoint]
(Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate)
values (1,	N'01-02-2-001',N'БГ "Костромская-Центральная"',null,N'01-02-2-001',N'101',N'101.01-02-2-001',2,null)
insert into [dbo].[TerraPoint]
(Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate)
values (1,N'01-02-01-000',N'Дополнительный офис  № 1',null,N'01-02-01-000',N'01-02-2-001',N'101.01-02-2-001.01-02-01-000',3,null)
insert into [dbo].[TerraPoint]
(Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate)
values (1,N'01-02-04-000',N'Дополнительный офис № 12',null,N'01-02-04-000',N'01-02-2-001',N'101.01-02-2-001.01-02-04-000',3,'03.10.13')
