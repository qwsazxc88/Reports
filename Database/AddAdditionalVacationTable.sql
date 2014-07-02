create table AdditionalVacationType (
 Id INT IDENTITY NOT NULL,
  Version INT not null,
  Code NVARCHAR(16) not null,
  Name NVARCHAR(128) not null,
  constraint PK_AdditionalVacationType  primary key (Id)
)

INSERT INTO [dbo].[AdditionalVacationType]
           ([Version], [Code], [Name])
     VALUES
           (1, '1207', 'Дополнительный отпуск за ненормированный рабочий день'),
           (1, '1208', 'Дополнительный отпуск за работу в районах Крайнего Севера и приравненных местностях')

ALTER TABLE [Vacation] ADD AdditionalVacationTypeId INT NULL
ALTER TABLE Vacation ADD CONSTRAINT FK_Vacation_AdditionalVacationType FOREIGN KEY (AdditionalVacationTypeId) REFERENCES AdditionalVacationType
CREATE INDEX Vacation_AdditionalVacationType ON Vacation (AdditionalVacationTypeId)