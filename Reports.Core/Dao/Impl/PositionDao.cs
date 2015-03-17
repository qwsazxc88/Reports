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
    public class PositionDao : DefaultDao<Position>, IPositionDao
    {
        public const string NameFieldName = "Name";

        public PositionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        /// <summary>
        /// Годность к военной службе.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<IdNameDto> GetPositions(string Term)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[Position] WHERE Name like '" + (Term == null ? "" : Term) + "%' ORDER BY Name";
            IQuery query = CreateQuery(sqlQuery);
            IList<IdNameDto> documentList = query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
            return documentList;
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
        }

        #region IPositionDao Members

        //public IList<Position> LoadAllSorted()
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof (Position));
        //    criteria.AddOrder(new Order(NameFieldName, true));
        //    return criteria.List<Position>();
        //}

        #endregion
    }
}