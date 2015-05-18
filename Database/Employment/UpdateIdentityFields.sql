--РАЗОВАЯ ПРОЦЕДУРА
--после отработки процедуры в ручнеом режиме назначить новым колонкам свойство Identity и приоритет 
/*
	этот кусок запустить после того, как в ручном режиме установят свойство Identity

	dbcc checkident ('EmploymentCandidate', reseed)
	go
	dbcc checkident ('PersonnelManagers', reseed)
	go
	dbcc checkident ('GeneralInfo', reseed)
	go
	dbcc checkident ('Passport', reseed)
	go
	dbcc checkident ('Education', reseed)
	go
	dbcc checkident ('Family', reseed)
	go
	dbcc checkident ('MilitaryService', reseed)
	go
	dbcc checkident ('Experience', reseed)
	go
	dbcc checkident ('Contacts', reseed)
	go
	dbcc checkident ('BackgroundCheck', reseed)
	go
	dbcc checkident ('OnsiteTraining', reseed)
	go
	dbcc checkident ('Managers', reseed)
	go
	*/
return
--удаление ссылок
	--кадры
	ALTER TABLE [dbo].[PersonnelManagers] DROP CONSTRAINT [FK_PersonnelManagers_Candidate]
	GO
	--основная информация
	ALTER TABLE [dbo].[GeneralInfo] DROP CONSTRAINT [FK_GeneralInfo_Candidate]
	GO
	--паспорт
	ALTER TABLE [dbo].[Passport] DROP CONSTRAINT [FK_Passport_Candidate]
	GO
	--образование
	ALTER TABLE [dbo].[Education] DROP CONSTRAINT [FK_Education_Candidate]
	GO
	--семья
	ALTER TABLE [dbo].[Family] DROP CONSTRAINT [FK_Family_Candidate]
	GO
	--военый билет
	ALTER TABLE [dbo].[MilitaryService] DROP CONSTRAINT [FK_MilitaryService_Candidate]
	GO
	--трудовая деятельность
	ALTER TABLE [dbo].[Experience] DROP CONSTRAINT [FK_Experience_Candidate]
	GO
	--контакты
	ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_Contacts_Candidate]
	GO
	--ДБ
	ALTER TABLE [dbo].[BackgroundCheck] DROP CONSTRAINT [FK_BackgroundCheck_Candidate]
	GO
	--тренер
	ALTER TABLE [dbo].[OnsiteTraining] DROP CONSTRAINT [FK_OnsiteTraining_Candidate]
	GO
	--руководитель
	ALTER TABLE [dbo].[Managers] DROP CONSTRAINT [FK_Managers_Candidate]
	GO
	--кандидат
	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_PersonnelManagers]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Passport]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_OnsiteTraining]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_MilitaryService]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Managers]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_GeneralInfo]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Family]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Experience]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Education]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_Contacts]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] DROP CONSTRAINT [FK_Candidate_BackgroundCheck]
	GO
	--вспомогательные таблицы
	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] DROP CONSTRAINT [FK_EmploymentCandidateDocNeeded_EmploymentCandidate]
	GO

	ALTER TABLE [dbo].[NameChange] DROP CONSTRAINT [FK_NameChange_GeneralInfo]
	GO

	ALTER TABLE [dbo].[ForeignLanguage] DROP CONSTRAINT [FK_ForeignLanguage_GeneralInfo]
	GO

	ALTER TABLE [dbo].[Certification] DROP CONSTRAINT [FK_Certification_Education]
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma] DROP CONSTRAINT [FK_HigherEducationDiploma_Education]
	GO

	ALTER TABLE [dbo].[PostGraduateEducationDiploma] DROP CONSTRAINT [FK_PostGraduateEducationDiploma_Education]
	GO

	ALTER TABLE [dbo].[Training] DROP CONSTRAINT [FK_Training_Education]
	GO

	ALTER TABLE [dbo].[FamilyMember] DROP CONSTRAINT [FK_FamilyMember_Family]
	GO

	ALTER TABLE [dbo].[ExperienceItem] DROP CONSTRAINT [FK_ExperienceItem_Experience]
	GO

	ALTER TABLE [dbo].[Reference] DROP CONSTRAINT [FK_Reference_BackgroundCheck]
	GO

	ALTER TABLE [dbo].[SupplementaryAgreement] DROP CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers]
	GO


	
	--создаем в таблицах временные колонки
	ALTER TABLE PersonnelManagers ADD Id_New int null
	GO

	ALTER TABLE GeneralInfo ADD Id_New int null
	GO

	ALTER TABLE Passport ADD Id_New int null
	GO

	ALTER TABLE Education ADD Id_New int null
	GO

	ALTER TABLE Family ADD Id_New int null
	GO

	ALTER TABLE MilitaryService ADD Id_New int null
	GO

	ALTER TABLE Experience ADD Id_New int null
	GO

	ALTER TABLE Contacts ADD Id_New int null
	GO

	ALTER TABLE BackgroundCheck ADD Id_New int null
	GO

	ALTER TABLE OnsiteTraining ADD Id_New int null
	GO

	ALTER TABLE Managers ADD Id_New int null
	GO

	ALTER TABLE EmploymentCandidate ADD Id_New int null
	GO


	--в цикле присваиваем 
	declare @Id int, @Id_New int,
					@GeneralInfoId int, @PassportId int, @EducationId int, @FamilyId int, @MilitaryServiceId int, @ExperienceId int, @ContactsId int, @BackgroundCheckId int, @OnsiteTrainingId int, 
					@ManagersId int, @PersonnelManagersId int
	SET @Id = 0
	SET @Id_New = 0

	WHILE EXISTS (SELECT * FROM EmploymentCandidate WHERE Id > @Id)
	BEGIN
		SET @Id_New += 1;
		--определяем текущие значения идентификаторов
		SELECT top 1 @Id = Id, @GeneralInfoId = GeneralInfoId, @PassportId = PassportId, @EducationId = EducationId, @FamilyId = FamilyId, @MilitaryServiceId = MilitaryServiceId,
					@ExperienceId = ExperienceId, @ContactsId = ContactsId, @BackgroundCheckId = BackgroundCheckId, @OnsiteTrainingId = OnsiteTrainingId, @ManagersId = ManagersId, 
					@PersonnelManagersId = PersonnelManagersId
		FROM EmploymentCandidate WHERE Id > @Id ORDER BY Id

		--сохраняем новые значения идентификаторов
		UPDATE EmploymentCandidate SET Id_New = @Id_New WHERE Id = @Id
		UPDATE EmploymentCandidateDocNeeded SET CandidateId = @Id_New WHERE CandidateId = @Id
	
		UPDATE GeneralInfo SET Id_New = @Id_New WHERE Id = @GeneralInfoId
		UPDATE NameChange SET GeneralInfoId = @Id_New WHERE GeneralInfoId = @GeneralInfoId
		UPDATE ForeignLanguage SET GeneralInfoId = @Id_New WHERE GeneralInfoId = @GeneralInfoId

		UPDATE Passport SET Id_New = @Id_New WHERE Id = @PassportId
	
		UPDATE Education SET Id_New = @Id_New WHERE Id = @EducationId
		UPDATE Certification SET EducationId = @Id_New WHERE EducationId = @EducationId
		UPDATE HigherEducationDiploma SET EducationId = @Id_New WHERE EducationId = @EducationId
		UPDATE PostGraduateEducationDiploma SET EducationId = @Id_New WHERE EducationId = @EducationId
		UPDATE Training SET EducationId = @Id_New WHERE EducationId = @EducationId

		UPDATE Family SET Id_New = @Id_New WHERE Id = @FamilyId
		UPDATE FamilyMember SET FamilyId = @Id_New WHERE FamilyId = @FamilyId

		UPDATE MilitaryService SET Id_New = @Id_New WHERE Id = @MilitaryServiceId


		UPDATE Experience SET Id_New = @Id_New WHERE Id = @ExperienceId
		UPDATE ExperienceItem SET ExperienceId = @Id_New WHERE ExperienceId = @ExperienceId
	
		UPDATE Contacts SET Id_New = @Id_New WHERE Id = @ContactsId
	
		UPDATE BackgroundCheck SET Id_New = @Id_New WHERE Id = @BackgroundCheckId
		UPDATE Reference SET BackgroundCheckId = @Id_New WHERE BackgroundCheckId = @BackgroundCheckId
	
		UPDATE OnsiteTraining SET Id_New = @Id_New WHERE Id = @OnsiteTrainingId
	
		UPDATE Managers SET Id_New = @Id_New WHERE Id = @ManagersId
	
		UPDATE PersonnelManagers SET Id_New = @Id_New WHERE Id = @PersonnelManagersId
		UPDATE SupplementaryAgreement SET PersonnelManagersId = @Id_New WHERE PersonnelManagersId = @PersonnelManagersId

		UPDATE EmploymentCandidate SET GeneralInfoId = @Id_New, PassportId = @Id_New, EducationId = @Id_New, FamilyId = @Id_New, MilitaryServiceId = @Id_New,
					 ExperienceId = @Id_New, ContactsId = @Id_New, BackgroundCheckId = @Id_New, OnsiteTrainingId = @Id_New, ManagersId = @Id_New, PersonnelManagersId = @Id_New
		FROM EmploymentCandidate WHERE Id = @Id
		print cast(@id as varchar) + ' ' + cast(@Id_New as varchar)
	END


	--удаляем колонки с старыми идентификаторами
	ALTER TABLE PersonnelManagers
	DROP CONSTRAINT PK_PersonnelManagers
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE PersonnelManagers DROP COLUMN Id 
	GO

	ALTER TABLE GeneralInfo
	DROP CONSTRAINT PK_GeneralInfo
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE GeneralInfo DROP COLUMN Id
	GO

	ALTER TABLE Passport
	DROP CONSTRAINT PK_Passport
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE Passport DROP COLUMN Id 
	GO

	ALTER TABLE Education
	DROP CONSTRAINT PK_Education
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE Education DROP COLUMN Id
	GO

	ALTER TABLE Family
	DROP CONSTRAINT PK_Family
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE Family DROP COLUMN Id 
	GO

	ALTER TABLE MilitaryService
	DROP CONSTRAINT PK_MilitaryService
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE MilitaryService DROP COLUMN Id 
	GO

	ALTER TABLE Experience
	DROP CONSTRAINT PK_Experience
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE Experience DROP COLUMN Id 
	GO

	ALTER TABLE Contacts
	DROP CONSTRAINT PK_Contacts
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE Contacts DROP COLUMN Id 
	GO

	ALTER TABLE BackgroundCheck
	DROP CONSTRAINT PK_BackgroundCheck
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE BackgroundCheck DROP COLUMN Id
	GO

	ALTER TABLE OnsiteTraining
	DROP CONSTRAINT PK_OnsiteTraining
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE OnsiteTraining DROP COLUMN Id 
	GO

	ALTER TABLE Managers
	DROP CONSTRAINT PK_Managers
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE Managers DROP COLUMN Id 
	GO

	ALTER TABLE EmploymentCandidate
	DROP CONSTRAINT PK_EmploymentCandidate
	WITH (ONLINE = OFF);
	GO
	ALTER TABLE EmploymentCandidate DROP COLUMN Id 
	GO


	--создаем новые колонки
	ALTER TABLE PersonnelManagers ADD Id int null
	GO

	ALTER TABLE GeneralInfo ADD Id int null
	GO

	ALTER TABLE Passport ADD Id int null
	GO

	ALTER TABLE Education ADD Id int null
	GO

	ALTER TABLE Family ADD Id int null
	GO

	ALTER TABLE MilitaryService ADD Id int null
	GO

	ALTER TABLE Experience ADD Id int null
	GO

	ALTER TABLE Contacts ADD Id int null
	GO

	ALTER TABLE BackgroundCheck ADD Id int null
	GO

	ALTER TABLE OnsiteTraining ADD Id int null
	GO

	ALTER TABLE Managers ADD Id int null
	GO

	ALTER TABLE EmploymentCandidate ADD Id int null
	GO




	--сохраняем в новых колонках новые значения
	UPDATE PersonnelManagers SET Id = Id_New, CandidateId = Id_New
	UPDATE GeneralInfo SET Id = Id_New, CandidateId = Id_New
	UPDATE Passport SET Id = Id_New, CandidateId = Id_New
	UPDATE Education SET Id = Id_New, CandidateId = Id_New
	UPDATE Family SET Id = Id_New, CandidateId = Id_New
	UPDATE MilitaryService SET Id = Id_New, CandidateId = Id_New
	UPDATE Experience SET Id = Id_New, CandidateId = Id_New
	UPDATE Contacts SET Id = Id_New, CandidateId = Id_New
	UPDATE BackgroundCheck SET Id = Id_New, CandidateId = Id_New
	UPDATE OnsiteTraining SET Id = Id_New, CandidateId = Id_New
	UPDATE Managers SET Id = Id_New, CandidateId = Id_New
	UPDATE EmploymentCandidate SET Id = Id_New

	
	--меняем свойство новых колонок и расставляем в них ключи
	ALTER TABLE PersonnelManagers ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE PersonnelManagers WITH NOCHECK 
	ADD CONSTRAINT PK_PersonnelManagers PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO
	
	ALTER TABLE GeneralInfo ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE GeneralInfo WITH NOCHECK 
	ADD CONSTRAINT PK_GeneralInfo PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO
	
	ALTER TABLE Passport ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE Passport WITH NOCHECK 
	ADD CONSTRAINT PK_Passport PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE Education ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE Education WITH NOCHECK 
	ADD CONSTRAINT PK_Education PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE Family ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE Family WITH NOCHECK 
	ADD CONSTRAINT PK_Family PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE MilitaryService ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE MilitaryService WITH NOCHECK 
	ADD CONSTRAINT PK_MilitaryService PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE Experience ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE Experience WITH NOCHECK 
	ADD CONSTRAINT PK_Experience PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE Contacts ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE Contacts WITH NOCHECK 
	ADD CONSTRAINT PK_Contacts PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE BackgroundCheck ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE BackgroundCheck WITH NOCHECK 
	ADD CONSTRAINT PK_BackgroundCheck PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE OnsiteTraining ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE OnsiteTraining WITH NOCHECK 
	ADD CONSTRAINT PK_OnsiteTraining PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE Managers ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE Managers WITH NOCHECK 
	ADD CONSTRAINT PK_Managers PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO

	ALTER TABLE EmploymentCandidate ALTER COLUMN Id int NOT NULL ;
	GO 
	ALTER TABLE EmploymentCandidate WITH NOCHECK 
	ADD CONSTRAINT PK_EmploymentCandidate PRIMARY KEY CLUSTERED (ID)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
	GO



	--удаляем временные колонки 
	ALTER TABLE PersonnelManagers DROP COLUMN Id_New
	GO

	ALTER TABLE GeneralInfo DROP COLUMN Id_New
	GO

	ALTER TABLE Passport DROP COLUMN Id_New
	GO

	ALTER TABLE Education DROP COLUMN Id_New
	GO

	ALTER TABLE Family DROP COLUMN Id_New
	GO

	ALTER TABLE MilitaryService DROP COLUMN Id_New
	GO

	ALTER TABLE Experience DROP COLUMN Id_New
	GO

	ALTER TABLE Contacts DROP COLUMN Id_New
	GO

	ALTER TABLE BackgroundCheck DROP COLUMN Id_New
	GO

	ALTER TABLE OnsiteTraining DROP COLUMN Id_New
	GO

	ALTER TABLE Managers DROP COLUMN Id_New 
	GO

	ALTER TABLE EmploymentCandidate DROP COLUMN Id_New 
	GO

/*
	--после добавления колонок в таблицу они встают в конец списка колонок в таблице
	--по этому меняем у новых колонок приоритет
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'EmploymentCandidate') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'PersonnelManagers') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'GeneralInfo') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'Passport') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'Education') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'Family') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'MilitaryService') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'Experience') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'Contacts') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'BackgroundCheck') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'OnsiteTraining') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'OnsiteTraining') and name = N'Id'
	update syscolumns SET colorder = 1 where id = (select id from sysobjects where name = 'Managers') and name = N'Id'
	*/

--создание ссылок
	--кадры
	ALTER TABLE [dbo].[PersonnelManagers]  WITH CHECK ADD  CONSTRAINT [FK_PersonnelManagers_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[PersonnelManagers] CHECK CONSTRAINT [FK_PersonnelManagers_Candidate]
	GO
	--основная информация
	ALTER TABLE [dbo].[GeneralInfo]  WITH CHECK ADD  CONSTRAINT [FK_GeneralInfo_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[GeneralInfo] CHECK CONSTRAINT [FK_GeneralInfo_Candidate]
	GO
	--паспорт
	ALTER TABLE [dbo].[Passport]  WITH CHECK ADD  CONSTRAINT [FK_Passport_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_Candidate]
	GO
	--образование
	ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_Candidate]
	GO
	--семья
	ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_Candidate]
	GO
	--военый билет
	ALTER TABLE [dbo].[MilitaryService]  WITH CHECK ADD  CONSTRAINT [FK_MilitaryService_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[MilitaryService] CHECK CONSTRAINT [FK_MilitaryService_Candidate]
	GO
	--трудовая деятельность
	ALTER TABLE [dbo].[Experience]  WITH CHECK ADD  CONSTRAINT [FK_Experience_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Experience] CHECK CONSTRAINT [FK_Experience_Candidate]
	GO
	--контакты
	ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Candidate]
	GO
	--ДБ
	ALTER TABLE [dbo].[BackgroundCheck]  WITH CHECK ADD  CONSTRAINT [FK_BackgroundCheck_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[BackgroundCheck] CHECK CONSTRAINT [FK_BackgroundCheck_Candidate]
	GO
	--тренер
	ALTER TABLE [dbo].[OnsiteTraining]  WITH CHECK ADD  CONSTRAINT [FK_OnsiteTraining_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[OnsiteTraining] CHECK CONSTRAINT [FK_OnsiteTraining_Candidate]
	GO
	--руководитель
	ALTER TABLE [dbo].[Managers]  WITH CHECK ADD  CONSTRAINT [FK_Managers_Candidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[Managers] CHECK CONSTRAINT [FK_Managers_Candidate]
	GO
	--кандидат
	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_BackgroundCheck] FOREIGN KEY([BackgroundCheckId])
	REFERENCES [dbo].[BackgroundCheck] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_BackgroundCheck]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Contacts] FOREIGN KEY([ContactsId])
	REFERENCES [dbo].[Contacts] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Contacts]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Education]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Experience] FOREIGN KEY([ExperienceId])
	REFERENCES [dbo].[Experience] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Experience]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Family] FOREIGN KEY([FamilyId])
	REFERENCES [dbo].[Family] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Family]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_GeneralInfo] FOREIGN KEY([GeneralInfoId])
	REFERENCES [dbo].[GeneralInfo] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_GeneralInfo]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Managers] FOREIGN KEY([ManagersId])
	REFERENCES [dbo].[Managers] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Managers]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_MilitaryService] FOREIGN KEY([MilitaryServiceId])
	REFERENCES [dbo].[MilitaryService] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_MilitaryService]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_OnsiteTraining] FOREIGN KEY([OnsiteTrainingId])
	REFERENCES [dbo].[OnsiteTraining] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_OnsiteTraining]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Passport] FOREIGN KEY([PassportId])
	REFERENCES [dbo].[Passport] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_Passport]
	GO

	ALTER TABLE [dbo].[EmploymentCandidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_PersonnelManagers] FOREIGN KEY([PersonnelManagersId])
	REFERENCES [dbo].[PersonnelManagers] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidate] CHECK CONSTRAINT [FK_Candidate_PersonnelManagers]
	GO
	--вспомогательные таблицы
	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentCandidateDocNeeded_EmploymentCandidate] FOREIGN KEY([CandidateId])
	REFERENCES [dbo].[EmploymentCandidate] ([Id])
	GO

	ALTER TABLE [dbo].[EmploymentCandidateDocNeeded] CHECK CONSTRAINT [FK_EmploymentCandidateDocNeeded_EmploymentCandidate]
	GO

	ALTER TABLE [dbo].[NameChange]  WITH CHECK ADD  CONSTRAINT [FK_NameChange_GeneralInfo] FOREIGN KEY([GeneralInfoId])
	REFERENCES [dbo].[GeneralInfo] ([Id])
	GO

	ALTER TABLE [dbo].[NameChange] CHECK CONSTRAINT [FK_NameChange_GeneralInfo]
	GO

	ALTER TABLE [dbo].[ForeignLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ForeignLanguage_GeneralInfo] FOREIGN KEY([GeneralInfoId])
	REFERENCES [dbo].[GeneralInfo] ([Id])
	GO

	ALTER TABLE [dbo].[ForeignLanguage] CHECK CONSTRAINT [FK_ForeignLanguage_GeneralInfo]
	GO

	ALTER TABLE [dbo].[Certification]  WITH CHECK ADD  CONSTRAINT [FK_Certification_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[Certification] CHECK CONSTRAINT [FK_Certification_Education]
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma]  WITH CHECK ADD  CONSTRAINT [FK_HigherEducationDiploma_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[HigherEducationDiploma] CHECK CONSTRAINT [FK_HigherEducationDiploma_Education]
	GO

	ALTER TABLE [dbo].[PostGraduateEducationDiploma]  WITH CHECK ADD  CONSTRAINT [FK_PostGraduateEducationDiploma_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[PostGraduateEducationDiploma] CHECK CONSTRAINT [FK_PostGraduateEducationDiploma_Education]
	GO

	ALTER TABLE [dbo].[Training]  WITH CHECK ADD  CONSTRAINT [FK_Training_Education] FOREIGN KEY([EducationId])
	REFERENCES [dbo].[Education] ([Id])
	GO

	ALTER TABLE [dbo].[Training] CHECK CONSTRAINT [FK_Training_Education]
	GO

	ALTER TABLE [dbo].[FamilyMember]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMember_Family] FOREIGN KEY([FamilyId])
	REFERENCES [dbo].[Family] ([Id])
	GO

	ALTER TABLE [dbo].[FamilyMember] CHECK CONSTRAINT [FK_FamilyMember_Family]
	GO

	ALTER TABLE [dbo].[ExperienceItem]  WITH CHECK ADD  CONSTRAINT [FK_ExperienceItem_Experience] FOREIGN KEY([ExperienceId])
	REFERENCES [dbo].[Experience] ([Id])
	GO

	ALTER TABLE [dbo].[ExperienceItem] CHECK CONSTRAINT [FK_ExperienceItem_Experience]
	GO

	ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_Reference_BackgroundCheck] FOREIGN KEY([BackgroundCheckId])
	REFERENCES [dbo].[BackgroundCheck] ([Id])
	GO

	ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_Reference_BackgroundCheck]
	GO

	ALTER TABLE [dbo].[SupplementaryAgreement]  WITH CHECK ADD  CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers] FOREIGN KEY([PersonnelManagersId])
	REFERENCES [dbo].[PersonnelManagers] ([Id])
	GO

	ALTER TABLE [dbo].[SupplementaryAgreement] CHECK CONSTRAINT [FK_SupplementaryAgreement_PersonnelManagers]
	GO



	