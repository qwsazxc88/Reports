using System;

namespace Reports.Core.Domain
{
    public class Settings : AbstractEntityWithVersion
    {
        public virtual string DownloadDictionaryFilePath { get; set; }
        public virtual string UploadTimesheetFilePath { get; set; }
        public virtual DateTime ReleaseEmployeeDeleteDate { get; set; }
        public virtual string ExportImportEmail { get; set; }
        public virtual bool SendEmailToManagerAboutNew { get; set; }
        public virtual string NotificationEmail { get; set; }
        public virtual string NotificationSmtp { get; set; }
        public virtual int NotificationPort { get; set; }
        public virtual string NotificationLogin { get; set; }
        public virtual string NotificationPassword { get; set; }
        public virtual string BillingEmail { get; set; }
        public virtual string BillingSmtp { get; set; }
        public virtual int BillingPort { get; set; }
        public virtual string BillingLogin { get; set; }
        public virtual string BillingPassword { get; set; }
    }
}