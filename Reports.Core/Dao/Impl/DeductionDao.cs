using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using NHibernate.Criterion;
using NHibernate.Linq;
namespace Reports.Core.Dao.Impl
{
    public class DeductionDao : DefaultDao<Deduction>, IDeductionDao
    {
        protected const string sqlSelectForDeductionRn = @";with res as
                                ({0})
                                select {1} as Rn,* from res order by Rn";
        protected const string sqlSelectForDeduction =
                               @"select 
                                v.Id as Id,
                                u.Id as UserId,
                                v.Number as Number,
                                v.EditDate,
                                dep3.Name as Dep3Name,
                                v.Sum,
                                v.DeductionDate,
                                u.Name as UserName,
                                p.Name as Position,
                                k.Name as Kind,
                                v.NotUseInAnalyticalStatement as NotUseInAnalyticalStatement,
                                v.UploadingDocType as UploadingDocType,
                                dep.Name as  Dep7Name,
                                case 
                                    when mrd.id is not null then mrd.Number                                
                                    else mr.Number 
                                end as MissionReportNumber,
                                                        
                                v.DismissalDate,
                                case when v.DeleteDate is not null then N'Отклонена'
                                     when v.SendTo1C is not null then N'Выгружена в 1С' 
                                     when v.UploadingDocType is not null and v.UploadingDocType!=4 and v.SendTo1C is null and v.DeleteDate is null then N'Автовыгрузка'
                                     when v.ManualDeductionId is not null then N'Автоудержание'
                                     when v.UploadingDocType = 4 and v.SendTo1C is null and v.DeleteDate is null then N'Загруженно из файла'
                                     else N'Записана'
                                end as Status,
                                case when IsFastDismissal is null then null  
                                     when IsFastDismissal = 0 then N'Нет'
                                     else N'Да'
                                end as IsFastDismissal
                                from dbo.Deduction v
                                -- inner join dbo.DeductionType t on v.TypeId = t.Id
                                left join dbo.MissionReport mr on v.id=mr.DeductionId
                                inner join dbo.DeductionKind k on v.KindId = k.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                left join [dbo].ManualDeduction MD On v.ManualDeductionId=MD.id
                                left join [dbo].MissionReport MRD on MD.MissionReportId=MRD.id
                                left join dbo.Position p on p.Id = u.PositionId
                                left join dbo.Department dep on u.DepartmentId = dep.Id
                                left join dbo.Department dep3 on dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                                ";
        public DeductionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public Deduction GetDeductionByNumber(int number)
        {
            var d=Session.Query<Deduction>().Where(x => x.Number == number);
            if (d != null && d.Any()) return d.First();
            else return null;
        }
        public virtual IList<DeductionDto> GetDocuments(
                                int userId,
                                UserRole role,
                                int departmentId,
                                //int positionId,
                                int typeId,
                                int statusId,
                                DateTime? beginDate,
                                DateTime? endDate,
                                string userName,
                                //string sqlQuery,
                                int sortedBy,
                                bool? sortDescending,
                                string Number
            )
        {
            string whereString = GetWhereForUserRole(role, userId);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetDocumentNumberWhere(whereString, Number);
            string sqlQuery = GetSqlQueryOrdered(sqlSelectForDeduction, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DeductionDto))).List<DeductionDto>();
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
        //    public int Id { get; set; }
        //public int UserId { get; set; }
        //public string Number { get; set; }
        //public DateTime EditDate { get; set; }
        //public string Dep3Name { get; set; }
        //public decimal Sum { get; set; }
        //public DateTime? DeductionDate { get; set; }
        //public string UserName { get; set; }
        //public string Position { get; set; }
        //public string Kind { get; set; }
        //public string Dep7Name { get; set; }
        //public string Status { get; set; }
        //public string IsFastDismissal { get; set; }
        //public int Rn { get; set; }
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Rn", NHibernateUtil.Int32).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Number", NHibernateUtil.String).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("Dep3Name", NHibernateUtil.String).
                AddScalar("Sum", NHibernateUtil.Decimal).
                AddScalar("DeductionDate", NHibernateUtil.DateTime).
                //AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Position", NHibernateUtil.String).
                AddScalar("Kind", NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                AddScalar("DismissalDate", NHibernateUtil.DateTime).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("IsFastDismissal", NHibernateUtil.String).
                AddScalar("UploadingDocType",NHibernateUtil.Int32).
                AddScalar("MissionReportNumber",NHibernateUtil.Int32).
                AddScalar("NotUseInAnalyticalStatement",NHibernateUtil.Boolean);
        }
        public override string GetDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EditDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EditDate] < :endDate ";
            }
            return whereString;
        }
        public override string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1://1, "Записана"
                        statusWhere = @"v.SendTo1C is null and v.DeleteDate is null";
                        break;
                    case 2://2, "Выгружена в 1С"
                        statusWhere = @"v.SendTo1C is not null and v.DeleteDate is null";
                        break;
                    case 3://3, "Отклонена"
                        statusWhere = @"v.[DeleteDate] is not null";
                        break;
                    case 4: //4, "Автовыгрузка"
                        statusWhere = @"UploadingDocType is not null and v.SendTo1C is null and v.DeleteDate is null";
                        break;
                    case 5:
                        statusWhere = @" v.ManualDeductionId is not null ";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
            }
            return whereString;
        }
        public override string GetWhereForUserRole(UserRole role, int userId)
        {
            switch (role)
            {
                case UserRole.Accountant:
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                //case UserRole.Admin:
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
            int sortedBy,
            bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
            {
                orderBy = " ORDER BY UserName,EditDate DESC";
                return string.Format(sqlSelectForDeductionRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY UserName,EditDate DESC";
                    return string.Format(sqlSelectForDeductionRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by Number";
                    break;
                //case 2:
                //    sqlQuery += @" order by EditDate";
                //    break;
                case 3:
                    orderBy = @" order by EditDate";
                    break;
                case 4:
                    orderBy = @" order by Dep3Name";
                    break;
                case 5:
                    orderBy = @" order by Sum";
                    break;
                case 6:
                    orderBy = @" order by DeductionDate";
                    break;
                case 7:
                    orderBy = @" order by UserName";
                    break;
                case 8:
                    orderBy = @" order by Position";
                    break;
                case 9:
                    orderBy = @" order by Kind";
                    break;
                case 10:
                    orderBy = @" order by Dep7Name";
                    break;
                case 11:
                    orderBy = @" order by DismissalDate";
                    break;
                case 12:
                    orderBy = @" order by Status";
                    break;
                case 13:
                    orderBy = @" order by IsFastDismissal";
                    break;
                case 14:
                    orderBy = @" order by mr.Number";
                    break;
                case 15:
                    orderBy = @" order by NotUseInAnalyticalStatement";
                    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForDeductionRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }

        public DateTime? GetMinDeductionPeriod()
        {
            string sqlQuery = @"select min(DeductionDate) as DeductionDate from dbo.Deduction";
            IQuery query = Session.CreateSQLQuery(sqlQuery).AddScalar("DeductionDate", NHibernateUtil.DateTime);
            IList<DeductionDateTimeDto> list = query.SetResultTransformer(Transformers.AliasToBean(typeof(DeductionDateTimeDto))).List<DeductionDateTimeDto>();
            DeductionDateTimeDto dto = list.FirstOrDefault();
            if(dto == null)
                return new DateTime?();
            return dto.DeductionDate;
        }
        public IList<Deduction> GetDeductionsByImportId(int id)
        {
            var crit=Session.CreateCriteria(typeof(Deduction));
            crit.Add(Restrictions.Eq("DeductionImport.id", id));
            return crit.List<Deduction>();
        }
    }
    public class DeductionDateTimeDto
    {
        public DateTime? DeductionDate { get; set; }
    }
}