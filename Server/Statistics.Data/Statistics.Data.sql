--INSERT INTO [StoreAppStatisticsDB].[dbo].[BoughtProducts]
--SELECT TOP (100) k.id as [ProductId]
--	  ,1 as [UserId]
--FROM
--(SELECT p.Id
--   	   ,p.CategoryId
--	   ,RANK() OVER (PARTITION BY CategoryId order by Id) AS RankBy
--  FROM [StoreAppCustomerDB].[dbo].[Products] p
-- WHERE CategoryId in
--	(SELECT [CategoryId]
--	  FROM [StoreAppCustomerDB].[dbo].[Products]
--  GROUP BY CategoryId
--	HAVING COUNT(CategoryId)  >= 4)
--  GROUP BY p.Id, p.CategoryId) k
--WHERE k.RankBy <= 4


use [StoreAppStatisticsDB]

SET IDENTITY_INSERT [dbo].[BoughtProducts] ON; 

INSERT INTO [dbo].[BoughtProducts]
([Id],[ProductId],[UserId])
VALUES
 (1, 1	,1)
,(2, 2	,1)
,(3, 3	,1)
,(4, 4	,1)
,(5, 9	,1)
,(6, 10	,1)
,(7, 11	,1)
,(8, 12	,1)
,(9, 13	,1)
,(10, 14 ,1)
,(11, 15 ,1)
,(12, 21 ,1)
,(13, 22 ,1)
,(14, 23 ,1)
,(15, 24 ,1)
,(16, 25 ,1)
,(17, 26 ,1)
,(18, 48 ,1)
,(19, 49 ,1)
,(20, 50 ,1)
,(21, 51 ,1)
,(22, 60 ,1)
,(23, 61 ,1)
,(24, 62 ,1)
,(25, 63 ,1)
,(26, 83 ,1)
,(27, 102 ,1)
,(28, 185 ,1)

SET IDENTITY_INSERT [dbo].[BoughtProducts] ON; 