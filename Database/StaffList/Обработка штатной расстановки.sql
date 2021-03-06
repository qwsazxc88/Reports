--�� ������ ������ ������ �� ������������ �������������� ��/��/��, ��� ��� � �������������� ������ ����� �� �������������
--��� ������ �����������, ��� ���������� � �����, ����� ����� �������� � ���������
DECLARE
	@RegularCode nvarchar(20)
	,@UserCode nvarchar(20)
	,@PregCode nvarchar(20)
	,@MoveCode nvarchar(20)
	,@AbsentCode nvarchar(20)
	,@TempCode nvarchar(20)
	,@Id int
	,@DateAccept datetime
	,@PregBeginDate datetime
	,@PregEndDate datetime
	,@MoveBeginDate datetime
	,@AbsentBeginDate datetime
	,@UserId int			--����
	,@TempUserId int			--����
	,@RegUserId int		--������
	,@ReplaceUserId int	--��������� ���������� 
	,@PosititonId int
	,@RegPosititonId int
	,@DepartmentId int
	,@RegDepartmentId int
	,@SEPId int
	,@TempSEPId int
	,@UserLinkId int
	,@FactUserLinkId int
	,@TempUserLinkId int
	,@UserName nvarchar(250)
	,@RegUserName nvarchar(250)
	,@IsPreg bit
	,@IsRegPreg bit
	,@STDType int
	,@CountRow int
	,@OrderId int
	,@ReasonId int
	,@IsOff bit


SET NOCOUNT ON

SET @IsOff = 1	--��������� ��� �������� ����� ���������
--SET @DepartmentId = 4189	--�������� ���������������
--SET @DepartmentId = 9544 --������������ ����������� �1
--SET @DepartmentId = 8649 --�������� �����������
--SET @DepartmentId = 9545 --������������ ����������� �2
--SET @DepartmentId = 4188 --�������� ��������-���������
SET @DepartmentId = 8581 --�������� ���������


SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * 
,case when UserCode is not null and RegularCode is null then 1	--�������/����� �� ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--�������� ��������� �������� �� ����� �����
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--�������� ��������� � �� - ��������� ��������
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--����������� ��������� � �� 
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
							when UserCode <> RegularCode and UserCode = PregCode and RegularCode = MoveCode and AbsentCode is null then 11	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and UserCode = AbsentCode then 12	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							else 99 end as OrderId
,CAST(null as int) as UserLink	--���������
,CAST(null as int) as RegLink		--��������
INTO #PA FROM PersonnelArrangements
ORDER BY --RegularSurname,
		case when UserCode is not null and RegularCode is null then 1	--�������/����� �� ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--�������� ��������� �������� �� ����� �����
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--�������	� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--�������� ��������� � �� - ��������� ��������
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--����������� ��������� � �� 
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
							when UserCode <> RegularCode and UserCode = PregCode and RegularCode = MoveCode and AbsentCode is null then 11	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and UserCode = AbsentCode then 12	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							else 99 end
--��������� ��������


--���� ���� ������ � ������� ��������, ������ ��������� (���������� ������ �������� � ���������� (�������� ����� ������� ������� �������), ����� ��������� �������������� �������� � ��������� ���������)
--IF @IsOff = 0
--BEGIN
	IF EXISTS(SELECT * FROM PersonnelArrangements WHERE len(isnull(RegularCode, '')) = 0 or len(isnull(RegularSurname, '')) = 0 or len(isnull(UserCode, '')) = 0 or len(isnull(Surname, '')) = 0)
	BEGIN
		PRINT N'�1 ���������� ������, ��� �� ������ �������� ���������!'
		DROP TABLE #PA
		RETURN
	END
--END

--select UserCode, COUNT(UserCode) from PersonnelArrangements where UserCode <> RegularCode group by UserCode having COUNT(UserCode) > 1
IF EXISTS (SELECT * FROM #PA WHERE MoveCode is not null and RegularCode <> MoveCode)
BEGIN
	SELECT * FROM #PA WHERE MoveCode is not null and RegularCode <> MoveCode
	PRINT N'�2 ���������� ������, ��� ����������� ������� ������ �� ���������� ��������!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS (SELECT * FROM #PA WHERE OrderId = 99)
BEGIN
	PRINT N'�99 ���������� ������, ������� ������� ��������� � ������ ����� ������� ���������!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.RegularCode and (B.IsActive = 0 or B.RoleId & 2097152 > 0))
BEGIN
	PRINT N'�3.1 ���������� ��������� ����������, ������� � �������������� ������ �������� ���������!'
	DROP TABLE #PA
	RETURN
END



IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode and A.UserCode <> isnull(A.RegularCode, '') and (B.IsActive = 0 or B.RoleId & 2097152 > 0))
BEGIN
	PRINT N'�3.2 ���������� ��������� ����������, ������� � �������������� ������ �������� ������������!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS(SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and (RegularCode = PregCode /*or RegularCode = MoveCode*/ or RegularCode = AbsentCode))
BEGIN
	SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and (RegularCode = PregCode /*or RegularCode = MoveCode*/ or RegularCode = AbsentCode)
	PRINT N'�3.3 ���������� ����������� ���������� ������ �� ��, �� � �� ��� �������� �����������!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and PregCode is not null and PregCode <> RegularCode and PregCode <> UserCode)
BEGIN
	SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and PregCode is not null and PregCode <> RegularCode and PregCode <> UserCode
	PRINT N'�3.4 ���������� ����������� ���������� ������ �� ��!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and MoveCode is not null and MoveCode <> RegularCode and MoveCode <> UserCode)
BEGIN
	SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and MoveCode is not null and MoveCode <> RegularCode and MoveCode <> UserCode
	PRINT N'�3.5 ���������� ����������� ���������� ������ �� ��!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS(SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and AbsentCode is not null and AbsentCode <> RegularCode and AbsentCode <> UserCode)
BEGIN
	SELECT * FROM PersonnelArrangements WHERE RegularCode <> UserCode and AbsentCode is not null and AbsentCode <> RegularCode and AbsentCode <> UserCode
	PRINT N'�3.5 ���������� ����������� ���������� ������ �� ��!'
	DROP TABLE #PA
	RETURN
END


--������������ ���� � ���������
--IF @IsOff = 0
--BEGIN
	IF EXISTS(SELECT * FROM PersonnelArrangements as A
						INNER JOIN Users as B ON B.Code = A.UserCode and B.IsActive = 1 and B.RoleId & 2 > 0
						WHERE B.Name <> A.Surname)
	BEGIN
		PRINT N'�4.1 ���������� �������������� ��������� ������� � ��� ����������� ����������� � �������������� ������ � ����������� �����������!'
		DROP TABLE #PA
		RETURN
	END


	IF EXISTS(SELECT * FROM PersonnelArrangements as A
						INNER JOIN Users as B ON B.Code = A.RegularCode
						WHERE B.Name <> A.RegularSurname)
	BEGIN
		PRINT N'�4.2 ���������� �������������� ��������� ������� � ��� �������� ����������� � �������������� ������ � ����������� �����������!'
		DROP TABLE #PA
		RETURN
	END
--END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode
					WHERE A.PregCode is not null and B.Name <> A.Surname)
BEGIN
	PRINT N'�5 ���������� �������������� ��������� ������� � ��� ����������� � �� � �������������� ������ � ����������� �����������!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode
					WHERE A.MoveCode is not null and B.Name <> A.Surname)
BEGIN
	PRINT N'�6 ���������� �������������� ��������� ������� � ��� ����������� � �� � �������������� ������ � ����������� �����������!'
	DROP TABLE #PA
	RETURN
END

IF EXISTS(SELECT * FROM PersonnelArrangements as A
					INNER JOIN Users as B ON B.Code = A.UserCode
					WHERE A.AbsentCode is not null and B.Name <> A.Surname)
BEGIN
	PRINT N'�7 ���������� �������������� ��������� ������� � ��� ����������� � �� � �������������� ������ � ����������� �����������!'
	DROP TABLE #PA
	RETURN
END

--IF @IsOff = 0
--BEGIN
	IF EXISTS (SELECT C.* FROM Department as A
						 INNER JOIN Department as B ON B.Path like A.Path + '%' and B.ItemLevel = 7
						 INNER JOIN Users as C ON C.DepartmentId = B.Id and C.IsActive = 1 and (C.RoleId & 2 > 0 or C.RoleId & 16384 > 0)
						 INNER JOIN EmploymentCandidate as D ON D.UserId = C.Id and D.Status = 8 and D.SendTo1C is not null --and D.
						 WHERE a.Id = @DepartmentId and not exists (SELECT * FROM PersonnelArrangements WHERE UserCode = C.Code))
	BEGIN
		PRINT N'�8.0 ���������� ����� ����������, ������� ��� � �������������� ������!'
		SELECT C.* FROM Department as A
		INNER JOIN Department as B ON B.Path like A.Path + '%' and B.ItemLevel = 7
		INNER JOIN Users as C ON C.DepartmentId = B.Id and C.IsActive = 1 and (C.RoleId & 2 > 0 or C.RoleId & 16384 > 0)
		INNER JOIN EmploymentCandidate as D ON D.UserId = C.Id and D.Status = 8 and D.SendTo1C is not null --and D.
		WHERE a.Id = @DepartmentId and not exists (SELECT * FROM PersonnelArrangements WHERE UserCode = C.Code)
		DROP TABLE #PA
		RETURN
	END
--END



--==========================================================================
WHILE EXISTS (SELECT * FROM #PA WHERE IsComplete = 0)
BEGIN
	--����� �������������� ������
	SELECT top 1 @Id = A.Id, @OrderId = A.OrderId, @UserCode = A.UserCode, @RegularCode = A.RegularCode, @PregCode = A.PregCode, @MoveCode = A.MoveCode, @AbsentCode = A.AbsentCode
				 ,@MoveBeginDate = MoveBeginDate, @AbsentBeginDate = A.AbsentBeginDate, @DateAccept = A.DateAccept
				 ,@STDType = case when A.ContractType = '����������' then 1 when A.ContractType = '���' then 2 else 3 end 
				 ,@UserId = B.Id, @UserName = B.Name, @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @IsPreg = B.IsPregnant
				 ,@RegUserId = C.Id, @RegUserName = C.Name, @RegPosititonId = C.PositionId, @RegDepartmentId = C.DepartmentId, @IsRegPreg = C.IsPregnant
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode 
	LEFT JOIN Users as C ON C.Code = A.RegularCode 
	WHERE A.IsComplete = 0
	ORDER BY A.Id

	SELECT @PregBeginDate = null, @PregEndDate = null, @UserLinkId = null, @TempUserLinkId = null, @SEPId = null, @FactUserLinkId = null

	--���� ��������������, ����� ���������� ������������ ��������� � ���������� �������� ����� ������ ������
	SELECT top 1 @UserLinkId = C.Id
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode
	INNER JOIN StaffEstablishedPostUserLinks as C ON C.UserId = B.Id and C.IsUsed = 1
	INNER JOIN StaffEstablishedPost as D ON D.Id = C.SEPId and D.PositionId = B.PositionId and D.DepartmentId = B.DepartmentId
	WHERE A.RegularCode = @RegularCode
	ORDER BY A.OrderId desc

--��� ����� ��� �������� ����� ������
--	IF @OrderId not in (1, 2, 3, 4, 5)
	SELECT top 1 @FactUserLinkId = C.Id
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode
	INNER JOIN StaffEstablishedPostUserLinks as C ON C.UserId = B.Id and C.IsUsed = 1
	INNER JOIN StaffEstablishedPost as D ON D.Id = C.SEPId and D.PositionId = B.PositionId and D.DepartmentId = B.DepartmentId
	WHERE A.RegularCode = @UserCode
	ORDER BY A.OrderId desc
	--���� ���� � ����������� � ������ ����� ������ 
	IF isnull(@FactUserLinkId, 0) = 0
		SELECT @FactUserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId

	--���� ���� � �������, � � ����������� ��� ���
	IF isnull(@FactUserLinkId, 0) = 0
		SELECT @FactUserLinkId = Id FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1

	UPDATE #PA SET IsComplete = 1, UserLink = @FactUserLinkId, RegLink = @UserLinkId WHERE Id = @Id
END

UPDATE #PA SET IsComplete = 0


IF EXISTS(SELECT * FROM #PA	WHERE UserLink is null)
BEGIN
	PRINT N'�8 ������������ ����� � ����������� ��� ��������� �����������!'
	SELECT * FROM #PA	WHERE UserLink is null ORDER BY RegularSurname, OrderId
	DROP TABLE #PA
	RETURN
END


IF @IsOff = 0
BEGIN
	IF EXISTS (SELECT * FROM #PA as A
						 INNER JOIN (SELECT RegularCode, count(RegularCode) as cnt FROM #PA 
												 WHERE RegularCode in (SELECT RegularCode FROM #PA WHERE RegularCode = MoveCode)
												 GROUP BY RegularCode HAVING count(RegularCode) = 1) as B ON B.RegularCode = A.RegularCode and B.RegularCode = A.UserCode)
	BEGIN
		PRINT N'�9 � ���������� �������� ����������� ������������ ��������� ��������!'
		SELECT * FROM #PA as A
		INNER JOIN (SELECT RegularCode, count(RegularCode) as cnt FROM #PA 
								WHERE RegularCode in (SELECT RegularCode FROM #PA WHERE RegularCode = MoveCode)
								GROUP BY RegularCode HAVING count(RegularCode) = 1) as B ON B.RegularCode = A.RegularCode and B.RegularCode = A.UserCode
		ORDER BY RegularSurname, OrderId
		DROP TABLE #PA
		RETURN
	END
END
--==========================================================================
SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 0
PRINT N'����� ���������� ' + cast(@CountRow as nvarchar) + N' �������'


BEGIN TRANSACTION


--�� �������������� ����������� ������ �����������
UPDATE StaffEstablishedPostUserLinks SET UserId = null
WHERE UserId in (SELECT Id FROM Users as A
								INNER JOIN (SELECT distinct Code
														FROM (SELECT UserCode as Code FROM #PA 
																	UNION ALL
																	--��� ��� ����� ���� �������� �� ��������� ��������
																	SELECT RegularCode as Code FROM #PA WHERE RegularCode is not null) as A) as B ON B.Code = A.Code)

--������ ������� ������� � ������ ������
UPDATE Users SET RegularUserLinkId = null, TempUserLinkId = null
WHERE Id in (SELECT Id FROM Users as A
								INNER JOIN (SELECT distinct Code
														FROM (SELECT UserCode as Code FROM #PA 
																	UNION ALL
																	--��� ��� ����� ���� �������� �� ��������� ��������
																	SELECT RegularCode as Code FROM #PA WHERE RegularCode is not null) as A) as B ON B.Code = A.Code)

--���� �� �������
WHILE EXISTS (SELECT * FROM #PA WHERE IsComplete = 0)
BEGIN
	SELECT @PregBeginDate = null, @PregEndDate = null, @UserLinkId = null, @TempUserLinkId = null, @SEPId = null, @FactUserLinkId = null

	--����� �������������� ������
	SELECT top 1 @Id = A.Id, @OrderId = A.OrderId, @UserCode = A.UserCode, @RegularCode = A.RegularCode, @PregCode = A.PregCode, @MoveCode = A.MoveCode, @AbsentCode = A.AbsentCode
				 ,@MoveBeginDate = MoveBeginDate, @AbsentBeginDate = A.AbsentBeginDate, @DateAccept = A.DateAccept
				 ,@STDType = case when A.ContractType = '����������' then 1 when A.ContractType = '���' then 2 else 3 end 
				 ,@UserId = B.Id, @UserName = B.Name, @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @IsPreg = B.IsPregnant
				 ,@RegUserId = C.Id, @RegUserName = C.Name, @RegPosititonId = C.PositionId, @RegDepartmentId = C.DepartmentId, @IsRegPreg = C.IsPregnant
				 ,@UserLinkId = A.UserLink, @FactUserLinkId = RegLink
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode 
	LEFT JOIN Users as C ON C.Code = A.RegularCode 
	WHERE A.IsComplete = 0
	ORDER BY A.Id

	

	--���� ���� <> ������, �� ��������� ������ �����������
	IF NOT EXISTS (SELECT * FROM Users WHERE Id = @UserId and IsActive = 1 and (RoleId & 2 > 0 or RoleId & 16384 > 0))
	BEGIN
		--���� ����� ���������, �� ������ ��� ��������� ������ ��������� ����������
		--���� ����������� ��������� ������, ������ �� ������ �� �����������
		IF EXISTS (SELECT * FROM Users WHERE Id = @UserId and RoleId & 2097152 > 0)
		BEGIN
			--����� ������, ��� ������������ � ������� �� ��������� ����
			UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
			SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
			PRINT N'���������� ' + cast(@CountRow as nvarchar) + N' �������'
			CONTINUE
		END
		ELSE
		BEGIN
			PRINT N'�2 ������� ������ ������������ ���������� ' + @UserName + N' �� �������� �������� (Id = ' + cast(@UserId as nvarchar) + N')'
			ROLLBACK TRANSACTION
			DROP TABLE #PA
			RETURN
		END
	END
	

	IF @RegUserName is not null and NOT EXISTS (SELECT * FROM Users WHERE Id = isnull(@RegUserId, 0) and IsActive = 1 and (RoleId & 2 > 0 or RoleId & 16384 > 0))
	BEGIN
		PRINT N'�3 ������� ������ ��������� ���������� ' + @RegUserName + N' �� �������� �������� (Id = ' + cast(@RegUserId as nvarchar) + N')'
		ROLLBACK TRANSACTION
		DROP TABLE #PA
		RETURN
	END
	

	--���� ���� � �� ��������� �������� ����� ���� �������
	IF @PregCode is not null
	BEGIN
		SELECT @PregBeginDate = MIN(BeginDate), @PregEndDate = MAX(EndDate)
		FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @RegUserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
					UNION ALL
					SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @RegUserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A
	END

	
	--���� ������ ������� � �������� ��, �� � �� ������������, ��������� ���������� �� ����� (����������: ��, ��, ��)
	IF @RegularCode = @PregCode and @RegularCode = @MoveCode and @RegularCode = @AbsentCode
	BEGIN
		IF @PregBeginDate is not null
		BEGIN
			IF @PregBeginDate > @MoveBeginDate or @PregBeginDate > @AbsentBeginDate or @MoveBeginDate > @AbsentBeginDate
			BEGIN
				PRINT N'�4 ���������� ��������� ���������� ��� ���������� ' + @RegUserName
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
		END
	END


	--1. �����/������� �� ��������� ��������
	IF @OrderId = 1
	BEGIN
		--������ � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--������ ������ ��������� ����� ������
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId
	END
		
	--��������� ���������� ������ � ����������� � ����������� � ���� �������� ����� ������
	IF @OrderId in (2, 3, 4, 5)
	BEGIN
		--���� �� � ��� � ����
		IF @OrderId = 4 and NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @RegUserId)
		BEGIN
			INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			VALUES(1, @UserLinkId, @RegUserId, @AbsentBeginDate, null, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
		END

		--��� ��������� ���������, ���� ��� �� � ����, �� ������ ��
		IF @OrderId = 5 and NOT EXISTS (SELECT * FROM #PA WHERE Id <> @Id and RegularCode = @RegularCode and UserCode <> @RegularCode)
			 and NOT EXISTS (SELECT * FROM StaffMovements WHERE UserId = @RegUserId and IsTempMoving = 1 and Type in (2, 3) and Status = 12 and GETDATE() between MovementDate and MovementTempTo)
		BEGIN
			INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			VALUES(1, @UserLinkId, @RegUserId, @MoveBeginDate, null, 4, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ��� ������ ��������� �������.')
		END
		--������ � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--������ ������ ��������� ����� ������
		UPDATE Users SET RegularUserLinkId = @UserLinkId WHERE Id = @UserId
	END
	
	--������ ������ ��� �� � ��
	IF @OrderId in (6, 7, 11, 12)
	BEGIN
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1)
		BEGIN
			INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
			VALUES (@UserLinkId, @UserId, @RegUserId, 1, case when @OrderId in (6, 11) then 2 else 3 end)

			--INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			--VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
		END
		ELSE
		BEGIN
			UPDATE StaffPostReplacement SET UserLinkId = @UserLinkId WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1
		END

		--���� ���� �� � ��� � ����
		IF @OrderId = 12 and NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @UserId)
		BEGIN
			INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			VALUES(1, @UserLinkId, @UserId, @AbsentBeginDate, null, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
		END

		--������ � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--������ ��������� ����� ������ ������
		UPDATE Users SET TempUserLinkId = @UserLinkId, RegularUserLinkId = @UserLinkId WHERE Id = @RegUserId
		--������ ��������� ����� ������ � �����
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId
	END


	--������ ������ ����� ������ �� ����
	IF @OrderId in (8, 9, 10)
	BEGIN
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1)
		BEGIN
			--����� ���������� ���������� ������ �� ����� ��������� ���������� � ����� ����� ���������� ������� ������
			IF (SELECT top 1 PregCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc) is not null
				SET @ReasonId = 1
			IF (SELECT top 1 MoveCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc) is not null
				SET @ReasonId = 2
			IF (SELECT top 1 AbsentCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc) is not null
				SET @ReasonId = 3

			--���� ���� �������� �� �����
			IF @UserCode <> @PregCode and @UserCode <> @AbsentCode
				SELECT top 1 @TempCode = UserCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc
			ELSE	--���� ���� � ��, ������ �� ������� ������ ��� ���������� ����, ������� ����� ���� � �� ��� ��, ���� ����� �� �����, ������ ���� ������ ������
				SET @TempCode = isnull((SELECT top 1 UserCode FROM #PA WHERE RegularCode = @RegularCode and (UserCode = PregCode or UserCode = AbsentCode) and OrderId < @OrderId ORDER BY OrderId desc), @RegularCode)	
			
			

			SELECT @TempUserId = Id FROM Users WHERE Code = @TempCode

			--������ ������ ����������� ��������� �� ����� ������
			INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
			VALUES (@FactUserLinkId, @UserId, @TempUserId, 1, @ReasonId)

			
		END
		BEGIN
			UPDATE StaffPostReplacement SET UserLinkId = @FactUserLinkId WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1
		END

		IF @OrderId = 9 and NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @UserId)
		BEGIN
			INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			VALUES(1, @FactUserLinkId, @UserId, @AbsentBeginDate, null, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
		END

		--������ � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @FactUserLinkId
		--������ ��������� ����� ������ ������
		UPDATE Users SET RegularUserLinkId = @FactUserLinkId WHERE Id = @RegUserId
		--������ ��������� ����� ������ � �����
		UPDATE Users SET TempUserLinkId = @FactUserLinkId WHERE Id = @UserId
	END

	--����� ������, ����� �� �� ��������� � ���������� ���������	
	UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
	SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
	PRINT N'���������� ' + cast(@CountRow as nvarchar) + N' �������'
END




COMMIT TRANSACTION
PRINT N'������ ������� ����������!'
--ROLLBACK TRANSACTION


DROP TABLE #PA



/*
SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * 
,case when UserCode is not null and RegularCode is null then 1	--�������/����� �� ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--�������� ��������� �������� �� ����� �����
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--�������� ��������� � �� - ��������� ��������
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--����������� ��������� � �� 
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
							when UserCode <> RegularCode and UserCode = PregCode and RegularCode = MoveCode and AbsentCode is null then 11	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and UserCode = AbsentCode then 12	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							else 99 end as orderid
INTO #PA FROM PersonnelArrangements
ORDER BY --RegularSurname,
		case when UserCode is not null and RegularCode is null then 1	--�������/����� �� ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--�������� ��������� �������� �� ����� �����
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--�������	� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--�������� ��������� � �� - ��������� ��������
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--����������� ��������� � �� 
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
							when UserCode <> RegularCode and UserCode = PregCode and RegularCode = MoveCode and AbsentCode is null then 11	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and UserCode = AbsentCode then 12	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ��� � ���� � ��
							else 99 end



select * from #PA
--where RegularCode in ('0000003465', '0000034169')
order by regularsurname, orderid
--order by regularsurname
--order by orderid
drop table #PA
--�������� ����� ���������� - 0000011356 - ��������� ��������� � ������ ��������, ��� ��������� ��� ��������� ���������� ��������, ��� ��� �� �� ����� ���� ���������� ������ ����� ������������� - 0000026218
--�������� ����� ���������	0000007327 - ��������� ��������� � ������ ��������, ��� ��������� ��� ��������� ���, ��� ��� �� �� ����� ���� ���������� ��������� ���� ���������� - 0000012448 � �������� ������� ���������� -	0000034284
--select * from Users where Code = '0000000213'
--select * from Users where Name  = '��������� ������� ����������'
*/

/*
--��������������� ��������

--��������� ��������� ���������� (�� ���������)
delete from  StaffPostReplacement where Id = 230
--������ �� �� ���� ������ (�� ���������)
update StaffPostReplacement set UserLinkId = 5221 where Id = 82
--��������� ������ ��������� 0000036943 (�� ���������)
update StaffEstablishedPostUserLinks set UserId = 25619 WHERE Id = 5111
insert into StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
select Id, 25619, 8741, 1, 1 from StaffEstablishedPostUserLinks WHERE Id = 5111
--���������� ������ ��������	0000036945 (�� ���������)
update StaffEstablishedPostUserLinks set UserId = 25647 WHERE Id = 5117
insert into StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
select Id, 25647, 15191, 1, 1 from StaffEstablishedPostUserLinks WHERE Id = 5117

--������� ����� ������������ - 0000018076
update StaffEstablishedPostUserLinks set UserId = 8167 WHERE Id = 667
update Users SET RegularUserLinkId = 667 WHERE Id = 8167
--��������� ���� ������������� - 0000002248
--update StaffEstablishedPostUserLinks set UserId = 6152 WHERE Id = 25
--update Users SET RegularUserLinkId = 25 WHERE Id = 6152
--�������� ���������� ������������	0000035160
update StaffEstablishedPostUserLinks set UserId = 22833 WHERE Id = 111
update Users SET RegularUserLinkId = 111 WHERE Id = 22833
--���������� ��������� ���������	0000034532
update StaffEstablishedPostUserLinks set UserId = 20144 WHERE Id = 749
update Users SET RegularUserLinkId = 749 WHERE Id = 20144
--������ ����� ��������������	0000007810
update StaffEstablishedPostUserLinks set UserId = 1556 WHERE Id = 268
update Users SET RegularUserLinkId = 268 WHERE Id = 1556
--������ ����� ����������	0000008321
update StaffEstablishedPostUserLinks set UserId = 2360 WHERE Id = 836
update Users SET RegularUserLinkId = 836 WHERE Id = 2360
--���� ����� ������������	0000003389
update StaffEstablishedPostUserLinks set UserId = 6558 WHERE Id = 749
update Users SET RegularUserLinkId = 749 WHERE Id = 6558
--������ ��������� ��������� 0000016008
update StaffEstablishedPostUserLinks set UserId = 4636 WHERE Id = 1759
update Users SET RegularUserLinkId = 1759 WHERE Id = 4636


--�������� �����������
	--�� ��������
		--������� �.�. � ����������� ��� ������������ ������ ������������ - 0000034559
		UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE iD = 10655
		--������� ������� ���������	0000038005
		UPDATE StaffEstablishedPostUserLinks SET UserId = 26545 WHERE iD = 9118

	--����� �������� 
		--������������ ������ ������������ - 0000034559
		update StaffEstablishedPostUserLinks set UserId = 20285 WHERE Id = 10655
		update Users SET RegularUserLinkId = 10655 WHERE Id = 20285
		--���� �������� ������� ��, ��������� �� ��� �� ��� ��
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10655 where ReplacedId = 20285
		--������ ���������� / ��������� ������� �� ������ ������ � ������� �������� / ������
		update StaffPostReplacement set UserLinkId = 5683 where Id = 115


--������������ ����������� �2
	--�� ��������
		--���������
		--�������� ������� ��������� - 0000038015
		update StaffEstablishedPostUserLinks set UserId = 26640 WHERE Id = 2947
		update Users SET RegularUserLinkId = 2947 WHERE Id = 26640
		--�������� ��
			--5 ���������� ������ ���������� - 0000038016, VALUES(1, 356, 9522, 1, 12000, 1, '20151224')
			--update StaffEstablishedPostUserLinks set UserId = 26642 WHERE Id = 10674
			--update Users SET RegularUserLinkId = 10674 WHERE Id = 26642
			--6 ����� ������ ���������� - 0000038031, VALUES(1, 356, 10366, 1, 12000, 1, '20151224')
			--update StaffEstablishedPostUserLinks set UserId = 26603 WHERE Id = 10675
			--update Users SET RegularUserLinkId = 10675 WHERE Id = 26603
			--7 �������� ���� ���������� - 0000038001, VALUES(1, 59, 12226, 1, 18000, 1, '20151224')
			--update StaffEstablishedPostUserLinks set UserId = 26427 WHERE Id = 10676
			--update Users SET RegularUserLinkId = 10676 WHERE Id = 26427
			--8 �������� ������� ��������� - 0000037900,VALUES(1, 356, 12274, 1, 7000, 1, '20151224')
			--update StaffEstablishedPostUserLinks set UserId = 26327 WHERE Id = 10677
			--update Users SET RegularUserLinkId = 10677 WHERE Id = 26327
	--����� ��������
		--��������� �������� ����� ������ � ��
		--�������� ������ ���������� - 0000017261
		update StaffEstablishedPostUserLinks set UserId = 7419 WHERE Id = 3064
		update Users SET RegularUserLinkId = 3064 WHERE Id = 7419
		--��������� ������� ������������ - 0000025570
		update StaffEstablishedPostUserLinks set UserId = 14574 WHERE Id = 3239
		update Users SET RegularUserLinkId = 3239 WHERE Id = 14574
		--��������� ����� ������������� - 0000019816
		update StaffEstablishedPostUserLinks set UserId = 9186 WHERE Id = 3180
		update Users SET RegularUserLinkId = 3180 WHERE Id = 9186
		--���������� ���� ������������� - 0000029906
		--1 �������� ����� ������� ������� - '0000029906', VALUES(1, 356, 9243, 1, 12000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 17195 WHERE Id = 10670
		update Users SET RegularUserLinkId = 10670 WHERE Id = 17195
		--2 ������ ������ �������������� - 0000025599, VALUES(1, 356, 9522, 1, 12000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 14519 WHERE Id = 10671
		update Users SET RegularUserLinkId = 10671 WHERE Id = 14519
		--3 �������� ������ �������� - 0000025537, VALUES(1, 32, 9243, 1, 14000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 14543 WHERE Id = 10672
		update Users SET RegularUserLinkId = 10672 WHERE Id = 14543
		--������ ������� ��������� - 0000032097
		update StaffEstablishedPostUserLinks set UserId = 18475 WHERE Id = 8478
		update Users SET RegularUserLinkId = 8478 WHERE Id = 18475
		--����������� ������ ��������� - 0000025574
		update StaffEstablishedPostUserLinks set UserId = 14568 WHERE Id = 8481
		update Users SET RegularUserLinkId = 8481 WHERE Id = 14568
		--4 ������� ����� ������������ - 0000005139, VALUES(1, 317, 438, 1, 300000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 3825 WHERE Id = 10673
		update Users SET RegularUserLinkId = 10673 WHERE Id = 3825
		--9 �������� ������ ������������� -	0000025105, VALUES(1, 472, 9246, 1, 17000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 14231 WHERE Id = 10678
		update Users SET RegularUserLinkId = 10678 WHERE Id = 14231

		--���� �������� ������� �� ��� �����������, ������� � ��, �� �� ����� �� ������, ��������� �� ��� �� ��� ��
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10671 where ReplacedId = 14519
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 3064 where ReplacedId = 7419
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 3239 where ReplacedId = 14574
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 3180 where ReplacedId = 9186
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10670 where ReplacedId = 17195
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10672 where ReplacedId = 14543
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 8478 where ReplacedId = 18475
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 8481 where ReplacedId = 14568
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10673 where ReplacedId = 3825
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10678 where ReplacedId = 14231

		--�����
		delete from StaffPostReplacement where Id = 41
		update Users set RegularUserLinkId = null where Code = '0000037759'
		


--��������-��������� ��������
	--�� ��������� 
		--�������� ������ ���������� - 0000006797
		INSERT INTO StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
		VALUES(4799, 19249, 1657, 1, 2)
		--����������� ���� ���������� - 0000037915
		update StaffEstablishedPostUserLinks set UserId = 26565 where Id = 8926
		--��������� ������ ����������������	0000037978
		INSERT INTO StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
		VALUES(4705, 26594, 16912, 1, 1)
		update StaffEstablishedPostUserLinks set UserId = 26594 where Id = 4705
		--������� ������ �����������	0000037917
		INSERT INTO StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
		VALUES(4703, 26572, 16911, 1, 1)
		update StaffEstablishedPostUserLinks set UserId = 26572 where Id = 4703
	--����� ���������
		--��������� �������� ����� (�� ���������) ������ � ��
		--������������ ����� ������������� - 0000016812, � ���� �� �������������, VALUES(1, 233, 1761, 1, 6000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 6959 where Id = 10749
		update Users set RegularUserLinkId = 10749 where Id = 6959
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10749 where ReplacedId = 6959
		--������� ��������� ������� - 0000013538, VALUES(1, 233, 3714, 1, 6000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 4944 where Id = 10750
		update Users set RegularUserLinkId = 10750 where Id = 4944
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10750 where ReplacedId = 4944
		--�������� ����� ��������� - 0000020826, VALUES(1, 233, 2213, 1, 6000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 9743 where Id = 10751
		update Users set RegularUserLinkId = 10751 where Id = 9743
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10751 where ReplacedId = 9743
		--����� ������� ��������� - 0000024207
		update StaffEstablishedPostUserLinks set UserId = 12853 where Id = 4698
		update Users set RegularUserLinkId = 4698 where Id = 12853
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 4698 where ReplacedId = 12853
		--�������� ��� ����������� - 0000008427
		update StaffEstablishedPostUserLinks set UserId = 6250 where Id = 4619
		update Users set RegularUserLinkId = 4619 where Id = 6250
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 4619 where ReplacedId = 6250
		--��������� ����� �������� - 0000012949--, VALUES(1, 23, 3751, 1, 8500, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 6309 where Id = 1133
		update Users set RegularUserLinkId = 1133 where Id = 6309
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 1133 where ReplacedId = 6309
		--��������� ���� ���������� - 0000012448--, VALUES(1, 233, 1933, 1, 6000, 1, '20151224')
		update StaffEstablishedPostUserLinks set UserId = 4204 where Id = 1762
		update Users set RegularUserLinkId = 1762 where Id = 4204
		update StaffTemporaryReleaseVacancyRequest set UserLinkId = 1762 where ReplacedId = 4204


		--��������� ��������� ��������� �������
		update Users set RegularUserLinkId = 4973, TempUserLinkId = 4974 where Id = 4196
		update Users set RegularUserLinkId = 4975 where Id = 5684
		update Users set RegularUserLinkId = 4974, TempUserLinkId = 4975 where Id = 5966
		update Users set RegularUserLinkId = 8081, TempUserLinkId = 4973 where Id = 10296
		update Users set TempUserLinkId = 4973 where Id = 19710
		update Users set TempUserLinkId = 8081 where Id = 25675
		--������ ������
		update StaffPostReplacement set UserLinkId = 4974 where Id = 1652
		update StaffPostReplacement set UserLinkId = 4973, ReplacedId = 19710 where Id = 1656
		--�����������
		update StaffEstablishedPostUserLinks set UserId = 4196 where Id = 4974
		update StaffEstablishedPostUserLinks set UserId = 5966 where Id = 4975
		update StaffEstablishedPostUserLinks set UserId = 10296 where Id = 4973
		update StaffEstablishedPostUserLinks set UserId = 25675 where Id = 8081

		--========================================================
		update Users set RegularUserLinkId = 211 where id = 2513
		update Users set TempUserLinkId = 211 where id = 1443
		update StaffPostReplacement set UserLinkId = 211 where Id = 1658



--�������� ���������
	--�� ���������
	--����� ���������
	--������ ���� �������� 0000013386 - VALUES(1, 463, 8754, 1, 25000, 1, '20151224')
	update StaffEstablishedPostUserLinks set UserId = 1854 where Id = 10757
	update Users set RegularUserLinkId = 10757 where Id = 1854
	update StaffTemporaryReleaseVacancyRequest set UserLinkId = 10757 where ReplacedId = 1854
	--������� ���� ��������� 0000026815
	update StaffEstablishedPostUserLinks set UserId = 15505 where Id = 7455
	update Users set RegularUserLinkId = 7455 where Id = 15505
	update StaffTemporaryReleaseVacancyRequest set UserLinkId = 7455 where ReplacedId = 15505
	--����� ����� ���������� 0000027629
	update StaffEstablishedPostUserLinks set UserId = 16053 where Id = 6761
	update Users set RegularUserLinkId = 6761 where Id = 16053
	update StaffTemporaryReleaseVacancyRequest set UserLinkId = 6761 where ReplacedId = 16053


	--������������ ���������� ����� ������������� 2973, �������� ������� ���������� 9221 � �������� ������ ������������� 19308
	select * from StaffEstablishedPostUserLinks where Id = 6606
	select * from StaffPostReplacement where UserLinkId = 6606
	update StaffPostReplacement set userId = 9221 where Id = 1907
	update StaffPostReplacement set userid = 19308, ReplacedId = 9221 where Id = 1908
	update StaffEstablishedPostUserLinks set UserId = 19308 where Id = 6606
*/
