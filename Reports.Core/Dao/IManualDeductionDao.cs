using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using Reports.Core.Domain;
using Reports.Core.Dto;
namespace Reports.Core.Dao
{
    public interface IManualDeductionDao: IDao<ManualDeduction>
    {
        IList<ManualDeductionDto> GetDocuments(User CurrentUser, string UserName, int Status,Department department);
    }
}
