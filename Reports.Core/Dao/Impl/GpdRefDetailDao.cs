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
        /// <param name="Id">ID реквизита</param>
        /// <param name="Name">Название реквизита</param>
        /// <param name="ContractorName">Название контрагента.</param>
        /// <param name="SortBy">Номер поля для сортировки</param>
        /// <param name="SortDescending">Направление сортировки</param>
        /// <returns></returns>
        public IList<GpdDetailDto> GetRefDetail(UserRole role, int Id, string Name, string ContractorName, int SortBy, bool? SortDescending)
        {
            string sqlWhere = "";
            string sqlQuery = @"SELECT * FROM [dbo].[vwGpdDetailList] ";

            if (Id != 0)
                sqlWhere = "ID = " + Id.ToString();
            if (Name != null && Name.Trim().Length != 0)
                sqlWhere += (sqlWhere.Length != 0 ? " and " : "") + "Name like '" + Name + "%'";
            if (ContractorName != null && ContractorName.Trim().Length != 0)
                sqlWhere += (sqlWhere.Length != 0 ? " and " : "") + "ContractorName like '" + ContractorName + "%'";

            sqlQuery += (sqlWhere.Length != 0 ? " WHERE " + sqlWhere : "");
            sqlQuery += DetailOrderBy(SortBy, SortDescending);

            IQuery query = CreateGRDQuery(sqlQuery);
            IList<GpdDetailDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdDetailDto))).List<GpdDetailDto>();
            return documentList;
        }
        public virtual IQuery CreateGRDQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("ContractorName", NHibernateUtil.String).
                //AddScalar("DTID", NHibernateUtil.Int32).
                AddScalar("INN", NHibernateUtil.String).
                AddScalar("KPP", NHibernateUtil.String).
                AddScalar("Account", NHibernateUtil.String).
                AddScalar("BankName", NHibernateUtil.String).
                AddScalar("BankBIK", NHibernateUtil.String).
                AddScalar("CorrAccount", NHibernateUtil.String).
                AddScalar("PersonAccount", NHibernateUtil.String);
        }
        /// <summary>
        /// Составляем вид сортировки.
        /// </summary>
        /// <param name="SortBy">Переключатель для поля сортировки.</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        private string DetailOrderBy(int SortBy, bool? SortDescending)
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
                    SqlOrderBy += "ContractorName";
                    break;
                case 4:
                    SqlOrderBy += "INN";
                    break;
                case 5:
                    SqlOrderBy += "KPP";
                    break;
                case 6:
                    SqlOrderBy += "Account";
                    break;
                case 7:
                    SqlOrderBy += "PersonAccount";
                    break;
                case 8:
                    SqlOrderBy += "BankName";
                    break;
                case 9:
                    SqlOrderBy += "BankBIK";
                    break;
                case 10:
                    SqlOrderBy += "CorrAccount";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
        /// <summary>
        /// Запрос для наборов реквизитов.
        /// </summary>
        /// <param name="ID">ID набора.</param>
        /// <param name="Name">Название набора реквизитов</param>
        /// <param name="Surname">ФИО физического лица</param>
        /// <param name="PayerName">Плательщик</param>
        /// <param name="PayeeName">Получатель</param>
        /// <param name="flgView">Признак просмотра списка наборов</param>
        /// <param name="SortBy">Номер поля для сортировки</param>
        /// <param name="SortDescending">Направление сортировки</param>
        /// <returns></returns>
        public IList<GpdDetailSetsListDto> GetDetailSetList(int ID,
            string Name,
            string Surname,
            string PayerName,
            string PayeeName,
            bool flgView,
            int SortBy,
            bool? SortDescending)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[vwGpdDetailSetList]";



            sqlQuery += DetailSetWhere(ID, Name, Surname, PayerName, PayeeName, flgView) + DetailSetOrderBy(SortBy, SortDescending);

            IQuery query = CreateSQLDSQuery(sqlQuery);
            IList<GpdDetailSetsListDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdDetailSetsListDto))).List<GpdDetailSetsListDto>();
            return documentList;
        }
        public virtual IQuery CreateSQLDSQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Surname", NHibernateUtil.String).
                AddScalar("PayerName", NHibernateUtil.String).
                AddScalar("PayeeName", NHibernateUtil.String).
                AddScalar("Account", NHibernateUtil.String).
                AddScalar("CreatorID", NHibernateUtil.Int32).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("CreatorName", NHibernateUtil.String).
                AddScalar("EditorID", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("EditorName", NHibernateUtil.String).
                AddScalar("PersonID", NHibernateUtil.Int32).
                AddScalar("PayerID", NHibernateUtil.Int32).
                AddScalar("PayeeID", NHibernateUtil.Int32).
                AddScalar("AllowEdit", NHibernateUtil.Boolean);
        }
        /// <summary>
        /// Составляем условие запроса.
        /// </summary>
        /// <param name="ID">ID набора.</param>
        /// <param name="Name">Название набора реквизитов</param>
        /// <param name="Surname">ФИО физического лица</param>
        /// <param name="PayerName">Плательщик</param>
        /// <param name="PayeeName">Получатель</param>
        /// <param name="flgView">Признак просмотра списка</param>
        /// <returns></returns>
        private string DetailSetWhere(int ID,
            string Name,
            string Surname,
            string PayerName,
            string PayeeName,
            bool flgView)
        {
            string SqlWehere = "";
            if (!flgView)
                SqlWehere += " ID = " + ID.ToString();

            if (Name != null && Name.Trim().Length != 0)
                SqlWehere += " Name like '" + Name + "%'";

            if (Surname != null && Surname.Trim().Length != 0)
                SqlWehere += (SqlWehere.Length != 0 ? " and " : "") + " Surname like '" + Surname + "%'";

            if (PayerName != null && PayerName.Trim().Length != 0)
                SqlWehere += (SqlWehere.Length != 0 ? " and " : "") + " PayerName like '" + PayerName + "%'";

            if (PayeeName != null && PayeeName.Trim().Length != 0)
                SqlWehere += (SqlWehere.Length != 0 ? " and " : "") + " PayeeName like '" + PayeeName + "%'";

            return SqlWehere.Trim().Length != 0 ? " WHERE " + SqlWehere : "";
        }
        /// <summary>
        /// Составляем вид сортировки.
        /// </summary>
        /// <param name="SortBy">Переключатель для поля сортировки.</param>
        /// <param name="SortDescending">Направление сортировки.</param>
        /// <returns></returns>
        private string DetailSetOrderBy(int SortBy, bool? SortDescending)
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
                    SqlOrderBy += "Surname";
                    break;
                case 4:
                    SqlOrderBy += "PayerName";
                    break;
                case 5:
                    SqlOrderBy += "PayeeName";
                    break;
                case 6:
                    SqlOrderBy += "Account";
                    break;
                case 7:
                    SqlOrderBy += "CreateDate";
                    break;
                case 8:
                    SqlOrderBy += "CreatorName";
                    break;
                case 9:
                    SqlOrderBy += "EditDate";
                    break;
                case 10:
                    SqlOrderBy += "EditorName";
                    break;
            }
            return SqlOrderBy += (SortDescending.HasValue && !SortDescending.Value ? "" : " desc");
        }
        /// <summary>
        /// Запрос для физических лиц.
        /// </summary>
        /// <returns></returns>
        public IList<GpdContractSurnameDto> GetPersons(int Id)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[vwGpdRefPersons] ";
            sqlQuery += (Id != 0 ? " WHERE Id = " + Id.ToString() : "") + " ORDER BY Name";

            IQuery query = CreateSQLPersonQuery(sqlQuery);
            IList<GpdContractSurnameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractSurnameDto))).List<GpdContractSurnameDto>();
            return documentList;
        }
        public virtual IQuery CreateSQLPersonQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("SNILS", NHibernateUtil.String).
                AddScalar("LongName", NHibernateUtil.String);
        }

        public IList<GpdContractSurnameDto> GetAutocompletePersons(string Name, int PersonID)
        {

            string sqlQuery = @"SELECT * FROM [dbo].[vwGpdRefPersons] ";
            //sqlQuery += (Id != 0 ? " WHERE Id = " + Id.ToString() : "") + " ORDER BY Name";

            if (PersonID == 0)
                sqlQuery = @"SELECT * FROM vwGpdRefPersons WHERE LongName like '" + (Name == null ? "" : Name) + "%' ORDER BY LongName";
            else
                sqlQuery = @"SELECT * FROM vwGpdRefPersons WHERE ID =" + PersonID.ToString();

            IQuery query = CreateSQLPersonQuery(sqlQuery);
            IList<GpdContractSurnameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(GpdContractSurnameDto))).List<GpdContractSurnameDto>();
            return documentList;
        }
    }
}
