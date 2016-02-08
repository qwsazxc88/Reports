--�� ������ ������ ������ �� ������������ �������������� ��/��/��, ��� ��� � �������������� ������ ����� �� �������������
--��� ������ �����������, ��� ���������� � �����, ����� ����� �������� � ���������
DECLARE
	@RegularCode nvarchar(20)
	,@UserCode nvarchar(20)
	,@PregCode nvarchar(20)
	,@MoveCode nvarchar(20)
	,@AbsentCode nvarchar(20)
	,@Id int
	,@DateAccept datetime
	,@PregBeginDate datetime
	,@PregEndDate datetime
	,@MoveBeginDate datetime
	,@AbsentBeginDate datetime
	,@UserId int			--����
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

SET NOCOUNT ON

SELECT IDENTITY(INT, 1, 1) as Id, CAST(0 as bit) as IsComplete, * INTO #PA FROM PersonnelArrangements

--��������� ��������


--���� ���� ������ � ������� ��������, ������ ���������
IF EXISTS(SELECT * FROM PersonnelArrangements WHERE len(isnull(RegularCode, '')) = 0)
BEGIN
	PRINT N'�1 ���������� ������, ��� �� ������ �������� ���������!'
	DROP TABLE #PA
	RETURN
END


IF EXISTS (SELECT * FROM #PA WHERE MoveCode is not null and RegularCode <> MoveCode)
BEGIN
	PRINT N'�16 ���������� ������, ��� ����������� ������� ������ �� ���������� ��������!'
	DROP TABLE #PA
	RETURN
END


SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 0
PRINT N'����� ���������� ' + cast(@CountRow as nvarchar) + N' �������'

BEGIN TRANSACTION

--���� �� �������
WHILE EXISTS (SELECT * FROM #PA WHERE IsComplete = 0)
BEGIN
	--����� �������������� ������
	SELECT top 1 @Id = A.Id, @UserCode = A.UserCode, @RegularCode = A.RegularCode, @PregCode = A.PregCode, @MoveCode = A.MoveCode, @AbsentCode = A.AbsentCode
				 ,@MoveBeginDate = MoveBeginDate, @AbsentBeginDate = A.AbsentBeginDate, @DateAccept = A.DateAccept
				 ,@STDType = case when A.ContractType = '����������' then 1 when A.ContractType = '���' then 2 else 3 end 
				 ,@UserId = B.Id, @UserName = B.Name, @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @IsPreg = B.IsPregnant
				 ,@RegUserId = C.Id, @RegUserName = C.Name, @RegPosititonId = C.PositionId, @RegDepartmentId = C.DepartmentId, @IsRegPreg = C.IsPregnant
	FROM #PA as A
	INNER JOIN Users as B ON B.Code = A.UserCode 
	INNER JOIN Users as C ON C.Code = A.RegularCode 
	WHERE A.IsComplete = 0
	ORDER BY case when A.UserCode = A.RegularCode then 0 else 1 end


	--���� ���� <> ������, �� ��������� ������ �����������
	IF NOT EXISTS (SELECT * FROM Users WHERE Id = @UserId and IsActive = 1 and RoleId & 2 > 0)
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

	IF NOT EXISTS (SELECT * FROM Users WHERE Id = @RegUserId and IsActive = 1 and RoleId & 2 > 0)
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

	--������� ������� ������� �� ������������ ���������
	SELECT @SEPId = A.Id FROM StaffEstablishedPost as A
	INNER JOIN StaffEstablishedPostUserLinks as B ON B.SEPId = A.Id and B.UserId = @UserId and B.IsUsed = 1
	WHERE A.PositionId = @PosititonId and A.DepartmentId = @DepartmentId and A.IsUsed = 1 and A.Quantity <> 0
	
	IF isnull(@SEPId, 0) = 0
	BEGIN
		PRINT N'�5 ���������� ���������� ������� ������� ��� ���������� ' + @UserName
		ROLLBACK TRANSACTION
		DROP TABLE #PA
		RETURN
	END


	--���� ���� ��������� � ������� (������ ���� ������ � ��/��/��, �� �� ����� �� �������)
	IF @UserCode = @RegularCode
	BEGIN
		--������� ����� � �����������
		SELECT @UserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @UserId and IsUsed = 1 --and isnull(ReserveType, 0) = 0
	
		IF isnull(@UserLinkId, 0) = 0
		BEGIN
			PRINT N'�6 ���������� ���������� ������� � ������� ����������� ��� ���������� ' + @UserName + N' (' + @UserCode + N')'
			ROLLBACK TRANSACTION
			DROP TABLE #PA
			RETURN
		END

		--��� ��������� ������ Id ����������� � ������ ���������� � ���� ����������� ����� � �����������
		UPDATE Users SET RegularUserLinkId = @UserLinkId WHERE Id = @RegUserId


		--���� ������ � �� � ��� ������ ������ � ������ ��� ������, ��� ��������� � ����������� �������� ���� ����� ��� ��� �������
		IF @RegularCode = @PregCode and NOT EXISTS (SELECT * FROM #PA WHERE UserCode <> RegularCode and RegularCode = @RegularCode)
		BEGIN
			--���� �� ���������� ��� ����������� ������ �� ������ �� ����� �� ��������, ����������� �� ������������, � ������� ������������ � ������ ������ �� 1�
			IF NOT EXISTS (SELECT *
										 FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @RegUserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
											 		 UNION ALL
													 SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @RegUserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A) and isnull(@IsRegPreg, 0) = 1
			BEGIN
				--������������ ������ �� �� (����������)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'�������������� ��������� ������: �� ������ ��������� ������ ��� ������ ������� ��')
			END
		END

			

		--���� ������ � ��������� �������� ��� ������, ��� ��������� � ����������� �������� ���-�� ��� ��������� �����, ����� ������ ����� ���������� ���� ��� ���� <> ������
		/*
			--������� ������ ����������� ����� ������ (�������� ����������)
			--� ������ ������ �������� id ������� � ����������� � ���� ���������� ����� ������
		
			--���� ��� ����� ����� ��������� ����������, �� ����� ��������� ���� ��������� ��������� � ����������� ���  �������
				--���� � �������, �� ����� ��������� ��� �������� �������������� ��������� ��� ���.
				--���� �� ����������� ���������� ��� ������, �� ������� ������ � �������
				*/

		--���� ������ � ���������� ���������� � ��� ������ ������ � ������
		IF @RegularCode = @AbsentCode --and NOT EXISTS (SELECT * FROM #PA WHERE UserCode <> RegularCode and RegularCode = @RegularCode)
		BEGIN
			--���� �� ���������� ��� ����������� ������ �� ������ �� ����� �� ��������, ����������� �� ������������, � ������� ������������ � ������ ������ �� 1�
			IF NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @RegUserId and DateBegin = @AbsentBeginDate and IsUsed = 1)
			BEGIN
				--������������ ������ �� �� 
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @AbsentBeginDate, null, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
			END
		END
	END
	

	

	--���� ���� �� ��������� � ������� (������ �� ������) ������ �� ����� ��������� ���������� ��� ������ ����� ��������� �� ��� ��� ��� �������� ���-�� ������� �� ��� ����������
	IF @UserCode <> isnull(@RegularCode, '') and len(isnull(@RegularCode, '')) <> 0
	BEGIN
		--��� �����������, ��� ���� <> ������ 
		--�������� ����� ������ ���� ���������� �� ������������ ���������� �� ������, ��� ���� <> ������ (@UserLinkId)
		SELECT @UserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @UserId and IsUsed = 1 and isnull(ReserveType, 0) = 0
		--���� �� ������ ����������, �� �������� ��������� ��� ���-�� ��������, ���� � �������
		IF isnull(@UserLinkId, 0) = 0
			SELECT @UserLinkId = UserLinkId FROM StaffPostReplacement WHERE ReplacedId = @RegUserId and IsUsed = 1


		--��������� ����� ������ ���� ���������� �� ������, ��� ������ �������� ������ (@TempUserLinkId)
		IF EXISTS(SELECT * FROM #PA WHERE UserCode = @RegularCode and RegularCode <> @RegularCode)
		BEGIN
			--������� ������� ������� �� ������������ ���������
			--���������� ������� ������� ��� ���������� ����� ������
			
			SELECT @PosititonId = B.PositionId, @DepartmentId = B.DepartmentId, @TempSEPId = C.Id
			FROM #PA as A
			INNER JOIN Users as B ON B.Code = A.UserCode
			INNER JOIN StaffEstablishedPost as C ON C.PositionId = B.PositionId and C.DepartmentId = B.DepartmentId and C.IsUsed = 1 and C.Quantity <> 0
			INNER JOIN StaffEstablishedPostUserLinks as D ON D.SEPId = C.Id and D.UserId = B.Id and D.IsUsed = 1
			WHERE A.UserCode = @RegularCode and A.RegularCode <> @RegularCode

			--SELECT @TempSEPId = Id FROM StaffEstablishedPost WHERE PositionId = @PosititonId and DepartmentId = @DepartmentId and IsUsed = 1 and Quantity <> 0


			SELECT @TempUserLinkId = Id FROM StaffEstablishedPostUserLinks WHERE SEPId = @TempSEPId and UserId = @RegUserId and IsUsed = 1 and isnull(ReserveType, 0) = 0
			--���� �� ������ ����������, �� �������� ��������� ��� ���-�� ��������, ���� � �������
			IF isnull(@TempUserLinkId, 0) = 0
				SELECT @TempUserLinkId = UserLinkId FROM StaffPostReplacement WHERE ReplacedId = @RegUserId and IsUsed = 1

			IF isnull(@TempUserLinkId, 0) = 0
			BEGIN
				PRINT N'�15 ���������� ���������� ��������� ������� � ������� ����������� ��� ��������� ���������� ' + @RegUserName + N' (' + @RegularCode + N')'
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
		END
		ELSE
		BEGIN
			SET @TempUserLinkId = null
		END


		--��� ��������� ������ Id ����������� � ������ ���������� � ���� ����������� ����� � �����������
		UPDATE Users SET RegularUserLinkId = @UserLinkId, TempUserLinkId = @TempUserLinkId WHERE Id = @RegUserId
		--��� ����� ������ Id ����������� � ������ ���������� � ���� ���������� ����� � �����������
		UPDATE Users SET TempUserLinkId = @UserLinkId WHERE Id = @UserId

		--���� ��� ���� ���������� ������, �� ������ �� ��������� ����
		IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId = @RegUserId and UserLinkId = @UserLinkId)
		BEGIN
			--����� ������, ��� ������������ � ������� �� ��������� ����
			UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
			SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
			PRINT N'���������� ' + cast(@CountRow as nvarchar) + N' �������'
			CONTINUE
		END
		ELSE
		BEGIN
			IF isnull(@UserLinkId, 0) = 0
			BEGIN
				PRINT N'�7 ���������� ���������� ������� � ������� ����������� ��� ��������� ���������� ' + @RegUserName + N' (' + @RegularCode + N')'
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
		END


		--���� ������ � ��, 
		IF @RegularCode = @PregCode
		BEGIN
			--���� �� ���������� ��� ����������� ������ �� ������ �� ����� �� ��������, ����������� �� ������������, � ������� ������������ � ������ ������ �� 1�
			IF NOT EXISTS (SELECT *
										 FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @RegUserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
											 		 UNION ALL
													 SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @RegUserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A) and isnull(@IsRegPreg, 0) = 1
			BEGIN
				--������������ ������ �� �� (����������)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @PregBeginDate, @PregEndDate, 3, 1, N'�������������� ��������� ������: �� ������ ��������� ������ ��� ������ ������� ��')
			END


			--���� ����� ������������ ���������� � �����������, ���� �� �����
			IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
			BEGIN
				--���� ��������� � �����������, ���� �� ������ ������������ ����������
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
				BEGIN
					PRINT N'�9 ������������ ���������� ��� � ����������� � ��� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ��������� ' + @RegUserName + N' (' + @RegularCode + N')'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
					--���� ����, �� ���� ���� ������ ()
			END
			ELSE--���� ����������� ��������� ���� � �����������
			BEGIN
				--����� ��������� �������� �� �� ���������
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
				BEGIN
					--���� �� ��������, � �������� ��������� ��� ��� � ����������� �� ������� ����� ��� �������� ��� ��������� �� ������ �����, �� ������� ������ ������ 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
					BEGIN
						--������� �� ������ ������������ ����������
						INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
						SELECT Id, UserId, @RegUserId, 1, 1 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId

						--����� � ����������� ������������ ���������� ���������� �� ������� ��������� ����������
						IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
						BEGIN
							--����� ��������� ������� ���������
							UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

							--���� ����� � ��������� �� ��������� ���������� ���������� ������ �� ��, �� ��������� �� �� ��� �����
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
						END
						ELSE
						BEGIN
							--�������� � ����������� ��������� �������� ����� ���� � ������ ������� ��������, �� ����� ����� ������� ��������� ���������� ������� ��������� ������ ��������
							--���� ��� ������� � ������� �� Id ��������� ���������� � ���������� ����� �� ���� �������
							UPDATE StaffEstablishedPostUserLinks SET UserId = null 
							FROM StaffEstablishedPostUserLinks as A
							WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
						END
					END
				END
				ELSE	--��������� ������� ������ ����������� ����������� ����������� ����������, �� ���������
				BEGIN
					IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
					BEGIN
						PRINT N'�10 ���������� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ������������ ����������!'
						ROLLBACK TRANSACTION
						DROP TABLE #PA
						RETURN
					END
				END
			END
		END


		--���� ���� �� �������� �������� � � ��, (��� ���������� ������� �� ��� � � ��� ��� ����������� ����� ������)
		IF @UserCode <> @RegularCode and @UserCode = @PregCode
		BEGIN
			--���� �� ������������ ���������� ��� ����������� ������ �� ������ �� ����� �� ��������, ����������� �� ������������, � ������� ������������ � ������ ������ �� 1�
			IF NOT EXISTS (SELECT *
										 FROM (SELECT BeginDate, EndDate FROM ChildVacation WHERE UserId = @UserId and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
											 		 UNION ALL
													 SELECT BeginDate, EndDate FROM Sicklist WHERE UserId = @UserId and TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A) and isnull(@IsPreg, 0) = 1
			BEGIN
				--������������ ������ �� �� (����������)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @UserId, @PregBeginDate, @PregEndDate, 3, 1, N'�������������� ��������� ������: �� ������ ��������� ������ ��� ������ ������� ��')
			END


			--���� ����� ������������ ���������� � �����������, ���� �� �����
			IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
			BEGIN
				--���� ��������� � �����������, ���� �� ������ ������������ ����������
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
				BEGIN
					PRINT N'�9 ������������ ���������� ��� � ����������� � ��� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ��������� ' + @RegUserName + N' (' + @RegularCode + N')'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
					--���� ����, �� ���� ���� ������ ()
			END
			ELSE--���� ����������� ��������� ���� � �����������
			BEGIN
				--����� ��������� �������� �� �� ���������
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
				BEGIN
					--���� �� ��������, � �������� ��������� ��� ��� � ����������� �� ������� ����� ��� �������� ��� ��������� �� ������ �����, �� ������� ������ ������ 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
					BEGIN
						--������� �� ������ ������������ ����������
						INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
						SELECT Id, UserId, @RegUserId, 1, 1 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId

						--����� � ����������� ������������ ���������� ���������� �� ������� ��������� ����������
						IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
						BEGIN
							--����� ��������� ������� ���������
							UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

							--���� ����� � ��������� �� ��������� ���������� ���������� ������ �� ��, �� ��������� �� �� ��� �����
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
						END
						ELSE
						BEGIN
							--�������� � ����������� ��������� �������� ����� ���� � ������ ������� ��������, �� ����� ����� ������� ��������� ���������� ������� ��������� ������ ��������
							--���� ��� ������� � ������� �� Id ��������� ���������� � ���������� ����� �� ���� �������
							UPDATE StaffEstablishedPostUserLinks SET UserId = null 
							FROM StaffEstablishedPostUserLinks as A
							WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
						END
					END
					ELSE	--���� ���� �� �������� ������ � ������ ��� ��� � ����������� (��������������)
					BEGIN
						--���� ����� ������ ������ ���-��, ��� ���� ���-�� ��������� � �����
						IF EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @RegUserId and IsUsed = 1)
						BEGIN
							--���������� ���������� �����������
							SELECT top 1 @ReplaceUserId = B.UserId
							FROM StaffPostReplacement as A
							INNER JOIN StaffPostReplacement as B ON B.UserLinkId = A.UserLinkId
							WHERE A.ReplacedId = @RegUserId
							ORDER BY B.Id desc

							--��������� ������� ������� �� ��� �� � ����������, ������� �� ������ ������ �������� ������
							IF NOT EXISTS (SELECT * FROM Users as A
														 INNER JOIN #PA as B ON B.PregCode = A.Code or B.AbsentCode = A.Code or B.MoveCode = A.Code
														 WHERE A.Id = @ReplaceUserId)
							BEGIN
								--���� ��� �������, �� ������ ��������� (�������� �������� ���������� ������� ��� ���������)
								PRINT N'�17 ���������� ������ ��������� ���������� ' + @RegUserName + N' (' + @RegularCode + N') �����������, � �������� ��� ��������� �� � ��!'
								ROLLBACK TRANSACTION
								DROP TABLE #PA
								RETURN
							END

							IF NOT EXISTS(SELECT * FROM StaffPostReplacement as A
														WHERE ReplacedId = @RegUserId)
							
						--���� ����, �� ������� ������
						--� ����������� ��������� ����� �� ������
						END
						--���� ��� ������ ������, ������ ���������
					END
				END
				ELSE	
				BEGIN
					--��������� ������� ������ ����������� ����������� ����������� ����������, �� ���������
					IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
					BEGIN
						PRINT N'�10 ���������� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ������������ ����������!'
						ROLLBACK TRANSACTION
						DROP TABLE #PA
						RETURN
					END
				END
			END
		END




		--���� ������ � ��������� ��������
		IF @RegularCode = @MoveCode
		BEGIN
			--���� ��� ��������, �� ������� ������ �� ��
			IF NOT EXISTS(SELECT * FROM StaffMovements WHERE Type in (2, 3) and Status = 12 and UserId = @RegUserId and TargetStaffEstablishedPostRequest = @UserLinkId)
			BEGIN
				--������������ ������ �� �� (����������)
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @MoveBeginDate, null, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� �������� �����������, �� ������ ���!')
			END


			--���� ����� ������������ ���������� � �����������, ���� �� �����
			IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
			BEGIN
				--���� ��������� � �����������, ���� �� ������ ������������ ����������
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
				BEGIN
					PRINT N'�11 ������������ ���������� ��� � ����������� � ��� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ��������� ' + @RegUserName + N' (' + @RegularCode + N')'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
					--���� ����, �� ���� ���� ������ ()
			END
			ELSE--���� ����������� ��������� ���� � �����������
			BEGIN
				--����� ��������� �������� �� �� ���������
				IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
				BEGIN
					--���� �� ��������, � �������� ��������� ��� ��� � ����������� �� ������� ����� ��� �������� ��� ��������� �� ������ �����, �� ������� ������ ������ 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
					BEGIN
						--������� �� ������ ������������ ����������
						INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
						SELECT Id, UserId, @RegUserId, 1, 2 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId
						
						--����� � ����������� ������������ ���������� ���������� �� ������� ��������� ����������
						IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
						BEGIN
							--����� ��������� ������� ���������
							UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

							--���� ����� � ��������� �� ��������� ���������� ���������� ������ �� ��, �� ��������� �� �� ��� �����
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
						END
						/*ELSE
						BEGIN
							--�������� � ����������� ��������� �������� ����� ���� � ������ ������� ��������, �� ����� ����� ������� ��������� ���������� ������� ��������� ������ ��������
							--���� ��� ������� � ������� �� Id ��������� ���������� � ���������� ����� �� ���� �������
							UPDATE StaffEstablishedPostUserLinks SET UserId = null 
							FROM StaffEstablishedPostUserLinks as A
							WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
						END*/
					END
				END
				ELSE	--��������� ������� ������ ����������� ����������� ����������� ����������, �� ���������
				BEGIN
					IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
					BEGIN
						PRINT N'�12 ���������� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ������������ ����������!'
						ROLLBACK TRANSACTION
						DROP TABLE #PA
						RETURN
					END
				END
			END
		END



		--���� ������ � ���������� ����������
		IF @RegularCode = @AbsentCode
		BEGIN
			--���� �� ���������� ��� ����������� ������ �� ��
			IF NOT EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @RegUserId and DateBegin = @AbsentBeginDate and IsUsed = 1)
			BEGIN
				--������������ ������ �� �� 
				INSERT INTO StaffTemporaryReleaseVacancyRequest(Version, UserLinkId, ReplacedId, DateBegin, DateEnd, AbsencesTypeId, IsUsed, Note)
				VALUES(1, @UserLinkId, @RegUserId, @AbsentBeginDate, null, 3, 1, N'�������������� ��������� ������: � �������������� ������ ����������� ���� ������� ���������� ����������.')
			END
		END


		--���� ����� ������������ ���������� � �����������, ���� �� �����
		IF NOT EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @UserId)
		BEGIN
			--���� ��������� � �����������, ���� �� ������ ������������ ����������
			IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE ReplacedId = @UserId and IsUsed = 1)
			BEGIN
				PRINT N'�13 ������������ ���������� ��� � ����������� � ��� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ��������� ' + @RegUserName + N' (' + @RegularCode + N')'
				ROLLBACK TRANSACTION
				DROP TABLE #PA
				RETURN
			END
				--���� ����, �� ���� ���� ������ ()
		END
		ELSE--���� ����������� ��������� ���� � �����������
		BEGIN
			--����� ��������� �������� �� �� ���������
			IF NOT EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId AND ReplacedId = @RegUserId and IsUsed = 1)
			BEGIN
				--���� �� ��������, � �������� ��������� ��� ��� � ����������� �� ������� ����� ��� �������� ��� ��������� �� ������ �����, �� ������� ������ ������ 
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId = @SEPId) or
						 EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE UserId = @RegUserId and SEPId <> @SEPId)
				BEGIN
				--������� �� ������ ������������ ����������
					INSERT INTO StaffPostReplacement (UserLinkId, UserId, ReplacedId, IsUsed, ReasonId)
					SELECT Id, UserId, @RegUserId, 1, 3 FROM StaffEstablishedPostUserLinks WHERE Id = @UserLinkId
					
					--����� � ����������� ������������ ���������� ���������� �� ������� ��������� ����������
					IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE SEPId = @SEPId and UserId = @RegUserId)
					BEGIN
						--����� ��������� ������� ���������
						UPDATE StaffEstablishedPostUserLinks SET UserId = null WHERE SEPId = @SEPId and UserId = @RegUserId

						--���� ����� � ��������� �� ��������� ���������� ���������� ������ �� ��, �� ��������� �� �� ��� �����
							UPDATE StaffTemporaryReleaseVacancyRequest SET UserLinkId = @UserLinkId WHERE ReplacedId = @RegUserId and CreatorId is null
					END
					ELSE
					BEGIN
						--�������� � ����������� ��������� �������� ����� ���� � ������ ������� ��������, �� ����� ����� ������� ��������� ���������� ������� ��������� ������ ��������
						--���� ��� ������� � ������� �� Id ��������� ���������� � ���������� ����� �� ���� �������
						UPDATE StaffEstablishedPostUserLinks SET UserId = null 
						FROM StaffEstablishedPostUserLinks as A
						WHERE A.UserId = @RegUserId and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = A.Id)
					END
				END
			END
			ELSE	--��������� ������� ������ ����������� ����������� ����������� ����������, �� ���������
			BEGIN
				IF EXISTS (SELECT * FROM StaffPostReplacement WHERE UserId = @UserId and ReplacedId <> @RegUserId and IsUsed = 1)
				BEGIN
					PRINT N'�14 ���������� ������ ����������� ����������� ' + @UserName + N' (' + @UserCode + N') ������������ ����������!'
					ROLLBACK TRANSACTION
					DROP TABLE #PA
					RETURN
				END
			END
		END
	END


	--����� ������, ����� �� �� ��������� � ���������� ���������	
	UPDATE #PA SET IsComplete = 1 WHERE Id = @Id
	SELECT @CountRow = COUNT(*) FROM #PA WHERE IsComplete = 1
	PRINT N'���������� ' + cast(@CountRow as nvarchar) + N' �������'
END


PRINT N'������ ������� ����������!'
COMMIT TRANSACTION
--ROLLBACK TRANSACTION


DROP TABLE #PA