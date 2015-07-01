--������ ������ ������ �������� � ���� �������� �� ������������� ������ 
--������� � ���������� ��������
--return

SET NOCOUNT ON 
DECLARE @aa varchar(5000), @bb varchar(5000), @len int, @i int, @RowId int

--SET @aa = '����������� �������� ������� �� ������ ���������� ����������� �������� ���������� ����� �� ���������� ���������� �������� ������� �� ��������� ���������������� � ������� ������ �������� � �������������� ��� �������� ������������� "������� ������"����������� ������� "�����"'
SELECT identity (int, 1, 1) as RowId, Operation into #TMP1
FROM (SELECT distinct [��������] as Operation FROM [TestExpress].[dbo].[Fingrag_csv] WHERE [��������] <> '-' and [��������] <> '') as a
order by a.Operation
--������� ������� � ������ ��� ����������� ���������
UPDATE #TMP1 SET Operation = replace(Operation, '2) ������ ��������:', ';') WHERE Operation like '%2) ������ ��������:%'
UPDATE #TMP1 SET Operation = replace(Operation, 'II. ������ ��������:', ';') WHERE Operation like '%II. ������ ��������:%'
UPDATE #TMP1 SET Operation = replace(Operation, '1) � �������� ����������� ������� � ������� ���������� ��������� � ������������ �����:', '') WHERE Operation like '1) � �������� ����������� ������� � ������� ���������� ��������� � ������������ �����:%'
UPDATE #TMP1 SET Operation = replace(Operation, '������� 1) � �������� ����������� ������� � ������� ���������� ��������� � ������������ �����:', '') WHERE Operation like '%�������%'
DELETE FROM #TMP1 WHERE Operation  = '�������� �������: ������'
UPDATE #TMP1 SET Operation = replace(Operation, '. -', ';') WHERE Operation  like '%. -%'
UPDATE #TMP1 SET Operation = replace(Operation, '; -', ';') WHERE Operation  like '%; -%'
UPDATE #TMP1 SET Operation = replace(Operation, ';-', ';') WHERE Operation  like '%;-%'
UPDATE #TMP1 SET Operation = replace(Operation, '.;', ';') WHERE Operation  like '%.;%'
UPDATE #TMP1 SET Operation = replace(Operation, '18.', ';') WHERE Operation  like '%18.%'
UPDATE #TMP1 SET Operation = replace(Operation, '17.', ';') WHERE Operation  like '%17.%'
UPDATE #TMP1 SET Operation = replace(Operation, '16.', ';') WHERE Operation  like '%16.%'
UPDATE #TMP1 SET Operation = replace(Operation, '15.', ';') WHERE Operation  like '%15.%'
UPDATE #TMP1 SET Operation = replace(Operation, '14.', ';') WHERE Operation  like '%14.%'
UPDATE #TMP1 SET Operation = replace(Operation, '13.', ';') WHERE Operation  like '%13.%'
UPDATE #TMP1 SET Operation = replace(Operation, '12.', ';') WHERE Operation  like '%12.%'
UPDATE #TMP1 SET Operation = replace(Operation, '11.', ';') WHERE Operation  like '%11.%'
UPDATE #TMP1 SET Operation = replace(Operation, '10.', ';') WHERE Operation  like '%10.%'
UPDATE #TMP1 SET Operation = replace(Operation, '9.', ';') WHERE Operation  like '%9.%'
UPDATE #TMP1 SET Operation = replace(Operation, '8.', ';') WHERE Operation  like '%8.%'
UPDATE #TMP1 SET Operation = replace(Operation, '7.', ';') WHERE Operation  like '%7.%'
UPDATE #TMP1 SET Operation = replace(Operation, '6.', ';') WHERE Operation  like '%6.%'
UPDATE #TMP1 SET Operation = replace(Operation, '5.', ';') WHERE Operation  like '%5.%'
UPDATE #TMP1 SET Operation = replace(Operation, '4.', ';') WHERE Operation  like '%4.%'
UPDATE #TMP1 SET Operation = replace(Operation, '3.', ';') WHERE Operation  like '%3.%'
UPDATE #TMP1 SET Operation = replace(Operation, '2.', ';') WHERE Operation  like '%2.%'
UPDATE #TMP1 SET Operation = replace(Operation, '1.', ';;;') WHERE Operation  like '%1.%'
UPDATE #TMP1 SET Operation = replace(Operation, ';;;', '') WHERE Operation  like '%;;;%'
UPDATE #TMP1 SET Operation = replace(Operation, '.;', ';') WHERE Operation  like '%.;%'
UPDATE #TMP1 SET Operation = replace(Operation, ';;', ';') WHERE Operation  like '%;;%'
UPDATE #TMP1 SET Operation = replace(Operation, '-�', '�') WHERE Operation  like '%-�%'
UPDATE #TMP1 SET Operation = replace(Operation, '��� ��� "����������"', '++"') WHERE Operation  like '%��� ��� "����������"%'



--select len('������������� �������, �������, �����, �������� � ���� �������� � ������� ��������, ������������ ������� ���������� ���������, � ������� ��������, ��������������� ����������� �������� ������� �� ������ � �� ���������� �����, � ����� ������� ��������, ������������� �������� � �������� �� ������� ��������� ����������� �������� � ������������ � ������������ ��������,')

CREATE TABLE #TMP (id int, [Description] varchar(400))

--SELECT * FROM #TMP1
--delete FROM #TMP1 where rowid <> 18

WHILE EXISTS(SELECT * FROM #TMP1)
BEGIN
	SET @RowId = (SELECT top 1 rowid FROM #TMP1)

	SET @aa = (SELECT Operation FROM #TMP1 WHERE RowId = @RowId)
	SET @bb = ''

	SELECT @i = 1, @len = len(@aa) + 1
	--select PATINDEX ('%;%', @aa), @aa
		--� ����� ����� �� �������� ������ � ���� �� �� �����
	WHILE @i < @len
	BEGIN
		--������������ ������, ��� ���� ����������� ';'
		IF (PATINDEX ('%;%', @aa) <> 0)
		BEGIN
			IF substring(@aa, @i, 1) = ';'
			BEGIN
--				print rtrim(ltrim(@bb))
				IF NOT EXISTS (SELECT * FROM #TMP WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END
			ELSE
			BEGIN
				SET @bb = isnull(@bb, '') + substring(@aa, @i, 1)
			END

			--��������� �����
			IF @len - @i = 1
			BEGIN
--				print rtrim(ltrim(@bb))
				IF NOT EXISTS (SELECT * FROM #TMP WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END
		END
		

		--��� ������������ �������� ��������� �����
		IF (PATINDEX ('%;%', @aa) = 0)
		BEGIN
			IF ascii(substring(@aa, @i, 1)) in (ascii('�'), ascii('�'), ascii('�'), ascii('�')) and len(isnull(@bb, '')) <> 0
			BEGIN
--				print rtrim(ltrim(@bb))
				IF NOT EXISTS (SELECT * FROM #TMP WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END

			SET @bb = isnull(@bb, '') + substring(@aa, @i, 1)

			--��������� �����
			IF @len - @i = 1
			BEGIN
--				print rtrim(ltrim(@bb))
				IF NOT EXISTS (SELECT * FROM #TMP WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END
		END


		SET @i += 1
	END

	DELETE FROM #TMP1 WHERE RowId = @RowId
END


DELETE FROM #TMP WHERE [Description] = ''


UPDATE #TMP SET [Description] = replace([Description], '++', '��� "����������"') WHERE [Description]  like '%++%'

--SELECT @bb
--SELECT SUBSTRING(@aa, 1, char(ascii('�')))

--SELECT ascii('�'), ascii('�'), ascii('�')

INSERT INTO StaffDepartmentOperations(Version, Name)
SELECT 1, [Description] FROM #TMP

--SELECT * FROM #TMP


drop table #TMP
drop table #TMP1