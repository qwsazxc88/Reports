﻿using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto;
using System.Linq;
using NHibernate.Linq;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник штатных единиц.
    /// </summary>
    public class StaffEstablishedPostDao : DefaultDao<StaffEstablishedPost>, IStaffEstablishedPostDao
    {
        public StaffEstablishedPostDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Список штатных единиц к подразделению.
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public IList<StaffEstablishedPostDto> GetStaffEstablishedPosts(int DepartmentId)
        {
            const string sqlQuery = (@"SELECT A.Id, A.PositionId, B.Name as PositionName, A.DepartmentId, A.Quantity, A.Salary, C.Path, D.Id as RequestId
                                       FROM StaffEstablishedPost as A
                                       INNER JOIN Position as B ON B.Id = A.PositionId
                                       INNER JOIN Department as C ON C.Id = A.DepartmentId
                                       LEFT JOIN StaffEstablishedPostRequest as D ON D.SEPId = A.Id and D.IsUsed = 1
                                       WHERE A.DepartmentId = :DepartmentId and A.IsUsed = 1 ORDER BY A.Priority");
            return Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("PositionId", NHibernateUtil.Int32)
                .AddScalar("PositionName", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("Quantity", NHibernateUtil.Int32)
                .AddScalar("Salary", NHibernateUtil.Decimal)
                .AddScalar("Path", NHibernateUtil.String)
                .AddScalar("RequestId", NHibernateUtil.Int32)
                .SetInt32("DepartmentId", DepartmentId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(StaffEstablishedPostDto))).
                List<StaffEstablishedPostDto>();
        }

        /// <summary>
        /// Список сотрудников с должностями к подразделению.
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public IList<StaffEstablishedPostDto> GetStaffEstablishedArrangements(int DepartmentId)
        {
            const string sqlQuery = (@"SELECT Id, SEPId, PositionId, PositionName, DepartmentId, Quantity, Salary, Path, RequestId, Rate, UserId, Surname, ReplacedId, ReplacedName, IsPregnant, IsVacation, IsSTD
                                       FROM dbo.fnGetStaffEstablishedArrangements(:DepartmentId)");
            return Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("SEPId", NHibernateUtil.Int32)
                .AddScalar("PositionId", NHibernateUtil.Int32)
                .AddScalar("PositionName", NHibernateUtil.String)
                .AddScalar("DepartmentId", NHibernateUtil.Int32)
                .AddScalar("Quantity", NHibernateUtil.Int32)
                .AddScalar("Salary", NHibernateUtil.Decimal)
                .AddScalar("Path", NHibernateUtil.String)
                .AddScalar("RequestId", NHibernateUtil.Int32)
                .AddScalar("UserId", NHibernateUtil.Int32)
                .AddScalar("Surname", NHibernateUtil.String)
                .AddScalar("Rate", NHibernateUtil.Decimal)
                .AddScalar("ReplacedId", NHibernateUtil.Int32)
                .AddScalar("ReplacedName", NHibernateUtil.String)
                .AddScalar("IsPregnant", NHibernateUtil.Boolean)
                .AddScalar("IsVacation", NHibernateUtil.Boolean)
                .AddScalar("IsSTD", NHibernateUtil.Boolean)
                .SetInt32("DepartmentId", DepartmentId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(StaffEstablishedPostDto))).
                List<StaffEstablishedPostDto>();
        }
        /// <summary>
        /// Достаем количество сотрудников, закрепленных за данной штатной единицей.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        public int GetEstablishedPostUsed(int SEPId)
        {
            return Session.Query<StaffEstablishedPostUserLinks>().Where(x => x.StaffEstablishedPost.Id == SEPId && x.User.IsActive == true && !x.User.IsPregnant.HasValue).ToList().Count;
        }
        /// <summary>
        /// Достаем связи штатной единицы и сотрудников.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        public IList<StaffEstablishedPostUserLinks> GetEstablishedPostUserLinks(int SEPId)
        {
            return Session.Query<StaffEstablishedPostUserLinks>().Where(x => x.StaffEstablishedPost.Id == SEPId).ToList();
        }
    }
}
