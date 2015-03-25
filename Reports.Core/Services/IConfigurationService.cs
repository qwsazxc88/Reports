using System.Configuration;

namespace Reports.Core.Services
{
    public interface IConfigurationService
    {
        int UsersDocumentsDelayInDays { get; }
        int PageSize { get; }
        int SicklistTypeIdBabyMinding { get; }
        int TimesheetPageSize { get; }
        string DefaultDeductionEmail { get; }
        int? SuperPersonnelId { get; }

        string AppointmentPersonnelManagerEmail { get; }
        string AppointmentStaffManagerEmail { get; }

        string EmploymentCandidateToBackgroundCheckEmail { get; }
        string EmploymentCandidateToManagerEmail { get; }
        string EmploymentBackgroundCheckToManagerEmail { get; }
        string EmploymentManagerToTrainingEmail { get; }
		//string ImageStorageDir { get; }
		//string MailNotificationSubject { get; }
		//string MailNoticeSubject { get; }
		//string MailInfoSubject { get; }
		//string MailPasswordRestoreSubject { get; }
		//string MailNewUserAccountSubject { get; }
		//string MailContentManagementPrivilegesGrantedSubject { get; }
		//string MailUserAccountApprovedSubject { get; }
		//string MailServerHost { get; }
		//int MailServerPort { get; }
		//string SmtpLogin { get; }
		//string SmtpPassword { get; }
		//string MailFrom { get; }
		//string MailFromTitle { get; }
		//string MailTemplateXslt { get; }
		////string PortalUrl { get; }

		//string SssPSMail { get; }
		//string CipPSMail{ get; }

		//string TimssWSUrl { get; }
		//string TimssWSTimeout { get; }
		//string TimssWSUserName { get; }
		//string TimssWSPassword { get; }


		//int MaxSearchResultsCount { get; }
		//string IndexRootDirectory { get; }

        /// <summary>
        /// Retrieves a specified configuration section for the current application's default configuration.
        /// </summary>
        /// <typeparam name="TSection">Section type</typeparam>
        /// <param name="sectionName">Section Name</param>
        /// <returns>Section instance</returns>
        TSection GetSection<TSection>(string sectionName) where TSection : ConfigurationSection;
        
    }
}
