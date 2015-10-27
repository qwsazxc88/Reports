using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Режим работы подразделений.
    /// </summary>
    public class StaffDepartmentOperationModesDao : DefaultDao<StaffDepartmentOperationModes>, IStaffDepartmentOperationModesDao
    {
        public StaffDepartmentOperationModesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем список операций для подразделения по заявке.
        /// </summary>
        /// <param name="DMDetailId">Id управленческих реквизитов текущей заявки.</param>
        /// <returns></returns>
        public IList<DepOperationModeDto> GetDepartmentOperationModes(int DMDetailId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT A.Id, A.DMDetailId, A.ModeType, A.WeekDay, A.WorkBegin, A.WorkEnd, A.BreakBegin, A.BreakEnd, A.IsWorkDay
                                                    FROM dbo.fnGetDepartmentOperationModes(" + DMDetailId.ToString() + ") as A")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("DMDetailId", NHibernateUtil.Int32)
                .AddScalar("ModeType", NHibernateUtil.Int32)
                .AddScalar("WeekDay", NHibernateUtil.Int32)
                .AddScalar("WorkBegin", NHibernateUtil.String)
                .AddScalar("WorkEnd", NHibernateUtil.String)
                .AddScalar("BreakBegin", NHibernateUtil.String)
                .AddScalar("BreakEnd", NHibernateUtil.String)
                .AddScalar("IsWorkDay", NHibernateUtil.Boolean);

            return query.SetResultTransformer(Transformers.AliasToBean<DepOperationModeDto>()).List<DepOperationModeDto>();
        }
    }
}
