﻿using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentCandidateDao : IDao<EmploymentCandidate>
    {
        IList<CandidateDto> GetCandidates(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                DateTime? CompleteDate,
                string userName,
                string ContractNumber1C,
                int CandidateId,
                string AppointmentReportNumber,
                int AppointmentNumber,
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