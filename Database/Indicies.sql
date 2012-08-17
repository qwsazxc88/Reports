set identity_insert  [Role] on
INSERT INTO [Role] (Id,[Name],Version) values (1,'�������������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'���������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'������������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (8,'��������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (16,'������',1) 
INSERT INTO [Role] (Id,[Name],Version) values (32,'����������',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'�� �������� �����������',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'�������� �����������',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'�������� �������������',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'�������� ����������',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'��������� � 1�',1)
set identity_insert  [RequestStatus] off 

declare @OrganizationId int
declare @Organization1Id int
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (1,'�������� �����������',1)	
set @OrganizationId = @@Identity
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (2,'�������� ����������� 1',1)	
set @Organization1Id = @@Identity	
	

declare @DepartmentId int
declare @Department1Id int
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (1,'�������� �����������',1)		
set @DepartmentId = @@Identity	
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (2,'�������� ����������� 1',1)		
set @Department1Id = @@Identity	


declare @PositionId int
declare @Position1Id int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (1,'�������� ���������',1)		
set @PositionId = @@Identity
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (2,'�������� ��������� 1',1)		
set @Position1Id = @@Identity	
	

INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (51,'�������������� ������� ������ ��� ������ #1203',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (31,'������ ��� ����� ����� � ���. ��� ������ ������� #1125',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'������ ��������������� ������� �� ����������� ���� #1207',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (32,'������ �������������� �������� ���� �� ����� �� ������ - ���������� #1504',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'������ ������� �� ����������� ���� #1201',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (42,'������ ������� �� ����������� #1202',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'������ �������� ������� �� ����������� ���� #1204',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (53,'������ ��� ������ �������� �� �� #1205',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (54,'������ �� ���� ���� #1206',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (23,'������ �� ������������ � ����� #1501',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (52,'������ �� ����� �� �������� ��� ������ #1802',1)		

INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (51,'�������������� ������� ������ ��� ������ #1203',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (53,'������ ��� ������ �������� �� �� #1205',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (54,'������ �� ���� ����#1206',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (10024,'���������� �� ������� (�� ������������ � �����) #1804',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (10023,'���������� �� ������� #1803',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (55,'���������� �� ������������ ������� #1806',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (56,'������, ������� �� ���� ��������� #1807',1)	

INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (25,'�� �� ������ � ���� (�� ������������) #1805',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (26,'������� �� ���������� ������ #1402',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (22,'������ �� �� ������ �� ������������ #1403',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (25,'������ ���������� ������ �� ���� ������������ #1426',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (21,'������ ���������� ������ #1469',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (10024,'���������� �� ������� (�� ������������ � �����) #1804',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (10023,'���������� �� ������� #1803',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (55,'���������� �� ������������ ������� #1806',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (71,'������� �� ����� �� ������� �� 1.5 ��� #1502',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (72,'������� �� ����� �� ������� �� 3 ��� #1503',1)

INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (13,'������� �� ������ � ��������� � �������� #1107',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (12,'������ ����������� � �������� ���� #1106',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (null,'�����',1)

INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('������������ �� ������',1)
INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('������������ �� ���',1)
INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('������������  �� �����',1)

INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('�� ����������������',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('�� �������� ����� ������',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('�� �������� ��������� ��������',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('���������',1)

INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('����������   0,2 ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,001',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,1',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,16',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,2 ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,55 ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,68 ������ (27,2 ����)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,7 ������ (28 �����)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,8 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0.8',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('1 ���',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('10 ����� (0,25)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('10-�������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('12 � (0,3������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('14� (0,35 ������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('16 � (0,4������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('16,8 � (0,42 ������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('20 ����� (0,5 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('24 �(0,6��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('25 (0,625)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('29,2 ���� (0.73 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('30 (0,75��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('32 ���� ( 0.8��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('33.6 ���� ( 0.84��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('34,8 ���� ( 0,87 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('35 ����� (0,87 ��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('35,6 ���� (0,89��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('36,4 ���� ( 0,91��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('36.8 ����  (0,92��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('37,5 ����� (0,94 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('38,4 ���� (0,96��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('39.6 ���� ( 0.99��.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('4 ���� (0,1 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('6,4 ���� (0,16 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('6,8 ���� (0,17 ��)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('������ 15�/� (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('������ 2 ����� 2 2�����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('������ 2 ����� 2 �� 11 ����� 1�����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('������ 25�/� (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('��������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('�������� ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('�������� ������ (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('�������� ������ 20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('�������� ������ 20����� ������ (�������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('�������� ������ 40����� ������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('����������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� (30 �����)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 0.4��',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 10����� (�������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 20 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 30 �����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 32 ����',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 35',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 35 ����� (�������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���������� 37,5',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('�������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('����������� ���� �� 7 ����� (���������)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('���. ���� ���. �������',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('������������� ����',1)

INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� �� ������� ��� ������� � �������� #1114','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� �� ������������ #1115','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� �� ���������� �������� ������ #1116','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� ������������ #1117','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','�������� ��������������� #1123','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','�������� ����������� #1301','��������� ��������� ������� ������',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values (null,'���������� ���� ������������ #1120','�� �������� �������� ������ �� ����� ���������',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','�������� �������� 1 #1302','��������� ��������� ������� ������',1)

--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('��������� ���������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('��������� �������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('������������ ������������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('����������� ����������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('������ ������������',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('������� � ������',1)

INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 1 ��. 81 ��','����������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 1 ����� 1 ��. 77 ��','���������� ������.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 2 ��. 77 ��','��������� ����� ��������� ��������.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 2 ����� 1 ��. 81 ��','���������� ����������� �����',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 3 ��. 77 ��','����������� ��������� �������� �� ���������� ���������.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 4 ��. 77 ��','����������� ��������� �������� �� ���������� ������������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 5 ��. 77 ��','������� ��������� �� ������ ������ � ������� ������������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 5 ����� ������ ��. 77','������� ��������� �� �������� ���������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 6 ��. 81 ��','����������� ��������� �������� �� ���������� ������������ �� ������.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 6 ��.83 ��','������  ���������  ���� ������������ - ���. ����, � ����� ��������� ����� ��������� ���� ������������ - ���. ���� �������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 7 �. 1 ��.81 �� ��','�������� ������� ���������� � ����� � ����������� �������� �������� ����������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�. 7 ����� 1 ��. 77','����� ��������� �� ����������� ������ � ����� � ���������� ������������ ��������� ������� ��',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�.3 ����� 1 ��. 77 �� ��','����������� �������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�.5 ����� ������ ��. 83 �� ��','�������� ������� ��������� � ����� � ���������� ��������� ��������� ����������� � �������� ������������ � ������������ � ����������� �����������',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('�.6 �.1 ��. 83','�������� ������� ��������� � ����� �� ������� ���������',1)

INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (2,'����� �� ����� #1102','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (3,'������ �� �������� ������ #1103','�� ������� �������� ������',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (66,'������ ���������� ������� �� ������ �� ����� #1707','�� �������� �������� ������ �� �����',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (67,'������ ���������� ������� �� �������� ������# 1708','�� ������� �������� ������',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (68,'��������� ������� �� ���� ������������ #1702','�� �������� ���������',1)



--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('���� 1',1)
--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('���� 2',1)


INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('� ������� ����',1)
INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('�����, � ������������ � ������� � ������� ���',1)

--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'�� �� ������ � ���� (�� ������������) #1805','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (26,'������� �� ���������� ������ #1402','������� �� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (22,'������ �� �� ������ �� ������������ #1403','�� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'������ ���������� ������ �� ���� ������������ #1426','�� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (21,'������ ���������� ������ #1469','�� �������� ��������� ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10024,'���������� �� ������� (�� ������������ � �����) #1804','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10023,'���������� �� ������� #1803','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (55,'���������� �� ������������ ������� #1806','������� �����',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (71,'������� �� ����� �� ������� �� 1.5 ��� #1502','������� �� ����� �� �������� �� 1.5 ���',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (72,'������� �� ����� �� ������� �� 3 ��� #1503','������� �� ����� �� �������� �� 3 ���',1)

INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (60,3,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (80,2,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (100,1,1)



set identity_insert  [TimesheetStatus] on
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (1,1,'�','����')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (2,1,'�','��������� ������������������ � ����������� ������� �������� ����������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (3,1,'�','��������� ������������������ ��� ������. ������� � �������, �������. �����������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (4,1,'��','�������� ����')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (5,1,'�','������ ����')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (6,1,'�','�������� � ��������� ���')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (7,1,'�','������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (8,1,'��','������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (9,1,'��','������ ��� ���������� ���������� ����� � �������, ��������������� �����������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (10,1,'��','������ ��� ���������� ���������� �����, ��������������� ����. �� ������. ������������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (11,1,'�','������ �� ������������ � ����� (������ � ����� � ������������ ��������. �������)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (12,1,'��','������ �� ����� �� �������� �� ���������� �� �������� ���� ���')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (13,1,'��','����������������� ������ � �������� � ���������, ����������� ���')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (14,1,'�','����������������� ������������ ������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (15,1,'��','������� (���������� �� ������� ����� ��� ����. ������ � ���. �������, ���. �����������������)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (16,1,'��','������ �� ������������ ��������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (17,1,'��','������� �� ���� ����������')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (18,1,'��','����� ������� �� ���� ������������')
set identity_insert  [TimesheetStatus] off


declare @typeId int
declare @firstTypeId int
declare @firstSubTypeId int
INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������',1) 
set @typeId = @@Identity
set @firstTypeId = @typeId
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('���������',1,@typeId) 
set @firstSubTypeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ����� �� ��������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��� ���������� ��',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� ���',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ��',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� �����',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('����������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ����� �� ��������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������ �� ������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� � ���������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������ � �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ����� �� ��������',1,@typeId) 


INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('���������� �� ������� �����',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������ �������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('����������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ������������ �������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���������� �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���������� ������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�� ���������� ������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('� ����� � ���������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('�����������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� ������ �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����� ������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('���������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �� ���� ������������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('���. ������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����������� ����� (������)',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������, �����������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �� ����� �� ���. 3� ���',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �� ����� �� ���. 1,5� ���',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('����������� ��������� �� ����� ���������������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('������� �� ���� ���',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� � ����� �� �������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� ��� ���������� �� ���� � ������ ����� ������������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� ��� �������� �������',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� ��� ��� ����������� �������',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('���������',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������� �����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('�������������� ����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('������������ ����',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('��������� �����',1,@typeId) 


-- User
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]		, [IsNew]	 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'�������������',             1,		  null ,          1,           '��0000000001' , 0)
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew]) 
VALUES			   (1,       	0              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'������������',              1,         null								, 4,		   '��0000000001' , 0)
set @managerId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew]) 
VALUES			   (1,       	0              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'��������',                    1,         null								, 8,		   '��0000000001' , 0)
set @personnelId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'������',           1,         null								        , 16,		   '��0000000001' , 0)
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'����������',                        1,         null,              32,		       '��0000000001' , 0)
set @outsorsingId = @@Identity
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         PersonnelManagerId, OrganizationId,/*DepartmentId,*/PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'������������',                   1,         null            , 2,		   '��0000000001' ,  @managerId,       @personnelId,       @OrganizationId,/*@DepartmentId,*/@PositionId , 0)
set @userId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId,            OrganizationId,/*DepartmentId,*/PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'������ ���� ��������',            1,         null            , 2,		   '��0000000001' ,  @managerId,       @personnelId,@Organization1Id,/*@Department1Id,*/@Position1Id , 0)
set @user1Id = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId , [IsNew]) 
VALUES			   (1,       	0              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'������ ���� ��������',         1,         null            , 2,		   '��0000000001' ,  @managerId,       @personnelId , 0)

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     '��������',  @firstTypeId, @firstSubTypeId, @userId, 0)
