using System;
using System.Configuration;
using System.Reflection;
using log4net;
using Reports.Core.Services;

namespace Reports.Presenters.Services
{
    public class ConfigurationService : IConfigurationService
    {
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //    public string ImageStorageDir
        //    {
        //        get { return Settings.Default.ImageStorageDir; }
        //    }

        //    #region Mail Members

        //    public string MailNotificationSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["NotificationSubject"]; }
        //    }

        //    public string MailNoticeSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["NoticeSubject"]; }
        //    }

        //    public string MailPasswordRestoreSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["PasswordRestoreSubject"]; }
        //    }
        //    public string MailInfoSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["InfoSubject"]; }
        //    }

        //    public string MailNewUserAccountSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["NewUserAccountSubject"]; }
        //    }

        //    public string MailContentManagementPrivilegesGrantedSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["ContentManagementPrivilegesGrantedSubject"]; }
        //    }

        //    public string MailUserAccountApprovedSubject
        //    {
        //        get { return ConfigurationManager.AppSettings["UserAccountApprovedSubject"]; }
        //    }

        //    public string MailServerHost
        //    {
        //        get { return ConfigurationManager.AppSettings["MailServerHost"]; }
        //    }

        //    public int MailServerPort
        //    {
        //        get
        //        {
        //            int _mailServerPort;
        //            string mailServerPort = ConfigurationManager.AppSettings["MailServerPort"];
        //            if (!int.TryParse(mailServerPort, out _mailServerPort))
        //            {
        //                _mailServerPort = 25;
        //            }
        //            return _mailServerPort;
        //        }
        //    }

        //    public string SmtpLogin
        //    {
        //        get { return ConfigurationManager.AppSettings["SMTPLogin"]; }
        //    }

        //    public string SmtpPassword
        //    {
        //        get { return ConfigurationManager.AppSettings["SMTPPassword"]; }
        //    }

        //    public string MailFrom
        //    {
        //        get { return ConfigurationManager.AppSettings["FromMail"]; }
        //    }

        //    public string MailFromTitle
        //    {
        //        get { return ConfigurationManager.AppSettings["FromTitle"]; }
        //    }

        //    public string MailTemplateXslt
        //    {
        //        get { return ConfigurationManager.AppSettings["MailTemplateXSLT"]; }
        //    }

        //    //public string PortalUrl
        //    //{
        //    //    get
        //    //    {
        //    //        return ConfigurationManager.AppSettings["PortalUrl"];
        //    //    }
        //    //}

        //    #endregion


        //    #region AcrPsMails

        //    #region Constants

        //    public const string PsMailSectionName = "acrPsMailSettings";

        //    private const string keySss = "SSS";
        //    private const string keyCip = "CIP";
        //    private const string keyIsa = "ISA";
        //    private const string keyMridb = "MRIDB";
        //    private const string keyPse = "PSE";
        //    private const string keyLf = "LF";


        //    public const string PageSizeName = "PagesSize";

        //    private const string keyUsersList = "UsersList";
        //    private const string keyDefault = "Default";
        //    #endregion

        //    #region Private Properties
        //    private static NameValueCollection psMailInfo
        //    {
        //        get
        //        {
        //            return ConfigurationManager.GetSection(PsMailSectionName) as NameValueCollection;
        //        }
        //    }
        //    private static NameValueCollection pageSizes
        //    {
        //        get
        //        {
        //            return ConfigurationManager.GetSection(PageSizeName) as NameValueCollection;
        //        }
        //    }


        //    #endregion

        //    #region Public Properties

        //    public string SssPSMail
        //    {
        //        get { return psMailInfo[keySss]; }
        //    }

        //    public string CipPSMail
        //    {
        //        get { return psMailInfo[keyCip]; }
        //    }

        //    public string IsaPsMail
        //    {
        //        get { return psMailInfo[keyIsa]; }
        //    }

        //    public string MridbPsMail
        //    {
        //        get { return psMailInfo[keyMridb]; }
        //    }

        //    public string PsePsMail
        //    {
        //        get { return psMailInfo[keyPse]; }
        //    }

        //    public string LfPsMail
        //    {
        //        get { return psMailInfo[keyLf]; }
        //    }

        public const string keyUsersDocumentsDelayInDays = "UsersDocumentsDelayInDays";
        public const int defaultUsersDocumentsDelayInDays = 45;

        public const string keyPageSize = "PageSize";
        public const string keyTimesheetPageSize = "TimesheetPageSize";
        public const int defaultPageSize = 100;
        public const int defaultTimesheetPageSize = 50;

        public const string keySicklistTypeIdBabyMinding = "SicklistTypeIdBabyMinding";
        public const string keySuperPersonnelId = "SuperPersonnelId";
        public const string keyDefaultDeductionEmail = "DefaultDeductionEmail";
        public const string keyAppointmentPersonnelManagerEmail = "AppointmentPersonnelManagerEmail";
        public const string keyAppointmentStaffManagerEmail = "AppointmentStaffManagerEmail";
        public const string keyEmploymentCandidateToBackgroundCheckEmail = "EmploymentCandidateToBackgroundCheckEmail";
        public const string keyEmploymentCandidateToManagerEmail = "EmploymentCandidateToManagerEmail";
        public const string keyEmploymentBackgroundCheckToManagerEmail = "EmploymentBackgroundCheckToManagerEmail";
        public const string keyEmploymentManagerToTrainingEmail = "EmploymentManagerToTrainingEmail";
        public const string keyEmploymentManagerToHighManagerEmail = "EmploymentManagerToHighManagerEmail";
        public const string keyEmploymentPersonnelManagerToManagerEmail = "EmploymentPersonnelManagerToManagerEmail";

        #region IConfigurationService Members

        public int PageSize
        {
            get
            {
                int pageSize;
                if (!Int32.TryParse(ConfigurationManager.AppSettings[keyPageSize], out pageSize) || (pageSize < 0))
                {
                    Log.WarnFormat("No or incorrect {0} setting on configuration file. Use {1} as default value.",keyPageSize,defaultPageSize);
                    return defaultPageSize;
                }
                return pageSize;
            }
        }
        public int TimesheetPageSize
        {
            get
            {
                int pageSize;
                if (!Int32.TryParse(ConfigurationManager.AppSettings[keyTimesheetPageSize], out pageSize) || (pageSize < 0))
                {
                    Log.WarnFormat("No or incorrect {0} setting on configuration file. Use {1} as default value.", keyTimesheetPageSize, defaultTimesheetPageSize);
                    return defaultTimesheetPageSize;
                }
                return pageSize;
            }
        }

        public int SicklistTypeIdBabyMinding
        {
            get
            {
                int sicklistTypeIdBabyMinding;
                if (!Int32.TryParse(ConfigurationManager.AppSettings[keySicklistTypeIdBabyMinding], out sicklistTypeIdBabyMinding) || (sicklistTypeIdBabyMinding < 0))
                    throw new ArgumentException("No or incorrect {0} setting on configuration file.", keyPageSize);
                return sicklistTypeIdBabyMinding;
            }
        }
        public int? SuperPersonnelId
        {
            get
            {
                int superPersonnelId;
                string strValue = ConfigurationManager.AppSettings[keySuperPersonnelId];
                if (string.IsNullOrEmpty(strValue))
                    return null;
                if (!Int32.TryParse( strValue,out superPersonnelId) || (superPersonnelId < 0))
                    throw new ArgumentException("No or incorrect {0} setting on configuration file.", keySuperPersonnelId);
                return superPersonnelId;
            }
        }
        public int? StaffBossId
        {
            get
            {
                int StaffBossId;
                string strValue = ConfigurationManager.AppSettings["StaffBossId"];
                if (string.IsNullOrEmpty(strValue))
                    return null;
                if (!Int32.TryParse(strValue, out StaffBossId) || (StaffBossId < 0))
                    throw new ArgumentException("No or incorrect {0} setting on configuration file.", "StaffBossId");
                return StaffBossId;
            }
        }
        public string DefaultDeductionEmail
        {
            get { return ConfigurationManager.AppSettings[keyDefaultDeductionEmail]; }
        }
        public string AppointmentPersonnelManagerEmail
        {
            get { return ConfigurationManager.AppSettings[keyAppointmentPersonnelManagerEmail]; }
        }
        public string AppointmentStaffManagerEmail
        {
            get { return ConfigurationManager.AppSettings[keyAppointmentStaffManagerEmail]; }
        }

        public string EmploymentCandidateToBackgroundCheckEmail
        {
            get { return ConfigurationManager.AppSettings[keyEmploymentCandidateToBackgroundCheckEmail]; }
        }

        public string EmploymentCandidateToManagerEmail
        {
            get { return ConfigurationManager.AppSettings[keyEmploymentCandidateToManagerEmail]; }
        }

        public string EmploymentBackgroundCheckToManagerEmail
        {
            get { return ConfigurationManager.AppSettings[keyEmploymentBackgroundCheckToManagerEmail]; }
        }

        public string EmploymentManagerToTrainingEmail
        {
            get { return ConfigurationManager.AppSettings[keyEmploymentManagerToTrainingEmail]; }
        }

        public string EmploymentManagerToHighManagerEmail
        {
            get { return ConfigurationManager.AppSettings[keyEmploymentManagerToHighManagerEmail]; }
        }

        public string EmploymentPersonnelManagerToManagerEmail
        {
            get { return ConfigurationManager.AppSettings[keyEmploymentPersonnelManagerToManagerEmail]; }
        }
        public int UsersDocumentsDelayInDays
        {
            get
            {
                int delay;
                if (!Int32.TryParse(ConfigurationManager.AppSettings[keyUsersDocumentsDelayInDays], out delay) ||
                    (delay < 0))
                    return defaultUsersDocumentsDelayInDays;
                return delay;
            }
        }

        //    public static int DefaultPageSize
        //    {
        //        get
        //        {
        //            int pageSize;
        //            if (!Int32.TryParse(pageSizes[keyDefault], out pageSize) || (pageSize <= 0))
        //                throw new ConfigurationErrorsException(Resources.Configuration_DefaultPageSizeError);
        //            return pageSize;
        //        }
        //    }

        //    #endregion

        //    #endregion

        //    #region Timss WebService
        //    public string TimssWSUrl
        //    {
        //        get
        //        {
        //            return ConfigurationManager.AppSettings["TimssWsUrl"];
        //        }
        //    }
        //    public string TimssWSTimeout
        //    {
        //        get
        //        {
        //            return ConfigurationManager.AppSettings["TimssWsTimeout"];
        //        }
        //    }
        //    public string TimssWSUserName
        //    {
        //        get
        //        {
        //            return ConfigurationManager.AppSettings["TimssWsUserName"];
        //        }
        //    }
        //    public string TimssWSPassword
        //    {
        //        get
        //        {
        //            return ConfigurationManager.AppSettings["TimssWsPassword"];
        //        }
        //    }
        //    #endregion

        //    public const string keyMaxSearchResultsCount = "MaxSearchCaseResultsCount";
        //    public int MaxSearchResultsCount
        //    {
        //        get
        //        {
        //            int maxResults;
        //            if (!Int32.TryParse(ConfigurationManager.AppSettings[keyMaxSearchResultsCount], out maxResults) || (maxResults <= 0))
        //                throw new ConfigurationErrorsException(Resources.Configuration_MaxSearchCaseResultsCountError);
        //            return maxResults;
        //        }
        //    }
        //    public const string keyIndexRootDirectory = "IndexRootDirectory";
        //    public string IndexRootDirectory
        //    {
        //        get
        //        {
        //            string indexRootDirectory = ConfigurationManager.AppSettings[keyIndexRootDirectory];
        //            if (string.IsNullOrEmpty(indexRootDirectory))
        //                throw new ConfigurationErrorsException(Resources.Configuration_MaxSearchCaseResultsCountError);
        //            return indexRootDirectory;
        //        }
        //    }


        TSection IConfigurationService.GetSection<TSection>(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as TSection;
        }

        #endregion
    }
}