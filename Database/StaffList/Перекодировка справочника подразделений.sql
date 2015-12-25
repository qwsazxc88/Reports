--update Department set codeold = code, Code1Cold = Code1C, ParentIdold = ParentId, pathold = path
--update Department set code = codeold, Code1C = Code1Cold, ParentId = ParentIdold, path = pathold
--FK_Department_Department

ALTER TABLE [dbo].[Department] DROP CONSTRAINT [FK_Department_Department]
GO

set nocount on

update Department set Code1COld = Code1C, CodeOld = Code, ParentIdOld = ParentId, PathOld = Path

declare @id int, @code1c int
select * into #tmp from Department order by ItemLevel

while exists(select * from #tmp)
begin
	select top 1 @id = id, @code1c = Code1C from #tmp order by ItemLevel

	update Department set Code = cast(Id as varchar), Code1C = Id, Path = replace(Path, cast(@code1c as nvarchar), cast(id as nvarchar))
	where Id = @id

	update Department set ParentId = @id where ParentId = @code1c

	update Department set Path = REPLACE(path, cast(@code1c as nvarchar), cast(@id as nvarchar)) where path like N'%' + cast(@code1c as nvarchar) + N'%'

	delete from #tmp where id = @id

	print cast(@id as varchar)
end


drop table #tmp 


ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Code1C])
GO

ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO


select * from Department order by ItemLevel
