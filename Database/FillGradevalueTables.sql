﻿--select * from dbo.MissionAirTicketType
--select * from dbo.MissionTrainTicketType
--select * from dbo.MissionGraid
--select * from dbo.MissionAirTicketTypeGradeValue
--select * from dbo.MissionTrainTicketTypeGradeValue
--select * from dbo.MissionResidence
-- select * from dbo.MissionDailyAllowance
-- rollback
-- commit
begin tran
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (1,1,'2013-01-01',3000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (1,2,'2013-01-01',3000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (1,3,'2013-01-01',3000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (1,4,'2013-01-01',3000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (2,1,'2013-01-01',4000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (2,2,'2013-01-01',4000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (2,3,'2013-01-01',4000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (2,4,'2013-01-01',4000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (3,1,'2013-01-01',6000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (3,2,'2013-01-01',7000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (3,3,'2013-01-01',7000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (3,4,'2013-01-01',7000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (4,1,'2013-01-01',10000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (4,2,'2013-01-01',10000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (4,3,'2013-01-01',10000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (4,4,'2013-01-01',10000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (5,1,'2013-01-01',17000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (5,2,'2013-01-01',27000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (5,3,'2013-01-01',37000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (5,4,'2013-01-01',47000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (6,1,'2013-01-01',20000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (6,2,'2013-01-01',30000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (6,3,'2013-01-01',40000)
insert into dbo.MissionAirTicketTypeGradeValue (AirTicketTypeId, GradeId, GradeDate, Amount)
values (6,4,'2013-01-01',50000)


insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (1,1,'2013-01-01',3000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (1,2,'2013-01-01',3000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (1,3,'2013-01-01',3000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (1,4,'2013-01-01',3000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (2,1,'2013-01-01',4000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (2,2,'2013-01-01',4000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (2,3,'2013-01-01',4000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (2,4,'2013-01-01',4000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (3,1,'2013-01-01',6000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (3,2,'2013-01-01',7000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (3,3,'2013-01-01',7000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (3,4,'2013-01-01',7000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (4,1,'2013-01-01',10000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (4,2,'2013-01-01',10000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (4,3,'2013-01-01',10000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (4,4,'2013-01-01',10000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (5,1,'2013-01-01',17000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (5,2,'2013-01-01',27000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (5,3,'2013-01-01',37000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (5,4,'2013-01-01',47000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (6,1,'2013-01-01',20000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (6,2,'2013-01-01',30000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (6,3,'2013-01-01',40000)
insert into  dbo.MissionTrainTicketTypeGradeValue (TrainTicketTypeId, GradeId, GradeDate, Amount)
values (6,4,'2013-01-01',50000)

insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (1,1,'2013-01-01',8000)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (1,2,'2013-01-01',6000)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (1,3,'2013-01-01',4000)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (1,4,'2013-01-01',2500)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (2,1,'2013-01-01',600)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (2,2,'2013-01-01',600)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (2,3,'2013-01-01',600)
insert into  dbo.MissionResidenceGradeValue (ResidenceId, GradeId, GradeDate, Amount)
values (2,4,'2013-01-01',600)

insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (1,1,'2013-01-01',400)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (1,2,'2013-01-01',400)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (1,3,'2013-01-01',400)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (1,4,'2013-01-01',400)

insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (2,1,'2013-01-01',600)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (2,2,'2013-01-01',600)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (2,3,'2013-01-01',600)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (2,4,'2013-01-01',600)

insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (3,1,'2013-01-01',700)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (3,2,'2013-01-01',700)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (3,3,'2013-01-01',700)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (3,4,'2013-01-01',700)

insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (4,1,'2013-01-01',700)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (4,2,'2013-01-01',700)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (4,3,'2013-01-01',700)
insert into  dbo.MissionDailyAllowanceGradeValue (DailyAllowanceId, GradeId, GradeDate, Amount)
values (4,4,'2013-01-01',700)
