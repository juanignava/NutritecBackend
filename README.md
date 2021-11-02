# NutritecBackend
Repository of the Nutritec app backend

## Requests

### 1 (Nutritionist)

GET nutritionist by email or username

Url: `​/api​/Nutritionist​/login​/{credential}` where credential is the email or username

GET all nutritionists

Url: `/api/Nutritionist`

POST nutritionist

Url: `/api/Nutritionist`

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

