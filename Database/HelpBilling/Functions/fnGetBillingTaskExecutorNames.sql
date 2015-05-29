IF OBJECT_ID ('fnGetBillingTaskExecutorNames', 'IF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetBillingTaskExecutorNames]
GO


CREATE FUNCTION dbo.fnGetBillingTaskExecutorNames
(
	@HelpBillingId int
)
RETURNS nvarchar(max)
AS
BEGIN
	DECLARE @Names nvarchar(max)
	DECLARE @Tbl table (UserId int, Name nvarchar(150))

	INSERT INTO @Tbl 
	SELECT B.Id, B.Name
	FROM HelpBillingExecutorTasks as A
	INNER JOIN Users as B ON B.id = A.UserId
	WHERE A.HelpBillingId = @HelpBillingId 

	UPDATE @Tbl SET @Names = isnull(@Names, '') + case when @Names is not null then N', ' else N'' end + B.Name
	FROM @Tbl as A
	INNER JOIN Users as B ON B.id = A.UserId
	--WHERE A.HelpBillingId = @HelpBillingId 

	
	RETURN @Names

END
GO

