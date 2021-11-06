
-- Create tables --


-- Table #1 Nutritionist -- 

CREATE TABLE NUTRITIONIST
(
	Email				VARCHAR(100) NOT NULL,
	Username			VARCHAR(100) NOT NULL,
	NutritionistCode	INT NOT NULL,
	Id					INT NOT NULL,
	Active				INT NOT NULL,
	FirstName			VARCHAR(100) NOT NULL,
	LastName1			VARCHAR(100) NOT NULL,
	LastName2			VARCHAR(100),
	BirthDate			DATE,
	Password			VARCHAR(200) NOT NULL,			-- encrypted password
	ChargeType			VARCHAR(100) NOT NULL,	
	Weight				FLOAT(2),						-- weight in kg	
	Height				FLOAT(2),						-- height in m
	CreditCardNumber	INT,
	Country				VARCHAR(100),
	Province			VARCHAR(100),
	Canton				VARCHAR(100),
	PrictureUrl			VARCHAR(500)

	PRIMARY KEY (Email),
	UNIQUE (Username, NutritionistCode, Id, CreditCardNumber)
);

-- Table #2 Patient --

CREATE TABLE PATIENT
(
	Email				VARCHAR(100) NOT NULL,
	Username			VARCHAR(100) NOT NULL,
	FirstName			VARCHAR(100) NOT NULL,
	LastName1			VARCHAR(100) NOT NULL, 
	LastName2			VARCHAR(100) NOT NULL,
	BirthDate			DATE,
	Passowrd			VARCHAR(100) NOT NULL,
	NutritionistEmail	VARCHAR(100),

	PRIMARY KEY (Email),
	UNIQUE (Username)
);

-- Table #3 Prduct -- 

CREATE TABLE PRODUCT
(
	Barcode				INT NOT NULL,
	Approved			VARCHAR(100) NOT NULL,
	Name				VARCHAR(100) NOT NULL, 
	Description			VARCHAR(300),
	Sodium				FLOAT(2) NOT NULL,				-- mg/serving
	Carbohydrates		FLOAT(2) NOT NULL,				-- g/serving
	Protein				FLOAT(2) NOT NULL,				-- g/serving
	Fat					FLOAT(2) NOT NULL,				-- g/serving
	Iron				FLOAT(2) NOT NULL,				-- mg/serving
	Calcium				FLOAT(2) NOT NULL,				-- mg/serving
	Calories			FLOAT(2) NOT NULL,				-- amount/serving
	
	PRIMARY KEY (Barcode)
);

-- Table #4 Recipe --

CREATE TABLE RECIPE
(
	Number				INT NOT NULL,
	Name				VARCHAR(100) NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,

	PRIMARY KEY(Number)
);

-- Table #5 Measurement --

CREATE TABLE MEASUREMENT
(
	Number				INT NOT NULL,
	Date				DATE NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,
	Height				FLOAT(2),						-- in m
	Weight				FLOAT(2),						-- in kg
	Hips				FLOAT(2),						-- in cm
	Waist				FLOAT(2),						-- in cm
	Neck				FLOAT(2),						-- in cm
	FatPercentage		FLOAT(2) CHECK(FatPercentage > 0 AND FatPercentage < 100),
	MusclePercentage	FLOAT(2) CHECK(MusclePercentage > 0 AND MusclePercentage < 100),

	PRIMARY KEY (PatientEmail, Number)
);

-- Table #6 Vitamin --

CREATE TABLE VITAMIN
(
	Code				VARCHAR(10) NOT NULL,
	Name				VARCHAR(100) NOT NULL,

	PRIMARY KEY(Code)
);

-- Table #7 Has_vitamin -- 

CREATE TABLE HAS_VITAMIN
(
	ProductBarcode		INT NOT NULL,
	VitaminCode			VARCHAR(10) NOT NULL,

	PRIMARY KEY(ProductBarcode, VitaminCode)
);

-- Table #8 Daily plan --

CREATE TABLE DAILY_PLAN
(
	Number				INT NOT NULL,
	Name				VARCHAR(100) NOT NULL, 
	NutritionistEmail	VARCHAR(100) NOT NULL,

	PRIMARY KEY(Number)
);

-- Table #9 Follows -- 

CREATE TABLE FOLLOWS
(
	PatientEmail		VARCHAR(100) NOT NULL,
	PlanNumber			INT NOT NULL,
	Month				VARCHAR(100) NOT NULL,

	PRIMARY KEY (PatientEmail, PlanNumber)
);

-- Table #10 Plan_has --

CREATE TABLE PLAN_HAS
(
	PlanNumber			INT NOT NULL,
	ProductBarcode		INT NOT NULL,
	Mealtime			VARCHAR(100) NOT NULL,
	Servings			INT NOT NULL,

	PRIMARY KEY(PlanNumber, ProductBarcode, Mealtime)
);

-- Table #11 Consumes_product -- 

CREATE TABLE CONSUMES_PRODUCT
(
	ProductBarcode		INT NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,
	Day					VARCHAR(100) NOT NULL,
	Meal				VARCHAR(100) NOT NULL,
	Servings			INT NOT NULL,

	PRIMARY KEY (ProductBarcode, PatientEmail, Day, Meal)
);

-- Table #12 Consumes_recipe --

CREATE TABLE CONSUMES_RECIPE
(
	RecipeNumber		INT NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,
	Day					VARCHAR(100) NOT NULL,
	Meal				VARCHAR(100) NOT NULL,

	PRIMARY KEY (RecipeNumber, PatientEmail, Day, Meal)
);

-- Table #13 Recipe_has --

CREATE TABLE RECIPE_HAS
(
	RecipeNumber		INT NOT NULL,
	ProductBarcode		INT NOT NULL,
	Servings			INT NOT NULL,

	PRIMARY KEY (RecipeNumber, ProductBarcode)
);

CREATE TABLE ADMIN
(
	Email				VARCHAR(100) NOT NULL,
	Username			VARCHAR(100) NOT NULL,
	Password			VARCHAR(200) NOT NULL
);


-- Alter tables --

-- fk #1 --
ALTER TABLE DAILY_PLAN
ADD CONSTRAINT DAILY_PLAN_NUTRITIONIST_FK FOREIGN KEY (NutritionistEmail)
REFERENCES Nutritionist (Email);

-- fk #2 --
ALTER TABLE FOLLOWS
ADD CONSTRAINT FOLLOWS_DAILY_PLAN FOREIGN KEY (PlanNumber)
REFERENCES DAILY_PLAN (Number);

-- fk #3 --
ALTER TABLE FOLLOWS
ADD CONSTRAINT FOLLOWS_PATIENT_FK FOREIGN KEY (PatientEmail)
REFERENCES PATIENT(Email);

-- fk #4 --
ALTER TABLE PLAN_HAS
ADD CONSTRAINT PLAN_HAS_DAILY_PLAN_FK FOREIGN KEY (PlanNumber)
REFERENCES DAILY_PLAN(Number);

-- fk #5 --
ALTER TABLE PLAN_HAS
ADD CONSTRAINT PLAN_HAS_PRODUCT_FK FOREIGN KEY (ProductBarcode)
REFERENCES PRODUCT(Barcode);

-- fk #6 --
ALTER TABLE CONSUMES_PRODUCT
ADD CONSTRAINT CONSUMES_PRODUCT_PRODUCT_FK FOREIGN KEY (ProductBarcode)
REFERENCES PRODUCT(Barcode);

-- fk #7 --
ALTER TABLE CONSUMES_PRODUCT
ADD CONSTRAINT CONSUMES_PRODUCT_PATIENT_FK FOREIGN KEY (PatientEmail)
REFERENCES PATIENT(Email);

-- fk #8 --
ALTER TABLE CONSUMES_RECIPE
ADD CONSTRAINT CONSUMES_RECIPE_RECIPE_FK FOREIGN KEY (RecipeNumber)
REFERENCES RECIPE(Number);

-- fk #9 --
ALTER TABLE CONSUMES_RECIPE
ADD CONSTRAINT CONSUMES_RECIPE_PATIENT_FK FOREIGN KEY (PatientEmail)
REFERENCES PATIENT(Email);

-- fk #10 --
ALTER TABLE RECIPE
ADD CONSTRAINT RECIPE_PATIENT_FK FOREIGN KEY (PatientEmail)
REFERENCES PATIENT(Email);

-- fk #11 --
ALTER TABLE PATIENT
ADD CONSTRAINT PATIENT_NUTRITIONIST_FK FOREIGN KEY (NutritionistEmail)
REFERENCES NUTRITIONIST(Email);

-- fk #12 --
ALTER TABLE MEASUREMENT
ADD CONSTRAINT MEASUREMENT_PATIENT_FK FOREIGN KEY (PatientEmail)
REFERENCES PATIENT(Email);

-- fk #13 --
ALTER TABLE RECIPE_HAS
ADD CONSTRAINT RECIPE_HAS_RECIPE_FK FOREIGN KEY (RecipeNumber)
REFERENCES RECIPE(Number);

-- fk #14 --
ALTER TABLE RECIPE_HAS
ADD CONSTRAINT RECIPE_HAS_PRODUCT_FK FOREIGN KEY (ProductBarcode)
REFERENCES PRODUCT(Barcode);

-- fk #15 --
ALTER TABLE HAS_VITAMIN
ADD CONSTRAINT HAS_VITAMIN_PRODUCT_FK FOREIGN KEY (ProductBarcode)
REFERENCES PRODUCT(Barcode);

--fk #16 --
ALTER TABLE HAS_VITAMIN
ADD CONSTRAINT HAS_VITAMIN_VITAMIN_FK FOREIGN KEY (VitaminCode)
REFERENCES VITAMIN(Code);


