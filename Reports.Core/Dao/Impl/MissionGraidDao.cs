using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionGraidDao : DefaultDao<MissionGraid>, IMissionGraidDao
    {
        public MissionGraidDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<GradeAmountDto> GetAirTicketTypeGradeAmountForGradeAndDate(int gradeId, DateTime date)
        {
            return GetGradeAmountForGradeAndDate("[dbo].[MissionAirTicketTypeGradeValue]", "AirTicketTypeId", gradeId,
                                                 date);
        }
        public IList<GradeAmountDto> GetTrainTicketTypeGradeAmountForGradeAndDate(int gradeId, DateTime date)
        {
            return GetGradeAmountForGradeAndDate("[dbo].[MissionTrainTicketTypeGradeValue]", "TrainTicketTypeId", gradeId,
                                                 date);
        }
        public IList<GradeAmountDto> GetResidenceGradeAmountForGradeAndDate(int gradeId, DateTime date)
        {
            return GetGradeAmountForGradeAndDate("[dbo].[MissionResidenceGradeValue]", "ResidenceId", gradeId,
                                                 date);
        }
        public IList<GradeAmountDto> GetDailyAllowanceGradeAmountForGradeAndDate(int gradeId, DateTime date)
        {
            return GetGradeAmountForGradeAndDate("[dbo].[MissionDailyAllowanceGradeValue]", "[DailyAllowanceId]", gradeId,
                                                 date);
        }
        public IList<GradeAmountNameDto> GetDailyAllowanceGradeAmountForDate(DateTime date)
        {
            return GetGradeAmountForDate("[dbo].[MissionDailyAllowanceGradeValue]",
                "[DailyAllowanceId]", "dbo.MissionDailyAllowance", date);
        }
        public IList<GradeAmountNameDto> GetResidenceGradeAmountForDate(DateTime date)
        {
            return GetGradeAmountForDate("[dbo].[MissionResidenceGradeValue]", "ResidenceId", "dbo.MissionResidence",
                                                 date);
        }
    }
}