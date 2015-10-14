IF OBJECT_ID ('vwEmploymentPositions', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentPositions]
GO



--достаем комментарии к разделам приема
CREATE VIEW [dbo].[vwEmploymentPositions]
AS
SELECT Id, Name + case when IsDeleted = 1 then ' (не использовать)' else '' end as Name
FROM dbo.Position



GO





