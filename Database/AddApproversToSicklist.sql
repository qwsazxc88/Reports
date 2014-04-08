alter table dbo.Sicklist add ApprovedByManagerId INT null
alter table dbo.Sicklist add ApprovedByPersonnelManagerId INT null

create index IX_Sicklist_ApprovedByManagerUser_Id on Sicklist (ApprovedByManagerId)
create index IX_Sicklist_ApprovedByPersonnelManagerUser_Id on Sicklist (ApprovedByPersonnelManagerId)

alter table Sicklist add constraint FK_Sicklist_ApprovedByManagerUser foreign key (ApprovedByManagerId) references [Users]
alter table Sicklist add constraint FK_Sicklist_ApprovedByPersonnelManagerUser foreign key (ApprovedByPersonnelManagerId) references [Users]