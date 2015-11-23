--insert into WebAppSKB.dbo.ExpressVolga
--����� �� ������ ����������, ������� �������������� ����������� ��, �� �� ����������� ��������
--� ������� ������ �� ������, ����� ��������� ������ ������������ �� ������������


--��������� �����
SELECT A.[����� ��������], A.�����
			 ,substring(A.[����� ��������], 1, 6) as zipcode
			 ,substring(A.[����� ��������], 9, charindex(',', A.[����� ��������], 8) - 8)  as region

			 ,case when 
							substring(A.[����� ��������], charindex(',', A.[����� ��������], 8) + 2, 
							charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) - 
							charindex(',', A.[����� ��������], 8) + 2 - 3) like '%�-�%'
						 then 
							substring(A.[����� ��������], charindex(',', A.[����� ��������], 8) + 2, 
							charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) - 
							charindex(',', A.[����� ��������], 8) + 2 - 3) end as District
			 ,case when 
							substring(A.[����� ��������], charindex(',', A.[����� ��������], 8) + 2, 
							charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) - 
							charindex(',', A.[����� ��������], 8) + 2 - 3) not like '%�-�%'
						 then 
							substring(A.[����� ��������], charindex(',', A.[����� ��������], 8) + 2, 
							charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) - 
							charindex(',', A.[����� ��������], 8) + 2 - 3) end as City
			 ,case when charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) = 0
						 then null
						 else
							case when 
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) not like '%��%' and
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) not like '%���%'
									then 
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) end
						 end
				as City1
				,case when charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) = 0
						 then null
						 else
							case when 
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) like '%��%' or
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) like '%���%' or
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) like '%���,%'
									then 
									substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 2, 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) - 
									charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1)) end
						 end
				as Street
			 ,case when charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) + 1) = 0
						then null 
						else
							substring(A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) + 2, 
								charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1) + 1) - 
								charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], charindex(',', A.[����� ��������], 8) + 1) + 1))
						end as Street1

			 ,case when charindex('��.', A.[����� ��������]) <> 0
						 then substring(A.[����� ��������], charindex('�.', A.[����� ��������]), charindex('��.', A.[����� ��������]) - charindex('�.', A.[����� ��������])) 
						 else substring(A.[����� ��������], charindex('�.', A.[����� ��������]), 10)end as StreetNumber
			 ,case when A.[����� ��������] like '%����.%'
						 then substring(A.[����� ��������], charindex('����.', A.[����� ��������]), charindex('��.', A.[����� ��������]) - charindex('����.', A.[����� ��������])) end as Building
			 ,case when charindex('��.', A.[����� ��������]) = 0 then null else substring(A.[����� ��������], charindex('��.', A.[����� ��������]), 10) end as Apartment
INTO #Adress
FROM ExpressVolga as A


update #Adress set Street1 = null where Street1 like '�.%'

SELECT
			--����� ���������� 
			substring(���, 1, charindex(' ', a.���) - 1) as LastName	--�������
			,substring(���, len(substring(���, 1, charindex(' ', a.���))) + 2, (charindex(' ', a.���, charindex(' ', a.���) + 1)) - (charindex(' ', a.���) + 1)) as FirstName	--���
			,substring(���, charindex(' ', a.���, charindex(' ', a.���) + 1) + 1, len(a.���)) as Patronymic	--��������
			,case when isnull(a.[�������� ���������], '�������') in ('�������', '�� �������', '���������', '�����') then 0 else 1 end as IsMale	--���
			,B.Id as CitizenshipId	--����������� (������)
			,B.Id as CountryBirthId	--������ ��������
			,A.[������������] as DateOfBirth	--���� ��������
			--������� �������� (��� �������)
			--���������� ����� ��������
			,A.�����	--�����
			,C.Id as DisabilityDegreeId --������ ������������
			--���������� ������
			,A.����� as InternalPassportSeries--�����
			,A.����� as InternalPassportNumber--�����
			,A.[���� ������] as InternalPassportDateOfIssue--���� ������
			,A.����� as InternalPassportIssuedBy--��� �����
			,A.���������������� as InternalPassportSubdivisionCode--��� �������������
			,A.[���� �����������] as RegistrationDate--���� �����������
			,substring(A.[����� ��������], 1, 6) as ZipCode--�������� ������
			,A.[����� ��������]
			,D.Region--������� (��� �������)
			,D.District --����� (��� �������)
			,isnull(D.City, D.City1) as City--�����/���������� �����
			,isnull(D.Street, D.Street1) as Street--�����
			,D.StreetNumber--���
			,D.Building	--������
			,D.Apartment	--��������
			--�������� ����
			,A.[����� �/�] + ' ' + A.[����� �/�] as MilitaryCardNumber--����� �/� (�����+�����)
			,A.[���� ������ �/�] as MilitaryCardDate--���� ������
			,A.[��������� ������] as ReserveCategoryId--��������� ������
			,E.Id as RankId--�������� ������
			,case when A.[����� �/�] is not null then 9 end as SpecialityCategoryId--������
			,case when A.[��������� ��������] like '%"�"%' 
					  then 1 
						else case when A.[��������� ��������] is not null then 3 end end MilitaryValidityCategoryId--��������� ��������
			,A.[������� �����������] as Commissariat--�����������
			,A.[���� �� 11/11/2015] 
			,substring(A.[���� �� 11/11/2015], 1, charindex(' ', A.[���� �� 11/11/2015]) - 1) as Days
			,substring(A.[���� �� 11/11/2015], len(substring(A.[���� �� 11/11/2015], 1, charindex(' ', A.[���� �� 11/11/2015])) + '���� ') + 2, 
						charindex('�����', A.[���� �� 11/11/2015]) - len(substring(A.[���� �� 11/11/2015], 1, charindex(' ', A.[���� �� 11/11/2015])) + '���� ') - 3) as Months
			,substring(A.[���� �� 11/11/2015], CHARINDEX('�������', A.[���� �� 11/11/2015]) + len('�������') + 1, 2) as Years
			--InsurableExperienceYears
			--InsurableExperienceMonths
			--InsurableExperienceDays
			--OverallExperienceYears
			--OverallExperienceMonths
			--OverallExperienceDays

INTO #Anketa
FROM ExpressVolga as A
LEFT JOIN Country as B ON B.Name = A.[������]
LEFT JOIN DisabilityDegree as C ON C.Name = substring(A.[������������], 1, charindex(' ', A.[������������]) - 1)
INNER JOIN #Adress as D ON D.����� = A.�����
LEFT JOIN MilitaryRanks as E ON E.Name = A.[�������� ������]
	--�����
		--���� �� ���� ������

--select * from #Anketa

select c.Status, c.ID, b.ID as UserId, B.Cnilc
INTO #candidate
from ExpressVolga as a
inner join users as b on b.Cnilc = a.����� and b.roleid & 16384 > 0 and b.IsActive = 1
inner join EmploymentCandidate as c on c.UserId = b.Id
inner join GeneralInfo as d on d.CandidateId = c.Id
inner join BackgroundCheck as E on e.CandidateId = C.id
WHERE E.PrevApproverId is not null and E.PrevApprovalStatus = 1 and E.ApproverId is null --and C.Status = 0

select * from #candidate

--����� ����������
UPDATE GeneralInfo set FirstName = C.LastName	--�������
											 ,FirstName = C.FirstName	--���
											 ,Patronymic = C.Patronymic --��������
											 ,IsMale = C.IsMale --���
											 ,CitizenshipId = C.CitizenshipId --����������� (������)
											 ,CountryBirthId = C.CountryBirthId --������ ��������
											 ,DateOfBirth = C.DateOfBirth	--���� ��������
			--������� �������� (��� �������)
			--���������� ����� ��������
			,A.�����	--�����
			,C.Id as DisabilityDegreeId --������ ������������
FROM #candidate as A
INNER JOIN GeneralInfo as B ON B.CandidateId = A.Id
INNER JOIN #Anketa as C ON C.����� = A.Cnilc
--���������� ������
--�����������
--������ �����
--�������� ����
--�������� ������������
--���������� ������
--��
--�����


drop table #Adress
drop table #candidate
drop table #Anketa