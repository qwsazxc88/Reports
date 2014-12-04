ALTER TABLE GeneralInfo ALTER COLUMN LastName nvarchar(50) NULL
ALTER TABLE GeneralInfo ALTER COLUMN FirstName nvarchar(50) NULL
ALTER TABLE GeneralInfo ALTER COLUMN CityOfBirth nvarchar(50) NULL
ALTER TABLE GeneralInfo ALTER COLUMN Status int NULL

ALTER TABLE Passport ALTER COLUMN DocumentTypeId int NULL
ALTER TABLE Passport ALTER COLUMN InternalPassportSeries nvarchar(4) NULL
ALTER TABLE Passport ALTER COLUMN InternalPassportNumber nvarchar(6) NULL
ALTER TABLE Passport ALTER COLUMN InternalPassportDateOfIssue datetime NULL
ALTER TABLE Passport ALTER COLUMN InternalPassportIssuedBy nvarchar(250) NULL
ALTER TABLE Passport ALTER COLUMN InternalPassportSubdivisionCode nvarchar(15) NULL
ALTER TABLE Passport ALTER COLUMN ZipCode nvarchar(6) NULL
ALTER TABLE Passport ALTER COLUMN City nvarchar(50) NULL

ALTER TABLE OnsiteTraining ALTER COLUMN Description nvarchar(200) NULL
ALTER TABLE OnsiteTraining ALTER COLUMN Type nvarchar(200) NULL

ALTER TABLE PersonnelManagers ALTER COLUMN EmploymentOrderNumber nvarchar(20) NULL
ALTER TABLE PersonnelManagers ALTER COLUMN ContractNumber nvarchar(20) NULL
ALTER TABLE PersonnelManagers ALTER COLUMN InsurableExperienceYears int NULL
ALTER TABLE PersonnelManagers ALTER COLUMN InsurableExperienceMonths int NULL
ALTER TABLE PersonnelManagers ALTER COLUMN InsurableExperienceDays int NULL
ALTER TABLE PersonnelManagers ALTER COLUMN PersonalAccount nvarchar(23) NULL
ALTER TABLE PersonnelManagers ALTER COLUMN OverallExperienceYears int NULL
ALTER TABLE PersonnelManagers ALTER COLUMN OverallExperienceMonths int NULL
ALTER TABLE PersonnelManagers ALTER COLUMN OverallExperienceDays int NULL
ALTER TABLE PersonnelManagers ALTER COLUMN PersonalAccountContractorId int NULL