if exists (select * from dbo.sysobjects where id = object_id(N'MissionDailyAllowance') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionDailyAllowance
if exists (select * from dbo.sysobjects where id = object_id(N'MissionGoal') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionGoal
if exists (select * from dbo.sysobjects where id = object_id(N'MissionResidence') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionResidence
if exists (select * from dbo.sysobjects where id = object_id(N'MissionAirTicketType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionAirTicketType
if exists (select * from dbo.sysobjects where id = object_id(N'MissionTrainTicketType') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionTrainTicketType
if exists (select * from dbo.sysobjects where id = object_id(N'MissionGraid') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionGraid
if exists (select * from dbo.sysobjects where id = object_id(N'MissionCountry') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MissionCountry

create table MissionDailyAllowance (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionDailyAllowance  primary key (Id)
)
create table MissionGoal (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionGoal  primary key (Id)
)
create table MissionResidence (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionResidence  primary key (Id)
)
create table MissionAirTicketType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionAirTicketType  primary key (Id)
)
create table MissionTrainTicketType (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionTrainTicketType  primary key (Id)
)
create table MissionGraid (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionGraid  primary key (Id)
)
create table MissionCountry (
 Id INT IDENTITY NOT NULL,
  Code NVARCHAR(16) null,
  Name NVARCHAR(128) not null,
  constraint PK_MissionCountry  primary key (Id)
)
