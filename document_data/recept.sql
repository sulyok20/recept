### get food
#get http://localhost:3000/food

select * from food;

### get category
#get http://localhost:3000/category

select * FROM category;

### get ingredient
##get http://localhost:3000/ingredient

SELECT * from ingredient;

### get used
#get http://localhost:3000/used

SELECT * from used;

### get food by id
#get http://localhost:3000/food/117

SELECT * from food
  WHERE id = 117;

### get ingredient by id
#get http://localhost:3000/ingredient/32

SELECT * from ingredient
  WHERE id = 233;

### get category by id
#get http://localhost:3000/category/4

SELECT * from category
  WHERE id = 7;

### get used by id
#get http://localhost:3000/used/5541

SELECT * from used
  WHERE id = 6152;

### post food 
#post http://localhost:3000/food
#Content-Type: application/json

#{
#    "foodName": "sajtos szendvics",
#   "categoryID": 3,
#  "descriptionDate": "1992-03-20",
# "firstDate": "1992-04-04"
#}

INSERT food 
    (foodName, categoryID, descriptionDate, firstDate)
    VALUES
    ('körtés pite',3 ,'2012.01.01', '2014.01.01');


### post category
#post http://localhost:3000/category
#Content-Type: application/json

#{
#    "categoryName": "péksütemény"
#}

INSERT category
  (categoryName)
  VALUES
  ('péksütemény');

### post category
#post http://localhost:3000/ingredient
#Content-Type: application/json

#{
#    "ingredientName": "tört fahéj"
#};

INSERT ingredient
(ingredientName)
VALUES
('tört fahéj');


  ### post used
#post http://localhost:3000/used
#Content-Type: application/json

#{
#    "quantity": "2",
#    "unit": "szelet",
#    "foodID": 117,
#    "ingredientID": 75
#};

  INSERT used
  (quantity,unit ,foodID , ingredientID)
  VALUES
  ('2','db' ,117 ,232);


### Delete food
#delete http://localhost:3000/food/122
DELETE from food
  where id = 117;

### Delete category
#delete http://localhost:3000/category/122
DELETE from category
  where id = 117;

### Delete ingredient
#delete http://localhost:3000/ingredient/122
DELETE from ingredient
  where id = 117;

### Delete used
#delete http://localhost:3000/used/122
DELETE from used
  where id = 117;


#put food
  UPDATE food set 
    foodName = 'almás pite',
    categoryID = 7,
    firstDate = '2014.01.01',
    descriptionDate = '2014.01.05'
    where id = 117;


  #put category
  UPDATE category 
    set categoryName = leves
    where id = 2;

  #put ingredient
  UPDATE ingredient 
    set ingredientName = 'tört fahéj',
    where id = 231;

  #put used
  UPDATE used set 
    foodID = 7,
    quantity = 0,
    unit = 'db',
    ingredientID = 6
    where id = 117;


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
  DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate, c.categoryName, i.ingredientName from food f
    INNER JOIN category c on c.id = f.categoryID
    INNER JOIN used u on u.foodID = f.id 
      INNER join ingredient i on i.id = u.ingredientID
    WHERE f.id = 1
    ;


  #get osszes tabla 

     select f.id, f.foodName, f.categoryID, 
  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
  DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate, c.categoryName, i.ingredientName from food f
    INNER JOIN category c on c.id = f.categoryID
    INNER JOIN used u on u.foodID = f.id 
    INNER join ingredient i on i.id = u.ingredientID
    ORDER by f.id
    ;

  #get foodWithCategroryById

     select f.id, f.foodName, f.categoryID, 
  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
  DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate, c.categoryName, c.id from food f
    INNER JOIN category c on c.id = f.categoryID
    where f.id   = 1 
    order by f.foodName;

#kategoriankent kajak
    select c.id "kategoria id", c.categoryName, f.id, f.foodName, DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate, DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate from category c 
      INNER JOIN food f on c.id = f.categoryID;

#kategoriankent kajak byID
      select c.id "kategoria id", c.categoryName, f.id, f.foodName, DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
        DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate from category c 
      INNER JOIN food f on c.id = f.categoryID
      where f.categoryID = 1;


#foodWithCategroryBySearch

  select DISTINCT f.foodName, c.categoryName,  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
    DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate from food f
    inner join category c on c.id = f.categoryID
    inner join used u on u.foodID = f.id
    INNER join ingredient i on u.ingredientID = i.id
  where f.foodName like '%bab%' or i.ingredientName LIKE '%bab%'
  ;


  #get foodSearchByCategory

    select DISTINCT f.foodName, c.categoryName,  DATE_FORMAT(f.descriptionDate, '%Y.%m.%d') descriptionDate,
    DATE_FORMAT(f.firstDate, '%Y.%m.%d') firstDate from food f
    inner join category c on c.id = f.categoryID
    inner join used u on u.foodID = f.id
    INNER join ingredient i on u.ingredientID = i.id
  where c.categoryName = 'leves'
  ;

