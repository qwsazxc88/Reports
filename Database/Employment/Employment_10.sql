alter table PersonnelManagers add AccessGroupId INT null

create table AccessGroup (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_AccessGroup  primary key (Id)
)

create index PersonnelManagers_AccessGroup on PersonnelManagers (AccessGroupId)
alter table PersonnelManagers add constraint FK_PersonnelManagers_AccessGroup foreign key (AccessGroupId) references AccessGroup