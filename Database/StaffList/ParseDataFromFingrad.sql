--������ ��������� � ������� ������ �� �������� � ���� ������ 
--���� ���������� ���� ��� ���������� ����� ��� ��������, ���������� �������������, �� �� ����������� ������������� �������� ����� ������� ������ � ��������� 4 � ���� BFGId

use WebAppTest
go

SET NOCOUNT ON

--return

--�������� �� ������������ ����� ����� �������������
select PossibleDepartmentId, count(PossibleDepartmentId) as cnt 
into #checkpoint
from TerraPoint where PossibleDepartmentId is not null and ItemLevel = 3 and (EndDate is null or (EndDate is not null and DATEDIFF(dd, EndDate, getdate()) <= 30))
group by PossibleDepartmentId
having count(PossibleDepartmentId) > 1


if exists(select * from #checkpoint)
begin
	print '���������� ������ � ����������'

	select a.Id, c.Name, a.Name, c.City, c.Street, c.House, a.City, a.Street  from TerraPoint as a
	inner join #checkpoint as b on b.PossibleDepartmentId = a.PossibleDepartmentId
	inner join Department as c on c.id = a.PossibleDepartmentId
	order by c.Name

	drop table #checkpoint
	--update TerraPoint set PossibleDepartmentId = null where id in (1241, 3995, 1823, 2409, 227)
	--update TerraPoint set PossibleDepartmentId = null where name like '%(�������)%' or id in (3499, 227)
	return
end
else
	drop table #checkpoint

--����� ��������


DECLARE @Id int, @DepRequestId int, @LegalAddressId int, @FactAddressId int, @DMDetailId int, @WorkDays varchar(7), 
				@aa varchar(5000), @bb varchar(5000), @len int, @i int, @RowId int, @Oper varchar(max), @CreatorId int,
				@DepartmentID int, @DepTmpId int, @DepNewId int, @Code varchar(15), @OperGroupId int

--������� ����������� ��������� ������, ������� �� ������� ������� (���� � ����������� ������� ����� ������������� ������� 4, ����� ����� ���������)
DELETE DepartmentArchive
FROM DepartmentArchive as A
INNER JOIN Department as B on B.Id = A.DepartmentId and B.BFGId = 4

DELETE Department WHERE BFGId = 4	

--������� � ���� ������� 7 ������ ���� �� ��������, ����� ��� ��������� ���������� �� ������
UPDATE Department SET FingradCode = null

--����������� � ��������� ������ ��� ������� � ��������� �������� ����������
--����� ������ �� �������� �������� � �������� �����, ���� �������� �� ������ 30 ���� ������������ ������� ����
--���� ����� ������� � ����� ���������� �������������, �� � ��� ���������� ����������� �� ���
UPDATE Department SET FingradCode = B.Code
FROM Department as A
INNER JOIN TerraPoint as B ON B.PossibleDepartmentId = A.Id and B.ItemLevel = 3 and (B.EndDate is null or (B.EndDate is not null and DATEDIFF(dd, B.EndDate, getdate()) <= 30)) and isnull(A.ParentId, '') <> ''
															and not exists (select * from DepFinRP where [��� �� � �������] = B.Code)
INNER JOIN Fingrad_csv as C ON C.[���_�������������] = B.Code
WHERE A.ItemLevel = 7

--���� ����� ��������� � ����� ���������� �������������, �� ���� ����� ������������ �� ��������
--� ����� ������� ���������� ����� ������������� �� ������� � � ��� �� ����� ��������� ����� ������ (��� ���������� �������������)
SELECT A.* INTO #TPLink FROM TerraPoint as A 
INNER JOIN Fingrad_csv as B ON B.[���_�������������] = A.Code
--INNER JOIN Department as C ON C.Id = A.PossibleDepartmentId and C.ItemLevel = 7
WHERE A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, A.EndDate, getdate()) <= 30)) and isnull(A.ParentId, '') <> ''
			and A.PossibleDepartmentId is null
			--�������� ��-��������
			and not exists (SELECT * FROM DepFinRP WHERE [��� �� � �������] = A.Code)
			and A.ParentDepartmentId is not null

--���� �� ����� �������
WHILE EXISTS (SELECT * FROM #TPLink)
BEGIN
	SELECT top 1 @id = A.Id FROM #TPLink as A
		--������� ������������ ������������� 6 ������ � ��������� ������ � ���������� ��������
	INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
	SELECT 1, null, A.Name, null, C.Code1C, C.Path + N'1', 7, null, 99, 1, 4, A.Code
	FROM #TPLink as A
	INNER JOIN Department as B ON B.Id = A.ParentDepartmentId
	INNER JOIN Department as C ON C.Code1C = B.ParentId
	WHERE A.Id = @Id

	SET @DepNewId = @@IDENTITY
	
	--����������� ���� 
	UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
	WHERE Id = @DepNewId
	
	
	DELETE FROM #TPLink WHERE Id = @Id
END

print '��������� ��������� ������������� ��������� �� ��������� ���������'



--���� ����� �� ������� ������� ������
--������� ����� �� 7 ������ � ���������� ���� ��� �����/�����
INSERT INTO #TPLink
SELECT A.* FROM TerraPoint as A 
INNER JOIN Fingrad_csv as B ON B.[���_�������������] = A.Code
--INNER JOIN Department as C ON C.Id = A.PossibleDepartmentId and C.ItemLevel = 7
WHERE A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, A.EndDate, getdate()) <= 30)) and isnull(A.ParentId, '') <> ''
			and A.PossibleDepartmentId is null
			--�������� ��-��������
			and not exists (SELECT * FROM DepFinRP WHERE [��� �� � �������] = A.Code)
			and A.ParentDepartmentId is null

--2 �������
--declare @DepNewId int
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'� �������', null, Code1C, Path + N'1', 2, null, 99, 1, 4, null 
FROM Department WHERE ItemLevel = 1

SET @DepNewId = @@IDENTITY

--����������� ���� 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--3 �������
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'� �������', null, Code1C, Path + N'1', 3, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--����������� ���� 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--4 �������
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'� �������', null, Code1C, Path + N'1', 4, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--����������� ���� 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--5 �������
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'� �������', null, Code1C, Path + N'1', 5, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--����������� ���� 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId


--6 �������
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'� �������', null, Code1C, Path + N'1', 6, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--����������� ���� 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--SET @DepTmpId = @DepNewId
--��������� � ������������� 6 �������
UPDATE #TPLink SET ParentDepartmentId = @DepNewId

WHILE EXISTS (SELECT * FROM #TPLink)
BEGIN
	SELECT top 1 @id = A.Id FROM #TPLink as A
	--������� ������������ ������������� 6 ������ � ��������� ������ � ���������� ��������
	INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
	SELECT 1, null, A.Name, null, B.Code1C, B.Path + N'1', 7, null, 99, 1, 4, A.Code
	FROM #TPLink as A
	INNER JOIN Department as B ON B.Id = A.ParentDepartmentId
	--INNER JOIN Department as C ON C.Code1C = B.ParentId
	WHERE A.Id = @Id

	SET @DepNewId = @@IDENTITY
	
	--����������� ���� 
	UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
	WHERE Id = @DepNewId

	DELETE FROM #TPLink WHERE Id = @Id
END

print '��������� ��������� ����������� �������������'


		
drop table #TPLink

--������� ������, ������� ������� �� ���� 1� � ����� ��� � ������� �������� �� �� ����
SELECT A.Id, A.ParentId, C.[�����������_������������] as FinDepName, 
			 case A.BFGId when 1 then '���'
										when 2 then '�����'
										when 3 then '���'
										else '��������������' end as FinDepNameShort
			 ,C.* INTO #TMP
FROM Department as A
INNER JOIN TerraPoint as B ON B.PossibleDepartmentId = A.Id and B.ItemLevel = 3 and (B.EndDate is null or (B.EndDate is not null and DATEDIFF(dd, B.EndDate, getdate()) <= 30))
--INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
LEFT JOIN Fingrad_csv as C ON C.[���_�������������] = B.Code
WHERE A.ItemLevel = 7 and A.FingradCode is not null
UNION ALL
SELECT A.Id, A.ParentId, C.[�����������_������������] as FinDepName, 
			 case A.BFGId when 1 then '���'
										when 2 then '�����'
										when 3 then '���'
										else '��������������' end as FinDepNameShort
			 ,C.* --INTO #TMP
FROM Department as A
INNER JOIN TerraPoint as B ON B.Code = A.FingradCode and B.ItemLevel = 3 and (B.EndDate is null or (B.EndDate is not null and DATEDIFF(dd, B.EndDate, getdate()) <= 30))
--INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
LEFT JOIN Fingrad_csv as C ON C.[���_�������������] = B.Code
WHERE A.ItemLevel = 7 and A.BFGId = 4 and A.FingradCode is not null

--��������� ������� ��� ������ ��������
ALTER TABLE #TMP ADD OperGroupId int null


--�������� ������ � �������
UPDATE #TMP SET --[����_���������] = case when year([����_���������]) = 1900 then null else [����_���������] end
								--[���_���������] = case when len([���_���������]) = 0 or [���_���������] = N'-' then null else [���_���������] end
								--,[������_������������] = case when len([������_������������]) = 0 or [������_������������] = N'-' then null else [������_������������] end
								[�����������_������������] = isnull(FinDepName, case when len([�����������_������������]) = 0 or [�����������_������������] = N'-' then null else [�����������_������������] end)
								,[������] = case when len([������]) = 0 or [������] = N'-' then null else REPLACE(REPLACE([������], N',', N'.'), CHAR(32), '') end
								,[�������_���������] = case when len([�������_���������]) = 0 or [�������_���������] = N'-' then null else [�������_���������] end
								,[����������_�����] = case when len([����������_�����]) = 0 or [����������_�����] = N'-' then null else [����������_�����] end
								,[�����_���] = case when len([�����_���]) = 0 or [�����_���] = N'-' then null else [�����_���] end
								--,[������_�������������] = case when len([������_�������������]) = 0 or [������_�������������] = N'-' then null else [������_�������������] end
								,[����_��������_�����] = case when year([����_��������_�����]) = 1900 then null else [����_��������_�����] end
								,[����_��������_�����] = case when year([����_��������_�����]) = 1900 then null else [����_��������_�����] end
								,[������������_���������] = case when len([������������_���������]) = 0 or [������������_���������] = N'-' then null else [������������_���������] end
								,[�������_�������������] = case when len([�������_�������������]) = 0 then '0' else REPLACE([�������_�������������], N',', N'.') end	--�������� ����
								--,[���������_��������] = case when len([���������_��������]) = 0 or [���������_��������] = N'-' then null else [���������_��������] end
								,[�����_������������_�������] = case when len([�����_������������_�������]) = 0 then '0' else REPLACE([�����_������������_�������], N',', N'.') end	--�������� ����
								,[���_��������] = case when len([���_��������]) = 0 or [���_��������] = N'-' then null else [���_��������] end
								,[���_���] = case when len([���_���]) = 0 or [���_���] = N'-' then null else [���_���] end
								--,[���_��������] = case when len([���_��������]) = 0 or [���_��������] = N'-' then null else [���_��������] end
								--,[���_��] = case when len([���_��]) = 0 or [���_��] = N'-' then null else [���_��] end
								--,[���_1�] = case when len([���_1�]) = 0 or [���_1�] = N'-' then null else [���_1�] end
								,[���_�������������] = case when len([���_�������������]) = 0 or [���_�������������] = N'-' then null else [���_�������������] end
								--,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
								--,[������������_�_���_��] = case when len([������������_�_���_��]) = 0 or [������������_�_���_��] = N'-' then null else [������������_�_���_��] end
								,[��_��������] = case when len([��_��������]) = 0 or [��_��������] = N'-' then null else [��_��������] end
								,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
								,[�������_���_�������������] = case when len([�������_���_�������������]) = 0 or [�������_���_�������������] = N'-' then null else [�������_���_�������������] end
								,[Front_Back1] = case when len([Front_Back1]) = 0 or [Front_Back1] = N'-' then null else [Front_Back1] end
								,[�������������_��������_��������] = case when len([�������������_��������_��������]) = 0 or [�������������_��������_��������] = N'-' then null else [�������������_��������_��������] end
								,[������_������] = case when len([������_������]) = 0 or [������_������] = N'-' then null else [������_������] end
								--,[�����_��_����_��_���������_�_�������_�����] = case when len([�����_��_����_��_���������_�_�������_�����]) = 0 or [�����_��_����_��_���������_�_�������_�����] = N'-' then null else [�����_��_����_��_���������_�_�������_�����] end
								--,[������������_������] = case when len([������������_������]) = 0 or [������������_������] = N'-' then null else [������������_������] end
								,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
								,[�������] = case when len([�������]) = 0 or [�������] = N'-' then null else [�������] end
								,[�_��������] = case when len([�_��������]) = 0 or [�_��������] = N'-' then null else [�_��������] end
								,[�����_������_�����_�������_�_��] = case when len([�����_������_�����_�������_�_��]) = 0 or [�����_������_�����_�������_�_��] = N'-' then null else [�����_������_�����_�������_�_��] end
								,[�����_������_�����] = case when len([�����_������_�����]) = 0 or [�����_������_�����] = N'-' then null else [�����_������_�����] end
								,[�����_������_���������] = case when len([�����_������_���������]) = 0 or [�����_������_���������] = N'-' then null else [�����_������_���������] end
								,[�����_������_Cash_in] = case when len([�����_������_Cash_in]) = 0 or [�����_������_Cash_in] = N'-' then null else [�����_������_Cash_in] end
								,[�������������_�������������_������] = case when len([�������������_�������������_������]) = 0 or [�������������_�������������_������] = N'-' then null else [�������������_�������������_������] end
								,[�������������_�������������_���������] = case when len([�������������_�������������_���������]) = 0 or [�������������_�������������_���������] = N'-' then null else [�������������_�������������_���������] end
								,[����_�������_������_������] = case when year([����_�������_������_������]) = 1900 then null else [����_�������_������_������] end
								,[���_��_����������_����������_�_��������_�����] = case when len([���_��_����������_����������_�_��������_�����]) = 0 then '0' else REPLACE([���_��_����������_����������_�_��������_�����], N',', N'.') end
								,[��������] = case when len([��������]) = 0 or [��������] = N'-' then null else [��������] end
								,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
								,[�������_�����] = case when len([�������_�����]) = 0 or [�������_�����] = N'-' then null else [�������_�����] end
								--,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
								--,[���_���������] = case when len([���_���������]) = 0 or [���_���������] = N'-' then null else [���_���������] end
								,[����_�������_���������_������] = case when year([����_�������_���������_������]) = 1900 then null else [����_�������_���������_������] end
								,[ID_��������] = case when len([ID_��������]) = 0 or [ID_��������] = N'-' then null else [ID_��������] end
								,[���_������_�����] = case when len([���_������_�����]) = 0 or [���_������_�����] = N'-' then null else [���_������_�����] end
								,[����_������_�������_�����] = case when year([����_������_�������_�����]) = 1900 then null else [����_������_�������_�����] end
								,[����_�������������_������_�����] = case when year([����_�������������_������_�����]) = 1900 then null else [����_�������������_������_�����] end
								--,[J_�����_���������] = case when len([J_�����_���������]) = 0 or [J_�����_���������] = N'-' then null else [J_�����_���������] end
								--,[�������������_��_��������_�����] = case when len([�������������_��_��������_�����]) = 0 or [�������������_��_��������_�����] = N'-' then null else [�������������_��_��������_�����] end
								--,[���_���] = case when len([���_���]) = 0 or [���_���] = N'-' then null else [���_���] end
								,[���_GE] = case when len([���_GE]) = 0 or [���_GE] = N'-' then null else [���_GE] end
								,[���������_�������_�����] = case when len([���������_�������_�����]) = 0 or [���������_�������_�����] = N'-' then null else [���������_�������_�����] end
								,[���������_���������_����������] = case when len([���������_���������_����������]) = 0 or [���������_���������_����������] = N'-' then null else [���������_���������_����������] end
								,[���������_��������_�������] = case when len([���������_��������_�������]) = 0 or [���������_��������_�������] = N'-' then null else [���������_��������_�������] end
								,[���������_��������_������] = case when len([���������_��������_������]) = 0 or [���������_��������_������] = N'-' then null else [���������_��������_������] end
								,[���������_�����_������] = case when len([���������_�����_������]) = 0 or [���������_�����_������] = N'-' then null else [���������_�����_������] end
								,[�������_��������_�_����������] = case when len([�������_��������_�_����������]) = 0 or [�������_��������_�_����������] = N'-' then null else [�������_��������_�_����������] end
								--,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
								--,[����������2] = case when len([����������2]) = 0 or [����������2] = N'-' then null else [����������2] end
								--,[��������_��_��������] = case when len([��������_��_��������]) = 0 or [��������_��_��������] = N'-' then null else [��������_��_��������] end
								--,[���_��_�_�������_��_��������] = case when len([���_��_�_�������_��_��������]) = 0 or [���_��_�_�������_��_��������] = N'-' then null else [���_��_�_�������_��_��������] end
								--,[ID_������_������_������_������] = case when len([ID_������_������_������_������]) = 0 or [ID_������_������_������_������] = N'-' then null else [ID_������_������_������_������] end
								--,[ID_��������_��������] = case when len([ID_��������_��������]) = 0 or [ID_��������_��������] = N'-' then null else [ID_��������_��������] end
								--,[����������_��������_������_������] = case when len([����������_��������_������_������]) = 0 or [����������_��������_������_������] = N'-' then null else [����������_��������_������_������] end
								,[ID_����������_��������_����������_��������] = case when len([ID_����������_��������_����������_��������]) = 0 or [ID_����������_��������_����������_��������] = N'-' then null else [ID_����������_��������_����������_��������] end
--								,[�����] = case when len([�����]) = 0 or [�����] = N'-' then null else [�����] end
--								,[�����] = case when len([�����]) = 0 or [�����] = N'-' then null else [�����] end


--UPDATE #TMP SET [������] = REPLACE([������], char(160), '')
--UPDATE #TMP SET [������] = SUBSTRING([������], 1, case when charindex('.', [������]) = 0 then 0 else (charindex('.', [������]) - 1) end) WHERE [������] is not null --[���_�������������] = '04-07-24-011'
--UPDATE #TMP SET [������] = SUBSTRING([������], 1, 6) 
--UPDATE #TMP SET [���_��_����������_����������_�_��������_�����] = null where [���_��_����������_����������_�_��������_�����] = '06.08.2014'
UPDATE #TMP SET [���_������_�����] = '1111110' WHERE [���_������_�����] = '111110'

/*
UPDATE #TMP SET [���������_�������_�����] = REPLACE([���������_�������_�����], char(32) + char(32), '')
UPDATE #TMP SET [���������_��������_������] = REPLACE([���������_��������_������], char(32) + char(32), '')
UPDATE #TMP SET [���������_�����_������] = REPLACE([���������_�����_������], char(32) + char(32), '')

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
*/


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

--����� �������� ��� ������������ ����� ��������
SELECT * INTO #TMP1_1 FROM #TMP1

--������� ��� �������� �������������
CREATE TABLE #TMP2 (id int, [Description] varchar(400))

--#####################################
--������� ������������� ������������� �� ������� ���� �� ������� ����
SELECT A.Id, A.ParentId, A.ItemLevel  INTO #TMP3
FROM Department as A
--INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
--INNER JOIN Fingrad_csv as C ON C.[���_�������������] = B.FinDepPointCode

--������ ������������� (�������� ����������)
SELECT * INTO #TMP4
FROM Users as A WHERE A.RoleId = 4 and A.IsMainManager = 1 and A.IsActive = 1
--��������� ����������
and not exists (SELECT * FROM ChildVacation WHERE getdate() between BeginDate and EndDate and UserId in (SELECT id FROM Users WHERE RoleId = 2 and IsActive = 1 and Email = A.Email)) 

SELECT distinct Id, UserId
INTO #TMP5
FROM(--�� ���������
			SELECT A.Id, A.ParentId, A.ItemLevel ,isnull(C.Id, isnull(E.id, isnull(G.id, isnull(I.id, K.Id)))) as UserId
			FROM #TMP3 as A
			INNER JOIN Department as B ON B.Code1C = A.ParentId and B.ItemLevel = 6
			LEFT JOIN #TMP4 as C ON C.DepartmentId = B.id and C.Level = 6
			INNER JOIN Department as D ON D.Code1C = B.ParentId and D.ItemLevel = 5
			LEFT JOIN #TMP4 as E ON E.DepartmentId = D.id and E.Level = 5
			INNER JOIN Department as F ON F.Code1C = D.ParentId and F.ItemLevel = 4
			LEFT JOIN #TMP4 as G ON G.DepartmentId = F.id and G.Level = 4
			INNER JOIN Department as H ON H.Code1C = F.ParentId and H.ItemLevel = 3
			LEFT JOIN #TMP4 as I ON I.DepartmentId = H.id and I.Level = 3
			INNER JOIN Department as J ON J.Code1C = H.ParentId and J.ItemLevel = 2
			LEFT JOIN #TMP4 as K ON K.DepartmentId = J.id and K.Level = 2
			WHERE isnull(C.Id, isnull(E.id, isnull(G.id, isnull(I.id, K.Id)))) is not null
			UNION ALL
			--�� ������ ���������
			SELECT A.Id, A.ParentId, A.ItemLevel 
							,isnull(B6.UserId, isnull(B5.UserId, isnull(B4.UserId, isnull(B3.UserId, B2.UserId)))) as UserId
			FROM #TMP3 as A
			INNER JOIN Department as B ON B.Code1C = A.ParentId and B.ItemLevel = 6
				LEFT JOIN ManualRoleRecord as B6 ON B6.TargetDepartmentId = B.Id
			LEFT JOIN #TMP4 as C ON C.DepartmentId = B.id and C.Level = 6
			INNER JOIN Department as D ON D.Code1C = B.ParentId and D.ItemLevel = 5
				LEFT JOIN ManualRoleRecord as B5 ON B5.TargetDepartmentId = D.Id
			LEFT JOIN #TMP4 as E ON E.DepartmentId = D.id and E.Level = 5
			INNER JOIN Department as F ON F.Code1C = D.ParentId and F.ItemLevel = 4
				LEFT JOIN ManualRoleRecord as B4 ON B4.TargetDepartmentId = F.Id
			LEFT JOIN #TMP4 as G ON G.DepartmentId = F.id and G.Level = 4
			INNER JOIN Department as H ON H.Code1C = F.ParentId and H.ItemLevel = 3
				LEFT JOIN ManualRoleRecord as B3 ON B3.TargetDepartmentId = H.Id
			LEFT JOIN #TMP4 as I ON I.DepartmentId = H.id and I.Level = 3
			INNER JOIN Department as J ON J.Code1C = H.ParentId and J.ItemLevel = 2
				LEFT JOIN ManualRoleRecord as B2 ON B2.TargetDepartmentId = J.Id
			LEFT JOIN #TMP4 as K ON K.DepartmentId = J.id and K.Level = 2
			WHERE isnull(C.Id, isnull(E.id, isnull(G.id, isnull(I.id, K.Id)))) is null) as A

--#####################################


--SELECT * FROM #TMP1
--delete FROM #TMP1 where rowid <> 18
--��������� ��������
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

INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed)
SELECT distinct 1, [Description], cast(0 as bit) FROM #TMP2 as A
WHERE NOT EXISTS (SELECT * FROM StaffDepartmentOperations WHERE Name = A.[Description])

--������ �������� �������� ������ � ����� ���������

--��������� ����� ��������
WHILE EXISTS(SELECT * FROM #TMP1_1)
BEGIN
	SELECT top 1 @RowId = RowId, @Oper = Operation FROM #tmp1_1 ORDER BY RowId

	INSERT INTO StaffDepartmentOperationGroups ([Version], Name, IsUsed) 
	SELECT  1, '������ ������� ' + cast(max(Id) + 1 as varchar), 0
	FROM StaffDepartmentOperationGroups

	SET @OperGroupId = @@IDENTITY

	UPDATE #TMP SET OperGroupId = @OperGroupId WHERE [��������] = @Oper

	SET @Id = 0
	WHILE EXISTS (SELECT * FROM StaffDepartmentOperations WHERE Id > @Id)
	BEGIN
		SELECT top 1 @Id = Id, @aa = Name FROM StaffDepartmentOperations WHERE Id > @Id ORDER BY Id

		IF @Oper like '%' + @aa + '%'
			INSERT INTO  StaffDepartmentOperationLinks([Version], OperGroupId, OperationId, IsUsed) VALUES(1, @OperGroupId, @Id, 1)

	END

	DELETE FROM #TMP1_1 WHERE RowId = @rowid
END


/*
	���������� �������� � ������� �������� ���������
--��������� ������� ���������� ����� �������������
--INSERT INTO StaffDepartmentTypes ([Version], Name)
--SELECT distinct 1, [���_�������������] FROM #TMP ORDER BY [���_�������������]
*/


	
--���� �� ���������� ������
WHILE EXISTS(SELECT * FROM #TMP)
BEGIN
	SELECT top 1 @Id = Id FROM #TMP

	print 'Id ��������������� ������������� ' + cast(@Id as varchar)

	--���������� ������������ ��� �������������
	--��� ��� �� ����� ���� ���������, �� ����� �� ������ �����������
	SELECT top 1 @CreatorId = UserId FROM #TMP5 as A
	INNER JOIN Users as B ON B.Id = A.UserId
	WHERE A.Id = @Id
	ORDER BY B.Level desc

	SET @CreatorId = isnull(@CreatorId, 5)	--������, ��� �� ��������� ������������

	--��� ������ ������ �����������
	--������� �����
	INSERT INTO RefAddresses([Version]
													,[Address]
													,PostIndex
													,RegionName
													,CityName
													,SettlementName
													,StreetName
													,CreatorId)
	SELECT 1, isnull([������], '') + case when len(isnull([������], '')) = 0 then '' else ', ' end + [�������_���������] + ', ' + [����������_�����] + ', ' + [�����_���]
				,[������]
				,[�������_���������]
				,case when [����������_�����] like '�. %' then [����������_�����] else null end
				,case when [����������_�����] not like '�. %' then [����������_�����] else null end
				,substring([�����_���], 1, 50)
				,@CreatorId 
	FROM #TMP WHERE Id = @Id

	SET @LegalAddressId = @@IDENTITY

	INSERT INTO RefAddresses([Version]
													,[Address]
													,PostIndex
													,RegionName
													,CityName
													,SettlementName
													,StreetName
													,CreatorId)
	SELECT 1, isnull([������], '') + case when len(isnull([������], '')) = 0 then '' else ', ' end + [�������_���������] + ', ' + [����������_�����] + ', ' + [�����_���]
				,[������]
				,[�������_���������]
				,case when [����������_�����] like '�. %' then [����������_�����] else null end
				,case when [����������_�����] not like '�. %' then [����������_�����] else null end
				,substring([�����_���], 1, 50)
				,@CreatorId 
	FROM #TMP WHERE Id = @Id

	SET @FactAddressId = @@IDENTITY


	--������� � ������
	INSERT INTO StaffDepartmentRequest ([Version]
																			,DateRequest
																			,RequestTypeId
																			,DepartmentId
																			,ItemLevel
																			,ParentId
																			,Name
																			,BFGId
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
																			,DateState
																			,CreatorId)
	SELECT 1
					,'20151031'--A.[����_���������]
					,4--���� ��������� ������
					,@Id
					,B.ItemLevel
					,C.Id
					,B.Name
					,B.BFGId
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
					,getdate()
					,getdate()
					,@CreatorId
	FROM #TMP as A
	INNER JOIN Department as B ON B.Id = A.Id
	INNER JOIN Department as C ON C.Code1C = B.ParentId
	WHERE A.Id = @Id
	

	SET @DepRequestId = @@IDENTITY

	--������� �� ���������
	INSERT INTO StaffDepartmentCBDetails (Version
																				,DepRequestId
																				,ATMCountTotal
																				,ATMCashInCount
																				,ATMCount
																				,ATMCashInStarted
																				,DepCachinId
																				,DepATMId
																				,CashInStartedDate
																				,ATMStartedDate
																				,CreatorId)
	SELECT 1
					,@DepRequestId
					,A.[���_��_����������_����������_�����]
					,A.[���_��_����������_�������] 
					,A.[���_��_����������_����������_�_��������_����������]
					,A.[���_��_����������_����������_�_��������_�����]
					,null
					,null
					,A.[����_�������_������_������]
					,A.[����_�������_���������_������]
					,@CreatorId
	FROM #TMP as A
	WHERE A.Id = @Id



	--������� �������������� ���������
	INSERT INTO StaffDepartmentManagerDetails([Version]
																						,DepRequestId
																						,DepCode
																						,NameShort
																						,NameComment
																						,ReasonId
																						,PrevDepCode
																						,FactAddressId
																						,DepStatus
																						,DepTypeId
																						,OpenDate
																						,CloseDate
																						,OperationMode
																						,OperationModeCash
																						,OperationModeATM
																						,OperationModeCashIn
																						,BeginIdleDate
																						,EndIdleDate
																						,RentPlaceId
																						,AgreementDetails
																						,DivisionArea
																						,AmountPayment
																						,Phone
																						,IsBlocked
																						,NetShopId
																						,IsLegalEntity
																						,PlanEPCount
																						,PlanSalaryFund
																						,Note
																						,CDAvailableId
																						,SKB_GE_Id
																						,SoftGroupId
																						,OperGroupId
																						,CreatorId)
	SELECT 1
					,@DepRequestId
					,A.[���_�������������]
					,A.[�����������_������������]
					,A.FinDepNameShort
					,C.Id
					,A.[�������_���_�������������]
					,@FactAddressId		--�����
					,null --A.[������_�������������]
					,B.Id			--���� �������������
					,A.[����_��������_�����]
					,A.[����_��������_�����]
					,A.[�����_������_�����_�������_�_��]
					,A.[�����_������_�����]
					,A.[�����_������_���������]
					,A.[�����_������_Cash_in]
					,A.[����_������_�������_�����]
					,A.[����_�������������_������_�����]
					,F.Id
					,null--A.[���������_��������]
					,A.[�������_�������������]
					,A.[�����_������������_�������]
					,A.[�_��������]
					,case when A.[����������] = '���������' then 0 else 1 end
					,D.Id
					,case when A.[������������_��] = '��' then 1 else 0 end
					,0
					,0
					,null --A.[����������]
					,E.Id
					,G.Id
					,case when A.[�������������_��_�_���] = N'*' then 1
								when A.[�������������_��_�_���] = N'=' then 2
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���-������ ��%%���%%��������%%������� ������� (��, ��,��)' then 15
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���-������ ��%%���%%��������%' then 14
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���-������ ��%%���%%������� ������� (��, ��,��)' then 13
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���-������ ��%%���%' then 12
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���-������ ��%%������� ������� (��, ��,��)' then 11
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���-������ ��%' then 10
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���%%��������%%������� ������� (��, ��,��)' then 9
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���%%��������%' then 8
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���%%������� ������� (��, ��,��)' then 7
								when A.[�������������_��_�_���] like  N'���-��� ��������%%���%' then 6
								when A.[�������������_��_�_���] like  N'���-��� ��������%%������� ������� (��, ��,��)' then 5
								when A.[�������������_��_�_���] =  N'���-��� ��������; ; ; ;' then 4
								when A.[�������������_��_�_���] =  N'; ���-������ ��; ; ;' then 3
					end as SoftGroupId
					,A.OperGroupId
					,@CreatorId
	FROM #TMP as A
	LEFT JOIN StaffDepartmentTypes as B ON B.Name = A.[���_�������������]
	LEFT JOIN StaffDepartmentReasons as C ON C.Name = A.[�������_��������_�_����������]
	LEFT JOIN StaffNetShopIdentification as D ON D.Name = A.[�������������_��������_��������]
	LEFT JOIN StaffDepartmentCashDeskAvailable as E ON E.Name = A.[�������_�����]
	LEFT JOIN StaffDepartmentRentPlace as F ON F.Name = A.[������������_���������]
	LEFT JOIN StaffDepartmentSKB_GE as G ON G.Name = A.[���_GE]
	WHERE A.Id = @Id

	SET @DMDetailId = @@IDENTITY


		--��������� ���� ������������� ��������
		IF (SELECT [���_��������] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 1, [���_��������], @CreatorId FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [���_���] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 2, [���_���], @CreatorId FROM #TMP WHERE Id = @Id
		END
		/*
		IF (SELECT [���_��������] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 3, [���_��������], @CreatorId FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [���_��] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 4, [���_��], @CreatorId FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [���_���������] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 5, [���_���������], @CreatorId FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [���_���] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 6, [���_���], @CreatorId FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [���_GE] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 7, [���_GE], @CreatorId FROM #TMP WHERE Id = @Id
		END
		*/
		
		
		--��������� ����� ������ ������
		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 1, [���������_�������_�����], @CreatorId
		FROM #TMP WHERE Id = @Id and [���������_�������_�����] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 2, [���������_���������_����������], @CreatorId 
		FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 3, [���������_��������_�������], @CreatorId 
		FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 4, [���������_��������_������], @CreatorId 
		FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 5, [���������_�����_������], @CreatorId 
		FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null

		/*
		������ ������
		--��������� (���������� ������� ������� � ������ � �������� ��)		
		--�� ������ ��������� � ������ ����� null-�� ���
		IF (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) is null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 2, [���������_���������_����������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 3, [���������_��������_�������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 4, [���������_��������_������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 5, [���������_�����_������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null
		END
		
		--�� ������ ��������� null-� ���� �� ���� �����
		IF (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) = '���'
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 2, [���������_���������_����������], @CreatorId
			FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 3, [���������_��������_�������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 4, [���������_��������_������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 5, [���������_�����_������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null
		END

		IF (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) <> '���' and (SELECT [���������_�������_�����] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 1, [���������_�������_�����], @CreatorId
			FROM #TMP WHERE Id = @Id and [���������_�������_�����] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 2, [���������_���������_����������], @CreatorId
			FROM #TMP WHERE Id = @Id and [���������_���������_����������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 3, [���������_��������_�������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_��������_�������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 4, [���������_��������_������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_��������_������] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 5, [���������_�����_������], @CreatorId 
			FROM #TMP WHERE Id = @Id and [���������_�����_������] is not null
		END
		*/


		--����� ������ �������������
		SELECT @WorkDays = [���_������_�����] FROM #TMP WHERE Id = @Id
		IF @WorkDays is not null
		BEGIN
			SET @i = 1
			WHILE @i < 8
			BEGIN
				--�������������
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 1, @i, cast(SUBSTRING(@WorkDays, @i, 1) as bit), @CreatorId)

				--�����
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 2, @i, 0, @CreatorId)
				--��������
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 3, @i, 0, @CreatorId)
				--�����
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 4, @i, 0, @CreatorId)
				SET @i += 1
			END	
		END

		/*
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
					INSERT INTO StaffDepartmentOperationLinks ([Version], DMDetailId, OperationId, CreatorId)
					VALUES(1, @DMDetailId, @i, @CreatorId)
				END

				SET @RowId += 1
			END
		END
		*/

	DELETE FROM #TMP WHERE Id = @Id
END


--� ��������� ����� �������� ����������, �� ����� � ��� � ��������� ����� ����� �����
--����� ������� ��� ���������� ���
SET @i = 1
--������� ������������ ��� ������ ��� ��������� � 1 �� 7
WHILE @i < 8
BEGIN
	--������ ���������
	UPDATE Department SET BFGId = 5
	WHERE (Name like '%����������%' or Name like '%������%' or Name like '%�� ���%' or Name like '%�������%') 
	and isnull(BFGId, 0) not in (4, 5) and ItemLevel = @i

	--��������� ���������� �� �������� �� ������ ����
	UPDATE Department SET BFGId = 5
	FROM Department as A
	INNER JOIN (SELECT * FROM Department 
							WHERE (Name like '%����������%' or Name like '%������%' or Name like '%�� ���%' or Name like '%�������%') 
							and isnull(BFGId, 0) = 5 
							and ItemLevel = @i) as B ON A.Path like B.Path + '%'
	

	SET @i += 1
END


--����� �������� ������������ ��� ����� 7 ������ � ������� ����� � ��� � �� ��������� �� ������ �����
--�� ���� �������� ����� � �� ���������, ���������� � ��������, ����:
	--� ��� ��������� ���������� 
	--���� ����� � ������ �� ��������
--�������� � ������� ������ �� �����
SELECT A.*,
			 case when exists (SELECT * FROM Users WHERE DepartmentId = A.Id and IsActive = 1 and (RoleId & 2 > 0)) then 1 else 0 end as IsPeople,
			 case when A.FingradCode is not null then 1 else 0 end as IsFin
			 INTO #DelDep
FROM Department as A
WHERE --A.BFGId = 5 
			(A.Name like '%����������%' or A.Name like '%������%' or A.Name like '%�� ���%' or A.Name like '%�������%' )
			and A.ItemLevel = 7
			and isnull(A.BFGId, 0) <> 4	--�� ��� �� �������� ���������


--��� 7
UPDATE Department SET BFGId = null
FROM Department as A
INNER JOIN (select * from #DelDep WHERE IsPeople = 1 or IsFin = 1) as B ON B.Id = A.Id 
WHERE isnull(A.BFGId, 0) <> 4

--��� ��� ���������
UPDATE Department SET BFGId = null
FROM Department as A
INNER JOIN (SELECT distinct A.*
						FROM Department as A
						INNER JOIN (select * from #DelDep WHERE IsPeople = 1 or IsFin = 1) as B ON B.Path like A.Path + '%'
						WHERE A.ItemLevel < 7) as B ON B.Id = A.Id
WHERE isnull(A.BFGId, 0) <> 4



--��������� ������� ������������� �������������
UPDATE Department SET IsUsed = case when isnull(BFGId, 1) <> 5 then 1 else 0 end



--������� ��������� ������ ��� ��� ������������ ������������� ������ ������� � �� ��������� � ���������
INSERT INTO StaffDepartmentRequest ([Version]
																			,DateRequest
																			,RequestTypeId
																			,DepartmentId
																			,ItemLevel
																			,ParentId
																			,Name
																			,BFGId
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
																			,DateState
																			,CreatorId)
	SELECT 1
					,'20151031'--A.[����_���������]
					,4--���� ��������� ������
					,A.Id
					,A.ItemLevel
					,B.Id
					,A.Name
					,A.BFGId
					,null
					,null
					,null	--�����
					,case when A.ItemLevel in (1, 2) then 1 when A.ItemLevel in (3, 4, 5, 6) then 0 else (case when exists (SELECT * FROM Users WHERE DepartmentId = A.Id and IsActive = 1 and (RoleId & 2 > 0)) then 1 else 0 end) end
					,case when A.ItemLevel < 7 then 0 else (case when exists (SELECT * FROM Users WHERE DepartmentId = A.Id and IsActive = 1 and (RoleId & 2 > 0)) then 1 else 0 end) end
					,null
					,0
					,1
					,0
					,null
					,getdate()
					,getdate()
					,(SELECT top 1 UserId FROM #TMP5 WHERE Id = A.Id)--C.UserId
	FROM (SELECT * FROM Department as A
				WHERE isnull(A.BFGId, 1) not in (3, 4, 5) and A.FingradCode is null) as A
	LEFT JOIN Department as B ON B.Code1C = A.ParentId
	--LEFT JOIN #TMP5 as C ON C.Id = A.Id
	ORDER BY A.ItemLevel



--����������� ������������� ������������� � ������ (����� ��� ������� ������ ����� ����� ������������ ��������)
UPDATE StaffDepartmentCBDetails SET DepCachinId = E.DepartmentId, DepATMId = F.DepartmentId
FROM StaffDepartmentCBDetails as A
INNER JOIN StaffDepartmentRequest as B ON B.Id = A.DepRequestId and B.IsDraft = 0
INNER JOIN Department as C ON C.id = B.DepartmentId and C.FingradCode is not null
INNER JOIN Fingrad_csv as D ON D.[���_�������������] = C.FingradCode and (D.[�������������_�������������_������] is not null or D.[�������������_�������������_���������] is not null)
LEFT JOIN StaffDepartmentRPLink as E ON E.Code = D.[�������������_�������������_������_���_��_�_�������]
LEFT JOIN StaffDepartmentRPLink as F ON F.Code = D.[�������������_�������������_���������_���_��_�_�������]


drop table #TMP
drop table #TMP1
drop table #TMP1_1
drop table #TMP2
drop table #TMP3
drop table #TMP4
drop table #TMP5
drop table #DelDep

