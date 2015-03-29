using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpPersonnelBillingRequestDao : DefaultDao<HelpPersonnelBillingRequest>,
                                                  IHelpPersonnelBillingRequestDao
    {
        public const string sqlSelectForRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";
        protected const string sqlSelectForPbList =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                u.Name as UserName,
                                up.Name as Position,
                                case when v.CreatorId != v.UserId then crUser.Name else N'' end as ManagerName,
                                dep.Name as Dep7Name,
                                v.Number as RequestNumber,
                                v.FiredUserName as FiredUserName,
                                v.FiredUserSurname as FiredUserSurname,
                                v.FiredUserPatronymic as FiredUserPatronymic,
                                v.UserBirthDate as UserBirthDate,
                                v.IsOriginalReceived as IsOriginalReceived,
                                NT.Name as NoteName,
                                v.CreateDate as CreateDate,
                                v.EditDate as EditDate,
                                v.EndWorkDate as EndWorkDate,
                                v.ConfirmWorkDate as ConfirmWorkDate,
                                t.Name as RequestType,
                                m.Name as RequestTransferType,
                                case when v.[SendDate] is null then 1
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then 2 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then 3 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null and v.[NotEndWorkDate] is null then 4
                                     when v.[ConfirmWorkDate] is not null then 5 
                                     when v.[NotEndWorkDate] is not null then 6
                                    else 0
                                end as StatusNumber,
                                case when v.[SendDate] is null then N'Черновик сотрудника'
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then N'Услуга запрошена' 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then N'Услуга формируется' 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null and v.[NotEndWorkDate] is null then N'Услуга сформирована' 
                                     when v.[ConfirmWorkDate] is not null then N'Услуга оказана' 
                                     when v.[NotEndWorkDate] is not null then N'Услуга не может быть сформирована'
                                    else N''
                                end as Status,
                                v.Address as address,
                                dep3.Name as Dep3Name,
                                L.Name as ProdTimeName
                                from dbo.HelpServiceRequest v
                                inner join [dbo].[HelpServiceType] t on v.TypeId = t.Id
                                inner join [dbo].[HelpServiceTransferMethod] m on v.[TransferMethodId] = m.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                inner join [dbo].[Users] crUser on crUser.Id = v.CreatorId
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                inner join dbo.Department dep on u.DepartmentId = dep.Id
                                inner join dbo.Users currentUser on currentUser.Id = :userId
                                LEFT JOIN [dbo].[NoteType] as NT ON v.NoteId=NT.Id
                                LEFT JOIN dbo.Department dep3 ON dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                                LEFT JOIN [dbo].[HelpServiceProductionTime] as L ON L.Id = v.ProductionTimeId
                                {0}";
       
        public HelpPersonnelBillingRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public List<HelpPersonnelBillingRequestDto> GetDocuments(int userId,
               UserRole role,
               int departmentId,
               int statusId,
               DateTime? beginDate,
               DateTime? endDate,
               string initiatorUserName,
               string workerUserName,
               string  number,
               int titleId,
               int urgencyId,
               int sortBy,
               bool? sortDescending
           )
        {
            string sqlQuery = sqlSelectForPbList;

            //для кадровиков показываем вопросы по своим дирекциям
            if (role == UserRole.ConsultantOutsorsingManager)
            {
                sqlQuery = string.Format(sqlQuery, string.Empty);
                sqlQuery += "INNER JOIN [dbo].[UserToPersonnel] as N ON N.[UserID] = v.[UserID] and N.[PersonnelId] = " + userId.ToString() + " {0}";
            }

            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            //whereString = GetUserNameWhere(whereString, userName);
            whereString = GetNumberWhere(whereString, number);
            //whereString = GetTypeWhere(whereString, typeId);

            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            //AddDatesToQuery(query, beginDate, endDate, userName);
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
            query.SetInt32("userId", userId);
            List<HelpPersonnelBillingRequestDto> documentList = query
                .SetResultTransformer(Transformers.AliasToBean(typeof(HelpPersonnelBillingRequestDto)))
                .List<HelpPersonnelBillingRequestDto>().ToList();
            return documentList;
        }
    }
}