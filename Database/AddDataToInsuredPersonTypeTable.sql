set identity_insert dbo.InsuredPersonType  ON
	insert into dbo.InsuredPersonType (Id, Version, Name) values (1,1,N'Постоянно проживает на территории РФ')
	insert into dbo.InsuredPersonType (Id, Version, Name) values (2,1,N'Высококвалифицированный специалист (член семьи), постоянно проживает на территории РФ')
	insert into dbo.InsuredPersonType (Id, Version, Name) values (4,1,N'Временно проживает на территории РФ')
	insert into dbo.InsuredPersonType (Id, Version, Name) values (8,1,N'Высококвалифицированный специалист (член семьи), временно проживает на территории РФ')
	insert into dbo.InsuredPersonType (Id, Version, Name) values (16,1,N'Временно пребывает на территории РФ')
set identity_insert dbo.InsuredPersonType OFF