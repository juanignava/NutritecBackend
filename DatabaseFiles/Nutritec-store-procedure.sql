
-- Create Views --
/*
Description: this view joins the tables needed to get the products of an
specific plan

Used in: PR.5 and PR.6
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
