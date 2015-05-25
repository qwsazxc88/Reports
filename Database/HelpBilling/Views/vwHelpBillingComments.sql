IF OBJECT_ID ('vwHelpBillingComments', 'V') IS NOT NULL
	DROP VIEW [dbo].[vwHelpBillingComments]
GO



--достаем комментарии к разделам приема
CREATE VIEW [dbo].[vwHelpBillingComments]
AS
SELECT B.Name as Creator, A.CreatedDate, A.Comment, A.IsQuestion, A.UserId, A.HelpBillingId
FROM HelpBillingComments as A
INNER JOIN Users as B ON B.Id = A.UserId





GO





