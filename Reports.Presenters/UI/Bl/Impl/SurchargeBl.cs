using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Presenters.UI.Bl;
using Reports.Core.Domain;
using Reports.Core.Dao;
using Reports.Core;
namespace Reports.Presenters.UI.Bl.Impl
{
    public class SurchargeBl: ISurchargeBL
    {

        public bool AddSurcharge(int userId, float sum, int creatorId, DateTime editDate,  int missionReportId)
        {
            var SurchargeDao = Ioc.Resolve<ISurchargeDao>();
            SurchargeDao.AddDocument(userId, (decimal)sum, creatorId, editDate,missionReportId);
            return true;
        }
    }
}
