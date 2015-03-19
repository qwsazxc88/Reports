using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentHigherEducationDiplomaDao : IDao<HigherEducationDiploma>
    {
        IList<HigherEducationDiplomaDto> GetHighEducationTypes(int EducationId);
    }
}
