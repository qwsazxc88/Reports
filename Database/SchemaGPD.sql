use WebAppTest
go
--������ ������� ��������� ��� ������� ��� � ���������� ��������� ������ � �����������
/*
select * from GpdDetailType
insert into dbo.GpdDetailType ([Name]) values ('����������')
insert into dbo.GpdDetailType ([Name]) values ('����������')

select * from GpdRefStatus
insert into GpdRefStatus (name) values('��������� � 1�')
insert into GpdRefStatus (name) values('��������')
insert into GpdRefStatus (name) values('���������')
insert into GpdRefStatus (name) values('��������')

select * from GpdChargingType
insert into GpdChargingType (name, code) values ('������ �� ��������� �������#3601', '3601')
insert into GpdChargingType (name, code) values ('������ �� ��������� ������� � ���#3602', '3602')
insert into GpdChargingType (name, code) values ('������ �� ��������� ������� (�� ����������� ���)#3610', '3610')
insert into GpdChargingType (name, code) values ('������ �� ��������� ������ (���) ��� ���������� ����� ����� �����������#3612', '3612')
insert into GpdChargingType (name, code) values ('������ �� ��������� ������ ������������� ��������#3605', '3605')
insert into GpdChargingType (name, code) values ('������ �� ��������� ������ ���������#3607', '3607')
insert into GpdChargingType (name, code) values ('������ �� ��������� ������ ��������� (�� ����������� ���)#3611', '3611')
*/

--select * from GpdRefDetail
--select * from Role
--select * from Users where RoleId = 2
--select * from gpdcontract
--SELECT Id as Id, LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS as Surname FROM RefPeople ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS



IF OBJECT_ID ('vwGpdContractList', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdContractList]
GO

CREATE VIEW [dbo].[vwGpdContractList]
AS
SELECT     A.Id as [Id], A.CreatorID, A.DepartmentId, G.Name AS DepartmentName, 
					 A.PersonID, B.LastName + ' ' + B.FirstName + ' ' + B.SecondName AS Surname, A.CTID, 
           C.Name AS CTName, A.StatusID, E.Name AS StatusName, A.NumContract, A.NameContract, A.DateBegin, A.DateEnd, 
					 A.PayeeID, A.PayerID, A.GPDID, 
           A.PurposePayment, A.DateP as DateP, A.DateP AS DatePOld, A.IsDraft, F.Name AS CreatorName, A.CreateDate, F.Name AS Autor, dep3.Name AS DepLevel3Name, 
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


IF OBJECT_ID ('vwGpdStatus', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdStatus]
GO

CREATE VIEW [dbo].[vwGpdStatus]
AS
SELECT 0 as Id, '���' as Name
UNION ALL
SELECT Id as Id, Name as Name FROM dbo.GpdRefStatus
GO


IF OBJECT_ID ('vwGpdChargingType', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwGpdChargingType]
GO

CREATE VIEW [dbo].[vwGpdChargingType]
AS
SELECT 0 as Id, '���' as Name
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

