IF OBJECT_ID ('vwEmploymentHighEducations', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentHighEducations]
GO

--состояние заполнения анкет
CREATE VIEW [dbo].[vwEmploymentHighEducations]
AS
SELECT A.*, B.Name as EducationTypeName
FROM HigherEducationDiploma as A
LEFT JOIN EmploymentEducationType as B ON B.Id = A.EducationTypeId

GO