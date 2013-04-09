begin tran
insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'3',N'Структура',3,null,N'3.',1)
insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'4',N'Уровень 2',4, 3,N'3.4.',2)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'5',N'Уровень 2 1',5, 3,N'3.5.',2)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'6',N'Уровень 3',6, 4,N'3.4.6.',3)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'7',N'Уровень 3 1',7, 4,N'3.4.7.',3)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'8',N'Уровень 3 2',8, 5,N'3.4.8.',3)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'9',N'Уровень 3 3',9, 5,N'3.4.9.',3)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'10',N'Уровень 4',10, 6,N'3.4.6.10.',4)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'11',N'Уровень 4 1',11, 6,N'3.4.6.11.',4)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'12',N'Уровень 4 2',12, 7,N'3.4.7.12.',4)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'13',N'Уровень 4 3',13, 7,N'3.4.7.13.',4)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'14',N'Уровень 5',14, 10,N'3.4.6.10.14',5)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'15',N'Уровень 5',15, 10,N'3.4.6.10.15',5)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'16',N'Уровень 6',16, 14,N'3.4.6.10.14.16',6)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'17',N'Уровень 6',17, 14,N'3.4.6.10.14.17',6)

update dbo.Department set 
ParentId = 16,Path = '3.4.6.10.14.16.1',ItemLevel = 7
where id = 1

update dbo.Department set 
ParentId = 16,Path = '3.4.6.10.14.16.2',ItemLevel = 7
where id = 2

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'18',N'Уровень 7',18, 17,N'3.4.6.10.14.17.18',7)

insert into dbo.Department
	   (Version, Code, Name, Code1C, ParentId, Path, ItemLevel)
values (1,N'19',N'Уровень 7 1',19, 17,N'3.4.6.10.14.17.19',7)


--select * from dbo.Department
commit
--rollback
