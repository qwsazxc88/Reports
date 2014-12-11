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
--SELECT Id as Id, LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS as Surname FROM GpdRefPersons ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS

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
	
  INSERT INTO GpdMagProlongation (GCID, DateP, CreatorID)
  SELECT Id, DateP, CreatorID
  FROM inserted 
	
GO


IF OBJECT_ID ('vwGpdContractList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdContractList]
GO

CREATE VIEW [dbo].[vwGpdContractList]
AS
SELECT     A.Id, A.Version, A.CreatorID, A.DepartmentId, G.Name AS DepartmentName, A.PersonID, B.LastName + ' ' + B.FirstName + ' ' + B.SecondName AS Surname, A.CTID, 
                      C.Name AS CTName, A.StatusID, E.Name AS StatusName, A.NumContract, A.NameContract, A.DateBegin, A.DateEnd, A.PayeeID, A.PayerID, A.GPDID, 
                      A.PurposePayment, A.DateP, A.DateP AS DatePOld, A.IsDraft, F.Name AS CreatorName, A.CreateDate, F.Name AS Autor, K.Name AS DepLevel3Name, 
                      G.Name AS DepLevel7Name
FROM         dbo.GpdContract AS A INNER JOIN
                      dbo.GpdRefPersons AS B ON B.Id = A.PersonID INNER JOIN
                      dbo.GpdChargingType AS C ON C.Id = A.CTID INNER JOIN
                      dbo.GpdRefStatus AS E ON E.Id = A.StatusID LEFT OUTER JOIN
                      dbo.Users AS F ON F.Id = A.CreatorID INNER JOIN
                      dbo.Department AS G ON G.Id = A.DepartmentId LEFT OUTER JOIN
                      dbo.Department AS H ON H.Code = G.ParentId LEFT OUTER JOIN
                      dbo.Department AS I ON I.Code = H.ParentId LEFT OUTER JOIN
                      dbo.Department AS J ON J.Code = I.ParentId LEFT OUTER JOIN
                      dbo.Department AS K ON K.Code = J.ParentId

GO


IF OBJECT_ID ('vwGpdStatus', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdStatus]
GO

CREATE VIEW [dbo].[vwGpdStatus]
AS
SELECT 0 as Id, 'Все' as Name
UNION ALL
SELECT Id as Id, Name as Name FROM dbo.GpdRefStatus
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
FROM GpdRefPersons 
--ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS
GO


IF OBJECT_ID ('vwGpdRefDetail', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdRefDetail]
GO

CREATE VIEW [dbo].[vwGpdRefDetail]
AS
SELECT DTID as DTID, Id as Id, Name as Name FROM GpdRefDetail
GO

