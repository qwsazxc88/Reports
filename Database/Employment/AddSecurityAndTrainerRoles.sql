set identity_insert dbo.Role  ON
insert into dbo.Role (Id, Version, Name) values (32768,1,N'Сотрудник службы безопасности')
insert into dbo.Role (Id, Version, Name) values (65536,1,N'Тренер')
set identity_insert dbo.Role OFF