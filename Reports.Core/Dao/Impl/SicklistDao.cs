﻿using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistDao : DefaultDao<Sicklist>, ISicklistDao
    {
        public SicklistDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
       
        public IList<SicklistDto> GetSicklistDocuments(
               int userId, 
               UserRole role,
               int departmentId,
               int positionId,
               int typeId,
               int statusId,
               int paymentPercentTypeId,
               DateTime? beginDate,
               DateTime? endDate,
               string userName, 
               string SicklistNumber,
               int sortedBy,
               bool? sortDescending
            )
        {
            string sqlQuery =
                string.Format(sqlSelectForListSicklist,
                    DeleteRequestText,
                    "dbo.SicklistType",
                    "v.[CreateDate]",
                    "Болезнь (неявка)",
                    "[dbo].[Sicklist]",
                    "v.[BeginDate]",
                    "v.[EndDate]"
                );

            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);

            //фильт по номеру больничного
            if (SicklistNumber != null)
            {
                if (whereString.Trim().Length != 0)
                    whereString += " and SicklistNumber = '" + SicklistNumber + "' ";
                else
                    whereString += " SicklistNumber = '" + SicklistNumber + "' ";
            }

            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            query.SetInt32("userId", userId);
            AddDatesToQuery(query, beginDate, endDate, userName);

            return query.SetResultTransformer(Transformers.AliasToBean<SicklistDto>()).List<SicklistDto>();
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestStatus", NHibernateUtil.String).
                AddScalar("UserExperienceIn1C", NHibernateUtil.Boolean).
                AddScalar("IsOriginalReceived", NHibernateUtil.Boolean).
                AddScalar("SicklistNumber", NHibernateUtil.String);
        }

        public bool ResetApprovals(int id)
        {
            var sicklist = Session.Get<Sicklist>(id);
            if (sicklist != null)
            {
                var transaction = Session.BeginTransaction();
                sicklist.ManagerDateAccept = null;
                sicklist.UserDateAccept = null;
                Session.Update(sicklist);
                transaction.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Sicklist> LoadForIdsList(List<int> ids)
        {
            if (ids.Count == 0)
                return new List<Sicklist>();
            ICriteria criteria = Session.CreateCriteria(typeof(Sicklist));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<Sicklist>();
        }
        
        public int GetRequestCountsForUserAndDates(DateTime beginDate, DateTime endDate, int userId, int requestId)
        {
            return GetRequestsCountForType(beginDate, endDate, RequestTypeEnum.Sicklist, userId, UserRole.Employee, requestId);
        }
                
    }
}