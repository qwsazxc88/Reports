declare @typeId int
insert into [dbo].[HelpQuestionType] (Name, SortOrder) values (N'По начислению заработной платы',1)
set @typeId = scope_identity()
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Больничные листы',1,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Премии',2,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Отпуска',3,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Исполнительные листы',4,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Авансы',5,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Районные, северные коэф.',6,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Прочие',7,@typeId)

insert into [dbo].[HelpQuestionType] (Name, SortOrder) values (N'Кадровые вопросы',2)
set @typeId = scope_identity()
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Остаток отпуска',1,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Табель',2,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Смена ФИО',3,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Смена адреса',4,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Смена паспорта',5,@typeId)

insert into [dbo].[HelpQuestionType] (Name, SortOrder) values (N'Вопросы по командировкам',3)

insert into [dbo].[HelpQuestionType] (Name, SortOrder) values (N'Техподдержка',4)

insert into [dbo].[HelpQuestionType] (Name, SortOrder) values (N'По удержаниям',5)
set @typeId = scope_identity()
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Удержания по подотчетным суммам',1,@typeId)
insert into [dbo].[HelpQuestionSubtype] (Name, SortOrder, TypeId) values (N'Удержания по испол. листам',2,@typeId)