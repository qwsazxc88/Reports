--������ ��������� � ������� ������ �� �������� � ���� ������ 

--select * from Fingrag_csv 
DECLARE @Id int, @DepRequestId int, @LegalAddressId int, @FactAddressId int, @DMDetailId int, @WorkDays varchar(7), 
				@aa varchar(5000), @bb varchar(5000), @len int, @i int, @RowId int, @Oper varchar(max)

--������� ������, ������� ������� �� ���� 1� � ����� ��� � ������� �������� �� ������ ����
SELECT A.Id, A.ParentId, C.* INTO #TMP
FROM Department as A
INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
INNER JOIN Fingrag_csv as C ON C.[���_�������������] = B.FinDepPointCode

--�������� ������ � �������
UPDATE #TMP SET [����_���������] = case when year([����_���������]) = 1899 then null else [����_���������] end
								,[���_���������] = case when len([���_���������]) = 0 or [���_���������] = N'-' then null else [���_���������] end
								,[������_������������] = case when len([������_������������]) = 0 or [������_������������] = N'-' then null else [������_������������] end
								,[�����������_������������] = case when len([�����������_������������]) = 0 or [�����������_������������] = N'-' then null else [�����������_������������] end
								,[������] = case when len([������]) = 0 or [������] = N'-' then null else REPLACE(REPLACE([������], N',', N'.'), CHAR(32), '') end
								,[�������_���������] = case when len([�������_���������]) = 0 or [�������_���������] = N'-' then null else [�������_���������] end
								,[����������_�����] = case when len([����������_�����]) = 0 or [����������_�����] = N'-' then null else [����������_�����] end
								,[�����_���] = case when len([�����_���]) = 0 or [�����_���] = N'-' then null else [�����_���] end
								,[������_�������������] = case when len([������_�������������]) = 0 or [������_�������������] = N'-' then null else [������_�������������] end
								,[����_��������_�����] = case when year([����_��������_�����]) = 1899 then null else [����_��������_�����] end
								,[����_ ��������_�����] = case when year([����_ ��������_�����]) = 1899 then null else [����_ ��������_�����] end
								,[������������_���������] = case when len([������������_���������]) = 0 or [������������_���������] = N'-' then null else [������������_���������] end
								,[�������_�������������] = case when len([�������_�������������]) = 0 then '0' else REPLACE([�������_�������������], N',', N'.') end	--�������� ����
								,[���������_��������] = case when len([���������_��������]) = 0 or [���������_��������] = N'-' then null else [���������_��������] end
								,[�����_������������_�������] = case when len([�����_������������_�������]) = 0 then '0' else REPLACE([�����_������������_�������], N',', N'.') end	--�������� ����
								,[���_��������] = case when len([���_��������]) = 0 or [���_��������] = N'-' then null else [���_��������] end
								,[���_���] = case when len([���_���]) = 0 or [���_���] = N'-' then null else [���_���] end
								,[���_��������] = case when len([���_��������]) = 0 or [���_��������] = N'-' then null else [���_��������] end
								,[���_��] = case when len([���_��]) = 0 or [���_��] = N'-' then null else [���_��] end
								,[���_1�] = case when len([���_1�]) = 0 or [���_1�] = N'-' then null else [���_1�] end
								,[���_�������������] = case when len([���_�������������]) = 0 or [���_�������������] = N'-' then null else [���_�������������] end
								,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
								,[������������_�_���_��] = case when len([������������_�_���_��]) = 0 or [������������_�_���_��] = N'-' then null else [������������_�_���_��] end
								,[��_��������] = case when len([��_��������]) = 0 or [��_��������] = N'-' then null else [��_��������] end
								,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
								,[�������_���_�������������] = case when len([�������_���_�������������]) = 0 or [�������_���_�������������] = N'-' then null else [�������_���_�������������] end
								,[Front_Back1] = case when len([Front_Back1]) = 0 or [Front_Back1] = N'-' then null else [Front_Back1] end
								,[�������������_��������_��������] = case when len([�������������_��������_��������]) = 0 or [�������������_��������_��������] = N'-' then null else [�������������_��������_��������] end
								,[������_������] = case when len([������_������]) = 0 or [������_������] = N'-' then null else [������_������] end
								,[�����_��_����_��_���������_�_�������_�����] = case when len([�����_��_����_��_���������_�_�������_�����]) = 0 or [�����_��_����_��_���������_�_�������_�����] = N'-' then null else [�����_��_����_��_���������_�_�������_�����] end
								,[������������_������] = case when len([������������_������]) = 0 or [������������_������] = N'-' then null else [������������_������] end
								,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
								,[�������] = case when len([�������]) = 0 or [�������] = N'-' then null else [�������] end
								,[�����_��������] = case when len([�����_��������]) = 0 or [�����_��������] = N'-' then null else [�����_��������] end
								,[�����_������_�����_�������_�_��] = case when len([�����_������_�����_�������_�_��]) = 0 or [�����_������_�����_�������_�_��] = N'-' then null else [�����_������_�����_�������_�_��] end
								,[�������������_�������������_������] = case when len([�������������_�������������_������]) = 0 or [�������������_�������������_������] = N'-' then null else [�������������_�������������_������] end
								,[�������������_�������������_���������] = case when len([�������������_�������������_���������]) = 0 or [�������������_�������������_���������] = N'-' then null else [�������������_�������������_���������] end
								,[����_�������_������_������] = case when year([����_�������_������_������]) = 1899 then null else [����_�������_������_������] end
								,[���_��_����������_����������_�_��������_�����] = case when len([���_��_����������_����������_�_��������_�����]) = 0 then '0' else REPLACE([���_��_����������_����������_�_��������_�����], N',', N'.') end
								,[��������] = case when len([��������]) = 0 or [��������] = N'-' then null else [��������] end
								,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
								,[�������_�����] = case when len([�������_�����]) = 0 or [�������_�����] = N'-' then null else [�������_�����] end
								,[���������_�������_�����] = case when len([���������_�������_�����]) = 0 or [���������_�������_�����] = N'-' then null else [���������_�������_�����] end
								,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
								,[���_���������] = case when len([���_���������]) = 0 or [���_���������] = N'-' then null else [���_���������] end
								,[����_�������_���������_������] = case when year([����_�������_���������_������]) = 1899 then null else [����_�������_���������_������] end
								,[ID_��������] = case when len([ID_��������]) = 0 or [ID_��������] = N'-' then null else [ID_��������] end
								,[���_������_�����] = case when len([���_������_�����]) = 0 or [���_������_�����] = N'-' then null else [���_������_�����] end
								,[����_������_�������_�����] = case when year([����_������_�������_�����]) = 1899 then null else [����_������_�������_�����] end
								,[����_�������������_������_�����] = case when year([����_�������������_������_�����]) = 1899 then null else [����_�������������_������_�����] end
								,[J_�����_���������] = case when len([J_�����_���������]) = 0 or [J_�����_���������] = N'-' then null else [J_�����_���������] end
								,[�������������_��_��������_�����] = case when len([�������������_��_��������_�����]) = 0 or [�������������_��_��������_�����] = N'-' then null else [�������������_��_��������_�����] end
								,[���_���] = case when len([���_���]) = 0 or [���_���] = N'-' then null else [���_���] end
								,[���_GE] = case when len([���_GE]) = 0 or [���_GE] = N'-' then null else [���_GE] end
								,[���������_���������_����������] = case when len([���������_���������_����������]) = 0 or [���������_���������_����������] = N'-' then null else [���������_���������_����������] end
								,[���������_��������_�������] = case when len([���������_��������_�������]) = 0 or [���������_��������_�������] = N'-' then null else [���������_��������_�������] end
								,[���������_��������_������] = case when len([���������_��������_������]) = 0 or [���������_��������_������] = N'-' then null else [���������_��������_������] end
								,[���������_�����_������] = case when len([���������_�����_������]) = 0 or [���������_�����_������] = N'-' then null else [���������_�����_������] end
								,[�������_��������_�_����������] = case when len([�������_��������_�_����������]) = 0 or [�������_��������_�_����������] = N'-' then null else [�������_��������_�_����������] end
								,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
								,[����������2] = case when len([����������2]) = 0 or [����������2] = N'-' then null else [����������2] end
								,[��������_��_��������] = case when len([��������_��_��������]) = 0 or [��������_��_��������] = N'-' then null else [��������_��_��������] end
								,[���_��_�_�������_��_��������] = case when len([���_��_�_�������_��_��������]) = 0 or [���_��_�_�������_��_��������] = N'-' then null else [���_��_�_�������_��_��������] end
								,[ID_������_������_������_������] = case when len([ID_������_������_������_������]) = 0 or [ID_������_������_������_������] = N'-' then null else [ID_������_������_������_������] end
								,[ID_��������_��������] = case when len([ID_��������_��������]) = 0 or [ID_��������_��������] = N'-' then null else [ID_��������_��������] end
								,[����������_��������_������_������] = case when len([����������_��������_������_������]) = 0 or [����������_��������_������_������] = N'-' then null else [����������_��������_������_������] end
								,[ID_����������_��������_����������_��������] = case when len([ID_����������_��������_����������_��������]) = 0 or [ID_����������_��������_����������_��������] = N'-' then null else [ID_����������_��������_����������_��������] end
								,[�����] = case when len([�����]) = 0 or [�����] = N'-' then null else [�����] end
								,[�����] = case when len([�����]) = 0 or [�����] = N'-' then null else [�����] end


UPDATE #TMP SET [������] = REPLACE([������], char(160), '')
UPDATE #TMP SET [������] = SUBSTRING([������], 1, isnull(charindex('.', [������]), 1) - 1) --WHERE [���_�������������] = '04-07-24-011'
UPDATE #TMP SET [������] = SUBSTRING([������], 1, 6) 
UPDATE #TMP SET ���_��_����������_����������_�_��������_����� = null where ���_��_����������_����������_�_��������_����� = '06.08.2014'
UPDATE #TMP SET [���_������_�����] = '1111110' WHERE [���_������_�����] = '111110'


UPDATE #TMP SET [���������_�������_�����] = REPLACE([���������_�������_�����], char(32) + char(32), '')
UPDATE #TMP SET [���������_��������_������] = REPLACE([���������_��������_������], char(32) + char(32), '')
UPDATE #TMP SET [���������_���������_����������] = 
								case when [���������_���������_����������] = '2. ��������� ����������' then null 
										 when [���������_���������_����������] like '%��������� ����������%' 
										 then ltrim(rtrim(substring([���������_���������_����������], CHARINDEX('��������� ����������', [���������_���������_����������]) + 21, len([���������_���������_����������])))) 
										 else (case when [���������_���������_����������] = '���' then null else [���������_���������_����������] end)
										 end
WHERE [���������_�������_�����] = '���'

UPDATE #TMP SET [���������_��������_�������] = 
								REPLACE(
												REPLACE(
																REPLACE(
																				REPLACE(
																							REPLACE([���������_��������_�������], '3. �������� ������� (�������, ��������, "��������" �������� ������ � ��)', ''), 
																							'3. �������� ������� (�������, ��������, "��������" �������� ������)', ''),
																							'3. �������� ������� (�������, ��������, "��������" �������� ������', ''),
																							'3. �������� �������', ''),
																							'3.�������� �������', '')
WHERE [���������_�������_�����] = '���'

UPDATE #TMP SET [���������_��������_�������] = case when ltrim(rtrim([���������_��������_�������])) = '���' or len(ltrim(rtrim([���������_��������_�������]))) = 0 then null else ltrim(rtrim([���������_��������_�������])) end
WHERE [���������_�������_�����] = '���'


UPDATE #TMP SET [���������_��������_������] = 
								REPLACE( 
											 REPLACE( 
															 REPLACE(
																			REPLACE([���������_��������_������], '4. �������� ������ � �������� (����� ��� �� � ���)', ''),
																			'4. �������� ������ � ��������', ''),
																			'4. �������� ������', ''), '4.', '')
WHERE [���������_�������_�����] = '���'

UPDATE #TMP SET [���������_��������_������] = case when ltrim(rtrim([���������_��������_������])) = '���' or len(ltrim(rtrim([���������_��������_������]))) = 0 then null else ltrim(rtrim([���������_��������_������])) end
WHERE [���������_�������_�����] = '���'


UPDATE #TMP SET [���������_�����_������] = case when ltrim(rtrim([���������_�����_������])) = '���' or len(ltrim(rtrim([���������_�����_������]))) = 0 then null else ltrim(rtrim([���������_�����_������])) end
WHERE [���������_�������_�����] = '���'


UPDATE #TMP SET [���������_�������_�����] = 
								REPLACE(
								REPLACE(
											REPLACE(
														REPLACE(
																		case when [���������_�������_�����] like '%2.%' 
																				 then  substring([���������_�������_�����], 1, (charindex('2', [���������_�������_�����]) - 1))
																				 else [���������_�������_�����]
																				 end, '1. C������ ����� - ���', ''), 
																				 '1. C������ ����� ���', ''), 
																				 '1. C������ �����', ''),
																				 '1. -', '')
WHERE [���������_�������_�����] is not null and [���������_�������_�����] <> '���'

UPDATE #TMP SET [���������_�������_�����] = case when ltrim(rtrim([���������_�������_�����])) like '%���%' or len(ltrim(rtrim([���������_�������_�����]))) = 0 then null else ltrim(rtrim([���������_�������_�����])) end
WHERE  [���������_�������_�����] is not null and [���������_�������_�����] <> '���'


--��� ��������
UPDATE #TMP SET [��������] = replace([��������], '2) ������ ��������:', ';') WHERE [��������] like '%2) ������ ��������:%'
UPDATE #TMP SET [��������] = replace([��������], 'II. ������ ��������:', ';') WHERE [��������] like '%II. ������ ��������:%'
UPDATE #TMP SET [��������] = replace([��������], '1) � �������� ����������� ������� � ������� ���������� ��������� � ������������ �����:', '') WHERE [��������] like '1) � �������� ����������� ������� � ������� ���������� ��������� � ������������ �����:%'
UPDATE #TMP SET [��������] = replace([��������], '������� 1) � �������� ����������� ������� � ������� ���������� ��������� � ������������ �����:', '') WHERE [��������] like '%�������%'
UPDATE #TMP SET [��������] = replace([��������], '. -', ';') WHERE [��������]  like '%. -%'
UPDATE #TMP SET [��������] = replace([��������], '; -', ';') WHERE [��������]  like '%; -%'
UPDATE #TMP SET [��������] = replace([��������], ';-', ';') WHERE [��������]  like '%;-%'
UPDATE #TMP SET [��������] = replace([��������], '.;', ';') WHERE [��������]  like '%.;%'
UPDATE #TMP SET [��������] = replace([��������], '18.', ';') WHERE [��������]  like '%18.%'
UPDATE #TMP SET [��������] = replace([��������], '17.', ';') WHERE [��������]  like '%17.%'
UPDATE #TMP SET [��������] = replace([��������], '16.', ';') WHERE [��������]  like '%16.%'
UPDATE #TMP SET [��������] = replace([��������], '15.', ';') WHERE [��������]  like '%15.%'
UPDATE #TMP SET [��������] = replace([��������], '14.', ';') WHERE [��������]  like '%14.%'
UPDATE #TMP SET [��������] = replace([��������], '13.', ';') WHERE [��������]  like '%13.%'
UPDATE #TMP SET [��������] = replace([��������], '12.', ';') WHERE [��������]  like '%12.%'
UPDATE #TMP SET [��������] = replace([��������], '11.', ';') WHERE [��������]  like '%11.%'
UPDATE #TMP SET [��������] = replace([��������], '10.', ';') WHERE [��������]  like '%10.%'
UPDATE #TMP SET [��������] = replace([��������], '9.', ';') WHERE [��������]  like '%9.%'
UPDATE #TMP SET [��������] = replace([��������], '8.', ';') WHERE [��������]  like '%8.%'
UPDATE #TMP SET [��������] = replace([��������], '7.', ';') WHERE [��������]  like '%7.%'
UPDATE #TMP SET [��������] = replace([��������], '6.', ';') WHERE [��������]  like '%6.%'
UPDATE #TMP SET [��������] = replace([��������], '5.', ';') WHERE [��������]  like '%5.%'
UPDATE #TMP SET [��������] = replace([��������], '4.', ';') WHERE [��������]  like '%4.%'
UPDATE #TMP SET [��������] = replace([��������], '3.', ';') WHERE [��������]  like '%3.%'
UPDATE #TMP SET [��������] = replace([��������], '2.', ';') WHERE [��������]  like '%2.%'
UPDATE #TMP SET [��������] = replace([��������], '1.', ';;;') WHERE [��������]  like '%1.%'
UPDATE #TMP SET [��������] = replace([��������], ';;;', '') WHERE [��������]  like '%;;;%'
UPDATE #TMP SET [��������] = replace([��������], '.;', ';') WHERE [��������]  like '%.;%'
UPDATE #TMP SET [��������] = replace([��������], ';;', ';') WHERE [��������]  like '%;;%'
UPDATE #TMP SET [��������] = replace([��������], '-�', '�') WHERE [��������]  like '%-�%'
UPDATE #TMP SET [��������] = replace([��������], '��� ��� "����������"', '++"') WHERE [��������]  like '%��� ��� "����������"%'

SELECT identity (int, 1, 1) as RowId, Operation into #TMP1
FROM (SELECT distinct [��������] as Operation FROM #TMP WHERE [��������] <> '-' and [��������] <> '') as a
order by a.Operation

DELETE FROM #TMP1 WHERE Operation  = '�������� �������: ������'

CREATE TABLE #TMP2 (id int, [Description] varchar(400))

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
				IF NOT EXISTS (SELECT * FROM #TMP2 WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP2 (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
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
				IF NOT EXISTS (SELECT * FROM #TMP2 WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP2 (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
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
				IF NOT EXISTS (SELECT * FROM #TMP2 WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP2 (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END

			SET @bb = isnull(@bb, '') + substring(@aa, @i, 1)

			--��������� �����
			IF @len - @i = 1
			BEGIN
--				print rtrim(ltrim(@bb))
				IF NOT EXISTS (SELECT * FROM #TMP2 WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP2 (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END
		END


		SET @i += 1
	END

	DELETE FROM #TMP1 WHERE RowId = @RowId
END


DELETE FROM #TMP2 WHERE [Description] = ''


UPDATE #TMP2 SET [Description] = replace([Description], '++', '��� "����������"') WHERE [Description]  like '%++%'

INSERT INTO StaffDepartmentOperations(Version, Name)
SELECT distinct 1, [Description] FROM #TMP2

--������ �������� �������� ������ � ����� ���������





--��������� ������� ���������� ����� �������������
INSERT INTO StaffDepartmentTypes ([Version], Name)
SELECT distinct 1, [���_�������������] FROM #TMP ORDER BY [���_�������������]




--���� �� ���������� ������
WHILE EXISTS(SELECT * FROM #TMP)
BEGIN
	SELECT top 1 @Id = Id FROM #TMP

	--��� ������ ������ �����������
	--������� �����
	INSERT INTO RefAddresses([Version], [Address], PostIndex)
	SELECT 1, isnull([������], '') + case when [������] is null then '' else ', ' end + [�������_���������] + ', ' + [����������_�����] + ', ' + [�����_���], [������] FROM #TMP WHERE Id = @Id

	SET @LegalAddressId = @@IDENTITY

	INSERT INTO RefAddresses([Version], [Address], PostIndex)
	SELECT 1, isnull([������], '') + case when [������] is null then '' else ', ' end + [�������_���������] + ', ' + [����������_�����] + ', ' + [�����_���], [������] FROM #TMP WHERE Id = @Id

	SET @FactAddressId = @@IDENTITY


	--������� � ������
	INSERT INTO StaffDepartmentRequest ([Version]
																			,DateRequest
																			,RequestTypeId
																			,DepartmentId
																			,ItemLevel
																			,ParentId
																			,Name
																			,IsBack
																			,OrderNumber
																			,OrderDate
																			,LegalAddressId
																			,IsTaxAdminAccount
																			,IsEmployeAvailable
																			,DepNextId
																			,IsPlan
																			,IsUsed
																			,IsDraft
																			,DateSendToApprove
																			,BeginAccountDate
																			,DateState)
	SELECT 1
					,A.[����_���������]
					,case when A.[���_���������] = '��������� � ����������' then 1 else 2 end
					,@Id
					,B.ItemLevel
					,B.ParentId
					,B.Name
					,case when A.[Front_Back1] = 'Front' then 0 when A.[Front_Back1] = 'Back' then 1 else null end
					,[�������]
					,null
					,@LegalAddressId	--�����
					,1
					,1
					,null
					,case when A.[�������_��������_�_����������] = '�� ����� ��' then 1 else 0 end
					,1
					,0
					,null
					,null
					,null
	FROM #TMP as A
	INNER JOIN Department as B ON B.Id = A.Id
	WHERE A.Id = @Id
	

	SET @DepRequestId = @@IDENTITY

	--������� �� ���������
	INSERT INTO StaffDepartmentCBDetails (Version
																				,DepRequestId
																				,ATMCountTotal
																				,ATMCashInCount
																				,ATMCount
																				,DepCachinId
																				,DepATMId
																				,CashInStartedDate
																				,ATMStartedDate)
	SELECT 1
					,@DepRequestId
					,A.[���_��_����������_����������_�����]
					,A.[���_��_����������_�������] + isnull(A.[���_��_����������_����������_�_��������_�����], 0)
					,A.[���_��_����������_����������_�_��������_����������]
					,null
					,null
					,A.[����_�������_������_������]
					,A.[����_�������_���������_������]
	FROM #TMP as A
	WHERE A.Id = @Id



	--������� �������������� ���������
	INSERT INTO StaffDepartmentManagerDetails([Version]
																						,DepRequestId
																						,DepCode
																						,NameShort
																						,ReferenceReason
																						,PrevDepCode
																						,FactAddressId
																						,DepStatus
																						,DepTypeId
																						,OpenDate
																						,CloseDate
																						,Reason
																						,OperationMode
																						,BeginIdleDate
																						,EndIdleDate
																						,IsRentPlace
																						,Phone
																						,IsBlocked
																						,IsNetShop
																						,IsAvailableCash
																						,IsLegalEntity
																						,PlanEPCount
																						,PlanSalaryFund
																						,Note)
	SELECT 1
					,@DepRequestId
					,A.[���_�������������]
					,A.[�����������_������������]
					,A.[�������_��������_�_����������]
					,A.[�������_���_�������������]
					,@FactAddressId		--�����
					,A.[������_�������������]
					,B.Id			--���� �������������
					,A.[����_��������_�����]
					,A.[����_��������_�����]
					,A.[�������_��������_�_����������]
					,A.[�����_������_�����_�������_�_��]
					,A.[����_������_�������_�����]
					,A.[����_�������������_������_�����]
					,case when A.[������������_���������] = '�������������' then 0 else 1 end
					,A.[�����_��������]
					,case when A.[����������] = '���������' then 0 else 1 end
					,case when A.[�������������_��������_��������] = '�������' then 1 else 0 end
					,case when isnull(A.[�������_�����], '���') like '%���%' then 0 else 1 end
					,case when A.[������������_��] = '��' then 1 else 0 end
					,0
					,0
					,A.[����������]
	FROM #TMP as A
	LEFT JOIN StaffDepartmentTypes as B ON B.Name = A.[���_�������������]
	WHERE A.Id = @Id

	SET @DMDetailId = @@IDENTITY


		--��������� ���� ������������� ��������
		IF (SELECT [���_��������] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 1, [���_��������] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [���_���] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 2, [���_���] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [���_��������] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 3, [���_��������] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [���_��] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 4, [���_��] FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [���_���������] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 5, [���_���������] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [���_���] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 6, [���_���] FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [���_GE] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 7, [���_GE] FROM #TMP WHERE Id = @Id
		END
		
		
		--��������� (���������� ������� ������� � ������ � �������� ��)		
		--�� ������ ��������� � ������ ����� null-�� ���
		IF (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) is null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 2, [���������_���������_����������] 
			FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 3, [���������_��������_�������] 
			FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 4, [���������_��������_������] 
			FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 5, [���������_�����_������] 
			FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null
		END
		
		--�� ������ ��������� null-� ���� �� ���� �����
		IF (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) = '���'
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 2, [���������_���������_����������]
			FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 3, [���������_��������_�������] 
			FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 4, [���������_��������_������] 
			FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 5, [���������_�����_������] 
			FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null
		END

		IF (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) <> '���' and (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 1, [���������_�������_�����]
			FROM #TMP WHERE Id = @Id and [���������_�������_�����] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 2, [���������_���������_����������]
			FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 3, [���������_��������_�������] 
			FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 4, [���������_��������_������] 
			FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 5, [���������_�����_������] 
			FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null
		END



		--����� ������ �������������
		SELECT @WorkDays = [���_������_�����] FROM #TMP WHERE Id = @Id
		IF @WorkDays is not null
		BEGIN
			SET @i = 1
			WHILE @i < 8
			BEGIN
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, [WeekDay], IsWorkDay)
				VALUES(1, @DMDetailId, @i, cast(SUBSTRING(@WorkDays, @i, 1) as bit))
				SET @i += 1
			END	
		END


		--��������
		SELECT @Oper = [��������] FROM #TMP WHERE Id = @Id
		SET @RowId = 1

		IF @Oper is not null
		BEGIN
			WHILE EXISTS (SELECT * FROM StaffDepartmentOperations WHERE Id = @RowId)
			BEGIN
				SELECT @i = id, @aa = Name FROM StaffDepartmentOperations WHERE Id = @RowId

				IF @Oper like '%' + @aa + '%'
				BEGIN
					INSERT INTO StaffDepartmentOperationLinks ([Version], DMDetailId, OperationId)
					VALUES(1, @DMDetailId, @i)
				END

				SET @RowId += 1
			END
		END


	DELETE FROM #TMP WHERE Id = @Id
END


drop table #TMP
drop table #TMP1
drop table #TMP2