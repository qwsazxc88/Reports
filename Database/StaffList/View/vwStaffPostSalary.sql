IF OBJECT_ID ('vwStaffPostSalary', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffPostSalary]
GO

--представление дает раскладку для сотрудника по его зарплате
CREATE VIEW [dbo].[vwStaffPostSalary]
AS

SELECT A.SEPId
			,A.UserLinkId
			,A.UserId
			,A.Salary as Salary
			,Regional as Regional
			,Personnel as Personnel
			,Territory as Territory
			,Front as Front
			,Drive as Drive
			,isnull(Amount, 0) as North
			,Qualification as Qualification
			,((A.Salary + Personnel + Territory + Front + Drive + Qualification) * isnull(B.Rate, 0)) +
			 ((A.Salary + Personnel + Territory + Front + Drive + Qualification) * isnull(B.Rate, 0) * (Regional / 100)) +
			 ((A.Salary + Personnel + Territory + Front + Drive + Qualification) * isnull(B.Rate, 0) * (isnull(Amount, 0) / 100))
			as TotalSalary
FROM (/*--персональные надбваки (старый вариант)
			SELECT UserId, 0 as Salary, 0 as Regional
						,case when StaffExtraChargeId = 4 then Salary else 0 end as Personnel	--персональная надбавка
						,case when StaffExtraChargeId = 5 then Salary else 0 end as Territory	--территориальная надбавка
						,case when StaffExtraChargeId = 10 then Salary else 0 end as Front	--фронт надбавка
						,case when StaffExtraChargeId = 3 then Salary else 0 end as Drive	--разъездная надбавка
						--,case when StaffExtraChargeId = 7 then Salary else 0 end as NorthAuto	--северная автомат надбавка
						--,case when StaffExtraChargeId = 16 then Salary else 0 end as North	--северная ручная надбавка
						,case when StaffExtraChargeId = 2 then Salary else 0 end as Qualification	--квалификация надбавка
			FROM StaffPostChargeLinks
			WHERE IsActive = 1
			UNION ALL
			*/
			--оклад и должностные надбавки
			SELECT A.Id as SEPId, C.Id as UserLinkId, C.UserId, A.Salary, isnull(B.Amount, 0) as Regional, isnull(F.Personnel, 0) as Personnel, isnull(F.Territory, 0) as Territory, isnull(F.Front, 0) as Front, isnull(F.Drive, 0) as Drive, isnull(F.Qualification, 0) as Qualification
			FROM StaffEstablishedPost as A
			INNER JOIN StaffEstablishedPostUserLinks as C ON C.SEPId = A.Id and C.IsUsed = 1
			LEFT JOIN Users as D ON D.Id = C.UserId and D.IsActive = 1
			INNER JOIN StaffEstablishedPostRequest as E ON E.SEPId = A.Id and E.IsUsed = 1
			LEFT JOIN StaffEstablishedPostChargeLinks as B ON B.SEPId = A.Id and B.SEPRequestId = E.Id
			--персональные надбавки
			LEFT JOIN (SELECT UserId
												,sum(case when StaffExtraChargeId = 4 then Salary else 0 end) as Personnel	--персональная надбавка
												,sum(case when StaffExtraChargeId = 5 then Salary else 0 end) as Territory	--территориальная надбавка
												,sum(case when StaffExtraChargeId = 10 then Salary else 0 end) as Front	--фронт надбавка
												,sum(case when StaffExtraChargeId = 3 then Salary else 0 end) as Drive	--разъездная надбавка
												,sum(case when StaffExtraChargeId = 2 then Salary else 0 end) as Qualification	--квалификация надбавка
									FROM StaffPostChargeLinks
									WHERE IsActive = 1
									GROUP BY UserId) as F ON F.UserId = C.UserId
			WHERE A.IsUsed = 1) as A
LEFT JOIN Users as B ON B.Id = A.UserId
LEFT JOIN StaffUserNorthAdditional as C ON C.UserId = A.UserId and year(DateCalculate) = year(getdate()) and month(DateCalculate) = month(getdate())

--select * from vwStaffPostSalary where userid = 6118
--select SUM(Salary), SUM(Regional), SUM(Personnel), SUM(Territory), SUM(Front), SUM(Drive), SUM(North), SUM(Qualification), SUM(TotalSalary) from vwStaffPostSalary
GO