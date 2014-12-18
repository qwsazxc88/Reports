using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Модуль договоров для работы с базой данных.
    /// </summary>
    public class GpdContractDao : DefaultDao<GpdContract>, IGpdContractDao
    {
        public GpdContractDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<GpdPermissionDto> GetPermission(UserRole role)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[GpdPermission] WHERE RoleID = " + (int)role + " and MenuID = 2";
            IQuery query = CreatePermissionQuery(sqlQuery);
            IList<GpdPermissionDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdPermissionDto))).List<GpdPermissionDto>();
            return documentList;
        }
        public virtual IQuery CreatePermissionQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("IsCreate", NHibernateUtil.Boolean).
                AddScalar("IsDraft", NHibernateUtil.Boolean).
                AddScalar("IsWrite", NHibernateUtil.Boolean).
                AddScalar("IsCancel", NHibernateUtil.Boolean).
                AddScalar("IsComment", NHibernateUtil.Boolean).
                AddScalar("IsCreateAct", NHibernateUtil.Boolean);
        }
        /// <summary>
        /// Список статусов договоров.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<GpdContractStatusesDto> GetStatuses(UserRole role,
                                                    int Id,
                                                    string Name)
        {
            string sqlQuery = @"SELECT * FROM dbo.vwGpdStatus";

            IQuery query = CreateRefQuery(sqlQuery);
            IList<GpdContractStatusesDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractStatusesDto))).List<GpdContractStatusesDto>();
            return documentList;
        }
        /// <summary>
        /// Создание запроса для простых справочников.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateRefQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
        }
        /// <summary>
        /// Список видов начисления.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<GpdContractChargingTypesDto> GetChargingTypes(UserRole role,
                                                    int Id,
                                                    string Name)
        {
            string sqlQuery = @"SELECT * FROM vwGpdChargingType";

            IQuery query = CreateRefQuery(sqlQuery);
            IList<GpdContractChargingTypesDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractChargingTypesDto))).List<GpdContractChargingTypesDto>();
            return documentList;
        }
        /// <summary>
        /// Список физических лиц.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<GpdContractSurnameDto> GetPersons(UserRole role,
                                                    int Id,
                                                    string Name)
        {
            string sqlQuery = @"SELECT * FROM vwGpdRefPersons ORDER BY Name";

            IQuery query = CreateRefQuery(sqlQuery);
            IList<GpdContractSurnameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractSurnameDto))).List<GpdContractSurnameDto>();
            return documentList;
        }
        public IList<GpdContractDetailDto> GetDetails(UserRole role,
                                                    int DTID,
                                                    int Id,
                                                    string Name)
        {
            string sqlQuery = @"SELECT * FROM vwGpdRefDetail WHERE DTID = " + (DTID == 0 ? "" : DTID.ToString()) + " ORDER BY Name";

            IQuery query = CreateRefQuery(sqlQuery);
            IList<GpdContractDetailDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractDetailDto))).List<GpdContractDetailDto>();
            return documentList;
        }
        /// <summary>
        /// Список договоров.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="DepartmentId"></param>
        /// <param name="CTID"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="Surname"></param>
        /// <param name="IsFind"></param>
        /// <param name="SortBy"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        public IList<GpdContractDto> GetContracts(UserRole role,
                                            int Id,
                                            int DepartmentId,
                                            int CTID,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            string Surname,
                                            bool IsFind,
                                            int SortBy,
                                            bool? SortDescending)
        {

            string sqlQuery = @"SELECT  *  FROM [dbo].[vwGpdContractList]";

            string SqlWhere = "";

            if (!IsFind)
                SqlWhere = "Id = " + Id.ToString();
            else
            {
                SqlWhere = (DepartmentId != 0 ? " DepartmentId = " + DepartmentId.ToString() : "");//подразделение
                SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + (CTID != 0 ? " CTID = " + CTID.ToString() : "");//вид начисления
                SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + " DateBegin between '" + DateBegin.Value.ToString("d") + "' and '" + DateEnd.Value.ToString("d") + "'";//дата начала действия договора
                
                if (Surname != null)
                    SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + (Surname.Trim().Length != 0 ? " Surname like '" + Surname + "%'" : "");//по фио
            }

            sqlQuery = sqlQuery + (SqlWhere.Length != 0 ? " WHERE " + SqlWhere : "") + SqlOrderBy(SortBy, SortDescending);

            IQuery query = CreateContractQuery(sqlQuery);
            IList<GpdContractDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractDto))).List<GpdContractDto>();
            return documentList;
        }
        /// <summary>
        /// Составляем строку для сортировки в запросе.
        /// </summary>
        /// <param name="SortBy"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        private string SqlOrderBy(int SortBy, bool? SortDescending)
        {
            switch (SortBy)
            {
                case 1:
                    return " ORDER BY Id " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 2:
                    return " ORDER BY Surname " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 3:
                    return " ORDER BY CTName " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 4:
                    return " ORDER BY Id " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 5:
                    return " ORDER BY DateBegin " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 6:
                    return " ORDER BY DateEnd " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 7:
                    return " ORDER BY DateP " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 8:
                    return " ORDER BY DepLevel3Name " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 9:
                    return " ORDER BY DepLevel7Name " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 10:
                    return " ORDER BY StatusName " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
            }
            return "";
        }
        /// <summary>
        /// Создание запроса для договоров.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateContractQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("CreatorID", NHibernateUtil.Int32).
                AddScalar("DepartmentId", NHibernateUtil.Int32).
                AddScalar("DepartmentName", NHibernateUtil.String).
                AddScalar("PersonID", NHibernateUtil.Int32).
                AddScalar("CTID", NHibernateUtil.Int32).
                AddScalar("CTName", NHibernateUtil.String).
                AddScalar("StatusID", NHibernateUtil.Int32).
                AddScalar("StatusName", NHibernateUtil.String).
                AddScalar("NumContract", NHibernateUtil.String).
                AddScalar("NameContract", NHibernateUtil.String).
                AddScalar("DateBegin", NHibernateUtil.DateTime).
                AddScalar("DateEnd", NHibernateUtil.DateTime).
                AddScalar("DateP", NHibernateUtil.DateTime).
                AddScalar("DatePOld", NHibernateUtil.DateTime).
                AddScalar("PayeeID", NHibernateUtil.Int32).
                AddScalar("PayerID", NHibernateUtil.Int32).
                AddScalar("GPDID", NHibernateUtil.String).
                AddScalar("PurposePayment", NHibernateUtil.String).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("Surname", NHibernateUtil.String).
                AddScalar("Autor", NHibernateUtil.String).
                AddScalar("DepLevel3Name", NHibernateUtil.String).
                AddScalar("DepLevel7Name", NHibernateUtil.String).
                AddScalar("IsLong", NHibernateUtil.Boolean);
        }
        /// <summary>
        /// Достаем уровень подразделения.
        /// </summary>
        /// <param name="Id">ID подразделения.</param>
        /// <returns></returns>
        public int GetDepLevel(int Id)
        {
            IQuery query = CreateDLQuery("SELECT ItemLevel FROM dbo.Department WHERE Id = " + Id.ToString());
            return query.UniqueResult<int>();
        }
        /// <summary>
        /// Создание подзапроса.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateDLQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("ItemLevel", NHibernateUtil.Int32);
        }
    }
}
