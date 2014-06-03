-- НЕ ИСПОЛЬЗОВАТЬ на тестовом сервере или на продакшене.
-- Это - скрипт для добавления точек выходных в базу вручную (без иморта всех точек из файла) 
begin tran 
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values (1,N'410',N'Табель',NULL,N'410',NULL,N'410.',1,NULL,1)
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values (1,N'04-10-0-001',N'Табель',NULL,N'04-10-0-001',N'410',N'410.04-10-0-001.',2,NULL,1)
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values(1,N'04-10-00-000',N'Отпуск',NULL,N'04-10-00-000',N'04-10-0-001',N'410.04-10-0-001.04-10-00-000.',3,NULL,	1)
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values(1,N'04-10-01-000',N'Больничный',NULL,N'04-10-01-000',N'04-10-0-001',N'410.04-10-0-001.04-10-01-000.',3,NULL,1)
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values (1,N'04-10-02-000',N'Неявка',NULL,N'04-10-02-000',N'04-10-0-001',N'410.04-10-0-001.04-10-02-000.',3,NULL,1)
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values(1,N'04-10-03-000',N'Выходной',NULL,N'04-10-03-000',N'04-10-0-001',N'410.04-10-0-001.04-10-03-000.',3,NULL,1)
insert into [dbo].[TerraPoint] (Version, Code, Name, ShortName, Code1C, ParentId, Path, ItemLevel, EndDate, IsHoliday)
values(1,N'04-10-04-000',N'Командировка',NULL,N'04-10-04-000',N'04-10-0-001',N'410.04-10-0-001.04-10-04-000.',3,NULL,1)
commit