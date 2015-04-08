using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentCandidateDocNeededDao : IDao<EmploymentCandidateDocNeeded>
    {
        /// <summary>
        /// Достаем запись о документе из списка записей о документах на подпись кандидату пр приеме.
        /// </summary>
        /// <param name="CandidateID">Id кандидата</param>
        /// <returns></returns>
        IList<EmploymentCandidateDocNeededDto> GetCandidateDocNeeded(int CandidateID);
        /// <summary>
        /// Достаем список записей о документах на подпись кандидату пр приеме.
        /// </summary>
        /// <param name="CandidateID">Id кандидата</param>
        /// <returns></returns>
        IList<AttachmentNeedListDto> GetCandidateDocListNeeded(int CandidateID);
    }
}
