--СКРИПТ ОБРАБОТКИ И ЗАКАЧКИ ДАННЫХ ИЗ ФИНГРАДА В БАЗУ ДАННЫХ 

--select * from Fingrag_csv 
DECLARE @Id int, @DepRequestId int, @LegalAddressId int, @FactAddressId int, @DMDetailId int, @WorkDays varchar(7), 
				@aa varchar(5000), @bb varchar(5000), @len int, @i int, @RowId int, @Oper varchar(max)

--находим записи, которые связаны по коду 1С и потом уже с данными Финграда по ихнему коду
SELECT A.Id, A.ParentId, C.* INTO #TMP
FROM Department as A
INNER JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
INNER JOIN Fingrag_csv as C ON C.[Код_подразделения] = B.FinDepPointCode

--приводим данные в порядок
UPDATE #TMP SET [Дата_процедуры] = case when year([Дата_процедуры]) = 1899 then null else [Дата_процедуры] end
								,[Вид_процедуры] = case when len([Вид_процедуры]) = 0 or [Вид_процедуры] = N'-' then null else [Вид_процедуры] end
								,[Полное_наименование] = case when len([Полное_наименование]) = 0 or [Полное_наименование] = N'-' then null else [Полное_наименование] end
								,[Сокращенное_наименование] = case when len([Сокращенное_наименование]) = 0 or [Сокращенное_наименование] = N'-' then null else [Сокращенное_наименование] end
								,[Индекс] = case when len([Индекс]) = 0 or [Индекс] = N'-' then null else REPLACE(REPLACE([Индекс], N',', N'.'), CHAR(32), '') end
								,[Субъект_федерации] = case when len([Субъект_федерации]) = 0 or [Субъект_федерации] = N'-' then null else [Субъект_федерации] end
								,[Населенный_пункт] = case when len([Населенный_пункт]) = 0 or [Населенный_пункт] = N'-' then null else [Населенный_пункт] end
								,[Улица_дом] = case when len([Улица_дом]) = 0 or [Улица_дом] = N'-' then null else [Улица_дом] end
								,[Статус_подразделения] = case when len([Статус_подразделения]) = 0 or [Статус_подразделения] = N'-' then null else [Статус_подразделения] end
								,[Дата_отркытия_офиса] = case when year([Дата_отркытия_офиса]) = 1899 then null else [Дата_отркытия_офиса] end
								,[Дата_ закрытия_офиса] = case when year([Дата_ закрытия_офиса]) = 1899 then null else [Дата_ закрытия_офиса] end
								,[Арендованное_помещение] = case when len([Арендованное_помещение]) = 0 or [Арендованное_помещение] = N'-' then null else [Арендованное_помещение] end
								,[Площадь_подразделения] = case when len([Площадь_подразделения]) = 0 then '0' else REPLACE([Площадь_подразделения], N',', N'.') end	--числовые поля
								,[Реквизиты_договора] = case when len([Реквизиты_договора]) = 0 or [Реквизиты_договора] = N'-' then null else [Реквизиты_договора] end
								,[Сумма_ежемесячного_платежа] = case when len([Сумма_ежемесячного_платежа]) = 0 then '0' else REPLACE([Сумма_ежемесячного_платежа], N',', N'.') end	--числовые поля
								,[Код_СВКредит] = case when len([Код_СВКредит]) = 0 or [Код_СВКредит] = N'-' then null else [Код_СВКредит] end
								,[Код_РБС] = case when len([Код_РБС]) = 0 or [Код_РБС] = N'-' then null else [Код_РБС] end
								,[Код_Инверсия] = case when len([Код_Инверсия]) = 0 or [Код_Инверсия] = N'-' then null else [Код_Инверсия] end
								,[Код_ХД] = case when len([Код_ХД]) = 0 or [Код_ХД] = N'-' then null else [Код_ХД] end
								,[Код_1С] = case when len([Код_1С]) = 0 or [Код_1С] = N'-' then null else [Код_1С] end
								,[Тип_подразделения] = case when len([Тип_подразделения]) = 0 or [Тип_подразделения] = N'-' then null else [Тип_подразделения] end
								,[Контрагент] = case when len([Контрагент]) = 0 or [Контрагент] = N'-' then null else [Контрагент] end
								,[Наименование_в_СВК_ХД] = case when len([Наименование_в_СВК_ХД]) = 0 or [Наименование_в_СВК_ХД] = N'-' then null else [Наименование_в_СВК_ХД] end
								,[РП_привязка] = case when len([РП_привязка]) = 0 or [РП_привязка] = N'-' then null else [РП_привязка] end
								,[Блокировка] = case when len([Блокировка]) = 0 or [Блокировка] = N'-' then null else [Блокировка] end
								,[Прежний_код_подразделения] = case when len([Прежний_код_подразделения]) = 0 or [Прежний_код_подразделения] = N'-' then null else [Прежний_код_подразделения] end
								,[Front_Back1] = case when len([Front_Back1]) = 0 or [Front_Back1] = N'-' then null else [Front_Back1] end
								,[Идентификация_сетевого_магазина] = case when len([Идентификация_сетевого_магазина]) = 0 or [Идентификация_сетевого_магазина] = N'-' then null else [Идентификация_сетевого_магазина] end
								,[Бизнес_группа] = case when len([Бизнес_группа]) = 0 or [Бизнес_группа] = N'-' then null else [Бизнес_группа] end
								,[Адрес_УС_если_не_совпадает_с_адресом_офиса] = case when len([Адрес_УС_если_не_совпадает_с_адресом_офиса]) = 0 or [Адрес_УС_если_не_совпадает_с_адресом_офиса] = N'-' then null else [Адрес_УС_если_не_совпадает_с_адресом_офиса] end
								,[Долгосрочная_аренда] = case when len([Долгосрочная_аренда]) = 0 or [Долгосрочная_аренда] = N'-' then null else [Долгосрочная_аренда] end
								,[Руководитель_РП] = case when len([Руководитель_РП]) = 0 or [Руководитель_РП] = N'-' then null else [Руководитель_РП] end
								,[Приказы] = case when len([Приказы]) = 0 or [Приказы] = N'-' then null else [Приказы] end
								,[Номер_телефона] = case when len([Номер_телефона]) = 0 or [Номер_телефона] = N'-' then null else [Номер_телефона] end
								,[Режим_работы_офиса_доступа_к_УС] = case when len([Режим_работы_офиса_доступа_к_УС]) = 0 or [Режим_работы_офиса_доступа_к_УС] = N'-' then null else [Режим_работы_офиса_доступа_к_УС] end
								,[Инкассирующее_подразделение_кэшина] = case when len([Инкассирующее_подразделение_кэшина]) = 0 or [Инкассирующее_подразделение_кэшина] = N'-' then null else [Инкассирующее_подразделение_кэшина] end
								,[Инкассирующее_подразделение_банкомата] = case when len([Инкассирующее_подразделение_банкомата]) = 0 or [Инкассирующее_подразделение_банкомата] = N'-' then null else [Инкассирующее_подразделение_банкомата] end
								,[Дата_запуска_кэшина_первая] = case when year([Дата_запуска_кэшина_первая]) = 1899 then null else [Дата_запуска_кэшина_первая] end
								,[Кол_во_запущенных_банкоматов_с_функцией_кэшин] = case when len([Кол_во_запущенных_банкоматов_с_функцией_кэшин]) = 0 then '0' else REPLACE([Кол_во_запущенных_банкоматов_с_функцией_кэшин], N',', N'.') end
								,[Операции] = case when len([Операции]) = 0 or [Операции] = N'-' then null else [Операции] end
								,[Обслуживание_ЮЛ] = case when len([Обслуживание_ЮЛ]) = 0 or [Обслуживание_ЮЛ] = N'-' then null else [Обслуживание_ЮЛ] end
								,[Наличие_кассы] = case when len([Наличие_кассы]) = 0 or [Наличие_кассы] = N'-' then null else [Наличие_кассы] end
								,[Ориентиры_станция_метро] = case when len([Ориентиры_станция_метро]) = 0 or [Ориентиры_станция_метро] = N'-' then null else [Ориентиры_станция_метро] end
								,[Примечание] = case when len([Примечание]) = 0 or [Примечание] = N'-' then null else [Примечание] end
								,[Код_Террасофт] = case when len([Код_Террасофт]) = 0 or [Код_Террасофт] = N'-' then null else [Код_Террасофт] end
								,[Дата_запуска_банкомата_первая] = case when year([Дата_запуска_банкомата_первая]) = 1899 then null else [Дата_запуска_банкомата_первая] end
								,[ID_Дирекции] = case when len([ID_Дирекции]) = 0 or [ID_Дирекции] = N'-' then null else [ID_Дирекции] end
								,[Дни_работы_точки] = case when len([Дни_работы_точки]) = 0 or [Дни_работы_точки] = N'-' then null else [Дни_работы_точки] end
								,[Дата_начала_простоя_точки] = case when year([Дата_начала_простоя_точки]) = 1899 then null else [Дата_начала_простоя_точки] end
								,[Дата_возобновления_работы_точки] = case when year([Дата_возобновления_работы_точки]) = 1899 then null else [Дата_возобновления_работы_точки] end
								,[J_шники_устройств] = case when len([J_шники_устройств]) = 0 or [J_шники_устройств] = N'-' then null else [J_шники_устройств] end
								,[Ответственный_за_кассовый_лимит] = case when len([Ответственный_за_кассовый_лимит]) = 0 or [Ответственный_за_кассовый_лимит] = N'-' then null else [Ответственный_за_кассовый_лимит] end
								,[Код_ФЕС] = case when len([Код_ФЕС]) = 0 or [Код_ФЕС] = N'-' then null else [Код_ФЕС] end
								,[СКБ_GE] = case when len([СКБ_GE]) = 0 or [СКБ_GE] = N'-' then null else [СКБ_GE] end
								,[Ориентиры_остановка_транспорта] = case when len([Ориентиры_остановка_транспорта]) = 0 or [Ориентиры_остановка_транспорта] = N'-' then null else [Ориентиры_остановка_транспорта] end
								,[Ориентиры_значимые_объекты] = case when len([Ориентиры_значимые_объекты]) = 0 or [Ориентиры_значимые_объекты] = N'-' then null else [Ориентиры_значимые_объекты] end
								,[Ориентиры_торговые_центры] = case when len([Ориентиры_торговые_центры]) = 0 or [Ориентиры_торговые_центры] = N'-' then null else [Ориентиры_торговые_центры] end
								,[Ориентиры_район_города] = case when len([Ориентиры_район_города]) = 0 or [Ориентиры_район_города] = N'-' then null else [Ориентиры_район_города] end
								,[Причины_внесения_в_справочник] = case when len([Причины_внесения_в_справочник]) = 0 or [Причины_внесения_в_справочник] = N'-' then null else [Причины_внесения_в_справочник] end
								,[Арендованные_УС] = case when len([Арендованные_УС]) = 0 or [Арендованные_УС] = N'-' then null else [Арендованные_УС] end
								,[Блокировка2] = case when len([Блокировка2]) = 0 or [Блокировка2] = N'-' then null else [Блокировка2] end
								,[Дирекция_РП_привязка] = case when len([Дирекция_РП_привязка]) = 0 or [Дирекция_РП_привязка] = N'-' then null else [Дирекция_РП_привязка] end
								,[Код_РП_в_Финград_РП_Привязка] = case when len([Код_РП_в_Финград_РП_Привязка]) = 0 or [Код_РП_в_Финград_РП_Привязка] = N'-' then null else [Код_РП_в_Финград_РП_Привязка] end
								,[ID_Бизнес_группа_Бизнес_группа] = case when len([ID_Бизнес_группа_Бизнес_группа]) = 0 or [ID_Бизнес_группа_Бизнес_группа] = N'-' then null else [ID_Бизнес_группа_Бизнес_группа] end
								,[ID_Дирекции_Дирекция] = case when len([ID_Дирекции_Дирекция]) = 0 or [ID_Дирекции_Дирекция] = N'-' then null else [ID_Дирекции_Дирекция] end
								,[Управление_Дирекции_Бизнес_группа] = case when len([Управление_Дирекции_Бизнес_группа]) = 0 or [Управление_Дирекции_Бизнес_группа] = N'-' then null else [Управление_Дирекции_Бизнес_группа] end
								,[ID_Управления_Дирекции_Управление_Дирекции] = case when len([ID_Управления_Дирекции_Управление_Дирекции]) = 0 or [ID_Управления_Дирекции_Управление_Дирекции] = N'-' then null else [ID_Управления_Дирекции_Управление_Дирекции] end
								,[ОКАТО] = case when len([ОКАТО]) = 0 or [ОКАТО] = N'-' then null else [ОКАТО] end
								,[ОКТМО] = case when len([ОКТМО]) = 0 or [ОКТМО] = N'-' then null else [ОКТМО] end


UPDATE #TMP SET [Индекс] = REPLACE([Индекс], char(160), '')
UPDATE #TMP SET [Индекс] = SUBSTRING([Индекс], 1, isnull(charindex('.', [Индекс]), 1) - 1) --WHERE [Код_подразделения] = '04-07-24-011'
UPDATE #TMP SET [Индекс] = SUBSTRING([Индекс], 1, 6) 
UPDATE #TMP SET Кол_во_запущенных_банкоматов_с_функцией_кэшин = null where Кол_во_запущенных_банкоматов_с_функцией_кэшин = '06.08.2014'
UPDATE #TMP SET [Дни_работы_точки] = '1111110' WHERE [Дни_работы_точки] = '111110'


UPDATE #TMP SET [Ориентиры_станция_метро] = REPLACE([Ориентиры_станция_метро], char(32) + char(32), '')
UPDATE #TMP SET [Ориентиры_торговые_центры] = REPLACE([Ориентиры_торговые_центры], char(32) + char(32), '')
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

INSERT INTO StaffDepartmentOperations(Version, Name)
SELECT distinct 1, [Description] FROM #TMP2

--теперь пытаемся закачать данные в новую структуру





--заполняем данными справочник типов подразделений
INSERT INTO StaffDepartmentTypes ([Version], Name)
SELECT distinct 1, [Тип_подразделения] FROM #TMP ORDER BY [Тип_подразделения]




--цикл по полученным данным
WHILE EXISTS(SELECT * FROM #TMP)
BEGIN
	SELECT top 1 @Id = Id FROM #TMP

	--оба адреса делаем одинаковыми
	--заносим адрес
	INSERT INTO RefAddresses([Version], [Address], PostIndex)
	SELECT 1, isnull([Индекс], '') + case when [Индекс] is null then '' else ', ' end + [Субъект_федерации] + ', ' + [Населенный_пункт] + ', ' + [Улица_дом], [Индекс] FROM #TMP WHERE Id = @Id

	SET @LegalAddressId = @@IDENTITY

	INSERT INTO RefAddresses([Version], [Address], PostIndex)
	SELECT 1, isnull([Индекс], '') + case when [Индекс] is null then '' else ', ' end + [Субъект_федерации] + ', ' + [Населенный_пункт] + ', ' + [Улица_дом], [Индекс] FROM #TMP WHERE Id = @Id

	SET @FactAddressId = @@IDENTITY


	--заносим в заявки
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
					,A.[Дата_процедуры]
					,case when A.[Вид_процедуры] = 'Занесение в справочник' then 1 else 2 end
					,@Id
					,B.ItemLevel
					,B.ParentId
					,B.Name
					,case when A.[Front_Back1] = 'Front' then 0 when A.[Front_Back1] = 'Back' then 1 else null end
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
					,null
					,null
	FROM #TMP as A
	INNER JOIN Department as B ON B.Id = A.Id
	WHERE A.Id = @Id
	

	SET @DepRequestId = @@IDENTITY

	--заносим ЦБ реквизиты
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
					,A.[Кол_во_запущенных_банкоматов_всего]
					,A.[Кол_во_запущенных_кэшинов] + isnull(A.[Кол_во_запущенных_банкоматов_с_функцией_кэшин], 0)
					,A.[Кол_во_запущенных_банкоматов_с_функцией_ресайклинг]
					,null
					,null
					,A.[Дата_запуска_кэшина_первая]
					,A.[Дата_запуска_банкомата_первая]
	FROM #TMP as A
	WHERE A.Id = @Id



	--заносим управленческие реквизиты
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
					,A.[Код_подразделения]
					,A.[Сокращенное_наименование]
					,A.[Причины_внесения_в_справочник]
					,A.[Прежний_код_подразделения]
					,@FactAddressId		--адрес
					,A.[Статус_подразделения]
					,B.Id			--типа подразделения
					,A.[Дата_отркытия_офиса]
					,A.[Дата_закрытия_офиса]
					,A.[Причины_внесения_в_справочник]
					,A.[Режим_работы_офиса_доступа_к_УС]
					,A.[Дата_начала_простоя_точки]
					,A.[Дата_возобновления_работы_точки]
					,case when A.[Арендованное_помещение] = 'Собственность' then 0 else 1 end
					,A.[Номер_телефона]
					,case when A.[Блокировка] = 'Действует' then 0 else 1 end
					,case when A.[Идентификация_сетевого_магазина] = 'сетевая' then 1 else 0 end
					,case when isnull(A.[Наличие_кассы], 'нет') like '%нет%' then 0 else 1 end
					,case when A.[Обслуживание_ЮЛ] = 'да' then 1 else 0 end
					,0
					,0
					,A.[Примечание]
	FROM #TMP as A
	LEFT JOIN StaffDepartmentTypes as B ON B.Name = A.[Тип_подразделения]
	WHERE A.Id = @Id

	SET @DMDetailId = @@IDENTITY


		--заполняем коды совместимости программ
		IF (SELECT [Код_СВКредит] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 1, [Код_СВКредит] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [Код_РБС] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 2, [Код_РБС] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [Код_Инверсия] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 3, [Код_Инверсия] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [Код_ХД] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 4, [Код_ХД] FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [Код_Террасофт] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 5, [Код_Террасофт] FROM #TMP WHERE Id = @Id
		END

		IF (SELECT [Код_ФЕС] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 6, [Код_ФЕС] FROM #TMP WHERE Id = @Id
		END
		
		IF (SELECT [СКБ_GE] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffProgramCodes([Version], DMDetailId, ProgramId, Code)
			SELECT 1, @DMDetailId, 7, [СКБ_GE] FROM #TMP WHERE Id = @Id
		END
		
		
		--ориентиры (попытаться навести порядок в данных и закачать их)		
		--на момент написания в данных полях null-ов нет
		IF (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) is null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 5, [Ориентиры_район_города] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null
		END
		
		--на момент написания null-ы есть во всех полях
		IF (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) = 'нет'
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта]
			FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 5, [Ориентиры_район_города] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null
		END

		IF (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) <> 'нет' and (SELECT [Ориентиры_станция_метро] FROM #TMP WHERE Id = @Id) is not null
		BEGIN
			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 1, [Ориентиры_станция_метро]
			FROM #TMP WHERE Id = @Id and [Ориентиры_станция_метро] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 2, [Ориентиры_остановка_транспорта]
			FROM #TMP WHERE Id = @Id and [Ориентиры_остановка_транспорта] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 3, [Ориентиры_значимые_объекты] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_значимые_объекты] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 4, [Ориентиры_торговые_центры] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_торговые_центры] is not null

			INSERT INTO StaffDepartmentLandmarks([Version], DMDetailId, LandmarkId, [Description])
			SELECT 1, @DMDetailId, 5, [Ориентиры_район_города] 
			FROM #TMP WHERE Id = @Id and [Ориентиры_район_города] is not null
		END



		--режим работы подразделения
		SELECT @WorkDays = [Дни_работы_точки] FROM #TMP WHERE Id = @Id
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