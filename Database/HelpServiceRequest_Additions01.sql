--изменения к пункту "ИнфоУслуги"
use WebAppTest2
go

ALTER TABLE [dbo].[HelpServiceRequest] ADD [Address] nvarchar(200) null;
go

UPDATE [dbo].[HelpQuestionType] SET [Name] = 'Вопрос кадровику' WHERE [Id] = 2;
UPDATE [dbo].[HelpQuestionType] SET [Name] = 'Техподдержка по программе Вэб' WHERE [Id] = 3;