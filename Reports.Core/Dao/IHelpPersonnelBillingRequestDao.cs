using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IHelpPersonnelBillingRequestDao : IDao<HelpPersonnelBillingRequest>
    {
        List<HelpPersonnelBillingRequestDto> GetDocuments(int userId,
                                                          UserRole role,
                                                          int departmentId,
                                                          int statusId,
                                                          DateTime? beginDate,
                                                          DateTime? endDate,
                                                          string initiatorUserName,
                                                          string workerUserName,
                                                          string number,
                                                          int titleId,
                                                          int urgencyId,
                                                          int sortBy,
                                                          bool? sortDescending
            );

    }
}