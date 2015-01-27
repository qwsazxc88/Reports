--�������� ������ ����������� ����������
--������� ���������� ���������� �� ������ ��������
--������ ������ ���� �� �������, ������ ������� 
--� ������� ��������� ��������� 3 ����� ����
	--PayreID - ���������� (��������� ��� ���� ����� �������� �����)
	--PayeeID - ���������� (��������� ���� �������  �������� �����������, �������� �����������, ������� ����)
	--AccountID - ������� ����
--��� ��� ���� ��������� �� ����� ���������� ����������

set nocount on
/*
ALTER TABLE dbo.gpdcontract ADD column_b VARCHAR(20) NULL 
go
*/
CREATE TABLE #GpdRefDetailTmp(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	DSID int null,	
	DetailID int null,
	--[Version] [int] NOT NULL CONSTRAINT [DF_GpdRefDetail_Version]  DEFAULT ((1)),
	--[CreateDate] [datetime] NULL CONSTRAINT [DF_GpdRefDetail_CreateDate]  DEFAULT (getdate()),
	--[CreatorID] [int] NULL,
	--[EditDate] [datetime] NULL,
	--[EditorID] [int] NULL,
	--[DTID] [int] NULL,
	PersonID int,
	SetName [nvarchar](150) NULL,					--�������  �������� �����������
	[ContractorName] [nvarchar](150) null,						--������������ �����������
	[INN] [nvarchar](12) NULL,						--���
	[KPP] [nvarchar](9) NULL,							--���
	[Account] [nvarchar](23) NULL,				--���������  ����
	[BankName] [nvarchar](100) NULL,			--����
	[BankBIK] [nvarchar](9) NULL,					--���
	[CorrAccount] [nvarchar](23) NULL,		--���. ����
	[PersonAccount] [nvarchar](23) NULL)--������� ����

--����� ������ ����������
SELECT A.Id as DSID, B.Id as DetailID, C.Id as PersonID, A.name as SetName, C.LastName + ' ' + C.FirstName + ' ' + C.SecondName as PersonName, B.INN, B.KPP, B.Account, A.Account as PersonAccount, 
			 B.BankName, B.BankBIK, B.CorrAccount
INTO #TMP
FROM GpdDetailSets as A
INNER JOIN GpdRefDetail as B ON B.Id = A.PayeeID
INNER JOIN RefPeople as C ON C.Id = A.PersonID 
WHERE b.DTID = 1
ORDER BY A.id


--����� ������ ������, ����� ����� �� ������� ��
UPDATE GpdRefDetail SET OldRecord = 1


DECLARE 
	@ID int,
	@RowID int,
	@DSID int,
	@DetailID int, 
	@SetName nvarchar(250),
	@PersonName nvarchar(250),
	@INN nvarchar(12),
	@KPP nvarchar(9),
	@Account nvarchar(23), 
	@PersonAccount nvarchar(23),
	@BankName nvarchar(100),
	@BankBIK nvarchar(9),
	@CorrAccount nvarchar(23),
	@PersonID int


WHILE EXISTS(SELECT * FROM #TMP)
BEGIN
	SELECT top 1 @DSID = DSID, @DetailID = DetailID, @PersonID = PersonID, @SetName = SetName, @PersonName = PersonName, @INN = INN, @KPP = KPP, @Account = Account, @PersonAccount = PersonAccount, @BankName = BankName,
							 @BankBIK = BankBIK, @CorrAccount = CorrAccount
	FROM #TMP
	--���� ��������� ���� ����� ��������, �� ��������� 1 ������
	IF @Account = @PersonAccount
	BEGIN
		INSERT INTO #GpdRefDetailTmp (DSID, DetailID, PersonID, SetName, ContractorName, INN, KPP, Account, BankName, BankBIK, CorrAccount)
		SELECT DSID, DetailID, PersonID, SetName, isnull(PersonName, SetName), INN, KPP, Account, BankName, BankBIK, CorrAccount FROM #TMP WHERE DSID = @DSID
	END
	ELSE	--����� ��������� 2 ������ ��� ����� � �������� �����
	BEGIN
		--����, �� ������ �����������
		IF NOT EXISTS (SELECT * FROM #GpdRefDetailTmp WHERE BankName = @BankName and BankBIK = @BankBIK and Account = @Account)
		BEGIN
			INSERT INTO #GpdRefDetailTmp (DSID, DetailID, PersonID, SetName, ContractorName, INN, KPP, Account, BankName, BankBIK, CorrAccount)
			VALUES (@DSID, @DetailID, @PersonID, @BankName, @BankName, @INN, @KPP, @Account, @BankName, @BankBIK, @CorrAccount)
		END
		--������� ����, �� ������ �����������
		IF NOT EXISTS (SELECT * FROM #GpdRefDetailTmp WHERE PersonID = @PersonID and PersonAccount = @PersonAccount)
		BEGIN
			INSERT INTO #GpdRefDetailTmp (DSID, DetailID, PersonID, SetName, ContractorName, PersonAccount)
			VALUES (@DSID, @DetailID, @PersonID, @SetName, isnull(@PersonName, @SetName), @PersonAccount)
		END
	END

	DELETE FROM #TMP WHERE DSID = @DSID
END



--�������� �� ID ������ �������� ����� ��������� � ��������
--��������� ��������� �������� � ��������� ��� � ��������� �� ������� ID ������
--�������������� ������ ���������� � �� ������� �����

WHILE EXISTS (SELECT * FROM #GpdRefDetailTmp)
BEGIN
	SELECT top 1 @RowID = ID, @DSID = DSID, @DetailID = DetailID, @PersonAccount = PersonAccount FROM #GpdRefDetailTmp

	--������� ���������� ������ � ���������� ����������
	INSERT INTO GpdRefDetail([Version], Name, ContractorName, INN, KPP, Account, BankName, BankBIK, CorrAccount, PersonAccount, OldRecord)
	SELECT 1, SetName, ContractorName, INN, KPP, Account, BankName, BankBIK, CorrAccount, PersonAccount, 0 FROM #GpdRefDetailTmp WHERE ID = @RowID

	SET @ID = @@IDENTITY

	--���� ��� �������� ��� �������� �����, �� ������ � ���������� ����� � ���������� ���������
	IF @PersonAccount is null
	BEGIN
		UPDATE GpdContract SET PayeeID = case when @PersonAccount is null then @ID else A.PayeeID end 
													 --PAccountID  = case when @PersonAccount is not null then @ID else A.PAccountID end
		FROM GpdContract as A
		INNER JOIN GpdDetailSets as B ON B.ID = A.DSID and B.PayeeID = @DetailID
	END
	ELSE
	BEGIN
		UPDATE GpdContract SET --PayeeID = case when @PersonAccount is null then @ID else A.PayeeID end, 
													 PAccountID  = case when @PersonAccount is not null then @ID else A.PAccountID end
		FROM GpdContract as A
		WHERE A.DSID = @DSID
	END

	DELETE FROM #GpdRefDetailTmp WHERE Id = @RowID
END


--����������� ����������� �� ������ ������� ���������
UPDATE GpdContract SET PayerID = B.PayerID
FROM GpdContract as A
INNER JOIN GpdDetailSets as B ON B.id = A.DSID


--��������� ����� ���� � ������������
UPDATE GpdRefDetail SET ContractorName = name where DTID = 2


SELECT * FROM #GpdRefDetailTmp 
--where dsid = 596
ORDER BY DetailID, ContractorName

--select * from GpdRefDetail where id = 309

--11798
--7452
--DROP TABLE #TMP
--DROP TABLE #GpdRefDetailTmp


/*
update GpdContract set PayerID = null, PayeeID = null, PAccountID = null

delete from GpdRefDetail where OldRecord = 0
*/