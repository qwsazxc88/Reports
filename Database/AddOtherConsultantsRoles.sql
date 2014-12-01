set identity_insert dbo.Role  ON
insert into dbo.Role (Id, Version, Name) values (262144,1,N'Консультант кадры')
insert into dbo.Role (Id, Version, Name) values (524288,1,N'Консультант бухгалтер')
set identity_insert dbo.Role OFF