using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Presenters.UI.Bl
{
    public interface ISurchargeBL
    {
        bool AddSurcharge(int userId, float sum, int creatorId, DateTime editDate, int missionReportId);

    }
}
