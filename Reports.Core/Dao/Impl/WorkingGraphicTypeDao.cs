using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class WorkingGraphicTypeDao : DefaultDao<WorkingGraphicType>, IWorkingGraphicTypeDao
    {
        public WorkingGraphicTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
             

        }
        public IList<WorkingGraphicTypeDto> GetWorkingGraphicTypeDtoForUsers(IList<int> userIds)
        {
            if(userIds.Count == 0)
                return new List<WorkingGraphicTypeDto>();
            string sqlQuery =
                string.Format(@"select UserId,FillDays
                         from  
                        [dbo].[WorkingGraphicType] wgt
                        inner join [dbo].[WorkingGraphicTypeToUser] wgtu
                        on wgtu.[WorkingGraphicTypeId] = wgt.[Id]
                        where UserId in (:userList)");
           
            IQuery query = Session.CreateSQLQuery(sqlQuery).
               AddScalar("UserId", NHibernateUtil.Int32).
               AddScalar("FillDays", NHibernateUtil.Boolean).
               SetParameterList("userList", userIds);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(WorkingGraphicTypeDto))).List<WorkingGraphicTypeDto>();
        }
       
    }
}