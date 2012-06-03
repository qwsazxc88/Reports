--begin tran
insert into settings 
(
--	Id
	Version
	,DownloadDictionaryFilePath
	,UploadTimesheetFilePath
	,ReleaseEmployeeDeleteDate
	,ExportImportEmail
	,SendEmailToManagerAboutNew
	,NotificationEmail
	,NotificationSmtp
	,NotificationPort
	,NotificationLogin
	,NotificationPassword
	,BillingEmail
	,BillingSmtp
	,BillingPort
	,BillingLogin
	,BillingPassword
)
values
(
--	1
	1
	,'c:\temp\import.csv'
	,'c:\temp\export1.csv'
	,'2011-09-01 00:00:00.000'
	,'andreyu_2003@mail.ru'
	,1
	,'andreyu@migmail.ru'
	,'mail.migmail.ru'
	,25
	,'andreyu@migmail.ru'
	,'BPp8KQA7'
	,'andreyu@migmail.ru'
	,'mail.migmail.ru'
	,25
	,'andreyu@migmail.ru'
	,'BPp8KQA7'
)
--rollback