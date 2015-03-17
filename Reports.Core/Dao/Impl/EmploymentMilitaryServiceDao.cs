using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentMilitaryServiceDao : DefaultDao<MilitaryService>, IEmploymentMilitaryServiceDao
    {
        public EmploymentMilitaryServiceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Годность к военной службе.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<MilitaryValidityCategoryDto> GetMilitaryValidityCategoryes()
        {
            string sqlQuery = @"SELECT * FROM [dbo].[MilitaryValidityCategory]";
            IQuery query = CreateQuery(sqlQuery);
            IList<MilitaryValidityCategoryDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(MilitaryValidityCategoryDto))).List<MilitaryValidityCategoryDto>();
            return documentList;
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
        }

        /// <summary>
        /// Отношение к воинскому учету.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<MilitaryRelationAccountDto> GetMilitaryRelationAccounts()
        {
            string sqlQuery = @"SELECT * FROM [dbo].[MilitaryRelationAccount]";
            IQuery query = CreateQuery(sqlQuery);
            IList<MilitaryRelationAccountDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(MilitaryRelationAccountDto))).List<MilitaryRelationAccountDto>();
            return documentList;
        }


        /// <summary>
        /// Отношение к воинскому учету.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<IdNameDto> GetMilitarySpecialityCategoryes()
        {
            string sqlQuery = @"SELECT * FROM [dbo].[MilitarySpecialityCategory]";
            IQuery query = CreateQuery(sqlQuery);
            IList<IdNameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
            return documentList;
        }
    }
}