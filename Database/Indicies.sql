set identity_insert  [Role] on
INSERT INTO [Role] (Id,[Name],Version) values (1,'Администратор',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'Сотрудник',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'Руководитель',1) 
INSERT INTO [Role] (Id,[Name],Version) values (8,'Кадровик',1) 
INSERT INTO [Role] (Id,[Name],Version) values (16,'Бюджет',1) 
INSERT INTO [Role] (Id,[Name],Version) values (32,'Отусорсинг',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'Не одобрена сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'Одобрена сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'Одобрена руководителем',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'Одобрена кадровиком',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'Выгружена в 1С',1)
set identity_insert  [RequestStatus] off 

declare @OrganizationId int
declare @Organization1Id int
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (1,'Тестовая организация',1)	
set @OrganizationId = @@Identity
INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (2,'Тестовая организация 1',1)	
set @Organization1Id = @@Identity	
	

declare @DepartmentId int
declare @Department1Id int
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (1,'Тестовый департамент',1)		
set @DepartmentId = @@Identity	
INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (2,'Тестовый департамент 1',1)		
set @Department1Id = @@Identity	


declare @PositionId int
declare @Position1Id int
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (1,'Тестовая должность',1)		
set @PositionId = @@Identity
INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (2,'Тестовая должность 1',1)		
set @Position1Id = @@Identity	
	

INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (51,'Дополнительный учебный отпуск без оплаты #1203',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (31,'Оплата дня сдачи крови и доп. дня отдыха донорам #1125',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'Оплата дополнительного отпуска по календарным дням #1207',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (32,'Оплата дополнительных выходных дней по уходу за детьми - инвалидами #1504',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'Оплата отпуска по календарным дням #1201',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (42,'Оплата отпуска по шестидневке #1202',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (41,'Оплата учебного отпуска по календарным дням #1204',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (53,'Отпуск без оплаты согласно ТК РФ #1205',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (54,'Отпуск за свой счет #1206',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (23,'Отпуск по беременности и родам #1501',1)			
INSERT INTO [dbo].[VacationType]  ([Code],[Name],Version) values (52,'Отпуск по уходу за ребенком без оплаты #1802',1)		

INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (51,'Дополнительный учебный отпуск без оплаты #1203',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (53,'Отпуск без оплаты согласно ТК РФ #1205',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (54,'Отпуск за свой счет#1206',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (10023,'Отсутствие по болезни #1803',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (55,'Отсутствие по невыясненной причине #1806',1)	
INSERT INTO [dbo].[AbsenceType]  ([Code],[Name],Version) values (56,'Прогул, простой по вине работника #1807',1)	

INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (25,'БЛ по травме в быту (не оплачивается) #1805',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (26,'Доплата по больничным листам #1402',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (22,'Оплата БЛ по травме на производстве #1403',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (25,'Оплата больничных листов за счет работодателя #1426',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (21,'Оплата больничных листов #1469',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (10023,'Отсутствие по болезни #1803',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (55,'Отсутствие по невыясненной причине #1806',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (71,'Пособие по уходу за ребёнком до 1.5 лет #1502',1)
INSERT INTO [dbo].[SicklistType]  ([Code],[Name],Version) values (72,'Пособие по уходу за ребёнком до 3 лет #1503',1)

INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (13,'Доплата за работу в праздники и выходные #1107',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (12,'Оплата праздничных и выходных дней #1106',1)
INSERT INTO [dbo].[HolidayWorkType]  ([Code],[Name],Version) values (null,'Отгул',1)

INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('Командировку по России',1)
INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('Командировку по СНГ',1)
INSERT INTO [dbo].[MissionType]  ([Name],Version) values ('Командировку  за рубеж',1)

INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('По совместительству',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('На основное место работы',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('По срочному трудовому договору',1)
INSERT INTO [dbo].[EmploymentType]  ([Name],Version) values ('Переводом',1)

INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка   0,2 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,001',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,01',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,1',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,16',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,2 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,55 ставки',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,68 ставки (27,2 часа)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,7 ставки (28 часов)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0,8 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('0.8',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('1 час',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('10 часов (0,25)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('10-часовая',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('12 ч (0,3ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('14ч (0,35 ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('16 ч (0,4ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('16,8 ч (0,42 ставки)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('20 часов (0,5 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('24 ч(0,6ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('25 (0,625)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('29,2 часа (0.73 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('30 (0,75ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('32 часа ( 0.8ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('33.6 часа ( 0.84ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('34,8 часа ( 0,87 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('35 часов (0,87 ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('35,6 часа (0,89ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('36,4 часа ( 0,91ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('36.8 часа  (0,92ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('37,5 часов (0,94 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('38,4 часа (0,96ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('39.6 часа ( 0.99ст.)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('4 часа (0,1 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('6,4 часа (0,16 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('6,8 часа (0,17 ст)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('График 15ч/н (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('график 2 через 2 2смена',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('график 2 через 2 по 11 часов 1смена',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('График 25ч/н (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график 20часов неделя (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Основной график 40часов неделя',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка (30 часов)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 0.4ст',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('пятидневка 10часов (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 20 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 30 часов',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 32 часа',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 35',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 35 часов (Иваново)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Пятидневка 37,5',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Сменный',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Сокращенный день по 7 часов (Ярославль)',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Сум. учет раб. времени',1)
INSERT INTO [dbo].[EmploymentHoursType]  ([Name],Version) values ('Суммированный учет',1)

INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за выслугу лет рабочим и служащим #1114','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за квалификацию #1115','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка за разъездной характер работы #1116','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка Персональная #1117','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00002','Надбавка территориальная #1123','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','Районный коэффициент #1301','Процентом Зависимое второго уровня',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values (null,'Расширение зоны обслуживания #1120','По месячной тарифной ставке по часам Первичное',1)
INSERT INTO [dbo].[EmploymentAddition]  ([Code],[Name],[CalculationMethod],Version) values ('00070','Северная надбавка 1 #1302','Процентом Зависимое второго уровня',1)

--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Заявление работника',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Служебная записка',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Распоряжение руководителя',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Медицинское заключение',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Приказ руководителя',1)
--INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('Справка о смерти',1)

INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 1 ст. 81 ТК','Ликвидация',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 1 части 1 ст. 77 ТК','Соглашение сторон.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 2 ст. 77 ТК','Истечение срока трудового договора.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 2 части 1 ст. 81 ТК','Сокращение численности штата',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 3 ст. 77 ТК','Расторжение трудового договора по инициативе работника.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 4 ст. 77 ТК','Расторжение трудового договора по инициативе работодателя',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 5 ст. 77 ТК','Перевод работника на другую работу к другому работодателю',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 5 части первой ст. 77','Перевод работника на выборную должность',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 6 ст. 81 ТК','Расторжение трудового договора по инициативе работодателя за прогул.',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 6 ст.83 ТК','Смерть  работника  либо работодателя - физ. лица, а также признание судом работника либо работодателя - физ. лица умершим',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 7 ч. 1 ст.81 ТК РФ','Трудовой договор расторгнут в связи с совершением виновных действий работником',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п. 7 части 1 ст. 77','Отказ работника от продолжения работы в связи с изменением определенных сторонами условий ТД',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п.3 части 1 ст. 77 ТК РФ','Собственное желание',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п.5 части первой ст. 83 ТК РФ','Трудовой договор прекращен в связи с признанием работника полностью неспособным к трудовой деятельности в соответствии с медицинским заключением',1)
INSERT INTO [dbo].[DismissalType]  ([Name],[Reason],Version) values ('п.6 ч.1 ст. 83','Трудовой договор прекращен в связи со смертью работника',1)

INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (2,'Оклад по часам #1102','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (3,'Оплата по часовому тарифу #1103','По часовой тарифной ставке',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (66,'Оплата почасового простоя от оклада по часам #1707','По месячной тарифной ставке по часам',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (67,'Оплата почасового простоя по часовому тарифу# 1708','По часовой тарифной ставке',1)
INSERT INTO [dbo].[TimesheetCorrectionType]  ([Code],[Name],[Reason],Version) values (68,'Почасовой простой по вине работодателя #1702','По среднему заработку',1)



--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('Тест 1',1)
--INSERT INTO [dbo].[DismissalCompensationType]  ([Name],Version) values ('Тест 2',1)


INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('В размере ММОТ',1)
INSERT INTO [dbo].[SicklistPaymentRestrictType] ([Name],Version) values ('Общее, в соответствии с Законом о бюджете ФСС',1)

--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'БЛ по травме в быту (не оплачивается) #1805','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (26,'Доплата по больничным листам #1402','Доплата до среднего заработка ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (22,'Оплата БЛ по травме на производстве #1403','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (25,'Оплата больничных листов за счет работодателя #1426','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (21,'Оплата больничных листов #1469','По среднему заработку ФСС',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10024,'Отсутствие по болезни (по беременности и родам) #1804','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (10023,'Отсутствие по болезни #1803','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (55,'Отсутствие по невыясненной причине #1806','Нулевая сумма',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (71,'Пособие по уходу за ребёнком до 1.5 лет #1502','Пособие по уходу за ребенком до 1.5 лет',1)
--INSERT INTO [dbo].[SicklistPaymentType]  ([Code],[Name],[PaymentMethod],Version) values (72,'Пособие по уходу за ребёнком до 3 лет #1503','Пособие по уходу за ребенком до 3 лет',1)

INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (60,3,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (80,2,1)
INSERT INTO [dbo].[SicklistPaymentPercent]  ([SicklistPercent],SortOrder,Version) values (100,1,1)



set identity_insert  [TimesheetStatus] on
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (1,1,'Я','Явка')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (2,1,'Б','Временная нетрудоспособность с назначением пособия согласно законодательству')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (3,1,'Т','Временная нетрудоспособность без назнач. пособия в случаях, предусм. законодательством')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (4,1,'ВЧ','Вечерние часы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (5,1,'Н','Ночные часы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (6,1,'В','Выходные и нерабочие дни')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (7,1,'К','Командировка')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (8,1,'ОТ','Отпуск')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (9,1,'ОЗ','Отпуск без сохранения заработной платы в случаях, предусмотренных законодательством')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (10,1,'ДО','Отпуск без сохранения заработной платы, предоставляемый сотр. по разреш. работодателя')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (11,1,'Р','Отпуск по беременности и родам (отпуск в связи с усыновлением новорожд. ребенка)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (12,1,'ОЖ','Отпуск по уходу за ребенком до достижения им возраста трех лет')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (13,1,'РВ','Продолжительность работы в выходные и нерабочие, праздничные дни')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (14,1,'С','Продолжительность сверхурочной работы')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (15,1,'ПР','Прогулы (отсутствие на рабочем месте без уваж. причин в теч. времени, уст. законодательством)')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (16,1,'НН','Неявки по невыясненным причинам')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (17,1,'ВП','Простои по вине сотрудника')
INSERT INTO [TimesheetStatus] (Id,Version,ShortName,Name) values (18,1,'РП','Время простоя по вине работодателя')
set identity_insert  [TimesheetStatus] off


declare @typeId int
declare @firstTypeId int
declare @firstSubTypeId int
INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Отпуск',1) 
set @typeId = @@Identity
set @firstTypeId = @typeId
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Ежегодный',1,@typeId) 
set @firstSubTypeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Дополнительный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Учебный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за ребенком',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Без сохранения ЗП',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Донорские дни',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Командировка',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По РФ',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По СНГ',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('За рубеж',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Больничный',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По нетрудоспособности',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за ребенком',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Травма на производстве',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Санитарно — курортный',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По беременности и родам',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По уходу за взрослым',1,@typeId) 


INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Отсутствие на рабочем месте',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Прогул',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По невыясненной причине',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Увольнение',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По собственному желанию',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По сокращению штата',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По соглашению сторон',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('По инициативе работодателя',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('В связи с переводом',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Перемещение',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Изменение оплаты труда',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('места работы',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('должности',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Прочее',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Простой по вине работодателя',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Мат. помощь',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Погребение',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Регистрация брака (прочее)',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Рождение, усыновление',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие по уходу за реб. 3х лет',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие по уходу за реб. 1,5х лет',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Сохраняемый заработок на время трудоустройства',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Выплата за счет ФСС',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС в связи со смертью',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при постановке на учет в ранние сроки беременности',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при рождении ребенка',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Пособие ФСС при усыновлении ребенка',1,@typeId) 

INSERT INTO [EmployeeDocumentType] ([Name],Version) values ('Удержание',1) 
set @typeId = @@Identity
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Сотовая связь',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Исполнительный лист',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Обслуживание авто',1,@typeId) 
INSERT INTO [EmployeeDocumentSubType] ([Name],Version,TypeId) values ('Налоговый вычет',1,@typeId) 


-- User
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]		, [IsNew]	 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'Администратор',             1,		  null ,          1,           'АА0000000001' , 0)
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew]) 
VALUES			   (1,       	0              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'Руководитель',              1,         null								, 4,		   'АВ0000000001' , 0)
set @managerId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@managerId,@Department1Id,1)
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code]  , [IsNew]) 
VALUES			   (1,       	0              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'Кадровик',                    1,         null								, 8,		   'АГ0000000001' , 0)
set @personnelId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@DepartmentId,1)
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@personnelId,@Department1Id,1)
declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'Бюджет',           1,         null								        , 16,		   'АГ0000000001' , 0)
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , [IsNew]) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'Аутсорсинг',                        1,         null,              32,		       'АД0000000001' , 0)
set @outsorsingId = @@Identity
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         PersonnelManagerId, OrganizationId,/*DepartmentId,*/PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'Пользователь',                   1,         null            , 2,		   'АБ0000000001' ,  @managerId,       @personnelId,       @OrganizationId,/*@DepartmentId,*/@PositionId , 0)
set @userId = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@userId,@DepartmentId,1)

declare @user1Id int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId,            OrganizationId,/*DepartmentId,*/PositionId , [IsNew]) 
VALUES			   (1,       	0              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'Иванов Иван Иванович',            1,         null            , 2,		   'АЕ0000000001' ,  @managerId,       @personnelId,@Organization1Id,/*@Department1Id,*/@Position1Id , 0)
set @user1Id = @@Identity
INSERT INTO UserToDepartment (UserId,DepartmentId,Version) values (@user1Id,@Department1Id,1)

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId , [IsNew]) 
VALUES			   (1,       	0              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'Петров Петр Петрович',         1,         null            , 2,		   'АЖ0000000001' ,  @managerId,       @personnelId , 0)

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     'Тестовый',  @firstTypeId, @firstSubTypeId, @userId, 0)
