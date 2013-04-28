﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IVacationDao : IDao<Vacation>
    {
        IList<VacationDto> GetDocuments(int userId, UserRole role, int departmentId, int positionId, int vacationTypeId,
                                        int requestStatusId,
                                        DateTime? beginDate, DateTime? endDate,
                                        int sortedBy,bool? sortDescending);

        int GetRequestCountsForUserAndDates(DateTime beginDate,
                                            DateTime endDate, int userId, int vacationId);
    }
}