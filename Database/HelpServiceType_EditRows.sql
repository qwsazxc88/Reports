--РАЗОВЫЙ СКРИПТ ДЛЯ ИЗМЕНЕНИЯ ДАННЫХ В СПРАВОЧНИКЕ УСЛУГ
select * from HelpServiceType order by SortOrder

--редактируем записи
UPDATE HelpServiceType SET Name = N'Справка с места работы с указанием должности (без указания оклада)' WHERE Id = 3

--добавляем новые строки часть по шаблону строки с id = 3, а другая по шаблону записи с id = 1
INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Заявление на налоговый вычет (стандартный) с пакетом подтверждающих документов' as Name, 7 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка для центра занятости' as Name, 8 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка с места работы с указанием должности и оклада' as Name, 9 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка о среднем заработке за последние 3 месяца работы' as Name, 10 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка о среднем заработке за последние 6 месяцев работы' as Name, 11 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка для визы с места работы' as Name, 12 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Выписка из кадрового приказа' as Name, 13 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, 0 FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Пакет документов для кассира' as Name, 14 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, 0 FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Пакет документов для оформления пособия на ребенка сотруднику Банка' as Name, 15 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка с места работы по образцу Клиента' as Name, 16 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка о невыплате единовременного пособия при рождении ребенка' as Name, 17 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 1

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка о ненахождении в отпуске по уходу за ребенком до 1,5 лет и о неначислении ежемесячного пособия до 1,5 лет' as Name, 18 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 1

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Справка о ненахождении в отпуске по уходу за ребенком до 3 лет и о неначислении ежемесячного пособия до 3 лет' as Name, 19 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 1

INSERT INTO HelpServiceType (Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable)
SELECT N'Материальная помощь' as Name, 20 as SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable FROM HelpServiceType WHERE Id = 3

--меняем сортировку по алфавиту
UPDATE HelpServiceType SET SortOrder = 0

DECLARE @ID int, @i int
SET @i = 1
WHILE EXISTS (SELECT * FROM HelpServiceType WHERE SortOrder = 0)
BEGIN
	SET @ID = (SELECT top 1 Id FROM HelpServiceType WHERE SortOrder = 0 ORDER BY Name)
	UPDATE HelpServiceType SET SortOrder = @i WHERE id = @ID
	SET @i = @i + 1
END
--для проверки
select * from HelpServiceType order by SortOrder


--в справочнике способов передачи (HelpServiceTransferMethod) редактируем записи
UPDATE HelpServiceTransferMethod SET Name = N'ИнфоУслуги' WHERE Id = 1

