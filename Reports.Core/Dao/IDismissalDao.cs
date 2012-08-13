﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IDismissalDao : IDao<Dismissal>
    {
        IList<VacationDto> GetDocuments(
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int statusId,
            DateTime? endDate);
    }
}