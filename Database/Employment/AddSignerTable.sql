if exists (select * from dbo.sysobjects where id = object_id(N'Signer') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Signer

create table Signer (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Name NVARCHAR(50) null,
  PreamblePartyTemplate NVARCHAR(500) null,
  constraint PK_Signer  primary key (Id)
)

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PersonnelManagers_Signer]') AND parent_object_id = OBJECT_ID('PersonnelManagers'))  
  alter table PersonnelManagers drop constraint FK_PersonnelManagers_Signer

alter table PersonnelManagers add SignerId INT null
create index PersonnelManagers_Signer on PersonnelManagers (SignerId)
alter table PersonnelManagers add constraint FK_PersonnelManagers_Signer foreign key (SignerId) references Signer