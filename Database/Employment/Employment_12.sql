alter table PersonnelManagers drop column PersonalAccountContractor
alter table PersonnelManagers add PersonalAccountContractorId INT not null

create table PersonalAccountContractor (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_PersonalAccountContractor  primary key (Id)
)

create index PersonnelManagers_PersonalAccountContractor on PersonnelManagers (PersonalAccountContractorId)
alter table PersonnelManagers add constraint FK_PersonnelManagers_PersonalAccountContractor foreign key (PersonalAccountContractorId) references PersonalAccountContractor