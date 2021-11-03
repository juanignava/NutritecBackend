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
	
	SELECT 
		DISTINCT N.Email,
		N.FirstName,
		N.LastName1,
		N.LastName2,
		N.CreditCardNumber,
		dbo.func_getPatients(N.Email) AS Payment,
		dbo.func_getPatients(N.Email)*dbo.discount(N.ChargeType) AS Discount,
		dbo.func_getPatients(N.Email)-dbo.func_getPatients(N.Email)*dbo.discount(N.ChargeType) AS Amount
	FROM (NUTRITIONIST AS N JOIN PATIENT AS P ON N.Email = P.NutritionistEmail)
	WHERE (@type IS NULL OR N.ChargeType = @type);

END

-- procedure #2 --

/*
Description: This procedure is used to generate the nutritional information
of the plan

Input: @number corresponds to the number of the plan in analysis
*/

CREATE PROCEDURE uspPlanDetails(
	@number INT -- plan number
)
AS
BEGIN

	SELECT
		ROUND(SUM(P.Sodium*PH.Servings), 2) AS TotalSodium,
		ROUND(SUM(P.Carbohydrates*PH.Servings), 2) AS TotalCarbohydrates,
		ROUND(SUM(P.Protein*PH.Servings), 2) AS TotalProtein,
		ROUND(SUM(P.Fat*PH.Servings), 2) AS TotalFat,
		ROUND(SUM(P.Iron*PH.Servings), 2) AS TotalIron,
		ROUND(SUM(P.Calcium*PH.Servings), 2) AS TotalCalcium,
		ROUND(SUM(P.Calories*PH.Servings), 2) AS TotalCalories
	FROM ((DAILY_PLAN AS DP JOIN PLAN_HAS AS PH ON DP.Number = PH.PlanNumber) JOIN PRODUCT AS P ON PH.ProductBarcode = P.Barcode)
	WHERE DP.Number = @number;

END

