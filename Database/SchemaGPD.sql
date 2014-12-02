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