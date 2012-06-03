using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Acr3S.Core.Domain;
//using Acr3S.Core.Dto;
//using Acr3S.Core.Properties;
//using Lucene.Net.Analysis;
//using Lucene.Net.Analysis.Standard;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Utils
{
	//public sealed class LuceneUtils
	//{
	//    public const string ExactStringDelimiter = "\"";
	//    public const string WildcardSymbol = "*";
	//    public const string Delimiter = " ";
	//    public const string IndexSubdirName = "Index";

	//    private LuceneUtils()
	//    {
            
	//    }
	//    public static string GetStringForId(int id)
	//    {
	//        return string.Format(CultureInfo.InvariantCulture, "{0:D10}", id);
	//    }
	//    public static Analyzer Analyzer
	//    {
	//        get { return new StandardAnalyzer(); }
	//    }
	//    public static string AddWildcardSymbol(string word)
	//    {
	//        return WildcardSymbol + word + WildcardSymbol;
	//    }

	//}
    public sealed class CoreUtils
    {
        public const string ViewStateVersionName = "Version";

        #region Constants
        public const string UrlPattern = "^(https?://)"
        + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
        + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
        + "|" // allows either IP or domain
        + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
        + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain
        + "[a-z]{2,6})" // first level domain- .com or .museum
        + "(:[0-9]{1,4})?" // port number- :80
        + "((/?)|" // a slash isn't required if there is no file name
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
        #endregion

        private CoreUtils()
        {
            
        }
        public static List<int> CreateIdsList<T>(IEnumerable<T> items) where T : AbstractEntity
        {
            List<int> Ids = new List<int>();
            foreach (T item in items)
            {
                if(!Ids.Contains(item.Id))
                    Ids.Add(item.Id);
            }
            return Ids;
        }

        public static string CreateIn(string beginWhereStr, ArrayList ids)
        {
            Validate.NotNull(ids,"ids");
            if (ids.Count <= 0)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (int id in ids)
            {
                builder.Append(id + ", ");
            }
            string str = builder.ToString();
            return beginWhereStr + str.Substring(0, str.Length - 2) + ")";
        }
        public static string CreateIn(string beginWhereStr, IList<int> ids)
        {
            Validate.NotNull(ids, "ids");
            if (ids.Count <= 0)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (int id in ids)
            {
                builder.Append(id + ", ");
            }
            string str = builder.ToString();
            return beginWhereStr + str.Substring(0, str.Length - 2) + ")";
        }

        public static void SetUsedFlag<T>(Iesi.Collections.Generic.ISet<T> entityList, IList<ItemUsedDto> usedList) where T : AbstractUsedEntity
        {
            foreach (T record in entityList)
            {
                record.IsUsed = IsItemUsed(usedList, record.Id);
            }
        }
        public static IList<ItemUsedDto> GetFKsForEntitiesList<T>(ISession session,string viewName,  IList<T> list) where T : AbstractEntity
        {
            return session.CreateSQLQuery("SELECT * FROM " + viewName +
                                 CreateIn(" where Id in ( ", list))
                                 .AddScalar("Id", NHibernateUtil.Int32)
                                 .AddScalar("Counter", NHibernateUtil.Int16).
                                 SetResultTransformer(Transformers.AliasToBean(typeof(ItemUsedDto))).List<ItemUsedDto>();

        }
        public static IList<ItemUsedDto> GetFKsForEntitiesList<T>(ISession session, string viewName, Iesi.Collections.Generic.ISet<T> list) where T : AbstractEntity
        {
            return session.CreateSQLQuery("SELECT * FROM " + viewName +
                                 CreateIn(" where Id in ( ", list))
                                 .AddScalar("Id", NHibernateUtil.Int32)
                                 .AddScalar("Counter", NHibernateUtil.Int16).
                                 SetResultTransformer(Transformers.AliasToBean(typeof(ItemUsedDto))).List<ItemUsedDto>();

        }
        public static void SetUsedEntityFlag<T>(IList<T> entityList, IList<ItemUsedDto> usedList) where T : AbstractUsedEntity
        {
            foreach (T record in entityList)
            {
                record.IsUsed = IsItemUsed(usedList, record.Id);
            }
        }
        public static void SetUsedEntityFlag<T>(Iesi.Collections.Generic.ISet<T> entityList, IList<ItemUsedDto> usedList) where T : AbstractUsedEntity
        {
            foreach (T record in entityList)
            {
                record.IsUsed = IsItemUsed(usedList, record.Id);
            }
        }
        public static string CreateIn<T>(string beginWhereStr, IList<T> list) where T : AbstractEntity
        {
            if (list.Count <= 0)
                return string.Empty;
            string str = list.Aggregate(string.Empty, (current, entity) => current + (entity.Id + ", "));
            return beginWhereStr + str.Substring(0, str.Length - 2) + ")";
        }
        public static string CreateIn<T>(string beginWhereStr, Iesi.Collections.Generic.ISet<T> list) where T : AbstractEntity
        {
            if (list.Count <= 0)
                return string.Empty;
            string str = list.Aggregate(string.Empty, (current, entity) => current + (entity.Id + ", "));
            return beginWhereStr + str.Substring(0, str.Length - 2) + ")";
        }
        //public static string CreateIn(string beginWhereStr, IList<Institution> list)
        //{
        //    if (list.Count <= 0)
        //        return string.Empty;
        //    string str = string.Empty;
        //    foreach (AbstractEntity entity in list)
        //    {
        //        str += entity.Id + ", ";
        //    }
        //    return beginWhereStr + str.Substring(0, str.Length - 2) + ")";
        //}
        public static bool IsItemUsed(IList<ItemUsedDto> list, int itemId)
        {
            foreach (ItemUsedDto dto in list)
            {
                if (dto.Id == itemId)
                    return dto.Counter > 0 ? true : false;
            }
            return false;
        }


		//public static bool IsNotNameUsed<T>(ISet<T> list, string name) where T : IEntityWithName
		//{
		//    Validate.NotNull(name, "name");
		//    foreach (T entity in list)
		//    {
		//        if (entity.Name == name) return false;
		//    }
		//    return true;
		//}


        public static int? ParseInt32(string value)
        {
            int intValue;
            if (int.TryParse(value, out intValue))
                return intValue;
            return null;
        }

        public static int ParseInt(string value)
        {
            int intValue;
            int.TryParse(value, out intValue);
            return intValue;
        }

        public static bool ParseBool(string value)
        {
            bool boolValue;
            bool.TryParse(value, out boolValue);
            return boolValue;
        }
        public static ArrayList CreateArrayList<T>(IList<T> iList) where T:AbstractEntity 
        {
            ArrayList list = new ArrayList();
            foreach (T entity in iList)
            {
                if (!list.Contains(entity.Id))
                    list.Add(entity.Id);
            }
            return list;
        }
        public static ArrayList CreateArrayList(IList<int> iList) 
        {
            ArrayList list = new ArrayList();
            foreach (int entity in iList)
            {
                if (!list.Contains(entity))
                    list.Add(entity);
            }
            return list;
        }
		//public static string GetTipsForText(string text)
		//{
		//    return GetTipsForText(text, true);
		//}
		//public static string GetTipsForText(string text,bool convertHtml)
		//{
		//    if(text == null)
		//        return null;
		//    string plainText; 
		//    if(convertHtml)
		//        plainText = PlainTextConverter.StripHtml(text);
		//    else
		//        plainText = text;

		//    return (plainText.Length > QuestionElement.FirstStemCharactersNumber) ?
		//            plainText.Substring(0, QuestionElement.FirstStemCharactersNumber): 
		//            plainText; 
		//}

		//public static void CheckCaseIsLockedByUser(BaseCase baseCase,int userId)
		//{
		//    Validate.NotNull(baseCase, "baseCase");
		//    if ((baseCase.LockUser == null) || (baseCase.LockUser.Id != userId))
		//        throw new ApplicationException(Resources.Exception_CaseIsLockedByOtherUserOrNotLock); 
		//}
        public static bool AreAllowNullStringEqual(string str1,string str2)
        {
            if (str1 == null)
                return str2 != null; 
            return str1.CompareTo(str2) == 0;
        }
    }
}
