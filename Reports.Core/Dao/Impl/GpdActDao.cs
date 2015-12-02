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
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<GpdPermissionDto> GetPermission(UserRole role)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[GpdPermission] WHERE RoleID = " + (int)role + " and MenuID = 3";
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
        /// Запрос для создания нового акта ГПД.
        /// </summary>
        /// <param name="role">Текущая роль пользователя.</param>
        /// <param name="GCID">ID договора.</param>
        /// <returns></returns>
        public IList<GpdActDto> GetNewAct(UserRole role, int GCID)
        {
            string sqlQuery = @"SELECT  *  FROM [dbo].[vwGpdActNew] WHERE GCID = " + GCID.ToString();

            IQuery query = CreateActQuery(sqlQuery);
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
        /// <param name="ActNumber">№ акта.</param>
        /// <param name="SortBy">Переменная определяет поле сортировки.</param>
        /// <param name="SortDescending">Признак направления сортировки.</param>
        /// <returns></returns>
        public IList<GpdActDto> GetAct(UserRole role, 
                                        int userId,
                                        int? ID, 
                                        bool IsFind,
                                        DateTime? DateBegin,
                                        DateTime? DateEnd, 
                                        int DepartmentId,
                                        int CTType,
                                        string Surname, 
                                        int StatusID,
                                        int? ChargingYear,
                                        int ChargingMonth,
                                        string ActNumber,
                                        string CardNumber,
                                        int SortBy, 
                                        bool? SortDescending)
        {
            string sqlQuery = @"SELECT  A.[Id]
              ,A.[CreatorID]
              ,A.[ActDate]
              ,A.[ActNumber]
              ,A.[GCCount]
              ,A.[PersonID]
              ,A.[Surname]
              ,A.[NameContract]
              ,A.[NumContract]
              ,A.[ContractBeginDate]
              ,A.[ContractEndDate]
              ,A.[CreatorName]
              ,A.[CreateDate]
              ,A.[DepLevel3Name]
              ,A.[ChargingDate]
              ,A.[DateBegin]
              ,A.[DateEnd]
              ,A.[Amount]
              ,A.[AmountPayment]
              ,A.[POrderDate]
              ,A.[PurposePayment]
              ,A.[ESSSNum]
              ,A.[StatusID]
              ,A.[StatusName]
              ,A.[SendTo1C]
              ,A.[GCID]
              ,A.[flgRed]
              ,A.[CTName]
              ,A.[CTType]
              ,A.[DateP]
              ,A.[DepLevel7Name]
              ,A.[GPDID]
              ,A.[DepartmentId]
              ,A.[PayerID]
              ,A.[PayerName]
              ,A.[PayerINN]
              ,A.[PayerKPP]
              ,A.[PayerAccount]
              ,A.[PayerBankName]
              ,A.[PayerBankBIK]
              ,A.[PayerCorrAccount]
              ,A.[PayeeID]
              ,A.[PayeeName]
              ,A.[PayeeINN]
              ,A.[PayeeKPP]
              ,A.[PayeeAccount]
              ,A.[PayeeBankName]
              ,A.[PayeeBankBIK]
              ,A.[PayeeCorrAccount]
              ,A.[PAccountID]
              ,A.[Account]  
              ,A.[PAName]
            FROM [dbo].[vwGpdActList] as A ";
            string sqlWhere=""; 
            if (!IsFind)
                sqlWhere += "WHERE A.Id = " + ID.ToString();
            else
                sqlWhere += ActListSqlWhere(DateBegin, DateEnd, DepartmentId, Surname, StatusID, ActNumber, ID) + ActListSqlOrderBy(SortBy, SortDescending);
            
            string forUserRole = GetWhereForUserRole(role,ref sqlQuery);
            if (!String.IsNullOrWhiteSpace(forUserRole))
            {
                if (String.IsNullOrWhiteSpace(sqlWhere)) sqlWhere += "WHERE " + forUserRole; else sqlWhere += " AND " + forUserRole;
            }
            if (!String.IsNullOrWhiteSpace(CardNumber))
            {
                if (String.IsNullOrWhiteSpace(sqlWhere)) sqlWhere += "WHERE A.PersonId= " + CardNumber+" "; else sqlWhere += " AND A.PersonId=" + CardNumber+" ";
            }
            if (ChargingMonth > 0 && ChargingMonth<13)
            {
                if (String.IsNullOrWhiteSpace(sqlWhere)) sqlWhere += "WHERE MONTH(A.ChargingDate)= " + ChargingMonth + " "; else sqlWhere += " AND MONTH(A.ChargingDate)=" + ChargingMonth + " ";
            }
            if (ChargingYear.HasValue)
            {
                if (String.IsNullOrWhiteSpace(sqlWhere)) sqlWhere += "WHERE YEAR(A.ChargingDate)= " + ChargingYear + " "; else sqlWhere += " AND YEAR(A.ChargingDate)=" + ChargingYear + " ";
            }
            if(CTType>0)
            {
                if (String.IsNullOrWhiteSpace(sqlWhere)) sqlWhere += "WHERE A.CTType=" + CTType + " "; else sqlWhere += " AND A.CTType=" + CTType+" ";
            }
            sqlQuery += sqlWhere;
            IQuery query = CreateActQuery(sqlQuery);
            if(sqlQuery.Contains(":userId")) query.SetInt32("userId", userId);
            IList<GpdActDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdActDto))).List<GpdActDto>();
            return documentList;
        }
        private string GetWhereForUserRole(UserRole role, ref string sqlQuery)
        {
            string where = "";
            switch (role)
            {
                case UserRole.Chief:
                case UserRole.Manager:
                    sqlQuery =sqlQuery+@" INNER JOIN Department dep on A.DepartmentId=dep.Id
	                            INNER JOIN Users currentUser on currentUser.Id=:userId
	                            INNER JOIN Department userDep on currentUser.DepartmentId=userDep.Id
                                ";
                    where= " dep.Path like userDep.Path+'%' ";
                    break;
            }
            return where;
        }
        /// <summary>
        /// Создание запроса для актов.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateActQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("CreatorID", NHibernateUtil.Int32).
                AddScalar("ActDate", NHibernateUtil.DateTime).
                AddScalar("ActNumber", NHibernateUtil.String).
                AddScalar("GCCount", NHibernateUtil.Int32).
                AddScalar("PersonId",NHibernateUtil.Int32).
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
                AddScalar("SendTo1C",NHibernateUtil.DateTime).
                AddScalar("GCID", NHibernateUtil.Int32).
                AddScalar("CTName", NHibernateUtil.String).
                AddScalar("DateP", NHibernateUtil.DateTime).
                AddScalar("DepLevel7Name", NHibernateUtil.String).
                AddScalar("GPDID", NHibernateUtil.String).
                //реквизиты плательщика
                AddScalar("PayerID", NHibernateUtil.Int32).
                AddScalar("PayerName", NHibernateUtil.String).
                AddScalar("PayerINN", NHibernateUtil.String).
                AddScalar("PayerKPP", NHibernateUtil.String).
                AddScalar("PayerAccount", NHibernateUtil.String).
                AddScalar("PayerBankName", NHibernateUtil.String).
                AddScalar("PayerBankBIK", NHibernateUtil.String).
                AddScalar("PayerCorrAccount", NHibernateUtil.String).
                //реквизиты получателя
                AddScalar("PayeeID", NHibernateUtil.Int32).
                AddScalar("PayeeName", NHibernateUtil.String).
                AddScalar("PayeeINN", NHibernateUtil.String).
                AddScalar("PayeeKPP", NHibernateUtil.String).
                AddScalar("PayeeAccount", NHibernateUtil.String).
                AddScalar("PayeeBankName", NHibernateUtil.String).
                AddScalar("PayeeBankBIK", NHibernateUtil.String).
                AddScalar("PayeeCorrAccount", NHibernateUtil.String).
                AddScalar("PAccountID", NHibernateUtil.Int32).
                AddScalar("Account", NHibernateUtil.String).
                AddScalar("PAName", NHibernateUtil.String).
                AddScalar("flgRed", NHibernateUtil.Boolean).
                AddScalar("CTType",NHibernateUtil.Int32);
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
        /// <param name="ActNumber">№ акта</param>
        /// <param name="id">ID акта</param>
        /// <returns></returns>
        private string ActListSqlWhere(DateTime? DateBegin, DateTime? DateEnd, int DepartmentId, string Surname, int StatusID, string ActNumber, int? ID)
        {
            string SqlWhere = "";
            if (DateBegin.HasValue && DateEnd.HasValue)
                SqlWhere = " WHERE ActDate between '" + DateBegin.Value.ToString("d") + "' and '" + DateEnd.Value.ToString("d") + "'";

            //if (DepartmentId != 0)
            //    SqlWhere += SqlWhere.Length == 0 ? "  WHERE  DepartmentId = " + DepartmentId.ToString() : " and DepartmentId = " + DepartmentId.ToString();
            if (DepartmentId != 0)
                SqlWhere += SqlWhere.Length == 0 ? @"  WHERE exists (select d1.ID from dbo.Department d
				                                                     inner join dbo.Department d1 on d1.Path like d.Path +'%' and A.DepartmentID = d1.ID and d.Id = " + DepartmentId.ToString() + ")" :
                                                   @"  and exists (select d1.ID from dbo.Department d
				                                                   inner join dbo.Department d1 on d1.Path like d.Path +'%' and A.DepartmentID = d1.ID and d.Id = " + DepartmentId.ToString() + ")";

            if (Surname != null && Surname.Trim().Length != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  Surname like '" + Surname + "%'" : " and Surname like '" + Surname + "%'";

            if (StatusID != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  StatusID = " + StatusID.ToString() : " and StatusID = " + StatusID.ToString();

            if (ActNumber != null && ActNumber.Trim().Length != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  ActNumber = '" + ActNumber + "'" : " and ActNumber = '" + ActNumber + "'";

            if (ID != null && ID != 0)
                SqlWhere += SqlWhere.Length == 0 ? "  WHERE  ID = " + ID.ToString() + "" : " and ID = " + ID.ToString() + "";
           
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
            if (SortBy == 0 || SortBy == 21|| SortBy==23) return "";

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
                case 19:
                    SqlOrderBy += "Amount";
                    break;
                case 20:
                    SqlOrderBy += "CreateDate";
                    break;
                case 21:
                    SqlOrderBy += "PayeeName"; 
                    break;
                case 22:
                    SqlOrderBy += "PAName";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
        /// <summary>
        /// Запрос для комментариев к акту.
        /// </summary>
        /// <param name="Id">Id акта ГПД.</param>
        /// <returns></returns>
        public IList<GpdActCommentDto> GetComments(int Id)
        {
            string sqlQuery = @"SELECT  *  FROM [dbo].[vwGpdActComments] WHERE ActId = " + Id.ToString() + " ORDER BY CreateDate desc";
            IQuery query = CreateActCommentQuery(sqlQuery);
            IList<GpdActCommentDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdActCommentDto))).List<GpdActCommentDto>();
            return documentList;
        }
        /// <summary>
        /// Создание запроса для комментариев к акту.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateActCommentQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("ActId", NHibernateUtil.Int32).
                AddScalar("Comment", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("Creator", NHibernateUtil.String);
        }
        /// <summary>
        /// Список физических лиц с набором реквизитов.
        /// </summary>
        /// <param name="Name">ФИО физ лица</param>
        /// <returns></returns>
        public IList<GpdContractSurnameDto> GetAutocompletePersonDS(string Name, int DSID)
        {
            string sqlQuery = "";
            if (DSID == 0)
                sqlQuery = @"SELECT * FROM vwGpdDetailSetList WHERE Name like '" + (Name == null ? "" : Name) + "%' ORDER BY LongName";
            else
                sqlQuery = @"SELECT * FROM vwGpdDetailSetList WHERE ID =" + DSID.ToString();

            IQuery query = CreatePersonQuery(sqlQuery);
            IList<GpdContractSurnameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractSurnameDto))).List<GpdContractSurnameDto>();
            return documentList;
        }
        /// <summary>
        /// Создание запроса физических лиц.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreatePersonQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("PersonID", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("SNILS", NHibernateUtil.String).
                AddScalar("LongName", NHibernateUtil.String).
                //реквизиты плательщика
                AddScalar("PayerName", NHibernateUtil.String).
                AddScalar("PayerINN", NHibernateUtil.String).
                AddScalar("PayerKPP", NHibernateUtil.String).
                AddScalar("PayerAccount", NHibernateUtil.String).
                AddScalar("PayerBankName", NHibernateUtil.String).
                AddScalar("PayerBankBIK", NHibernateUtil.String).
                AddScalar("PayerCorrAccount", NHibernateUtil.String).
                //реквизиты получателя
                AddScalar("PayeeName", NHibernateUtil.String).
                AddScalar("PayeeINN", NHibernateUtil.String).
                AddScalar("PayeeKPP", NHibernateUtil.String).
                AddScalar("PayeeAccount", NHibernateUtil.String).
                AddScalar("PayeeBankName", NHibernateUtil.String).
                AddScalar("PayeeBankBIK", NHibernateUtil.String).
                AddScalar("PayeeCorrAccount", NHibernateUtil.String).

                AddScalar("Account", NHibernateUtil.String);
        }
        /// <summary>
        /// Проверка на наличие занесенных актов для договора с повторяющимися номерами.
        /// </summary>
        /// <param name="ID">ID акта</param>
        /// <param name="GCID"> договора</param>
        /// <param name="ActNumber">Номер акта</param>
        /// <returns></returns>
        public bool ExistsActsByNumber(int ID, int GCID, string ActNumber)
        {
            string SqlQuery = "";
            if (ID == 0)
                SqlQuery = "SELECT cast(case when count(*) > 0 then 1 else 0 end as bit) flgExists FROM dbo.GpdAct WHERE GCID = " + GCID.ToString() + " and ActNumber = '" + ActNumber + "'";
            else
                SqlQuery = "SELECT cast(case when count(*) > 0 then 1 else 0 end as bit) flgExists FROM dbo.GpdAct WHERE GCID = " + GCID.ToString() + " and ActNumber = '" + ActNumber + "' and ID <> " + ID.ToString();

            IQuery query = CreateEAQuery(SqlQuery);
            return query.UniqueResult<bool>();
        }
        /// <summary>
        /// Проверка на статус договора.
        /// </summary>
        /// <param name="GCID"> договора</param>
        /// <returns></returns>
        public bool CheckContractEntry(int GCID)
        {
            string SqlQuery = "SELECT cast(case when StatusID <> 4 then 1 else 0 end as bit) flgExists FROM dbo.GpdContract WHERE ID = " + GCID.ToString();

            IQuery query = CreateEAQuery(SqlQuery);
            return query.UniqueResult<bool>();
        }
        /// <summary>
        /// Создание подзапроса.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateEAQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("flgExists", NHibernateUtil.Boolean);
        }
        
    }
}
