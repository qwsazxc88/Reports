if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_SupplementaryAgreement_PersonnelManagers]') AND parent_object_id = OBJECT_ID('SupplementaryAgreement'))
alter table SupplementaryAgreement  drop constraint FK_SupplementaryAgreement_PersonnelManagers

if exists (select * from dbo.sysobjects where id = object_id(N'SupplementaryAgreement') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SupplementaryAgreement

create table SupplementaryAgreement (
 Id INT IDENTITY NOT NULL,
  CreateDate DATETIME null,
  Number INT null,
  OrderCreateDate DATETIME null,
  OrderNumber INT null,
  PersonnelManagersId INT not null,
  constraint PK_SupplementaryAgreement  primary key (Id)
)

create index IX_SupplementaryAgreement_PersonnelManagers on SupplementaryAgreement (PersonnelManagersId)

alter table SupplementaryAgreement add constraint FK_SupplementaryAgreement_PersonnelManagers foreign key (PersonnelManagersId) references PersonnelManagers