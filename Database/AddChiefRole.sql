set identity_insert dbo.Role  ON
insert into dbo.Role (Id, Version, Name)
values (512,1,N'Член Правления')
set identity_insert dbo.Role OFF