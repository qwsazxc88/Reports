alter table [Vacation] add AdditionalVacationBeginDate DATETIME null
alter table [Vacation] add AdditionalVacationDaysCount INT not null
alter table [Vacation] add PrincipalVacationDaysLeft DECIMAL(19, 2) null
alter table [Vacation] add AdditionalVacationDaysLeft DECIMAL(19, 2) null