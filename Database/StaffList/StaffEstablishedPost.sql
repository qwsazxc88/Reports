--������ ��������� ������� ������� � ������ �� �� ��������

--��������� ������
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestType
																				,SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,StaffECSalary
																				,IsUsed
																				,IsDraft
																				,DateSendToApprove
																				,BeginAccountDate
																				,ReasonId
																				,CreatorID)

SELECT 1
			 ,1 -- ���� ����������� ���, ������ 1 ��� ������ �� �������� ������� �������
			 ,null	--��� ���� ��� ������ �� ������������ ������� �������� (������ �� ���������)
			 ,B.PositionId
			 ,A.Id
			 ,count(B.PositionId)
			 ,isnull(B.Salary, 0)
			 ,0
			 ,1
			 ,0
			 ,getdate()
			 ,getdate()
			 ,null	--������ ���� �� ��������
			 ,null	--��������� ����
FROM Department as A
INNER JOIN Users as B ON B.DepartmentId = A.Id and B.IsActive = 1
INNER JOIN Position as C ON C.id = B.PositionId
GROUP BY A.Id, B.PositionId, C.Name, B.Salary



--��������� ���������� ������� ������ �� ������ �������������� ������
INSERT INTO StaffEstablishedPost([Version]
																	,PositionId
																	,DepartmentId
																	,Quantity
																	,Salary
																	,StaffECSalary
																	,IsUsed
																	,BeginAccountDate
																	,[Priority]
																	,CreatorID)
SELECT 1
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,StaffECSalary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--���� �� ��������
			 ,null	--����
FROM StaffEstablishedPostRequest



--������� ��������� ����� ���������� �������
INSERT INTO StaffEstablishedPostArchive(SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,StaffECSalary
																				,IsUsed
																				,BeginAccountDate
																				,[Priority]
																				,CreatorID)
SELECT Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,StaffECSalary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--���� �� ��������
			 ,null	--����
FROM StaffEstablishedPost