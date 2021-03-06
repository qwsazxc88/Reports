﻿using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;
using Reports.Core.Services;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник кодов совместимых программ.
    /// </summary>
    public class StaffProgramCodesDao : DefaultDao<StaffProgramCodes>, IStaffProgramCodesDao
    {
        public StaffProgramCodesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        /// <summary>
        /// Достаем список кодов к совместимым программам.
        /// </summary>
        /// <param name="DMDetailId">Id управленческих реквизитов текущей заявки.</param>
        /// <returns></returns>
        public IList<ProgramCodeDto> GetProgramCodes(int DMDetailId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT B.Id, A.Id as ProgramId, A.Name as ProgramName, B.Code, C.Name as UserName
                                                    FROM StaffProgramReference as A
                                                    LEFT JOIN StaffProgramCodes as B ON B.ProgramId = A.id and " + (DMDetailId == 0 ? " B.DMDetailId is null " : " B.DMDetailId = " + DMDetailId.ToString()) + @"
                                                    LEFT JOIN Users as C ON C.Id = isnull(B.EditorId, B.CreatorId)")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("ProgramId", NHibernateUtil.Int32)
                .AddScalar("ProgramName", NHibernateUtil.String)
                .AddScalar("Code", NHibernateUtil.String)
                .AddScalar("UserName", NHibernateUtil.String);

            return query.SetResultTransformer(Transformers.AliasToBean<ProgramCodeDto>()).List<ProgramCodeDto>();
        }
    }
}
