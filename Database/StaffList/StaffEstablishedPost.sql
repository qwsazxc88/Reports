use WebAppTest2
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


--��������� ������� ������ ���������� � ��������
SELECT B.Id as UserId, C.Id as StaffExtraChargeId, A.Amount as Salary, D.Id as ActionId,  
			 case when A.ActionName = '������' then 1 
						when A.ActionName = '��������' then 2 
						when A.ActionName = '�� ��������' then 3 
						else 4 end as IsActive, A.GUID, A.ChargeName--A.*
INTO #TMP
FROM UserExtraCharges as A
INNER JOIN Users as B ON B.Code = A.UserCode
INNER JOIN StaffExtraCharges as C ON C.GUID = A.GUID
INNER JOIN StaffExtraChargeActions as D ON D.Name = A.ActionName

--��������� ������ � �������� �����������
INSERT INTO StaffPostChargeLinks(UserId, StaffExtraChargeId, Salary, ActionId, IsActive)
SELECT UserId, StaffExtraChargeId, Salary, ActionId, IsActive FROM #TMP
WHERE GUID not in ('35c7a5dd-d8e9-4aa0-8378-2a7e501d846a', '537ff7ed-5e51-48d1-bf5e-4f680cb3e1b7', '66f08438-f006-44e8-b9ee-32a8dcf557ba')
ORDER BY UserId, ChargeName


--������� ��� ����� �������� ������������
DELETE FROM #TMP WHERE GUID not in ('66f08438-f006-44e8-b9ee-32a8dcf557ba')

--������� �������� � ������� ��������
INSERT INTO StaffEstablishedPostChargeLinks(Version, SEPRequestId, SEPId, StaffExtraChargeId, Amount, ActionId)
SELECT 1 as Version, C.Id as SEPRequestId, B.SEPId, A.StaffExtraChargeId, A.Salary as Amount, A.ActionId
FROM #TMP as A
INNER JOIN StaffEstablishedPostUserLinks as B ON B.UserId = A.UserId
INNER JOIN StaffEstablishedPostRequest as C ON C.SEPId = B.SEPId
GROUP BY C.Id, B.SEPId, A.StaffExtraChargeId, A.Salary, A.ActionId

--����������� Id ������� ������� ��� ������������� �� ������� ������
--UPDATE Users SET SEPId = B.Id
--FROM Users as A
--INNER JOIN StaffEstablishedPost as B ON B.PositionId = A.PositionId and B.DepartmentId = A.DepartmentId and B.Salary = A.Salary
--where a.DepartmentId = 11356

--�������� ������ �����������, ���� ������� ���������� �������� ����
--UPDATE StaffEstablishedPost SET Salary = 0
--UPDATE StaffEstablishedPostRequest SET Salary = 0
--UPDATE StaffEstablishedPostArchive SET Salary = 0


--�� ������ ������ ���������� ����� ��������� �������� � ������� �������
SELECT A.[��� �������������7] as DepartmentId, A.[��� �����������], A.[��� �#�#], A.[��� ���������], B.Id as PositionId
			 ,case when not exists(SELECT * FROM StaffEstablishedPost WHERE DepartmentId = A.[��� �������������7] and PositionId = B.Id) and A.[��� �����������] is null and A.[��� �#�#] is null
						 then 1 else 0 end as IsNewSP
			 ,case when exists(SELECT * FROM StaffEstablishedPost WHERE DepartmentId = A.[��� �������������7] and PositionId = B.Id) and A.[��� �����������] is null and A.[��� �#�#] is null
						 then 1 else 0 end as IsEditSP
INTO #SP
FROM StaffEstablishedPostUserLinksTemp as A
INNER JOIN Position as B ON B.Code = A.[��� ���������]
WHERE isnull(A.[������� ��� �������������], N'') = N'1' and A.[��� �������������7] is not null 
			and isnull(A.[������� ��� ����������], N'') <> N'2' and A.[��� ���������] is not null
ORDER BY A.[��� ���������], A.[��� �������������7]


--select * from #SP
--�������� ������ � �����������

INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed, ReserveType, DocId, IsDismissal)
SELECT 1, B.Id, null, 1, null, null, 0
FROM #SP as A
INNER JOIN StaffEstablishedPost as B ON B.DepartmentId = A.DepartmentId and B.PositionId = A.PositionId
WHERE IsEditSP = 1


--����������, ������� ����� �������� � ������� �������

SELECT A.Id, B.cnt INTO #SPAdd
FROM StaffEstablishedPost as A
INNER JOIN (SELECT  DepartmentId, PositionId, sum(IsEditSP) as cnt
						FROM #SP 
						WHERE IsEditSP = 1
						GROUP BY DepartmentId, PositionId) as B ON B.DepartmentId = A.DepartmentId and B.PositionId = A.PositionId


UPDATE StaffEstablishedPost SET Quantity = A.Quantity + B.cnt
FROM StaffEstablishedPost as A
INNER JOIN #SPAdd as B ON B.Id = A.Id


IF (select sum(Quantity) from StaffEstablishedPost) <> (select count(*) from StaffEstablishedPostUserLinks)
	PRINT '���������� ������� � ����������� ���������� �� ����� ���������� ������� ������'
ELSE
	PRINT '������ ������� ����������!'

drop table #SP
drop table #SPAdd
drop table #Users
drop table #Vacation
drop table #TMP