alter table GeneralInfo alter column DisabilityCertificateSeries nvarchar(20) null
alter table GeneralInfo alter column DisabilityCertificateNumber nvarchar(20) null
alter table Passport alter column InternalPassportIssuedBy nvarchar(250) not null
alter table Passport alter column InternalPassportSubdivisionCode nvarchar(15) not null
alter table Managers alter column EmploymentConditions nvarchar(250) null
alter table Managers alter column WorkCity nvarchar(100) null
alter table PersonnelManagers alter column PersonalAccount nvarchar(23) not null