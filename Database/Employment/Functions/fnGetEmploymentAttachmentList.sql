


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
		SELECT '���� ���������', '��' FROM RequestAttachment WHERE RequestType = 210 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 212 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���', '��' FROM RequestAttachment WHERE RequestType = 212 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 213 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �����', '��' FROM RequestAttachment WHERE RequestType = 213 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 214 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������', '��' FROM RequestAttachment WHERE RequestType = 214 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������', '��' FROM RequestAttachment WHERE RequestType = 211 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� �� �����������', '��' FROM RequestAttachment WHERE RequestType = 221 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� �� �����������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '��' FROM RequestAttachment WHERE RequestType = 222 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '���' 
	END

	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '��' FROM RequestAttachment WHERE RequestType = 223 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � �������������� �����������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ��������� ������������', '��' FROM RequestAttachment WHERE RequestType = 224 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ��������� ������������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � �����', '��' FROM RequestAttachment WHERE RequestType = 231 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������ � �������� �����', '��' FROM RequestAttachment WHERE RequestType = 232 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������ � �������� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '��' FROM RequestAttachment WHERE RequestType = 241 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������������� ������', '��' FROM RequestAttachment WHERE RequestType = 242 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 215 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '��' FROM RequestAttachment WHERE RequestType = 215 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 216 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � �������� ������', '��' FROM RequestAttachment WHERE RequestType = 216 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � �������� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ �������� �� ��������� ������������ ������', '��' FROM RequestAttachment WHERE RequestType = 261 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ �������� �� ��������� ������������ ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ � ������������� ��������', '��' FROM RequestAttachment WHERE RequestType = 262 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ����������� ������ � ������������� ��������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ������ �� ������', '��' FROM RequestAttachment WHERE RequestType = 271 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� � ������ �� ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� ��������', '��' FROM RequestAttachment WHERE RequestType = 272 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ��������� ��������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� � ������', '��' FROM RequestAttachment WHERE RequestType = 273 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� � ������', '���' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �2', '��' FROM RequestAttachment WHERE RequestType = 274 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �2', '���' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � ������������ ���������������', '��' FROM RequestAttachment WHERE RequestType = 275 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� � ������������ ���������������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �� ������������ ������', '��' FROM RequestAttachment WHERE RequestType = 276 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �� ������������ ������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� ������������������', '��' FROM RequestAttachment WHERE RequestType = 277 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� ������������������', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������ �� ����� ������', '��' FROM RequestAttachment WHERE RequestType = 278 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������ �� ����� ������', '���' 
	END
	/*
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ������� ����', '��' FROM RequestAttachment WHERE RequestType = 279 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ������� ����', '���' 
	END
	*/
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ���������� � ���������� ������������, ���������� � ��������� �����', '��' FROM RequestAttachment WHERE RequestType = 280 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� ���������� � ���������� ������������, ���������� � ��������� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������� �� ����������� ����������� ��������, ������������ ������������, � ��������� �����', '��' FROM RequestAttachment WHERE RequestType = 281 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ���������� �� ����������� ����������� ��������, ������������ ������������, � ��������� �����', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ����������� ���� �� �������� ������������ ������ (���������� �3)', '��' FROM RequestAttachment WHERE RequestType = 282 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� �������� ����������� ���� �� �������� ������������ ������ (���������� �3)', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ���������� ���������� ��� ����������� �������� ������ ������������ ��� (���������� 1)', '��' FROM RequestAttachment WHERE RequestType = 283 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ���������� ���������� ��� ����������� �������� ������ ������������ ��� (���������� 1)', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������ �������� � ����� ������������ ��� (���������� 2)', '��' FROM RequestAttachment WHERE RequestType = 284 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� �� ������������ �������� � ����� ������������ ��� (���������� 2)', '���' 
	END
	
	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � ������������� ������������ � ��������� �����', '��' FROM RequestAttachment WHERE RequestType = 285 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������������� � ������������� ������������ � ��������� �����', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 286 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� 182-� �� ����������� ������������', '��' FROM RequestAttachment WHERE RequestType = 286 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� 182-� �� ����������� ������������', '���' 
	END

	IF EXISTS (SELECT * FROM RequestAttachment WHERE RequestType = 287 and RequestId = @CandidateId)
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� 2-���� �� ����������� ������������', '��' FROM RequestAttachment WHERE RequestType = 287 and RequestId = @CandidateId
	END
	ELSE
	BEGIN
		INSERT INTO @ReturnTable(AttachmentTypeName, AtachmentAvalable)
		SELECT '���� ������� 2-���� �� ����������� ������������', '���' 
	END
	
--select * from dbo.fnGetEmploymentAttachmentList(36) order by AttachmentTypeName

	RETURN 
END

GO


