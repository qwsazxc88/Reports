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
            string sqlQuery = @"SELECT Id as Id, Name as Name FROM dbo.GpdChargingType";

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
        /// <param name="CreatorID"></param>
        /// <param name="DepartmentId"></param>
        /// <param name="PersonID"></param>
        /// <param name="CTID"></param>
        /// <param name="StatusID"></param>
        /// <param name="NumContract"></param>
        /// <param name="NameContract"></param>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="PayeeID"></param>
        /// <param name="PayerID"></param>
        /// <param name="GPDID"></param>
        /// <param name="PurposePayment"></param>
        /// <returns></returns>
        public IList<GpdContractDto> GetContracts(UserRole role,
                                            int Id,
                                            int CreatorID,
                                            int DepartmentId,
                                            int PersonID,
                                            int CTID,
                                            int StatusID,
                                            string NumContract,
                                            string NameContract,
                                            DateTime? DateBegin,
                                            DateTime? DateEnd,
                                            DateTime? DateP,
                                            DateTime? DatePOld,
                                            int PayeeID,
                                            int PayerID,
                                            string GPDID,
                                            string PurposePayment,
                                            bool flgDraft,
                                            string CreatorName,
                                            DateTime CreateDate,
                                            string Surname,
                                            string CTName,
                                            string StatusName,
                                            string Autor)
        {
            string sqlQuery = @"SELECT A.Id as Id, A.[Version] as [Version], A.CreatorID as CreatorID, A.DepartmentId as DepartmentId, 
			                             A.PersonID as PersonID, B.LastName + ' ' + B.FirstName + ' ' + B.SecondName as Surname, 
			                             A.CTID as CTID, C.[Name] as CTName,
			                             A.StatusID as StatusID, E.[Name] as StatusName, A.NumContract as NumContract, 
			                             A.NameContract as NameContract, A.DateBegin as DateBegin, A.DateEnd as DateEnd, A.PayeeID as PayeeID, A.PayerID as PayerID, A.GPDID as GPDID, A.PurposePayment as PurposePayment, 
																	 A.DateP as DateP, A.DateP as DatePOld,
			                             A.flgDraft as flgDraft, F.Name as CreatorName, A.CreateDate, F.Name as Autor
                            FROM dbo.GpdContract as A
                            INNER JOIN dbo.GpdRefPersons as B ON B.Id = A.PersonID
                            INNER JOIN dbo.GpdChargingType as C ON C.Id = A.CTID
                            INNER JOIN dbo.GpdRefStatus as E ON E.Id = A.StatusID
                            LEFT JOIN dbo.Users as F ON F.Id = A.CreatorID";
            sqlQuery = sqlQuery + " WHERE A.Id = " + Id.ToString();


            IQuery query = CreateContractQuery(sqlQuery);
            IList<GpdContractDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractDto))).List<GpdContractDto>();
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
                AddScalar("DepartmentId", NHibernateUtil.Int32).
                AddScalar("PersonID", NHibernateUtil.Int32).
                AddScalar("CTID", NHibernateUtil.Int32).
                AddScalar("StatusID", NHibernateUtil.Int32).
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
                AddScalar("flgDraft", NHibernateUtil.Boolean).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("Surname", NHibernateUtil.String).
                AddScalar("CTName", NHibernateUtil.String).
                AddScalar("StatusName", NHibernateUtil.String).
                AddScalar("Autor", NHibernateUtil.String);
        }
    }
}
