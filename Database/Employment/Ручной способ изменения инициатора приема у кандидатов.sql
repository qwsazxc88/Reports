--РУЧНОЙ СПОСОБ ИЗМЕНЕНИЯ ИНИЦИАТОРА У КАНДИДАТОВ
--ВАЖНО - скрипт меняет инициатора у всех кандидатов по ветке подразделений. 
--по номеру заявки в приеме выявляем учетку руководителя с ролью сотрудника
select * from users where id in (select UserId from EmploymentCandidate where id in (3486, 3423, 3421, 3417, 3413))

--при необходимости формируем руководительскую учетку, чтобы не ждать синхронизации
--создание руководительских учеток в ручном режиме
--необходимо правильно указать подразделение
/*
insert into Users(Version, IsFirstTimeLogin, IsActive, IsNew, Login, Password, code, Name, Email, Comment, RoleId, OrganizationId, PositionId, DepartmentId, GivesCredit, Level, IsMainManager, Salary)
--Котенко Марина Николаевна
values(1, 0, 1, 0, N'0000037264R', N'G6QB7xdOR', N'0000037264R', N'Котенко Марина Николаевна', N'KotenkoMN@sovcombank.ru', N'Руководитель (5)', 4, 3, 463, 11935, 0, 5, 1, 0)
--Крылова Ольга Викторовна
,(1, 0, 1, 0, N'0000037265R', N'vKfXrfXCR', N'0000037265R', N'Крылова Ольга Викторовна', N'KrylovaOV@sovcombank.ru', N'Руководитель (5)', 4, 3, 463, 11988, 0, 5, 1, 0)
--Назаркина Татьяна Владимировна
,(1, 0, 1, 0, N'0000037266R', N'7v0Jc7Q2R', N'0000037266R', N'Назаркина Татьяна Владимировна', N'NazarkinaTV@sovcombank.ru', N'Руководитель (5)', 4, 3, 463, 11931, 0, 5, 1, 0)
--Барышева Екатерина Сергеевна
,(1, 0, 1, 0, N'0000037257R', N'QBzPSlj1R', N'0000037257R', N'Барышева Екатерина Сергеевна', N'BaryshevaES@sovcombank.ru', N'Руководитель (5)', 4, 3, 463, 11992, 0, 5, 1, 0)
*/

--выявляем для этих руководителей ветку подразделений, в данном случае это ветки начинаются с 5 уровня
select A.Id, A.Name, A.RoleId, A.DepartmentId, b.Name, b.ItemLevel, c.id, c.Name, c.ItemLevel
from users as A
inner join Department as b on b.id = a.DepartmentId
inner join Department as c on b.Path like c.Path + '%' and c.ItemLevel = 5
where A.id in (select UserId from EmploymentCandidate where id in (3486, 3423, 3421, 3417, 3413))



--дальше запускать для каждого руководителя отдельно
declare @UserId int, @DepartmentId int

select @UserId = Id, @DepartmentId = DepartmentId from users where name like 'Котенко Марина Николаевна%' and RoleId = 4 and IsActive = 1
--select * from users where name like 'Крылова Ольга Викторовна%'
--select * from users where name like 'Назаркина Татьяна Владимировна%'
--select * from users where name like 'Барышева Екатерина Сергеевна%'

--проверочный запрос, показывает наличие неоформленных кандидатов для данного руководителя
select * from EmploymentCandidate 
where UserId in(select id from Users 
								where DepartmentId in (select b.Id from Department as a
																			 inner join Department as b on B.Path like A.Path + '%' and b.ItemLevel = 7
																			 where a.id = @DepartmentId))
and Status <> 8

--select 8 + 3 + 7 + 9
--делаем текущего руководителя инициатором приема для кандидатов из его ветки подразделений
update EmploymentCandidate set AppointmentCreatorId = @UserId
where UserId in(select id from Users 
								where DepartmentId in (select b.Id from Department as a
																			 inner join Department as b on B.Path like A.Path + '%' and b.ItemLevel = 7
																			 where a.id = @DepartmentId))
and Status <> 8

--567