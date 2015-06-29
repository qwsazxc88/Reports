using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник кодов совместимых программ.
    /// </summary>
    public interface IStaffProgramCodesDao : IDao<StaffProgramCodes>
    {
        /// <summary>
        /// Достаем список кодов к совместимым программам.
        /// </summary>
        /// <param name="DMDetailId">Id текущей заявки.</param>
        /// <returns></returns>
        IList<ProgramCodeDto> GetProgramCodes(int DMDetailId);
    }
}
