alter table dbo.Users add IsMainManager bit not null default(0)
alter table dbo.Users add Grade int null 