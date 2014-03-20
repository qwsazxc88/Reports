set identity_insert [dbo].[AppointmentReason]  ON
insert into [dbo].[AppointmentReason] (Id, Name) values (1,N'Открытие новой точки продаж')
insert into [dbo].[AppointmentReason] (Id, Name) values (2,N'Увеличение штата действующего подразделения')
insert into [dbo].[AppointmentReason] (Id, Name) values (3,N'Кадровый резерв')
insert into [dbo].[AppointmentReason] (Id, Name) values (4,N'Декрет')
insert into [dbo].[AppointmentReason] (Id, Name) values (5,N'Увольнение')
set identity_insert [dbo].[AppointmentReason] OFF