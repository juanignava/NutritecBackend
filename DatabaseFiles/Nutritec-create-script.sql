-- TABLES --

-- Table #1 Measurement --
DROP TABLE IF EXISTS MEASUREMENT;
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

-- Table #2 Has_vitamin -- 
DROP TABLE IF EXISTS HAS_VITAMIN;
CREATE TABLE HAS_VITAMIN
(
	ProductBarcode		INT NOT NULL,
	VitaminCode			VARCHAR(10) NOT NULL,

	PRIMARY KEY(ProductBarcode, VitaminCode)
);

-- Table #3 Vitamin --
DROP TABLE IF EXISTS VITAMIN;
CREATE TABLE VITAMIN
(
	Code				VARCHAR(10) NOT NULL,
	Name				VARCHAR(100) NOT NULL,

	PRIMARY KEY(Code)
);

-- Table #4 Follows --
DROP TABLE IF EXISTS FOLLOWS;
CREATE TABLE FOLLOWS
(
	PatientEmail		VARCHAR(100) NOT NULL,
	PlanNumber			INT NOT NULL,
	Month				VARCHAR(100) NOT NULL,

	PRIMARY KEY (PatientEmail, PlanNumber)
);

-- Table #5 Plan_has --
DROP TABLE IF EXISTS PLAN_HAS;
CREATE TABLE PLAN_HAS
(
	PlanNumber			INT NOT NULL,
	ProductBarcode		INT NOT NULL,
	Mealtime			VARCHAR(100) NOT NULL,
	Servings			INT NOT NULL,

	PRIMARY KEY(PlanNumber, ProductBarcode, Mealtime)
);

-- Table #6 Daily plan --
DROP TABLE IF EXISTS DAILY_PLAN;
CREATE TABLE DAILY_PLAN
(
	Number				INT NOT NULL,
	Name				VARCHAR(100) NOT NULL, 
	NutritionistEmail	VARCHAR(100) NOT NULL,

	PRIMARY KEY(Number)
);

-- Table #7 Consumes_product -- 
DROP TABLE IF EXISTS CONSUMES_PRODUCT;
CREATE TABLE CONSUMES_PRODUCT
(
	ProductBarcode		INT NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,
	Day					VARCHAR(100) NOT NULL,
	Meal				VARCHAR(100) NOT NULL,
	Servings			INT NOT NULL,

	PRIMARY KEY (ProductBarcode, PatientEmail, Day, Meal)
);

-- Table #8 Consumes_recipe --
DROP TABLE IF EXISTS CONSUMES_RECIPE;
CREATE TABLE CONSUMES_RECIPE
(
	RecipeNumber		INT NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,
	Day					VARCHAR(100) NOT NULL,
	Meal				VARCHAR(100) NOT NULL,

	PRIMARY KEY (RecipeNumber, PatientEmail, Day, Meal)
);

-- Table #9 Recipe_has --
DROP TABLE IF EXISTS RECIPE_HAS;
CREATE TABLE RECIPE_HAS
(
	RecipeNumber		INT NOT NULL,
	ProductBarcode		INT NOT NULL,
	Servings			INT NOT NULL,

	PRIMARY KEY (RecipeNumber, ProductBarcode)
);

-- Table #10 Recipe --
DROP TABLE IF EXISTS RECIPE;
CREATE TABLE RECIPE
(
	Number				INT NOT NULL,
	Name				VARCHAR(100) NOT NULL,
	PatientEmail		VARCHAR(100) NOT NULL,

	PRIMARY KEY(Number)
);

-- Table #11 Patient --
DROP TABLE IF EXISTS PATIENT;
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

-- Table #12 Nutritionist -- 
DROP TABLE IF EXISTS NUTRITIONIST;
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

-- Table #13 Prduct -- 
DROP TABLE IF EXISTS PRODUCT;
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

-- Table #14 Prduct -- 
DROP TABLE IF EXISTS ADMIN;
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

-- FUNCTIONS  --

/*
Description: this function determines the respective
discount each nutritionist has based on the type of charge

Input: @type is the type of charge of a respective
nutritionist.
*/
DROP FUNCTION IF EXISTS discount;
GO
CREATE FUNCTION discount(
	@type AS VARCHAR(100)
)
RETURNS FLOAT(2) AS
BEGIN
		DECLARE @dis FLOAT(2);

		IF (@type = 'anual')
		BEGIN
			SET @dis = 0.1;
		END

		IF @type = 'monthly'
		BEGIN
			SET @dis = 0.05;
		END

		IF @type = 'weekly'
		BEGIN
			SET @dis = 0;
		END

		RETURN @dis;
			
END
GO

/*
Description: this function gets the amount of patients related
to a nutritionist.

Input: @email corresponds to the nutritionist email that filters
the funtion
*/
DROP FUNCTION IF EXISTS func_getPatients;
GO
CREATE FUNCTION func_getPatients(
	@email AS VARCHAR(100)
)
RETURNS INT AS
BEGIN
		DECLARE @countPatient INT;
		SELECT @countPatient = COUNT(*)
		FROM PATIENT
		WHERE NutritionistEmail = @email;

		RETURN @countPatient;
END
GO

-- PROCEDURES --

-- procedure #1 --
/*
Description: This procedure is used to generate the information of the reports

Input: @type corresponds to the charge type of the nutritionist (weekly, monthly or anual)
	if @type is null it doesn't filter
*/
DROP PROCEDURE IF EXISTS uspNutritionistReport
GO
CREATE PROCEDURE uspNutritionistReport(
	@type VARCHAR(100) = NULL
)
AS
BEGIN
	
	-- temporal table with the needed values per nutritionist --
	CREATE TABLE #TEMP_CHARGES
	(
		NutritionistEmail		VARCHAR(100),
		ClientAmount			INT,
		Discount				FLOAT(2)
	);

	-- insert the values per nutritionist --
	INSERT INTO #TEMP_CHARGES (NutritionistEmail, ClientAmount, Discount)
		SELECT
			DISTINCT Email,
			dbo.func_getPatients(Email) AS ClientAmount,
			dbo.discount(ChargeType) AS Discount
		FROM NUTRITIONIST
		WHERE (@type IS NULL OR ChargeType = @type);

	-- response query with the requires data --
	SELECT 
		DISTINCT N.Email,
		N.FirstName,
		N.LastName1,
		N.LastName2,
		N.CreditCardNumber,
		TP.ClientAmount AS Payment,
		TP.ClientAmount * TP.Discount AS Discount,
		TP.ClientAmount - TP.ClientAmount * TP.Discount AS Amount
	FROM (NUTRITIONIST AS N JOIN #TEMP_CHARGES AS TP ON N.Email = TP.NutritionistEmail);

	-- drop the temporal table --
	IF(OBJECT_ID('tempdb..#TEMP_CHARGES') IS NOT NULL)
	BEGIN
		DROP TABLE #TEMP_CHARGES
	END
END
GO

-- procedure #2 --
/*
Description: This procedure is used to generate the nutritional information
of the plan

Input: @number corresponds to the number of the plan in analysis
*/

DROP PROCEDURE IF EXISTS uspPlanDetails;
GO
CREATE PROCEDURE uspPlanDetails(
	@number INT = NULL-- plan number
)
AS
BEGIN

	-- table that calculates the total values per plan --
	CREATE TABLE #TEMP_PRODUCTS_DATA
	(
		PlanNumber			INT,
		TotalSodium			FLOAT(2),
		TotalCarbohydrates	FLOAT(2),
		TotalProtein		FLOAT(2),
		TotalFat			FLOAT(2),
		TotalIron			FLOAT(2),
		TotalCalcium		FLOAT(2),
		TotalCalories		FLOAT(2)
	);

	-- insert the result of the calculations --
	INSERT INTO #TEMP_PRODUCTS_DATA
		SELECT
			PH.PlanNumber AS Number,
			SUM(P.Sodium*PH.Servings),
			SUM(P.Carbohydrates*PH.Servings),
			SUM(P.Protein*PH.Servings),
			SUM(P.Fat*PH.Servings),
			SUM(P.Iron*PH.Servings),
			SUM(P.Calcium*PH.Servings),
			SUM(P.Calories*PH.Servings)
		FROM PLAN_HAS AS PH JOIN PRODUCT AS P ON PH.ProductBarcode = P.Barcode
		
		GROUP BY PH.PlanNumber;

	-- add the plan number -- (this considers the plans that have no products yet)
	SELECT DP.Number, TPD.* INTO #TEMP_RESULT
	FROM DAILY_PLAN AS DP LEFT OUTER JOIN #TEMP_PRODUCTS_DATA AS TPD 
		ON DP.Number = TPD.PlanNumber
	WHERE (@number IS NULL OR DP.Number = @number);
	
	ALTER TABLE #TEMP_RESULT DROP COLUMN PlanNumber;

	-- response request --
	SELECT * FROM #TEMP_RESULT;

	-- drop temp tables --
	DROP TABLE #TEMP_RESULT;
	DROP TABLE #TEMP_PRODUCTS_DATA

END
GO

-- procedure #3 --
/*
Description: This procedure is used to get the products that are
NOT consumed yet by a user for a patient at a specific time

Input: @email is the email of the patient
	   @day and @time specify the time of consumption.
*/
DROP PROCEDURE IF EXISTS uspProductsNotConsumed;
GO
CREATE PROCEDURE uspProductsNotConsumed(
	@email	AS VARCHAR(100),
	@day	AS VARCHAR(100),
	@meal	AS VARCHAR(100)
)
AS
BEGIN
	
	CREATE TABLE #TEMP_PRODUCT
	(
		Barcode		INT,
		Name		VARCHAR(100),
		Description VARCHAR(200)
	);

	INSERT INTO #TEMP_PRODUCT
		SELECT PR.Barcode, PR.Name, PR.Description
		FROM
			CONSUMES_PRODUCT AS CP
			JOIN PRODUCT AS PR ON CP.ProductBarcode =PR.Barcode
		WHERE
			CP.PatientEmail = @email AND
			CP.Day = @day AND
			CP.Meal = @meal;

	SELECT Barcode, Name, Description
	FROM PRODUCT
	EXCEPT
	SELECT * FROM #TEMP_PRODUCT;
	
	DROP TABLE #TEMP_PRODUCT;

END
GO

-- procedure #4 --
/*
Description: This procedure is used to get the recipes that are
NOT consumed yet by a user for a patient at a specific time

Input: @email is the email of the patient
	   @day and @time specify the time of consumption.
*/
DROP PROCEDURE IF EXISTS uspRecipesNotConsumed;
GO
CREATE PROCEDURE uspRecipesNotConsumed(
	@email	AS VARCHAR(100),
	@day	AS VARCHAR(100),
	@meal	AS VARCHAR(100)
)
AS
BEGIN
	
	CREATE TABLE #TEMP_RECIPE
	(
		Number		INT,
		Name		VARCHAR(100),
	);

	INSERT INTO #TEMP_RECIPE
		SELECT RE.Number, RE.Name
		FROM
			CONSUMES_RECIPE AS CR
			JOIN RECIPE AS RE ON CR.RecipeNumber = RE.Number
		WHERE
			CR.PatientEmail = @email AND
			CR.Day = @day AND
			CR.Meal = @meal;

	SELECT Number, Name
	FROM RECIPE
	EXCEPT
	SELECT * FROM #TEMP_RECIPE;
	
	DROP TABLE #TEMP_RECIPE;

END
GO

-- VIEWS --

-- view #1 --
/*
Description: this view joins the tables needed to get the products of an
specific plan

Used : PR.5 and PR.6
*/
DROP VIEW IF EXISTS PLAN_PRODUCT_VIEW;
GO
CREATE VIEW PLAN_PRODUCT_VIEW
AS SELECT DP.Number, P.Barcode, P.Name, P.Description, PH.Servings, PH.Mealtime
FROM (DAILY_PLAN AS DP JOIN PLAN_HAS AS PH ON  DP.Number = PH.PlanNumber) JOIN PRODUCT AS P ON PH.ProductBarcode = P.Barcode
GO

-- view #2 --
/*
Description: this view joins the tables needed to get the products consumed
by an specific patient.

Used in: PR.8
*/
DROP VIEW IF EXISTS PATIENT_PRODUCTS;
GO
CREATE VIEW PATIENT_PRODUCTS
AS SELECT PA.Email, PR.Barcode, PR.Name, CP.Day, CP.Meal
FROM 
	(PATIENT AS PA JOIN CONSUMES_PRODUCT AS CP ON PA.Email = CP.PatientEmail) 
	JOIN PRODUCT AS PR ON CP.ProductBarcode =PR.Barcode;
GO

-- view #3 --
/*
Description: this view joins the tables needed to get the recipes consumed
by an specific patient.

Used in: PR.8
*/
DROP VIEW IF EXISTS PATIENT_RECIPES;
GO
CREATE VIEW PATIENT_RECIPES
AS SELECT PA.Email, RE.Number, RE.Name, CR.Day, CR.Meal
FROM 
	(PATIENT AS PA JOIN CONSUMES_RECIPE AS CR ON PA.Email = CR.PatientEmail) 
	JOIN RECIPE AS RE ON CR.RecipeNumber = RE.Number;
GO
-- TRIGGERS --

-- trigger 1 --
/*
Description: this trigger disallows the the insertion or deletion of an
administrator unless the trigger gets disabled.
*/
DROP TRIGGER IF EXISTS admin_security;
GO
CREATE TRIGGER admin_security
ON ADMIN
FOR INSERT, DELETE 
AS	
	SET NOCOUNT ON;
	PRINT 'You must disable admin_security trigger before adding or deleting an admin'
	ROLLBACK;
GO
--- trigger 2 --
/*
Description: this trigger validates that the patient email has a correct email format
and also verifies the password is MD5 encrypted by confirming its lenght. 
*/
DROP TRIGGER IF EXISTS patient_validation;
GO
CREATE TRIGGER patient_validation
ON PATIENT
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE
		@email AS VARCHAR(100),
		@password AS VARCHAR(100);
	SELECT
		@email = Email,
		@password = Passowrd
	FROM inserted

	IF @email Like '%@%.com'
	BEGIN
		IF LEN(@password) = 32
		PRINT('Patient inserted correctly');

		ELSE
		BEGIN
			DELETE FROM PATIENT
			WHERE Email = @email;
			
			PRINT ('Could not insert patient');
			PRINT ('Patient password must be encrypted with MD5');
		END
	END
	ELSE
	BEGIN
		DELETE FROM PATIENT
		WHERE Email = @email;

		PRINT ('Could not insert patient');
		PRINT ('Make sure the patient email has the correct sintax');
	END

END
GO

--- trigger 3 --
/*
Description: this trigger validates that the admin email has a correct email format
and also verifies the password is MD5 encrypted by confirming its lenght. 
*/
DROP TRIGGER IF EXISTS admin_validation;
GO
CREATE TRIGGER admin_validation
ON ADMIN
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE
		@email AS VARCHAR(100),
		@password AS VARCHAR(100);
	SELECT
		@email = Email,
		@password = Password
	FROM inserted

	IF @email Like '%@%.com' OR @email Like '%@%.cr'
	BEGIN
		IF LEN(@password) = 32
		PRINT('Administrator inserted correctly');

		ELSE
		BEGIN
			DELETE FROM ADMIN
			WHERE Email = @email;
			
			PRINT ('Could not insert administrator');
			PRINT ('Administrator password must be encrypted with MD5');
		END
	END
	ELSE
	BEGIN
		DELETE FROM ADMIN
		WHERE Email = @email;

		PRINT ('Could not insert administrator');
		PRINT ('Make sure the administrator email has the correct sintax');
	END

END
GO

--- trigger 4 --
/*
Description: this trigger validates that the nutritionist email has a correct email format
and also verifies the password is MD5 encrypted by confirming its lenght. 
*/
DROP TRIGGER IF EXISTS nutritionist_validation;
GO
CREATE TRIGGER nutritionist_validation
ON NUTRITIONIST
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE
		@email AS VARCHAR(100),
		@password AS VARCHAR(100);
	SELECT
		@email = Email,
		@password = Password
	FROM inserted

	IF @email Like '%@%.com' OR @email Like '%@%.cr'
	BEGIN
		IF LEN(@password) = 32
		PRINT('Nutritionist inserted correctly');

		ELSE
		BEGIN
			DELETE FROM NUTRITIONIST
			WHERE Email = @email;
			
			PRINT ('Could not insert nutritionist');
			PRINT ('Nutritionist password must be encrypted with MD5');
		END
	END
	ELSE
	BEGIN
		DELETE FROM NUTRITIONIST
		WHERE Email = @email;

		PRINT ('Could not insert nutritionist');
		PRINT ('Make sure the nutritionist email has the correct sintax');
	END

END
