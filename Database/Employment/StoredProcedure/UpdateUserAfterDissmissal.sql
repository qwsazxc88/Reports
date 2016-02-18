IF OBJECT_ID ('UpdateUserAfterDissmissal', 'P') IS NOT NULL
	DROP PROCEDURE dbo.UpdateUserAfterDissmissal
GO

--��������� ��������� ������ ��� ��������� ��� ����������
CREATE PROCEDURE dbo.UpdateUserAfterDissmissal
	@Code nvarchar(32)	--��������� ����� ����������
AS
BEGIN
SET NOCOUNT ON
DECLARE 
	@UserId int	--Id ����������

BEGIN TRANSACTION	
	SELECT @UserId = Id FROM Users WHERE Code = @Code
	--���������, ������� �� ���� ��������� ���� ���� � ����������� � �� �������� �� ��� ������ ���������
	IF EXISTS (SELECT * FROM StaffPostReplacement as A WHERE UserId = @UserId and IsUsed = 1)
	BEGIN
		--���� ������������ ���������� ����� �� �������, ���������� ����������� �� �����
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement as A WHERE ReplacedId = @UserId and IsUsed = 1)
		BEGIN
			--���������� ����������� ���������� � �����������
			UPDATE StaffEstablishedPostUserLinks SET UserId = C.ReplacedId
			FROM StaffEstablishedPostUserLinks as A
			INNER JOIN StaffPostReplacement as C ON C.UserLinkId = A.Id and C.UserId = A.UserId
			WHERE A.UserId = @UserId

			--������� ���������� � ������ ���������
			UPDATE StaffPostReplacement SET IsUsed = 0, EditDate = GETDATE()
			WHERE UserId = @UserId
		END
		/*
		ELSE
		BEGIN
		END
		*/
	END
	ELSE
	BEGIN
		--����������� ����� � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = null
		WHERE UserId = @UserId
	END


	--������� ����� ������
	UPDATE Users SET RegularUserLinkId = null, TempUserLinkId = null WHERE Id = @UserId

	--���� ��������� ��� � ������ �� �������� ��������� �������� ��� ���������� ����������
	IF EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @UserId and IsUsed = 1)
	BEGIN
		UPDATE StaffTemporaryReleaseVacancyRequest SET IsUsed = 0, EditDate = GETDATE() WHERE ReplacedId = @UserId and IsUsed = 1
	END

COMMIT TRANSACTION
    
END