
-- Create Views --
/*
Description: this view joins the tables needed to get the products of an
specific plan

Used : PR.5 and PR.6
*/
CREATE VIEW PLAN_PRODUCT_VIEW
AS SELECT DP.Number, P.Barcode, P.Name, P.Description, PH.Servings, PH.Mealtime
FROM (DAILY_PLAN AS DP JOIN PLAN_HAS AS PH ON  DP.Number = PH.PlanNumber) JOIN PRODUCT AS P ON PH.ProductBarcode = P.Barcode

GO

/*
Description: this view joins the tables needed to get the products consumed
by an specific patient.

Used in: PR.8
*/
CREATE VIEW PATIENT_PRODUCTS
AS SELECT PA.Email, PR.Barcode, PR.Name, CP.Day, CP.Meal
FROM 
	(PATIENT AS PA JOIN CONSUMES_PRODUCT AS CP ON PA.Email = CP.PatientEmail) 
	JOIN PRODUCT AS PR ON CP.ProductBarcode =PR.Barcode;

GO

/*
Description: this view joins the tables needed to get the recipes consumed
by an specific patient.

Used in: PR.8
*/
CREATE VIEW PATIENT_RECIPES
AS SELECT PA.Email, RE.Number, RE.Name, CR.Day, CR.Meal
FROM 
	(PATIENT AS PA JOIN CONSUMES_RECIPE AS CR ON PA.Email = CR.PatientEmail) 
	JOIN RECIPE AS RE ON CR.RecipeNumber = RE.Number;


GO
-- Create functions --

/*
Description: this function gets the amount of patients related
to a nutritionist.

Input: @email corresponds to the nutritionist email that filters
the funtion
*/
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
/*
Description: this function determines the respective
discount each nutritionist has based on the type of charge

Input: @type is the type of charge of a respective
nutritionist.
*/
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
-- Create procedues -- 

-- procedure #1 --

/*
Description: This procedure is used to generate the information of the reports

Input: @type corresponds to the charge type of the nutritionist (weekly, monthly or anual)
	if @type is null it doesn't filter
*/
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
-- triggers  --- 

-- trigger 1 --

/*
Description: this trigger disallows the the insertion or deletion of an
administrator unless the trigger gets disabled.
*/
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




