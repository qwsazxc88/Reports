if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ParentToManager2Child_Parent]') AND parent_object_id = OBJECT_ID('AppointmentManager2ParentToManager2Child'))
alter table AppointmentManager2ParentToManager2Child  drop constraint FK_AppointmentManager2ParentToManager2Child_Parent

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ParentToManager2Child_Child]') AND parent_object_id = OBJECT_ID('AppointmentManager2ParentToManager2Child'))
alter table AppointmentManager2ParentToManager2Child  drop constraint FK_AppointmentManager2ParentToManager2Child_Child

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ToManager3_Manager2]') AND parent_object_id = OBJECT_ID('AppointmentManager2ToManager3'))
alter table AppointmentManager2ToManager3  drop constraint FK_AppointmentManager2ToManager3_Manager2

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager2ToManager3_Manager3]') AND parent_object_id = OBJECT_ID('AppointmentManager2ToManager3'))
alter table AppointmentManager2ToManager3  drop constraint FK_AppointmentManager2ToManager3_Manager3

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager23ToDepartment3_User]') AND parent_object_id = OBJECT_ID('AppointmentManager23ToDepartment3'))
alter table AppointmentManager23ToDepartment3  drop constraint FK_AppointmentManager23ToDepartment3_User

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_AppointmentManager23ToDepartment3_Department]') AND parent_object_id = OBJECT_ID('AppointmentManager23ToDepartment3'))
alter table AppointmentManager23ToDepartment3  drop constraint FK_AppointmentManager23ToDepartment3_Department

if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager2ParentToManager2Child') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table AppointmentManager2ParentToManager2Child
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager2ToManager3') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table AppointmentManager2ToManager3
if exists (select * from dbo.sysobjects where id = object_id(N'AppointmentManager23ToDepartment3') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
	drop table AppointmentManager23ToDepartment3
GO

create table AppointmentManager2ToManager3 (
 Id INT IDENTITY NOT NULL,
  Manager2Id INT not null,
  Manager3Id INT not null,
  constraint PK_AppointmentManager2ToManager3  primary key (Id)
)
create table AppointmentManager23ToDepartment3 (
 Id INT IDENTITY NOT NULL,
  ManagerId INT not null,
  DepartmentId INT not null,
  constraint PK_AppointmentManager23ToDepartment3  primary key (Id)
)
create table AppointmentManager2ParentToManager2Child (
 Id INT IDENTITY NOT NULL,
  ParentId INT not null,
  ChildId INT not null,
  constraint PK_AppointmentManager2ParentToManager2Child  primary key (Id)
)

create index IX_AppointmentManager2ToManager3_Manager2 on AppointmentManager2ToManager3 (Manager2Id)
create index IX_AppointmentManager2ToManager3_Manager3 on AppointmentManager2ToManager3 (Manager3Id)
alter table AppointmentManager2ToManager3 add constraint FK_AppointmentManager2ToManager3_Manager2 foreign key (Manager2Id) references [Users]
alter table AppointmentManager2ToManager3 add constraint FK_AppointmentManager2ToManager3_Manager3 foreign key (Manager3Id) references [Users]
create index IX_AppointmentManager23ToDepartment3_User on AppointmentManager23ToDepartment3 (ManagerId)
create index IX_AppointmentManager23ToDepartment3_Department on AppointmentManager23ToDepartment3 (DepartmentId)
alter table AppointmentManager23ToDepartment3 add constraint FK_AppointmentManager23ToDepartment3_User foreign key (ManagerId) references [Users]
alter table AppointmentManager23ToDepartment3 add constraint FK_AppointmentManager23ToDepartment3_Department foreign key (DepartmentId) references Department
create index IX_AppointmentManager2ParentToManager2Child_Parent on AppointmentManager2ParentToManager2Child (ParentId)
create index IX_AppointmentManager2ParentToManager2Child_Child on AppointmentManager2ParentToManager2Child (ChildId)
alter table AppointmentManager2ParentToManager2Child add constraint FK_AppointmentManager2ParentToManager2Child_Parent foreign key (ParentId) references [Users]
alter table AppointmentManager2ParentToManager2Child add constraint FK_AppointmentManager2ParentToManager2Child_Child foreign key (ChildId) references [Users]

GO