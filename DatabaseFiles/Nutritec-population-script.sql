INSERT INTO NUTRITIONIST(Email, Username, NutritionistCode, Id, Active, FirstName, LastName1, LastName2, BirthDate, Password, ChargeType, Weight, Height, CreditCardNumber, Country, Province, Canton)
			VALUES		('ju.navarro@gmail.com', 'juanignava', 10000, 118180814, 1, 'Juan', 'Navarro', 'Navarro', '02-08-2001', '81dc9bdb52d04dc20036dbd8313ed055', 'weekly', 60, 1.7, 123456789, 'Costa Rica', 'Cartago', 'Cartago'),
						('an.rodriguez@gmail.com', 'anarodri', 10001, 123245673, 1, 'Ana', 'Rodriguez', 'Quesada', '04-05-2003', '81dc9bdb52d04dc20036dbd8313ed055', 'monthly', 65, 1.7, 564897123, 'Mexico', 'Guadalajara', 'Central'),
						('sa.salazar@outlook.com', 'samuel-s', 10002, 303040899, 1, 'Samuel', 'Salazar', 'Carvajal', '02-03-1995', '81dc9bdb52d04dc20036dbd8313ed055', 'anual', 80, 1.9, 231658899, 'Costa Rica', 'San Jose', 'Moravia');

INSERT INTO PATIENT (Email, Username, FirstName, LastName1, LastName2, BirthDate, Passowrd, NutritionistEmail)
			VALUES	('lu.morales@gmail.com', 'luismorales', 'Luis', 'Morales', 'Rodriguez', '05-30-1999', '81dc9bdb52d04dc20036dbd8313ed055', 'ju.navarro@gmail.com'),
					('mo.waterhouse@gmail.com', 'moniwaterhouse', 'Monica', 'Waterhouse', 'Montoya', '07-08-1999', '81dc9bdb52d04dc20036dbd8313ed055', 'ju.navarro@gmail.com'),
					('jo.granados@gmail.com', 'nachogranados', 'Jose', 'Granados', 'Marin', '07-09-2000', '81dc9bdb52d04dc20036dbd8313ed055', NULL),
					('ca.sanabria@gmail.com', 'carlos', 'Carlos', 'Sanabria', 'Perez', '07-08-2001', '81dc9bdb52d04dc20036dbd8313ed055', 'an.rodriguez@gmail.com');

INSERT INTO MEASUREMENT (Number, Date, PatientEmail, Height, Weight, Hips, Waist, Neck, FatPercentage, MusclePercentage)
			VALUES	(1, '08-02-2020', 'lu.morales@gmail.com', 1.81, 70, 60, 80, 40, 15, 85),
					(2, '09-15-2020', 'lu.morales@gmail.com', 1.81, 72, 60, 82, 41, 14, 86),
					(3, '10-22-2020', 'lu.morales@gmail.com', 1.81, 72, 60, 82, 43, 14, 86),
					(4, '12-23-2020', 'lu.morales@gmail.com', 1.81, 73, 60, 83, 44, 14, 86),

					(1, '08-02-2021', 'jo.granados@gmail.com', 1.80, 73, 60, 80, 40, 15, 85),
					(2, '09-15-2021', 'jo.granados@gmail.com', 1.80, 74, 60, 82, 41, 14, 86),
					(3, '10-22-2021', 'jo.granados@gmail.com', 1.80, 73, 60, 82, 43, 14, 86),
					(4, '11-01-2021', 'jo.granados@gmail.com', 1.80, 75, 60, 83, 44, 14, 86),

					(1, '01-02-2021', 'mo.waterhouse@gmail.com', 1.70, 63, 60, 70, 40, 15, 85),
					(2, '02-15-2021', 'mo.waterhouse@gmail.com', 1.70, 64, 60, 72, 41, 14, 86),
					(3, '04-22-2021', 'mo.waterhouse@gmail.com', 1.70, 63, 60, 72, 43, 14, 86),
					(4, '08-01-2021', 'mo.waterhouse@gmail.com', 1.71, 65, 60, 73, 44, 14, 86),

					(1, '08-22-2021', 'ca.sanabria@gmail.com', 1.80, 73, 60, 80, 40, 15, 85),
					(2, '09-25-2021', 'ca.sanabria@gmail.com', 1.80, 74, 60, 82, 41, 14, 86),
					(3, '10-02-2021', 'ca.sanabria@gmail.com', 1.80, 73, 60, 82, 43, 14, 86),
					(4, '11-11-2021', 'ca.sanabria@gmail.com', 1.80, 75, 60, 83, 44, 14, 86);







INSERT INTO PRODUCT (Barcode, Approved, Name, Description, Sodium, Carbohydrates, Protein, Fat, Iron, Calcium, Calories)
			VALUES	(1000, 'Approved', 'Rice', 'A serving of white rice salt', 6, 73, 8, 3, 2, 20, 332),
					(1001, 'Approved', 'Beans', 'A serving black beans', 252, 13.3, 4, 0, 1.6, 28, 71),
					(1002, 'Approved', 'Banana', 'A unit of this fruit', 1, 26.9, 1.29, 0.39, 1.5, 15, 105),
					(1003, 'Approved', 'Beef', '100 mg steak without salt', 384, 0, 26.33, 19.5, 2.6, 18, 288),
					(1004, 'Pending', 'Coffee', 'The drink without milk or sugar', 0, 0.7, 1.19, 0.15, 0, 0, 200),
					(1005, 'Approved', 'Apple', 'A unit of this fruit', 0.8, 9.1, 0.2, 0.2, 0.1, 6, 52),
					(1006, 'Approved', 'Bread', '100 mg of normal bread', 0, 52, 7.5, 1.3, 3.6, 260, 210),
					(1007, 'Pending', 'Honey', '100 g of Bee honey', 4, 76.4, 0.4, 0, 0.4, 6, 288);

INSERT INTO RECIPE (Number, Name, PatientEmail)
			VALUES	(1, 'Pinto', 'lu.morales@gmail.com'),
					(2, 'Fruit salad', 'mo.waterhouse@gmail.com'),
					(3, 'Beef and rice', 'mo.waterhouse@gmail.com'),
					(4, 'Pinto', 'mo.waterhouse@gmail.com'),
					(5, 'Beef and beans', 'jo.granados@gmail.com'),
					(6, 'Beef and beans', 'lu.morales@gmail.com');

INSERT INTO RECIPE_HAS(RecipeNumber, ProductBarcode, Servings)
			VALUES	(1, 1000, 2),
					(1, 1001, 2),
					(2, 1002, 3),
					(2, 1005, 2),
					(3, 1003, 1),
					(3, 1000, 2),
					(4, 1000, 2),
					(4, 1001, 2),
					(5, 1003, 1),
					(5, 1001, 2),
					(6, 1003, 1),
					(6, 1001, 2);





INSERT INTO VITAMIN (Code, Name)  
			VALUES	('A', 'Vitamin A'),
					('C', 'Vitamin C'),
					('D', 'Vitamin D'),
					('E', 'Vitamin E'),
					('K', 'Vitamin K'),
					('B1', 'Tiamina'),
					('B2', 'Riboflavina'),
					('B3', 'Niacina'),
					('B6', 'Piridoxina'),
					('B12', 'Cianocobalamina'),
					('B9', 'Folato');

INSERT INTO HAS_VITAMIN (ProductBArcode, VitaminCode)
			VALUES	(1000, 'B1'),
					(1000, 'B2'),
					(1000, 'B3'),
					(1000, 'E'),

					(1001, 'B1'),
					(1001, 'B2'),
					(1001, 'B3'),

					(1002, 'A'),
					(1002, 'B1'),
					(1002, 'B2'),
					(1002, 'B3'),
					(1002, 'C'),

					(1003, 'C'),
					(1003, 'D'),
					(1003, 'E'),

					(1004, 'B2'),

					(1005, 'B1'),
					(1005, 'B2'),
					(1005, 'B3'),
					(1005, 'C'),

					(1006, 'B1'),
					(1006, 'B2'),
					(1006, 'B3'),

					(1007, 'A'),
					(1007, 'D'),
					(1007, 'E');


INSERT INTO DAILY_PLAN (Number, Name, NutritionistEmail)
			VALUES	(1, 'Fruit plan', 'ju.navarro@gmail.com'),
					(2, 'Carbohydrate plan', 'ju.navarro@gmail.com'),
					(3, 'High calories plan', 'an.rodriguez@gmail.com');


INSERT INTO FOLLOWS (PatientEmail, PlanNumber, Month)
			VALUES	('lu.morales@gmail.com', 1, 'November'),
					('mo.waterhouse@gmail.com', 2, 'November'),
					('ca.sanabria@gmail.com', 3, 'November');

INSERT INTO PLAN_HAS (PlanNumber, ProductBarcode, Mealtime, Servings)
			VALUES	(1, 1002, 'Breakfast', 1),
					(1, 1005, 'Breakfast', 2),
					(1, 1006, 'Breakfast', 1),

					(1, 1002, 'Morning snack', 1),

					(1, 1000, 'Lunch', 2),
					(1, 1001, 'Lunch', 2),

					(1, 1005, 'Afternoon snack', 1),

					(1, 1000, 'Diner', 2),
					(1, 1001, 'Diner', 2),

					(2, 1000, 'Breakfast', 1),
					(2, 1001, 'Breakfast', 2),
					(2, 1006, 'Breakfast', 1),

					(2, 1000, 'Lunch', 2),
					(2, 1001, 'Lunch', 2),

					(2, 1000, 'Diner', 2),
					(2, 1001, 'Diner', 2),

					(3, 1005, 'Breakfast', 3),
					(3, 1006, 'Breakfast', 4),

					(3, 1002, 'Morning snack', 2),

					(3, 1000, 'Lunch', 3),
					(3, 1001, 'Lunch', 3),

					(3, 1005, 'Afternoon snack', 2),

					(3, 1000, 'Diner', 4),
					(3, 1001, 'Diner', 4);

INSERT INTO CONSUMES_PRODUCT (ProductBarcode, PatientEmail, Day, Meal, Servings)
			VALUES	(1000, 'lu.morales@gmail.com', 'Monday', 'Breakfast', 2),
					(1000, 'lu.morales@gmail.com', 'Monday', 'Lunch', 2),
					(1001, 'lu.morales@gmail.com', 'Monday', 'Diner', 2),

					(1005, 'lu.morales@gmail.com', 'Tuesday', 'Morning snack', 2),
					(1001, 'lu.morales@gmail.com', 'Tuesday', 'Lunch', 2),
					(1001, 'lu.morales@gmail.com', 'Tuesday', 'Diner', 2),

					(1000, 'lu.morales@gmail.com', 'Wednesday', 'Breakfast', 3),
					(1003, 'lu.morales@gmail.com', 'Wednesday', 'Afternoon snack', 2),

					(1005, 'lu.morales@gmail.com', 'Thursday', 'Morning snack', 2),
					(1001, 'lu.morales@gmail.com', 'Thursday', 'Lunch', 2),
					(1001, 'lu.morales@gmail.com', 'Thursday', 'Diner', 2),

					(1000, 'lu.morales@gmail.com', 'Friday', 'Breakfast', 3),
					(1003, 'lu.morales@gmail.com', 'Friday', 'Afternoon snack', 2);


INSERT INTO CONSUMES_RECIPE (RecipeNumber, PatientEmail, Day, Meal)
			VALUES	(1, 'lu.morales@gmail.com', 'Tuesday', 'Breakfast'),
					(1, 'lu.morales@gmail.com', 'Wednesday', 'Lunch'),
					(1, 'lu.morales@gmail.com', 'Friday', 'Breakfast');
