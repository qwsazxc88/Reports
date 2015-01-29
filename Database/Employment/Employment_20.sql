alter table Managers add IsSecondaryJob bit not null default 0

alter table Managers add SalaryBasis decimal(15, 2) NULL
alter table Managers drop column DailySalaryBasis
alter table Managers drop column HourlySalaryBasis
alter table PersonnelManagers add IsHourlySalaryBasis bit not null default 0