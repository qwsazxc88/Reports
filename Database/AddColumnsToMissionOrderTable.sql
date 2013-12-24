alter table dbo.MissionOrder add IsResidencePaid BIT not null default 0
alter table dbo.MissionOrder add IsAirTicketsPaid BIT not null default 0
alter table dbo.MissionOrder add IsTrainTicketsPaid BIT not null default 0
alter table dbo.MissionOrder add ResidenceRequestNumber NVARCHAR(16) null
alter table dbo.MissionOrder add AirTicketsRequestNumber NVARCHAR(16) null
alter table dbo.MissionOrder add TrainTicketsRequestNumber NVARCHAR(16) null