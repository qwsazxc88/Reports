--Участие в тренинге.
--Участие в семинаре.
--Участие в производственном совещании.
--Проведение тренингового обучения для сотрудников Банка.
--Взыскание просроченной задолженности.
--Регистрация договора КП изъятого залога, получение документов БТИ, снятие обременения с изъятого залога.
--Осмотр недвижимости (предмет залога).
--Замещение сотрудника.
--Открытие нового офиса.
--Подведение итогов работы подразделения.
--Решение производственных вопросов.
delete from [dbo].[MissionGoal]
insert into [dbo].[MissionGoal] (Name) values (N'Участие в тренинге')
insert into [dbo].[MissionGoal] (Name) values (N'Участие в семинаре')
insert into [dbo].[MissionGoal] (Name) values (N'Участие в производственном совещании')
insert into [dbo].[MissionGoal] (Name) values (N'Проведение тренингового обучения для сотрудников Банка')
insert into [dbo].[MissionGoal] (Name) values (N'Взыскание просроченной задолженности')
insert into [dbo].[MissionGoal] (Name) values (N'Регистрация договора КП изъятого залога, получение документов БТИ, снятие обременения с изъятого залога')
insert into [dbo].[MissionGoal] (Name) values (N'Осмотр недвижимости (предмет залога)')
insert into [dbo].[MissionGoal] (Name) values (N'Замещение сотрудника')
insert into [dbo].[MissionGoal] (Name) values (N'Открытие нового офиса')
insert into [dbo].[MissionGoal] (Name) values (N'Подведение итогов работы подразделения')
insert into [dbo].[MissionGoal] (Name) values (N'Решение производственных вопросов')

-- Россия
-- СНГ
-- Москва
-- За рубеж
delete from [dbo].[MissionCountry] 
insert into [dbo].[MissionCountry] (Name) values (N'Россия')
insert into [dbo].[MissionCountry] (Name) values (N'СНГ')
insert into [dbo].[MissionCountry] (Name) values (N'Москва')
insert into [dbo].[MissionCountry] (Name) values (N'За рубеж')


-- Россия, кроме Москвы и стран СНГ (проживание с завтраком)
-- Россия, кроме Москвы и стран СНГ (проживание без завтрака)
-- Москва
-- Остальные страны
delete from [dbo].[MissionDailyAllowance] 
set identity_insert [dbo].[MissionDailyAllowance] ON
insert into [dbo].[MissionDailyAllowance] (Id,Name) values (1,N'Россия, кроме Москвы и стран СНГ (проживание с завтраком)')
insert into [dbo].[MissionDailyAllowance] (Id,Name) values (2,N'Россия, кроме Москвы и стран СНГ (проживание без завтрака)')
insert into [dbo].[MissionDailyAllowance] (Id,Name) values (3,N'Москва')
insert into [dbo].[MissionDailyAllowance] (Id,Name) values (4,N'Остальные страны')
set identity_insert [dbo].[MissionDailyAllowance] OFF

-- Россия, кроме Москвы и стран СНГ
-- Москва
delete from [dbo].[MissionResidence]
set identity_insert [dbo].[MissionResidence] ON
insert into [dbo].[MissionResidence] (Id,Name) values (1,N'Россия, кроме Москвы и стран СНГ')
insert into [dbo].[MissionResidence] (Id,Name) values (2,N'Москва')
set identity_insert [dbo].[MissionResidence] OFF

-- Проезд, длительностью менее 1 суток
-- Проезд, длительностью от 1 до 2 суток
-- Проезд, длительностью от 2 до 3 суток
-- Проезд, длительностью от 3 до 4 суток
-- Проезд, длительностью от 4 до 5 суток
-- Проезд длительностью от 5 суток
delete from [dbo].[MissionTrainTicketType]
set identity_insert [dbo].[MissionTrainTicketType] ON
insert into [dbo].[MissionTrainTicketType] (Id,Name) values (1,N'Проезд, длительностью менее 1 суток')
insert into [dbo].[MissionTrainTicketType] (Id,Name) values (2,N'Проезд, длительностью от 1 до 2 суток')
insert into [dbo].[MissionTrainTicketType] (Id,Name) values (3,N'Проезд, длительностью от 2 до 3 суток')
insert into [dbo].[MissionTrainTicketType] (Id,Name) values (4,N'Проезд, длительностью от 3 до 4 суток')
insert into [dbo].[MissionTrainTicketType] (Id,Name) values (5,N'Проезд, длительностью от 4 до 5 суток')
insert into [dbo].[MissionTrainTicketType] (Id,Name) values (6,N'Проезд длительностью от 5 суток')
set identity_insert [dbo].[MissionTrainTicketType] OFF

-- Перелет, длительностью менее 1 часа
-- Перелет, длительностью от 1 до 2 часов
-- Перелет, длительностью от 2 до 3 часов
-- Перелет, длительностью от 3 до 4 часов
-- Перелет, длительностью от 4 до 5 часов
-- Перелет, длительностью от 5 часов
delete from [dbo].[MissionAirTicketType]
set identity_insert [dbo].[MissionAirTicketType] ON
insert into [dbo].[MissionAirTicketType] (Id,Name) values (1,N'Перелет, длительностью менее 1 часа')
insert into [dbo].[MissionAirTicketType] (Id,Name) values (2,N'Перелет, длительностью от 1 до 2 часов')
insert into [dbo].[MissionAirTicketType] (Id,Name) values (3,N'Перелет, длительностью от 2 до 3 часов')
insert into [dbo].[MissionAirTicketType] (Id,Name) values (4,N'Перелет, длительностью от 3 до 4 часов')
insert into [dbo].[MissionAirTicketType] (Id,Name) values (5,N'Перелет, длительностью от 4 до 5 часов')
insert into [dbo].[MissionAirTicketType] (Id,Name) values (6,N'Перелет, длительностью от 5 часов')
set identity_insert [dbo].[MissionAirTicketType] OFF


delete from [dbo].[MissionGraid]
set identity_insert [dbo].[MissionGraid] ON
insert into [dbo].[MissionGraid] (Id,Name) values (1,N'G1')
insert into [dbo].[MissionGraid] (Id,Name) values (2,N'G2')
insert into [dbo].[MissionGraid] (Id,Name) values (3,N'G3')
insert into [dbo].[MissionGraid] (Id,Name) values (4,N'G4')
set identity_insert [dbo].[MissionGraid] OFF

set identity_insert [dbo].[MissionType] ON
insert into  [dbo].[MissionType] (Id,[Version],Code,Name) values (4,1,N'1',N'Москва')
set identity_insert  [dbo].[MissionType] OFF

