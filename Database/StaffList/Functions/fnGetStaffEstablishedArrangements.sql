IF OBJECT_ID ('fnGetStaffEstablishedArrangements', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
GO

--функция достает штатную расстановку по выбранному подразделению + текущее состояние надбавок 
CREATE FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
(
	@DepartmentId int	--подразделение	
	,@PersonnelId int	--кадровик РК
	,@ManagerId int	--руководитель
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
	,SEPId int
	,PositionId int
	,PositionName nvarchar(250)
	,DepartmentId int
	,Quantity int
	,Salary numeric(18, 2)
	,Path nvarchar(250)
	,RequestId int
	,Rate decimal(18, 2)
	,UserId int
	,Surname nvarchar(250)
	,ReplacedId int
	,ReplacedName nvarchar(500)
	,ReserveType int
	,Reserve nvarchar(50)
	,DocId int
	,IsReserve bit	--признак бронирования вакансии
	,IsPregnant bit
	,IsVacation bit	--вакансия
	,IsSTD bit			--вакансия по срочному договору
	,IsDismiss bit
	,IsDismissal bit
	--оклад и надбавки
	,SalaryPersonnel numeric(18, 2)	--оклад (из представления)
	,Regional numeric(18, 2)
	,Personnel numeric(18, 2)
	,Territory numeric(18, 2)
	,Front numeric(18, 2)
	,Drive numeric(18, 2)
	,North numeric(18, 2)
	,Qualification numeric(18, 2)
	,TotalSalary numeric(18, 2)
	,DateDistribNote datetime
	,DateReceivNote datetime
	,BasicUser nvarchar(250)
	,TemporaryMovementUsers	nvarchar(500)	--временно переведенные
	,LongAbsencesUsers nvarchar(500)--сотрудники в длительном отсутствии
	,PositionRank int
	,PositionLevel int
)
AS
BEGIN
DECLARE 
	@IsSalaryEnable bit
	,@IsMainManager bit
	,@Rank int	--ранг должности руководителя
	,@Login nvarchar(50)
	,@UserId int		--учетка руководителя-сотрудника
	,@Itemlevel int

	SET @IsSalaryEnable = 0

	IF @PersonnelId = 0
		SET @IsSalaryEnable = 1
	ELSE
	BEGIN
		IF EXISTS (SELECT * FROM vwDepartmentToPersonnels WHERE PersonnelId = @PersonnelId and DepartmentId = @DepartmentId)
			SET @IsSalaryEnable = 1
	END


	IF @ManagerId <> 0
	BEGIN
		--определяем руководителя/зама
		SELECT @IsMainManager = A.IsMainManager
					,@Rank = B.[Rank], @Itemlevel = B.Itemlevel, @Login = A.[Login]
		FROM Users as A
		INNER JOIN Position as B ON B.Id = A.PositionId
		INNER JOIN Department as C ON C.Id = A.DepartmentId
		WHERE A.Id = @ManagerId

		SELECT @UserId = Id FROM Users WHERE RoleId & 2 > 0 and [Login] = substring(@Login, 1, LEN(@Login) - 1)
	END


--select * from dbo.fnGetStaffEstablishedArrangements(126, 0, 14406) 
	INSERT INTO @ReturnTable
	SELECT F.Id, A.Id as SEPId, A.PositionId, B.Name as PositionName, A.DepartmentId, 1 as Quantity
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then A.Salary --руководитель видит все оклады
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then A.Salary --на одном уровне зам видит только свой оклад
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then A.Salary --зам видит подчиненных
																																			else 0 end)		--замы видят свои оклады и оклады подчиненых, не видят оклад начальника и других замов
							 when @IsSalaryEnable = 1 then A.Salary	--для кадровиков
							 else 0 end as Salary
				 ,C.Path, D.Id as RequestId, 
				 E.Rate,	--ставка
				 --если в ОЖ, КП или ДО и нет замены показываем в колонках для заменяемых
				 --старый вариант
				 /*
				 case when E.IsPregnant = 1 then null else E.Id end as UserId, 
				 --case when E.IsPregnant = 1 then null else E.Name end as Surname, 
				 case when (case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) then 1 else 0 end) = 1 
										or exists(SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1)
										or exists(SELECT * FROM StaffMovements WHERE UserId = F.UserId and IsTempMoving = 1 and Type in (2, 3) and Status = 12 and GETDATE() between MovementDate and MovementTempTo) 
							then (case when (case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
															or exists(SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1)
															or exists(SELECT * FROM StaffMovements WHERE UserId = F.UserId and IsTempMoving = 1 and Type in (2, 3) and Status = 12 and GETDATE() between MovementDate and MovementTempTo) 
												 then 'Временная вакансия' else 'Вакансия' end) 
							else E.Name end as Surname, */
				 case when ((E.IsPregnant = 1 or L.UserId is not null) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если текущего перевели и на это место никто не приходил
									 (exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId <> F.Id and UserId = F.UserId and IsUsed = 1) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если в ДО и место никто не занимал (в заявках на ДО обращаем внимание на заполнение поля CreatorId, автоматическое формирование данных при обработке не проставляет это поле)
									 (exists (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1 and CreatorId is not null) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1))
							then null else E.Id end as UserId, 

				 case when F.UserId is null then N'Вакансия'
							when ((E.IsPregnant = 1 or L.UserId is not null) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если текущего перевели и на это место никто не приходил
									 (exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId <> F.Id and UserId = F.UserId and IsUsed = 1) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если в ДО и место никто не занимал (в заявках на ДО обращаем внимание на заполнение поля CreatorId, автоматическое формирование данных при обработке не проставляет это поле)
									 (exists (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1 and CreatorId is not null) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1))
							then N'Временная вакансия'
							else E.Name end as Surname,
												 
				 case when isnull(E.IsPregnant, 0) = 1 or L.UserId is not null then E.Id else G.ReplacedId end as ReplacedId
				 --,case when E.IsPregnant = 1 or L.UserId is not null then isnull(dbo.fnGetReplacedName(null, E.Id, 1), N'(' + E.Name + N')')  else isnull(dbo.fnGetReplacedName(F.Id, null, 1), N'(' + H.Name + N')') end as ReplacedName
				 ,dbo.fnGetReplacedName(F.Id, null, 1) as ReplacedName
				 ,F.ReserveType
				 ,case when F.ReserveType = 1 then N'Перемещение' 
							 when F.ReserveType = 2 then N'Прием'
							 when F.ReserveType = 3 then N'Сокращение' end as Reserve
				 ,F.DocId
				 ,cast(case when isnull(F.ReserveType, 0) = 0 then 0 else 1 end as bit) as IsReserve
				 ,isnull(E.IsPregnant, 0) as IsPregnant
				 --,case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 or exists(SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1) then 1 else 0 end as IsVacation
				 --,case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null or exists(SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1) then 1 else 0 end) end as IsSTD
				 ,case when F.UserId is null then 1
							 when ((E.IsPregnant = 1 or L.UserId is not null) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если текущего перевели и на это место никто не приходил
									 (exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId <> F.Id and UserId = F.UserId and IsUsed = 1) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если в ДО и место никто не занимал
									 (exists (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1))
							 then 1 else 0 end as IsVacation
				 ,case when ((E.IsPregnant = 1 or L.UserId is not null) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если текущего перевели и на это место никто не приходил
									 (exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId <> F.Id and UserId = F.UserId and IsUsed = 1) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1)) or
									 --если в ДО и место никто не занимал
									 (exists (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE UserLinkId = F.Id and ReplacedId = F.UserId and IsUsed = 1) and not exists(SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and UserId <> F.UserId and IsUsed = 1))
							then 1 else 0 end as IsSTD
				 ,case when J.UserId is null then 0 else 1 end as IsDismiss	--увольнение
				 ,F.IsDismissal		--сокращение
				 --оклад и надбавки
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Salary --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Salary --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Salary --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Salary else 0 end as SalaryPersonnel
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Regional --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Regional --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Regional --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Regional else 0 end as Regional
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Personnel --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Personnel --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Personnel --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Personnel else 0 end as Personnel
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Territory --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Territory --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Territory --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Territory else 0 end as Territory
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Front --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Front --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Front --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Front else 0 end as Front
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Drive --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Drive --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Drive --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Drive else 0 end as Drive
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.North --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.North --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.North --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.North else 0 end as North
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then I.Qualification --руководитель видит надбавки всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then I.Qualification --на одном уровне зам видит только свои надбавки
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then I.Qualification --зам видит надбавки подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then I.Qualification else 0 end as Qualification
				 ,case when @ManagerId <> 0 and @IsMainManager = 1 then isnull(I.TotalSalary, A.Salary) --руководитель видит зп всех
							 when @ManagerId <> 0 and @IsMainManager = 0 then (case when @Itemlevel = isnull(B.Itemlevel, 7) and @Rank = isnull(B.[Rank], 3) and isnull(F.UserId, 0) = @UserId then isnull(I.TotalSalary, A.Salary) --на одном уровне зам видит только свою зп
																																			when @Itemlevel < isnull(B.Itemlevel, 7) then isnull(I.TotalSalary, A.Salary) --зам видит зп подчиненных
																																			else 0 end)		
							 when @IsSalaryEnable = 1 then isnull(I.TotalSalary, A.Salary) else 0 end as TotalSalary	--если вакансия, то надо показать оклад штатной единицы
				 ,F.DateDistribNote
				 ,F.DateReceivNote
				 ,K.Name as BasicUser
				 ,dbo.fnGetReplacedName(F.Id, null, 2) TemporaryMovementUsers
				 ,dbo.fnGetReplacedName(F.Id, null, 3) as LongAbsencesUsers
				 ,B.[Rank], B.ItemLevel
	FROM StaffEstablishedPost as A
	INNER JOIN Position as B ON B.Id = A.PositionId
	INNER JOIN Department as C ON C.Id = A.DepartmentId
	INNER JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
	INNER JOIN StaffEstablishedPostUserLinks as F ON F.SEPId = A.Id and F.IsUsed = 1
	LEFT JOIN Users as E ON E.Id = F.UserId and E.IsActive = 1 and (E.RoleId & 2 > 0 or E.RoleId & 16384 > 0) --and E.IsPregnant = 0
	--LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = F.Id and G.IsUsed = 1
	LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = F.Id and G.IsUsed = 1 and G.ReplacedId = F.UserId
	LEFT JOIN Users as H ON H.Id = G.ReplacedId
	LEFT JOIN vwStaffPostSalary as I ON I.UserLinkId = F.Id
	LEFT JOIN (SELECT UserId FROM Dismissal 
						 WHERE UserDateAccept is not null and DeleteDate is null
						 GROUP BY UserId) as J ON J.UserId = E.Id
	LEFT JOIN Users as K ON K.RegularUserLinkId = F.Id
	LEFT JOIN vwStaffPregnantUsers as L ON L.UserId = F.UserId
	WHERE A.DepartmentId = @DepartmentId /*and A.PositionId = 356*/ and A.IsUsed = 1 
				--замещенных убираем из списка этим условием
				--and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and ReplacedId = E.Id)
	ORDER BY A.Priority

		
--select * from dbo.fnGetStaffEstablishedArrangements(1761, 0, 0) 

	RETURN 
END

GO


