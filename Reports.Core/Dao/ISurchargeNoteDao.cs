using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface ISurchargeNoteDao: IDao<SurchargeNote>
    {
        IList<SurchargeNoteDto> GetDocuments(
               int userId,
               UserRole role,
               int departmentId,
               int typeId,
               DateTime? beginDate,
               DateTime? endDate,
               string userName,
               int sortedBy,
               bool? sortDescending,
               string docNumber
            );
    }
}
