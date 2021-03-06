﻿using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;
using System;
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
        public virtual List<AppointmentReport> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                int sortBy,
                bool? sortDescending)
        {
            var criteria = Session.CreateCriteria<AppointmentReport>();
            return criteria.List<AppointmentReport>().ToList();
        }
        public virtual bool IsApprovedReportForAppointmentIdExists(int id)
        {
            List<AppointmentReport> list = Session.CreateCriteria(typeof(AppointmentReport))
                          .Add(Restrictions.Eq("Appointment.Id", id))
                          .Add(Restrictions.IsNotNull("DateAccept"))
                          .List<AppointmentReport>().ToList();
            return list.Count > 0;
        }
    }
}