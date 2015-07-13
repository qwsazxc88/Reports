IF OBJECT_ID ('vwEmploymentFillState', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentFillState]
GO

--состояние заполнения анкет
CREATE VIEW [dbo].[vwEmploymentFillState]
AS
SELECT -1 as Id, cast(0 as bit) as ScanFinal, cast(0 as bit) as GeneralFinal, cast(0 as bit) as PassportFinal, cast(0 as bit) as EducationFinal, cast(0 as bit) as FamilyFinal, cast(0 as bit) as MilitaryFinal, 
			 cast(0 as bit) as ExperienceFinal, cast(0 as bit) as ContactFinal, cast(0 as bit) as BackgroundFinal, cast(0 as bit) as CandidateApp,
			 --кандидат полностью заполнил анкету
			 cast(0 as bit) as CandidateReady,
			 --согласование
			 cast(0 as bit) as BackgroundApproval, cast(0 as bit) as TrainingApproval, cast(0 as bit) as ManagerApproval, cast(0 as bit) as PrevBackgroundApproval, cast(0 as bit) as PersonnelManagerApproval
UNION ALL
SELECT A.Id, isnull(A.IsScanFinal, 0) as ScanFinal, B.IsFinal as GeneralFinal, C.IsFinal as PassportFinal, D.IsFinal as EducationFinal, E.IsFinal as FamilyFinal, F.IsFinal as MilitaryFinal, G.IsFinal as ExperienceFinal,
			 H.IsFinal as ContactFinal, I.IsFinal as BackgroundFinal, 
			 --cast(case when L.cnt = 8 then 1 else 0 end as bit) as CandidateApp,
			 cast(case when L.cnt is null or (isnull(L.cnt, 0) <> 0)  then 0 else 1 end as bit) as CandidateApp,
			 --кандидат полностью заполнил анкету
			 cast(case when B.IsFinal = 1 and C.IsFinal = 1 and D.IsFinal = 1 and E.IsFinal = 1 and F.IsFinal = 1 and G.IsFinal = 1 and H.IsFinal = 1 and I.IsFinal = 1 and A.IsScanFinal = 1 then 1 else 0 end as bit) as CandidateReady,
			 --согласование
			 I.ApprovalStatus as BackgroundApproval, J.IsComplete as TrainingApproval, K.HigherManagerApprovalStatus as ManagerApproval, 
			 I.PrevApprovalStatus as PrevBackgroundApproval,
			 cast(case when A.Status = 7 or A.Status = 8 then 1 else 0 end as bit) as PersonnelManagerApproval
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
LEFT JOIN (SELECT A.CandidateId, A.cnt - isnull(B.cnt, 0) as cnt 
					FROM (SELECT CandidateId, count(CandidateId) as cnt FROM EmploymentCandidateDocNeeded WHERE IsNeeded = 1 GROUP BY CandidateId) as A
					LEFT JOIN (SELECT B.CandidateId, count(B.CandidateId) as cnt
										FROM RequestAttachment as A
										INNER JOIN EmploymentCandidateDocNeeded as B ON B.CandidateId = A.RequestId and B.DocTypeId = A.RequestType and B.IsNeeded = 1
										GROUP BY B.CandidateId) as B ON B.CandidateId = A.CandidateId) as L ON L.CandidateId = A.Id	
GO