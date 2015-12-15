IF OBJECT_ID ('UpdateUserAfterEmployment', 'P') IS NOT NULL
	DROP PROCEDURE dbo.UpdateUserAfterEmployment
GO

--��������� ��������� ������ ��� ��������
CREATE PROCEDURE dbo.UpdateUserAfterEmployment
	@CandidateId int	--Id ������ � ������
AS
BEGIN
SET NOCOUNT ON
DECLARE 
	@UserId int	--Id ����������
BEGIN TRANSACTION	
	SELECT @UserId = UserId FROM EmploymentCandidate WHERE Id = @CandidateId

	--���� ����� �� �������� ��, �� � ����������� ����� ������ ������ ����������� � ����� ������� ������� �� ������
	--������� ������ � ������ ���������
	IF EXISTS (SELECT * FROM StaffEstablishedPostUserLinks WHERE DocId = @CandidateId and ReserveType = 2 and UserId is not null)
	BEGIN
		INSERT INTO StaffPostReplacement(UserLinkId, UserId, ReplacedId, IsUsed)
		SELECT Id, @UserId, UserId, 1 FROM StaffEstablishedPostUserLinks WHERE DocId = @CandidateId and ReserveType = 2
	END

	--������ ��������� ���������� �� ����������������� �����
	UPDATE StaffEstablishedPostUserLinks SET UserId = @UserId, DocId = null, ReserveType = null, IsDismissal = 0, IsUsed = 1
	WHERE DocId = @CandidateId and ReserveType = 2

COMMIT TRANSACTION
    
END
GO
