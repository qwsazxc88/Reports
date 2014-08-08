alter table Managers drop column Schedule
alter table Managers add ScheduleId INT null

create table Schedule (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_Schedule  primary key (Id)
)

create index Managers_Schedule on Managers (ScheduleId)
alter table Managers add constraint FK_Managers_Schedule foreign key (ScheduleId) references Schedule