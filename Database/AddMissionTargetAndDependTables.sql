if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_Country]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_Country

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_DailyAllowance]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_DailyAllowance

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_Residence]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_Residence

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_AirTicketType]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_AirTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTarget_TrainTicketType]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionTarget_TrainTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionOrderTarget_MissionOrder]') AND parent_object_id = OBJECT_ID('MissionTarget'))
alter table MissionTarget  drop constraint FK_MissionOrderTarget_MissionOrder

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionDailyAllowanceGradeValue_DailyAllowance]') AND parent_object_id = OBJECT_ID('MissionDailyAllowanceGradeValue'))
alter table MissionDailyAllowanceGradeValue  drop constraint FK_MissionDailyAllowanceGradeValue_DailyAllowance

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionDailyAllowanceGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionDailyAllowanceGradeValue'))
alter table MissionDailyAllowanceGradeValue  drop constraint FK_MissionDailyAllowanceGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionResidenceGradeValue_Residence]') AND parent_object_id = OBJECT_ID('MissionResidenceGradeValue'))
alter table MissionResidenceGradeValue  drop constraint FK_MissionResidenceGradeValue_Residence

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionResidenceGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionResidenceGradeValue'))
alter table MissionResidenceGradeValue  drop constraint FK_MissionResidenceGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTrainTicketTypeGradeValue_TrainTicketType]') AND parent_object_id = OBJECT_ID('MissionTrainTicketTypeGradeValue'))
alter table MissionTrainTicketTypeGradeValue  drop constraint FK_MissionTrainTicketTypeGradeValue_TrainTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionTrainTicketTypeGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionTrainTicketTypeGradeValue'))
alter table MissionTrainTicketTypeGradeValue  drop constraint FK_MissionTrainTicketTypeGradeValue_Grade

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionAirTicketTypeGradeValue_AirTicketType]') AND parent_object_id = OBJECT_ID('MissionAirTicketTypeGradeValue'))
alter table MissionAirTicketTypeGradeValue  drop constraint FK_MissionAirTicketTypeGradeValue_AirTicketType

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_MissionAirTicketTypeGradeValue_Grade]') AND parent_object_id = OBJECT_ID('MissionAirTicketTypeGradeValue'))
alter table MissionAirTicketTypeGradeValue  drop constraint FK_MissionAirTicketTypeGradeValue_Grade

if exists (select * from dbo.sysobjects where id = object_id(N'MissionTarget') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTarget
if exists (select * from dbo.sysobjects where id = object_id(N'MissionDailyAllowanceGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionDailyAllowanceGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionResidenceGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionResidenceGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTrainTicketTypeGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTrainTicketTypeGradeValue
if exists (select * from dbo.sysobjects where id = object_id(N'MissionAirTicketTypeGradeValue') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionAirTicketTypeGradeValue




create table MissionTarget (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  MissionOrderId INT not null,
  CountryId INT not null,
  City NVARCHAR(32) not null,
  Organization NVARCHAR(32) not null,
  BeginDate DATETIME not null,
  EndDate DATETIME not null,
  DaysCount INT not null,
  RealDaysCount INT not null,
  DailyAllowanceId INT null,
  ResidenceId INT null,
  AirTicketTypeId INT null,
  TrainTicketTypeId INT null,
  constraint PK_MissionTarget  primary key (Id)
)

create index IX_MissionTarget_Country_Id on MissionTarget (CountryId)
create index IX_MissionTarget_DailyAllowance_Id on MissionTarget (DailyAllowanceId)
create index IX_MissionTarget_Residence_Id on MissionTarget (ResidenceId)
create index IX_MissionTarget_AirTicketType_Id on MissionTarget (AirTicketTypeId)
create index IX_MissionTarget_TrainTicketType_Id on MissionTarget (TrainTicketTypeId)
create index IX_MissionOrderTarget_MissionOrder on MissionTarget (MissionOrderId)
alter table MissionTarget add constraint FK_MissionTarget_Country foreign key (CountryId) references MissionCountry
alter table MissionTarget add constraint FK_MissionTarget_DailyAllowance foreign key (DailyAllowanceId) references MissionDailyAllowance
alter table MissionTarget add constraint FK_MissionTarget_Residence foreign key (ResidenceId) references MissionResidence
alter table MissionTarget add constraint FK_MissionTarget_AirTicketType foreign key (AirTicketTypeId) references MissionAirTicketType
alter table MissionTarget add constraint FK_MissionTarget_TrainTicketType foreign key (TrainTicketTypeId) references MissionTrainTicketType
alter table MissionTarget add constraint FK_MissionOrderTarget_MissionOrder foreign key (MissionOrderId) references MissionOrder


create table MissionDailyAllowanceGradeValue (
 Id INT IDENTITY NOT NULL,
  DailyAllowanceId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionDailyAllowanceGradeValue  primary key (Id)
)
create table MissionResidenceGradeValue (
 Id INT IDENTITY NOT NULL,
  ResidenceId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionResidenceGradeValue  primary key (Id)
)
create table MissionTrainTicketTypeGradeValue (
 Id INT IDENTITY NOT NULL,
  TrainTicketTypeId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionTrainTicketTypeGradeValue  primary key (Id)
)
create table MissionAirTicketTypeGradeValue (
 Id INT IDENTITY NOT NULL,
  AirTicketTypeId INT not null,
  GradeId INT not null,
  GradeDate DATETIME not null,
  Amount DECIMAL(19,5) not null,
  constraint PK_MissionAirTicketTypeGradeValue  primary key (Id)
)

create index IX_MissionDailyAllowanceGradeValue_DailyAllowance_Id on MissionDailyAllowanceGradeValue (DailyAllowanceId)
create index IX_MissionDailyAllowanceGradeValue_Grade_Id on MissionDailyAllowanceGradeValue (GradeId)
alter table MissionDailyAllowanceGradeValue add constraint FK_MissionDailyAllowanceGradeValue_DailyAllowance foreign key (DailyAllowanceId) references MissionDailyAllowance
alter table MissionDailyAllowanceGradeValue add constraint FK_MissionDailyAllowanceGradeValue_Grade foreign key (GradeId) references MissionGraid
create index IX_MissionResidenceGradeValue_Residence_Id on MissionResidenceGradeValue (ResidenceId)
create index IX_MissionResidenceGradeValue_Grade_Id on MissionResidenceGradeValue (GradeId)
alter table MissionResidenceGradeValue add constraint FK_MissionResidenceGradeValue_Residence foreign key (ResidenceId) references MissionResidence
alter table MissionResidenceGradeValue add constraint FK_MissionResidenceGradeValue_Grade foreign key (GradeId) references MissionGraid
create index IX_MissionTrainTicketTypeGradeValue_TrainTicketType_Id on MissionTrainTicketTypeGradeValue (TrainTicketTypeId)
create index IX_MissionTrainTicketTypeGradeValue_Grade_Id on MissionTrainTicketTypeGradeValue (GradeId)
alter table MissionTrainTicketTypeGradeValue add constraint FK_MissionTrainTicketTypeGradeValue_TrainTicketType foreign key (TrainTicketTypeId) references MissionTrainTicketType
alter table MissionTrainTicketTypeGradeValue add constraint FK_MissionTrainTicketTypeGradeValue_Grade foreign key (GradeId) references MissionGraid
create index IX_MissionAirTicketTypeGradeValue_AirTicketType_Id on MissionAirTicketTypeGradeValue (AirTicketTypeId)
create index IX_MissionAirTicketTypeGradeValue_Grade_Id on MissionAirTicketTypeGradeValue (GradeId)
alter table MissionAirTicketTypeGradeValue add constraint FK_MissionAirTicketTypeGradeValue_AirTicketType foreign key (AirTicketTypeId) references MissionAirTicketType
alter table MissionAirTicketTypeGradeValue add constraint FK_MissionAirTicketTypeGradeValue_Grade foreign key (GradeId) references MissionGraid


