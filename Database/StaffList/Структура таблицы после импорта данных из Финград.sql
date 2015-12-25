USE [TestExpress]
GO

/****** Object:  Table [dbo].[Fingrad_csv]    Script Date: 27.08.2015 14:41:05 ******/
DROP TABLE [dbo].[Fingrad_csv]
GO

/****** Object:  Table [dbo].[Fingrad_csv]    Script Date: 27.08.2015 14:41:05 ******/
SET ANSI_NULLS ON
GO

--НУЖНО ПРИВЕСТИ КОЛОНКИ К СЛЕДУЮЩЕМУ ВИДУ

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fingrad_csv](
	[Код_подразделения] [nvarchar](20) NULL,
	[Причины_внесения_в_справочник] [nvarchar](50) NULL,
	[Прежний_код_подразделения] [nvarchar](50) NULL,
	[Сокращенное_наименование] [nvarchar](250) NULL,
	[Индекс] [nvarchar](6) NULL,
	[Населенный_пункт] [nvarchar](50) NULL,
	[Субъект_федерации] [nvarchar](50) NULL,
	[Улица_дом] [nvarchar](100) NULL,
	--[Адрес_УС_если_не_совпадает_с_адресом_офиса] [nvarchar](500) NULL,
	[Дата_открытия_офиса] [datetime] NULL,
	[Дата_закрытия_офиса] [datetime] NULL,
	[Тип_подразделения] [nvarchar](50) NULL,
	[Код_СВКредит] [nvarchar](50) NULL,
	--[Код_ФЕС] [nvarchar](50) NULL,
	[Код_РБС] [nvarchar](50) NULL,
	--[Код_Инверсия] [nvarchar](50) NULL,
	[РП_привязка] [nvarchar](250) NULL,
	[РП_привязка_Дирекция] [nvarchar](50) NULL,
	[Бизнес_группа] [nvarchar](250) NULL,
	[Бизнес_группа_Управление_Дирекции] [nvarchar](250) NULL,
	[Бизнес_группа_ФИО_РБГ] [nvarchar](250) NULL,
	[Приказы] [nvarchar](250) NULL,
	[Руководитель_РП] [nvarchar](max) NULL,
	[Кол_во_запущенных_кэшинов] [int] NULL,
	[Дата_запуска_кэшина_первая] [datetime] NULL,
	[Инкассирующее_подразделение_кэшина] [nvarchar](250) NULL,
	[Кол_во_запущенных_банкоматов_всего] [int] NULL,
	[Кол_во_запущенных_банкоматов_с_функцией_кэшин] [int] NULL,
	[Кол_во_запущенных_банкоматов_с_функцией_ресайклинг] [int] NULL,
	[Дата_запуска_банкомата_первая] [datetime] NULL,
	[Инкассирующее_подразделение_банкомата] [nvarchar](250) NULL,
	--[Арендованные_УС] [nvarchar](50) NULL,
	[Блокировка] [nvarchar](50) NULL,
	[Идентификация_сетевого_магазина] [nvarchar](50) NULL,
	[№_телефона] [nvarchar](250) NULL,
	[Режим_работы_офиса_доступа_к_УС] [nvarchar](500) NULL,
	[Режим_работы_кассы] [nvarchar](500) NULL,
	[Ориентиры_станция_метро] [nvarchar](max) NULL,
	[Ориентиры_остановка_транспорта] [nvarchar](max) NULL,
	[Ориентиры_значимые_объекты] [nvarchar](max) NULL,
	[Ориентиры_торговые_центры] [nvarchar](max) NULL,
	[Ориентиры_район_города] [nvarchar](max) NULL,
	[Наличие_кассы] [nvarchar](50) NULL,
	[Операции] [nvarchar](max) NULL,
	[Обслуживание_ЮЛ] [nvarchar](50) NULL,
	--[Примечание] [nvarchar](250) NULL,
	[Дни_работы_точки] [nvarchar](7) NULL,
	[Дата_начала_простоя_точки] [datetime] NULL,
	[Дата_возобновления_работы_точки] [datetime] NULL,
	--[J_шники_устройств] [nvarchar](50) NULL,
	[СКБ_GE] [nvarchar](50) NULL,
	--[Код_SuperВизор] [nvarchar](50) NULL,
	--[ФИО_заменяющее_РБГ_в_т_ч_для_заказа_ДС] [nvarchar](50) NULL,
	[Арендованное_помещение] [nvarchar](50) NULL,
	[Сумма_ежемесячного_платежа] [numeric](18, 2) NULL,
	[Площадь_подразделения] [numeric](18, 2) NULL,
	--[Долгосрочная_аренда] [nvarchar](500) NULL,
	--[Дата_процедуры] [datetime] NULL,
	--[Вид_процедуры] [nvarchar](100) NULL,
	--[Полное_наименование] [nvarchar](250) NULL,
	--[Дирекция_РП_привязка] [nvarchar](50) NULL,
	--[Управление_Дирекции_Бизнес_группа] [nvarchar](250) NULL,
	--[Реквизиты_договора] [nvarchar](250) NULL,
	--[Логистический_центр] [nvarchar](250) NULL,
	--[Код_ХД] [nvarchar](50) NULL,
	--[Код_1С] [nvarchar](50) NULL,
	--[Бизнес_группа_СНИЛС_РБГ] [nvarchar](50) NULL,
	--[Контрагент] [nvarchar](250) NULL,
	--[Наименование_в_СВК_ХД] [nvarchar](250) NULL,
	[Установленное_ПО_в_ВСП] [nvarchar](250) NULL,
	--[СНИЛС_РБГ_Бизнес_группа] [nvarchar](50) NULL,
	--[СНИЛС_Управляющего_Управление_Дирекции] [nvarchar](50) NULL,
	[Режим_работы_Банкомата] [nvarchar](250) NULL,
	[Режим_работы_Cash_in] [nvarchar](250) NULL,
	[Код_депозитной_точки] [nvarchar](50) NULL,
	[Front_Back1] [nvarchar](50) NULL,
	[ID_Дирекции] [nvarchar](50) NULL,
	--[Код_Террасофт] [nvarchar](50) NULL,
	[ID_Управления_Дирекции_Управление_Дирекции] [nvarchar](50) NULL,
	[РП_привязка_Код_РП_в_финград] [nvarchar](50) NULL,
	[Бизнес_группа_ID_Бизнес_группа] [nvarchar](50) NULL,
	[Бизнес_группа_ФИО_заменяющее_РБГ_в_т_ч_для_заказа_ДС] [nvarchar](250) NULL
	--[Инкассирующее_подразделение_кэшина_Дирекция] [nvarchar](50) NULL,
	--[Инкассирующее_подразделение_банкомата_Дирекция] [nvarchar](50) NULL,
	--[Инкассирующее_подразделение_банкомата_Код_РП_в_финград] [nvarchar](50) NULL,
	--[Инкассирующее_подразделение_кэшина_Код_РП_в_финград] [nvarchar](50) NULL
	--[Ответственный_за_кассовый_лимит] [nvarchar](50) NULL,
	--[Блокировка2] [nvarchar](50) NULL,
	--[Код_РП_в_Финград_РП_Привязка] [nvarchar](50) NULL,
	--[Бизнес_группа_Управление_Дирекции1] [nvarchar](250) NULL,
	--[ID_Бизнес_группа_Бизнес_группа] [nvarchar](50) NULL,
	--[ID_Дирекции_Дирекция] [nvarchar](50) NULL,
	--[Статус_подразделения] [nvarchar](50) NULL,
	--[Тип_подразделения_Front_Back] [nvarchar](50) NULL,
	--[Контрагент_Контактная_информация] [nvarchar](250) NULL,
	--[Контрагент_ИНН] [nvarchar](50) NULL,
	--[Контрагент_КПП] [nvarchar](50) NULL,
	--[Контрагент_Адрес] [nvarchar](250) NULL,
	--[Бизнес_группа_Дирекция] [nvarchar](50) NULL,
	
	
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


