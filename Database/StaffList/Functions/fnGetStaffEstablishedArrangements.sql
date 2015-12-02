IF OBJECT_ID ('fnGetStaffEstablishedArrangements', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
GO

--функция достает штатную расстановку по выбранному подразделению + текущее состояние надбавок 
CREATE FUNCTION [dbo].[fnGetStaffEstablishedArrangements]
(
	@DepartmentId int
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
	,DocId int
	,IsReserve bit	--признак бронирования вакансии
	,IsPregnant bit
	,IsVacation bit	--вакансия
	,IsSTD bit			--вакансия по срочному договору
)
AS
BEGIN
	INSERT INTO @ReturnTable
	SELECT F.Id, A.Id as SEPId, A.PositionId, B.Name as PositionName, A.DepartmentId, 1 as Quantity, A.Salary, C.Path, D.Id as RequestId, 
				 E.Rate,	--ставка
				 --если в отпуске о уходу за ребенокм и нет замены показываем в колонках для заменяемых
				 case when E.IsPregnant = 1 then null else E.Id end as UserId, 
				 case when E.IsPregnant = 1 then null else E.Name end as Surname, 
				 case when E.IsPregnant = 1 then E.Id else G.ReplacedId end as ReplacedId
				 ,case when E.IsPregnant = 1 then isnull(dbo.fnGetReplacedName(null, E.Id), E.Name)  else isnull(dbo.fnGetReplacedName(F.Id, null), H.Name) end as ReplacedName
				 ,F.ReserveType
				 ,F.DocId
				 ,cast(case when F.DocId is null then 0 else 1 end as bit) as IsReserve
				 ,E.IsPregnant
				 ,case when (case when E.IsPregnant = 1 then null else E.Id end) is null or F.UserId is null then 1 else 0 end as IsVacation
				 --,case when (case when E.IsPregnant = 1 then null else E.Id end) is null and H.Id is not null then 1 else 0 end as IsSTD
				 ,case when F.UserId is null then 0 else (case when (case when E.IsPregnant = 1 then null else E.Id end) is null or H.Id is not null then 1 else 0 end) end as IsSTD
	FROM StaffEstablishedPost as A
	INNER JOIN Position as B ON B.Id = A.PositionId
	INNER JOIN Department as C ON C.Id = A.DepartmentId
	INNER JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
	--INNER JOIN Users as E ON E.SEPId = A.Id and E.IsActive = 1 and E.RoleId & 2 > 0
	INNER JOIN StaffEstablishedPostUserLinks as F ON F.SEPId = A.Id and F.IsUsed = 1
	LEFT JOIN Users as E ON E.Id = F.UserId and E.IsActive = 1 and E.RoleId & 2 > 0 --and E.IsPregnant = 0
	LEFT JOIN StaffPostReplacement as G ON G.UserLinkId = F.Id and F.IsUsed = 1
	LEFT JOIN Users as H ON H.Id = G.ReplacedId
	WHERE A.DepartmentId = @DepartmentId /*and A.PositionId = 356*/ and A.IsUsed = 1 
				--замещенных убираем из списка этим условием
				--and not exists (SELECT * FROM StaffPostReplacement WHERE UserLinkId = F.Id and ReplacedId = E.Id)
	ORDER BY A.Priority


	/*
	SELECT UserId
			,sum(case when StaffExtraChargeId = 4 then Salary else 0 end) as Personnel	--персональная надбавка
			,sum(case when StaffExtraChargeId = 5 then Salary else 0 end) as Territory	--территориальная надбавка
			,sum(case when StaffExtraChargeId = 10 then Salary else 0 end) as Front	--фронт надбавка
			,sum(case when StaffExtraChargeId = 3 then Salary else 0 end) as Drive	--разъездная надбавка
			,sum(case when StaffExtraChargeId = 7 then Salary else 0 end) as NorthAuto	--северная автомат надбавка
			,sum(case when StaffExtraChargeId = 16 then Salary else 0 end) as North	--северная ручная надбавка
			,sum(case when StaffExtraChargeId = 2 then Salary else 0 end) as Qualification	--квалификация надбавка
FROM StaffPostChargeLinks
	*/

--select * from dbo.fnGetStaffEstablishedArrangements(7924) 

	RETURN 
END

GO


