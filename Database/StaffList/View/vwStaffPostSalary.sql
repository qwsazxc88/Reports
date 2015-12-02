IF OBJECT_ID ('vwStaffPostSalary', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffPostSalary]
GO

--представление дает раскладку для сотрудника по его зарплате
CREATE VIEW [dbo].[vwStaffPostSalary]
AS
SELECT UserId
			,sum(Salary) as Salary
			,sum(Regional) as Regional
			,sum(Personnel) as Personnel
			,sum(Territory) as Territory
			,sum(Front) as Front
			,sum(Drive) as Drive
			,sum(NorthAuto) as NorthAuto
			,sum(North) as North
			,sum(Qualification) as Qualification
			,(sum(Salary) + sum(Personnel) + sum(Territory) + sum(Front) + sum(Drive) + sum(Qualification)) +
			 ((sum(Salary) + sum(Personnel) + sum(Territory) + sum(Front) + sum(Drive) + sum(Qualification)) * (sum(Regional) / 100)) +
			 ((sum(Salary) + sum(Personnel) + sum(Territory) + sum(Front) + sum(Drive) + sum(Qualification)) * (case when sum(NorthAuto) = 0 then sum(North) else sum(NorthAuto) end / 100))
			as TotalSalary
FROM (--персональные надбваки
			SELECT UserId, 0 as Salary, 0 as Regional
						,case when StaffExtraChargeId = 4 then Salary else 0 end as Personnel	--персональная надбавка
						,case when StaffExtraChargeId = 5 then Salary else 0 end as Territory	--территориальная надбавка
						,case when StaffExtraChargeId = 10 then Salary else 0 end as Front	--фронт надбавка
						,case when StaffExtraChargeId = 3 then Salary else 0 end as Drive	--разъездная надбавка
						,case when StaffExtraChargeId = 7 then Salary else 0 end as NorthAuto	--северная автомат надбавка
						,case when StaffExtraChargeId = 16 then Salary else 0 end as North	--северная ручная надбавка
						,case when StaffExtraChargeId = 2 then Salary else 0 end as Qualification	--квалификация надбавка
			FROM StaffPostChargeLinks
			WHERE IsActive = 1
			UNION ALL
			--оклад и должностные надбавки
			SELECT C.UserId, A.Salary, isnull(B.Amount, 0) as Regional, 0 as Personnel, 0 as Territory, 0 as Front, 0 as Drive, 0 as NorthAuto, 0 as North, 0 as Qualification
			FROM StaffEstablishedPost as A
			LEFT JOIN StaffEstablishedPostChargeLinks as B ON B.SEPId = A.Id
			INNER JOIN StaffEstablishedPostUserLinks as C ON C.SEPId = A.Id and C.IsUsed = 1
			INNER JOIN Users as D ON D.Id = C.UserId and D.IsActive = 1
			WHERE A.IsUsed = 1) as A
GROUP BY UserId

--select * from StaffEstablishedPostUserLinks
GO