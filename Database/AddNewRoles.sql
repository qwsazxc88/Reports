set identity_insert dbo.Role  ON
insert into dbo.Role (Id, Version, Name) values (1024,1,N'Секретарь')
insert into dbo.Role (Id, Version, Name) values (2048,1,N'Сотрудник фин. департамента')
set identity_insert dbo.Role OFF