# NutritecBackend
Repository of the Nutritec app backend

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

Observations: All details have to be different than null (except to picture Url). Consider that email, username, nutritionistCode and creditCArdNumber are unique values (must not repeat with the values of other nustritionists).


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

Observations: All details have to be different than null. Consider that email and username.

## Requests PRODUCT

## Requests RECIPE

## Requests DAILY PLAN

## Requests MEASUREMENT



### 1 (Patients)

GET patient by email or username

Url: `​/api/Patient/login/{credential}` where credential is the email or username

POST patient

Url: `/api/Patient`

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


### (Measurement)

POST measurement

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

### (Products)

POST product

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


