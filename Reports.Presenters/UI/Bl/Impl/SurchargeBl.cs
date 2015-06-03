using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.Bl;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core;
using Reports.Core.Dto;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class SurchargeBl: ISurchargeBL
    {
        private ISurchargeDao SurchargeDao = Ioc.Resolve<ISurchargeDao>();
        public int AddSurcharge(int userId, float sum, int creatorId, DateTime editDate, int missionReportId, int deductionNumber)
        {

            return SurchargeDao.AddDocument(userId, (decimal)sum, creatorId, editDate, missionReportId, deductionNumber);
        }
        public IList<SurchargeDto> GetDocuments(int userId, UserRole role, int departmentId, int statusId, DateTime? beginDate, DateTime? endDate, string userName, int sortedBy, bool? sortDescending, string Number)
        {
            return SurchargeDao.GetDocuments(userId, role, departmentId, statusId, beginDate, endDate, userName, sortedBy, sortDescending, Number);
        }
    }
}
