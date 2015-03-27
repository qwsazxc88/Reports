--select * from [dbo].[HelpBillingUrgency]
-- delete from [dbo].[HelpBillingUrgency]
insert into [dbo].[HelpBillingUrgency] (Name, SortOrder) values (N'',1)
insert into [dbo].[HelpBillingUrgency] (Name, SortOrder) values (N'Срочно',2)
insert into [dbo].[HelpBillingUrgency] (Name, SortOrder) values (N'Очень срочно',3)

-- select * from [dbo].[HelpBillingTitle]
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Служба занятости запрос справка',1)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Справка по месту требования',2)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Справка с предыдущего места работы сотрудника',3)
insert into [dbo].[HelpBillingTitle] (Name, SortOrder) values (N'Справки уволенному',4)