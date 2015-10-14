/*
SELECT DepName1 as [Структура 2012], DepName2 as [Подразделение2], DepName3 as [Подразделение3], DepName4 as [Подразделение4], DepName5 as [Подразделение5], DepName6 as [Подразделение6], DepName7 as [Подразделение7], 
			 CodeSKD as [Код1С], AvailableEmployees as [ЕстьСотрудники], FinDepNameShort as [подразделение-кратко], FinDepPointCode as [КодТочкиФинград], FinDepName [Название ВСП в ФГ], FinDepCode as [ID Дирекции в ФГ], 
			 FinAdminCode as [ID управления в ФГ], FinBGCode as [IDБГ в ФГ], RPLinkCode as [ID РП-привязки в ФГ]
FROM FingradDepCodes
UNION ALL
*/
--скрипт выстраивает дерево горизонтально и цепляет к нему точки Финграда
SELECT Dep1.Name as Dep1Name,
			 Dep2.Name as Dep2Name , 
       Dep3.Name as Dep3Name, 
       Dep4.Name as Dep4Name, 
       Dep5.Name as Dep5Name, 
       Dep6.Name as Dep6Name, 
			 A.Name as Dep7Name,
			 A.CodeSKD as CodeSKD, 
			 B.AvailableEmployees as [ЕстьСотрудники], 
			 B.FinDepNameShort [подразделение-кратко], 
			 B.FinDepPointCode as [КодТочкиФинград], 
			 B.FinDepName [Название ВСП в ФГ], 
			 B.FinDepCode [ID Дирекции в ФГ], 
			 B.FinAdminCode as [ID управления в ФГ], 
			 B.FinBGCode as [IDБГ в ФГ], 
			 B.RPLinkCode as [ID РП-привязки в ФГ]
FROM Department as A
LEFT JOIN Department as Dep1 ON A.Path like Dep1.Path + N'%' and Dep1.ItemLevel = 1
LEFT JOIN Department as Dep2 ON A.Path like Dep2.Path + N'%' and Dep2.ItemLevel = 2
LEFT JOIN Department as Dep3 ON A.Path like Dep3.Path + N'%' and Dep3.ItemLevel = 3
LEFT JOIN Department as Dep4 ON A.Path like Dep4.Path + N'%' and Dep4.ItemLevel = 4
LEFT JOIN Department as Dep5 ON A.Path like Dep5.Path + N'%' and Dep5.ItemLevel = 5
LEFT JOIN Department as Dep6 ON A.Path like Dep6.Path + N'%' and Dep6.ItemLevel = 6
LEFT JOIN FingradDepCodes as B ON B.CodeSKD = A.CodeSKD
WHERE A.ItemLevel = 7
ORDER BY Dep1.Name, Dep2.Name, Dep3.Name, Dep4.Name, Dep5.Name, Dep6.Name, A.Name
--437
--6877
--7314