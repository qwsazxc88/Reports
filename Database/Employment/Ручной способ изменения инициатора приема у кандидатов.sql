--������ ������ ��������� ���������� � ����������
--����� - ������ ������ ���������� � ���� ���������� �� ����� �������������. 
--�� ������ ������ � ������ �������� ������ ������������ � ����� ����������
select * from users where id in (select UserId from EmploymentCandidate where id in (3418, 3496, 3589))--in (3486, 3423, 3421, 3417, 3413))

--��� ������������� ��������� ���������������� ������, ����� �� ����� �������������
--�������� ���������������� ������ � ������ ������
--���������� ��������� ������� �������������
/*
insert into Users(Version, IsFirstTimeLogin, IsActive, IsNew, Login, Password, code, Name, Email, Comment, RoleId, OrganizationId, PositionId, DepartmentId, GivesCredit, Level, IsMainManager, Salary)
--������� ����� �������������
values(1, 0, 1, 0, N'0000037273R', N'dQdSiLO1R', N'0000037273R', N'������� ����� �������������', N'CHernovaIA@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11977, 0, 5, 1, 0)
--������� �������� ����������
,(1, 0, 1, 0, N'0000037311R', N'MIM9amcIR', N'0000037311R', N'������� �������� ����������', N'MileevaGR@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11937, 0, 5, 1, 0)
--����� ������� ��������
,(1, 0, 1, 0, N'0000037288R', N'Fktyf2015R', N'0000037288R', N'����� ������� ��������', N'ZajkoTI@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11936, 0, 5, 1, 0)
--������� ������ ����������
values(1, 0, 1, 0, N'0000037264R', N'G6QB7xdOR', N'0000037264R', N'������� ������ ����������', N'KotenkoMN@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11935, 0, 5, 1, 0)
--������� ����� ����������
,(1, 0, 1, 0, N'0000037265R', N'vKfXrfXCR', N'0000037265R', N'������� ����� ����������', N'KrylovaOV@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11988, 0, 5, 1, 0)
--��������� ������� ������������
,(1, 0, 1, 0, N'0000037266R', N'7v0Jc7Q2R', N'0000037266R', N'��������� ������� ������������', N'NazarkinaTV@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11931, 0, 5, 1, 0)
--�������� ��������� ���������
,(1, 0, 1, 0, N'0000037257R', N'QBzPSlj1R', N'0000037257R', N'�������� ��������� ���������', N'BaryshevaES@sovcombank.ru', N'������������ (5)', 4, 3, 463, 11992, 0, 5, 1, 0)
*/

--�������� ��� ���� ������������� ����� �������������, � ������ ������ ��� ����� ���������� � 5 ������
select A.Id, A.Name, A.RoleId, A.DepartmentId, b.Name, b.ItemLevel, c.id, c.Name, c.ItemLevel
from users as A
inner join Department as b on b.id = a.DepartmentId
inner join Department as c on b.Path like c.Path + '%' and c.ItemLevel = 5
where A.id in (select UserId from EmploymentCandidate where id in (3418, 3496, 3589)) --(3486, 3423, 3421, 3417, 3413))



--������ ��������� ��� ������� ������������ ��������
declare @UserId int, @DepartmentId int

select @UserId = Id, @DepartmentId = DepartmentId from users where name like '�������� ��������� ���������%' and RoleId = 4 and IsActive = 1
--select * from users where name like '������� ����� ����������%'
--select * from users where name like '��������� ������� ������������%'
--select * from users where name like '�������� ��������� ���������%'

--����������� ������, ���������� ������� ������������� ���������� ��� ������� ������������
select @UserId, * from EmploymentCandidate 
where UserId in(select id from Users 
								where DepartmentId in (select b.Id from Department as a
																			 inner join Department as b on B.Path like A.Path + '%' and b.ItemLevel = 7
																			 where a.id = @DepartmentId))
and Status <> 8

--select 8 + 3 + 7 + 9
--������ �������� ������������ ����������� ������ ��� ���������� �� ��� ����� �������������
update EmploymentCandidate set AppointmentCreatorId = @UserId
where UserId in(select id from Users 
								where DepartmentId in (select b.Id from Department as a
																			 inner join Department as b on B.Path like A.Path + '%' and b.ItemLevel = 7
																			 where a.id = @DepartmentId))
and Status <> 8

--567