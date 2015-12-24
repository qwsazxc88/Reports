--СКРИПТ ОБРАБОТКИ И ЗАКАЧКИ ДАННЫХ ИЗ ФИНГРАДА В БАЗУ ДАННЫХ 
--ЕСЛИ ОБНОВИЛАСЬ БАЗА ИЛИ СПРАВОЧНИК ТОЧЕК ДЛЯ ГРАФИКОВ, СПРАВОЧНИК ПОДРАЗДЕЛЕНИЙ, ТО ИЗ СПРАВОЧНИКА ПОДРАЗДЕЛЕНИЙ ВОЗМОЖНО НУЖНО УДАЛИТЬ ЗАПИСИ С ЗНАЧЕНИЕМ 4 В ПОЛЕ BFGId

use WebAppTest
go

SET NOCOUNT ON

--return

--проверка на задвоенность точек после сопоставления
select PossibleDepartmentId, count(PossibleDepartmentId) as cnt 
into #checkpoint
from TerraPoint where PossibleDepartmentId is not null and ItemLevel = 3 and (EndDate is null or (EndDate is not null and DATEDIFF(dd, EndDate, getdate()) <= 30))
group by PossibleDepartmentId
having count(PossibleDepartmentId) > 1


if exists(select * from #checkpoint)
begin
	print 'задвоенные записи в террасофте'

	select a.Id, c.Name, a.Name, c.City, c.Street, c.House, a.City, a.Street  from TerraPoint as a
	inner join #checkpoint as b on b.PossibleDepartmentId = a.PossibleDepartmentId
	inner join Department as c on c.id = a.PossibleDepartmentId
	order by c.Name

	drop table #checkpoint
	--update TerraPoint set PossibleDepartmentId = null where id in (1241, 3995, 1823, 2409, 227)
	--update TerraPoint set PossibleDepartmentId = null where name like '%(закрыто)%' or id in (3499, 227)
	return
end
else
	drop table #checkpoint

--конец проверки


DECLARE @Id int, @DepRequestId int, @LegalAddressId int, @FactAddressId int, @DMDetailId int, @WorkDays varchar(7), 
				@aa varchar(5000), @bb varchar(5000), @len int, @i int, @RowId int, @Oper varchar(max), @CreatorId int,
				@DepartmentID int, @DepTmpId int, @DepNewId int, @Code varchar(15), @OperGroupId int

--удаляем искуственно созданные записи, которые не удалось связать (пока у добавляемых записей будет проставляться признак 4, потом может изменится)
DELETE DepartmentArchive
FROM DepartmentArchive as A
INNER JOIN Department as B on B.Id = A.DepartmentId and B.BFGId = 4

DELETE Department WHERE BFGId = 4	

--убираем у всех записей 7 уровня коды из Финграда, чтобы при обработке проставить их заново
UPDATE Department SET FingradCode = null

--анализируем и формируем данные для закачки в структуру штатного расписания
--берем данные из графиков открытые и закрытые точки, дата закрытия не старше 30 дней относительно текущей даты
--если точка связана с нашей структурой подразделения, то в наш справочник проставляем ее код
UPDATE Department SET FingradCode = B.Code
FROM Department as A
INNER JOIN TerraPoint as B ON B.PossibleDepartmentId = A.Id and B.ItemLevel = 3 and (B.EndDate is null or (B.EndDate is not null and DATEDIFF(dd, B.EndDate, getdate()) <= 30)) and isnull(A.ParentId, '') <> ''
															and not exists (select * from DepFinRP where [код рп в финград] = B.Code)
INNER JOIN Fingrad_csv as C ON C.[Код_подразделения] = B.Code
WHERE A.ItemLevel = 7

--если точка несвязана с нашей структурой подразделений, но есть связь определенная по графикам
--в таких случаях определяем ветку подразделения из графика и в эту же ветку добавляем новую запись (наш справочник подразделений)
SELECT A.* INTO #TPLink FROM TerraPoint as A 
INNER JOIN Fingrad_csv as B ON B.[Код_подразделения] = A.Code
--INNER JOIN Department as C ON C.Id = A.PossibleDepartmentId and C.ItemLevel = 7
WHERE A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, A.EndDate, getdate()) <= 30)) and isnull(A.ParentId, '') <> ''
			and A.PossibleDepartmentId is null
			--исключил рп-привязки
			and not exists (SELECT * FROM DepFinRP WHERE [код рп в финград] = A.Code)
			and A.ParentDepartmentId is not null

--цикл по таким записям
WHILE EXISTS (SELECT * FROM #TPLink)
BEGIN
	SELECT top 1 @id = A.Id FROM #TPLink as A
		--находим родительское подразделение 6 уровня и добавляем запись в наеденного родителя
	INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
	SELECT 1, null, A.Name, null, C.Code1C, C.Path + N'1', 7, null, 99, 1, 4, A.Code
	FROM #TPLink as A
	INNER JOIN Department as B ON B.Id = A.ParentDepartmentId
	INNER JOIN Department as C ON C.Code1C = B.ParentId
	WHERE A.Id = @Id

	SET @DepNewId = @@IDENTITY
	
	--достраиваем путь 
	UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
	WHERE Id = @DepNewId
	
	
	DELETE FROM #TPLink WHERE Id = @Id
END

print 'Закончена обработка подразделений связанных по косвенным признакам'



--если точку не удалось связать вообще
--создаем ветку до 7 уровня и засовываем туда это говно/добро
INSERT INTO #TPLink
SELECT A.* FROM TerraPoint as A 
INNER JOIN Fingrad_csv as B ON B.[Код_подразделения] = A.Code
--INNER JOIN Department as C ON C.Id = A.PossibleDepartmentId and C.ItemLevel = 7
WHERE A.ItemLevel = 3 and (A.EndDate is null or (A.EndDate is not null and DATEDIFF(dd, A.EndDate, getdate()) <= 30)) and isnull(A.ParentId, '') <> ''
			and A.PossibleDepartmentId is null
			--исключил рп-привязки
			and not exists (SELECT * FROM DepFinRP WHERE [код рп в финград] = A.Code)
			and A.ParentDepartmentId is null

--2 уровень
--declare @DepNewId int
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'К разбору', null, Code1C, Path + N'1', 2, null, 99, 1, 4, null 
FROM Department WHERE ItemLevel = 1

SET @DepNewId = @@IDENTITY

--достраиваем путь 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--3 уровень
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'К разбору', null, Code1C, Path + N'1', 3, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--достраиваем путь 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--4 уровень
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'К разбору', null, Code1C, Path + N'1', 4, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--достраиваем путь 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--5 уровень
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'К разбору', null, Code1C, Path + N'1', 5, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--достраиваем путь 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId


--6 уровень
INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
SELECT 1, null, N'К разбору', null, Code1C, Path + N'1', 6, null, 99, 1, 4, null 
FROM Department WHERE Id = @DepNewId

SET @DepNewId = @@IDENTITY

--достраиваем путь 
UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
WHERE Id = @DepNewId

--SET @DepTmpId = @DepNewId
--связываем с искусственным 6 уровнем
UPDATE #TPLink SET ParentDepartmentId = @DepNewId

WHILE EXISTS (SELECT * FROM #TPLink)
BEGIN
	SELECT top 1 @id = A.Id FROM #TPLink as A
	--находим родительское подразделение 6 уровня и добавляем запись в наеденного родителя
	INSERT INTO Department(Version, Code, Name, Code1C, ParentId, Path, ItemLevel, CodeSKD, Priority, IsUsed, BFGId, FingradCode)
	SELECT 1, null, A.Name, null, B.Code1C, B.Path + N'1', 7, null, 99, 1, 4, A.Code
	FROM #TPLink as A
	INNER JOIN Department as B ON B.Id = A.ParentDepartmentId
	--INNER JOIN Department as C ON C.Code1C = B.ParentId
	WHERE A.Id = @Id

	SET @DepNewId = @@IDENTITY
	
	--достраиваем путь 
	UPDATE Department SET Code = cast(@DepNewId as nvarchar(10)), Code1C = @DepNewId, Path = SUBSTRING(Path, 1, len(Path) - 1) + cast(@DepNewId as nvarchar(10)) + N'.'
	WHERE Id = @DepNewId

	DELETE FROM #TPLink WHERE Id = @Id
END

print 'Закончена обработка несвязанных подразделений'


		
drop table #TPLink

--находим записи, которые связаны по коду 1С и потом уже с данными Финграда по их коду
SELECT A.Id, A.ParentId, C.[Сокращенное_наименование] as FinDepName, 
			 case A.BFGId when 1 then 'Бэк'
										when 2 then 'Фронт'
										when 3 then 'ГПД'
										else 'Управленческое' end as FinDepNameShort
			 ,C.* INTO #TMP
FROM Department as A
INNER JOIN TerraPoint as B ON B.PossibleDepartmentId = A.Id and B.ItemLevel = 3 and (B.EndDate is null or (B.EndDate is not null and DATEDIFF(dd, B.EndDate, getdate()) <= 30))
--INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
LEFT JOIN Fingrad_csv as C ON C.[Код_подразделения] = B.Code
WHERE A.ItemLevel = 7 and A.FingradCode is not null
UNION ALL
SELECT A.Id, A.ParentId, C.[Сокращенное_наименование] as FinDepName, 
			 case A.BFGId when 1 then 'Бэк'
										when 2 then 'Фронт'
										when 3 then 'ГПД'
										else 'Управленческое' end as FinDepNameShort
			 ,C.* --INTO #TMP
FROM Department as A
INNER JOIN TerraPoint as B ON B.Code = A.FingradCode and B.ItemLevel = 3 and (B.EndDate is null or (B.EndDate is not null and DATEDIFF(dd, B.EndDate, getdate()) <= 30))
--INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
LEFT JOIN Fingrad_csv as C ON C.[Код_подразделения] = B.Code
WHERE A.ItemLevel = 7 and A.BFGId = 4 and A.FingradCode is not null

--добавляем колонку для группы операций
ALTER TABLE #TMP ADD OperGroupId int null


--приводим данные в порядок
UPDATE #TMP SET --[Дата_процедуры] = case when year([Дата_процедуры]) = 1900 then null else [Дата_процедуры] end
								--[Вид_процедуры] = case when len([Вид_процедуры]) = 0 or [Вид_процедуры] = N'-' then null else [Вид_процедуры] end
								--,[Полное_наименование] = case when len([Полное_наименование]) = 0 or [Полное_наименование] = N'-' then null else [Полное_наименование] end
								[Сокращенное_наименование] = isnull(FinDepName, case when len([Сокращенное_наименование]) = 0 or [Сокращенное_наименование] = N'-' then null else [Сокращенное_наименование] end)
								,[Индекс] = case when len([Индекс]) = 0 or [Индекс] = N'-' then null else REPLACE(REPLACE([Индекс], N',', N'.'), CHAR(32), '') end
								,[Субъект_федерации] = case when len([Субъект_федерации]) = 0 or [Субъект_федерации] = N'-' then null else [Субъект_федерации] end
								,[Населенный_пункт] = case when len([Населенный_пункт]) = 0 or [Населенный_пункт] = N'-' then null else [Населенный_пункт] end
								,[Улица_дом] = case when len([Улица_дом]) = 0 or [Улица_дом] = N'-' then null else [Улица_дом] end
								--,[Статус_подразделения] = case when len([Статус_подразделения]) = 0 or [Статус_подразделения] = N'-' then null else [Статус_подразделения] end
								,[Дата_открытия_офиса] = case when year([Дата_открытия_офиса]) = 1900 then null else [Дата_открытия_офиса] end
								,[Дата_закрытия_офиса] = case when year([Дата_закрытия_офиса]) = 1900 then null else [Дата_закрытия_офиса] end
								,[Арендованное_помещение] = case when len([Арендованное_помещение]) = 0 or [Арендованное_помещение] = N'-' then null else [Арендованное_помещение] end
								,[Площадь_подразделения] = case when len([Площадь_подразделения]) = 0 then '0' else REPLACE([Площадь_подразделения], N',', N'.') end	--числовые поля
								--,[Реквизиты_договора] = case when len([Реквизиты_договора]) = 0 or [Реквизиты_договора] = N'-' then null else [Реквизиты_договора] end
								,[Сумма_ежемесячного_платежа] = case when len([Сумма_ежемесячного_платежа]) = 0 then '0' else REPLACE([Сумма_ежемесячного_платежа], N',', N'.') end	--числовые поля
								,[Код_СВКредит] = case when len([Код_СВКредит]) = 0 or [Код_СВКредит] = N'-' then null else [Код_СВКредит] end
								,[Код_РБС] = case when len([Код_РБС]) = 0 or [Код_РБС] = N'-' then null else [Код_РБС] end
								--,[Код_Инверсия] = case when len([Код_Инверсия]) = 0 or [Код_Инверсия] = N'-' then null else [Код_Инверсия] end
								--,[Код_ХД] = case when len([Код_ХД]) = 0 or [Код_ХД] = N'-' then null else [Код_ХД] end
								--,[Код_1С] = case when len([Код_1С]) = 0 or [Код_1С] = N'-' then null else [Код_1С] end
								,[Тип_подразделения] = case when len([Тип_подразделения]) = 0 or [Тип_подразделения] = N'-' then null else [Тип_подразделения] end
								--,[Контрагент] = case when len([Контрагент]) = 0 or [Контрагент] = N'-' then null else [Контрагент] end
								--,[Наименование_в_СВК_ХД] = case when len([Наименование_в_СВК_ХД]) = 0 or [Наименование_в_СВК_ХД] = N'-' then null else [Наименование_в_СВК_ХД] end
								,[РП_привязка] = case when len([РП_привязка]) = 0 or [РП_привязка] = N'-' then null else [РП_привязка] end
								,[Блокировка] = case when len([Блокировка]) = 0 or [Блокировка] = N'-' then null else [Блокировка] end
								,[Прежний_код_подразделения] = case when len([Прежний_код_подразделения]) = 0 or [Прежний_код_подразделения] = N'-' then null else [Прежний_код_подразделения] end
								,[Front_Back1] = case when len([Front_Back1]) = 0 or [Front_Back1] = N'-' then null else [Front_Back1] end
								,[Идентификация_сетевого_магазина] = case when len([Идентификация_сетевого_магазина]) = 0 or [Идентификация_сетевого_магазина] = N'-' then null else [Идентификация_сетевого_магазина] end
								,[Бизнес_группа] = case when len([Бизнес_группа]) = 0 or [Бизнес_группа] = N'-' then null else [Бизнес_группа] end
								--,[Адрес_УС_если_не_совпадает_с_адресом_офиса] = case when len([Адрес_УС_если_не_совпадает_с_адресом_офиса]) = 0 or [Адрес_УС_если_не_совпадает_с_адресом_офиса] = N'-' then null else [Адрес_УС_если_не_совпадает_с_адресом_офиса] end
								--,[Долгосрочная_аренда] = case when len([Долгосрочная_аренда]) = 0 or [Долгосрочная_аренда] = N'-' then null else [Долгосрочная_аренда] end
								,[Руководитель_РП] = case when len([Руководитель_РП]) = 0 or [Руководитель_РП] = N'-' then null else [Руководитель_РП] end
								,[Приказы] = case when len([Приказы]) = 0 or [Приказы] = N'-' then null else [Приказы] end
								,[№_телефона] = case when len([№_телефона]) = 0 or [№_телефона] = N'-' then null else [№_телефона] end
								,[Режим_работы_офиса_доступа_к_УС] = case when len([Режим_работы_офиса_доступа_к_УС]) = 0 or [Режим_работы_офиса_доступа_к_УС] = N'-' then null else [Режим_работы_офиса_доступа_к_УС] end
								,[Режим_работы_кассы] = case when len([Режим_работы_кассы]) = 0 or [Режим_работы_кассы] = N'-' then null else [Режим_работы_кассы] end
								,[Режим_работы_Банкомата] = case when len([Режим_работы_Банкомата]) = 0 or [Режим_работы_Банкомата] = N'-' then null else [Режим_работы_Банкомата] end
								,[Режим_работы_Cash_in] = case when len([Режим_работы_Cash_in]) = 0 or [Режим_работы_Cash_in] = N'-' then null else [Режим_работы_Cash_in] end
								,[Инкассирующее_подразделение_кэшина] = case when len([Инкассирующее_подразделение_кэшина]) = 0 or [Инкассирующее_подразделение_кэшина] = N'-' then null else [Инкассирующее_подразделение_кэшина] end
								,[Инкассирующее_подразделение_банкомата] = case when len([Инкассирующее_подразделение_банкомата]) = 0 or [Инкассирующее_подразделение_банкомата] = N'-' then null else [Инкассирующее_подразделение_банкомата] end
								,[Дата_запуска_кэшина_первая] = case when year([Дата_запуска_кэшина_первая]) = 1900 then null else [Дата_запуска_кэшина_первая] end
								,[Кол_во_запущенных_банкоматов_с_функцией_кэшин] = case when len([Кол_во_запущенных_банкоматов_с_функцией_кэшин]) = 0 then '0' else REPLACE([Кол_во_запущенных_банкоматов_с_функцией_кэшин], N',', N'.') end
								,[Операции] = case when len([Операции]) = 0 or [Операции] = N'-' then null else [Операции] end
								,[Обслуживание_ЮЛ] = case when len([Обслуживание_ЮЛ]) = 0 or [Обслуживание_ЮЛ] = N'-' then null else [Обслуживание_ЮЛ] end
								,[Наличие_кассы] = case when len([Наличие_кассы]) = 0 or [Наличие_кассы] = N'-' then null else [Наличие_кассы] end
								--,[Примечание] = case when len([Примечание]) = 0 or [Примечание] = N'-' then null else [Примечание] end
								--,[Код_Террасофт] = case when len([Код_Террасофт]) = 0 or [Код_Террасофт] = N'-' then null else [Код_Террасофт] end
								,[Дата_запуска_банкомата_первая] = case when year([Дата_запуска_банкомата_первая]) = 1900 then null else [Дата_запуска_банкомата_первая] end
								,[ID_Дирекции] = case when len([ID_Дирекции]) = 0 or [ID_Дирекции] = N'-' then null else [ID_Дирекции] end
								,[Дни_работы_точки] = case when len([Дни_работы_точки]) = 0 or [Дни_работы_точки] = N'-' then null else [Дни_работы_точки] end
								,[Дата_начала_простоя_точки] = case when year([Дата_начала_простоя_точки]) = 1900 then null else [Дата_начала_простоя_точки] end
								,[Дата_возобновления_работы_точки] = case when year([Дата_возобновления_работы_точки]) = 1900 then null else [Дата_возобновления_работы_точки] end
								--,[J_шники_устройств] = case when len([J_шники_устройств]) = 0 or [J_шники_устройств] = N'-' then null else [J_шники_устройств] end
								--,[Ответственный_за_кассовый_лимит] = case when len([Ответственный_за_кассовый_лимит]) = 0 or [Ответственный_за_кассовый_лимит] = N'-' then null else [Ответственный_за_кассовый_лимит] end
								--,[Код_ФЕС] = case when len([Код_ФЕС]) = 0 or [Код_ФЕС] = N'-' then null else [Код_ФЕС] end
								,[СКБ_GE] = case when len([СКБ_GE]) = 0 or [СКБ_GE] = N'-' then null else [СКБ_GE] end
								,[Ориентиры_станция_метро] = case when len([Ориентиры_станция_метро]) = 0 or [Ориентиры_станция_метро] = N'-' then null else [Ориентиры_станция_метро] end
								,[Ориентиры_остановка_транспорта] = case when len([Ориентиры_остановка_транспорта]) = 0 or [Ориентиры_остановка_транспорта] = N'-' then null else [Ориентиры_остановка_транспорта] end
								,[Ориентиры_значимые_объекты] = case when len([Ориентиры_значимые_объекты]) = 0 or [Ориентиры_значимые_объекты] = N'-' then null else [Ориентиры_значимые_объекты] end
								,[Ориентиры_торговые_центры] = case when len([Ориентиры_торговые_центры]) = 0 or [Ориентиры_торговые_центры] = N'-' then null else [Ориентиры_торговые_центры] end
								,[Ориентиры_район_города] = case when len([Ориентиры_район_города]) = 0 or [Ориентиры_район_города] = N'-' then null else [Ориентиры_район_города] end
								,[Причины_внесения_в_справочник] = case when len([Причины_внесения_в_справочник]) = 0 or [Причины_внесения_в_справочник] = N'-' then null else [Причины_внесения_в_справочник] end
								--,[Арендованные_УС] = case when len([Арендованные_УС]) = 0 or [Арендованные_УС] = N'-' then null else [Арендованные_УС] end
								--,[Блокировка2] = case when len([Блокировка2]) = 0 or [Блокировка2] = N'-' then null else [Блокировка2] end
								--,[Дирекция_РП_привязка] = case when len([Дирекция_РП_привязка]) = 0 or [Дирекция_РП_привязка] = N'-' then null else [Дирекция_РП_привязка] end
								--,[Код_РП_в_Финград_РП_Привязка] = case when len([Код_РП_в_Финград_РП_Привязка]) = 0 or [Код_РП_в_Финград_РП_Привязка] = N'-' then null else [Код_РП_в_Финград_РП_Привязка] end
								--,[ID_Бизнес_группа_Бизнес_группа] = case when len([ID_Бизнес_группа_Бизнес_группа]) = 0 or [ID_Бизнес_группа_Бизнес_группа] = N'-' then null else [ID_Бизнес_группа_Бизнес_группа] end
								--,[ID_Дирекции_Дирекция] = case when len([ID_Дирекции_Дирекция]) = 0 or [ID_Дирекции_Дирекция] = N'-' then null else [ID_Дирекции_Дирекция] end
								--,[Управление_Дирекции_Бизнес_группа] = case when len([Управление_Дирекции_Бизнес_группа]) = 0 or [Управление_Дирекции_Бизнес_группа] = N'-' then null else [Управление_Дирекции_Бизнес_группа] end
								,[ID_Управления_Дирекции_Управление_Дирекции] = case when len([ID_Управления_Дирекции_Управление_Дирекции]) = 0 or [ID_Управления_Дирекции_Управление_Дирекции] = N'-' then null else [ID_Управления_Дирекции_Управление_Дирекции] end
--								,[ОКАТО] = case when len([ОКАТО]) = 0 or [ОКАТО] = N'-' then null else [ОКАТО] end
--								,[ОКТМО] = case when len([ОКТМО]) = 0 or [ОКТМО] = N'-' then null else [ОКТМО] end


--UPDATE #TMP SET [Индекс] = REPLACE([Индекс], char(160), '')
--UPDATE #TMP SET [Индекс] = SUBSTRING([Индекс], 1, case when charindex('.', [Индекс]) = 0 then 0 else (charindex('.', [Индекс]) - 1) end) WHERE [Индекс] is not null --[Код_подразделения] = '04-07-24-011'
--UPDATE #TMP SET [Индекс] = SUBSTRING([Индекс], 1, 6) 
--UPDATE #TMP SET [Кол_во_запущенных_банкоматов_с_функцией_кэшин] = null where [Кол_во_запущенных_банкоматов_с_функцией_кэшин] = '06.08.2014'
UPDATE #TMP SET [Дни_работы_точки] = '1111110' WHERE [Дни_работы_точки] = '111110'

/*
UPDATE #TMP SET [Ориентиры_станция_метро] = REPLACE([Ориентиры_станция_метро], char(32) + char(32), '')
UPDATE #TMP SET [Ориентиры_торговые_центры] = REPLACE([Ориентиры_торговые_центры], char(32) + char(32), '')
UPDATE #TMP SET [Ориентиры_район_города] = REPLACE([Ориентиры_район_города], char(32) + char(32), '')

UPDATE #TMP SET [Ориентиры_остановка_транспорта] = 
								case when [Ориентиры_остановка_транспорта] = '2. остановка транспорта' then null 
										 when [Ориентиры_остановка_транспорта] like '%остановка транспорта%' 
										 then ltrim(rtrim(substring([Ориентиры_остановка_транспорта], CHARINDEX('Остановка транспорта', [Ориентиры_остановка_транспорта]) + 21, len([Ориентиры_остановка_транспорта])))) 
										 else (case when [Ориентиры_остановка_транспорта] = 'нет' then null else [Ориентиры_остановка_транспорта] end)
										 end
WHERE [Ориентиры_станция_метро] = 'нет'

UPDATE #TMP SET [Ориентиры_значимые_объекты] = 
								REPLACE(
												REPLACE(
																REPLACE(
																				REPLACE(
																							REPLACE([Ориентиры_значимые_объекты], '3. Значимые объекты (площадь, памятник, "народное" название района и др)', ''), 
																							'3. Значимые объекты (площадь, памятник, "народное" название района)', ''),
																							'3. Значимые объекты (площадь, памятник, "народное" название района', ''),
																							'3. Значимые объекты', ''),
																							'3.Значимые объекты', '')
WHERE [Ориентиры_станция_метро] = 'нет'

UPDATE #TMP SET [Ориентиры_значимые_объекты] = case when ltrim(rtrim([Ориентиры_значимые_объекты])) = 'нет' or len(ltrim(rtrim([Ориентиры_значимые_объекты]))) = 0 then null else ltrim(rtrim([Ориентиры_значимые_объекты])) end
WHERE [Ориентиры_станция_метро] = 'нет'


UPDATE #TMP SET [Ориентиры_торговые_центры] = 
								REPLACE( 
											 REPLACE( 
															 REPLACE(
																			REPLACE([Ориентиры_торговые_центры], '4. Торговые центры и магазины (рядом или мы в них)', ''),
																			'4. Торговые центры и магазины', ''),
																			'4. Торговые центры', ''), '4.', '')
WHERE [Ориентиры_станция_метро] = 'нет'

UPDATE #TMP SET [Ориентиры_торговые_центры] = case when ltrim(rtrim([Ориентиры_торговые_центры])) = 'нет' or len(ltrim(rtrim([Ориентиры_торговые_центры]))) = 0 then null else ltrim(rtrim([Ориентиры_торговые_центры])) end
WHERE [Ориентиры_станция_метро] = 'нет'


UPDATE #TMP SET [Ориентиры_район_города] = case when ltrim(rtrim([Ориентиры_район_города])) = 'нет' or len(ltrim(rtrim([Ориентиры_район_города]))) = 0 then null else ltrim(rtrim([Ориентиры_район_города])) end
WHERE [Ориентиры_станция_метро] = 'нет'


UPDATE #TMP SET [Ориентиры_станция_метро] = 
								REPLACE(
								REPLACE(
											REPLACE(
														REPLACE(
																		case when [Ориентиры_станция_метро] like '%2.%' 
																				 then  substring([Ориентиры_станция_метро], 1, (charindex('2', [Ориентиры_станция_метро]) - 1))
																				 else [Ориентиры_станция_метро]
																				 end, '1. Cтанции метро - нет', ''), 
																				 '1. Cтанции метро нет', ''), 
																				 '1. Cтанции метро', ''),
																				 '1. -', '')
WHERE [Ориентиры_станция_метро] is not null and [Ориентиры_станция_метро] <> 'нет'

UPDATE #TMP SET [Ориентиры_станция_метро] = case when ltrim(rtrim([Ориентиры_станция_метро])) like '%нет%' or len(ltrim(rtrim([Ориентиры_станция_метро]))) = 0 then null else ltrim(rtrim([Ориентиры_станция_метро])) end
WHERE  [Ориентиры_станция_метро] is not null and [Ориентиры_станция_метро] <> 'нет'
*/


--для операций
UPDATE #TMP SET [Операции] = replace([Операции], '2) прочие операции:', ';') WHERE [Операции] like '%2) прочие операции:%'
UPDATE #TMP SET [Операции] = replace([Операции], 'II. прочие операции:', ';') WHERE [Операции] like '%II. прочие операции:%'
UPDATE #TMP SET [Операции] = replace([Операции], '1) С наличной иностранной валютой и валютой Российской Федерации в операционной кассе:', '') WHERE [Операции] like '1) С наличной иностранной валютой и валютой Российской Федерации в операционной кассе:%'
UPDATE #TMP SET [Операции] = replace([Операции], 'Ипотека 1) С наличной иностранной валютой и валютой Российской Федерации в операционной кассе:', '') WHERE [Операции] like '%Ипотека%'
UPDATE #TMP SET [Операции] = replace([Операции], '. -', ';') WHERE [Операции]  like '%. -%'
UPDATE #TMP SET [Операции] = replace([Операции], '; -', ';') WHERE [Операции]  like '%; -%'
UPDATE #TMP SET [Операции] = replace([Операции], ';-', ';') WHERE [Операции]  like '%;-%'
UPDATE #TMP SET [Операции] = replace([Операции], '.;', ';') WHERE [Операции]  like '%.;%'
UPDATE #TMP SET [Операции] = replace([Операции], '18.', ';') WHERE [Операции]  like '%18.%'
UPDATE #TMP SET [Операции] = replace([Операции], '17.', ';') WHERE [Операции]  like '%17.%'
UPDATE #TMP SET [Операции] = replace([Операции], '16.', ';') WHERE [Операции]  like '%16.%'
UPDATE #TMP SET [Операции] = replace([Операции], '15.', ';') WHERE [Операции]  like '%15.%'
UPDATE #TMP SET [Операции] = replace([Операции], '14.', ';') WHERE [Операции]  like '%14.%'
UPDATE #TMP SET [Операции] = replace([Операции], '13.', ';') WHERE [Операции]  like '%13.%'
UPDATE #TMP SET [Операции] = replace([Операции], '12.', ';') WHERE [Операции]  like '%12.%'
UPDATE #TMP SET [Операции] = replace([Операции], '11.', ';') WHERE [Операции]  like '%11.%'
UPDATE #TMP SET [Операции] = replace([Операции], '10.', ';') WHERE [Операции]  like '%10.%'
UPDATE #TMP SET [Операции] = replace([Операции], '9.', ';') WHERE [Операции]  like '%9.%'
UPDATE #TMP SET [Операции] = replace([Операции], '8.', ';') WHERE [Операции]  like '%8.%'
UPDATE #TMP SET [Операции] = replace([Операции], '7.', ';') WHERE [Операции]  like '%7.%'
UPDATE #TMP SET [Операции] = replace([Операции], '6.', ';') WHERE [Операции]  like '%6.%'
UPDATE #TMP SET [Операции] = replace([Операции], '5.', ';') WHERE [Операции]  like '%5.%'
UPDATE #TMP SET [Операции] = replace([Операции], '4.', ';') WHERE [Операции]  like '%4.%'
UPDATE #TMP SET [Операции] = replace([Операции], '3.', ';') WHERE [Операции]  like '%3.%'
UPDATE #TMP SET [Операции] = replace([Операции], '2.', ';') WHERE [Операции]  like '%2.%'
UPDATE #TMP SET [Операции] = replace([Операции], '1.', ';;;') WHERE [Операции]  like '%1.%'
UPDATE #TMP SET [Операции] = replace([Операции], ';;;', '') WHERE [Операции]  like '%;;;%'
UPDATE #TMP SET [Операции] = replace([Операции], '.;', ';') WHERE [Операции]  like '%.;%'
UPDATE #TMP SET [Операции] = replace([Операции], ';;', ';') WHERE [Операции]  like '%;;%'
UPDATE #TMP SET [Операции] = replace([Операции], '-О', 'О') WHERE [Операции]  like '%-О%'
UPDATE #TMP SET [Операции] = replace([Операции], 'ООО ИКБ "Совкомбанк"', '++"') WHERE [Операции]  like '%ООО ИКБ "Совкомбанк"%'

SELECT identity (int, 1, 1) as RowId, Operation into #TMP1
FROM (SELECT distinct [Операции] as Operation FROM #TMP WHERE [Операции] <> '-' and [Операции] <> '') as a
order by a.Operation

DELETE FROM #TMP1 WHERE Operation  = 'Значимые объекты: Аптека'

--копия операций для формирования групп операций
SELECT * INTO #TMP1_1 FROM #TMP1

--таблица для операций подразделений
CREATE TABLE #TMP2 (id int, [Description] varchar(400))

--#####################################
--находим руководителей подразделений на уровень выше на ступень выше
SELECT A.Id, A.ParentId, A.ItemLevel  INTO #TMP3
FROM Department as A
--INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
--INNER JOIN Fingrad_csv as C ON C.[Код_подразделения] = B.FinDepPointCode

--список руководителей (исключил беременных)
SELECT * INTO #TMP4
FROM Users as A WHERE A.RoleId = 4 and A.IsMainManager = 1 and A.IsActive = 1
--отключаем беременных
and not exists (SELECT * FROM ChildVacation WHERE getdate() between BeginDate and EndDate and UserId in (SELECT id FROM Users WHERE RoleId = 2 and IsActive = 1 and Email = A.Email)) 

SELECT distinct Id, UserId
INTO #TMP5
FROM(--по дирекциям
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
			--по ручным привязкам
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
--обработка операций
WHILE EXISTS(SELECT * FROM #TMP1)
BEGIN
	SET @RowId = (SELECT top 1 rowid FROM #TMP1)

	SET @aa = (SELECT Operation FROM #TMP1 WHERE RowId = @RowId)
	SET @bb = ''

	SELECT @i = 1, @len = len(@aa) + 1
	--select PATINDEX ('%;%', @aa), @aa
		--в цикле ходим по символам строки и бьем ее на части
	WHILE @i < @len
	BEGIN
		--обрабатываем строки, где есть разделитель ';'
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

			--последняя часть
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
		

		--где разделителем являются заглавные буквы
		IF (PATINDEX ('%;%', @aa) = 0)
		BEGIN
			IF ascii(substring(@aa, @i, 1)) in (ascii('В'), ascii('П'), ascii('О'), ascii('Т')) and len(isnull(@bb, '')) <> 0
			BEGIN
--				print rtrim(ltrim(@bb))
				IF NOT EXISTS (SELECT * FROM #TMP2 WHERE [Description] = rtrim(ltrim(@bb)))
				BEGIN
					INSERT INTO #TMP2 (id, [Description]) VALUES(@rowid, rtrim(ltrim(@bb)))
				END
				SET @bb = ''
			END

			SET @bb = isnull(@bb, '') + substring(@aa, @i, 1)

			--последняя часть
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


UPDATE #TMP2 SET [Description] = replace([Description], '++', 'ПАО "Совкомбанк"') WHERE [Description]  like '%++%'

INSERT INTO StaffDepartmentOperations(Version, Name, IsUsed)
SELECT distinct 1, [Description], cast(0 as bit) FROM #TMP2 as A
WHERE NOT EXISTS (SELECT * FROM StaffDepartmentOperations WHERE Name = A.[Description])

--теперь пытаемся закачать данные в новую структуру

--формируем групп операций
WHILE EXISTS(SELECT * FROM #TMP1_1)
BEGIN
	SELECT top 1 @RowId = RowId, @Oper = Operation FROM #tmp1_1 ORDER BY RowId

	INSERT INTO StaffDepartmentOperationGroups ([Version], Name, IsUsed) 
	SELECT  1, 'Группа опеаций ' + cast(max(Id) + 1 as varchar), 0
	FROM StaffDepartmentOperationGroups

	SET @OperGroupId = @@IDENTITY

	UPDATE #TMP SET OperGroupId = @OperGroupId WHERE [Операции] = @Oper

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
	СПРАВОЧНИК ЗАПОЛНИЛ В СКРИПТЕ СОЗДАНИЯ СТРУКТУРЫ
--заполняем данными справочник типов подразделений
--INSERT INTO StaffDepartmentTypes ([Version], Name)
--SELECT distinct 1, [Тип_подразделения] FROM #TMP ORDER BY [Тип_подразделения]
*/


	
--цикл по полученным данным
WHILE EXISTS(SELECT * FROM #TMP)
BEGIN
	SELECT top 1 @Id = Id FROM #TMP

	print 'Id обрабатываемого подразделения ' + cast(@Id as varchar)

	--определяем руководителя для подразделения
	--так как их может быть несколько, то берем по уровню наименьшего
	SELECT top 1 @CreatorId = UserId FROM #TMP5 as A
	INNER JOIN Users as B ON B.Id = A.UserId
	WHERE A.Id = @Id
	ORDER BY B.Level desc

	SET @CreatorId = isnull(@CreatorId, 5)	--бывает, что не определен руководитель

	--оба адреса делаем одинаковыми
	--заносим адрес
	INSERT INTO RefAddresses([Version]
													,[Address]
													,PostIndex
													,RegionName
													,CityName
													,SettlementName
													,StreetName
													,CreatorId)
	SELECT 1, isnull([Индекс], '') + case when len(isnull([Индекс], '')) = 0 then '' else ', ' end + [Субъект_федерации] + ', ' + [Населенный_пункт] + ', ' + [Улица_дом]
				,[Индекс]
				,[Субъект_федерации]
				,case when [Населенный_пункт] like 'г. %' then [Населенный_пункт] else null end
				,case when [Населенный_пункт] not like 'г. %' then [Населенный_пункт] else null end
				,substring([Улица_дом], 1, 50)
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
	SELECT 1, isnull([Индекс], '') + case when len(isnull([Индекс], '')) = 0 then '' else ', ' end + [Субъект_федерации] + ', ' + [Населенный_пункт] + ', ' + [Улица_дом]
				,[Индекс]
				,[Субъект_федерации]
				,case when [Населенный_пункт] like 'г. %' then [Населенный_пункт] else null end
				,case when [Населенный_пункт] not like 'г. %' then [Населенный_пункт] else null end
				,substring([Улица_дом], 1, 50)
				,@CreatorId 
	FROM #TMP WHERE Id = @Id

	SET @FactAddressId = @@IDENTITY


	--заносим в заявки
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
					,'20151031'--A.[Дата_процедуры]
					,4--ввод начальных данных
					,@Id
					,B.ItemLevel
					,C.Id
					,B.Name
					,B.BFGId
					,[Приказы]
					,null
					,@LegalAddressId	--адрес
					,1
					,1
					,null
					,case when A.[Причины_внесения_в_справочник] = 'по плану РР' then 1 else 0 end
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

	--заносим ЦБ реквизиты
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
					,A.[Кол_во_запущенных_банкоматов_всего]
					,A.[Кол_во_запущенных_кэшинов] 
					,A.[Кол_во_запущенных_банкоматов_с_функцией_ресайклинг]
					,A.[Кол_во_запущенных_банкоматов_с_функцией_кэшин]
					,null
					,null
					,A.[Дата_запуска_кэшина_первая]
					,A.[Дата_запуска_банкомата_первая]
					,@CreatorId
	FROM #TMP as A
	WHERE A.Id = @Id



	--заносим управленческие реквизиты
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
					,A.[Код_подразделения]
					,A.[Сокращенное_наименование]
					,A.FinDepNameShort
					,C.Id
					,A.[Прежний_код_подразделения]
					,@FactAddressId		--адрес
					,null --A.[Статус_подразделения]
					,B.Id			--типа подразделения
					,A.[Дата_открытия_офиса]
					,A.[Дата_закрытия_офиса]
					,A.[Режим_работы_офиса_доступа_к_УС]
					,A.[Режим_работы_кассы]
					,A.[Режим_работы_Банкомата]
					,A.[Режим_работы_Cash_in]
					,A.[Дата_начала_простоя_точки]
					,A.[Дата_возобновления_работы_точки]
					,F.Id
					,null--A.[Реквизиты_договора]
					,A.[Площадь_подразделения]
					,A.[Сумма_ежемесячного_платежа]
					,A.[№_телефона]
					,case when A.[Блокировка] = 'Действует' then 0 else 1 end
					,D.Id
					,case when A.[Обслуживание_ЮЛ] = 'да' then 1 else 0 end
					,0
					,0
					,null --A.[Примечание]
					,E.Id
					,G.Id
					,case when A.[Установленное_ПО_в_ВСП] = N'*' then 1
								when A.[Установленное_ПО_в_ВСП] = N'=' then 2
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%СВК-только ТК%%РБС%%Инверсия%%Внешние системы (ЗК, КТ,СГ)' then 15
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%СВК-только ТК%%РБС%%Инверсия%' then 14
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%СВК-только ТК%%РБС%%Внешние системы (ЗК, КТ,СГ)' then 13
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%СВК-только ТК%%РБС%' then 12
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%СВК-только ТК%%Внешние системы (ЗК, КТ,СГ)' then 11
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%СВК-только ТК%' then 10
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%РБС%%Инверсия%%Внешние системы (ЗК, КТ,СГ)' then 9
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%РБС%%Инверсия%' then 8
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%РБС%%Внешние системы (ЗК, КТ,СГ)' then 7
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%РБС%' then 6
								when A.[Установленное_ПО_в_ВСП] like  N'СВК-все продукты%%Внешние системы (ЗК, КТ,СГ)' then 5
								when A.[Установленное_ПО_в_ВСП] =  N'СВК-все продукты; ; ; ;' then 4
								when A.[Установленное_ПО_в_ВСП] =  N'; СВК-только ТК; ; ;' then 3
					end as SoftGroupId
					,A.OperGroupId
					,@CreatorId
	FROM #TMP as A
	LEFT JOIN StaffDepartmentTypes as B ON B.Name = A.[Тип_подразделения]
	LEFT JOIN StaffDepartmentReasons as C ON C.Name = A.[Причины_внесения_в_справочник]
	LEFT JOIN StaffNetShopIdentification as D ON D.Name = A.[Идентификация_сетевого_магазина]
	LEFT JOIN StaffDepartmentCashDeskAvailable as E ON E.Name = A.[Наличие_кассы]
	LEFT JOIN StaffDepartmentRentPlace as F ON F.Name = A.[Арендованное_помещение]
	LEFT JOIN StaffDepartmentSKB_GE as G ON G.Name = A.[СКБ_GE]
	WHERE A.Id = @Id

	SET @DMDetailId = @@IDENTITY


		--заполняем коды совместимости программ
		IF (SELECT [Код_СВКредит] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 1, [Код_СВКредит], @CreatorId FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [Код_РБС] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 2, [Код_РБС], @CreatorId FROM #TMP WHERE Id = @Id
		END
		/*
		IF (SELECT [Код_Инверсия] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 3, [Код_Инверсия], @CreatorId FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [Код_ХД] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 4, [Код_ХД], @CreatorId FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [Код_Террасофт] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 5, [Код_Террасофт], @CreatorId FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [Код_ФЕС] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 6, [Код_ФЕС], @CreatorId FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [СКБ_GE] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code, CreatorId)
			SELECT 1, @DMDetailId, 7, [СКБ_GE], @CreatorId FROM #TMP WHERE Id = @Id
		END
		*/
		
		
		--ориентиры новая версия двнных
		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 1, [Ориентиры_станция_метро], @CreatorId
		FROM #TMP WHERE Id = @Id and [Ориентиры_станция_метро] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта], @CreatorId 
		FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты], @CreatorId 
		FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры], @CreatorId 
		FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

		INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
		SELECT 1, @DMDetailId, 5, [Ориентиры_район_города], @CreatorId 
		FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null

		/*
		старая версия
		--ориентиры (попытаться навести порядок в данных и закачать их)		
		--на момент написания в данных полях null-ов нет
		IF (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) is null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 5, [Ориентиры_район_города], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null
		END
		
		--на момент написания null-ы есть во всех полях
		IF (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) = 'нет'
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта], @CreatorId
			FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 5, [Ориентиры_район_города], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null
		END

		IF (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) <> 'нет' and (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 1, [Ориентиры_станция_метро], @CreatorId
			FROM #TMP WHERE Id = @Id and [Ориентиры_станция_метро] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта], @CreatorId
			FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description], CreatorId)
			SELECT 1, @DMDetailId, 5, [Ориентиры_район_города], @CreatorId 
			FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null
		END
		*/


		--режим работы подразделения
		SELECT @WorkDays = [Дни_работы_точки] FROM #TMP WHERE Id = @Id
		IF @WorkDays is not null
		BEGIN
			SET @i = 1
			WHILE @i < 8
			BEGIN
				--подразделения
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 1, @i, cast(SUBSTRING(@WorkDays, @i, 1) as bit), @CreatorId)

				--касса
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 2, @i, 0, @CreatorId)
				--банкомат
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 3, @i, 0, @CreatorId)
				--кэшин
				INSERT INTO StaffDepartmentOperationModes([Version], DMDetailId, ModeType, [WeekDay], IsWorkDay, CreatorId)
				VALUES(1, @DMDetailId, 4, @i, 0, @CreatorId)
				SET @i += 1
			END	
		END

		/*
		--операции
		SELECT @Oper = [Операции] FROM #TMP WHERE Id = @Id
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


--в удаленных могут работать сотрудники, по этому с них и родителей нужно снять метки
--одним заходом еще обработаем ГПД
SET @i = 1
--сначала прокрашиваем все уровни для удаленных с 1 до 7
WHILE @i < 8
BEGIN
	--красим родителей
	UPDATE Department SET BFGId = 5
	WHERE (Name like '%ликвидиров%' or Name like '%закрыт%' or Name like '%не исп%' or Name like '%корзина%') 
	and isnull(BFGId, 0) not in (4, 5) and ItemLevel = @i

	--стараемся прокрасить от родителя до самого низа
	UPDATE Department SET BFGId = 5
	FROM Department as A
	INNER JOIN (SELECT * FROM Department 
							WHERE (Name like '%ликвидиров%' or Name like '%закрыт%' or Name like '%не исп%' or Name like '%корзина%') 
							and isnull(BFGId, 0) = 5 
							and ItemLevel = @i) as B ON A.Path like B.Path + '%'
	

	SET @i += 1
END


--потом выявляем интересующие нас точки 7 уровня и снимаем метки с них и их родителей до самого верха
--не надо скрывать точки и их родителей, помеченные к удалению, если:
	--к ним привязаны сотрудники 
	--есть связь с точкой из финграда
--начинаем с нижнего уровня до верха
SELECT A.*,
			 case when exists (SELECT * FROM Users WHERE DepartmentId = A.Id and IsActive = 1 and (RoleId & 2 > 0)) then 1 else 0 end as IsPeople,
			 case when A.FingradCode is not null then 1 else 0 end as IsFin
			 INTO #DelDep
FROM Department as A
WHERE --A.BFGId = 5 
			(A.Name like '%ликвидиров%' or A.Name like '%закрыт%' or A.Name like '%не исп%' or A.Name like '%корзина%' )
			and A.ItemLevel = 7
			and isnull(A.BFGId, 0) <> 4	--то что из финграда оставляем


--для 7
UPDATE Department SET BFGId = null
FROM Department as A
INNER JOIN (select * from #DelDep WHERE IsPeople = 1 or IsFin = 1) as B ON B.Id = A.Id 
WHERE isnull(A.BFGId, 0) <> 4

--для его родителей
UPDATE Department SET BFGId = null
FROM Department as A
INNER JOIN (SELECT distinct A.*
						FROM Department as A
						INNER JOIN (select * from #DelDep WHERE IsPeople = 1 or IsFin = 1) as B ON B.Path like A.Path + '%'
						WHERE A.ItemLevel < 7) as B ON B.Id = A.Id
WHERE isnull(A.BFGId, 0) <> 4



--проставим признак использования подразделения
UPDATE Department SET IsUsed = case when isnull(BFGId, 1) <> 5 then 1 else 0 end



--создаем фиктивные заявки для уже существующих подразделений разных уровней и не связанных с Финградом
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
					,'20151031'--A.[Дата_процедуры]
					,4--ввод начальных данных
					,A.Id
					,A.ItemLevel
					,B.Id
					,A.Name
					,A.BFGId
					,null
					,null
					,null	--адрес
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



--проставляем инкассирующие подразделения в заявки (ВАЖНО ПРИ НАЛИЧИИ ЗАЯВОК КУСОК МОЖНО ИСПОЛЬЗОВАТЬ ОТДЕЛЬНО)
UPDATE StaffDepartmentCBDetails SET DepCachinId = E.DepartmentId, DepATMId = F.DepartmentId
FROM StaffDepartmentCBDetails as A
INNER JOIN StaffDepartmentRequest as B ON B.Id = A.DepRequestId and B.IsDraft = 0
INNER JOIN Department as C ON C.id = B.DepartmentId and C.FingradCode is not null
INNER JOIN Fingrad_csv as D ON D.[Код_подразделения] = C.FingradCode and (D.[Инкассирующее_подразделение_кэшина] is not null or D.[Инкассирующее_подразделение_банкомата] is not null)
LEFT JOIN StaffDepartmentRPLink as E ON E.Code = D.[Инкассирующее_подразделение_кэшина_Код_РП_в_финград]
LEFT JOIN StaffDepartmentRPLink as F ON F.Code = D.[Инкассирующее_подразделение_банкомата_Код_РП_в_финград]


drop table #TMP
drop table #TMP1
drop table #TMP1_1
drop table #TMP2
drop table #TMP3
drop table #TMP4
drop table #TMP5
drop table #DelDep

