using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto.Employment2;
using System;
using System.Linq;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCandidateDocNeededDao : DefaultDao<EmploymentCandidateDocNeeded>, IEmploymentCandidateDocNeededDao
    {
        public EmploymentCandidateDocNeededDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Достаем запись о документе из списка записей о документах на подпись кандидату пр приеме.
        /// </summary>
        /// <param name="CandidateID">Id кандидата</param>
        /// <returns></returns>
        public IList<EmploymentCandidateDocNeededDto> GetCandidateDocNeeded(int CandidateID)
        {
            IQuery query = CreateDocQuery(@"SELECT Id as Id, CandidateId as CandidateId, DocTypeId, IsNeeded, DateCreate, CreatorId as CreatorId, DateEdit, EditorId as EditorId 
                                            FROM dbo.EmploymentCandidateDocNeeded WHERE CandidateId =" + CandidateID.ToString());// + " and DocTypeId = " + DocTypeId.ToString());
            return query.SetResultTransformer(Transformers.AliasToBean<EmploymentCandidateDocNeededDto>()).List<EmploymentCandidateDocNeededDto>();
            //return query.UniqueResult<EmploymentCandidateDocNeeded>();
        }
        public virtual IQuery CreateDocQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("CandidateId", NHibernateUtil.Int32)
                .AddScalar("DocTypeId", NHibernateUtil.Int32)
                .AddScalar("IsNeeded", NHibernateUtil.Boolean)
                .AddScalar("DateCreate", NHibernateUtil.DateTime)
                .AddScalar("DateEdit", NHibernateUtil.DateTime)
                .AddScalar("CreatorId", NHibernateUtil.Int32)
                .AddScalar("EditorId", NHibernateUtil.Int32)
                ;

            return query;
        }

        /// <summary>
        /// Достаем список записей о документах на подпись кандидату пр приеме.
        /// </summary>
        /// <param name="CandidateID">Id кандидата</param>
        /// <returns></returns>
        public IList<AttachmentNeedListDto> GetCandidateDocListNeeded(int CandidateID)
        {
            IQuery query = CreateDocListQuery("SELECT  * FROM dbo.EmploymentCandidateDocNeeded WHERE CandidateId =" + CandidateID.ToString());
            return query.SetResultTransformer(Transformers.AliasToBean<AttachmentNeedListDto>()).List<AttachmentNeedListDto>();
            
        }
        public virtual IQuery CreateDocListQuery(string sqlQuery)
        {
            IQuery query = Session.CreateSQLQuery(sqlQuery)
                //.AddScalar("Id", NHibernateUtil.Int32)
                //.AddScalar("CandidateId", NHibernateUtil.Int32)
                .AddScalar("DocTypeId", NHibernateUtil.Int32)
                .AddScalar("IsNeeded", NHibernateUtil.Boolean)
                //.AddScalar("DateCreate", NHibernateUtil.DateTime)
                //.AddScalar("DateEdit", NHibernateUtil.DateTime)
                //.AddScalar("CreatorId", NHibernateUtil.Int32)
                //.AddScalar("EditorId", NHibernateUtil.Int32)
                ;

            return query;
        }

        /// <summary>
        /// Проверка на наличие полного списка подписанных документов кандидатом.
        /// </summary>
        /// <param name="CandidateId">Id кандидата</param>
        /// <returns></returns>
        public bool CheckCandidateSignDocExists(int CandidateId)
        {
            IQuery query = Session.CreateSQLQuery(@"SELECT cast(case when count(*) = 0 then 1 else 0 end as bit) as DocExists
                                                    FROM EmploymentCandidateDocNeeded as A
                                                    LEFT JOIN RequestAttachment as B ON B.RequestId = A.CandidateId and B.RequestType = A.DocTypeId
                                                    WHERE A.CandidateId = " + CandidateId.ToString() + " and A.IsNeeded = 1 and B.Id is null")
                           .AddScalar("DocExists", NHibernateUtil.Boolean);
            return query.UniqueResult<bool>();
        }
    }
}
