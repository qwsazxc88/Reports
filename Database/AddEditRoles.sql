--����� ������ �������� ����� � ��������� ���� ����� ����
UPDATE dbo.Role SET Name = '����������� ����� ����' WHERE Id = 262144 --���� '����������� �����'
UPDATE dbo.Role SET Name = '����������� ��������� ����' WHERE Id = 524288 --���� '����������� ���������'
GO

SET IDENTITY_INSERT dbo.Role ON
GO
INSERT INTO dbo.Role (Id, Version, Name) VALUES (1048576, 1, '����������� ���������� �����')
GO
SET IDENTITY_INSERT dbo.Role OFF
GO

