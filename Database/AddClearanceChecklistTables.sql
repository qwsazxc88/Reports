create table [ClearanceChecklistRole] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(32) null,
  Description NVARCHAR(256) null,
  DaysForApproval INT null,
  constraint PK_ClearanceChecklistRole primary key (Id)
)
create table ClearanceChecklistApproval (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  DismissalId INT not null,
  RoleId INT not null,
  ApprovedById INT null,
  ApprovalDate DATETIME null,
  Comment NVARCHAR(256) null,
  constraint PK_ClearanceChecklistApproval  primary key (Id)
)
create table [ClearanceChecklistRoleRecord] (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  RoleId INT not null,
  TargetUserId INT null,
  TargetDepartmentId INT null,
  constraint PK_ClearanceChecklistRoleRecord primary key (Id)
)

create index IX_ClearanceChecklistApproval_Dismissal_Id on ClearanceChecklistApproval (DismissalId)
create index IX_ClearanceChecklistApproval_ClearanceChecklistRole_Id on ClearanceChecklistApproval (RoleId)
create index IX_ClearanceChecklistApproval_ApprovedBy_Id on ClearanceChecklistApproval (ApprovedById)
alter table ClearanceChecklistApproval add constraint FK_ClearanceChecklistApproval_Dismissal foreign key (DismissalId) references Dismissal
alter table ClearanceChecklistApproval add constraint FK_ClearanceChecklistApproval_ClearanceChecklistRole foreign key (RoleId) references [ClearanceChecklistRole]
alter table ClearanceChecklistApproval add constraint FK_ClearanceChecklistApproval_ApprovedBy foreign key (ApprovedById) references [Users]
create index IX_ClearanceChecklistRoleRecord_User_Id on [ClearanceChecklistRoleRecord] (UserId)
create index IX_ClearanceChecklistRoleRecord_ClearanceChecklistRole_Id on [ClearanceChecklistRoleRecord] (RoleId)
create index IX_ClearanceChecklistRoleRecord_TargetUser_Id on [ClearanceChecklistRoleRecord] (TargetUserId)
create index IX_ClearanceChecklistRoleRecord_TargetDepartment_Id on [ClearanceChecklistRoleRecord] (TargetDepartmentId)
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_User foreign key (UserId) references [Users]
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_ClearanceChecklistRole foreign key (RoleId) references [ClearanceChecklistRole]
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_TargetUser foreign key (TargetUserId) references [Users]
alter table [ClearanceChecklistRoleRecord] add constraint FK_ClearanceChecklistRoleRecord_TargetDepartment foreign key (TargetDepartmentId) references Department