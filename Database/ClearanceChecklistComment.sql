create table ClearanceChecklistComment (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  UserId INT not null,
  ClearanceChecklistId INT not null,
  DateCreated DATETIME not null,
  Comment NVARCHAR(256) not null,
  constraint PK_ClearanceChecklistComment  primary key (Id)
)

create index IX_ClearanceChecklistComment_User_Id on ClearanceChecklistComment (UserId)
create index IX_ClearanceChecklistComment_ClearanceChecklist_Id on ClearanceChecklistComment (ClearanceChecklistId)
alter table ClearanceChecklistComment add constraint FK_ClearanceChecklistComment_User foreign key (UserId) references [Users]
alter table ClearanceChecklistComment add constraint FK_ClearanceChecklistComment_ClearanceChecklist foreign key (ClearanceChecklistId) references Dismissal
