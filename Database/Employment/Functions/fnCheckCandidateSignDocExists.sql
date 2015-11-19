IF OBJECT_ID ('fnCheckCandidateSignDocExists', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnCheckCandidateSignDocExists]
GO


--функция проверяет наличие всех подписанных документов кандидатом
CREATE FUNCTION dbo.fnCheckCandidateSignDocExists
(
	@CandidateId int	
)
RETURNS bit
AS
BEGIN
	DECLARE @IsExists bit
	
	IF NOT EXISTS(SELECT * FROM EmploymentCandidateDocNeeded WHERE CandidateId = @CandidateId)
		SET @IsExists = 0
	ELSE
		SELECT @IsExists = cast(case when count(*) = 0 then 1 else 0 end as bit) --as DocExists
		FROM EmploymentCandidateDocNeeded as A
		LEFT JOIN RequestAttachment as B ON B.RequestId = A.CandidateId and B.RequestType = A.DocTypeId
		WHERE A.CandidateId = @CandidateId and A.IsNeeded = 1 and B.Id is null
	
	RETURN @IsExists
--SELECT dbo.fnCheckCandidateSignDocExists(1922) as DocExists

END
GO