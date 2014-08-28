set identity_insert dbo.Country  ON
	insert into dbo.Country (Id, Version, Name) values (643,1,N'РОССИЯ')
	insert into dbo.Country (Id, Version, Name) values (112,1,N'БЕЛАРУСЬ')
	insert into dbo.Country (Id, Version, Name) values (398,1,N'КАЗАХСТАН')
	insert into dbo.Country (Id, Version, Name) values (804,1,N'УКРАИНА')
set identity_insert dbo.Country OFF