DROP FUNCTION [dbo].[fnGetActivityDiscussionBrand]
GO
CREATE  FUNCTION [dbo].[fnGetActivityDiscussionBrand] (@ActivityID int)  
RETURNS nvarchar(1000) AS  
BEGIN
declare @retVal nvarchar(1000)
set @retVal = N''
select @retVal = @retVal + br.name /*+ ' - ' + isnull(cast(adb.Percentage as char(3)), '0%')*/ + ', '
from activitydiscussionbrand adb
join Brand br on br.ID = adb.BrandID
where adb.ActivityID = @ActivityID
if len(@retVal) > 2 
	set @retVal = substring(@retVal, 1, len(@retVal) - 1 )
return @retVal
END
GO


