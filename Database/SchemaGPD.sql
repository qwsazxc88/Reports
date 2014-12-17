use WebAppTest
go
--скрипт создает структуру для системы ГПД с занесением первичных данных в справочники
/*
select * from GpdDetailType
insert into dbo.GpdDetailType ([Name]) values ('Получатель')
insert into dbo.GpdDetailType ([Name]) values ('Плательщик')

select * from GpdRefStatus
insert into GpdRefStatus (name) values('Выгружено в 1С')
insert into GpdRefStatus (name) values('Записано')
insert into GpdRefStatus (name) values('Отклонено')
insert into GpdRefStatus (name) values('Черновик')

select * from GpdChargingType
insert into GpdChargingType (name, code) values ('Оплата по договорам подряда#3601', '3601')
insert into GpdChargingType (name, code) values ('Оплата по договорам подряда с ФСС#3602', '3602')
insert into GpdChargingType (name, code) values ('Оплата по договорам подряда (не уменьшающая НОБ)#3610', '3610')
insert into GpdChargingType (name, code) values ('Оплата по договорам аренды (ГПД) для возмещения найма жилья сотрудникам#3612', '3612')
insert into GpdChargingType (name, code) values ('Оплата по договорам аренды транспортного средства#3605', '3605')
insert into GpdChargingType (name, code) values ('Оплата по договорам аренды имущества#3607', '3607')
insert into GpdChargingType (name, code) values ('Оплата по договорам аренды имущества (не уменьшающая НОБ)#3611', '3611')
*/

--select * from GpdRefDetail
--select * from Role
--select * from Users where RoleId = 2
--select * from gpdcontract
--SELECT Id as Id, LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS as Surname FROM RefPeople ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS
/*
--не нужен этот триггер
IF OBJECT_ID ('updGpdContract', 'TR') IS NOT NULL
   DROP TRIGGER updGpdContract;
GO


CREATE TRIGGER updGpdContract
ON dbo.GpdContract
AFTER INSERT, UPDATE 
AS
  
	IF NOT EXISTS (SELECT * FROM GpdMagProlongation as A
								 INNER JOIN inserted as B ON B.Id = A.GCID and B.DateP = A.DateP) and 
								 (SELECT DateP FROM inserted) is not null
	BEGIN
		INSERT INTO GpdMagProlongation (GCID, DateP, CreatorID)
		SELECT Id, DateP, CreatorID
		FROM inserted 
	END
	
GO
*/

IF OBJECT_ID ('updGpdContractSetStatus', 'TR') IS NOT NULL
   DROP TRIGGER updGpdContractSetStatus;
GO

--при выгрузке дат из 1С проставляем статус
CREATE TRIGGER updGpdContractSetStatus
ON dbo.GpdContract
AFTER INSERT, UPDATE 
AS
	IF EXISTS(SELECT * FROM GpdContract as A
						INNER JOIN inserted as B ON B.id = A.id
						WHERE A.SendTo1C is not null and A.StatusID <> 1)
	BEGIN
		UPDATE GpdContract SET StatusID = 1 
		FROM GpdContract as A
		INNER JOIN inserted as B ON B.id = A.id
	END
	
GO


IF OBJECT_ID ('updGpdActSetStatus', 'TR') IS NOT NULL
   DROP TRIGGER updGpdActSetStatus;
GO

--при выгрузке дат из 1С проставляем статус
CREATE TRIGGER updGpdActSetStatus
ON dbo.GpdAct
AFTER INSERT, UPDATE 
AS
	IF EXISTS(SELECT * FROM GpdAct as A
						INNER JOIN inserted as B ON B.id = A.id
						WHERE A.SendTo1C is not null and A.StatusID <> 1)
	BEGIN
		UPDATE GpdAct SET StatusID = 1 
		FROM GpdAct as A
		INNER JOIN inserted as B ON B.id = A.id
	END
	
GO


IF OBJECT_ID ('vwGpdContractList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdContractList]
GO

CREATE VIEW [dbo].[vwGpdContractList]
AS
SELECT     A.Id as [Id], A.CreatorID, A.DepartmentId, G.Name AS DepartmentName, 
					 A.PersonID, B.LastName + ' ' + B.FirstName + ' ' + B.SecondName AS Surname, A.CTID, 
           C.Name AS CTName, A.StatusID, E.Name AS StatusName, A.NumContract, A.NameContract, A.DateBegin, A.DateEnd, 
					 A.PayeeID, A.PayerID, A.GPDID, 
           A.PurposePayment, A.DateP as DateP, A.DateP AS DatePOld, A.IsLong, F.Name AS CreatorName, A.CreateDate, F.Name AS Autor, dep3.Name AS DepLevel3Name, 
           G.Name AS DepLevel7Name
FROM         dbo.GpdContract AS A INNER JOIN
                      dbo.RefPeople AS B ON B.Id = A.PersonID INNER JOIN
                      dbo.GpdChargingType AS C ON C.Id = A.CTID INNER JOIN
                      dbo.GpdRefStatus AS E ON E.Id = A.StatusID LEFT OUTER JOIN
                      dbo.Users AS F ON F.Id = A.CreatorID 
											INNER JOIN dbo.Department AS G ON G.Id = A.DepartmentId 
											--LEFT OUTER JOIN dbo.Department AS H ON H.Code = G.ParentId 
											--LEFT OUTER JOIN dbo.Department AS I ON I.Code = H.ParentId 
											--LEFT OUTER JOIN dbo.Department AS J ON J.Code = I.ParentId 
											--LEFT OUTER JOIN dbo.Department AS K ON K.Code = J.ParentId
											LEFT JOIN dbo.Department as dep3 ON G.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 

GO


IF OBJECT_ID ('vwGpdActList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdActList]
GO

CREATE VIEW [dbo].[vwGpdActList]
AS
--список актов
SELECT A.Id, A.CreatorID, A.ActDate, A.ActNumber, isnull(C.Gcount, 0) as GCCount,
			 B.PersonID, D.LastName + ' ' + D.FirstName + ' ' + D.SecondName AS Surname,
			 B.NameContract, B.NumContract, B.DateBegin as ContractBeginDate, B.DateEnd as ContractEndDate,
			 E.Name AS CreatorName, A.CreateDate, dep3.Name AS DepLevel3Name, A.ChargingDate, A.DateBegin, A.DateEnd,
			 A.Amount, A.AmountPayment, A.POrderDate, A.PurposePayment, A.ESSSNum, A.StatusID, G.Name AS StatusName, B.Id as GCID,
			 H.[Name] as CTName, B.DateP, F.[Name] as DepLevel7Name, B.GPDID, B.DepartmentId
FROM dbo.GpdAct as A
INNER JOIN dbo.GpdContract as B ON B.Id = A.GCID
LEFT JOIN (SELECT GCID, count(GCID) as Gcount FROM dbo.GpdAct GROUP BY GCID) as C ON C.GCID = A.GCID
INNER JOIN dbo.RefPeople AS D ON D.Id = B.PersonID
INNER JOIN dbo.Users AS E ON E.Id = A.CreatorID 
INNER JOIN dbo.Department AS F ON F.Id = B.DepartmentId 
LEFT JOIN dbo.Department as dep3 ON F.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
INNER JOIN dbo.GpdRefStatus AS G ON G.Id = A.StatusID
INNER JOIN dbo.GpdChargingType as H ON H.Id = B.CTID


GO


IF OBJECT_ID ('vwGpdActNew', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdActNew]
GO

CREATE VIEW [dbo].[vwGpdActNew]
AS
--для создания нового акта ГПД
SELECT A.Id, A.CreatorID, A.ActDate, A.ActNumber, isnull(C.Gcount, 0) + 1 as GCCount,
			 B.PersonID, D.LastName + ' ' + D.FirstName + ' ' + D.SecondName AS Surname,
			 B.NameContract, B.NumContract, B.DateBegin as ContractBeginDate, B.DateEnd as ContractEndDate,
			 E.Name AS CreatorName, A.CreateDate, dep3.Name AS DepLevel3Name, A.ChargingDate, A.DateBegin, A.DateEnd,
			 A.Amount, A.AmountPayment, A.POrderDate, B.PurposePayment, A.ESSSNum, G.Id as StatusID, G.Name AS StatusName, B.Id as GCID,
			 H.[Name] as CTName, B.DateP, F.[Name] as DepLevel7Name, B.GPDID, B.DepartmentId
FROM dbo.GpdContract as B
LEFT JOIN dbo.GpdAct as A ON B.Id = A.GCID and B.Id = -1
LEFT JOIN (SELECT GCID, count(GCID) as Gcount FROM dbo.GpdAct GROUP BY GCID) as C ON C.GCID = B.ID
INNER JOIN dbo.RefPeople AS D ON D.Id = B.PersonID
LEFT JOIN dbo.Users AS E ON E.Id = A.CreatorID 
INNER JOIN dbo.Department AS F ON F.Id = B.DepartmentId 
LEFT JOIN dbo.Department as dep3 ON F.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
LEFT JOIN dbo.GpdRefStatus AS G ON G.Id = isnull(A.StatusID, 4)
INNER JOIN dbo.GpdChargingType as H ON H.Id = B.CTID



GO



IF OBJECT_ID ('vwGpdStatus', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdStatus]
GO

CREATE VIEW [dbo].[vwGpdStatus]
AS
SELECT 0 as Id, 'Все' as Name
UNION ALL
SELECT Id as Id, Name as Name FROM dbo.GpdRefStatus WHERE Id <> 3
GO


IF OBJECT_ID ('vwGpdChargingType', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdChargingType]
GO

CREATE VIEW [dbo].[vwGpdChargingType]
AS
SELECT 0 as Id, 'Все' as Name
UNION ALL
SELECT Id as Id, Name as Name FROM dbo.GpdChargingType
GO



IF OBJECT_ID ('vwGpdRefPersons', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdRefPersons]
GO

CREATE VIEW [dbo].[vwGpdRefPersons]
AS
SELECT Id as Id, LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS as Name, SNILS
FROM RefPeople 
--ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS
GO


IF OBJECT_ID ('vwGpdRefDetail', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdRefDetail]
GO

CREATE VIEW [dbo].[vwGpdRefDetail]
AS
SELECT DTID as DTID, Id as Id, Name as Name FROM GpdRefDetail
GO

