using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentReportDao : DefaultDao<AppointmentReport>, IAppointmentReportDao
    {
        public AppointmentReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<AppointmentReport> LoadForAppointmentId(int id)
        {
            return Session.CreateCriteria(typeof(AppointmentReport))
                          .Add(Restrictions.Eq("Appointment.Id", id)).List<AppointmentReport>().ToList();
        }
    }
}