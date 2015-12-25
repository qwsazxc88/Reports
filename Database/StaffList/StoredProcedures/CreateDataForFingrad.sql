IF OBJECT_ID ('CreateDataForFingrad', 'P') IS NOT NULL
	DROP PROCEDURE dbo.CreateDataForFingrad
GO

--процедура формирует данные для финграда
CREATE PROCEDURE dbo.CreateDataForFingrad
	@Switch int	--переключатель 1 - данные для финграда, 2 - справочник дирекций, 3 - справочник управлений, 4 - справочник бизнес-групп, 5 - справочник РП-привязок
AS
BEGIN
SET NOCOUNT ON
DECLARE 
	@Id int
	,@OperGroupId int
	,@SoftGroupId int
	,@DMDetailId int
	,@IsWorkDay bit
	,@Name nvarchar(250)

IF @Switch = 1
BEGIN
	--список руководителей (исключил беременных)
	SELECT * INTO #Managers
	FROM Users as A WHERE (A.RoleId & 4 > 0) and A.IsMainManager = 1 and A.IsActive = 1
	--отключаем беременных и отпуска по уходу за ребенком
	and not exists (SELECT * FROM ChildVacation WHERE getdate() between BeginDate and EndDate and UserId in (SELECT id FROM Users WHERE RoleId = 2 and IsActive = 1 and Email = A.Email)) 
	and not exists (SELECT * FROM Sicklist WHERE getdate() between BeginDate and EndDate and UserId in (SELECT id FROM Users WHERE RoleId = 2 and IsActive = 1 and Email = A.Email)
																								and TypeId in (12, 28, 29, 30, 31, 32)) 

	--режимы работ (пока только дни работы точек, остальные режимы из текстовых полей заявки)
	CREATE TABLE #WorkDays (DMDetailId int, WorkDays nvarchar(8))

	SELECT A.* 
	INTO #WD
	FROM StaffDepartmentOperationModes as A
	WHERE A.ModeType = 1 --and A.DMDetailId = 1
	ORDER BY A.DMDetailId, A.ModeType, A.WeekDay


	WHILE EXISTS (SELECT * FROM #WD)
	BEGIN
		SELECT top 1 @Id = Id, @DMDetailId = DMDetailId, @IsWorkDay = IsWorkDay FROM #WD ORDER BY DMDetailId, ModeType, WeekDay

		IF NOT EXISTS (SELECT * FROM #WorkDays WHERE DMDetailId = @DMDetailId)
			INSERT INTO #WorkDays(DMDetailId, WorkDays) VALUES(@DMDetailId, cast(@IsWorkDay as nvarchar))
		ELSE
			UPDATE #WorkDays SET WorkDays = isnull(WorkDays, '') + cast(@IsWorkDay as nvarchar) WHERE DMDetailId = @DMDetailId

		DELETE FROm #WD WHERE Id = @Id
	END



	--операции
	CREATE TABLE #Operations (OperGroupId int, Operation nvarchar(max))

	SELECT B.Id, B.OperGroupId, C.Name
	INTO #Oper
	FROM StaffDepartmentOperationGroups as A
	INNER JOIN StaffDepartmentOperationLinks as B ON B.OperGroupId = A.Id and B.IsUsed = 1
	INNER JOIN StaffDepartmentOperations as C ON C.Id = B.OperationId
	--where a.id = 49
	ORDER BY A.Id

	WHILE EXISTS (SELECT * FROM #Oper)
	BEGIN
		SELECT top 1 @Id = Id, @OperGroupId = OperGroupId, @Name = Name FROM #Oper

		IF NOT EXISTS (SELECT * FROM #Operations WHERE OperGroupId = @OperGroupId)
			INSERT INTO #Operations(OperGroupId, Operation) VALUES(@OperGroupId, @Name)
		ELSE
			UPDATE #Operations SET Operation = isnull(Operation, '') + case when len(isnull(Operation, '')) != 0 then N'; ' else '' end + @Name WHERE OperGroupId = @OperGroupId

		DELETE FROm #Oper WHERE Id = @Id
	END


	--установленное ПО
	CREATE TABLE #Soft (SoftGroupId int, Soft nvarchar(max))

	SELECT B.Id, B.SoftGroupId, C.Name
	INTO #SoftLink
	FROM StaffDepartmentSoftGroup as A
	INNER JOIN StaffDepartmentSoftGroupLinks as B ON B.SoftGroupId = A.Id and B.IsUsed = 1
	INNER JOIN StaffDepartmentInstallSoft as C ON C.Id = B.SoftId


	WHILE EXISTS (SELECT * FROM #SoftLink)
	BEGIN
		SELECT top 1 @Id = Id, @SoftGroupId = SoftGroupId, @Name = Name FROM #SoftLink

		IF NOT EXISTS (SELECT * FROM #Soft WHERE SoftGroupId = @SoftGroupId)
			INSERT INTO #Soft(SoftGroupId, Soft) VALUES(@SoftGroupId, @Name)
		ELSE
			UPDATE #Soft SET Soft = isnull(Soft, '') + case when len(isnull(Soft, '')) != 0 then N'; ' else '' end + @Name WHERE SoftGroupId = @SoftGroupId

		DELETE FROm #SoftLink WHERE Id = @Id
	END



	SELECT --b.id, a.id, 
				 D.DepCode, E.Name as ReasonName, D.PrevDepCode, D.NameShort, F.PostIndex, isnull(F.CityName, F.SettlementName) as CityName, F.RegionName, F.StreetName, D.OpenDate, D.CloseDate, G.Name as DepType, H.SVCreditCode, H.RBSCode
				 ,I.RPName, I.ManagementName, I.BGName, I.AdminName, I.RBGName
				 ,B.OrderNumber, I.RRPName, C.ATMCashInStarted, C.CashInStartedDate, J.Name as CashInDep, C.ATMCountTotal, C.ATMCashInCount, C.ATMCount, C.ATMStartedDate, K.Name as ATMDep
				 ,case when isnull(D.IsBlocked, 0) = 1 then 'Заблокирован' else 'Действует' end as Blocked
				 ,L.Name as NetShopIdent, D.Phone
				 --режимы работы + дни работы точки (ПЕРЕДЕЛАТЬ): так как нужен срочно образец, то беру данные закачанные, а не из новой структуры
				 ,D.OperationMode, D.OperationModeCash
				 --ориентиры
				 ,M.LandMark1, M.LandMark2, M.LandMark3, M.LandMark4, M.LandMark5
				 ,N.Name as CashDeskAvailable
				 ,O.Operation--операции 
				 ,case when D.IsLegalEntity = 1 then 'да' else 'нет' end as LegalEntity
				 ,S.WorkDays--дни работы точки
				 ,D.BeginIdleDate, D.EndIdleDate, P.Name as SKB_GE, Q.Name as RentName, D.AmountPayment, D.DivisionArea
				 ,R.Soft--установленное ПО
				 ,I.SNILS_RBG--СНИЛС_РБГ_Бизнес_группа
				 ,I.SNILS_Admin--СНИЛС_Управляющего_Управление_Дирекции
				 ,D.OperationModeATM--Режим_работы_Банкомата
				 ,D.OperationModeCashIn--Режим_работы_Cash_in
				 ,T.FingradCode as DepositPointCode --Код_депозитной_точки (null)
				 ,case B.BFGId when 1 then N'Back' when 2 then N'Front' when 6 then N'BackFront' end as Front_Back1--Front_Back1 (Front/Back/null)
				 ,I.ManagementCode--ID_Дирекции
				 ,I.AdminCode--ID_Управления_Дирекции_Управление_Дирекции
				 ,I.RPCode--РП_привязка_Код_РП_в_финград
				 ,I.BGCode--Бизнес_группа_ID_Бизнес_группа
				 ,null as RBGExchange--Бизнес_группа_ФИО_заменяющее_РБГ_в_т_ч_для_заказа_ДС
				 ,mJ.Name as CashInDepManagement--Инкассирующее_подразделение_кэшина_Дирекция
				 ,mK.Name as ATMDepManagement--Инкассирующее_подразделение_банкомата_Дирекция
				 ,K.Code as ATMDepCode--Инкассирующее_подразделение_банкомата_Код_РП_в_финград
				 ,J.Code as CashInDepCode--Инкассирующее_подразделение_кэшина_Код_РП_в_финград
	FROM Department as A
	INNER JOIN StaffDepartmentRequest as B ON B.DepartmentId = A.Id and B.IsUsed = 1
	INNER JOIN StaffDepartmentCBDetails as C ON C.DepRequestId = B.Id
	INNER JOIN StaffDepartmentManagerDetails as D ON D.DepRequestId = B.Id
	LEFT JOIN StaffDepartmentReasons as E ON E.Id = D.ReasonId
	LEFT JOIN RefAddresses as F ON F.Id = B.LegalAddressId
	INNER JOIN StaffDepartmentTypes as G ON G.id = D.DepTypeId
	--справочник кодов совместимых программ
	LEFT JOIN (SELECT A.DMDetailId, B.Code as SVCreditCode, C.Code as RBSCode 
						 FROM (SELECT DISTINCT DMDetailId FROM StaffProgramCodes) as A
						 LEFT JOIN StaffProgramCodes as B ON B.DMDetailId = A.DMDetailId and B.ProgramId = 1
						 LEFT JOIN StaffProgramCodes as C ON C.DMDetailId = A.DMDetailId and C.ProgramId = 2) as H ON H.DMDetailId = D.Id
	--справочник кодировок подразделений
	INNER JOIN (SELECT distinct A.Id, A.Name, C.Name as RPName, E.Name as BGName, G.Name as AdminName, I.Name as ManagementName, uB.Name as RRPName, uD.Name as RBGName, uDS.Cnilc as SNILS_RBG, uFS.Cnilc as SNILS_Admin
										 ,I.Code as ManagementCode, G.Code as AdminCode, E.Code as BGCode, C.Code as RPCode
							FROM Department as A
							--РП-привязка
							INNER JOIN Department as B ON B.Code1C = A.ParentId
							LEFT JOIN StaffDepartmentRPLink as C ON C.DepartmentId = B.Id
							LEFT JOIN #Managers as uB ON uB.DepartmentId = B.Id and (uB.RoleId & 4 > 0) and uB.IsMainManager = 1 and uB.IsActive = 1

							--Бизнес-группа
							INNER JOIN Department as D ON D.Code1C = B.ParentId
							LEFT JOIN StaffDepartmentBusinessGroup as E ON E.DepartmentId = D.Id
							--руководители бизнесгрупп
							LEFT JOIN #Managers as uD ON uD.DepartmentId = D.Id and (uD.RoleId & 4 > 0) and uD.IsMainManager = 1 and uD.IsActive = 1
							--снилс руководителей бизнесгрупп
							LEFT JOIN Users as uDS ON uDS.Email = uD.Email and (uDS.RoleId & 2 > 0) and uDS.IsActive = 1

							--управление
							INNER JOIN Department as F ON F.Code1C = D.ParentId
							LEFT JOIN StaffDepartmentAdministration as G ON G.DepartmentId = F.Id
							LEFT JOIN #Managers as uF ON uF.DepartmentId = F.Id and (uF.RoleId & 4 > 0) and uF.IsMainManager = 1 and uF.IsActive = 1
							--снилс руководителей бизнесгрупп
							LEFT JOIN Users as uFS ON uFS.Email = uF.Email and (uFS.RoleId & 2 > 0) and uFS.IsActive = 1

							--дирекции
							INNER JOIN Department as H ON H.Code1C = F.ParentId
							LEFT JOIN StaffDepartmentManagement as I ON I.DepartmentId = H.Id) as I ON I.Id = A.Id
	--кэшин инкассирующие подразделения 
	LEFT JOIN StaffDepartmentRPLink as J ON J.DepartmentId = C.DepCachinId
	LEFT JOIN StaffDepartmentBusinessGroup as bJ ON bJ.Id = J.BGId
	LEFT JOIN StaffDepartmentAdministration as aJ ON aJ.Id = bJ.AdminId
	LEFT JOIN StaffDepartmentManagement as mJ ON mJ.Id = aJ.ManagementId

	--банкомат инкассирующие подразделения
	LEFT JOIN StaffDepartmentRPLink as K ON K.DepartmentId = C.DepATMId
	LEFT JOIN StaffDepartmentBusinessGroup as bK ON bK.Id = K.BGId
	LEFT JOIN StaffDepartmentAdministration as aK ON aK.Id = bK.AdminId
	LEFT JOIN StaffDepartmentManagement as mK ON mK.Id = aK.ManagementId

	LEFT JOIN StaffNetShopIdentification as L ON L.id = D.NetShopId
	--оринетиры
	LEFT JOIN (SELECT A.DMDetailId, B.[Description] as LandMark1, C.[Description] as LandMark2, D.[Description] as LandMark3, E.[Description] as LandMark4, F.[Description] as LandMark5
						 FROM (SELECT DISTINCT DMDetailId FROM StaffDepartmentLandmarks) as A
						 LEFT JOIN StaffDepartmentLandmarks as B ON B.DMDetailId = A.DMDetailId and B.LandmarkId = 1
						 LEFT JOIN StaffDepartmentLandmarks as C ON C.DMDetailId = A.DMDetailId and C.LandmarkId = 2
						 LEFT JOIN StaffDepartmentLandmarks as D ON D.DMDetailId = A.DMDetailId and D.LandmarkId = 3
						 LEFT JOIN StaffDepartmentLandmarks as E ON E.DMDetailId = A.DMDetailId and E.LandmarkId = 4
						 LEFT JOIN StaffDepartmentLandmarks as F ON F.DMDetailId = A.DMDetailId and F.LandmarkId = 5) as M ON M.DMDetailId = D.Id
	LEFT JOIN StaffDepartmentCashDeskAvailable as N ON N.Id = D. CDAvailableId
	--операции
	LEFT JOIN #Operations as O ON O.OperGroupId = D.OperGroupId
	LEFT JOIN StaffDepartmentSKB_GE as P ON P.Id = D.SKB_GE_Id
	LEFT JOIN StaffDepartmentRentPlace as Q ON Q.Id = D.RentPlaceId
	--установленное ПО
	LEFT JOIN #Soft as R ON R.SoftGroupId = D.SoftGroupId
	LEFT JOIN #WorkDays as S ON S.DMDetailId = D.Id
	--депозитное подразделение
	LEFT JOIN Department as T ON T.Id = B.DepDepositId
	WHERE A.FingradCode is not null
	--where a.FingradCode = '01-01-13-010'
END
--dbo.CreateDataForFingrad 1

--2 - справочник дирекций
IF @Switch = 2
	SELECT Name, Code FROM StaffDepartmentManagement

--3 - справочник управлений 
IF @Switch = 3
	SELECT A.Name, A.Code, B.Name as Management--, D.*
				 ,(SELECT top 1 Name FROM Users WHERE DepartmentId = A.DepartmentId and IsActive = 1 and RoleId = 4 and IsMainManager = 1 ORDER BY Id) as AdminName
				 ,(SELECT top 1 substring(Cnilc, 1, 11) + N'_' + substring(Cnilc, 13, 2) FROM Users WHERE Email = (SELECT top 1 Email FROM Users WHERE DepartmentId = A.DepartmentId and IsActive = 1 and RoleId = 4 and IsMainManager = 1 ORDER BY Id)
																							 and RoleId & 2 > 0) as AdminSNILS
	FROM StaffDepartmentAdministration as A
	LEFT JOIN StaffDepartmentManagement as B ON B.Id = A.ManagementId
	--LEFT JOIN Department as C ON C.id = A.DepartmentId
	--LEFT JOIN Users as D ON D.DepartmentId = C.Id and D.IsActive = 1 and D.IsMainManager = 1

--4 - справочник бизнес-групп
IF @Switch = 4
	SELECT A.Name, A.Code, B.Name as Administration, C.Name as Management
				 ,(SELECT top 1 Name FROM Users WHERE DepartmentId = A.DepartmentId and IsActive = 1 and RoleId = 4 and IsMainManager = 1 ORDER BY Id) as RBGName
				 --,(SELECT top 1 Cnilc FROM Users WHERE DepartmentId = A.DepartmentId and IsActive = 1 and RoleId = 4 and IsMainManager = 1 ORDER BY Id) as RBGSNILS
				 ,(SELECT top 1 substring(Cnilc, 1, 11) + N'_' + substring(Cnilc, 13, 2) FROM Users WHERE Email = (SELECT top 1 Email FROM Users WHERE DepartmentId = A.DepartmentId and IsActive = 1 and RoleId = 4 and IsMainManager = 1 ORDER BY Id)
																							 and RoleId & 2 > 0) as RBGSNILS
				 ,(SELECT top 1 Name FROM Users WHERE DepartmentId = A.DepartmentId and IsActive = 1 and RoleId = 4 and IsMainManager = 0 ORDER BY Id) as RBGAssistant
	FROM StaffDepartmentBusinessGroup as A
	LEFT JOIN StaffDepartmentAdministration as B ON B.Id = A.AdminId
	LEFT JOIN StaffDepartmentManagement as C ON C.Id = B.ManagementId

--5 - справочник РП-привязок
IF @Switch = 5
	SELECT A.Name, A.Code, B.Name as BusinessGroup, C.Name as Administration, D.Name as Management 
	FROM StaffDepartmentRPLink as A
	LEFT JOIN StaffDepartmentBusinessGroup as B ON B.Id = A.BGId
	LEFT JOIN StaffDepartmentAdministration as C ON C.Id = B.AdminId
	LEFT JOIN StaffDepartmentManagement as D ON D.Id = C.ManagementId


    
END
GO
