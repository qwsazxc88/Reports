using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface IAnalyticalStatementDao
    {
        IList<AnalyticalStatementDto> GetDocuments(
              int  Id,
              UserRole  userRole,
              int   departamentId,
              DateTime? beginDate,
              DateTime?  endDate,
              string  name,
              string  Number,
              int  sortBy,
              bool? SortDescending);
    }
}
