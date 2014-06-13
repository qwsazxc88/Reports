update [dbo].[MissionReportCostType] set Name = N'Суточные (руб.)' where Id = 1
update [dbo].[MissionReportCostType] set Name = N'Проживание (руб.)' where Id = 2
update [dbo].[MissionReportCostType] set Name = N'Проезд билеты' where Id = 5
update [dbo].[MissionReportCostType] set Name = N'Прочие билеты' where Id = 8

-- «Такси», «Аэроэкспресс», «Паром», «Пароход», «Вертолет», «Посадочный талон», «Путевой лист», «ПТС», 
-- «Оплата стоянки», «Гостиница», «Аренда квартиры», «Упаковка багажа», «Провоз багажа», «Суточные»,  «Прочие расходы».
set identity_insert [dbo].[MissionReportCostType] ON
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (9,N'',N'Такси',9)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (10,N'',N'Аэроэкспресс',10)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (11,N'',N'Паром',11)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (12,N'',N'Пароход',12)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (13,N'',N'Вертолет',13)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (14,N'',N'Посадочный талон',14)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (15,N'',N'Путевой лист',15)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (16,N'',N'ПТС',16)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (17,N'',N'Оплата стоянки',17)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (18,N'',N'Гостиница',18)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (19,N'',N'Аренда квартиры',19)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (20,N'',N'Упаковка багажа',20)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (21,N'',N'Провоз багажа',21)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (22,N'',N'Суточные',22)
insert into [dbo].[MissionReportCostType] (Id,Code, Name, SortOrder) values (23,N'',N'Прочие расходы',23)
set identity_insert [dbo].[MissionReportCostType] OFF