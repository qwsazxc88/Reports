IF OBJECT_ID ('vwEmploymentComments', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwEmploymentComments]
GO



--достаем комментарии к разделам приема
CREATE VIEW [dbo].[vwEmploymentComments]
AS
SELECT B.Name as Creator, isnull(D.Name, '') as CreatorPosition, A.CreatedDate, A.Comment, CommentTypeId, A.CandidateId, A.UserId
FROM EmploymentCandidateComments as A
INNER JOIN Users as B ON B.Id = A.UserId
INNER JOIN Users as C ON C.Id = A.CandidateId
LEFT JOIN Position as D ON D.Id = B.PositionId



GO





