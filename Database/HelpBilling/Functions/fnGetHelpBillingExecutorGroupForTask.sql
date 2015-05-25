IF OBJECT_ID ('fnGetHelpBillingExecutorGroupForTask', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetHelpBillingExecutorGroupForTask]
GO
--������� �� ������ ������ ����� � ��������� ��� �������� ������
CREATE FUNCTION dbo.fnGetHelpBillingExecutorGroupForTask 
(	
	@HelpBillingId int	--ID ������ ��������
	
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT cast(case when B.RoleId is not null then 1 else 0 end as bit) as IsRecipient, cast(case when B.RoleId is not null then 1 else 0 end as bit) as IsRecipientOld, A.Id as RoleId, A.[Description]
	FROM HelpBillingRole as A
	LEFT JOIN (SELECT distinct B.RoleId
				FROM HelpBillingExecutorTasks as A 
				INNER JOIN HelpBillingRoleRecord as B ON B.UserId = A.UserId
				WHERE A.HelpBillingId = @HelpBillingId) as B ON B.RoleId = A.Id

)
GO
