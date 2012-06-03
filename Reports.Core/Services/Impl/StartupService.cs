using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Mapping;
using Reports.Core.Domain;

namespace Reports.Core.Services.Impl
{
    public class StartupService: IStartupService
    {
        public void Run()
        {
            InitMetaData();
        }

        private static int GetPropertyLength<T>(string propertyName)
        {
            Configuration cfg = NHibernateHelper.GetConfiguration();
            PersistentClass pc = cfg.GetClassMapping(typeof(T));
            Property prop = pc.GetRecursiveProperty(propertyName);
            int length = 0;

            foreach (Column col in prop.ColumnIterator)
            {
                length = col.Length;
            }

            return length;
        }

        private static void InitMetaData ()
        {
			//BaseCase.MaxNameLength = GetPropertyLength<BaseCase>("Name");
			//BaseCase.MaxKeywordsLength = GetPropertyLength<BaseCase>("Keywords");
			//BaseCase.MaxAcrCodeLength = GetPropertyLength<BaseCase>("AcrCode");
			//Course.MaxNameLength = GetPropertyLength<Course>("Name");
			//Category.MaxNameLength = GetPropertyLength<Category>("Name");

            User.MaxLoginLength = GetPropertyLength<User>("Login");
            //User.MaxPasswordLength = GetPropertyLength<User>("Password");
			//User.MaxEmailLength = GetPropertyLength<User>("Email");
			//User.MaxPhone1Length = GetPropertyLength<User>("Phone1");
			//User.MaxPhone2Length = GetPropertyLength<User>("Phone2");
			//User.MaxAddressLength = GetPropertyLength<User>("Address");
			//User.MaxMasterCustomerIdLength = GetPropertyLength<User>("MasterCustomerId");
			//User.MaxSubCustomerIdLength = GetPropertyLength<User>("SubCustomerId");
            //User.MaxFirstNameLength = GetPropertyLength<User>("FirstName");
            //User.MaxMiddleNameLength = GetPropertyLength<User>("MiddleName");
            //User.MaxLastNameLength = GetPropertyLength<User>("LastName");
            //Question.MaxTextLength = GetPropertyLength<Question>("Text");
            //Question.MaxAnswerLength = GetPropertyLength<Question>("AnswerText");
            //User.MaxDegreeLength = GetPropertyLength<User>("Profile.Degree");

			//ImageModality.MaxNameLength = GetPropertyLength<ImageModality>("Name");

			//ImagePositioning.MaxNameLength = GetPropertyLength<ImagePositioning>("Name");
			//PulseSequenceType.MaxNameLength = GetPropertyLength<PulseSequenceType>("Name");

			//Institution.MaxNameLength = GetPropertyLength<Institution>("Name");
			//Institution.MaxLocationLength = GetPropertyLength<Institution>("Location");

			//UserCreditsRecord.MaxFirstNameLength = GetPropertyLength<UserCreditsRecord>("Profile.FirstName");
			//UserCreditsRecord.MaxMiddleNameLength = GetPropertyLength<UserCreditsRecord>("Profile.MiddleName");
			//UserCreditsRecord.MaxLastNameLength = GetPropertyLength<UserCreditsRecord>("Profile.LastName");
			//UserCreditsRecord.MaxDegreeLength = GetPropertyLength<UserCreditsRecord>("Profile.Degree");
			//UserCreditsRecord.MaxTitleLength = GetPropertyLength<UserCreditsRecord>("Title");

			//Image.MaxNameLength = GetPropertyLength<Image>("Name");
			//ImageGroup.MaxNameLength = GetPropertyLength<ImageGroup>("Name");
			//ImageGroup.MaxDescriptionLength = GetPropertyLength<ImageGroup>("Description");

			////Init CipTemplatedScriptItems fields
			//CipTemplatedScriptItem.MaxDescriptionLength = GetPropertyLength<CipTemplatedScriptItem>("Description");
			//CipFindingsViewerSection.MaxTitleLength = GetPropertyLength<CipFindingsViewerSection>("Title");
			//CipStackViewSeries.MaxNameLength = GetPropertyLength<CipStackViewSeries>("Name");
			//IncorectFeedBack.MaxFeedbackLength = GetPropertyLength<IncorectFeedBack>("Feedback");
        }
    }
}
