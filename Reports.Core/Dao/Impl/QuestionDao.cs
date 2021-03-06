using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Filters;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class QuestionDao : DefaultUsedDao<Question>, IQuestionDao
    {
        public const string LastNameSortFieldName = "LastName";
        public const string FirstNameSortFieldName = "FirstName";
        public const string MiddleNameSortFieldName = "MiddleName";

        public const string AnswerLastNameSortFieldName = "AnswerLastName";


        //private const string ObjectPropertyFormat = "{0}.{1}";
        private const string QuestionAlias = "Question";
        private const string UserAlias = "users";
        private const string OtherUserAlias = "otherUsers";

        public QuestionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public IList<QuestionDto> FindByFilterForUser(QuestionFilter filter, out int count)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(Question), QuestionAlias);
            ICriteria criteria2 = Session.CreateCriteria(typeof(Question), QuestionAlias);
            Junction criterion = Restrictions.Conjunction();

            if(!filter.CanAnswer)
                criterion.Add(Restrictions.Eq("User.Id", filter.UserId));
            if (filter.DateFrom.HasValue)
                criterion.Add(Restrictions.Ge("Date", filter.DateFrom.Value));
            if (filter.DateTo.HasValue)
                criterion.Add(Restrictions.Le("Date", filter.DateTo.Value.Date.AddDays(1).AddMilliseconds(-1)));
            if (filter.Type == QuestionTypes.WithAnswer)
                criterion.Add(Restrictions.IsNotNull("AnswerDate"));
            else if(filter.Type == QuestionTypes.WithoutAnswer)
                criterion.Add(Restrictions.IsNull("AnswerDate"));



            criteria.CreateAlias(string.Format(ObjectPropertyFormat, QuestionAlias, Question.UserName), UserAlias, JoinType.InnerJoin);
            criteria2.CreateAlias(string.Format(ObjectPropertyFormat, QuestionAlias, Question.UserName), UserAlias, JoinType.InnerJoin);
            if (filter.SortExpression.CompareTo(AnswerLastNameSortFieldName) == 0)
            {
                criteria.CreateAlias(string.Format(ObjectPropertyFormat, QuestionAlias, Question.AnswerUserName), OtherUserAlias, JoinType.LeftOuterJoin);
                criteria2.CreateAlias(string.Format(ObjectPropertyFormat, QuestionAlias, Question.AnswerUserName), OtherUserAlias, JoinType.LeftOuterJoin);
            }

            //if (!string.IsNullOrEmpty(filter.FirstName) ||
            //    !string.IsNullOrEmpty(filter.MiddleName) ||
            //    !string.IsNullOrEmpty(filter.LastName))
            //{
            //    Junction filterUser = Restrictions.Conjunction();
            //    if (!string.IsNullOrEmpty(filter.FirstName))
            //        filterUser.Add(Restrictions.Like(string.Format(ObjectPropertyFormat, UserAlias, User.FirstNameFieldName), filter.FirstName, MatchMode.Start));
            //    if (!string.IsNullOrEmpty(filter.LastName))
            //        filterUser.Add(Restrictions.Like(string.Format(ObjectPropertyFormat, UserAlias, User.LastNameFieldName), filter.LastName, MatchMode.Start));
            //    if (!string.IsNullOrEmpty(filter.MiddleName))
            //        filterUser.Add(Restrictions.Like(string.Format(ObjectPropertyFormat, UserAlias, User.MiddleNameFieldName), filter.MiddleName, MatchMode.Start));
            //    criterion.Add(filterUser);
            //}
            criteria.Add(criterion);
            criteria2.Add(criterion);
            criteria2.SetProjection(Projections.RowCount());
            count = (int)criteria2.UniqueResult();
            if (filter.FirstResult > count) filter.FirstResult = 0;
            if (filter.MaxResults != -1)
                criteria.SetMaxResults(filter.MaxResults);
            if (filter.FirstResult != -1)
                criteria.SetFirstResult(filter.FirstResult);
            if (!string.IsNullOrEmpty(filter.SortExpression))
            {
                //if (filter.SortExpression.CompareTo(LastNameSortFieldName) == 0)
                //{
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, UserAlias, User.LastNameFieldName), filter.SortAscending));
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, UserAlias, User.FirstNameFieldName), filter.SortAscending));
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, UserAlias, User.MiddleNameFieldName), filter.SortAscending));
                //}
                //else if  (filter.SortExpression.CompareTo(AnswerLastNameSortFieldName) == 0)
                //{
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, OtherUserAlias, User.LastNameFieldName), filter.SortAscending));
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, OtherUserAlias, User.FirstNameFieldName), filter.SortAscending));
                //    criteria.AddOrder(new Order(string.Format(ObjectPropertyFormat, OtherUserAlias, User.MiddleNameFieldName), filter.SortAscending));
                //}
                //else
                    criteria.AddOrder(new Order(filter.SortExpression, filter.SortAscending));
            }
            IList<Question> list = criteria.List<Question>();
            IList<QuestionDto> result = new List<QuestionDto>();
            if (list.Count <= 0)
                return result;
            foreach (Question question in list)
            {
                QuestionDto dto = new QuestionDto(question);
                result.Add(dto);
            }
            return result;
        }

    }
}
