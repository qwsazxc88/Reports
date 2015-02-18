IF OBJECT_ID ('vwEmploymentFillState', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentFillState]
GO

--состояние заполнения анкет
CREATE VIEW [dbo].[vwEmploymentFillState]
AS
SELECT A.Id, B.IsFinal as GeneralFinal, C.IsFinal as PassportFinal, D.IsFinal as EducationFinal, E.IsFinal as FamilyFinal, F.IsFinal as MilitaryFinal, G.IsFinal as ExperienceFinal,
			 H.IsFinal as ContactFinal, I.IsFinal as BackgroundFinal 
FROM EmploymentCandidate as A
INNER JOIN GeneralInfo as B ON B.Id = A.GeneralInfoId
INNER JOIN Passport as C ON C.Id = A.PassportId
INNER JOIN Education as D ON D.Id = A.EducationId
INNER JOIN Family as E ON E.Id = A.FamilyId
INNER JOIN MilitaryService as F ON F.Id = A.MilitaryServiceId
INNER JOIN Experience as G ON G.Id = A.ExperienceId
INNER JOIN Contacts as H ON H.Id = A.ContactsId
INNER JOIN BackgroundCheck as I ON I.Id = A.BackgroundCheckId

GO