--скрип меняет названия ролей и добавляет одну новую роль
UPDATE dbo.Role SET Name = 'Консультант Кадры Банк' WHERE Id = 262144 --было 'Консультант кадры'
UPDATE dbo.Role SET Name = 'Консультант Бухгалтер Банк' WHERE Id = 524288 --было 'Консультант бухгалтер'
GO

SET IDENTITY_INSERT dbo.Role ON
GO
INSERT INTO dbo.Role (Id, Version, Name) VALUES (1048576, 1, 'Консультант Аутсорсинг Кадры')
GO
SET IDENTITY_INSERT dbo.Role OFF
GO

