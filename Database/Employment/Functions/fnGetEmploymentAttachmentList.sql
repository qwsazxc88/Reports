


IF OBJECT_ID ('fnGetEmploymentAttachmentList', 'TF') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetEmploymentAttachmentList]
GO



CREATE FUNCTION [dbo].[fnGetEmploymentAttachmentList]
(
	@CandidateId int
)
RETURNS 
@ReturnTable TABLE 
(
--	Id int, 
--	AttachmentType int,
	AttachmentTypeName nvarchar(150),
	AtachmentAvalable nvarchar(10)
)
AS
BEGIN
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 210 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Фото кандидата', 'да' FROM RequestAttachment WHERE RequestType = 210 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Фото кандидата', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 212 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ИНН', 'да' FROM RequestAttachment WHERE RequestType = 212 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ИНН', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 213 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан СНИЛС', 'да' FROM RequestAttachment WHERE RequestType = 213 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан СНИЛС', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 214 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки об инвалидности', 'да' FROM RequestAttachment WHERE RequestType = 214 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки об инвалидности', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан паспорта', 'да' FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан паспорта', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа об образовании', 'да' FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа об образовании', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о послевузовском образовании', 'да' FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о послевузовском образовании', 'нет' 
	END

	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о дополнительном образовании', 'да' FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о дополнительном образовании', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о повышении квалификации', 'да' FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан документа о повышении квалификации', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельства о браке', 'да' FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельства о браке', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельств о рождении детей', 'да' FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан свидетельств о рождении детей', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан военного билета', 'да' FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан военного билета', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан мобилизационного талона', 'да' FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан мобилизационного талона', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 215 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудовой книжки', 'да' FROM RequestAttachment WHERE RequestType = 215 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудовой книжки', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 216 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан вкладыша в трудовую книжку', 'да' FROM RequestAttachment WHERE RequestType = 216 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан вкладыша в трудовую книжку', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста согласия на обработку персональных данных', 'да' FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста согласия на обработку персональных данных', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста о достоверности сведений', 'да' FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан рукописного текста о достоверности сведений', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан заявления о приеме на работу', 'да' FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан заявления о приеме на работу', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудового договора', 'да' FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан трудового договора', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан приказа о приеме', 'да' FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан приказа о приеме', 'нет' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан Т2', 'да' FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан Т2', 'нет' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан договора о материальной ответственности', 'да' FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан договора о материальной ответственности', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ДС персональные данные', 'да' FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан ДС персональные данные', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства конфеденциальности', 'да' FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства конфеденциальности', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан листка по учету кадров', 'да' FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан листка по учету кадров', 'нет' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан реестра личного дела', 'да' FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан реестра личного дела', 'нет' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан памятки сотруднику о сохранении коммерческой, банковской и служебной тайны', 'да' FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан памятки сотруднику о сохранении коммерческой, банковской и служебной тайны', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан инструкции по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну', 'да' FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан инструкции по обеспечению сохранности сведений, составляющих коммерческую, и служебную тайну', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан согласия физического лица на проверку персональных данных (Приложение №3)', 'да' FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан согласия физического лица на проверку персональных данных (Приложение №3)', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)', 'да' FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по исполнению требований при организации кассовой работы сотрудниками ВСП (Приложение 1)', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)', 'да' FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан порядка по обслуживанию клиентов в кассе сотрудниками ВСП (Приложение 2)', 'нет' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства о неразглашении коммерческой и служебной тайны', 'да' FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан обязательства о неразглашении коммерческой и служебной тайны', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 286 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки 182-Н от предыдущего работодателя', 'да' FROM RequestAttachment WHERE RequestType = 286 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки 182-Н от предыдущего работодателя', 'нет' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 287 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки 2-НДФЛ от предыдущего работодателя', 'да' FROM RequestAttachment WHERE RequestType = 287 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT 'Скан справки 2-НДФЛ от предыдущего работодателя', 'нет' 
	END
	
--select * from dbo.fnGetEmploymentAttachmentList(36) order by AttachmentTypeName

	RETURN 
END

GO


