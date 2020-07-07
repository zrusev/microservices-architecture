INSERT INTO [StoreAppStatisticsDB].[dbo].[BoughtProducts]
SELECT TOP (100) k.id as [ProductId]
	  ,1 as [UserId]
FROM
(SELECT p.Id
   	   ,p.CategoryId
	   ,RANK() OVER (PARTITION BY CategoryId order by Id) AS RankBy
  FROM [StoreAppCustomerDB].[dbo].[Products] p
 WHERE CategoryId in
	(SELECT [CategoryId]
	  FROM [StoreAppCustomerDB].[dbo].[Products]
  GROUP BY CategoryId
	HAVING COUNT(CategoryId)  >= 4)
  GROUP BY p.Id, p.CategoryId) k
WHERE k.RankBy <= 4