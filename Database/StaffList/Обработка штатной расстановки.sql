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
SET @DepartmentId = 4189	--�������� ���������������


SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * 
,case when UserCode is not null and RegularCode is null then 1	--�������/����� �� ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 2	--�������� ��������� �������� �� ����� �����
							when UserCode = RegularCode and PregCode is not null and MoveCode is null and AbsentCode is null then 3	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is null and AbsentCode is not null then 4	--�������� ��������� � �� - ��������� ��������
							when UserCode = RegularCode and PregCode is null and MoveCode is not null and AbsentCode is null then 5	--�������� ��������� � �� - ��������� ��������
							when UserCode <> RegularCode and PregCode is null and RegularCode = MoveCode and AbsentCode is null then 6	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and RegularCode = AbsentCode then 7	--�������� ��������� � �� - �� ����� ��� ������� ��� ����� �� ���
							when UserCode <> RegularCode and UserCode = PregCode and MoveCode is null and AbsentCode is null then 8	--����������� ��������� � �� 
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
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
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
							else 99 end
--��������� ��������


--���� ���� ������ � ������� ��������, ������ ��������� (���������� ������ �������� � ���������� (�������� ����� ������� ������� �������), ����� ��������� �������������� �������� � ��������� ���������)
IF @IsOff = 0
BEGIN
	IF EXISTS(SELECT * FROM PersonnelArrangements WHERE len(isnull(RegularCode, '')) = 0 or len(isnull(RegularSurname, '')) = 0 or len(isnull(UserCode, '')) = 0 or len(isnull(Surname, '')) = 0)
	BEGIN
		PRINT N'�1 ���������� ������, ��� �� ������ �������� ���������!'
		DROP TABLE #PA
		RETURN
	END
END

--select UserCode, COUNT(UserCode) from PersonnelArrangements where UserCode <> RegularCode group by UserCode having COUNT(UserCode) > 1
IF EXISTS (SELECT * FROM #PA WHERE MoveCode is not null and RegularCode <> MoveCode)
BEGIN
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
					INNER JOIN Users as B ON B.Code = A.UserCode and (B.IsActive = 0 or B.RoleId & 2097152 > 0))
BEGIN
	PRINT N'�3.2 ���������� ��������� ����������, ������� � �������������� ������ �������� ������������!'
	DROP TABLE #PA
	RETURN
END

--������������ ���� � ���������
IF @IsOff = 0
BEGIN
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
END

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

IF @IsOff = 0
BEGIN
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
END



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

	IF isnull(@FactUserLinkId, 0) = 0
		SELECT @FactUserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId

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
	

	IF @RegUserName is not null and NOT EXISTS (SELECT * FROM Users WHERE Id = isnull(@RegUserId, 0) and IsActive = 1 and RoleId & 2 > 0)
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
		--������ � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId WHERE Id = @UserLinkId
		--������ ������ ��������� ����� ������
		UPDATE Users SET RegularUserLinkId = @UserLinkId WHERE Id = @UserId
	END
	
	--������ ������ ��� �� � ��
	IF @OrderId in (6, 7)
	BEGIN
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and IsUsed = 1)
		BEGIN
			INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
			VALUES (@UserLinkId, @UserId, @RegUserId, 1, case when @OrderId = 6 then 2 else 3 end)

			--INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			--VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
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

			SELECT top 1 @TempCode = UserCode FROM #PA WHERE RegularCode = @RegularCode and OrderId < @OrderId ORDER BY OrderId desc
			
			SELECT @TempUserId = Id FROM Users WHERE Code = @TempCode

			--������ ������ ����������� ��������� �� ����� ������
			INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
			VALUES (@FactUserLinkId, @UserId, @TempUserId, 1, @ReasonId)

			--INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
			--VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
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

/*
--����� ��������� ����� �������� � ����������� ������ ������ ��� �������� �����������
UPDATE StaffEstablishedPostUserLinks SET UserId = case when B.RegularUserLinkId = A.Id or B.TempUserLinkId = A.Id then A.UserId else null end
FROM StaffEstablishedPostUserLinks as A
INNER JOIN Users as B ON B.Id = A.UserId
--���������� ����������� ������� � ��/��/�� - ������ �� ������ �� ����� (� �������� ��������� ����� ������ ����� ���� �������� �� ������ ����� � �����������)
UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = B.RegularUserLinkId
FROM StaffTemporaryReleaseVacancyRequest as A
INNER JOIN Users as B ON B.Id = A.ReplacedId
INNER JOIN PersonnelArrangements as C ON C.RegularCode = B.Code and C.UserCode = B.Code
*/


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
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
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
							when UserCode <> RegularCode and PregCode is not null and MoveCode is null and UserCode = AbsentCode then 9	--����������� ��������� � ��
							when UserCode <> RegularCode and PregCode is null and MoveCode is null and AbsentCode is null then 10	--����������� ��������� �������� �� ����� �������� � ������� ������ �������
							else 99 end



select * from #PA
--where RegularCode in ('0000003465', '0000034169')
order by regularsurname, orderid
--order by regularsurname
--order by orderid
drop table #PA
--�������� ����� ���������� - 0000011356 - ��������� ��������� � ����� ��������, ��� ��������� ��� ��������� ���������� ��������, ��� ��� �� �� ����� ���� ���������� ������ ����� ������������� - 0000026218
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
*/