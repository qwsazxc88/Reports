using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto.Employment2;
using System;
using System.Linq;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class EmploymentCandidateDao : DefaultDao<EmploymentCandidate>, IEmploymentCandidateDao
    {
        public EmploymentCandidateDao(ISessionManager sessionManager)
            : base(sessionManager)
        {            
        }

        #region Dependencies

        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }

        #endregion

        #region sqlSelectForCandidateList

        protected const string sqlSelectForCandidateList =
            @"select candidate.Id Id
                , candidate.QuestionnaireDate
                , candidate.UserId UserId
                , candidate.ContractNumber1C as ContractNumber1C
                , isnull(generalInfo.LastName + ' ' + generalInfo.FirstName + ' ' + generalInfo.Patronymic, candidateUser.Name) as Name
                , managers.WorkCity WorkCity
                , managers.IsSecondaryJob IsSecondaryJob
                , department.Name Department
                , dep3.Name Department3
                , position.Name Position
                , case when candidate.Status = 9 then null else personnelManagers.EmploymentDate end EmploymentDate
                , personnelManagers.EmploymentOrderNumber EmploymentOrderNumber
                , personnelManagers.EmploymentOrderDate EmploymentOrderDate
                , personnelManagers.ContractNumber ContractNumber
                , personnelManagers.ContractDate ContractDate
                , personnelManagers.ContractEndDate ContractEndDate
                , candidateUser.IsFixedTermContract IsFixedTermContract
                , managers.ProbationaryPeriod ProbationaryPeriod
                , schedule.Name Schedule
                , generalInfo.DateOfBirth DateOfBirth
                , dis.EndDate as DismissalDate
				, case when generalInfo.DisabilityCertificateNumber is null then N''
					else N'Справка '
						+ generalInfo.DisabilityCertificateSeries
						+ N' ' + generalInfo.DisabilityCertificateNumber
						+ N', дата выдачи: ' + convert(varchar, generalInfo.DisabilityCertificateDateOfIssue, 104)
						+ N', группа ' + disabilityDegree.Name
						+ N', срок действия справки: ' + convert(varchar, generalInfo.DisabilityCertificateExpirationDate, 104)
					end Disabilities
                , candidateUser.Grade Grade
				, case 
                    when candidate.Status = 10 then N'Ожидает предварительного согласования ДБ'
					when candidate.Status = 1 then N'Ожидает согласование ДБ'
					when candidate.Status = 2 then N'Обучение'
                    when candidate.Status = 3 then N'Ожидается заявление о приеме'
					when candidate.Status = 4 then N'Ожидает согласование руководителем'
					when candidate.Status = 5 then N'Ожидает согласование вышестоящим руководителем'
					when candidate.Status = 6 then N'Оформление Кадры'
                    when candidate.Status = 11 then N'Контроль руководителя - пакет документов на подпись'
                    when candidate.Status = 12 then N'Документы подписаны кандидатом'
					when candidate.Status = 7 then N'Оформлен'
					when candidate.Status = 8 then N'Выгружено в 1С'
					when candidate.Status = 9 then N'Отклонен'
                    when candidate.Status < 5 and isnull(N.IsBlocked, 0) = 1 then N'Временно заблокирован'
					else N'Анкета в стадии заполнения'
					end Status
                , managers.ManagerApprovalStatus IsApprovedByManager
                , managers.HigherManagerApprovalStatus IsApprovedByHigherManager
                , supplementaryAgreement.CreateDate SupplementaryAgreementCreateDate
                , supplementaryAgreement.Number SupplementaryAgreementNumber
                , supplementaryAgreement.OrderCreateDate IndefiniteContractOrderCreateDate
                , supplementaryAgreement.OrderNumber IndefiniteContractOrderNumber
                , case
                    when supplementaryAgreement.Id > 0
                        then 1
                    else 0
                    end IsContractChangedToIndefinite
                , case
                    when candidate.AppointmentCreatorId = :currentId and candidateUser.IsFixedTermContract = 0
                        then 1
                    else 0
                    end IsChangeContractToIndefiniteAvailable
				, case
					when candidate.Status = 3 and candidate.AppointmentCreatorId = :currentId 
						then 1
					else 0
					end IsApproveByManagerAvailable
				, case
					when candidate.Status = 4 and appointmentCreatorDepartment.Path like currentDepartment.Path + N'%' then 1
					else 0
					end IsApproveByHigherManagerAvailable
                , appointmentCreator.Name as AppointmentManager
                , Personnel.Name as PersonnelName
                , case when candidate.IsTrainingNeeded = 0 then 'Не требуется'
							 when candidate.IsTrainingNeeded = 1 and isnull(J.IsComplete, 0) = 1 and isnull(J.IsFinal, 0) = 1 then 'Пройдено'
							 when candidate.IsTrainingNeeded = 1 and isnull(J.IsComplete, 0) = 0 and isnull(J.IsFinal, 0) = 1 then 'Не пройдено'
							 when candidate.IsTrainingNeeded = 1 and isnull(J.IsComplete, 0) = 0 and isnull(J.IsFinal, 0) = 0 then 'Проводится' end as Training
                ,personnelManagers.CompleteDate as CompleteDate
                ,case when K.cnt is null or (isnull(K.cnt, 0) <> 0)  then N'' else N'Документы подписаны' end as DocStatus
                ,candidate.AppointmentReportId
                ,cast(L2.Number as nvarchar(10)) + N'/'  + cast(M.SecondNumber as nvarchar(10)) as AppointmentReportNumber
                ,case
					 when candidate.AppointmentId is null then L2.Id
					 else candidate.AppointmentId 
				 end as AppointmentId
                ,case
					 when L.Number is null then L2.Number
					 else L.Number
				end as AppointmentNumber
                ,isnull(candidate.IsTechDissmiss, 0) as IsTechDissmiss
                ,cast(case when candidate.Status < 5 and isnull(N.IsBlocked, 0) = 1 then 1 else 0 end as bit) as IsBlocked
                ,managers.MentorName
                ,managers.PlanRegistrationDate
                ,isnull(candidate.IsTKReceived, 0) as IsTKReceived
                ,candidate.TKReceivedDate
                ,isnull(candidate.IsTDReceived, 0) as IsTDReceived
                ,candidate.TDReceivedDate
              from dbo.EmploymentCandidate candidate
                left join dbo.GeneralInfo generalInfo on candidate.GeneralInfoId = generalInfo.Id
                left join dbo.Dismissal dis on candidate.UserId=dis.UserId and dis.SendTo1C is not null
                left join dbo.Managers managers on candidate.ManagersId = managers.Id
                left join dbo.PersonnelManagers personnelManagers on candidate.PersonnelManagersId = personnelManagers.Id
                left join dbo.SupplementaryAgreement supplementaryAgreement on supplementaryAgreement.PersonnelManagersId = personnelManagers.Id
                left join dbo.Department department on managers.DepartmentId = department.Id
                left join dbo.Department as dep3 ON department.[Path] like dep3.[Path] + N'%' and dep3.ItemLevel = 3 
                left join dbo.Position position on managers.PositionId = position.Id
                left join dbo.Schedule schedule on personnelManagers.ScheduleId = schedule.Id
                left join dbo.Users candidateUser on candidate.UserId = candidateUser.Id
                left join dbo.DisabilityDegree disabilityDegree on generalInfo.DisabilityDegreeId = disabilityDegree.Id
                inner join dbo.Users currentUser on :currentId = currentUser.Id
                left join dbo.Department currentDepartment on currentUser.DepartmentId = currentDepartment.Id
                left join dbo.Users appointmentCreator on candidate.AppointmentCreatorId = appointmentCreator.Id
                inner join dbo.Department appointmentCreatorDepartment on appointmentCreator.DepartmentId = appointmentCreatorDepartment.Id
                left join dbo.Users as Personnel ON Personnel.id = candidate.PersonnelId
                inner join OnsiteTraining as J ON J.Id = candidate.OnsiteTrainingId
                LEFT JOIN (SELECT A.CandidateId, A.cnt - isnull(B.cnt, 0) as cnt 
						    FROM (SELECT CandidateId, count(CandidateId) as cnt FROM EmploymentCandidateDocNeeded WHERE IsNeeded = 1 GROUP BY CandidateId) as A
						    LEFT JOIN (SELECT B.CandidateId, count(B.CandidateId) as cnt
										FROM RequestAttachment as A
										INNER JOIN EmploymentCandidateDocNeeded as B ON B.CandidateId = A.RequestId and B.DocTypeId = A.RequestType and B.IsNeeded = 1
										GROUP BY B.CandidateId) as B ON B.CandidateId = A.CandidateId) as K ON K.CandidateId = candidate.Id
                LEFT JOIN Appointment as L ON L.Id = candidate.AppointmentId
                LEFT JOIN AppointmentReport as M ON M.Id = candidate.AppointmentReportId
                LEFT JOIN Appointment as L2 ON M.AppointmentId=L2.Id
                LEFT JOIN (SELECT B.AppointmentId, C.BankAccountantAcceptCount, count(B.AppointmentId) as CandidateCount,
								sum(case when A.Status = 8 then 1 else 0 end) as CandidateComplete,
								sum(case when A.Status = 9 then 1 else 0 end) as CandidateReject,
								sum(case when A.Status in (5, 6, 7, 8) then 1 else 0 end) as CandidateAgree,	--выгруженные и согласованные
								cast(case when sum(case when A.Status in (5, 6, 7, 8) then 1 else 0 end) = C.BankAccountantAcceptCount then 1 else 0 end as bit) as IsBlocked
				            FROM EmploymentCandidate as A
				            INNER JOIN AppointmentReport as B ON B.Id = A.AppointmentReportId
				            INNER JOIN Appointment as C ON C.Id = B.AppointmentId
				            GROUP BY B.AppointmentId, C.BankAccountantAcceptCount) as N ON N.AppointmentId = candidate.AppointmentId
            ";

        #endregion
        public IList<EmploymentCandidate> GetSomeCandidate()
        {
            return Session.Query<EmploymentCandidate>().Where(x => x.Appointment.Creator.Name.Contains("Поляк")).ToList();
        }
        public void CancelCandidatesByAppointmentId(int Id)
        {
            if (Id <= 0) return;
            var candidates = Session.Query<EmploymentCandidate>().Where(x => !x.SendTo1C.HasValue && x.Appointment.Id == Id).ToList();
            foreach (var el in candidates)
            {
                el.Status = Enum.EmploymentStatus.REJECTED;
                SaveAndFlush(el);
            }

        }
        public IList<CandidateDto> GetCandidates(int currentId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                DateTime? CompleteDate,
                DateTime? EmploymentDateBegin,
                DateTime? EmploymentDateEnd,
                string userName,
                string ContractNumber1C,
                int CandidateId,
                string AppointmentReportNumber,
                int AppointmentNumber,
                int PersonnelId,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForCandidateList;
            string whereString = GetWhereForUserRole(role, currentId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetCandidateIdWhere(whereString, CandidateId);
            whereString = GetDatesWhere(whereString, beginDate, endDate, CompleteDate, EmploymentDateBegin, EmploymentDateEnd);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetPersonnelWhere(whereString, PersonnelId);
            whereString = GetContractNumber1CWhere(whereString, ContractNumber1C);
            whereString = GetAppointmentWhere(whereString, AppointmentReportNumber, AppointmentNumber);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);

            AddNamedParamsToQuery(query, currentId, beginDate, endDate, userName, ContractNumber1C, CompleteDate, AppointmentReportNumber, AppointmentNumber, EmploymentDateBegin, EmploymentDateEnd);

            //AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean<CandidateDto>()).List<CandidateDto>();
        }

        public IList<EmploymentCandidate> LoadForIdsList(IList<int> ids)
        {
            if (ids.Count == 0)
                return new List<EmploymentCandidate>();
            ICriteria criteria = Session.CreateCriteria(typeof(EmploymentCandidate));
            criteria.Add(Restrictions.In("Id", ids.ToList()));
            return criteria.List<EmploymentCandidate>();
        }

        public override string GetWhereForUserRole(UserRole role, int currentId)
        {            
            string sqlQueryPart = string.Empty;
            User currentUser = UserDao.Load(currentId);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", currentId));

            if ((role & UserRole.Manager) == UserRole.Manager)
            {
                //// кандидаты, которых текущий пользователь может согласовать как руководитель, создавший заявку на подбор
                //sqlQueryPart = @" candidate.AppointmentCreatorId = :currentId ";

                string IdList = string.Empty;
                //решили, что показывать кандидата нужно не только руководителю-инициатору, но и его замам. До этого работала строка, закомментаренная выше
                IList<User> managers = DepartmentDao.GetDepartmentManagers(currentUser.Department.Id, false)
                        .Where<User>(x => x.Level == currentUser.Level && x.RoleId == (int)UserRole.Manager)
                        .ToList<User>();
                foreach (User mu in managers)
                {
                    if (!string.IsNullOrEmpty(mu.Email))
                    {
                        //Emailaddress += (string.IsNullOrEmpty(Emailaddress) ? "" : ", ") + "zagryazkin@ruscount.ru";//для теста
                        IdList += (string.IsNullOrEmpty(IdList) ? "" : ", ") + mu.Id.ToString();//рабочая строка
                    }
                }

                sqlQueryPart = string.Format(@" candidate.AppointmentCreatorId in ({0}) ", IdList);

                // кандидаты, которых текущий пользователь может согласовать как вышестоящий руководитель
                switch (currentUser.Level)
                {
                    // руководитель 2 уровня видит кандидатов, заявки на подбор которых создавали руководители 3 уровня его ветки
                    case 2:
                        sqlQueryPart += @" or (appointmentCreatorDepartment.Path like currentDepartment.Path + N'%' and appointmentCreator.Level in (2, 3))
                            ";
                        break;
                    // руководитель 3 уровня видит кандидатов, заявки на подбор которых создавали руководители нижележащих уровней его ветки
                    case 3:
//                        sqlQueryPart += @" or (appointmentCreatorDepartment.Path like currentDepartment.Path + N'%' and appointmentCreator.Level > 3)
//                            ";
//                        break;
                    case 4:
                    case 5:
                        // руководители уровней ниже 3 согласуют кандидатов созданных руководителями уровнем ниже
                        sqlQueryPart += @" or (appointmentCreatorDepartment.Path like currentDepartment.Path + N'%' and appointmentCreator.Level > " + currentUser.Level.ToString() + @")
                            ";
                        break;
                    default:
                        break;
                }
                sqlQueryPart = string.Format("( {0} )", sqlQueryPart);
                // Ручные привязки человек-человек и человек-подразделение из ManualRoleRecord
                sqlQueryPart += string.Format(@"
                        or appointmentCreator.Id in (select mrr.TargetUserId from [dbo].[ManualRoleRecord] mrr where mrr.UserId = {0} and mrr.RoleId = 3)", currentUser.Id);
                sqlQueryPart += string.Format(@"
                        or 
                        (
                            --(u.RoleId & 2) > 0
                            --and
                            candidateUser.DepartmentId in
                            (
                                select distinct branchDept.Id from [dbo].[ManualRoleRecord] mrr
                                    inner join Department targetDept
                                        on targetDept.Id = mrr.TargetDepartmentId
                                    inner join [dbo].[Department] branchDept
                                        on branchDept.Path like targetDept.Path + '%'
                                    inner join Users
                                        on mrr.UserId = {0}
                                    where mrr.RoleId = 3
                            )
                        )
                        ", currentUser.Id);
                sqlQueryPart = string.Format("( {0} )", sqlQueryPart);
            }
            else if ((role & UserRole.PersonnelManager) > 0)//для кадровиков
            {
                sqlQueryPart += string.Format(@" candidate.PersonnelId in (SELECT PersonnelId FROM vwEmploymentPersonnels WHERE UserId = {0})", currentUser.Id);
            }
            else if ((role & (UserRole.PersonnelManager
                | UserRole.Security
                | UserRole.ConsultantPersonnel
                | UserRole.Trainer
                | UserRole.OutsourcingManager
                | UserRole.StaffManager)) > 0)
            {
                //сотрудников ДБ и тренеров дополнительная фильтрация сейчас не производится
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }

            return sqlQueryPart;
        }

        public override string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != -2)
            {
                whereString = string.Format(@"{0} case when candidate.Status < 5 and isnull(N.IsBlocked, 0) = 1 then -1 else candidate.Status end = {1}", (whereString.Length > 0 ? whereString + @" and" : string.Empty), statusId);
            }
            //whereString = string.Format(@"{0} candidate.Status = {1}", (whereString.Length > 0 ? whereString + @" and" : string.Empty), statusId);

            return whereString;
        }

        public string GetCandidateIdWhere(string whereString, int CandidatId)
        {
            if (CandidatId > 0)
            {
                whereString = string.Format(@"{0} candidate.Id = {1}", (whereString.Length > 0 ? "(" + whereString + @") and" : string.Empty), CandidatId);
            }

            return whereString;
        }

        public string GetDatesWhere(string whereString, DateTime? beginDate, DateTime? endDate, DateTime? CompleteDate, DateTime? EmploymentDateBegin, DateTime? EmploymentDateEnd)
        {
            if (beginDate.HasValue)
            {
                whereString = string.Format(@"{0} cast(candidate.QuestionnaireDate as date) >= :beginDate",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }
            if (endDate.HasValue)
            {
                whereString = string.Format(@"{0} cast(candidate.QuestionnaireDate as date) <= :endDate",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }


            if (CompleteDate.HasValue)
            {
                whereString = string.Format(@"{0} cast(personnelManagers.CompleteDate as date) = :CompleteDate",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            if (EmploymentDateBegin.HasValue)
            {
                whereString = string.Format(@"{0} cast(personnelManagers.EmploymentDate as date) >= :EmploymentDateBegin",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }
            if (EmploymentDateEnd.HasValue)
            {
                whereString = string.Format(@"{0} cast(personnelManagers.EmploymentDate as date) <= :EmploymentDateEnd",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            return whereString;
        }

        public override string GetDepartmentWhere(string whereString, int departmentId)
        {
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                {
                    whereString += @" and ";
                }
                Department department = DepartmentDao.Load(departmentId);
                whereString += string.Format(@" department.Path  like '{0}' and department.ItemLevel = {1}", department.Path + "%", 7);
            }
            return whereString;
        }

        public override string GetUserNameWhere(string whereString, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                whereString = string.Format(@"{0} isnull((generalInfo.LastName + ' ' + generalInfo.FirstName + ' ' + generalInfo.Patronymic), candidateUser.Name) like N'%' + :userName + N'%'",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            return whereString;
        }

        public string GetPersonnelWhere(string whereString, int PersonnelId)
        {
            if (PersonnelId != 0)
            {
                whereString = string.Format(@"{0} candidate.PersonnelId = " + PersonnelId.ToString(),
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            return whereString;
        }

        public string GetContractNumber1CWhere(string whereString, string ContractNumber1C)
        {
            if (!string.IsNullOrEmpty(ContractNumber1C))
            {
                whereString = string.Format(@"{0} candidate.ContractNumber1C like N'%' + :ContractNumber1C + N'%'",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            return whereString;
        }

        public string GetAppointmentWhere(string whereString, string AppointmentReportNumber, int AppointmentNumber)
        {
            if (!string.IsNullOrEmpty(AppointmentReportNumber))
            {
                whereString = string.Format(@"{0} cast(L.Number as nvarchar(10)) + N'/' + cast(M.Number as nvarchar(10)) + N'_' + cast(M.SecondNumber as nvarchar(10)) = :AppointmentReportNumber ",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            if (AppointmentNumber != 0)
            {
                whereString = string.Format(@"{0} L.Number = :AppointmentNumber ",
                    (whereString.Length > 0 ? whereString + @" and" : string.Empty));
            }

            return whereString;
        }

        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
            {
                sqlQuery += @" where " + whereString;
            }

            switch (sortedBy)
            {
                case 1:
                    orderBy = "candidate.Id";
                    break;
                case 2:
                    orderBy = "QuestionnaireDate";
                    break;
                case 3:
                    orderBy = "Name";
                    break;
                case 4:
                    orderBy = "Position";
                    break;
                case 5:
                    orderBy = "Department3";
                    break;
                case 6:
                    orderBy = "Department";
                    break;
                case 7:
                    orderBy = "WorkCity";
                    break;
                case 8:
                    orderBy = "IsSecondaryJob";
                    break;
                case 9:
                    orderBy = "ProbationaryPeriod";
                    break;
                case 10:
                    orderBy = "Disabilities";
                    break;
                case 11:
                    orderBy = "Training";
                    break;
                case 12:
                    orderBy = "EmploymentDate";
                    break;
                case 13:
                    orderBy = "AppointmentManager";
                    break;
                case 14:
                    orderBy = "PersonnelName";
                    break;
                case 15:
                    orderBy = "Status";
                    break;
                case 16:
                    orderBy = "ContractNumber1C";
                    break;
                case 17:
                    orderBy = "CompleteDate";
                    break;
                case 18:
                    orderBy = "DocStatus";
                    break;
                case 19:
                    orderBy = "AppointmentReportNumber";
                    break;
                case 20:
                    orderBy = "AppointmentNumber";
                    break;
                case 21:
                    orderBy = "DismissalDate";
                    break;
                case 22:
                    orderBy = "MentorName";
                    break;
                case 23:
                    orderBy = "PlanRegistrationDate";
                    break;
                case 24:
                    orderBy = "TKReceivedDate";
                    break;
                case 25:
                    orderBy = "TDReceivedDate";
                    break;
                default:
                    orderBy = "candidate.Id desc";
                    break;
            }

            orderBy = string.Format(@" order by {0} {1} ", orderBy, (orderBy.Length > 0 && sortDescending.HasValue && sortDescending.Value) ? "desc" : string.Empty);

            sqlQuery += orderBy;

            return sqlQuery;
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("QuestionnaireDate", NHibernateUtil.DateTime)
                .AddScalar("UserId", NHibernateUtil.Int32)
                .AddScalar("ContractNumber1C", NHibernateUtil.String)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("WorkCity", NHibernateUtil.String)
                .AddScalar("Department", NHibernateUtil.String)
                .AddScalar("Department3", NHibernateUtil.String)
                .AddScalar("Position", NHibernateUtil.String)
                .AddScalar("EmploymentDate", NHibernateUtil.DateTime)
                .AddScalar("EmploymentOrderNumber", NHibernateUtil.String)
                .AddScalar("EmploymentOrderDate", NHibernateUtil.DateTime)
                .AddScalar("ContractNumber", NHibernateUtil.String)
                .AddScalar("ContractDate", NHibernateUtil.DateTime)
                .AddScalar("ContractEndDate", NHibernateUtil.DateTime)
                .AddScalar("IsFixedTermContract", NHibernateUtil.Boolean)
                .AddScalar("ProbationaryPeriod", NHibernateUtil.String)
                .AddScalar("Schedule", NHibernateUtil.String)
                .AddScalar("DateOfBirth", NHibernateUtil.DateTime)
                .AddScalar("Disabilities", NHibernateUtil.String)
                .AddScalar("Grade", NHibernateUtil.Int32)
                .AddScalar("Status", NHibernateUtil.String)
                .AddScalar("IsChangeContractToIndefiniteAvailable", NHibernateUtil.Boolean)
                .AddScalar("IsApprovedByManager", NHibernateUtil.Boolean)
                .AddScalar("IsApprovedByHigherManager", NHibernateUtil.Boolean)
                .AddScalar("IsApproveByManagerAvailable", NHibernateUtil.Boolean)
                .AddScalar("IsApproveByHigherManagerAvailable", NHibernateUtil.Boolean)
                .AddScalar("IsSecondaryJob", NHibernateUtil.Boolean)
                .AddScalar("IsContractChangedToIndefinite", NHibernateUtil.Boolean)
                .AddScalar("SupplementaryAgreementCreateDate", NHibernateUtil.DateTime)
                .AddScalar("SupplementaryAgreementNumber", NHibernateUtil.Int32)
                .AddScalar("IndefiniteContractOrderCreateDate", NHibernateUtil.DateTime)
                .AddScalar("IndefiniteContractOrderNumber", NHibernateUtil.Int32)
                .AddScalar("AppointmentManager", NHibernateUtil.String)
                .AddScalar("PersonnelName", NHibernateUtil.String)
                .AddScalar("Training", NHibernateUtil.String)
                .AddScalar("CompleteDate", NHibernateUtil.DateTime)
                .AddScalar("DocStatus", NHibernateUtil.String)
                .AddScalar("AppointmentReportId",NHibernateUtil.Int32)
                .AddScalar("AppointmentReportNumber", NHibernateUtil.String)
                .AddScalar("AppointmentId",NHibernateUtil.Int32)
                .AddScalar("AppointmentNumber", NHibernateUtil.Int32)
                .AddScalar("IsTechDissmiss", NHibernateUtil.Boolean)
                .AddScalar("IsBlocked", NHibernateUtil.Boolean)
                .AddScalar("DismissalDate", NHibernateUtil.DateTime)
                .AddScalar("MentorName", NHibernateUtil.String)
                .AddScalar("PlanRegistrationDate", NHibernateUtil.DateTime)
                .AddScalar("IsTKReceived", NHibernateUtil.Boolean)
                .AddScalar("TKReceivedDate", NHibernateUtil.DateTime)
                .AddScalar("IsTDReceived", NHibernateUtil.Boolean)
                .AddScalar("TDReceivedDate", NHibernateUtil.DateTime)
                ;

            return query;
        }

        private void AddNamedParamsToQuery(IQuery query, int currentId, DateTime? beginDate, DateTime? endDate, string userName, string ContractNumber1C, DateTime? CompleteDate, string AppointmentReportNumber, int AppointmentNumber,
            DateTime? EmploymentDateBegin, DateTime? EmploymentDateEnd)
        {
            query.SetInt32("currentId", currentId);

            if (beginDate.HasValue)
            {
                query.SetDateTime("beginDate", beginDate.Value);
            }
            if (endDate.HasValue)
            {
                query.SetDateTime("endDate", endDate.HasValue ? endDate.Value : DateTime.Now);
            }

            if (CompleteDate.HasValue)
                query.SetDateTime("CompleteDate", CompleteDate.Value);

            if (EmploymentDateBegin.HasValue)
            {
                query.SetDateTime("EmploymentDateBegin", EmploymentDateBegin.Value);
            }
            if (EmploymentDateEnd.HasValue)
            {
                query.SetDateTime("EmploymentDateEnd", EmploymentDateEnd.HasValue ? EmploymentDateEnd.Value : DateTime.Now);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                query.SetString("userName", userName);
            }

            if (!string.IsNullOrEmpty(ContractNumber1C))
            {
                query.SetString("ContractNumber1C", ContractNumber1C);
            }

            if (!string.IsNullOrEmpty(AppointmentReportNumber))
            {
                query.SetString("AppointmentReportNumber", AppointmentReportNumber);
            }

            if (AppointmentNumber != 0)
            {
                query.SetInt32("AppointmentNumber", AppointmentNumber);
            }
        }
        /// <summary>
        /// Состояние кандидата
        /// </summary>
        /// <param name="CandidateID">Id кандидата</param>
        /// <returns></returns>
        public IList<CandidateStateDto> GetCandidateState(int CandidateID)
        {
            IQuery query = CreateCandidateStateQuery("SELECT * FROM vwEmploymentFillState WHERE Id = " + CandidateID.ToString());
            return query.SetResultTransformer(Transformers.AliasToBean<CandidateStateDto>()).List<CandidateStateDto>();
        }
        public virtual IQuery CreateCandidateStateQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("ScanFinal", NHibernateUtil.Boolean)
                .AddScalar("GeneralFinal", NHibernateUtil.Boolean)
                .AddScalar("PassportFinal", NHibernateUtil.Boolean)
                .AddScalar("EducationFinal", NHibernateUtil.Boolean)
                .AddScalar("FamilyFinal", NHibernateUtil.Boolean)
                .AddScalar("MilitaryFinal", NHibernateUtil.Boolean)
                .AddScalar("ExperienceFinal", NHibernateUtil.Boolean)
                .AddScalar("ContactFinal", NHibernateUtil.Boolean)
                .AddScalar("BackgroundFinal", NHibernateUtil.Boolean)
                .AddScalar("CandidateApp", NHibernateUtil.Boolean)
                .AddScalar("CandidateReady", NHibernateUtil.Boolean)
                .AddScalar("BackgroundApproval", NHibernateUtil.Boolean)
                .AddScalar("PrevBackgroundApproval", NHibernateUtil.Boolean)
                .AddScalar("TrainingApproval", NHibernateUtil.Boolean)
                .AddScalar("ManagerApproval", NHibernateUtil.Boolean)
                .AddScalar("PersonnelManagerApproval", NHibernateUtil.Boolean)
                ;

            return query;
        }
        public IList<CandidatePersonnelDto> GetPersonnels()
        {
            //в запросе исключены Ибрагимова, Рогозина, Тиханова, Чеснова, Букова (осн)
            IQuery query = CreatePersonnelsQuery(@"SELECT 0 as Id, null as Name
                                                   UNION ALL
                                                   SELECT Id, Name FROM Users WHERE Code like N'%K' and IsActive = 1 and Id not in (1002, 998, 991, 1006, 990)
                                                   ORDER BY Name");
            return query.SetResultTransformer(Transformers.AliasToBean<CandidatePersonnelDto>()).List<CandidatePersonnelDto>();
        }
        public virtual IQuery CreatePersonnelsQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String)
                ;

            return query;
        }
        /// <summary>
        /// Список сканов документов по приему.
        /// </summary>
        /// <param name="CandidateID">Id кандидата</param>
        /// <returns></returns>
        public IList<AttachmentListDto> GetCandidateAttachmentList(int CandidateID)
        {
            IQuery query = CreateAttachmentListQuery("SELECT  ROW_NUMBER() OVER(ORDER BY AttachmentTypeName) as RowNumber, * FROM dbo.fnGetEmploymentAttachmentList(" + CandidateID.ToString() + ")");
            return query.SetResultTransformer(Transformers.AliasToBean<AttachmentListDto>()).List<AttachmentListDto>();
        }
        public virtual IQuery CreateAttachmentListQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                //.AddScalar("Id", NHibernateUtil.Int32)
                //.AddScalar("AttachmentType", NHibernateUtil.Int32)
                .AddScalar("RowNumber", NHibernateUtil.Int32)
                .AddScalar("AttachmentTypeName", NHibernateUtil.String)
                .AddScalar("AtachmentAvalable", NHibernateUtil.String)
                ;

            return query;
        }
        /// <summary>
        /// Достаем список сканов из анкеты.
        /// </summary>
        /// <param name="CandidateID">Id заявки кандидата.</param>
        /// <returns></returns>
        public IList<EmploymentAttachmentDto> GetCandidateQuestAttachmentList(int CandidateID)
        {
            IQuery query = Session.CreateSQLQuery("SELECT * FROM dbo.vwEmploymentScanInfo WHERE CandidateID = " + CandidateID.ToString())
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("CandidateId", NHibernateUtil.Int32)
                .AddScalar("RequestType", NHibernateUtil.Int32)
                .AddScalar("FileName", NHibernateUtil.String)
                .AddScalar("DateCreated", NHibernateUtil.DateTime)
                .AddScalar("Surname", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean<EmploymentAttachmentDto>()).List<EmploymentAttachmentDto>();
        }
    }
}