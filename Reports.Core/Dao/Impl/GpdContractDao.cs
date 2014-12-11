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
        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }

        public GpdContractDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
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
            string sqlQuery = @"SELECT 0 as Id, 'Все' as Name
                                UNION ALL
                                SELECT Id as Id, Name as Name FROM dbo.GpdRefStatus";

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
            string sqlQuery = @"SELECT 0 as Id, 'Все' as Name
                                UNION ALL
                                SELECT Id as Id, Name as Name FROM dbo.GpdChargingType";

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
            string sqlQuery = @"SELECT Id as Id, LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS as Name FROM GpdRefPersons ORDER BY LastName + ' ' + FirstName + ' ' + SecondName + ' - ' + SNILS";

            IQuery query = CreateRefQuery(sqlQuery);
            IList<GpdContractSurnameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractSurnameDto))).List<GpdContractSurnameDto>();
            return documentList;
        }
        public IList<GpdContractDetailDto> GetDetails(UserRole role,
                                                    int DTID,
                                                    int Id,
                                                    string Name)
        {
            string sqlQuery = @"SELECT DTID as DTID, Id as Id, Name as Name FROM GpdRefDetail WHERE DTID = " + (DTID == 0 ? "" : DTID.ToString()) + " ORDER BY Name";

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
        /// <param name="IsDraft"></param>
        /// <param name="Surname"></param>
        /// <param name="IsFind"></param>
        /// <param name="SortBy"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        public IList<GpdContractDto> GetContracts(UserRole role,
                                            int Id,
                                            //int CreatorID,
                                            int DepartmentId,
                                            //string DepartmentName,
                                            //int PersonID,
                                            int CTID,
                                            //int StatusID,
                                            //string NumContract,
                                            //string NameContract,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            //DateTime? DateP,
                                            //DateTime? DatePOld,
                                            //int PayeeID,
                                            //int PayerID,
                                            //string GPDID,
                                            //string PurposePayment,
                                            bool IsDraft,
                                            //string CreatorName,
                                            //DateTime CreateDate,
                                            string Surname,
                                            //string CTName,
                                            //string StatusName,
                                            //string Autor,
                                            //string DepLevel3Name,
                                            //string DepLevel7Name,
                                            bool IsFind,
                                            int SortBy,
                                            bool? SortDescending)
        {
//            string sqlQuery = @"SELECT A.Id as Id, A.[Version] as [Version], A.CreatorID as CreatorID, A.DepartmentId as DepartmentId, G.[Name] as DepartmentName,
//			                              A.PersonID as PersonID, B.LastName + ' ' + B.FirstName + ' ' + B.SecondName as Surname, 
//			                              A.CTID as CTID, C.[Name] as CTName,
//			                              A.StatusID as StatusID, E.[Name] as StatusName, A.NumContract as NumContract, 
//			                              A.NameContract as NameContract, A.DateBegin as DateBegin, A.DateEnd as DateEnd, A.PayeeID as PayeeID, A.PayerID as PayerID, A.GPDID as GPDID, A.PurposePayment as PurposePayment, 
//				                            A.DateP as DateP, A.DateP as DatePOld,
//			                              A.IsDraft as IsDraft, F.[Name] as CreatorName, A.CreateDate, F.[Name] as Autor,
//				                            K.[Name] as DepLevel3Name, G.[Name] as DepLevel7Name 
//                            FROM dbo.GpdContract as A
//                            INNER JOIN dbo.GpdRefPersons as B ON B.Id = A.PersonID
//                            INNER JOIN dbo.GpdChargingType as C ON C.Id = A.CTID
//                            INNER JOIN dbo.GpdRefStatus as E ON E.Id = A.StatusID
//                            LEFT JOIN dbo.Users as F ON F.Id = A.CreatorID
//                            INNER JOIN dbo.Department as G ON G.Id = A.DepartmentId
//                            LEFT JOIN [dbo].[Department] as H ON H.Code = G.ParentId
//                            LEFT JOIN [dbo].[Department] as I ON I.Code = H.ParentId
//                            LEFT JOIN [dbo].[Department] as J ON J.Code = I.ParentId
//                            LEFT JOIN [dbo].[Department] as K ON K.Code = J.ParentId";
            string sqlQuery = @"SELECT  *
                               FROM [dbo].[vwGpdContractList]";

            string SqlWhere = "";

            if (!IsFind)
                SqlWhere = "A.Id = " + Id.ToString();
            else
            {
                SqlWhere = (DepartmentId != 0 ? " DepartmentId = " + DepartmentId.ToString() : "");//подразделение
                SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + (CTID != 0 ? " CTID = " + CTID.ToString() : "");//вид начисления
                SqlWhere = SqlWhere + (SqlWhere.Length != 0 ? " and " : "") + " DateBegin between '" + DateBegin.Value.ToString("d") + "' and '" + DateEnd.Value.ToString("d") + "'";//дата начала действия договора
                //DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)
                //@"v.[EditDate] >= :beginDate "
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
                    return " ORDER BY Id " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 2:
                    return " ORDER BY Surname " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 3:
                    return " ORDER BY CTName " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 4:
                    return " ORDER BY Id " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 5:
                    return " ORDER BY DateBegin " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 6:
                    return " ORDER BY DateEnd " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 7:
                    return " ORDER BY DateP " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 8:
                    return " ORDER BY DepLevel3Name " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 9:
                    return " ORDER BY DepLevel7Name " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
                case 10:
                    return " ORDER BY StatusName " + (SortDescending.HasValue && !SortDescending.Value ? " desc" : "");
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
                AddScalar("IsDraft", NHibernateUtil.Boolean).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("Surname", NHibernateUtil.String).
                AddScalar("Autor", NHibernateUtil.String).
                AddScalar("DepLevel3Name", NHibernateUtil.String).
                AddScalar("DepLevel7Name", NHibernateUtil.String);
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
        public virtual IQuery CreateDLQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).AddScalar("ItemLevel", NHibernateUtil.Int32);
        }
    }
}
