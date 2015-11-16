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
/*																	
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
*/
SELECT 1
			 ,A.PositionId
			 ,A.DepartmentId
			 ,count(A.PositionId) as Quantity
			 ,isnull(A.Salary, 0)
			 ,1 as IsUsed
			 ,getdate() as BeginAccountDate
			 ,null	--���� �� ��������
			 ,null --���� ��������� ������
FROM (
			--��������
			SELECT B.Id as DepartmentId, C.Id as PositionId, cast(isnull(A.[�������� ������ (�����) � ��#, ���#], 0) as numeric(18, 2)) as Salary --[��� ����# 7], [��� ���������], [�������� ������ (�����) � ��#, ���#] 
			FROM StaffEstablishedPostTemp as A
			INNER JOIN Department as B ON B.Code1COld = A.[��� ����# 7]
			INNER JOIN Position as C ON C.Code = A.[��� ���������]
			WHERE [��� (�������)] = '��������' and [��� ����# 7] is not null and [��� ���������] is not null
			--358
			UNION ALL
			--�������/���������� ���������
			SELECT A.DepartmentId, A.PositionId, A.Salary--, a.IsPregnant, case when A.Salary = 0 then b.[�������� ������ (�����) � ��#, ���#] else b.[�������� ������ (�����) � ��#, ���#] end as Salary, 
						 --b.[�������� ������ (�����) � ��#, ���#], A.name, b.[��� (������)], 
			--			 ,b.[�������� ������ ��������� ����������/ ��������� ����������], b.[���# ����� ��������� ����������/���������]
			FROM Users as A
			inner join StaffEstablishedPostTemp as b on b.[��������� �����] = a.Code
			WHERE A.IsActive = 1 and (A.RoleId & 2) > 0 and isnull(a.IsPregnant, 0) = 0 
			--and b.[�������� ������ ��������� ����������/ ��������� ����������] is not null b.[���# ����� ��������� ����������/���������]
			UNION ALL
			--����������, �� �� ����������
			SELECT A.DepartmentId, A.PositionId, A.Salary--, a.IsPregnant, case when A.Salary = 0 then b.[�������� ������ (�����) � ��#, ���#] else b.[�������� ������ (�����) � ��#, ���#] end as Salary, 
						 --b.[�������� ������ (�����) � ��#, ���#], A.name, b.[��� (������)], b.[�������� ������ ��������� ����������/ ��������� ����������], b.[���# ����� ��������� ����������/���������]
			FROM Users as A
			inner join StaffEstablishedPostTemp as b on b.[��������� �����] = a.Code
			WHERE A.IsActive = 1 and (A.RoleId & 2) > 0 --and A.IsPregnant = 1
			--and a.DepartmentId = 105 and a.PositionId = 233 
			and isnull(a.IsPregnant, 0) = 1
			and not exists (select * from StaffEstablishedPostTemp where [���# ����� ��������� ����������/���������] = a.code)) as A
GROUP BY A.PositionId, A.DepartmentId, isnull(A.Salary, 0)



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
																				,DateAccept
																				,BeginAccountDate
																				,ReasonId
																				,CreatorID)

SELECT 1
			 ,4	--���� ��������� ������ 
			 ,Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,0
			 ,getdate()
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
--UPDATE StaffEstablishedPost SET Salary = 0
--UPDATE StaffEstablishedPostRequest SET Salary = 0
--UPDATE StaffEstablishedPostArchive SET Salary = 0