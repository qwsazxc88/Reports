IF OBJECT_ID ('fnGetReplacedName', 'FN') IS NOT NULL
	DROP FUNCTION [dbo].[fnGetReplacedName]
GO


--������� ���������� ������ � ����������� ������������ � ������� ������� ��� ������� ����������
CREATE FUNCTION dbo.fnGetReplacedName
(
	--��� ������� ������� ����� ������� ���� �� ����������
	@LinkId int	--Id ����� ������� ������� � ����������
	,@ReplacedId int	--Id ���������� ������� � �������, �� ��� ����� �� ��������
	,@Switch int	--������������� ��������� ��� ������ �������
			--1 - ��
			--2 - ��������� �����������
			--3 - ���������� ����������
)
RETURNS nvarchar(500)
AS
BEGIN
DECLARE 
	@ReplacedName nvarchar(500)

	IF @Switch = 1
	BEGIN
		--���������� ���� �������� ���������
		IF @ReplacedId is null	
			SELECT @ReplacedName = case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + N' ' + convert(nvarchar, C.BeginDate, 103) + N' - ' + convert(nvarchar, C.EndDate, 103) + ')'
			FROM StaffPostReplacement as A
			INNER JOIN Users as B ON B.Id = A.ReplacedId and B.IsActive = 1 and B.RoleId & 2 > 0
			--���� ��������� �������� �� ����� �� ��������
			LEFT JOIN ChildVacation as C ON C.UserId = B.Id and C.SendTo1C is not null and C.DeleteDate is null and getdate() between C.BeginDate and C.EndDate 
			WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
		ELSE	--���������� ����������, ������� ���� � ������ �� ����� �� ��������, �� ��������� ��� ��������
			SELECT @ReplacedName = N'(' + A.Name + N' ' + convert(nvarchar, B.BeginDate, 103) + N' - ' + convert(nvarchar, B.EndDate, 103) + N')'
			FROM Users as A 
			--���� ��������� �������� �� ����� �� ��������
			LEFT JOIN ChildVacation as B ON B.UserId = A.Id and B.SendTo1C is not null and B.DeleteDate is null and getdate() between B.BeginDate and B.EndDate 
			WHERE A.Id = @ReplacedId and A.IsActive = 1 and A.RoleId & 2 > 0
	END
	
	--IF @Switch = 2
	IF @Switch = 3
	BEGIN
		SET @ReplacedName = N''

		SELECT @ReplacedName += case when len(isnull(@ReplacedName, N'')) = 0 then N'' else N'; ' end +
															N'(' + B.Name + N' ' + convert(nvarchar, A.DateBegin, 103) + N' - ' + convert(nvarchar, A.DateEnd, 103) + ')' 
		FROM StaffTemporaryReleaseVacancyRequest as A
		INNER JOIN Users as B ON B.Id = A.ReplacedId
		WHERE A.UserLinkId = @LinkId and A.IsUsed = 1
		ORDER BY A.Id

	END
	
	RETURN @ReplacedName
--SELECT dbo.fnGetReplacedName(7353, NULL, 3)

END
GO

