/* This is one half of the code necessary to
determine rewards for purchased items.
The other half resides in .Net CSharp. 
That code takes the eligible rewards from this
procedure and runs combinations to determine
best reward value for purchased items.
*/

/* Only parameter */
DECLARE @receiptId INT = 1

/* Temp tables */
IF object_id('tempdb..#purchasedItems') IS NOT NULL
   DROP TABLE #purchasedItems
IF object_id('tempdb..#EligibleRewardIds') IS NOT NULL
   DROP TABLE #EligibleRewardIds
IF object_id('tempdb..#EligibleRewardIdsCategories') IS NOT NULL
   DROP TABLE #EligibleRewardIdsCategories
IF object_id('tempdb..#RequirementsForEligibleRewards') IS NOT NULL
   DROP TABLE #RequirementsForEligibleRewards

/* Items purchased and their categories */
SELECT 
p.Id 'RecItemId'
, ReceiptId
, p.ProductId 'pProdId'
, Quantity
, pc.CategoryId 'PCCatId'
INTO #purchasedItems
FROM ReceiptItems p 
LEFT JOIN ProductsCategories pc
ON p.ProductId = pc.ProductId
WHERE ReceiptId = @receiptId


/* Get eligible rewards based on products purchased */
SELECT      rr.RewardId 
INTO		#EligibleRewardIds
FROM        RewardRequirements rr 
            LEFT JOIN ReceiptItems p
                ON  (p.ProductId = rr.ProductId
                AND p.Quantity >= rr.Quantity)   
                AND p.ReceiptId = @receiptId	
GROUP BY    rr.RewardId 
HAVING      COUNT(p.ProductId) = COUNT(rr.RewardId)


/* Get eligible rewards based on categories purchased */
SELECT      rr.RewardId 
INTO		#EligibleRewardIdsCategories
FROM        RewardRequirements rr 
            LEFT JOIN #purchasedItems p
                ON  (p.PCCatId = rr.CategoryId
                AND p.Quantity >= rr.Quantity)   
                AND p.ReceiptId = @receiptId
GROUP BY    rr.RewardId 
HAVING      COUNT(p.PCCatId) = COUNT(rr.RewardId)


/* Select all requirements based on products */
SELECT 
rr2.Id 'ReqId'
, RewardId
, rr2.ProductId 'ReqProdId'
, Quantity
, pc.ProductId 'PCProdId'
, pc.CategoryId	'PCCatId'
INTO #RequirementsForEligibleRewards
FROM RewardRequirements rr2 
LEFT JOIN ProductsCategories pc on rr2.ProductId = pc.ProductId
WHERE RewardId in (SELECT RewardId FROM #EligibleRewardIds)

/* Select all requirements based on categories */
INSERT INTO #RequirementsForEligibleRewards
SELECT 
rr2.Id 'ReqIdxxxx'
, RewardId
, rr2.ProductId 'ReqProdId'
, Quantity
, pc.ProductId 'PCProdId'
, pc.CategoryId	'PCCatId'
FROM RewardRequirements rr2 
LEFT JOIN ProductsCategories pc on rr2.ProductId = pc.ProductId
WHERE RewardId in (SELECT RewardId FROM #EligibleRewardIdsCategories)


/* Return the Payload */

/* Purchased Items and their categories */
SELECT * FROM #purchasedItems

/* Eligible rewards */
SELECT * FROM #RequirementsForEligibleRewards