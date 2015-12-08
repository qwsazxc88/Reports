--insert into WebAppSKB.dbo.ExpressVolga
--берем из приема кандидатов, которых предварительно согласовали ДБ, но не согласовали вторично
--и доводим анкету до стадии, когда требуется второе согласование ДБ согласование

BEGIN TRANSACTION
--разбираем адрес
SELECT A.[Адрес прописки], A.СНИЛС
			 ,substring(A.[Адрес прописки], 1, 6) as zipcode
			 ,substring(A.[Адрес прописки], 9, charindex(',', A.[Адрес прописки], 8) - 8)  as region

			 ,case when 
							substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 2, 
							charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) - 
							charindex(',', A.[Адрес прописки], 8) + 2 - 3) like '%р-н%'
						 then 
							substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 2, 
							charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) - 
							charindex(',', A.[Адрес прописки], 8) + 2 - 3) end as District
			 ,case when 
							substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 2, 
							charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) - 
							charindex(',', A.[Адрес прописки], 8) + 2 - 3) not like '%р-н%'
						 then 
							substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 2, 
							charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) - 
							charindex(',', A.[Адрес прописки], 8) + 2 - 3) end as City
			 ,case when charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) = 0
						 then null
						 else
							case when 
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) not like '%ул%' and
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) not like '%тер%'
									then 
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) end
						 end
				as City1
				,case when charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) = 0
						 then null
						 else
							case when 
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) like '%ул%' or
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) like '%тер%' or
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) like '%пер,%'
									then 
									substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 2, 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) - 
									charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1)) end
						 end
				as Street
			 ,case when charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) + 1) = 0
						then null 
						else
							substring(A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) + 2, 
								charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1) + 1) - 
								charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], charindex(',', A.[Адрес прописки], 8) + 1) + 1))
						end as Street1

			 ,case when charindex('кв.', A.[Адрес прописки]) <> 0
						 then substring(A.[Адрес прописки], charindex('д.', A.[Адрес прописки]), charindex('кв.', A.[Адрес прописки]) - charindex('д.', A.[Адрес прописки])) 
						 else substring(A.[Адрес прописки], charindex('д.', A.[Адрес прописки]), 10)end as StreetNumber
			 ,case when A.[Адрес прописки] like '%корп.%'
						 then substring(A.[Адрес прописки], charindex('корп.', A.[Адрес прописки])
									, case when charindex('кв.', A.[Адрес прописки]) <> 0 then charindex('кв.', A.[Адрес прописки]) - charindex('корп.', A.[Адрес прописки]) else 10 end) end as Building
			 ,case when charindex('кв.', A.[Адрес прописки]) = 0 then null else substring(A.[Адрес прописки], charindex('кв.', A.[Адрес прописки]), 10) end as Apartment
INTO #Adress
FROM ExpressVolga as A


update #Adress set Street1 = null where Street1 like 'д.%'

SELECT
			--общая информация 
			substring(фио, 1, charindex(' ', a.фио) - 1) as LastName	--фамилия
			,substring(фио, len(substring(фио, 1, charindex(' ', a.фио))) + 2, (charindex(' ', a.фио, charindex(' ', a.фио) + 1)) - (charindex(' ', a.фио) + 1)) as FirstName	--имя
			,substring(фио, charindex(' ', a.фио, charindex(' ', a.фио) + 1) + 1, len(a.фио)) as Patronymic	--отчество
			,case when isnull(a.[Семейное положение], 'Замужем') in ('Замужем', 'Не замужем', 'Разведена', 'Вдова') then 0 else 1 end as IsMale	--пол
			,B.Id as CitizenshipId	--гражданство (страна)
			,B.Id as CountryBirthId	--Страна рождения
			,A.[ДатаРождения] as DateOfBirth	--дата рождения
			--область рождения (при наличии)
			--населенный пункт рождения
			,A.СНИЛС	--снилс
			,C.Id as DisabilityDegreeId --группа инвалидности
			--паспортные данные
			,A.Серия as InternalPassportSeries--серия
			,A.Номер as InternalPassportNumber--номер
			,A.[Дата выдачи] as InternalPassportDateOfIssue--дата выдачи
			,A.Выдан as InternalPassportIssuedBy--кем выдан
			,A.КодПодразделения as InternalPassportSubdivisionCode--код подразделения
			,A.[Дата регистрации] as RegistrationDate--дата регистрации
			,substring(A.[Адрес прописки], 1, 6) as ZipCode--почтовый индекс
			,A.[Адрес прописки]
			,D.Region--область (при наличии)
			,D.District --район (при наличии)
			,isnull(D.City, D.City1) as City--город/населенный пункт
			,case when substring(A.[Адрес прописки], 1, 6) = '307715' then null else isnull(D.Street, D.Street1) end as Street--улица
			,replace(REPLACE(case when D.StreetNumber not like 'д.%' then null 
						else (case when D.StreetNumber like '%корп.%' then substring(D.StreetNumber, 1, charindex('корп.', D.StreetNumber) - 1)  else D.StreetNumber end)
						end, 'д.', '' ), ',', '') as StreetNumber--дом
			,replace(replace(D.Building, 'корп.', ''), ',', '') as Building	--корпус
			,replace(D.Apartment, 'кв.', '') as Apartment	--квартира
			--воинский учет
			,A.[Серия в/б] + ' ' + A.[Номер в/б] as MilitaryCardNumber--номер в/б (серия+номер)
			,A.[Дата выдачи в/б] as MilitaryCardDate--дата выдачи
			,A.[Категория запаса] as ReserveCategoryId--категория запаса
			,E.Id as RankId--воинское звание
			,case when A.[Серия в/б] is not null then 9 end as SpecialityCategoryId--состав
			,case when A.[Категория годности] like '%"А"%' 
					  then 1 
						else case when A.[Категория годности] is not null then 3 end end MilitaryValidityCategoryId--категория годности
			,A.[Военный комиссариат] as Commissariat--комиссариат
			,A.[Стаж на 11/11/2015] 
			,substring(A.[Стаж на 11/11/2015], 1, charindex(' ', A.[Стаж на 11/11/2015]) - 1) as Days
			,substring(A.[Стаж на 11/11/2015], len(substring(A.[Стаж на 11/11/2015], 1, charindex(' ', A.[Стаж на 11/11/2015])) + 'дней ') + 2, 
						charindex('месяц', A.[Стаж на 11/11/2015]) - len(substring(A.[Стаж на 11/11/2015], 1, charindex(' ', A.[Стаж на 11/11/2015])) + 'дней ') - 3) as Months
			,substring(A.[Стаж на 11/11/2015], CHARINDEX('месяцев', A.[Стаж на 11/11/2015]) + len('месяцев') + 1, 2) as Years
			--InsurableExperienceYears
			--InsurableExperienceMonths
			--InsurableExperienceDays
			--OverallExperienceYears
			--OverallExperienceMonths
			--OverallExperienceDays

INTO #Anketa
FROM ExpressVolga as A
LEFT JOIN Country as B ON B.Name = A.[Страна]
LEFT JOIN DisabilityDegree as C ON C.Name = substring(A.[Инвалидность], 1, charindex(' ', A.[Инвалидность]) - 1)
INNER JOIN #Adress as D ON D.СНИЛС = A.СНИЛС
LEFT JOIN MilitaryRanks as E ON E.Name = A.[Воинское звание]
	--кадры
		--стаж на дату приема

--select * from #Anketa
/*
select c.Status, c.ID, b.ID as UserId, B.Cnilc
INTO #candidate
from ExpressVolga as a
inner join users as b on b.Cnilc = a.снилс and b.roleid & 16384 > 0 and b.IsActive = 1
inner join EmploymentCandidate as c on c.UserId = b.Id
inner join GeneralInfo as d on d.CandidateId = c.Id
inner join BackgroundCheck as E on e.CandidateId = C.id
WHERE E.PrevApproverId is not null and E.PrevApprovalStatus = 1 and E.ApproverId is null --and C.Status = 0
*/
SELECT D.Status, D.Id, A.Id as UserId, A.Cnilc
INTO #candidate
FROM Users as A
INNER JOIN Department as B ON B.Id = A.DepartmentId
INNER JOIN (SELECT * FROM Department WHERE Id = 11923) as C ON B.Path like C.Path + N'%'
INNER JOIN EmploymentCandidate as D ON D.UserId = A.Id and d.Status = 0
INNER JOIN BackgroundCheck as E ON E.CandidateId = D.Id and E.PrevApproverId is not null and E.PrevApprovalStatus = 1 and E.ApproverId is null
INNER JOIN ExpressVolga as F ON F.снилс = A.Cnilc
WHERE a.RoleId & 16384 > 0 and A.IsActive = 1

--select * from #candidate

--общая информация

UPDATE GeneralInfo set LastName = C.LastName	--фамилия
											 ,FirstName = C.FirstName	--имя
											 ,Patronymic = C.Patronymic --отчество
											 ,IsMale = C.IsMale --пол
											 ,CitizenshipId = C.CitizenshipId --гражданство (страна)
											 ,CountryBirthId = C.CountryBirthId --Страна рождения
											 ,DateOfBirth = C.DateOfBirth	--дата рождения
											 ,SNILS = A.Cnilc	--снилс
											 ,DisabilityDegreeId = C.DisabilityDegreeId --группа инвалидности
											 ,IsValidate = 1
											 ,IsFinal = 1
FROM #candidate as A
INNER JOIN GeneralInfo as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--паспортные данные
UPDATE Passport set InternalPassportSeries = C.InternalPassportSeries--серия
										,InternalPassportNumber = C.InternalPassportNumber--номер
										,InternalPassportDateOfIssue = C.InternalPassportDateOfIssue--дата выдачи
										,InternalPassportIssuedBy = C.InternalPassportIssuedBy--кем выдан
										,InternalPassportSubdivisionCode = C.InternalPassportSubdivisionCode--код подразделения
										,RegistrationDate = C.RegistrationDate--дата регистрации
										,ZipCode = C.ZipCode--почтовый индекс
										--,A.[Адрес прописки]
										,Region = C.Region--область (при наличии)
										,District = C.District --район (при наличии)
										,City = C.City--город/населенный пункт
										,Street = C.Street--улица
										,StreetNumber = C.StreetNumber--дом
										,Building = C.Building	--корпус
										,Apartment = C.Apartment	--квартира
										,IsValidate = 1
										,IsFinal = 1
FROM #candidate as A
INNER JOIN Passport as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--образование
UPDATE Education set IsValidate = 1
										,IsFinal = 1
FROM #candidate as A
INNER JOIN Education as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--состав семьи
UPDATE Family set IsValidate = 1
										,IsFinal = 1
FROM #candidate as A
INNER JOIN Family as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--воинский учет
UPDATE MilitaryService set IsLiableForMilitaryService = case when C.MilitaryCardNumber is null then 0 else 1 end
													,MilitaryCardNumber = C.MilitaryCardNumber--номер в/б (серия+номер)
													,MilitaryCardDate = C.MilitaryCardDate--дата выдачи
													,ReserveCategoryId = C.ReserveCategoryId--категория запаса
													,RankId = C.RankId--воинское звание
													,SpecialityCategoryId = C.SpecialityCategoryId--состав
													,MilitaryValidityCategoryId = C.MilitaryValidityCategoryId--категория годности
													,Commissariat = C.Commissariat--комиссариат
													,IsValidate = 1
													,IsFinal = 1
FROM #candidate as A
INNER JOIN MilitaryService as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--трудовая деятельность
UPDATE Experience set IsValidate = 1
											,IsFinal = 1
FROM #candidate as A
INNER JOIN Experience as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--контактные данные
UPDATE Contacts set IsValidate = 1
											,IsFinal = 1
FROM #candidate as A
INNER JOIN Contacts as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--ДБ
UPDATE BackgroundCheck set IsValidate = 1
													,IsFinal = 1
FROM #candidate as A
INNER JOIN BackgroundCheck as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc

--кадры
UPDATE PersonnelManagers set InsurableExperienceYears = C.Years
														 ,InsurableExperienceMonths = C.Months
														 ,InsurableExperienceDays = C.Days
														 ,OverallExperienceYears = C.Years
														 ,OverallExperienceMonths = C.Months
														 ,OverallExperienceDays = C.Years
FROM #candidate as A
INNER JOIN PersonnelManagers as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc


--меняем статус кандидата
UPDATE EmploymentCandidate set Status = 1
FROM #candidate as A
INNER JOIN EmploymentCandidate as B ON B.Id = A.Id
INNER JOIN #Anketa as C ON C.СНИЛС = A.Cnilc



SELECT D.Status, D.Id, A.Id as UserId, A.Cnilc
--INTO #candidate
FROM Users as A
INNER JOIN Department as B ON B.Id = A.DepartmentId
INNER JOIN (SELECT * FROM Department WHERE Id = 11923) as C ON B.Path like C.Path + N'%'
INNER JOIN EmploymentCandidate as D ON D.UserId = A.Id --and d.Status = 0
INNER JOIN BackgroundCheck as E ON E.CandidateId = D.Id and E.PrevApproverId is not null and E.PrevApprovalStatus = 1 and E.ApproverId is null
INNER JOIN ExpressVolga as F ON F.снилс = A.Cnilc
WHERE a.RoleId & 16384 > 0 and A.IsActive = 1

--ROLLBACK TRANSACTION
--COMMIT TRANSACTION


/*
drop table #Adress
drop table #candidate
drop table #Anketa
*/