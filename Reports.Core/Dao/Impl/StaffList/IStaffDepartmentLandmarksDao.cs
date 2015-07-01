using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Ориентиры подразделений.
    /// </summary>
    public interface IStaffDepartmentLandmarksDao : IDao<StaffDepartmentLandmarks>
    {
        /// <summary>
        /// Достаем список вариантов ориентиров и их описаний для данной заявки.
        /// </summary>
        /// <param name="DMDetailId">Id текущей заявки.</param>
        /// <returns></returns>
        IList<DepLandmarkDto> GetDepartmentLandmarks(int DMDetailId);
    }
}
