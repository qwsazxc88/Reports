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
        /// Список сроков оплаты.
        /// </summary>
        /// <returns></returns>
        public IList<GpdContractStatusesDto> GetPaymentPeriods()
        {
            string sqlQuery = @"SELECT * FROM dbo.vwGpdPaymentPeriod";

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
            string sqlQuery = @"SELECT * FROM vwGpdRefPersons WHERE Id = " + Id.ToString() + " ORDER BY Name";

            IQuery query = CreatePersonQuery(sqlQuery);
            IList<GpdContractSurnameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractSurnameDto))).List<GpdContractSurnameDto>();
            return documentList;
        }
        /// <summary>
        /// Список физических лиц с набором реквизитов.
        /// </summary>
        /// <param name="Name">ФИО физ лица</param>
        /// <param name="PersonID">ID физ лица</param>
        /// <returns></returns>
        public IList<GpdContractSurnameDto> GetAutocompletePersons(string Name, int PersonID)
        {
            string sqlQuery = "";
            if (PersonID == 0)
                sqlQuery = @"SELECT * FROM vwGpdDetailSetList WHERE LongName like '" + (Name == null ? "" : Name) + "%' ORDER BY LongName";
            else
                sqlQuery = @"SELECT * FROM vwGpdDetailSetList WHERE PersonID =" + PersonID.ToString();

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
        /// Запрос для реквизитов получателей и плательщиков
        /// </summary>
        /// <param name="Name">По рабочему названию</param>
        /// <param name="ID">По ID</param>
        /// <returns></returns>
        public IList<GpdContractDetailDto> GetAutocompleteDetails(string Name, int ID)
        {
            string sqlQuery = "";
            if (ID == 0)
                sqlQuery = @"SELECT * FROM vwGpdDetailList WHERE LongName like '" + (Name == null ? "" : Name) + "%' ORDER BY LongName";
            else
                sqlQuery = @"SELECT * FROM vwGpdDetailList WHERE ID =" + ID.ToString();

            IQuery query = CreateDetailsQuery(sqlQuery);
            IList<GpdContractDetailDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractDetailDto))).List<GpdContractDetailDto>();
            return documentList;
        }
        /// <summary>
        /// Создание запроса физических лиц.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateDetailsQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("LongName", NHibernateUtil.String).
                //реквизиты 
                AddScalar("ContractorName", NHibernateUtil.String).
                AddScalar("INN", NHibernateUtil.String).
                AddScalar("KPP", NHibernateUtil.String).
                AddScalar("Account", NHibernateUtil.String).
                AddScalar("PersonAccount", NHibernateUtil.String).
                AddScalar("BankName", NHibernateUtil.String).
                AddScalar("BankBIK", NHibernateUtil.String).
                AddScalar("CorrAccount", NHibernateUtil.String);
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
                                            int? Id,
                                            int DepartmentId,
                                            int CTID,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            string Surname,
                                            string NumContract,
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
                SqlWhere = SqlWhere + (SqlWhere.Length != 0 && CTID != 0 ? " and " : "") + (CTID != 0 ? " CTID = " + CTID.ToString() : "");//вид начисления
                if (DateBegin.HasValue && DateEnd.HasValue)
                    SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + " DateBegin between '" + DateBegin.Value.ToString("d") + "' and '" + DateEnd.Value.ToString("d") + "'";//дата начала действия договора
                
                if (Surname != null)
                    SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + (Surname.Trim().Length != 0 ? " Surname like '" + Surname + "%'" : "");//по фио
                if (NumContract != null)
                    SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + (NumContract.Trim().Length != 0 ? " NumContract = '" + NumContract + "'" : "");//по номеру договора
                if (Id != null && Id != 0)
                    SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + " id = " + Id.ToString();//по номеру договора
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
                case 11:
                    return " ORDER BY PayerName " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
                case 12:
                    return " ORDER BY PayeeName " + (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
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
                AddScalar("GPDID", NHibernateUtil.String).
                AddScalar("GPDContractID", NHibernateUtil.String).
                AddScalar("PurposePayment", NHibernateUtil.String).
                AddScalar("PurposePaymentPart", NHibernateUtil.String).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("Surname", NHibernateUtil.String).
                AddScalar("Autor", NHibernateUtil.String).
                AddScalar("DepLevel3Name", NHibernateUtil.String).
                AddScalar("DepLevel7Name", NHibernateUtil.String).
                AddScalar("IsLong", NHibernateUtil.Boolean).
                //реквизиты плательщика
                AddScalar("PayerID", NHibernateUtil.Int32).
                AddScalar("PayerName", NHibernateUtil.String).
                AddScalar("PayerINN", NHibernateUtil.String).
                AddScalar("PayerKPP", NHibernateUtil.String).
                AddScalar("PayerAccount", NHibernateUtil.String).
                AddScalar("PayerBankName", NHibernateUtil.String).
                AddScalar("PayerBankBIK", NHibernateUtil.String).
                AddScalar("PayerCorrAccount", NHibernateUtil.String).
                AddScalar("PayerContractor", NHibernateUtil.String).
                //реквизиты получателя
                AddScalar("PayeeID", NHibernateUtil.Int32).
                AddScalar("PayeeName", NHibernateUtil.String).
                AddScalar("PayeeINN", NHibernateUtil.String).
                AddScalar("PayeeKPP", NHibernateUtil.String).
                AddScalar("PayeeAccount", NHibernateUtil.String).
                AddScalar("PayeeBankName", NHibernateUtil.String).
                AddScalar("PayeeBankBIK", NHibernateUtil.String).
                AddScalar("PayeeCorrAccount", NHibernateUtil.String).
                AddScalar("PayeeContractor", NHibernateUtil.String).
                //лицевой счет
                AddScalar("PAccountID", NHibernateUtil.Int32).
                AddScalar("PersonAccount", NHibernateUtil.String).
                AddScalar("Account", NHibernateUtil.String).

                AddScalar("PaymentPeriodID", NHibernateUtil.Int32).
                AddScalar("Amount", NHibernateUtil.Decimal);
                //AddScalar("DSID", NHibernateUtil.Int32);
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
        /// <summary>
        /// Проверка на наличие занесенных актов для договора.
        /// </summary>
        /// <param name="ID">ID договора</param>
        /// <returns></returns>
        public bool ExistsReadyActs(int ID)
        {
            IQuery query = CreateERQuery("SELECT cast(case when count(*) > 0 then 1 else 0 end as bit) flgExists FROM dbo.GpdAct WHERE StatusID <> 4 and GCID = " + ID.ToString());
            return query.UniqueResult<bool>();
        }
        /// <summary>
        /// Создание подзапроса.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreateERQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("flgExists", NHibernateUtil.Boolean);
        }
        /// <summary>
        /// Проверка на наличие занесенных актов для договора.
        /// </summary>
        /// <param name="PersonID">ID физ. лица.</param>
        /// <returns></returns>
        public string GetPersonName(int PersonID)
        {
            IQuery query = CreatePNQuery("SELECT isnull(LastName, '') + ' ' + isnull(FirstName, '') + ' ' + isnull(SecondName, '') as PersonName FROM dbo.RefPeople WHERE ID = " + PersonID.ToString());
            return query.UniqueResult<string>();
        }
        /// <summary>
        /// Создание подзапроса.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public virtual IQuery CreatePNQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("PersonName", NHibernateUtil.String);
        }
    }
}
