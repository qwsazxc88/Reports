IF OBJECT_ID ('vwEmploymentFillState', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentFillState]
GO

--��������� ���������� �����
CREATE VIEW [dbo].[vwEmploymentFillState]
AS
SELECT -1 as Id, cast(0 as bit) as GeneralFinal, cast(0 as bit) as PassportFinal, cast(0 as bit) as EducationFinal, cast(0 as bit) as FamilyFinal, cast(0 as bit) as MilitaryFinal, cast(0 as bit) as ExperienceFinal,
			 cast(0 as bit) as ContactFinal, cast(0 as bit) as BackgroundFinal, 
			 --������������
			 cast(0 as bit) as BackgroundApproval, cast(0 as bit) as TrainingApproval, cast(0 as bit) as ManagerApproval, cast(0 as bit) as PersonnelManagerApproval
UNION ALL
SELECT A.Id, B.IsFinal as GeneralFinal, C.IsFinal as PassportFinal, D.IsFinal as EducationFinal, E.IsFinal as FamilyFinal, F.IsFinal as MilitaryFinal, G.IsFinal as ExperienceFinal,
			 H.IsFinal as ContactFinal, I.IsFinal as BackgroundFinal, 
			 --������������
			 I.ApprovalStatus as BackgroundApproval, J.IsComplete as TrainingApproval, K.HigherManagerApprovalStatus as ManagerApproval, 
			 cast(case when A.Status = 7 then 1 else 0 end as bit) as PersonnelManagerApproval
FROM EmploymentCandidate as A
INNER JOIN GeneralInfo as B ON B.Id = A.GeneralInfoId
INNER JOIN Passport as C ON C.Id = A.PassportId
INNER JOIN Education as D ON D.Id = A.EducationId
INNER JOIN Family as E ON E.Id = A.FamilyId
INNER JOIN MilitaryService as F ON F.Id = A.MilitaryServiceId
INNER JOIN Experience as G ON G.Id = A.ExperienceId
INNER JOIN Contacts as H ON H.Id = A.ContactsId
INNER JOIN BackgroundCheck as I ON I.Id = A.BackgroundCheckId
INNER JOIN OnsiteTraining as J ON J.Id = A.OnsiteTrainingId
INNER JOIN Managers as K ON K.Id = A.ManagersId

GO