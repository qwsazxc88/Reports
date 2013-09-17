set identity_insert dbo.Role  ON
insert into dbo.Role (Id, Version, Name)
values (256,1,N'Бухгалтер')
set identity_insert dbo.Role OFF