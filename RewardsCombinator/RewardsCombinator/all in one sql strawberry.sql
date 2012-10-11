DECLARE @product TABLE             
( 
    product_id          int IDENTITY 
    , name              varchar(50) 
) 
 
DECLARE @requirement TABLE             
( 
    requirement_id      int IDENTITY 
    , product_id        int 
    , quantity          int 
    , reward_id         int 
) 
 
DECLARE @reward TABLE             
( 
    reward_id           int IDENTITY 
    , reward            varchar(50) 
    , value				int
    , token				bit
) 
 
DECLARE @purchased TABLE             
( 
    purchased_id        int IDENTITY 
    , product_id        int 
    , quantity          int 
) 

INSERT INTO @product VALUES('Strawberry')
INSERT INTO @product VALUES('Banana')
INSERT INTO @product VALUES('Kiwi')

INSERT INTO @reward VALUES('Three Banana Reward 50', 50,1)
INSERT INTO @reward VALUES('Straweberry 20 Reward', 20,1)
INSERT INTO @reward VALUES('Straweberry 10 Reward', 10,0)
INSERT INTO @reward VALUES('Strawberry Banana 60 Reward',60,1)

INSERT INTO @requirement VALUES(2,3,1)

INSERT INTO @requirement VALUES(1,1,2)

INSERT INTO @requirement VALUES(1,1,3)

INSERT INTO @requirement VALUES(1,1,4)
INSERT INTO @requirement VALUES(2,1,4)

INSERT INTO @purchased VALUES(1,1)
INSERT INTO @purchased VALUES(2,3)
INSERT INTO @purchased VALUES(3,1)

--SELECT * FROM @product
--SELECT * FROM @reward
SELECT product_id, quantity FROM @purchased
--SELECT product_id, quantity, reward_id , (select cast(value as nvarchar) + ' , ' + cast(token as nvarchar) from @reward a where b.reward_id = a.reward_id) as value FROM @requirement b

select rr.product_id, rr.quantity, re.value, re.token, rr.reward_id from @requirement rr join @reward re on rr.reward_id = re.reward_id

/*

*/