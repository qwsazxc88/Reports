using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Services;
using Reports.Core.Dto.Employment2;
using Reports.Core.Dto;
using System;
using System.Linq;
using NHibernate.Linq;

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
        /// Должности.
        /// </summary>
        /// <param name="Term">фрагмент названия для поиска.</param>
        /// <returns></returns>
        public IList<IdNameDto> GetPositions(string Term)
        {
            string sqlQuery = @"SELECT * FROM [dbo].[vwEmploymentPositions] WHERE Name like '" + (Term == null ? "" : Term) + "%' ORDER BY Name";
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


        /// <summary>
        /// Действующие должности.
        /// </summary>
        /// <param name="Term">фрагмент названия для поиска.</param>
        /// <returns></returns>
        public IList<IdNameDto> GetOperatingPositions(string Term)
        {
            return Session.Query<Position>().Where(x => !x.IsDeleted && x.Name.StartsWith(Term)).OrderBy(x => x.Name).ToList().ConvertAll(x => new IdNameDto { Id = x.Id, Name = x.Name });
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