IF OBJECT_ID ('vwEmploymentPositions', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentPositions]
GO



--������� ����������� � �������� ������
CREATE VIEW [dbo].[vwEmploymentPositions]
AS
SELECT Id, Name + case when IsDeleted = 1 then ' (�� ������������)' else '' end as Name
FROM dbo.Position



GO





