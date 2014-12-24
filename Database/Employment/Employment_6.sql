alter table OnsiteTraining alter column IsComplete BIT null
alter table OnsiteTraining drop column IsConfirmed
alter table OnsiteTraining add ApproverId INT null

alter table Managers add ManagerApprovalStatus BIT null
alter table Managers add ApprovingManagerId INT null
alter table Managers add ManagerRejectionReason NVARCHAR(50) null
alter table Managers add HigherManagerApprovalStatus BIT null
alter table Managers add ApprovingHigherManagerId INT null
alter table Managers add HigherManagerRejectionReason NVARCHAR(50) null
alter table Managers add RejectingChiefId INT null
alter table Managers add ChiefRejectionReason NVARCHAR(50) null

alter table BackgroundCheck add ApprovalStatus BIT null
alter table BackgroundCheck add ApproverId INT null

create index OnsiteTraining_Approver on OnsiteTraining (ApproverId)
alter table OnsiteTraining add constraint FK_OnsiteTraining_Approver foreign key (ApproverId) references [Users]
create index Managers_ApprovingManager on Managers (ApprovingManagerId)
create index Managers_ApprovingHigherManager on Managers (ApprovingHigherManagerId)
create index Managers_RejectingChief on Managers (RejectingChiefId)
alter table Managers add constraint FK_Managers_ApprovingManager foreign key (ApprovingManagerId) references [Users]
alter table Managers add constraint FK_Managers_ApprovingHigherManager foreign key (ApprovingHigherManagerId) references [Users]
alter table Managers add constraint FK_Managers_RejectingChief foreign key (RejectingChiefId) references [Users]
create index BackgroundCheck_Approver on BackgroundCheck (ApproverId)
alter table BackgroundCheck add constraint FK_BackgroundCheck_Approver foreign key (ApproverId) references [Users]