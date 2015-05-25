IF OBJECT_ID ('fnGetHelpBillingExecutorForTask', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetHelpBillingExecutorForTask]
GO
--функция по заданному Id задачи внутреннего биллинга выдает список сотрудников 
CREATE FUNCTION dbo.fnGetHelpBillingExecutorForTask 
(	
	@HelpBillingId int,	--ID задачи биллинга
	@IsNew bit					--признак, создается задача или отправлена на выполнение
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT D.Id, --cast(case when exists (SELECT * FROM HelpBillingExecutorTasks WHERE HelpBillingId = @HelpBillingId and UserId = A.UserId) then 1 else 0 end as bit) as IsRecipient, 
				 cast(case when D.Id is not null then 1 else 0 end as bit) as IsRecipient, 
				 A.UserId, B.Name, A.RoleId, C.[Description]
	FROM HelpBillingRoleRecord as A
	INNER JOIN Users as B ON B.id = A.UserId
	INNER JOIN HelpBillingRole as C ON C.Id = A.RoleId
	LEFT JOIN HelpBillingExecutorTasks as D ON D.UserId = A.UserId and D.HelpBillingId = @HelpBillingId
	WHERE (case when D.Id is not null then 1 else 0 end) = (case when @IsNew = 1 then (case when D.Id is not null then 1 else 0 end) else 1 end)
)
GO
