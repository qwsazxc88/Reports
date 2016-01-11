IF OBJECT_ID ('fnGetStaffEstablishedArrangements', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
GO

--функция достает штатную расстановку по выбранному подразделению + текущее состояние надбавок 
CREATE FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
(
	@DepartmentId int	
	,@PersonnelId int
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
	,IsTemporary bit
	,DateTempBegin datetime
	,DateTempEnd datetime
)
AS
BEGIN
DECLARE 
	@IsSalaryEnable bit

	SET @IsSalaryEnable = 0

	IF @PersonnelId = 0
		SET @IsSalaryEnable = 1
	ELSE
	BEGIN
		IF EXISTS (SELECT * FROM vwDepartmentToPersonnels WHERE PersonnelId = @PersonnelId and DepartmentId = @DepartmentId)
			SET @IsSalaryEnable = 1
	END


	INSERT INTO @ReturnTable
	SELECT F.Id, A.Id as SEPId, A.PositionId, B.Name as PositionName, A.DepartmentId, 1 as Quantity
				 ,case when @IsSalaryEnable = 1 then A.Salary else 0 end as Salary
				 ,C.Path, D.Id as RequestId, 
				 E.Rate,	--ставка
				 --если в отпуске о уходу за ребенокм и нет замены показываем в колонках для заменяемых
				 case when E.IsPregnant = 1 then null else E.Id end as UserId, 
				 --case when E.IsPregnant = 1 then null else E.Name end as Surname, 
				 case when (case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 then 1 else 0 end) = 1 or (case when isnull(F.DocId, 0) = 0 then 0 else 1 end) = 1
							then (case when (case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end) = 1 
												 then 'Временная вакансия' else 'Вакансия' end) 
							else E.Name end as Surname, 
												 
				 case when isnull(E.IsPregnant, 0) = 1 then E.Id else G.ReplacedId end as ReplacedId
				 ,case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, E.Id), E.Name)  else isnull(dbo.fnGetReplacedName(F.Id, null), H.Name) end as ReplacedName
				 ,F.ReserveType
				 ,case when F.ReserveType = 1 then N'Перемещение' 
							 when F.ReserveType = 2 then N'Прием'
							 when F.ReserveType = 3 then N'Сокращение' end as Reserve
				 ,F.DocId
				 ,cast(case when isnull(F.DocId, 0) = 0 then 0 else 1 end as bit) as IsReserve
				 ,isnull(E.IsPregnant, 0) as IsPregnant
				 ,case when (isnull(E.IsPregnant, 0) = 1 or F.UserId is null) and isnull(F.ReserveType, 0) = 0 then 1 else 0 end as IsVacation
				 --,case when (case when E.IsPregnant = 1 then null else E.Id end) is null and H.Id is not null then 1 else 0 end as IsSTD
				 ,case when F.UserId is null then 0 else (case when isnull(E.IsPregnant, 0) = 1 or H.Id is not null then 1 else 0 end) end as IsSTD
				 ,case when J.UserId is null then 0 else 1 end as IsDismiss	--увольнение
				 ,F.IsDismissal		--сокращение
				 --оклад и надбавки
				 ,case when @IsSalaryEnable = 1 then I.Salary else 0 end as SalaryPersonnel
				 ,case when @IsSalaryEnable = 1 then I.Regional else 0 end as Regional
				 ,case when @IsSalaryEnable = 1 then I.Personnel else 0 end as Personnel
				 ,case when @IsSalaryEnable = 1 then I.Territory else 0 end as Territory
				 ,case when @IsSalaryEnable = 1 then I.Front else 0 end as Front
				 ,case when @IsSalaryEnable = 1 then I.Drive else 0 end as Drive
				 --,case when I.NorthAuto = 0 then I.North else I.NorthAuto end as North
				 ,case when @IsSalaryEnable = 1 then I.North else 0 end as North
				 ,case when @IsSalaryEnable = 1 then I.Qualification else 0 end as Qualification
				 ,case when @IsSalaryEnable = 1 then isnull(I.TotalSalary, A.Salary) else 0 end as TotalSalary	--если вакансия, то надо показать оклад штатной единицы
				 ,F.DateDistribNote
				 ,F.DateReceivNote
				 ,F.IsTemporary
				 ,F.DateTempBegin
				 ,F.DateTempEnd
	FROM StaffEstablishedPost as A
	INNER JOIN Position as B ON B.Id = A.PositionId
	INNER JOIN Department as C ON C.Id = A.DepartmentId
	INNER JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
	INNER JOIN StaffEstablishedPostUserLinks as F ON F.SEPId = A.Id and F.IsUsed = 1
	LEFT JOIN Users as E ON E.Id = F.UserId and E.IsActive = 1 and (E.RoleId & 2 > 0 or E.RoleId & 16384 > 0) --and E.IsPregnant = 0
	LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = F.Id and F.IsUsed = 1
	LEFT JOIN Users as H ON H.Id = G.ReplacedId
	LEFT JOIN vwStaffPostSalary as I ON I.UserId = E.Id
	LEFT JOIN (SELECT UserId FROM Dismissal 
						 WHERE UserDateAccept is not null and DeleteDate is null
						 GROUP BY UserId) as J ON J.UserId = E.Id
	WHERE A.DepartmentId = @DepartmentId /*and A.PositionId = 356*/ and A.IsUsed = 1 
				--замещенных убираем из списка этим условием
				--and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and ReplacedId = E.Id)
	ORDER BY A.Priority

		
--select * from dbo.fnGetStaffEstablishedArrangements(23230, 0) 

	RETURN 
END

GO


