begin tran
set identity_insert [dbo].[HelpServiceProductionTime]  ON
insert into [dbo].[HelpServiceProductionTime] (Id,Name, SortOrder) values (1,N'24 часа',1)
insert into [dbo].[HelpServiceProductionTime] (Id,Name, SortOrder) values (2,N'5 раб. дней',2)
set identity_insert [dbo].[HelpServiceProductionTime]  OFF

set identity_insert [dbo].[HelpServiceTransferMethod]  ON	
insert into [dbo].[HelpServiceTransferMethod] (Id,Name, SortOrder) values (1,N'Электронный адрес',1)
insert into [dbo].[HelpServiceTransferMethod] (Id,Name, SortOrder) values (2,N'Курьер - сервис',2)
insert into [dbo].[HelpServiceTransferMethod] (Id,Name, SortOrder) values (3,N'Почта России',3)
set identity_insert [dbo].[HelpServiceTransferMethod]  OFF	


set identity_insert [dbo].[HelpServiceType]  ON	
insert into [dbo].[HelpServiceType]  (Id, Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable) 
							   values (1,N'Копия ТК',1,0,0,0)
insert into [dbo].[HelpServiceType]  (Id, Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable) 
							   values (2,N'Справка 2НДФЛ',2,1,0,0)
insert into [dbo].[HelpServiceType]  (Id, Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable) 
							   values (3,N'Cправка с места работы',3,0,1,1)
insert into [dbo].[HelpServiceType]  (Id, Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable) 
							   values (4,N'Расчетный лист',4,1,0,0)
insert into [dbo].[HelpServiceType]  (Id, Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable) 
							   values (5,N'Cправка о заработке для расчета пособия (182Н)',5,1,0,0)
insert into [dbo].[HelpServiceType]  (Id, Name, SortOrder, IsPeriodAvailable, IsRequirementsAvailable, IsAttachmentAvailable) 
							   values (6,N'Анкета для загранпаспорта',6,0,0,1)
set identity_insert [dbo].[HelpServiceType]  OFF	
commit