using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentCandidateDao : IDao<EmploymentCandidate>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <param name="departmentId"></param>
        /// <param name="statusId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="CompleteDate"></param>
        /// <param name="EmploymentDateBegin">Начало периода даты приема</param>
        /// <param name="EmploymentDateEnd">Конец периода даты приема</param>
        /// <param name="userName"></param>
        /// <param name="ContractNumber1C"></param>
        /// <param name="CandidateId"></param>
        /// <param name="AppointmentReportNumber"></param>
        /// <param name="AppointmentNumber"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDescending"></param>
        /// <returns></returns>
        IList<CandidateDto> GetCandidates(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                DateTime? CompleteDate,
                DateTime? EmploymentDateBegin,
                DateTime? EmploymentDateEnd,
                string userName,
                string ContractNumber1C,
                int CandidateId,
                string AppointmentReportNumber,
                int AppointmentNumber,
                int PersonnelId,
                int AdditionId,
                int sortBy,
                bool? sortDescending);
        void CancelCandidatesByAppointmentId(int Id);
        IList<EmploymentCandidate> LoadForIdsList(IList<int> ids);
        IList<CandidateStateDto> GetCandidateState(int CandidateID);
        IList<CandidatePersonnelDto> GetPersonnels();
        IList<AttachmentListDto> GetCandidateAttachmentList(int CandidateID);
        /// <summary>
        /// Достаем список сканов из анкеты.
        /// </summary>
        /// <param name="CandidateID">Id заявки кандидата.</param>
        /// <returns></returns>
        IList<EmploymentAttachmentDto> GetCandidateQuestAttachmentList(int CandidateID);
    }
}