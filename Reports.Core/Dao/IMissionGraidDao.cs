using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionGraidDao : IDao<MissionGraid>
    {
        IList<GradeAmountDto> GetAirTicketTypeGradeAmountForGradeAndDate(int gradeId, DateTime date);
        IList<GradeAmountDto> GetTrainTicketTypeGradeAmountForGradeAndDate(int gradeId, DateTime date);
        IList<GradeAmountDto> GetResidenceGradeAmountForGradeAndDate(int gradeId, DateTime date);
        IList<GradeAmountDto> GetDailyAllowanceGradeAmountForGradeAndDate(int gradeId, DateTime date);
        IList<GradeAmountNameDto> GetAirTicketTypeGradeAmountForDate(DateTime date);
        IList<GradeAmountNameDto> GetTrainTicketTypeGradeAmountForDate(DateTime date);
        IList<GradeAmountNameDto> GetDailyAllowanceGradeAmountForDate(DateTime date);
        IList<GradeAmountNameDto> GetResidenceGradeAmountForDate(DateTime date);
    }
}