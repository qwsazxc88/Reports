using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IHelpServiceRequestDao : IDao<HelpServiceRequest>
    {
        List<HelpServiceRequestDto> GetDocuments(int userId,
                                                 UserRole role,
                                                 int departmentId,
                                                 int statusId,
                                                 DateTime? beginDate,
                                                 DateTime? endDate,
                                                 string userName,
                                                 string number,
                                                 int sortBy,
                                                 bool? sortDescending,
                                                 string Address,
                                                 int typeId=0
            );
    }
}