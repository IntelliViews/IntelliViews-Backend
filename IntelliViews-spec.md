# IntelliViews OpenAPI Specifications

## Table of Contents

1. [Users](#users)

## Users

Get all users

GET `/users`

Response:

```json
[
  {
    "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
    "username": "bob",
    "email": "bob.bobson@email.com",
    "role": "admin"
  },
  {
    "id": "b97e5d7c-1d5f-4c0d-b657-57140d29b6b1",
    "firstName": "Andy",
    "lastName": "McAnderson",
    "email": "andy.mcanderson@email.com",
    "role": "user"
  }
]
```

---

Get a specified user

GET `/users/{id}`

Response:

```json
{
  "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "username": "bob",
  "email": "bob.bobson@email.com",
  "role": "admin"
}
```

---

Add a user

POST `/users`

Payload:

```json
{
  "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "username": "bob",
  "email": "bob.bobson@email.com",
  "role": "admin"
}
```

Response:

```json
{
  "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "username": "bob",
  "email": "bob.bobson@email.com",
  "role": "admin"
}
```

---

Change the properties of a specified user

PUT `/users/{id}`

Payload:

```json
{
  "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "username": "bob",
  "email": "bob.bobson@email.com",
  "role": "admin"
}
```

Response:

```json
{
  "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "username": "bob",
  "email": "bob.bobson@email.com",
  "role": "admin"
}
```

---

Delete a specified user

DELETE `/users/{id}`

Response:

```json
{
  "id": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "username": "bob",
  "email": "bob.bobson@email.com",
  "role": "admin"
}
```

---

Get a list of all the user's active threads

GET `/users/{id}/threads`

Response:

```json
[
  {
    "userId": "8270a112-05bf-424b-9024-3ab8d097fad1",
    "threadId": "thread_abc123"
  },
  {
    "userId": "8270a112-05bf-424b-9024-3ab8d097fad1",
    "threadId": "thread_abc5261"
  }
]
```

---

Get a specific thread from a user

GET `/users/{user_id}/threads/{thread_id}`

Response:

```json
{
  "userId": "8270a112-05bf-424b-9024-3ab8d097fad1",
  "threadId": "thread_abc123"
}
```
