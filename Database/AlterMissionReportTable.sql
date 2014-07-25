alter table  [dbo].[MissionReport] add IsDocumentsSaveToArchive bit not null default 0

alter table  [dbo].[MissionReport] add ArchiveDate datetime null 
alter table  [dbo].[MissionReport] add ArchiveNumber nvarchar(128) null
if not exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionReport_Archivist]') AND parent_object_id = OBJECT_ID('MissionReport'))
begin	
	alter table  [dbo].[MissionReport] add Archivist INT null
	create index IX_MissionReport_Archivist on MissionReport (Archivist)
	alter table MissionReport add constraint FK_MissionReport_Archivist foreign key (Archivist) references [Users]
end
GO
