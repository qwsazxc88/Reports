alter table dbo.Users add LoginAd nvarchar(32) null
alter table dbo.Users add Rate decimal(19,2) null
alter table dbo.Users add GivesCredit  bit not null default 0
alter table dbo.Users add Level int null