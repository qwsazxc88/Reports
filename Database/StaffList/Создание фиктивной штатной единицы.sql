--����� ������� ������������� � ���������
declare @SEPId int, @UserId int
SELECT @UserId = Id FROM Users WHERE Code = '0000025105'
--select * from Department where Code1COld = 9900556
--select * from Position where Code = '000000305'
--��
INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
VALUES(1, 472, 9246, 1, 17000, 1, '20151224')

set @SEPId = @@IDENTITY

--�����������
INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
VALUES(1, @SEPId, @UserId, 1)
--����� ��.
--������
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestTypeId
																				,DateRequest
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
			 ,'20151031'
			 ,Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,0
			 ,'20151224'
			 ,'20151224'
			 ,'20151224'
			 ,null	--������� ���� �� ��������
			 ,null --���� ��������� ������
FROM StaffEstablishedPost WHERE Id = @SEPId

--�����
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
FROM StaffEstablishedPost WHERE Id = @SEPId


select @SEPId