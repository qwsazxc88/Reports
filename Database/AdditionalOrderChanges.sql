if not exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_AdditionalMissionOrder]') AND parent_object_id = OBJECT_ID('MissionReport'))
begin
	alter table MissionReport add AdditionalMissionOrderId INT null
	create index IX_MissionReport_AdditionalMissionOrder on MissionReport (AdditionalMissionOrderId)
	alter table MissionReport add constraint FK_MissionReport_AdditionalMissionOrder foreign key (AdditionalMissionOrderId) references MissionOrder
end
alter table MissionOrder add  IsAdditional BIT not null default(0)
alter table MissionOrder alter column  BeginDate DATETIME null
alter table MissionOrder alter column  EndDate DATETIME null
if not exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrder_MainMissionOrder]') AND parent_object_id = OBJECT_ID('MissionOrder'))
begin
alter table MissionOrder add MainOrderId INT null
create index IX_MissionOrder_MainMissionOrder on MissionOrder (MainOrderId)
alter table MissionOrder add constraint FK_MissionOrder_MainMissionOrder foreign key (MainOrderId) references MissionOrder
end 