--������� ������ ��� ��������� ������ � ����������� �����
select * from HelpServiceType order by SortOrder

--����������� ������
UPDATE HelpServiceType SET Name = N'������� � ����� ������ � ��������� ��������� (��� �������� ������)' WHERE Id = 3

--��������� ����� ������ ����� �� ������� ������ � id = 3, � ������ �� ������� ������ � id = 1
INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'��������� �� ��������� ����� (�����������) � ������� �������������� ����������' as Name, 7 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� ��� ������ ���������' as Name, 8 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ����� ������ � ��������� ��������� � ������' as Name, 9 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ������� ��������� �� ��������� 3 ������ ������' as Name, 10 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ������� ��������� �� ��������� 6 ������� ������' as Name, 11 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� ��� ���� � ����� ������' as Name, 12 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� �� ��������� �������' as Name, 13 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, 0 FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'����� ���������� ��� �������' as Name, 14 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, 0 FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'����� ���������� ��� ���������� ������� �� ������� ���������� �����' as Name, 15 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ����� ������ �� ������� �������' as Name, 16 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ��������� ��������������� ������� ��� �������� �������' as Name, 17 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 1

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ������������ � ������� �� ����� �� �������� �� 1,5 ��� � � ������������ ������������ ������� �� 1,5 ���' as Name, 18 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 1

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������� � ������������ � ������� �� ����� �� �������� �� 3 ��� � � ������������ ������������ ������� �� 3 ���' as Name, 19 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 1

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'������������ ������' as Name, 20 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

--������ ���������� �� ��������
UPDATE HelpServiceType SET SortOrder = 0

DECLARE @ID int, @i int
SET @i = 1
WHILE EXISTS (SELECT * FROM HelpServiceType WHERE SortOrder = 0)
BEGIN
	SET @ID = (SELECT top 1 Id FROM HelpServiceType WHERE SortOrder = 0 ORDER BY Name)
	UPDATE HelpServiceType SET SortOrder = @i WHERE id = @ID
	SET @i = @i + 1
END
--��� ��������
select * from HelpServiceType order by SortOrder


--� ����������� �������� �������� (HelpServiceTransferMethod) ����������� ������
UPDATE HelpServiceTransferMethod SET Name = N'����������' WHERE Id = 1

