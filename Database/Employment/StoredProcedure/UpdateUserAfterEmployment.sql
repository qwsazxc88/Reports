IF OBJECT_ID ('UpdateUserAfterEmployment', 'P') IS NOT NULL
	DROP PROCEDURE dbo.UpdateUserAfterEmployment
GO

--процедура формирует данные для финграда
CREATE PROCEDURE dbo.UpdateUserAfterEmployment
	@CandidateId int	--Id заявки в приеме
AS
BEGIN
SET NOCOUNT ON
DECLARE 
	@UserId int	--Id сотрудника
BEGIN TRANSACTION	
	SELECT @UserId = UserId FROM EmploymentCandidate WHERE Id = @CandidateId

	--если прием по срочному тд, то в расстановке место занято другим сотрудником и стоит признак резерва из приема
	--заносим запись в журнал замещения
	IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE DocId = @CandidateId and ReserveType = 2 and UserId is not null)
	BEGIN
		INSERT INTO StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed)
		SELECT Id, @UserId, UserId, 1 FROM StaffEstablishedPostUserLinks WHERE DocId = @CandidateId and ReserveType = 2
	END

	--ставим принятого сотрудника на зарезервированное место
	UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId, DocId = null, ReserveType = null, IsDismissal = 0, IsUsed = 1
	WHERE DocId = @CandidateId and ReserveType = 2

COMMIT TRANSACTION
    
END
GO
