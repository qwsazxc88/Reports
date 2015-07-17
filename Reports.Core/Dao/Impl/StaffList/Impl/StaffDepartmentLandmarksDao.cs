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
    /// Ориентиры подразделений.
    /// </summary>
    public class StaffDepartmentLandmarksDao : DefaultDao<StaffDepartmentLandmarks>, IStaffDepartmentLandmarksDao
    {
        public StaffDepartmentLandmarksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем список вариантов ориентиров и их описаний для данной заявки.
        /// </summary>
        /// <param name="DMDetailId">Id управленческих реквизитов текущей заявки.</param>
        /// <returns></returns>
        public IList<DepLandmarkDto> GetDepartmentLandmarks(int DMDetailId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT B.Id, A.Id as LandmarkId, A.Name as LandmarkName, B.[Description]
                                                    FROM StaffLandmarkTypes as A
                                                    LEFT JOIN StaffDepartmentLandmarks as B ON B.LandmarkId = A.id and " + (DMDetailId == 0 ? " B.DMDetailId is null " : " B.DMDetailId = " + DMDetailId.ToString()))
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("LandmarkId", NHibernateUtil.Int32)
                .AddScalar("LandmarkName", NHibernateUtil.String)
                .AddScalar("Description", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<DepLandmarkDto>()).List<DepLandmarkDto>();
        }
    }
}
