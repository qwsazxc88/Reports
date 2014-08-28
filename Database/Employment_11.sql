alter table GeneralInfo drop column DisabilityDegree
alter table GeneralInfo add DisabilityDegreeId INT null

create table DisabilityDegree (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(10) null,
  Name NVARCHAR(128) null,
  constraint PK_DisabilityDegree  primary key (Id)
)

create index GeneralInfo_DisabilityDegree on GeneralInfo (DisabilityDegreeId)
alter table GeneralInfo add constraint FK_GeneralInfo_DisabilityDegree foreign key (DisabilityDegreeId) references DisabilityDegree