using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Services;
namespace Reports.Core.Dao.Impl
{
    public class SurchargeDao:DefaultDao<Surcharge>, ISurchargeDao
    {
        public SurchargeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<Dto.SurchargeDto> GetDocuments(int userId, UserRole role, int departmentId, int statusId, DateTime? beginDate, DateTime? endDate, string userName, int sortedBy, bool? sortDescending, string Number)
        {
            throw new NotImplementedException();
        }
        public void AddDocument(int userId, decimal sum, int creatorId, DateTime editDate, int missionReportId)
        {
            var User = UserDao.Load(userId);
            var Editor = UserDao.Load(creatorId);
            var MissionReport = Ioc.Resolve<IMissionReportDao>().Load(missionReportId);
            
            var document = new Surcharge { Sum=sum, Editor=Editor, User=User, EditDate=editDate, MissionReport=MissionReport, SurchargeDate=editDate };
            
            SaveAndFlush(document);
        }
    }
}
