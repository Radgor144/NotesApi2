# NotesApi (.NET)

## Table of Contents

- [General Info](#general-info)
- [Features](#Features)
- [Endpoints](#Endpoints)

## General Info <a name="general-info"></a>

This is a web application built with ASP.NET Core that allows users to manage their notes. It includes features for user
registration, login, and role-based access control. The application uses Microsoft Identity for user authentication and
authorization, and it allows authenticated users to create, edit, delete, and view their personal notes.

## Features <a name="Features"></a>

- **User Registration**: Allows new users to create an account using their email and password.
- **Login/Logout**: Users can log in to access their notes and log out when finished.
- **Note Management**: Authenticated users can create, edit, and delete their notes.
- **Role-based Authorization**: Admin users can access a special admin dashboard, while regular users can only access
  their own notes.
- **Responsive Design**: The application is designed to be responsive and user-friendly on both desktop and mobile
  devices.

## Endpoints <a name="Endpoints"></a>

### HomeController

| Method | URL              | Description                                                                                                                                                                                                    |
|--------|------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Get`  | `/Home/Index`    | Redirects authenticated users to the Notes page; otherwise, redirects to the Register page.                                                                                                                    |
| `Get`  | `/Home/Register` | Displays the registration form.                                                                                                                                                                                |
| `Post` | `/Home/Register` | Handles the form submission for user registration. If the form is valid, it creates a new user and redirects to the login page. If there are errors, they are added to the model, and the form is redisplayed. |
| `Get`  | `/Home/Login`    | Displays the login form.                                                                                                                                                                                       |
| `Post` | `/Home/Login`    | Handles the form submission for logging in. If login is successful, the user is redirected to the Notes page. If the login attempt fails, an error message is displayed.                                       |
| `Post` | `/Home/Logout`   | Logs the user out and redirects them to the login page. This action is protected with the [Authorize] attribute, ensuring it can only be accessed by authenticated users.                                      |

### NotesController

| Method | URL                   | Description                                                                                                                                                |
|--------|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Get`  | `/Notes/Index`        | Retrieves and displays all notes for the currently authenticated user.                                                                                     |
| `Get`  | `/Notes/Details/{id}` | Displays details of a specific note identified by its `id`, ensuring it belongs to the authenticated user. If not, a `NotFound` response is returned.      |
| `Get`  | `/Notes/Create`       | Displays the form to create a new note.                                                                                                                    |
| `Post` | `/Notes/Create`       | Handles the form submission for creating a new note, assigning it to the authenticated user, and saving it to the database.                                |
| `Get`  | `/Notes/Edit/{id}`    | Displays the form to edit a specific note, ensuring that the note belongs to the authenticated user. If not, a `NotFound` response is returned.            |
| `Post` | `/Notes/Edit/{id}`    | Handles the form submission for editing a note. Ensures the note exists and belongs to the authenticated user. If valid, updates the note in the database. |
| `Get`  | `/Notes/Delete/{id}`  | Displays the confirmation page to delete a specific note, ensuring that the note belongs to the authenticated user.                                        |
| `Post` | `/Notes/Delete/{id}`  | Handles the deletion of a note, ensuring the note exists and belongs to the authenticated user. If valid, removes the note from the database.              |

### AdminController

| Method | URL            | Description                                                                                                               |
|--------|----------------|---------------------------------------------------------------------------------------------------------------------------|
| `Get`  | `/Admin/Index` | Displays a welcome message for the Admin user. The route is protected, so only users with the `Admin` role can access it. |

## Database Schema

### `Users` Table (Inherits from `IdentityUser`)

| Column Name    | Data Type | Description                                                         |
|----------------|-----------|---------------------------------------------------------------------|
| `Id`           | `string`  | The unique identifier for the user (inherited from `IdentityUser`). |
| `UserName`     | `string`  | The username of the user (inherited from `IdentityUser`).           |
| `Email`        | `string`  | The email address of the user (inherited from `IdentityUser`).      |
| `FirstName`    | `string`  | The first name of the user.                                         |
| `LastName`     | `string`  | The last name of the user.                                          |
| `PasswordHash` | `string`  | The hashed password (inherited from `IdentityUser`).                |
| `Role`         | `string`  | The role of the user (handled by `IdentityRole`).                   |

### `Notes` Table

| Column Name | Data Type  | Description                                                                         |
|-------------|------------|-------------------------------------------------------------------------------------|
| `Id`        | `int`      | The unique identifier for the note.                                                 |
| `Title`     | `string`   | The title of the note.                                                              |
| `Content`   | `string`   | The content of the note.                                                            |
| `CreatedAt` | `DateTime` | The date and time when the note was created. Defaults to the current date and time. |
| `UserId`    | `string`   | The foreign key linking to the `User` table.                                        |
| `User`      | `User`     | Navigation property to the `User` table.                                            |

### Relationship between `Users` and `Notes`

- Each `User` can have multiple `Notes`, and each `Note` is associated with a single `User`.
- The `Notes` table contains a foreign key (`UserId`) that links each note to a user.

## Example User Login Credentials

| Email               | Password            | Role    | Description                                   |
|---------------------|---------------------|---------|-----------------------------------------------|
| `admin@example.com` | `AdminPassword123!` | `Admin` | Admin user with full access to the dashboard. |
| `user1@example.com` | `UserPassword123!`  | `User`  | Regular user with access to personal notes.   |

