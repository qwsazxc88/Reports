sp_rename 'MissionOrderRoleRecord', 'ManualRoleRecord'
go
sp_rename 'PK_MissionOrderRoleRecord', 'PK_ManualRoleRecord'
go
sp_rename 'FK_MissionOrderRoleRecord_MissionOrderRole', 'FK_ManualRoleRecord_ManualRole'
go
sp_rename 'FK_MissionOrderRoleRecord_TargetDepartment', 'FK_ManualRoleRecord_TargetDepartment'
go
sp_rename 'FK_MissionOrderRoleRecord_TargetUser', 'FK_ManualRoleRecord_TargetUser'
go
sp_rename 'FK_MissionOrderRoleRecord_User', 'FK_ManualRoleRecord_User'
go

sp_rename 'MissionOrderRole', 'ManualRole'
go
sp_rename 'PK_MissionOrderRole', 'PK_ManualRole'
go