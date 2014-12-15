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
    /// Модуль справочника реквизитов для работы с базой данных
    /// </summary>
    public class GpdRefDetailDao : DefaultDao<GpdRefDetail>, IGpdRefDetailDao
    {
        public GpdRefDetailDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Список типов реквизитов.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IList<GpdRefDetailDto> GetDetailTypes(UserRole role,
                int Id,
                string Name)
        {
            string sqlQuery = @"SELECT Id as Id, Name as Name FROM dbo.GpdDetailType";

            IQuery query = CreateDTQuery(sqlQuery);
            IList<GpdRefDetailDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdRefDetailDto))).List<GpdRefDetailDto>();
            return documentList;
        }
        public virtual IQuery CreateDTQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
        }
        /// <summary>
        /// Список записей справочника.
        /// </summary>
        /// <param name="role">Группа пользователя.</param>
        /// <param name="Id">ID записи</param>
        /// <param name="Name">Наименование</param>
        /// <param name="DTID">ID типа реквизитов</param>
        /// <param name="INN">ИНН</param>
        /// <param name="KPP">КПП</param>
        /// <param name="Account">Расчетный счет</param>
        /// <param name="BankName">Банк</param>
        /// <param name="BankBIK">Банк БИК</param>
        /// <param name="CorrAccount">Банк кор/счет</param>
        /// <param name="CreatorID">ID Пользователя</param>
        /// <returns></returns>
        public IList<GpdRefDetailFullDto> GetRefDetail(UserRole role,
            int Id,
            string Name,
            int DTID,
            string INN,
            string KPP,
            string Account,
            string BankName,
            string BankBIK,
            string CorrAccount,
            int CreatorID,
            string Code)
        {
            string sqlQuery = @"SELECT Id as Id, Name as Name, DTID as DTID, INN as INN, KPP as KPP, Account as Account, BankName as BankName, BankBIK as BankBIK, CorrAccount as CorrAccount, CreatorID as CreatorID, Code as Code
                                FROM dbo.GpdRefDetail
                                WHERE " + (Id == 0 ? ("DTID = " + DTID.ToString() + (Name == null || Name.Trim().Length == 0 ? "" : " and Name like '" + Name + "%'")) : "ID = " + Id.ToString());

            IQuery query = CreateGRDQuery(sqlQuery);
            IList<GpdRefDetailFullDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdRefDetailFullDto))).List<GpdRefDetailFullDto>();
            return documentList;
        }
        public virtual IQuery CreateGRDQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("DTID", NHibernateUtil.Int32).
                AddScalar("INN", NHibernateUtil.String).
                AddScalar("KPP", NHibernateUtil.String).
                AddScalar("Account", NHibernateUtil.String).
                AddScalar("BankName", NHibernateUtil.String).
                AddScalar("BankBIK", NHibernateUtil.String).
                AddScalar("CorrAccount", NHibernateUtil.String).
                AddScalar("CreatorID", NHibernateUtil.Int32).
                AddScalar("Code", NHibernateUtil.String);
        }
    }
}
