IF OBJECT_ID ('vwStaffPregnantUsers', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwStaffPregnantUsers]
GO

--представление показывает сотрудников в ќ∆ относительно текущего момента
CREATE VIEW [dbo].[vwStaffPregnantUsers]
AS
	SELECT * FROM (SELECT UserId FROM ChildVacation WHERE SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate 
								 UNION ALL
								 SELECT UserId FROM Sicklist WHERE TypeId = 12 and SendTo1C is not null and DeleteDate is null and getdate() between BeginDate and EndDate ) as A
	GROUP BY UserId

	--select * from vwStaffPregnantUsers where userid = 18006
GO