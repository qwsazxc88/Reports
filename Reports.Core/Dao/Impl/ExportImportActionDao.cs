using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ExportImportActionDao : DefaultDao<ExportImportAction>, IExportImportActionDao
    {
        public ExportImportActionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<ExportImportAction> LoadForTypeSorted(ExportImportType type)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(ExportImportAction));
            criteria.Add(Restrictions.Eq("Type",(int)type));
            criteria.AddOrder(new Order("Date", false));
            return criteria.List<ExportImportAction>();
        }
    }
}