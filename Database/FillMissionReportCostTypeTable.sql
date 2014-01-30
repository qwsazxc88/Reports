
--Суточные (руб. в день командировки) 
--Проживание (стоимость номера руб/сутки) 
--Авиа билеты (руб) 
--Ж/д билеты (руб) 
--Проезд (билеты, такси, аэроэкспресс и т.д.) 
--Телефонные переговоры 
--Расходы  за пользование интернетом 
--Прочие
set identity_insert [dbo].[MissionReportCostType] ON
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (1,N'',N'Суточные (руб. в день командировки)',1)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (2,N'',N'Проживание (стоимость номера руб/сутки)',2)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (3,N'',N'Авиа билеты (руб)',3)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (4,N'',N'Ж/д билеты (руб)',4)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (5,N'',N'Проезд (билеты, такси, аэроэкспресс и т.д.)',5)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (6,N'',N'Телефонные переговоры',6)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (7,N'',N'Расходы  за пользование интернетом',7)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (8,N'',N'Прочие',8)
set identity_insert [dbo].[MissionReportCostType] OFF