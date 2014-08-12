using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto.Employment2;
using System;
using System.Linq;
using NHibernate.Transform;
using NHibernate;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCandidateDao : DefaultDao<EmploymentCandidate>, IEmploymentCandidateDao
    {
        public EmploymentCandidateDao(ISessionManager sessionManager)
            : base(sessionManager)
        {            
        }

        #region Dependencies

        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }

        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }

        #endregion

        protected const string sqlSelectForCandidateList =
            @"select candidate.Id Id
                , generalInfo.LastName + ' ' + generalInfo.FirstName + ' ' + generalInfo.Patronymic as Name
                , directorate.Name Directorate
                , managers.WorkCity WorkCity
                , department.Name Department
                , position.Name Position
                , personnelManagers.EmploymentDate EmploymentDate
                , personnelManagers.EmploymentOrderNumber EmploymentOrderNumber
                , personnelManagers.EmploymentOrderDate EmploymentOrderDate
                , personnelManagers.ContractNumber ContractNumber
                , personnelManagers.ContractDate ContractDate
                , managers.ProbationaryPeriod ProbationaryPeriod
                , schedule.Name Schedule
                , generalInfo.DateOfBirth DateOfBirth
				, case when generalInfo.DisabilityCertificateNumber is null then N''
					else N'Справка '
						+ generalInfo.DisabilityCertificateSeries
						+ N' ' + generalInfo.DisabilityCertificateNumber
						+ N', дата выдачи: ' + convert(varchar, generalInfo.DisabilityCertificateDateOfIssue, 104)
						+ N', группа ' + disabilityDegree.Name
						+ N', срок действия справки: ' + convert(varchar, generalInfo.DisabilityCertificateExpirationDate, 104)
					end Disabilities
                , users.Grade Grade
				, case
					when candidate.Status = 1 then N'Ожидает согласование СБ'
					when candidate.Status = 2 then N'Обучение'
					when candidate.Status = 3 then N'Ожидает согласование руководителем'
					when candidate.Status = 4 then N'Ожидает согласование вышестоящим руководителем'
					when candidate.Status = 6 then N'Оформление Кадры'
					when candidate.Status = 7 then N'Завершено'
					when candidate.Status = 8 then N'Выгружено в 1С'
					else N''
					end Status
                , managers.ManagerApprovalStatus IsApprovedByManager
                , managers.HigherManagerApprovalStatus IsApprovedByHigherManager
				, case
					when candidate.Status = 3 -- and candidate.AppointmentCreatorId = :userId 
						then 1
					else 0
					end IsApproveByManagerAvailable
				, case
					when candidate.Status = 4 then 1
					else 0
					end IsApproveByHigherManagerAvailable
              from dbo.EmploymentCandidate candidate
                left join dbo.GeneralInfo generalInfo on generalInfo.Id = candidate.GeneralInfoId
                left join dbo.Managers managers on managers.Id = candidate.ManagersId
                left join dbo.PersonnelManagers personnelManagers on personnelManagers.Id = candidate.PersonnelManagersId
                left join dbo.Department directorate on directorate.Id = managers.DirectorateId
                left join dbo.Department department on department.Id = managers.DepartmentId
                left join dbo.Position position on position.Id = managers.PositionId
				left join dbo.Schedule schedule on schedule.Id = managers.ScheduleId
				left join dbo.Users users on candidate.UserId = users.Id
				left join dbo.DisabilityDegree disabilityDegree on generalInfo.DisabilityDegreeId = disabilityDegree.Id
            ";

        public IList<CandidateDto> GetCandidates(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForCandidateList;
            string whereString = GetWhereForUserRole(role, userId);
            //whereString = GetStatusWhere(whereString, statusId);
            //whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            //whereString = GetUserNameWhere(whereString, userName);
            //sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);

            // Set named params
            query.SetInt32("userId", userId);

            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean<CandidateDto>()).List<CandidateDto>();
        }

        public override string GetWhereForUserRole(UserRole role, int userId)
        {
            User currentUser = UserDao.Load(userId);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", userId));
            switch (currentUser.Level)
            {
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                case 5:
                case 6:
                    break;
                default:
                    break;
            }

            return "";
        }

        public override string GetStatusWhere(string whereString, int statusId)
        {
            return whereString;
        }

        // ok
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

        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
            {
                sqlQuery += @" where " + whereString;
            }

            return sqlQuery;
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("Name", NHibernateUtil.String)
                .AddScalar("Directorate", NHibernateUtil.String)
                .AddScalar("WorkCity", NHibernateUtil.String)
                .AddScalar("Department", NHibernateUtil.String)
                .AddScalar("Position", NHibernateUtil.String)
                .AddScalar("EmploymentDate", NHibernateUtil.DateTime)
                .AddScalar("EmploymentOrderNumber", NHibernateUtil.String)
                .AddScalar("EmploymentOrderDate", NHibernateUtil.DateTime)
                .AddScalar("ContractNumber", NHibernateUtil.String)
                .AddScalar("ContractDate", NHibernateUtil.DateTime)
                .AddScalar("ProbationaryPeriod", NHibernateUtil.String)
                .AddScalar("Schedule", NHibernateUtil.String)
                .AddScalar("DateOfBirth", NHibernateUtil.DateTime)
                .AddScalar("Disabilities", NHibernateUtil.String)
                .AddScalar("Grade", NHibernateUtil.String)
                .AddScalar("Status", NHibernateUtil.String)
                .AddScalar("IsApprovedByManager", NHibernateUtil.Boolean)
                .AddScalar("IsApprovedByHigherManager", NHibernateUtil.Boolean)
                .AddScalar("IsApproveByManagerAvailable", NHibernateUtil.Boolean)
                .AddScalar("IsApproveByHigherManagerAvailable", NHibernateUtil.Boolean)
                ;
        }
    }
}