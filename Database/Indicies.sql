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

INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('�� ����',1)
INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('�� ������ ���',1)

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
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]			 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'�������������',             1,		  null ,          1,           '��0000000001' )
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'������������',              1,         null								, 4,		   '��0000000001')
set @managerId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'��������',                    1,         null								, 8,		   '��0000000001')
set @personnelId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'������',           1,         null								        , 16,		   '��0000000001')
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'����������',                        1,         null,              32,		       '��0000000001')
set @outsorsingId = @@Identity
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         PersonnelManagerId, OrganizationId,/*DepartmentId,*/PositionId) 
VALUES			   (1,       	0              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'������������',                   1,         null            , 2,		   '��0000000001' ,  @managerId,       @personnelId,       @OrganizationId,/*@DepartmentId,*/@PositionId)
set @userId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId,            OrganizationId,/*DepartmentId,*/PositionId) 
VALUES			   (1,       	0              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'������ ���� ��������',            1,         null            , 2,		   '��0000000001' ,  @managerId,       @personnelId,@Organization1Id,/*@Department1Id,*/@Position1Id)
set @user1Id = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId) 
VALUES			   (1,       	0              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'������ ���� ��������',         1,         null            , 2,		   '��0000000001' ,  @managerId,       @personnelId)

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     '��������',  @firstTypeId, @firstSubTypeId, @userId, 0)
