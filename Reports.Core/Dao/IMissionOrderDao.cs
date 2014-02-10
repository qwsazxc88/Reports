﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionOrderDao : IDao<MissionOrder>
    {
        IList<MissionOrderDto> GetDocuments(int userId,
                                            UserRole role,
                                            int departmentId,
                                            int statusId,
                                            DateTime? beginDate,
                                            DateTime? endDate,
                                            string userName,
                                            int sortBy,
                                            bool? ortDescending);

        IList<MissionOrder> LoadForIdsList(List<int> ids);

        IList<MissionUserDeptsListDto> GetUserDeptsDocuments(int userId,
                                                             UserRole role,
                                                             int departmentId,
                                                             int statusId,
                                                             DateTime? beginDate,
                                                             DateTime? endDate,
                                                             string userName,
                                                             int sortBy,
                                                             bool? sortDescending);
    }
}