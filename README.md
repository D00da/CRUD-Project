
# CRUD Web API

Simple CRUD (Create, Read, Update, Delete) API to manage selected tasks in a SQL database.

This project was made to test the REST architecture pattern for APIs and its integration with a SQL database (in my case, PostgreSQL, using the Npgsql package). 


## Parameters

The tasks have the following parameters:

- **Id**: A serial value incremented automatically.

- **title**: A string that defines the task title.

- **status**: An enum value for the task's current status. Can be either "Finished" or "Unfinished".

- **dateCreated**: DateTime for the date the task was add to the database.

- **dateLimit**: DateTime for the date the task's deadline.



## Documentation

#### Returns all tasks

```http
  GET /api/Task
```

#### Returns a task

```http
  GET /api/Task/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Required**. The ID from the task you want |

#### Returns all completed tasks

```http
  GET /api/Task/completed
```
Returns a list with all tasks with the status set to "Finished".

#### Post a new task

```http
  POST /api/Task
```

| Parameter  | Type       | Desciption                                   |
| :---------- | :--------- | :------------------------------------------ |
| `title`      | `string` | **Required**. The title of your task |
| `dateLimit`      | `DateTime` | **Required**. The deadline of your task |

Adds a new task to the database. **NOTE**: Only "title" and "dateLimit" can be added by the user, the others attributes (Id, status, dateCreated) are set automatically.

- **status**: Set as "Unfinished" when created (but can be modified later).

- **dateCreated**: Set as the current date by default.

#### Update a task

```http
  PUT /api/Task/{id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Required**. The ID from the task you want to access |
| `title`      | `string` | **Required**. The title of your task |
| `dateLimit`      | `DateTime` | **Required**. The deadline of your task |
| `status`      | `enum` | **Required**. The status of your task (Finished/Unfinished) |

**NOTE**: "Id" and "dateCreated" can't be modified.

#### Delete a task

```http
  DELETE /api/Task/{id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Required**. The ID from the task you want to delete|




