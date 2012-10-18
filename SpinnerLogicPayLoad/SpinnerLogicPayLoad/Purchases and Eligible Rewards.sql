DECLARE @receiptId INT = 1

IF object_id('tempdb..#EligibleRewardIds') IS NOT NULL
BEGIN
   DROP TABLE #EligibleRewardIds
END

IF object_id('tempdb..#RequirementsForEligibleRewards') IS NOT NULL
BEGIN
   DROP TABLE #RequirementsForEligibleRewards
END

/* Get Qualified Rewards based on products purchased */
SELECT      rr.RewardId --, COUNT(p.ProductId), COUNT(rr.RewardId)
INTO		#EligibleRewardIds
FROM        RewardRequirements rr 
            LEFT JOIN ReceiptItems p
                ON  (p.ProductId = rr.ProductId
                AND p.Quantity >= rr.Quantity)   
                AND p.ReceiptId = @receiptId	
GROUP BY    rr.RewardId 
HAVING      COUNT(p.ProductId) = COUNT(rr.RewardId)

/* Select all requirements based on products */
SELECT 
rr2.Id 'ReqId'
, RewardId
, rr2.ProductId 'ReqProdId'
, Quantity
--, pc.Id 'ProdCatId'
, pc.ProductId 'PCProdId'
, pc.CategoryId	'PCCatId'
INTO #RequirementsForEligibleRewards
FROM RewardRequirements rr2 
LEFT JOIN ProductsCategories pc on rr2.ProductId = pc.ProductId
WHERE RewardId in (SELECT RewardId FROM #EligibleRewardIds)

/* Select all requirements based on categories */
INSERT INTO #RequirementsForEligibleRewards
SELECT 
rr.Id 'ReqId'
, RewardId
, rr.ProductId 'ReqProdId'
, Quantity
--, pc.Id 'ProdCatId'
, pc.ProductId 'PCProdId'
, pc.CategoryId	'PCCatId'
 FROM RewardRequirements rr
join ProductsCategories pc 
on rr.CategoryId = pc.CategoryId
where pc.ProductId in (SELECT RewardId FROM #EligibleRewardIds)

/* Return the Payload */

/* Items Purchased and their categories */
SELECT 
p.Id 'RecItemId'
, ReceiptId
, p.ProductId 'pProdId'
, Quantity
--, pc.Id 'ProdCatId'
, pc.CategoryId 'PCCatId'
FROM ReceiptItems p 
LEFT JOIN ProductsCategories pc
ON p.ProductId = pc.ProductId
WHERE ReceiptId = @receiptId

/* Eligible Rewards */
SELECT * FROM #RequirementsForEligibleRewards
