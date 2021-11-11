# Nutritec Requests

The base of the url for these requests in the api is `https://nutritecrg.azurewebsites.net`

## Requests ADMIN

### AD.1

Type: GET

Description: get a single admin based on its email or username

Url: `/api/Admin/login/{credential}` where `credential` is the email or username.

Json: You get a Json like this one

```Json
{
  "email": "admin@nutritec.cr",
  "username": "admin",
  "password": "81dc9bdb52d04dc20036dbd8313ed055"
}
```

Observations: Resturned password is encrypted. All the initial population script admins have '1234' as password. 

## Requests NUTRITIONIST

### NU.1

Type: GET

Description: get a single nutritionist based on its email or username

Url: `/api/Nutritionist/login/{credential}` where `credential` is the email or username.

Json: You get a Json like this one

```Json
{
  "email": "ju.navarro@gmail.com",
  "username": "juanignava",
  "nutritionistCode": 10000,
  "id": 118180814,
  "active": 1,
  "firstName": "Juan",
  "lastName1": "Navarro",
  "lastName2": "Navarro",
  "birthDate": "2001-02-08T00:00:00",
  "password": "passwordnacho",
  "chargeType": "weekly",
  "weight": 60,
  "height": 1.7,
  "creditCardNumber": 123456789,
  "country": "Costa Rica",
  "province": "Cartago",
  "canton": "Cartago",
  "prictureUrl": null,
  "dailyPlans": [],
  "patients": []
}
```

Observations: Resturned password is encrypted. All the initial population script nutritionists have '1234' as password. 

### NU.2

Type: POST

Description: Post a new nutritionist.

Url: `/api/Nutritionist`

Json: You have to share a Json like this one

```Json
{
  "email": "ju.navarro@gmail.com",
  "username": "juanignava",
  "nutritionistCode": 10000,
  "id": 118180814,
  "firstName": "Juan",
  "lastName1": "Navarro",
  "lastName2": "Navarro",
  "birthDate": "2001-02-02",
  "password": "1234",
  "chargeType": "monthly",
  "weight": 70,
  "height": 1.7,
  "creditCardNumber": 123456789,
  "country": "Costa Rica",
  "province": "Cartago",
  "canton": "Cartago",
  "prictureUrl": null
}
```

Observations: All details have to be different than null (except to picture Url). Consider that email, username, nutritionistCode and creditCArdNumber are unique values (must not repeat with the values of other nustritionists). Dates must have the format "YY-MM-DD" (for example 2020-08-02 for August second 2020).

### NU.3

Type: GET

Description: Get the payment report of the nutritionists based on their charge type.

Url: `/api/Nutritionist/report/{chargeType}` where `chargeType` represents the chargeType we want to filter. The accepted values of `chargeType` are `weekly`, `anual` and `monthly`, for a complete report (without filters) use an empty string `""`.

Json: You have to share a Json like this one

```Json
[
  {
    "email": "an.rodriguez@gmail.com",
    "firstName": "Ana",
    "lastName1": "Rodriguez",
    "lastName2": "Quesada",
    "creditCardNumber": 564897123,
    "payment": 1,
    "discount": 0.05,
    "amount": 0.95
  },
  {
    "email": "ju.navarro@gmail.com",
    "firstName": "Juan",
    "lastName1": "Navarro",
    "lastName2": "Navarro",
    "creditCardNumber": 123456789,
    "payment": 2,
    "discount": 0,
    "amount": 2
  },
  {
    "email": "sa.salazar@outlook.com",
    "firstName": "Samuel",
    "lastName1": "Salazar",
    "lastName2": "Carvajal",
    "creditCardNumber": 231658899,
    "payment": 0,
    "discount": 0,
    "amount": 0
  }
]
```

Observations: The accepted values of `chargeType` are `weekly`, `anual` and `monthly`, for a complete report (without filters) use an empty string `""`. The empty string is used by the complete report pdf and the filters are used to display the information in the web application.

## Requests PATIENT

### PA.1

Type: GET

Description: get a single patient based on its email or username

Url: `​/api​/Patient​/login​/{credential}` where `credential` is the email or username.

Json: You get a Json like this one

```Json
{
  "email": "lu.morales@gmail.com",
  "username": "luismorales",
  "firstName": "Luis",
  "lastName1": "Morales",
  "lastName2": "Rodriguez",
  "birthDate": "1999-05-30T00:00:00",
  "passowrd": "passwordLuis",
  "nutritionistEmail": "ju.navarro@gmail.com",
  "nutritionistEmailNavigation": null,
  "consumesProducts": [],
  "consumesRecipes": [],
  "follows": [],
  "measurements": [],
  "recipes": []
}
```
Observations: Resturned password is encrypted. All the initial population script patients have '1234' as password.

### PA.2

Type: POST

Description: Post a new patient.

Url: `/api/Patient`

Json: You have to share a Json like this one

```Json
{
  "email": "patient@email.com",
  "username": "patientUsername",
  "firstName": "patientName",
  "lastName1": "patientLastname",
  "lastName2": "patientLastname2",
  "birthDate": "2003-02-03",
  "passowrd": "1234"
}
```

Observations: All details have to be different than null. Consider that email and username. Dates must have the format "YY-MM-DD" (for example 2020-08-02 for August second 2020).

### PA.3

Type: GET

Description: Get all not associated clients filtered by a portion of text of their email.

Url: `/api/Patient/unassociated/{emailtext}` where `emailText` corresponds to a portion of the email of the searched patients.

Json: You will get a Json like this (using `emailText` = 'n') look that all of their emails contain an 'n'

```Json
[
  {
    "email": "jo.granados@gmail.com",
    "username": "nachogranados",
    "firstName": "Jose",
    "lastName1": "Granados",
    "lastName2": "Marin",
    "birthDate": "2000-07-09T00:00:00",
    "passowrd": "passwordnacho",
    "nutritionistEmail": null,
    "nutritionistEmailNavigation": null,
    "consumesProducts": [],
    "consumesRecipes": [],
    "follows": [],
    "measurements": [],
    "recipes": []
  },
  {
    "email": "patient@email.com",
    "username": "patientUsername",
    "firstName": "patientName",
    "lastName1": "patientLastname",
    "lastName2": "patientLastname2",
    "birthDate": "2003-02-03T00:00:00",
    "passowrd": "81dc9bdb52d04dc20036dbd8313ed055",
    "nutritionistEmail": null,
    "nutritionistEmailNavigation": null,
    "consumesProducts": [],
    "consumesRecipes": [],
    "follows": [],
    "measurements": [],
    "recipes": []
  }
]
```

Observations: In order to use the "Search All" button filter by an empty string ""

### PA.4

Type: GET

Description: Get all associated clients to an specific nutritionist

Url: `/api/Patient/associated/{nutritionistemail}` where `nutritionistemail` corresponds to the email of the nutritionist.

Json: You will get a Json like this

```Json
[
  {
    "email": "lu.morales@gmail.com",
    "username": "luismorales",
    "firstName": "Luis",
    "lastName1": "Morales",
    "lastName2": "Rodriguez",
    "birthDate": "1999-05-30T00:00:00",
    "passowrd": "passwordLuis",
    "nutritionistEmail": "ju.navarro@gmail.com",
    "nutritionistEmailNavigation": null,
    "consumesProducts": [],
    "consumesRecipes": [],
    "follows": [],
    "measurements": [],
    "recipes": []
  },
  {
    "email": "mo.waterhouse@gmail.com",
    "username": "moniwaterhouse",
    "firstName": "Monica",
    "lastName1": "Waterhouse",
    "lastName2": "Montoya",
    "birthDate": "1999-07-08T00:00:00",
    "passowrd": "passwordmoni",
    "nutritionistEmail": "ju.navarro@gmail.com",
    "nutritionistEmailNavigation": null,
    "consumesProducts": [],
    "consumesRecipes": [],
    "follows": [],
    "measurements": [],
    "recipes": []
  }
]
```
### PA.5

Type: GET

Description: Get all associated clients to a nutritionist filtered by a portion of text of their email.

Url: `/api/Patient/associated/{nutritionistEmail}/{emailtext}` where `emailText` corresponds to a portion of the email of the searched patients.

Json: You will get a Json like this (using `emailText` = 'wa').

```Json
[
  {
    "email": "mo.waterhouse@gmail.com",
    "username": "moniwaterhouse",
    "firstName": "Monica",
    "lastName1": "Waterhouse",
    "lastName2": "Montoya",
    "birthDate": "1999-07-08T00:00:00",
    "passowrd": "passwordmoni",
    "nutritionistEmail": "ju.navarro@gmail.com",
    "nutritionistEmailNavigation": null,
    "consumesProducts": [],
    "consumesRecipes": [],
    "follows": [],
    "measurements": [],
    "recipes": []
  }
]
```

Observations: In order to use the "Search All" button filter by an empty string ""


## Requests PRODUCT

### PR.1

Type: GET

Description: get all products based on its approval state

Url: `/api/Product/state/{state}` where `state` is the approval state we want to filter on. The avalable states are `Pending`, `Approved` and `Declined`.

Json: You get a Json like this one (if you filter by `Pending`)

```Json
[
  {
    "barcode": 1004,
    "approved": "Pending",
    "name": "Coffee",
    "description": "The drink without milk or sugar",
    "sodium": 0,
    "carbohydrates": 0.7,
    "protein": 1.19,
    "fat": 0.15,
    "iron": 0,
    "calcium": 0,
    "calories": 200,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  },
  {
    "barcode": 1007,
    "approved": "Pending",
    "name": "Honey",
    "description": "100 g of Bee honey",
    "sodium": 4,
    "carbohydrates": 76.4,
    "protein": 0.4,
    "fat": 0,
    "iron": 0.4,
    "calcium": 6,
    "calories": 288,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  }
]
```

Observations: Consider that the valid approval states are `Pending`, `Approved` and `Declined`.

### PR.2

Type: GET

Description: get a single product based on its barcode

Url: `​/api/Product/{barcode}` where `barcode` is the barcode of the product. The avalable states are `Pending`, `Approved` and `Declined`.

Json: You get a Json like this one 

```Json
{
  "barcode": 1000,
  "approved": "Approved",
  "name": "Rice",
  "description": "A serving of white rice salt",
  "sodium": 6,
  "carbohydrates": 73,
  "protein": 8,
  "fat": 3,
  "iron": 2,
  "calcium": 20,
  "calories": 332,
  "consumesProducts": [],
  "hasVitamins": [],
  "planHas": [],
  "recipeHas": []
}
```
Observations: --

### PR.3

Type: PUT

Description: Update the approval state of a product.

Url: `​/api/Product/Approved/{barcode}/{state}` where `barcode` is the barcode of the product to update and `state` is the new state the product will have. The avalable states are `Pending`, `Approved` and `Declined`.

Json: This request doesn't need or return a Json

Observations: Consider that the avalable states are `Pending`, `Approved` and `Declined`.

### PR.4

Type: POST

Description: Post a product

Url: `/api/Product`.

Json: You need a Json like this one 

```Json
{
  "name": "Watermelon",
  "description": "A unit of this fruit",
  "sodium": 1,
  "carbohydrates": 26.9,
  "protein": 1.29,
  "fat": 0.39,
  "iron": 1.5,
  "calcium": 15,
  "calories": 105
}
```

Observations: There is no need to specify the product barcode, the backend ads a convenient number by itself. Similar situation with the approved value, all products start with `Pending`. Also make sure all data is added because of the NOT NULL constraints in the database.

### PR.5

Type: GET

Description: Get the products related to a plan and meal.

Url: `/api/Product/byplan/{planNumber}/{mealtime}` where `planNumber` is the number of the plan to filter and `mealtime` corresponds to the meal we are filtering the products. The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.

Json: You will receive a Json like this one.

```Json
[
  {
    "number": 1,
    "barcode": 1002,
    "name": "Banana",
    "description": "A unit of this fruit",
    "servings": 1,
    "mealtime": "Breakfast"
  },
  {
    "number": 1,
    "barcode": 1005,
    "name": "Apple",
    "description": "A unit of this fruit",
    "servings": 2,
    "mealtime": "Breakfast"
  },
  {
    "number": 1,
    "barcode": 1006,
    "name": "Bread",
    "description": "100 mg of normal bread",
    "servings": 1,
    "mealtime": "Breakfast"
  }
]
```

Observations: Notice that you receive just some of the deatils of a product (nutritional values are not included). The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.

### PR.6

Type: GET

Description: Get the products that are not related to a plan and meal.

Url: `/api/Product/notinplan/{planNumber}/{mealtime}` where `planNumber` is the number of the plan to filter and `mealtime` corresponds to the meal we are filtering the products. The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.

Json: You will receive a Json like this one.

```Json
[
  {
    "barcode": 1000,
    "approved": "Approved",
    "name": "Rice",
    "description": "A serving of white rice salt",
    "sodium": 6,
    "carbohydrates": 73,
    "protein": 8,
    "fat": 3,
    "iron": 2,
    "calcium": 20,
    "calories": 332,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  },
  {
    "barcode": 1001,
    "approved": "Approved",
    "name": "Beans",
    "description": "A serving black beans",
    "sodium": 252,
    "carbohydrates": 13.3,
    "protein": 4,
    "fat": 0,
    "iron": 1.6,
    "calcium": 28,
    "calories": 71,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  },
  {
    "barcode": 1003,
    "approved": "Approved",
    "name": "Beef",
    "description": "100 mg steak without salt",
    "sodium": 384,
    "carbohydrates": 0,
    "protein": 26.33,
    "fat": 19.5,
    "iron": 2.6,
    "calcium": 18,
    "calories": 288,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  }
]
```

Observations: Notice that you receive just some of the deatils of a product (nutritional values are not included). The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'lunch', 'Afternoon snack', 'diner'.

### PR.7

Type: POST

Description: Adds a new product into the plan.

Url: `/api/Product/newproductinplan/{planNumber}/{productBarcode}/{mealtime}/{servings}` where `planNumber` is the number of the plan, `productBarcode` is the barcode of the chosen product, `mealtime` corresponds to the time in the plan and `servings` is the amount of times the product has to be added. 

Json: You don't need send a Json in this request.

Observations: The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.

### PR.8

Type: GET

Description: Get the products and recipes the patient consumed at a specific day and meal.

Url: `/api/Product/consumption/{patientEmail}/{day}/{meal}` where `patientEmail` is the chosen patient `day` and `meal` filter the products/recipes.

Json: You will receive a Json like this one.

```Json
[
  {
    "email": "lu.morales@gmail.com",
    "number": 1000,
    "name": "Rice",
    "day": "Monday",
    "meal": "Breakfast"
  }
]
```

Observations: `day` need to have the value of ('Monday', 'Tuesday', 'Wednesday', 'Thursday' or 'Friday'). The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.

### PR.9

Type: POST

Description: Adds a product to the consumption tamble of a specific patient.

Url: `/api/Product/consumption/addproduct/{barcode}/{patientEmail}/{day}/{meal}/{servings}` where `barcode` is the barcode of the product to add, `patientEmail` is the email of the patient that consumed the product, `day` and `meal` correspond to the time of consumption and `servings` is the amount of product consumed.

Json: There is no Json in this request

Observations: `day` need to have the value of ('Monday', 'Tuesday', 'Wednesday', 'Thursday' or 'Friday'). The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.


## Requests RECIPE

### RE.1

Type: GET

Description: Get all the recipes done by a patient

Url: `/api/Recipe/getbypatient/{patientEmail}` where `patientEmail` is the logged patient.

Json: You will receive a Json like this one.

```Json
[
  {
    "number": 1,
    "name": "Pinto",
    "patientEmail": "lu.morales@gmail.com",
    "patientEmailNavigation": null,
    "consumesRecipes": [],
    "recipeHas": []
  },
  {
    "number": 6,
    "name": "Beef and beans",
    "patientEmail": "lu.morales@gmail.com",
    "patientEmailNavigation": null,
    "consumesRecipes": [],
    "recipeHas": []
  }
]
```

Observations: --

### RE.2

Type: POST

Description: Add a recipe to the consumption table

Url: `/api/Recipe/consumption/addrecipe/{number}/{patientEmail}/{day}/{meal}` where `patientEmail` is the logged patient, `number` is the number of the recipe to add and `day` and `meal` are the time of consumption.

Json: This request doesn't have Json.

Observations: `day` need to have the value of ('Monday', 'Tuesday', 'Wednesday', 'Thursday' or 'Friday'). The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.

### RE.3

Type: POST

Description: Post a recipe

Url: `/api/Recipe/postrecipe/{name}/{patientEmail}` where `patientEmail` is the logged patient, `name` is the name of the recipe to add.

Json: This request doesn't have Json.

Observations: There is no need to specify the recipe number, the backend does that.

### RE.4

Type: GET

Description: Get all the products in a recipe

Url: `/api/Recipe/getproductsin/{number}` where `number` is the number of the recipe.

Json: You will receive a Json like this one.

```Json
[
  {
    "barcode": 1000,
    "approved": "Approved",
    "name": "Rice",
    "description": "A serving of white rice salt",
    "sodium": 6,
    "carbohydrates": 73,
    "protein": 8,
    "fat": 3,
    "iron": 2,
    "calcium": 20,
    "calories": 332,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  },
  {
    "barcode": 1001,
    "approved": "Approved",
    "name": "Beans",
    "description": "A serving black beans",
    "sodium": 252,
    "carbohydrates": 13.3,
    "protein": 4,
    "fat": 0,
    "iron": 1.6,
    "calcium": 28,
    "calories": 71,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  }
]
```

Observations: --

### RE.5

Type: GET

Description: Get all the products that are not in a specific recipe

Url: `/api/Recipe/getproductsnotin/{number}` where `number` is the number of the recipe.

Json: You will receive a Json like this one.

```Json
[
  {
    "barcode": 1000,
    "approved": "Approved",
    "name": "Rice",
    "description": "A serving of white rice salt",
    "sodium": 6,
    "carbohydrates": 73,
    "protein": 8,
    "fat": 3,
    "iron": 2,
    "calcium": 20,
    "calories": 332,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  },
  {
    "barcode": 1001,
    "approved": "Approved",
    "name": "Beans",
    "description": "A serving black beans",
    "sodium": 252,
    "carbohydrates": 13.3,
    "protein": 4,
    "fat": 0,
    "iron": 1.6,
    "calcium": 28,
    "calories": 71,
    "consumesProducts": [],
    "hasVitamins": [],
    "planHas": [],
    "recipeHas": []
  }
]
```

### RE.6

Type: POST

Description: Add a product in a recipe

Url: `/api/Recipe/newproductinrecipe/{number}/{barcode}/{servings}` is the logged patient, `name` is the name of the recipe to add, `barcode`  is the barcode of the product and `servings` is the amount of times the product is in the recipe.

Json: This request doesn't have Json.

Observations: There is no need to specify the recipe number, the backend does that.

Observations: --

## Requests DAILY PLAN

### DP.1

Type: GET
Description: Filter plans by the nutritionist that did it.

Url: `/api/DailyPlan/bynutritionist/{nutritionist}` where `nutritionist` is the email of the nutritionist we filtiring with.

Json: You will get a Json like this one

```Json
[
  {
    "number": 1,
    "name": "Fruit plan",
    "nutritionistEmail": "ju.navarro@gmail.com",
    "nutritionistEmailNavigation": null,
    "follows": [],
    "planHas": []
  },
  {
    "number": 2,
    "name": "Carbohydrate plan",
    "nutritionistEmail": "ju.navarro@gmail.com",
    "nutritionistEmailNavigation": null,
    "follows": [],
    "planHas": []
  }
]
```

Observations: --

### DP.2

Type: POST
Description: Post a plan.

Url: `/api/DailyPlan/{name}/{nutritionistEmail}` where `name` is the name of the new plan and `nutritionistEmail` is the email of the nutritionist who created the plan.

Json: You dont need to send a Json on this request.

Observations: There is not need to specify the plan number, the backend adds a number to the plan. 

### DP.3

Type: GET
Description: Get the nutritional values of a plan. 

Url: `/api/DailyPlan/nutritionalvalue/{planNumber}` where `planNumber` is the number of the plan we want to request.

Json: You eill receive a Json like this one.

```Json
[
  {
    "number": 1,
    "totalSodium": 1036.4,
    "totalCarbohydrates": 478.3,
    "totalProtein": 58.68,
    "totalFat": 14.68,
    "totalIron": 21.3,
    "totalCalcium": 500,
    "totalCalories": 2188
  }
]
```

Observations: Notice that the request returns the result as an array of Json objects. (Entity framework didn't allow me to do it in the correct way). Also consider that is a plan has no products the total values will be returned as NULL.

### DP.4

Type: POST
Description: Associates a client to a plan for a specific month. 

Url: `/api/DailyPlan/followplan/{planNumber}/{patientEmail}/{month}` where `planNumber` is the number of the plan, `patientEmail` is the email of the patient we want to associate and `month` the month where the plan will work.

Json: You will receive a Json like this one.

```Json
[
  {
    "number": 1,
    "totalSodium": 1036.4,
    "totalCarbohydrates": 478.3,
    "totalProtein": 58.68,
    "totalFat": 14.68,
    "totalIron": 21.3,
    "totalCalcium": 500,
    "totalCalories": 2188
  }
]
```

Observations: Use predetermined values for `planNumber` a dropdown to select th month will work great.

## Requests MEASUREMENT

/api/Measurement/{patientEmail}/{number}

### ME.1

Type: GET
Description: Gets the measurements of a patient filtered by the initial and ending date.

Url: `/api/Measurement/filterdates/{patientEmail}/{initialDate}/{endingDate}` where `patientEmail`  is the email of the patient that logged in, `initialDate` and `endingDate` are the dates the request filters on. Dates must have the format "YY-MM-DD" (for example 2020-08-02 for August second 2020).

Json: You will receive a Json like this one.

```Json
[
  {
    "number": 1,
    "date": "2020-08-02T00:00:00",
    "patientEmail": "lu.morales@gmail.com",
    "height": 1.81,
    "weight": 70,
    "hips": 60,
    "waist": 80,
    "neck": 40,
    "fatPercentage": 15,
    "musclePercentage": 85,
    "patientEmailNavigation": null
  },
  {
    "number": 2,
    "date": "2020-09-15T00:00:00",
    "patientEmail": "lu.morales@gmail.com",
    "height": 1.81,
    "weight": 72,
    "hips": 60,
    "waist": 82,
    "neck": 41,
    "fatPercentage": 14,
    "musclePercentage": 86,
    "patientEmailNavigation": null
  },
  {
    "number": 3,
    "date": "2020-10-22T00:00:00",
    "patientEmail": "lu.morales@gmail.com",
    "height": 1.81,
    "weight": 72,
    "hips": 60,
    "waist": 82,
    "neck": 43,
    "fatPercentage": 14,
    "musclePercentage": 86,
    "patientEmailNavigation": null
  }
]
```

Observations: Dates must have the format "YY-MM-DD" (for example 2020-08-02 for August second 2020).

### ME.2

Type: GET

Description: Gets an specific measurement based on the user email and measurement number.

Url: `/api/Measurement/{patientEmail}/{number}` where `patientEmail`  is the email of the patient that logged in and `number` is the number of the measurement.

Json: You will receive a Json like this one.

```Json
{
  "number": 1,
  "date": "2020-08-02T00:00:00",
  "patientEmail": "lu.morales@gmail.com",
  "height": 1.81,
  "weight": 70,
  "hips": 60,
  "waist": 80,
  "neck": 40,
  "fatPercentage": 15,
  "musclePercentage": 85,
  "patientEmailNavigation": null
}
```

Observations: --

### ME.3

Type: POST

Description: Post a measurement.

Url: `/api/Measurement`.

Json: You need to share a Json like this one.

```Json
{
  "date": "2020-12-24",
  "patientEmail": "lu.morales@gmail.com",
  "height": 1.83,
  "weight": 70,
  "hips": 60,
  "waist": 82,
  "neck": 40,
  "fatPercentage": 16,
  "musclePercentage": 84
}
```

Observations: Dates must have the format "YY-MM-DD" (for example 2020-08-02 for August second 2020).


# Nutritec Comments Requests

The base of the url for these requests in the api is `https://nutriteccommentsrg.azurewebsites.net`

## Requests COMMENT

### CO.1

Type: GET

Description: Get all comments in the databse

Url: `/api/Comment/getcomments`.

Json: You get a Json like this one

```Json
[
    {
        "id": "6189cdd2f0048c5344c92376",
        "patientEmail": "lu.morales@gmail.com",
        "day": "Monday",
        "meal": "Breakfast",
        "commentOwnerEmail": "ju.navarro@gmail.com",
        "commentText": "Well done!"
    }
]
```

Observations: --

### CO.2

Type: GET

Description: Get all comments filtered by patient, day and meal

Url: `/api/Comment/getcomments/{patientEmail}/{day}/{meal}` where `patientEmail` stands for the email of the patient that receives the comment (the owner of the consumption data) and `day` and `meal` specify the place where the comment was done.

Json: You get a Json like this one

```Json
[
    {
        "id": "6189cdd2f0048c5344c92376",
        "patientEmail": "lu.morales@gmail.com",
        "day": "Monday",
        "meal": "Breakfast",
        "commentOwnerEmail": "ju.navarro@gmail.com",
        "commentText": "Well done!"
    }
]
```

Observations: --

### CO.3

Type: POST

Description: Post a comment.

Url: `/api/Comment/postcomment`.

Json: You need to share a Json like this one

```Json
{
    "patientEmail": "lu.morales@gmail.com",
    "day": "Monday",
    "meal": "Breakfast",
    "commentOwnerEmail": "lu.morales@gmail.com",
    "commentText": "Thank you!"
}
```

Observations: it is not necessary to specify the `id` field because the database does it automatically. `day` needs to have the value of ('Monday', 'Tuesday', 'Wednesday', 'Thursday' or 'Friday'). The allowed values for `mealtime` are 'Breakfast', 'Morning snack', 'Lunch', 'Afternoon snack', 'Diner'.



