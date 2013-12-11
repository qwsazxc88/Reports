IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetMissionOrderTargetsCities]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fnGetMissionOrderTargetsCities]
GO
CREATE  FUNCTION [dbo].[fnGetMissionOrderTargetsCities] (@missionOrderId int)  
RETURNS nvarchar(max) AS  
BEGIN
declare @retVal nvarchar(max)
set @retVal = N''
select @retVal = @retVal + t.City + N', '
from dbo.MissionTarget t
where t.MissionOrderId = @missionOrderId
if len(@retVal) >= 2 
	set @retVal = substring(@retVal, 1, len(@retVal) - 1 )
return @retVal
END
GO


