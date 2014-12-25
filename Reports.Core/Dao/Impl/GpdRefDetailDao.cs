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
        /// Права.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<GpdPermissionDto> GetPermission(UserRole role)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[GpdPermission] WHERE RoleID = " + (int)role + " and MenuID = 1";
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
            string sqlQuery = @"SELECT * FROM [dbo].[GpdDetailType]" ;

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
        /// <param name="SortBy">Номер поля по которому будет проводится сортировка.</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        public IList<GpdRefDetailFullDto> GetRefDetail(UserRole role,
            int Id,
            string Name,
            int DTID,
            int SortBy,
            bool? SortDescending
            )
        {
            string sqlQuery = @"SELECT * FROM [dbo].[vwGpdRefDetailList] 
                                WHERE " + (Id == 0 ? ("DTID = " + DTID.ToString() + (Name == null || Name.Trim().Length == 0 ? "" : " and Name like '" + Name + "%'")) : "ID = " + Id.ToString());


            sqlQuery += RefDetailListSqlOrderBy(SortBy, SortDescending);

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
                AddScalar("Code", NHibernateUtil.String).
                AddScalar("CreatorID", NHibernateUtil.Int32).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("CreatePositionName", NHibernateUtil.String).
                AddScalar("CrDep7Level", NHibernateUtil.String).
                AddScalar("CrDep3Level", NHibernateUtil.String).
                AddScalar("EditorID", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("EditorName", NHibernateUtil.String).
                AddScalar("EditPositionName", NHibernateUtil.String).
                AddScalar("EDep7Level", NHibernateUtil.String).
                AddScalar("EDep3Level", NHibernateUtil.String);
        }
        /// <summary>
        /// Составляем вид сортировки.
        /// </summary>
        /// <param name="SortBy">Переключатель для поля сортировки.</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        private string RefDetailListSqlOrderBy(int SortBy, bool? SortDescending)
        {
            if (SortBy == 0) return "";

            string SqlOrderBy = " ORDER BY ";
            switch (SortBy)
            {
                case 1:
                    SqlOrderBy += "Id";
                    break;
                case 2:
                    SqlOrderBy += "Name";
                    break;
                case 3:
                    SqlOrderBy += "CreateDate";
                    break;
                case 4:
                    SqlOrderBy += "CreatorName";
                    break;
                case 5:
                    SqlOrderBy += "CreatePositionName";
                    break;
                case 6:
                    SqlOrderBy += "CrDep3Level";
                    break;
                case 7:
                    SqlOrderBy += "CrDep7Level";
                    break;
                case 8:
                    SqlOrderBy += "EditDate";
                    break;
                case 9:
                    SqlOrderBy += "EditorName";
                    break;
                case 10:
                    SqlOrderBy += "EditPositionName";
                    break;
                case 11:
                    SqlOrderBy += "EDep3Level";
                    break;
                case 12:
                    SqlOrderBy += "EDep7Level";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
    }
}
