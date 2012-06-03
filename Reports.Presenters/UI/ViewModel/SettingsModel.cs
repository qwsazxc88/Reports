using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Presenters.UI.ViewModel
{
    public class SettingsModel
    {
        public int Id { get; set; }
        public int Version { get; set; }

        [Display(Name = "Путь к файлу для загрузки справочника сотрудников и руководителей")]
        [Required(ErrorMessageResourceName = "SettingsModel_DownloadDictionaryFilePath_Required",
                            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_DownloadDictionaryFilePath_Required", typeof(Resources))]
        public string DownloadDictionaryFilePath { get; set; }

        [Display(Name = "Путь к файлу выгрузки табеля")]
        [Required(ErrorMessageResourceName = "SettingsModel_UploadTimesheetFilePath_Required",
                    ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_UploadTimesheetFilePath_Required", typeof(Resources))]
        public string UploadTimesheetFilePath { get; set; }

        [Display(Name = "Удалить сотрудников(и их документы),уволеных до")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Required(ErrorMessageResourceName = "SettingsModel_ReleaseEmployeeDeleteDate_Required",
                ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_ReleaseEmployeeDeleteDate_Required", typeof(Resources))]
        public string ReleaseEmployeeDeleteDate { get; set; }
        public DateTime ValidReleaseEmployeeDeleteDate { get; set; }

        [Display(Name = "E-mail для отправки сообщений о произведенном иморте/экспорте")]
        [Email]
        [Required(ErrorMessageResourceName = "SettingsModel_ExportImportEmail_Required",
            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_ExportImportEmail_Required", typeof(Resources))]
        public string ExportImportEmail { get; set; }

        [Display(Name = "Отправлять уведомления о вновь созданных документах руководителю")]
        public bool SendEmailToManagerAboutNew { get; set; }

        [Display(Name = "E-mail для отправки уведомлений")]
        [Email]
        [Required(ErrorMessageResourceName = "SettingsModel_NotificationEmail_Required",
                    ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_NotificationEmail_Required", typeof(Resources))]
        public string NotificationEmail { get; set; }

        [Display(Name = "SMTP Сервер")]
        [Required(ErrorMessageResourceName = "SettingsModel_NotificationSmtp_Required",
            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_NotificationSmtp_Required", typeof(Resources))]
        public string NotificationSmtp { get; set; }

        [Display(Name = "SMTP порт")]
        [RegularExpression("[0-9]*", ErrorMessage = "Порт должен быть неотрицательным числом")]
        [Range(0, 65536, ErrorMessage = "Порт должен быть от 0 до 65536")]
        [Required(ErrorMessageResourceName = "SettingsModel_NotificationPort_Required",
                ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_NotificationPort_Required", typeof(Resources))]
        public int NotificationPort { get; set; }


        [Display(Name = "SMTP Логин")]
        [Required(ErrorMessageResourceName = "SettingsModel_NotificationLogin_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_NotificationLogin_Required", typeof(Resources))]
        public string NotificationLogin { get; set; }

        [Display(Name = "SMTP Пароль")]
        [Required(ErrorMessageResourceName = "SettingsModel_NotificationPassword_Required",
                ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_NotificationPassword_Required", typeof(Resources))]
        public string NotificationPassword { get; set; }

        [Display(Name = "E-mail для отправки в Биллинг")]
        [Email]
        [Required(ErrorMessageResourceName = "SettingsModel_BillingEmail_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_BillingEmail_Required", typeof(Resources))]
        public string BillingEmail { get; set; }

        [Display(Name = "SMTP Сервер")]
        [Required(ErrorMessageResourceName = "SettingsModel_BillingSmtp_Required",
                ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_BillingSmtp_Required", typeof(Resources))]
        public string BillingSmtp { get; set; }

        [Display(Name = "SMTP порт")]
        [RegularExpression("[0-9]*", ErrorMessage = "Порт должен быть неотрицательным числом")]
        [Range(0, 65536, ErrorMessage = "Порт должен быть от 0 до 65536")]
        [Required(ErrorMessageResourceName = "SettingsModel_BillingPort_Required",
        ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_BillingPort_Required", typeof(Resources))]
        public int BillingPort { get; set; }

        [Display(Name = "SMTP Логин")]
        [Required(ErrorMessageResourceName = "SettingsModel_BillingLogin_Required",
            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_BillingLogin_Required", typeof(Resources))]
        public string BillingLogin { get; set; }

        [Display(Name = "SMTP Пароль")]
        [Required(ErrorMessageResourceName = "SettingsModel_BillingPassword_Required",
            ErrorMessageResourceType = typeof(Resources))]
        [LocalizationDisplayName("SettingsModel_BillingPassword_Required", typeof(Resources))]
        public string BillingPassword { get; set; }

        public string Error { get; set; }
    }
}