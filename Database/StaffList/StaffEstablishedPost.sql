use WebAppTest
go

--������ ��������� ������� ������� � ������ �� �� ��������

/*
�������� ����� �������� �������� �� ���������� ��� ������������ ������� ������
�� ���� �� ���� �� ����� ���� ��������� �������, �� ���� ����������, � ��������� �������, ������ � �.�.
*/

--��������� ���������� ������� ������ �� ������ �������������� ������
INSERT INTO StaffEstablishedPost([Version]
																	,PositionId
																	,DepartmentId
																	,Quantity
																	,Salary
																	,IsUsed
																	,BeginAccountDate
																	,[Priority]
																	,CreatorID)
SELECT 1
			 ,B.PositionId
			 ,A.Id
			 ,count(B.PositionId) as Quantity
			 ,isnull(Salary, 0)
			 ,1 as IsUsed
			 ,getdate() as BeginAccountDate
			 ,null	--���� �� ��������
			 ,null --���� ��������� ������
--FROM StaffEstablishedPostRequest
FROM Department as A
INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and (RoleId & 2) > 0
INNER JOIN Position as C ON C.id = B.PositionId
GROUP BY A.Id, B.PositionId, C.Name, B.Salary


--��������� ������ �� ������ ������� ������
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestTypeId
																				,SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,IsUsed
																				,IsDraft
																				,DateSendToApprove
																				,BeginAccountDate
																				,ReasonId
																				,CreatorID)

SELECT 1
			 ,1 
			 ,Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,0
			 ,getdate()
			 ,getdate()
			 ,null	--������� ���� �� ��������
			 ,null --���� ��������� ������
FROM StaffEstablishedPost
--FROM Department as A
--INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1 and (RoleId & 2) > 0
--INNER JOIN Position as C ON C.id = B.PositionId
--GROUP BY A.Id, B.PositionId, C.Name, B.Salary

--������� ��������� ����� ���������� �������
INSERT INTO StaffEstablishedPostArchive(SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,IsUsed
																				,BeginAccountDate
																				,[Priority]
																				,CreatorID)
SELECT Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--���� �� ��������
			 ,CreatorID
FROM StaffEstablishedPost


--����������� Id ������� ������� ��� ������������� �� ������� ������
UPDATE Users SET SEPId = B.Id
FROM Users as A
INNER JOIN StaffEstablishedPost as B ON B.PositionId = A.PositionId and B.DepartmentId = A.DepartmentId and B.Salary = A.Salary
--where a.DepartmentId = 11356

--�������� ������ �����������, ���� ������� ���������� �������� ����
UPDATE StaffEstablishedPost SET Salary = 0
UPDATE StaffEstablishedPostRequest SET Salary = 0
UPDATE StaffEstablishedPostArchive SET Salary = 0