IF OBJECT_ID ('vwEmploymentScanInfo', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentScanInfo]
GO

--все сканы анкеты кроме фото, заявлениz о приеме и документов по приему
CREATE VIEW [dbo].[vwEmploymentScanInfo]
AS
SELECT A.Id, A.RequestId as CandidateId, A.RequestType, A.FileName, A.DateCreated, B.Name as Surname
FROM RequestAttachment as A
LEFT JOIN Users as B ON B.Id = A.UserId
WHERE A.RequestType in (212, 213, 214, 211, 221, 222, 223, 224, 231, 232, 241, 242, 215, 216, 261, 262)

GO