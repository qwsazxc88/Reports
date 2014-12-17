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
    /// Акты ГПД.
    /// </summary>
    public class GpdActDao : DefaultDao<GpdAct>, IGpdActDao
    {
        public GpdActDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Запрос для создания нового акта ГПД.
        /// </summary>
        /// <param name="role">Текущая роль пользователя.</param>
        /// <param name="GCID">ID договора.</param>
        /// <returns></returns>
        public IList<GpdActDto> GetNewAct(UserRole role, int GCID)
        {
            string sqlQuery = @"SELECT  *  FROM [dbo].[vwGpdActNew] WHERE GCID = " + GCID.ToString();

            IQuery query = CreateContractQuery(sqlQuery);
            IList<GpdActDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdActDto))).List<GpdActDto>();
            return documentList;
        }
        /// <summary>
        /// Запрос для просмотра/редактирования акта ГПД.
        /// </summary>
        /// <param name="role">Текущая роль пользователя.</param>
        /// <param name="ID">ID акта.</param>
        /// <param name="IsFind">Признак для составления условия запроса.</param>
        /// <param name="DateBegin">Начало периода для даты акта.</param>
        /// <param name="DateEnd">Конец периода для даты акта.</param>
        /// <param name="DepartmentId">ID подразделения в договоре.</param>
        /// <param name="Surname">ФИО физического лица.</param>
        /// <param name="StatusID">ID статуса акта.</param>
        /// <param name="SortBy">Переменная определяет поле сортировки.</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <returns></returns>
        public IList<GpdActDto> GetAct(UserRole role, 
                                        int ID, 
                                        bool IsFind,
                                        DateTime? DateBegin,
                                        DateTime? DateEnd, 
                                        int DepartmentId, 
                                        string Surname, 
                                        int StatusID,
                                        int SortBy, 
                                        bool? SortDescending)
        {
            string sqlQuery = @"SELECT  *  FROM [dbo].[vwGpdActList] ";

            if (!IsFind)
                sqlQuery += "WHERE Id = " + ID.ToString();
            else
                sqlQuery += ActListSqlWhere(DateBegin, DateEnd, DepartmentId, Surname, StatusID) + ActListSqlOrderBy(SortBy, SortDescending);

            IQuery query = CreateContractQuery(sqlQuery);
            IList<GpdActDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdActDto))).List<GpdActDto>();
            return documentList;
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
                AddScalar("ActDate", NHibernateUtil.DateTime).
                AddScalar("ActNumber", NHibernateUtil.String).
                AddScalar("GCCount", NHibernateUtil.Int32).
                AddScalar("Surname", NHibernateUtil.String).
                AddScalar("NameContract", NHibernateUtil.String).
                AddScalar("NumContract", NHibernateUtil.String).
                AddScalar("ContractBeginDate", NHibernateUtil.DateTime).
                AddScalar("ContractEndDate", NHibernateUtil.DateTime).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("DepLevel3Name", NHibernateUtil.String).
                AddScalar("ChargingDate", NHibernateUtil.DateTime).
                AddScalar("DateBegin", NHibernateUtil.DateTime).
                AddScalar("DateEnd", NHibernateUtil.DateTime).
                AddScalar("Amount", NHibernateUtil.Decimal).
                AddScalar("AmountPayment", NHibernateUtil.Decimal).
                AddScalar("POrderDate", NHibernateUtil.DateTime).
                AddScalar("PurposePayment", NHibernateUtil.String).
                AddScalar("ESSSNum", NHibernateUtil.String).
                AddScalar("StatusID", NHibernateUtil.Int32).
                AddScalar("StatusName", NHibernateUtil.String).
                AddScalar("GCID", NHibernateUtil.Int32).
                AddScalar("CTName", NHibernateUtil.String).
                AddScalar("DateP", NHibernateUtil.DateTime).
                AddScalar("DepLevel7Name", NHibernateUtil.String).
                AddScalar("GPDID", NHibernateUtil.String);
        }
        /// <summary>
        /// Список статусов актов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<GpdActStatusesDto> GetStatuses()
        {
            string sqlQuery = @"SELECT * FROM dbo.vwGpdStatus";

            IQuery query = CreateRefQuery(sqlQuery);
            IList<GpdActStatusesDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdActStatusesDto))).List<GpdActStatusesDto>();
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
        /// Составляем условие для запроса.
        /// </summary>
        /// <param name="DateBegin">Начало приода.</param>
        /// <param name="DateEnd">Конец периода</param>
        /// <param name="DepartmentId">ID подразделения.</param>
        /// <param name="Surname">ФИО физического лица.</param>
        /// <param name="StatusID">ID статуса акта.</param>
        /// <returns></returns>
        private string ActListSqlWhere(DateTime? DateBegin, DateTime? DateEnd, int DepartmentId, string Surname, int StatusID)
        {
            string SqlWhere = "";
            if (DateBegin.HasValue && DateEnd.HasValue)
                SqlWhere = " WHERE ActDate between '" + DateBegin.Value.ToString("d") + "' and '" + DateEnd.Value.ToString("d") + "'";

            if (DepartmentId != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  DepartmentId = " + DepartmentId.ToString() : " and DepartmentId = " + DepartmentId.ToString();

            if (Surname != null && Surname.Trim().Length != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  Surname like '" + Surname + "%'" : " and Surname like '" + Surname + "%'";

            if (StatusID != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  StatusID = " + StatusID.ToString() : " and StatusID = " + StatusID.ToString();

            return SqlWhere;
        }
        /// <summary>
        /// Составляем вид сортировки.
        /// </summary>
        /// <param name="SortBy">Переключатель для поля сортировки.</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        private string ActListSqlOrderBy(int SortBy, bool? SortDescending)
        {
            if (SortBy == 0) return "";

            string SqlOrderBy = " ORDER BY ";
            switch (SortBy)
            {
                case 1:
                    SqlOrderBy += "Id";
                    break;
                case 2:
                    SqlOrderBy += "Surname";
                    break;
                case 3:
                    SqlOrderBy += "CTName";
                    break;
                case 4:
                    SqlOrderBy += "NumContract";
                    break;
                case 5:
                    SqlOrderBy += "ContractBeginDate";
                    break;
                case 6:
                    SqlOrderBy += "ContractEndDate";
                    break;
                case 7:
                    SqlOrderBy += "DateP";
                    break;
                case 8:
                    SqlOrderBy += "ActNumber";
                    break;
                case 9:
                    SqlOrderBy += "ActDate";
                    break;
                case 10:
                    SqlOrderBy += "DepLevel3Name";
                    break;
                case 11:
                    SqlOrderBy += "DepLevel7Name";
                    break;
                case 12:
                    SqlOrderBy += "POrderDate";
                    break;
                case 13:
                    SqlOrderBy += "ChargingDate";
                    break;
                case 14:
                    SqlOrderBy += "DateBegin, DateEnd";
                    break;
                case 15:
                    SqlOrderBy += "AmountPayment";
                    break;
                case 16:
                    SqlOrderBy += "GPDID";
                    break;
                case 17:
                    SqlOrderBy += "ESSSNum";
                    break;
                case 18:
                    SqlOrderBy += "StatusName";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
    }
}
