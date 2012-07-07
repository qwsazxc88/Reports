set identity_insert  [Role] on
INSERT INTO [Role] (Id,[Name],Version) values (1,'Администратор',1) 
INSERT INTO [Role] (Id,[Name],Version) values (2,'Сотрудник',1) 
INSERT INTO [Role] (Id,[Name],Version) values (3,'Руководитель',1) 
INSERT INTO [Role] (Id,[Name],Version) values (4,'Кадровик',1) 
INSERT INTO [Role] (Id,[Name],Version) values (5,'Бюджет',1) 
INSERT INTO [Role] (Id,[Name],Version) values (6,'Отусорсинг',1)
set identity_insert  [Role] off 

set identity_insert  [RequestStatus] on
INSERT INTO [RequestStatus] (Id,[Name],Version) values (1,'Не одобрен сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (2,'Одобрен сотрудником',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (3,'Одобрен руководителем',1) 
INSERT INTO [RequestStatus] (Id,[Name],Version) values (4,'Одобрен кадровиком',1)
INSERT INTO [RequestStatus] (Id,[Name],Version) values (5,'Выгружен в 1С',1)
set identity_insert  [RequestStatus] off 


INSERT INTO [dbo].[Organization]  ([Code],[Name],Version) values (1,'Тестовая организация',1)		

INSERT INTO [dbo].[Department]  ([Code],[Name],Version) values (1,'Тестовый департамент',1)		

INSERT INTO [dbo].[Position]  ([Code],[Name],Version) values (1,'Тестовая должность',1)		

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


INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'Я','Явка')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'Б','Временная нетрудоспособность с назначением пособия согласно законодательству')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'Т','Временная нетрудоспособность без назнач. пособия в случаях, предусм. законодательством')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ВЧ','Вечерние часы')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'Н','Ночные часы')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'В','Выходные и нерабочие дни')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'К','Командировка')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ОТ','Отпуск')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ОЗ','Отпуск без сохранения заработной платы в случаях, предусмотренных законодательством')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ДО','Отпуск без сохранения заработной платы, предоставляемый сотр. по разреш. работодателя')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'Р','Отпуск по беременности и родам (отпуск в связи с усыновлением новорожд. ребенка)')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ОЖ','Отпуск по уходу за ребенком до достижения им возраста трех лет')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'РВ','Продолжительность работы в выходные и нерабочие, праздничные дни')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'С','Продолжительность сверхурочной работы')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ПР','Прогулы (отсутствие на рабочем месте без уваж. причин в теч. времени, уст. законодательством)')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'НН','Неявки по невыясненным причинам')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'ВП','Простои по вине сотрудника')
INSERT INTO [TimesheetStatus] (Version,ShortName,Name) values (1,'РП','Время простоя по вине работодателя')


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
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code]			 ) 
VALUES			   (1,       		0               ,'admin','adminadmin' ,	GetDate(),      N'Администратор',             1,		  null ,          1,           'АА0000000001' )
declare @managerId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                         Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	1              ,'manager' ,'manager'  ,	'2008-12-01 15:13:25:000',  N'Руководитель',              1,         null								, 3,		   'АВ0000000001')
set @managerId = @@Identity
declare @personnelId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,               Name,                          Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	1              ,'personnel' ,'personnel'  ,	'2008-12-01 15:13:25:000',    N'Кадровик',                    1,         null								, 4,		   'АГ0000000001')
set @personnelId = @@Identity
declare @budgetId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,              Name,               Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'budget' ,'budget'  ,	'2008-12-01 15:13:25:000',       N'Бюджет',           1,         null								        , 5,		   'АГ0000000001')
set @budgetId = @@Identity
declare @outsorsingId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,                       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] ) 
VALUES			   (1,       	0              ,'outsorsing' ,'outsorsing'  ,	'2008-12-01 15:13:25:000',       N'Аутсорсинг',                        1,         null,              6,		       'АД0000000001')
set @outsorsingId = @@Identity
declare @userId int
INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                        Version,  [DateRelease]    , [RoleId],      [Code] ,             ManagerId,         PersonnelManagerId) 
VALUES			   (1,       	1              ,'user' ,'user'  ,	'2008-12-01 15:13:25:000',    N'Пользователь',                   1,         null            , 2,		   'АБ0000000001' ,  @managerId,       @personnelId)
set @userId = @@Identity

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,       Name,                           Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId) 
VALUES			   (1,       	1              ,'ivanov' ,'ivanov'  ,	'2008-12-01 15:13:25:000',N'Иванов Иван Иванович',            1,         null            , 2,		   'АЕ0000000001' ,  @managerId,       @personnelId)

INSERT INTO [Users] (IsActive,IsFirstTimeLogin, Login ,Password,DateAccept                ,            Name,                         Version,  [DateRelease]    , [RoleId],      [Code] , ManagerId,PersonnelManagerId) 
VALUES			   (1,       	1              ,'petrov' ,'petrov'  ,	'2008-12-01 15:13:25:000',      N'Петров Петр Петрович',         1,         null            , 2,		   'АЖ0000000001' ,  @managerId,       @personnelId)

 
INSERT INTO DBVERSION (Version) VALUES('1.0.0.1')


--insert into dbo.Document
--([Version],LastModifiedDate, Comment, TypeId,          SubtypeId,     UserId, SendEmailToBilling)
--values (1,'2011-10-22',     'Тестовый',  @firstTypeId, @firstSubTypeId, @userId, 0)
