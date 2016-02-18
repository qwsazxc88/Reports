
declare @SEPId int, @UserId int
SELECT @UserId = Id FROM Users WHERE Code = '0000019702'

--ше
INSERT INTO StaffEstablishedPost([Version], PositionId, DepartmentId, Quantity, Salary, IsUsed, BeginAccountDate)
VALUES(1, 472, 8288, 1, 0, 1, '20151224')

set @SEPId = @@IDENTITY

--расстановка
INSERT INTO StaffEstablishedPostUserLinks(Version, SEPId, UserId, IsUsed)
VALUES(1, @SEPId, @UserId, 1)
--архив ше.
--заявка
INSERT INTO StaffEstablishedPostRequest([Version]
																				,RequestTypeId
																				,DateRequest
																				,SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,IsUsed
																				,IsDraft
																				,DateSendToApprove
																				,DateAccept
																				,BeginAccountDate
																				,ReasonId
																				,CreatorID)

SELECT 1
			 ,4	--ввод начальных данных 
			 ,'20151031'
			 ,Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,0
			 ,'20151224'
			 ,'20151224'
			 ,'20151224'
			 ,null	--причину пока не указываю
			 ,null --ввод начальных данных
FROM StaffEstablishedPost WHERE Id = @SEPId

--архив
			 INSERT INTO StaffEstablishedPostArchive(SEPId
																				,PositionId
																				,DepartmentId
																				,Quantity
																				,Salary
																				,IsUsed
																				,BeginAccountDate
																				,[Priority]
																				,CreatorID)
SELECT Id
			 ,PositionId
			 ,DepartmentId
			 ,Quantity
			 ,Salary
			 ,IsUsed
			 ,BeginAccountDate
			 ,null	--пока не заполняю
			 ,CreatorID
FROM StaffEstablishedPost WHERE Id = @SEPId