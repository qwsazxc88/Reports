--��������� � ������ "����������"
use WebAppTest2
go

ALTER TABLE [dbo].[HelpServiceRequest] ADD [Address] nvarchar(200) null;
go

UPDATE [dbo].[HelpQuestionType] SET [Name] = '������ ���������' WHERE [Id] = 2;
UPDATE [dbo].[HelpQuestionType] SET [Name] = '������������ �� ��������� ���' WHERE [Id] = 3;

INSERT INTO [dbo].[HelpQuestionSubtype](Code, Name, SortOrder, TypeId)
VALUES(NULL, '����������� �� ��������� ��������� "Web-������"', 5, 3)