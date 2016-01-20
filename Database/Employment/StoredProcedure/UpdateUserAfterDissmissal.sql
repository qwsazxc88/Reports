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
	--���������, ������� �� ���� ��������� ���� ���� � �����������
	IF EXISTS (SELECT * FROM StaffPostReplacement as A WHERE UserId = @UserId)
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
	ELSE
	BEGIN
		--����������� ����� � �����������
		UPDATE StaffEstablishedPostUserLinks SET UserId = null
		WHERE UserId = @UserId
	END

COMMIT TRANSACTION
    
END