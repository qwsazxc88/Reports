use WebAppTest
go

--������ ��������� ������� ������� � ������ �� �� ��������
SET NOCOUNT ON
DECLARE @UserId int, @ReplacedId int, @IsPregnant bit, @DepartmentId int, @PositionId int, @Salary numeric(18, 2), @SEPId int, @SPCount int, @UserCount int, @LinkId int
/*																	
--������ ������� ������������ ������� ������
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
*/

--����� ������� ������������ ������� ������ � ������ ���������� � �� ��������� 

--�������� �������� ������� ������ � ����� ���������� � �������� ��������� � ���� �� ��� � ����� � ����������� �� �������� ������������ (� ������ �� ����������)
SELECT * INTO #Users FROM Users WHERE RoleId & 2 > 0 and IsActive = 1

WHILE EXISTS(SELECT * FROM #Users)
BEGIN
	SET @ReplacedId = null

	SELECT top 1 @UserId = A.Id, @IsPregnant = isnull(A.IsPregnant, 0), @DepartmentId = A.DepartmentId, @PositionId = A.PositionId, 
				 @Salary = case when isnull(A.Salary, 0) = 0 then isnull(B.[�������� ������ (�����) � ��#, ���#], 0) else isnull(A.Salary, 0) end,
				 @ReplacedId = C.Id
	FROM #Users as A
	LEFT JOIN StaffEstablishedPostTemp as B ON B.[��������� �����] = A.Code
	LEFT JOIN #Users as C ON C.Code = B.[���# ����� ��������� ����������/���������]
	ORDER BY isnull(A.IsPregnant, 0)
	--���� ��� �������� ������������, �� 
	IF @IsPregnant = 0
	BEGIN
		--���� ��� ������� ������� � ������ ��������������, ���������� � �������
		IF NOT EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
		BEGIN
			--��������� ������ ������� ������� � ����������� 1 (���� ����� � ������ ����� 0, �� ���� ��� ����� �� ��������)
			INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
			VALUES(1, @PositionId, @DepartmentId, 1, @Salary, 1, getdate())

			SET @SEPId = @@IDENTITY

		
		END
		ELSE
		BEGIN
			--���� ��� ���� ����� ������� �������, �� ����������� �� ���������� �� 1
			SELECT @SEPId = Id FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary

			UPDATE StaffEstablishedPost SET Quantity = Quantity + 1 WHERE Id = @SEPId
		END

		--����������� ���������� Id ������� ������� � ������
		UPDATE Users SET SEPId = @SEPId WHERE Id = @UserId
		--��������� ������ � ��������� ������� ������� � ����������
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		VALUES(1, @SEPId, @UserId, 1)

		SET @LinkId = @@IDENTITY

		--������� � ������ ��������, �������� �� ���� ��������� ���� ������
		--���� ��
		IF @ReplacedId is not null
		BEGIN
				--�� ����������� Id ������� ������� � ������ ����������� ����������
				UPDATE Users SET SEPId = @SEPId WHERE Id = @ReplacedId

				--������� � ������� ��������� ��������������� ������
				INSERT INTO StaffPostReplacement(UserId, ReplacedId, IsUsed, UserLinkId)
				VALUES(@UserId, @ReplacedId, 1, @LinkId)

				--������� �� ��������� ��������� ������ �����������
				DELETE FROM #Users WHERE Id = @ReplacedId
		END
	END
	ELSE
	BEGIN
		--� ����� ������� ������ �������� ������ ���������� ������� ����� �� �������� ��� ��� �� ��� ������ � ����� ��������
		--���� ��� ������� ������� � ������ ��������������, ���������� � �������
		IF NOT EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
		BEGIN
			--��������� ������ ������� ������� � ����������� 1 (���� ����� � ������ ����� 0, �� ���� ��� ����� �� ��������)
			INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
			VALUES(1, @PositionId, @DepartmentId, 1, @Salary, 1, getdate())

			SET @SEPId = @@IDENTITY
		END
		ELSE
		BEGIN
			--���� ��� ���� ����� ������� �������, �� ����������� �� ���������� �� 1
			SELECT @SEPId = Id FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary

			UPDATE StaffEstablishedPost SET Quantity = Quantity + 1 WHERE Id = @SEPId
		END

		--����������� ���������� Id ������� ������� � ������
		UPDATE Users SET SEPId = @SEPId WHERE Id = @UserId

		--��������� ������ � ��������� ������� ������� � ����������
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		VALUES(1, @SEPId, @UserId, 1)

		SET @LinkId = @@IDENTITY
	END

	--������� ������ ����������
	DELETE FROM #Users WHERE Id = @UserId

	SET @UserCount = (SELECT count(*) FROM #Users)
	print '�������� ���������� ' + cast(@UserCount as varchar) + ' ������� �������'
END

print '������������ ������� �������'

--�� ����� �������� �������� ��������, ����
/*
SELECT DepartmentId, PositionId, Salary, sum(SPCount) as SPCount
INTO #Vacation
FROM (SELECT B.Id as DepartmentId, C.Id as PositionId, isnull(A.[�������� ������ (�����) � ��#, ���#], 0) as Salary, 1 as SPCount 
			FROM StaffEstablishedPostTemp as A
			INNER JOIN Department as B ON B.Code1COld = A.[��� ����# 7]
			INNER JOIN Position as c ON C.Code = A.[��� ���������]
			WHERE [��� (�������)] = '��������' and [��� ����# 7] is not null and [��� ���������] is not null) as A
			GROUP BY DepartmentId, PositionId, Salary
*/


SELECT B.Id as DepartmentId, C.Id as PositionId, isnull(A.[�������� ������ (�����) � ��#, ���#], 0) as Salary, 1 as SPCount 
INTO #Vacation
FROM StaffEstablishedPostTemp as A
INNER JOIN Department as B ON B.Code1COld = A.[��� ����# 7]
INNER JOIN Position as c ON C.Code = A.[��� ���������]
WHERE [��� (�������)] = '��������' and [��� ����# 7] is not null and [��� ���������] is not null

WHILE EXISTS(SELECT * FROM #Vacation)
BEGIN
	SELECT top 1 @DepartmentId = DepartmentId, @PositionId = PositionId, @Salary = Salary, @SPCount = SPCount FROM #Vacation

	--���� ��� ������� ������� � ������ ��������������, ���������� � �������
	IF NOT EXISTS (SELECT * FROM StaffEstablishedPost WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary)
	BEGIN
		--��������� ������ ������� ������� � ����������� 1 (���� ����� � ������ ����� 0, �� ���� ��� ����� �� ��������)
		INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
		VALUES(1, @PositionId, @DepartmentId, @SPCount, @Salary, 1, getdate())

		SET @SEPId = @@IDENTITY

		--��������� ������ � ��������� ������� ������� � ����������
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		VALUES(1, @SEPId, null, 1)
	END
	ELSE
	BEGIN
		--���� ��� ���� ����� ������� �������, �� ����������� �� ���������� 
		UPDATE StaffEstablishedPost SET Quantity = Quantity + @SPCount
		WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary

		--��������� ������ � ��������� ������� ������� � ����������
		INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
		SELECT 1, Id, null, 1 FROM StaffEstablishedPost 
		WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary
		--VALUES(1, @SEPId, null, 1)
	END

	

	DELETE FROM #Vacation WHERE DepartmentId = @DepartmentId and PositionId = @PositionId and Salary = @Salary and SPCount = @SPCount
END

print '�������� ���������'



--��������� ������ �� ������ ������� ������
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
--UPDATE Users SET SEPId = B.Id
--FROM Users as A
--INNER JOIN StaffEstablishedPost as B ON B.PositionId = A.PositionId and B.DepartmentId = A.DepartmentId and B.Salary = A.Salary
--where a.DepartmentId = 11356

--�������� ������ �����������, ���� ������� ���������� �������� ����
--UPDATE StaffEstablishedPost SET Salary = 0
--UPDATE StaffEstablishedPostRequest SET Salary = 0
--UPDATE StaffEstablishedPostArchive SET Salary = 0


drop table #Users
drop table #Vacation