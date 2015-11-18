IF OBJECT_ID ('fnGetStaffEstablishedArrangements', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
GO

--функци€ достает штатную расстановку по выбранному подразделению
CREATE FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
(
	@DepartmentId int
)
RETURNS 
@ReturnTable TABLE 
(
	 Id int 
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
	,IsPregnant bit
	,IsVacation bit	--ваканси€
	,IsSTD bit			--ваканси€ по срочному договору
)
AS
BEGIN
DECLARE 
	@Tmp TABLE (SEPId int, PositionId int, PositionName nvarchar(250), Quantity int, Salary numeric(18, 2), VacationCount int, Path nvarchar(250))
DECLARE @SEPId int, @i int, @VacationCount int
	--
	INSERT INTO @ReturnTable
	SELECT A.Id, A.PositionId, B.Name as PositionName, A.DepartmentId, 1 as Quantity, A.Salary, C.Path, D.Id as RequestId, 
				 E.Rate,	--ставка
				 --если в отпуске о уходу за ребенокм и нет замены показываем в колонках дл€ замен€емых
				 case when E.IsPregnant = 1 then null else E.Id end as UserId, 
				 case when E.IsPregnant = 1 then null else E.Name end as Surname, 
				 case when E.IsPregnant = 1 then E.Id else F.ReplacedId end as ReplacedId, 
				 case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, null, E.Id), E.Name)  else isnull(dbo.fnGetReplacedName(E.Id, A.Id, null), G.Name) end as ReplacedName, E.IsPregnant
				 ,case when (case when E.IsPregnant = 1 then null else E.Id end) is null then 1 else 0 end as IsVacation
				 ,case when (case when E.IsPregnant = 1 then null else E.Id end) is null or G.Id is not null then 1 else 0 end as IsSTD
	FROM StaffEstablishedPost as A
	INNER JOIN Position as B ON B.Id = A.PositionId
	INNER JOIN Department as C ON C.Id = A.DepartmentId
	INNER JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
	INNER JOIN Users as E ON E.SEPId = A.Id and E.IsActive = 1 and E.RoleId & 2 > 0
	LEFT JOIN StaffPostReplacement as F ON F.SEPId = A.Id and F.UserId = E.Id and F.IsUsed = 1
	LEFT JOIN Users as G ON G.Id = F.ReplacedId
	WHERE A.DepartmentId = @DepartmentId /*and A.PositionId = 356*/ and A.IsUsed = 1 
				--замещенных убираем из списка этим условием
				and not exists (SELECT * FROM StaffPostReplacement WHERE SEPId = A.Id and ReplacedId = E.Id)
	ORDER BY A.Priority
	
	--определ€ем вакансии
	INSERT INTO @Tmp(SEPId, PositionId, PositionName, Quantity, Salary, VacationCount, Path)
	SELECT A.Id, A.PositionId, B.PositionName, A.Quantity, B.Salary, case when A.Quantity > count(B.PositionId) then A.Quantity - count(B.PositionId) else 0 end as VacationCount, B.Path
	FROM StaffEstablishedPost as a
	INNER JOIN @ReturnTable as B ON B.Id = A.Id and B.PositionId = A.PositionId
	WHERE A.DepartmentId = @DepartmentId	
	GROUP BY A.Id, A.PositionId, B.PositionName, A.Quantity, B.Salary, B.Path

	DELETE FROM @Tmp WHERE VacationCount = 0

	--добавл€ем вакансии
	IF EXISTS (SELECT * FROM @Tmp)
	BEGIN
		SELECT top 1 @SEPId = SEPId, @VacationCount = VacationCount FROM @Tmp
		SET @i = 0

		WHILE @i < @VacationCount
		BEGIN
			INSERT INTO @ReturnTable (Id, PositionId, PositionName, DepartmentId, Quantity, Salary, Path, RequestId, Rate, UserId, Surname, ReplacedId, ReplacedName,	IsPregnant, IsVacation, IsSTD)
			SELECT top 1 SEPId, PositionId, PositionName, @DepartmentId, 1, Salary, Path, null, null, null, N'¬аканси€', null, null, null, 1, 0 FROM @Tmp WHERE SEPId = @SEPId

			SET @i += 1
		END

		DELETE FROM @Tmp WHERE SEPId = @SEPId
	END
	

--select * from dbo.fnGetStaffEstablishedArrangements(7924) 

	RETURN 
END

GO


