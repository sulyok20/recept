### get food
get http://localhost:3000/food

select * from food;

### get category
get http://localhost:3000/category

select * FROM category;

### get ingredient
##get http://localhost:3000/ingredient

SELECT * from ingredient;

### get used
#get http://localhost:3000/used

SELECT * from used;

### get food by id
get http://localhost:3000/food/117

SELECT * from food
  WHERE id = 117;

### get ingredient by id
get http://localhost:3000/ingredient/32

SELECT * from ingredient
  WHERE id = 32;

### get category by id
get http://localhost:3000/category/4

SELECT * from category
  WHERE id = 7;

### get used by id
get http://localhost:3000/used/5541

SELECT * from used
  WHERE id = 6152;

### post food
post http://localhost:3000/food
Content-Type: application/json

{
    "foodName": "sajtos szendvics",
    "categoryID": 3,
    "descriptionDate": "1992-03-20",
    "firstDate": "1992-04-04"
}

INSERT food 
    (foodName, categoryID, descriptionDate, firstDate)
    VALUES
    ('almás pite',3 ,2012.01.01, 2014.01.01);


### post category
post http://localhost:3000/category
Content-Type: application/json

{
    "categoryName": "szendvics"
}

INSERT category
  (categoryName)
  VALUES
  ('péksütemény');

### post category
post http://localhost:3000/ingredient
Content-Type: application/json

{
    "ingredientName": "egész fahéj"
};

INSERT ingredient
(ingredientName)
VALUES
('tört fahéj');


  ### post used
post http://localhost:3000/used
Content-Type: application/json

{
    "quantity": "2",
    "unit": "szelet",
    "foodID": 117,
    "ingredientID": 75
};

  INSERT used
  (quantity,unit ,foodID , ingredientID)
  VALUES
  ('2',szelet ,117 ,75);



#get food correct data (YYYY.MM.dd)
  select id, foodName, categoryID, 
  DATE_FORMAT(descriptionDate, '%Y.%m.%d') descriptionDate,
  DATE_FORMAT(firstDate, '%Y.%m.%d') firstDate from food;


  #get foodWithCategrory
     select f.id, f.foodName, f.categoryID, 
  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
  DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate, c.categoryName from food f
    INNER JOIN category c on c.id = f.categoryID
    ;

  #get osszes tabla ById

     select f.id, f.foodName, f.categoryID, 
  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
  DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate, c.categoryName, i.ingredientName, u.quantity, u.unit from food f
    INNER JOIN category c on c.id = f.categoryID
    INNER JOIN used u on u.foodID = f.id 
      INNER join ingredient i on i.id = u.ingredientID
    WHERE f.id = 1
    ;

  #get foodWithCategroryById

     select f.id, f.foodName, f.categoryID, 
  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
  DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate, c.categoryName from food f
    INNER JOIN category c on c.id = f.categoryID
    where f.id   = 1 
    order by f.foodName;
