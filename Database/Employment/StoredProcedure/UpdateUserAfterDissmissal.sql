IF OBJECT_ID ('UpdateUserAfterDissmissal', 'P') IS NOT NULL
	DROP PROCEDURE dbo.UpdateUserAfterDissmissal
GO

--процедура формирует данные для кандидата при увольнении
CREATE PROCEDURE dbo.UpdateUserAfterDissmissal
	@Code nvarchar(32)	--Табельный номер сотрудника
AS
BEGIN
SET NOCOUNT ON
DECLARE 
	@UserId int	--Id сотрудника

BEGIN TRANSACTION	
	SELECT @UserId = Id FROM Users WHERE Code = @Code
	--проверяем, заменял ли этот сотрудник кого либо в расстановке и не заменяет ли его другой сотрудник
	IF EXISTS (SELECT * FROM StaffPostReplacement as A WHERE UserId = @UserId and IsUsed = 1)
	BEGIN
		--если увольняемого сотрудника никто не замещал, возвращаем заменяемого на место
		IF NOT EXISTS (SELECT * FROM StaffPostReplacement as A WHERE ReplacedId = @UserId and IsUsed = 1)
		BEGIN
			--возвращаем замещаемого сотрудника в расстановку
			UPDATE StaffEstablishedPostUserLinks SET UserId = C.ReplacedId
			FROM StaffEstablishedPostUserLinks as A
			INNER JOIN StaffPostReplacement as C ON C.UserLinkId = A.Id and C.UserId = A.UserId
			WHERE A.UserId = @UserId

			--снимаем активность у записи замещения
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
		--освобождаем место в расстановке
		UPDATE StaffEstablishedPostUserLinks SET UserId = null
		WHERE UserId = @UserId
	END


	--убираем место работы
	UPDATE Users SET RegularUserLinkId = null, TempUserLinkId = null WHERE Id = @UserId

	--если сотрудник был в зявках на создание временной вакансии при длительном отсутствии
	IF EXISTS (SELECT * FROM StaffTemporaryReleaseVacancyRequest WHERE ReplacedId = @UserId and IsUsed = 1)
	BEGIN
		UPDATE StaffTemporaryReleaseVacancyRequest SET IsUsed = 0, EditDate = GETDATE() WHERE ReplacedId = @UserId and IsUsed = 1
	END

COMMIT TRANSACTION
    
END